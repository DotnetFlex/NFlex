using CodeGenerator.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Entities
{
    public class TemplateModel
    {
        public string DatabaseName { get; set; }
        public List<TableSchema> Tables { get; set; }
        public TableSchema Table { get; set; }
        public string TemplateFile { get; set; }
    }

    public class TableModel
    {
        public string DatabaseName { get; set; }
        public TableSchema Table { get; set; }
        public string TemplateFile { get; set; }
    }

    public class DatabaseModel
    {
        public string DatabaseName { get; set; }
        public List<TableSchema> Tables { get; set; }
        public string TemplateFile { get; set; }
    }
}
