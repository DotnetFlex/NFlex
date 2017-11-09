using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Schemas
{
    [Serializable]
    public class DatabaseSchema
    {
        public TableSchema[] Tables { get; internal set; }
        public TableSchema[] Views { get; internal set; }
    }

    [Serializable]
    public class TableSchema
    {
        public bool IsView { get; set; }

        private List<ColumnSchema> columns = new List<ColumnSchema>();
        private List<ForeignKeySchema> fks = new List<ForeignKeySchema>();

        private List<IRelationSchema> children = new List<IRelationSchema>();

        public void AddColumn(ColumnSchema column)
        {
            columns.Add(column);
        }

        public void AddFK(ForeignKeySchema fk)
        {
            fks.Add(fk);
            (fk.OtherTable as TableSchema).children.Add(new ForeignKeySchema { OtherTable = this, OtherKey = fk.ThisKey, ThisTable = fk.OtherTable, ThisKey = fk.OtherKey });
        }

        public string Schema { get; set; }
        public string TableName { get; set; }
        public ColumnSchema[] AllColumns { get { return columns.ToArray(); } }
        public ColumnSchema[] Columns { get { return columns.Where(p => !p.IsPrimaryKey).ToArray(); } }
        public ForeignKeySchema[] ForeignKeys { get { return fks.ToArray(); } }

        public ColumnSchema[] PrimaryKeys { get { return columns.Where(p => p.IsPrimaryKey).ToArray(); } }
        public IRelationSchema[] Children { get { return children.OfType<IRelationSchema>().ToArray(); } }

        public override string ToString()
        {
            return TableName;
        }
    }

    [Serializable]
    public class ColumnSchema
    {
        public string ColumnName { get; set; }
        public TableSchema Table { get; set; }
        public bool IsUniqule { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsNullable { get; set; }

        public bool IsComputed { get; set; }
        public bool IsGenerated { get; set; }
        public Type Type { get; set; }
        public DBType DbType { get; set; }

        public string DefaultValue { get; set; }
        public int Length { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public int Order { get; set; }

        public string Comment { get; set; }

        public string ColumnType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    [Serializable]
    public class ForeignKeySchema : IRelationSchema
    {
        public string Name { get; internal set; }
        public TableSchema ThisTable { get; internal set; }
        public ColumnSchema ThisKey { get; internal set; }
        public TableSchema OtherTable { get; internal set; }
        public ColumnSchema OtherKey { get; internal set; }
    }

    public interface IRelationSchema
    {
        /// <summary>
        /// This Table
        /// </summary>
        TableSchema ThisTable { get; }
        /// <summary>
        /// ThisKey
        /// </summary>
        ColumnSchema ThisKey { get; }

        /// <summary>
        /// OtherTable
        /// </summary>
        TableSchema OtherTable { get; }

        /// <summary>
        /// OtherKey
        /// </summary>
        ColumnSchema OtherKey { get; }
    }
}
