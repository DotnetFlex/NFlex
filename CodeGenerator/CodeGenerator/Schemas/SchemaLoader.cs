using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Schemas
{
    public class SchemaLoader
    {
        #region 内部类
        class ColumnData
        {
            public string ColumnName { get; set; }
            public string DataType { get; set; }
            public int Length { get; set; }
            public int Precision { get; set; }
            public int Scale { get; set; }
            public bool IsNullable { get; set; }
            public string DefaultValue { get; set; }
            public bool IsIdentity { get; set; }
            public int ObjectId { get; set; }
            public string Schema { get; set; }
            public string TableName { get; set; }
        }
        class PrimaryData
        {
            public string IndexName { get; set; }
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string Schema { get; set; }

        }
        class ForeignKeyData
        {
            public string ForeignTableName { get; set; }
            public string ForeignTableSchema { get; set; }
            public string ForeignColumnName { get; set; }
            public string PrimaryTableName { get; set; }
            public string PrimaryTableSchema { get; set; }
            public string PrimaryColumnName { get; set; }
            public string ForeignKeyName { get; set; }

        }
        class DefaultData
        {
            public string TableName { get; set; }
            public string Schema { get; set; }
            public string ColumnName { get; set; }
            public string DefaultValue { get; set; }
        }
        class CommentData
        {
            public string TableName { get; set; }
            public string Schema { get; set; }
            public string PropertyName { get; set; }
            public string Comment { get; set; }
            public string ColumnName { get; set; }
        }

        
        #endregion

        #region SqlStr
        #region 获取表
        private string GetTableSql = @"
            --所有表
            SELECT  OBJECT_NAME(so.id) AS [TableName] ,
                    SCHEMA_NAME(so.uid) AS [Schema]
            FROM    dbo.sysobjects so
                    LEFT JOIN ( SELECT  s.groupname AS file_group ,
                                        i.id AS id
                                FROM    dbo.sysfilegroups s
                                        INNER JOIN dbo.sysindexes i ON i.groupid = s.groupid
                                WHERE   i.indid < 2
                              ) AS fg ON so.id = fg.id
            WHERE   so.type = N'U'
                    AND PERMISSIONS(so.id) & 4096 <> 0
                    AND OBJECTPROPERTY(so.id, N'IsMSShipped') = 0
                    AND NOT EXISTS ( SELECT *
                                     FROM   sys.extended_properties
                                     WHERE  major_id = so.id
                                            AND name = 'microsoft_database_tools_support'
                                            AND value = 1 )
            ORDER BY SCHEMA_NAME(so.uid) ,
                    OBJECT_NAME(so.id);
        ";
        #endregion
        #region 获取列
        private string GetColumnSql = @"
                SELECT  clmns.[name] AS ColumnName ,
                        usrt.[name] AS [DataType] ,
                        CAST(CASE WHEN baset.[name] IN ( N'char', N'varchar', N'binary',
                                                         N'varbinary', N'nchar', N'nvarchar' )
                                  THEN clmns.prec
                                  ELSE clmns.length
                             END AS INT) AS [Length] ,
                        CAST(clmns.xprec AS TINYINT) AS [Precision] ,
                        CAST(clmns.xscale AS INT) AS [Scale] ,
                        CAST(clmns.isnullable AS BIT) AS [IsNullable] ,
                        defaults.text AS [DefaultValue] ,
                        CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') AS INT) AS [IsIdentity] ,
                        CAST(clmns.colid AS INT) AS ObjectId ,
                        SCHEMA_NAME(tbl.uid) AS [Schema] ,
                        tbl.[name] AS [TableName]
                FROM    dbo.sysobjects AS tbl WITH ( NOLOCK )
                        INNER JOIN dbo.syscolumns AS clmns WITH ( NOLOCK ) ON clmns.id = tbl.id
                        LEFT JOIN dbo.systypes AS usrt WITH ( NOLOCK ) ON usrt.xusertype = clmns.xusertype
                        LEFT JOIN dbo.sysusers AS sclmns WITH ( NOLOCK ) ON sclmns.uid = usrt.uid
                        LEFT JOIN dbo.systypes AS baset WITH ( NOLOCK ) ON baset.xusertype = clmns.xtype
                                                                           AND baset.xusertype = baset.xtype
                        LEFT JOIN dbo.syscomments AS defaults WITH ( NOLOCK ) ON defaults.id = clmns.cdefault
                        LEFT JOIN dbo.syscomments AS cdef WITH ( NOLOCK ) ON cdef.id = clmns.id
                                                                             AND cdef.number = clmns.colid
                WHERE   ( tbl.[type] = 'U' )
                ORDER BY tbl.[name] ,
                        clmns.colorder;
            ";
        #endregion
        #region 获取主键
        private string GetPrimaryKeySql = @"
            --所有主键
            SELECT  [i].[name] AS [IndexName] ,
                    [o].[name] AS [TableName] ,
                    [os].[name] AS [Schema] ,
                    [c].[name] AS [ColumnName]
            FROM    [sys].[indexes] i WITH ( NOLOCK )
                    LEFT JOIN [sys].[objects] [o] WITH ( NOLOCK ) ON [o].[object_id] = [i].[object_id]
                    LEFT JOIN [sys].[schemas] [os] WITH ( NOLOCK ) ON [os].[schema_id] = [o].[schema_id]
                    LEFT JOIN [sys].[index_columns] [ic] WITH ( NOLOCK ) ON [ic].[object_id] = [i].[object_id]
                                                                          AND [ic].[index_id] = [i].[index_id]
                                                                          AND [ic].[is_included_column] = 0
                    LEFT JOIN [sys].[columns] [c] WITH ( NOLOCK ) ON [c].[object_id] = [ic].[object_id]
                                                                     AND [c].[column_id] = [ic].[column_id]
                    LEFT JOIN [sys].[stats] [s] WITH ( NOLOCK ) ON [s].[object_id] = [i].[object_id]
                                                                   AND [s].[name] = [i].[name]
            WHERE   [i].[type] IN ( 0, 1, 2, 3 )
                    AND [o].[type] IN ( 'U', 'V', 'TF' )
                    AND [i].[is_primary_key] = 1
            ORDER BY [i].[object_id] ,
                    [i].[name] ,
                    [ic].[key_ordinal] ,
                    [ic].[index_column_id];
        ";

        #endregion
        #region 获取外键
        private string GetForeignKeySql = @"
            --所有外键
            SELECT  [fs].[name] AS [ForeignTableName] ,
                    [fschemas].[name] AS [ForeignTableSchema] ,
                    [rs].[name] AS [PrimaryTableName] ,
                    [rschemas].[name] AS [PrimaryTableSchema] ,
                    [sfk].[name] AS [ForeignKeyName] ,
                    [fc].[name] AS [ForeignColumnName] ,
                    [rc].[name] AS [PrimaryColumnName]
            FROM    [sys].[foreign_keys] AS [sfk] WITH ( NOLOCK )
                    INNER JOIN [sys].[foreign_key_columns] AS [sfkc] WITH ( NOLOCK ) ON [sfk].[object_id] = [sfkc].[constraint_object_id]
                    INNER JOIN [sys].[objects] [fs] WITH ( NOLOCK ) ON [sfk].[parent_object_id] = [fs].[object_id]
                    INNER JOIN [sys].[objects] [rs] WITH ( NOLOCK ) ON [sfk].[referenced_object_id] = [rs].[object_id]
                    LEFT JOIN [sys].[schemas] [fschemas] WITH ( NOLOCK ) ON [fschemas].[schema_id] = [fs].[schema_id]
                    LEFT JOIN [sys].[schemas] [rschemas] WITH ( NOLOCK ) ON [rschemas].[schema_id] = [rs].[schema_id]
                    INNER JOIN [sys].[columns] [fc] WITH ( NOLOCK ) ON [sfkc].[parent_column_id] = [fc].[column_id]
                                                                       AND [fc].[object_id] = [sfk].[parent_object_id]
                    INNER JOIN [sys].[columns] [rc] WITH ( NOLOCK ) ON [sfkc].[referenced_column_id] = [rc].[column_id]
                                                                       AND [rc].[object_id] = [sfk].[referenced_object_id]
            WHERE   [sfk].[is_ms_shipped] = 0 --Added to check for replication.
            ORDER BY [sfk].[name] ,
                    [sfkc].[constraint_column_id];
        ";
        #endregion
        #region 获取默认值
        private string GetDefaultSql = @"
            SELECT
              [tbl].[name] AS [TableName],
              SCHEMA_NAME([tbl].[uid]) AS [Schema], 
              [clmns].[name] AS [ColumnName],
              [constdef].[text] AS DefaultValue
            FROM
              dbo.sysobjects AS tbl WITH (NOLOCK)
              INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON [clmns].[id] = [tbl].[id]
              INNER JOIN dbo.sysconstraints const WITH (NOLOCK) ON [clmns].[id] = [const].[id] and [clmns].[colid] = [const].[colid]
              LEFT OUTER JOIN dbo.syscomments constdef WITH (NOLOCK) ON [const].[constid] = [constdef].[id]
            WHERE ([const].[status] & 5 = 5)
        ";

        #endregion
        #region 获取字段备注
        private string GetCommentSql = @"
            --字段描述
            SELECT  
                    [so].[name] AS [TableName], 
                    SCHEMA_NAME([so].[schema_id]) AS [Schema],  
                    [sp].[name] AS [PropertyName], 
                    [sp].[value] AS [Comment],
                    CASE [sp].[class]
	                    WHEN 2 THEN [spar].[name]
	                    ELSE [sc].[name]
                    END AS [ColumnName]
            FROM [sys].[extended_properties] AS [sp] WITH (NOLOCK) 
	            LEFT JOIN [sys].[objects] AS [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id]
	            LEFT JOIN [sys].[columns] AS [sc] WITH (NOLOCK) ON [sc].[object_id] = [sp].[major_id] AND [sc].[column_id] = [sp].[minor_id]
	            LEFT JOIN [sys].[parameters] AS [spar] WITH (NOLOCK) ON [spar].[object_id] = [sp].[major_id] AND [spar].[parameter_id] = [sp].[minor_id]
        ";
        #endregion
        #endregion

        public List<TableSchema> Load(ConnectionInfo connectionInfo,string dbName)
        {
            List<TableSchema> tableDatas;
            List<ColumnData> columnDatas;
            List<PrimaryData> primaryDatas;
            List<ForeignKeyData> foreignKeyDatas;
            //List<DefaultData> defaultDatas;
            List<CommentData> commentDatas;

            #region 读取原数据
            using (SqlConnection conn = new SqlConnection(connectionInfo.GetConnectionString(dbName)))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(GetTableSql, conn);
                using (var reader = cmd.ExecuteReader())
                    tableDatas = reader.ToList<TableSchema>();

                cmd = new SqlCommand(GetColumnSql, conn);
                using (var reader = cmd.ExecuteReader())
                    columnDatas = reader.ToList<ColumnData>();

                cmd = new SqlCommand(GetPrimaryKeySql, conn);
                using (var reader = cmd.ExecuteReader())
                    primaryDatas = reader.ToList<PrimaryData>();

                cmd = new SqlCommand(GetForeignKeySql, conn);
                using (var reader = cmd.ExecuteReader())
                    foreignKeyDatas = reader.ToList<ForeignKeyData>();

                //cmd = new SqlCommand(GetDefaultSql, conn);
                //using (var reader = cmd.ExecuteReader())
                //    defaultDatas = reader.ToList<DefaultData>();

                cmd = new SqlCommand(GetCommentSql, conn);
                using (var reader = cmd.ExecuteReader())
                    commentDatas = reader.ToList<CommentData>();
            }
            #endregion



            Dictionary<string, TableSchema> tables = PopulateTables(tableDatas, columnDatas,dbName);
            PopulatePrimaryKeys(tables, primaryDatas);
            PopulateComments(tables, commentDatas);
            PopulateForeignKeys(tables, foreignKeyDatas);


            return tables.Select(t=>t.Value).ToList();
        }

        public List<TableInfo> GetTables(string connStr)
        {
            
            List<TableInfo> list;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(GetTableSql, conn);
                using (var reader = cmd.ExecuteReader())
                    list = reader.ToList<TableInfo>();
            }
            return list;
        }

        private Dictionary<string, TableSchema> PopulateTables(List<TableSchema> tables,List<ColumnData> columns,string dbName)
        {
            var dic = new Dictionary<string, TableSchema>();
            foreach(var table in tables)
            {
                dic.Add(table.ToString(), table);
                table.DatabaseName = dbName;
                var tableColumns = columns.Where(t => t.TableName == table.TableName && t.Schema == table.Schema).OrderBy(t=>t.ObjectId).ToList();
                foreach(var c in tableColumns)
                {
                    table.AddColumn(ToColumn(c));
                }
            }
            return dic;
        }
        private void PopulatePrimaryKeys(Dictionary<string,TableSchema> tables ,List<PrimaryData> keys)
        {
            foreach(var data in keys)
            {
                TableSchema table = null;
                if (tables.TryGetValue(string.Format("{0}.{1}",data.Schema, data.TableName),out table))
                {
                    var primaryKey = table.Columns.FirstOrDefault(t => t.ColumnName == data.ColumnName);
                    primaryKey.IsPrimaryKey = true;
                }
            }
        }

        private void PopulateComments(Dictionary<string, TableSchema> tables, List<CommentData> comments)
        {
            foreach (var data in comments.Where(t=>t.PropertyName.ToLower()== "ms_description"))
            {
                TableSchema table = null;
                if (tables.TryGetValue(string.Format("{0}.{1}", data.Schema, data.TableName), out table))
                {
                    if(data.ColumnName==null)
                    {
                        table.Comment = data.Comment;
                        continue;
                    }
                    var column = table.AllColumns.FirstOrDefault(t => t.ColumnName == data.ColumnName);
                    column.Comment = data.Comment;
                }
            }
        }

        private void PopulateForeignKeys(Dictionary<string, TableSchema> tables,List<ForeignKeyData> fks)
        {
            foreach(var data in fks)
            {
                TableSchema thisTable = null;
                TableSchema otherTable = null;
                ColumnSchema thisKey = null;
                ColumnSchema otherKey = null;

                string thisName = string.Format("{0}.{1}", data.ForeignTableSchema, data.ForeignTableName);
                string otherName = string.Format("{0}.{1}", data.PrimaryTableSchema, data.PrimaryTableName);

                if (tables.TryGetValue(thisName, out thisTable))
                    thisKey = thisTable.AllColumns.FirstOrDefault(t => t.ColumnName == data.ForeignColumnName);
                if (tables.TryGetValue(otherName, out otherTable))
                    otherKey = otherTable.AllColumns.FirstOrDefault(t => t.ColumnName == data.PrimaryColumnName);

                thisTable.AddForeignKey(new ForeignKeySchema
                {
                    Name = data.ForeignKeyName,
                    ThisKey = thisKey,
                    ThisTable = thisTable,
                    OtherKey = otherKey,
                    OtherTable = otherTable
                });
            }
        }

        private ColumnSchema ToColumn(ColumnData data)
        {
            ColumnSchema cs = new Schemas.ColumnSchema
            {
                IsIdentity = data.IsIdentity,
                DefaultValue = data.DefaultValue,
                IsNullable = data.IsNullable,
                Length = data.Length,
                ColumnName = data.ColumnName,
                Precision = data.Precision,
                Scale = data.Scale,
                SortId = data.ObjectId,
                Comment=""
            };
            cs.TypeInfo = TypeMapping.GetTypeInfo(data.DataType);
            return cs;
        }
    }

    public static class SqlReaderExt
    {
        public static List<T> ToList<T>(this SqlDataReader dr) where T:new()
        {
            List<T> list = new List<T>();

            while (dr.Read())
            {
                T t=new T();
                foreach (var propertyInfo in t.GetType().GetProperties())
                {
                    var propertyName = propertyInfo.Name;
                    if(!ExistsField(dr,propertyName)) continue;
                    if (!propertyInfo.CanWrite) continue;
                    var value = dr[propertyName];
                    if (value != DBNull.Value) propertyInfo.SetValue(t,CheckType( value,propertyInfo.PropertyType), null);
                }
                list.Add(t);
            }

            return list;
        }

        private static bool ExistsField(SqlDataReader dr, string columnName)
        {
            dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName='"+columnName+"'";
            return (dr.GetSchemaTable().DefaultView.Count > 0);
        }

        private static object CheckType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }
    }

    class TypeMapping
    {

        private static Dictionary<string, TypeInfo> _mappingList;
        static TypeMapping()
        {
            _mappingList = new Dictionary<string, TypeInfo>()
            {
                {"bit",new TypeInfo {DbType=SqlDbType.Bit,ClrType=typeof(bool),DefineName="bool"} },
                {"tinyint",new TypeInfo {DbType=SqlDbType.TinyInt,ClrType=typeof(byte),DefineName="int"} },
                {"smallint",new TypeInfo {DbType=SqlDbType.SmallInt,ClrType=typeof(short),DefineName="int"} },
                {"int",new TypeInfo {DbType=SqlDbType.Int,ClrType=typeof(int),DefineName="int"} },
                {"bigint",new TypeInfo {DbType=SqlDbType.BigInt,ClrType=typeof(long),DefineName="long"} },
                {"real",new TypeInfo {DbType=SqlDbType.Real,ClrType=typeof(float),DefineName="float"} },
                {"float",new TypeInfo {DbType=SqlDbType.Float,ClrType=typeof(double),DefineName="double"} },
                {"money",new TypeInfo {DbType=SqlDbType.Money,ClrType=typeof(decimal),DefineName="decimal"} },
                {"smallmoney",new TypeInfo {DbType=SqlDbType.SmallMoney,ClrType=typeof(decimal),DefineName="decimal"} },
                {"timestamp",new TypeInfo {DbType=SqlDbType.Timestamp,ClrType=typeof(byte[]),DefineName="byte[]"} },
                {"datetime",new TypeInfo {DbType=SqlDbType.DateTime,ClrType=typeof(DateTime),DefineName="DateTime"} },
                {"date",new TypeInfo {DbType=SqlDbType.Date,ClrType=typeof(DateTime),DefineName="DateTime"} },
                {"time",new TypeInfo {DbType=SqlDbType.Time,ClrType=typeof(DateTime),DefineName="DateTime"} },
                {"smalldatetime",new TypeInfo {DbType=SqlDbType.SmallDateTime,ClrType=typeof(DateTime),DefineName="DateTime"} },
                {"char",new TypeInfo {DbType=SqlDbType.Char,ClrType=typeof(string),DefineName="string"} },
                {"varchar",new TypeInfo {DbType=SqlDbType.VarChar,ClrType=typeof(string),DefineName="string"} },
                {"nchar",new TypeInfo {DbType=SqlDbType.NChar,ClrType=typeof(string),DefineName="string"} },
                {"nvarchar",new TypeInfo {DbType=SqlDbType.NVarChar,ClrType=typeof(string),DefineName="string"} },
                {"text",new TypeInfo {DbType=SqlDbType.Text,ClrType=typeof(string),DefineName="string"} },
                {"ntext",new TypeInfo {DbType=SqlDbType.NText,ClrType=typeof(string),DefineName="string"} },
                {"image",new TypeInfo {DbType=SqlDbType.Image,ClrType=typeof(byte[]),DefineName="byte[]"} },
                {"binary",new TypeInfo {DbType=SqlDbType.Binary,ClrType=typeof(byte[]),DefineName="byte[]"} },
                {"uniqueidentifier",new TypeInfo {DbType=SqlDbType.UniqueIdentifier,ClrType=typeof(Guid),DefineName="Guid"} },
                {"decimal",new TypeInfo {DbType=SqlDbType.Decimal,ClrType=typeof(decimal),DefineName="decimal"} },
                {"varbinary",new TypeInfo {DbType=SqlDbType.VarBinary,ClrType=typeof(byte[]),DefineName="byte[]"} },
                {"xml",new TypeInfo {DbType=SqlDbType.Xml,ClrType=typeof(byte[]),DefineName="byte[]"} }
            };
        }

        public static TypeInfo GetTypeInfo(string dbType)
        {
            TypeInfo mappingInfo = null;
            _mappingList.TryGetValue(dbType, out mappingInfo);
            return mappingInfo;
        }
    }
}
