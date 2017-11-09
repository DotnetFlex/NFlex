using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Schemas
{
    /// <summary>
    /// 表
    /// </summary>
    [Serializable]
    public class TableSchema:TableInfo
    {
        #region 字段
        private List<ColumnSchema> _columns = new List<ColumnSchema>();
        private List<ForeignKeySchema> _foreignKeys = new List<ForeignKeySchema>();
        private List<TableSchema> _children = new List<TableSchema>();
        #endregion

        public string DatabaseName { get; set; }
        /// <summary>
        /// 列（不包含主键列）
        /// </summary>
        public List<ColumnSchema> Columns { get { return _columns.Where(t=>!t.IsPrimaryKey).ToList(); } }
        /// <summary>
        /// 所有列（包含主键列）
        /// </summary>
        public List<ColumnSchema> AllColumns { get { return _columns; } }
        /// <summary>
        /// 主键
        /// </summary>
        public List<ColumnSchema> PrimaryKeys { get { return _columns.Where(t => t.IsPrimaryKey).ToList(); } }
        /// <summary>
        /// 外键
        /// </summary>
        public List<ForeignKeySchema> ForeignKeys { get { return _foreignKeys; } }

        public List<TableSchema> Childrens { get { return _children; } }

        public bool IsRelation { get { return PrimaryKeys.Count == AllColumns.Count; } }

        public void AddColumn(ColumnSchema col)
        {
            _columns.Add(col);
        }

        public void AddForeignKey(ForeignKeySchema fk)
        {
            _foreignKeys.Add(fk);
            fk.OtherTable._children.Add(this);
            var col = AllColumns.Where(t => t.ColumnName == fk.ThisKey.ColumnName).First();
            col.IsForeignKey = true;
        }
    }

    [Serializable]
    public class TableInfo
    {
        /// <summary>
        /// 所属框架
        /// </summary>
        public string Schema { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        public string Comment { get; set; }
        public override string ToString()
        {
            return string.Format("{0}.{1}", Schema, TableName);
        }
    }

    /// <summary>
    /// 字段
    /// </summary>
    [Serializable]
    public class ColumnSchema
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 类型信息
        /// </summary>
        public TypeInfo TypeInfo { get; set; }
        public SqlDbType DbType { get { return TypeInfo.DbType; } }
        public Type Type { get { return TypeInfo.ClrType; } }
        public string DefineType {
            get
            {
                if (!IsNullable) return TypeInfo.DefineName;
                if (TypeInfo.DefineName == "string") return TypeInfo.DefineName;
                else return TypeInfo.DefineName + "?";

            }
        }
        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 是否是外键
        /// </summary>
        public bool IsForeignKey { get; set; }
        /// <summary>
        /// 是否是自动增长列
        /// </summary>
        public bool IsIdentity { get; set; }
        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 浮点精度
        /// </summary>
        public int Precision { get; set; }
        /// <summary>
        /// 小数点位数
        /// </summary>
        public int Scale { get; set; }
        /// <summary>
        /// 字段说明
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 字段顺序
        /// </summary>
        public int SortId { get; set; }

        public override string ToString()
        {
            return ColumnName;
        }
    }

    /// <summary>
    /// 外键
    /// </summary>
    [Serializable]
    public class ForeignKeySchema : IRelationSchema
    {
        /// <summary>
        /// 键名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 当前表
        /// </summary>
        public TableSchema ThisTable { get; set; }
        /// <summary>
        /// 当前表字段
        /// </summary>
        public ColumnSchema ThisKey { get; set; }
        /// <summary>
        /// 目标表
        /// </summary>
        public TableSchema OtherTable { get; set; }
        /// <summary>
        /// 目标字段
        /// </summary>
        public ColumnSchema OtherKey { get; set; }
    }

    /// <summary>
    /// 引用
    /// </summary>
    public interface IRelationSchema
    {
        /// <summary>
        /// 当前表
        /// </summary>
        TableSchema ThisTable { get; set; }
        /// <summary>
        /// 当前表字段
        /// </summary>
        ColumnSchema ThisKey { get; set; }
        /// <summary>
        /// 目标表
        /// </summary>
        TableSchema OtherTable { get; set; }
        /// <summary>
        /// 目标字段
        /// </summary>
        ColumnSchema OtherKey { get; set; }
    }

    [Serializable]
    public class TypeInfo
    {
        public SqlDbType DbType { get; set; }
        public Type ClrType { get; set; }
        public string DefineName { get; set; }
    }
}
