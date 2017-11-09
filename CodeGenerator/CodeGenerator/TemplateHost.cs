using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using System.IO;
using CodeGenerator.Schemas;
using System.Data;

namespace CodeGenerator
{
    [Serializable]
    public class TemplateHost : ITextTemplatingEngineHost
    {
        #region 字段
        private CompilerErrorCollection errorCollection;
        private Encoding fileEncoding = Encoding.UTF8;
        private string fileExtension = ".cs";
        internal string templateFile;
        #endregion

        #region 属性
        public TableSchema TableInfo { get; set; }
        /// <summary>
        /// 编译错误对象集合
        /// </summary>
        public CompilerErrorCollection ErrorCollection
        {
            get { return this.errorCollection; }
        }

        /// <summary>
        /// 文件编码方式
        /// </summary>
        public Encoding FileEncoding
        {
            get { return this.fileEncoding; }
        }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension
        {
            get { return this.fileExtension; }
        }

        /// <summary>
        /// 模版需调用的其他程序集引用
        /// </summary>
        public IList<string> StandardAssemblyReferences
        {
            get
            {
                return new string[] {
            typeof(Uri).Assembly.Location,
            typeof(TableSchema).Assembly.Location,
            typeof(TemplateHost).Assembly.Location,
            typeof(SqlDbType).Assembly.Location
        };
            }
        }

        /// <summary>
        /// 模版调用标准程序集引用
        /// </summary>
        public IList<string> StandardImports
        {
            get
            {
                return new string[] {
                    "System",
                    "System.Text",
                    "System.Data",
                    "System.Collections.Generic",
                    "CodeGenerator",
                    "CodeGenerator.Schemas"
                };
            }
        }

        /// <summary>
        /// 模版文件
        /// </summary>
        public string TemplateFile
        {
            get { return this.templateFile; }
            set { this.templateFile = value; }
        }
        
        #endregion

        #region 方法
        public object GetHostOption(string optionName)
        {
            string str;
            return (((str = optionName) != null) && (str == "CacheAssemblies"));
        }

        public bool LoadIncludeText(string requestFileName, out string content, out string location)
        {
            content = string.Empty;
            location = string.Empty;
            if (File.Exists(requestFileName))
            {
                content = File.ReadAllText(requestFileName);
                return true;
            }
            return false;
        }

        public void LogErrors(CompilerErrorCollection errors)
        {
            this.errorCollection = errors;
        }

        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            return AppDomain.CreateDomain("Generation App Domain");
        }

        public string ResolveAssemblyReference(string assemblyReference)
        {
            if (File.Exists(assemblyReference))
            {
                return assemblyReference;
            }
            string path = Path.Combine(Path.GetDirectoryName(this.TemplateFile), assemblyReference);
            if (File.Exists(path))
            {
                return path;
            }
            return "";
        }

        public Type ResolveDirectiveProcessor(string processorName)
        {
            string.Compare(processorName, "XYZ", StringComparison.OrdinalIgnoreCase);
            throw new Exception("没有找到指令处理器");
        }

        public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            if (directiveId == null)
            {
                throw new ArgumentNullException("the directiveId cannot be null");
            }
            if (processorName == null)
            {
                throw new ArgumentNullException("the processorName cannot be null");
            }
            if (parameterName == null)
            {
                throw new ArgumentNullException("the parameterName cannot be null");
            }
            return string.Empty;
        }

        public string ResolvePath(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("the file name cannot be null");
            }
            if (!File.Exists(fileName))
            {
                string path = Path.Combine(Path.GetDirectoryName(this.TemplateFile), fileName);
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return fileName;
        }

        public void SetFileExtension(string extension)
        {
            this.fileExtension = extension;
        }

        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
            this.fileEncoding = encoding;
        }
        #endregion
    }
}
