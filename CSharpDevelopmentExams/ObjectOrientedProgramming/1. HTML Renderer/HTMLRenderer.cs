using System;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.Collections.Generic;

namespace HTMLRenderer
{
	public interface IElement
	{
		string Name { get; }
		string TextContent { get; set; }
		IEnumerable<IElement> ChildElements { get; }
		void AddElement(IElement element);
		void Render(StringBuilder output);
		string ToString();
	}

	public interface ITable : IElement
	{
		int Rows { get; }
		int Cols { get; }
		IElement this[int row, int col] { get; set; }
	}

    public interface IElementFactory
    {
		IElement CreateElement(string name);
		IElement CreateElement(string name, string content);
		ITable CreateTable(int rows, int cols);
    }

    public class HTMLElementFactory : IElementFactory
    {
		public IElement CreateElement(string name)
		{
            return new HTMLElement(name);
		}

		public IElement CreateElement(string name, string content)
		{
            return new HTMLElement(name, content);
		}

		public ITable CreateTable(int rows, int cols)
		{
			return new HTMLTable(rows, cols);
		}
	}

    public class HTMLRendererCommandExecutor
    {
        static void Main()
        {
            string csharpCode = ReadInputCSharpCode();
            CompileAndRun(csharpCode);
        }

        private static string ReadInputCSharpCode()
        {
            StringBuilder result = new StringBuilder();
            string line;
            while ((line = Console.ReadLine()) != "")
            {
                result.AppendLine(line);
            }
            return result.ToString();
        }

        static void CompileAndRun(string csharpCode)
        {
            // Prepare a C# program for compilation
            string[] csharpClass =
            {
                @"using System;
                  using HTMLRenderer;

                  public class RuntimeCompiledClass
                  {
                     public static void Main()
                     {"
                        + csharpCode + @"
                     }
                  }"
            };

            // Compile the C# program
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.GenerateInMemory = true;
            compilerParams.TempFiles = new TempFileCollection(".");
            compilerParams.ReferencedAssemblies.Add("System.dll");
            compilerParams.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
            CSharpCodeProvider csharpProvider = new CSharpCodeProvider();
            CompilerResults compile = csharpProvider.CompileAssemblyFromSource(
                compilerParams, csharpClass);

            // Check for compilation errors
            if (compile.Errors.HasErrors)
            {
                string errorMsg = "Compilation error: ";
                foreach (CompilerError ce in compile.Errors)
                {
                    errorMsg += "\r\n" + ce.ToString();
                }
                throw new Exception(errorMsg);
            }

            // Invoke the Main() method of the compiled class
            Assembly assembly = compile.CompiledAssembly;
            Module module = assembly.GetModules()[0];
            Type type = module.GetType("RuntimeCompiledClass");
            MethodInfo methInfo = type.GetMethod("Main");
            methInfo.Invoke(null, null);
        }
    }

    class HTMLElement : IElement
    {
        private readonly string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        private string textContent;
        public string TextContent
        {
            get
            {
                return textContent;
            }
            set
            {
                if (value != null)
                    textContent = value.ReplaceTags();
                else
                    textContent = value;
            }
        }

        private List<IElement> childElements = new List<IElement>();
        public IEnumerable<IElement> ChildElements
        {
            get
            {
                return childElements;
            }
        }

        public void AddElement(IElement element)
        {
            if (element == null)
                throw new ArgumentNullException();
            this.childElements.Add(element);
        }

        public void Render(StringBuilder output)
        {
            if (output == null)
                throw new ArgumentNullException();
            output.AddSafeFormat("<{0}>", this.Name);
            output.AddSafeFormat("{0}", this.TextContent);
            foreach (var item in this.ChildElements)
            {
                if (item is HTMLTable)
                    output.Append(item.ToString().Trim());
                else
                {
                    output.AddSafeFormat("<{0}>", item.Name);
                    output.AddSafeFormat("{0}", item.TextContent);
                    output.AddSafeFormat("</{0}>", item.Name);
                }
            }
            output.AddSafeFormat("</{0}>", this.Name);
        }

        public HTMLElement(string name)
        {
            this.name = name;
        }

        public HTMLElement(string name, string content)
        {
            this.name = name;
            this.TextContent = content;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            this.Render(sb);
            return sb.ToString().Trim();
        }
    }

    class HTMLTable : ITable
    {
        private IElement[,] table;

        public string Name
        {
            get
            {
                return "table";
            }
        }

        private string textContent = string.Empty;
        public string TextContent
        {
            get
            {
                return textContent;
            }
            set
            {
                if (value != null)
                    textContent = value.ReplaceTags();
                else
                    textContent = value;
            }
        }

        private List<IElement> childElements = new List<IElement>();
        public IEnumerable<IElement> ChildElements
        {
            get
            {
                return childElements;
            }
        }

        public void AddElement(IElement element)
        {
            if (element == null)
                throw new ArgumentNullException();
            this.childElements.Add(element);
        }

        public void Render(StringBuilder output)
        {
            if (output == null)
                throw new ArgumentNullException();
            output.AddSafeFormat("<{0}>", this.Name);
            for (int row = 0; row < this.Rows; row++)
            {
                output.Append("<tr>");
                for (int col = 0; col < this.Cols; col++)
                {
                    output.Append("<td>");
                    output.Append(table[row, col].ToString().Trim());
                    output.Append("</td>");
                }
                output.Append("</tr>");
            }
            output.AddSafeFormat("</{0}>", this.Name);
        }

        private int rows = 0;
        public int Rows
        {
            get
            {
                return rows;
            }
        }

        private int cols = 0;
        public int Cols
        {
            get
            {
                return cols;
            }
        }

        public IElement this[int row, int col]
        {
            get
            {
                return this.table[row, col];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                this.table[row, col] = value;
            }
        }

        public HTMLTable(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            this.table = new IElement[this.Rows, this.Cols];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            this.Render(sb);
            return sb.ToString().Trim();
        }
    }

    static class Extensions
    {
        public static void AddSafeFormat(this StringBuilder source, string format, string value)
        {
            if (!string.IsNullOrEmpty(value))
                source.AppendFormat(format, value);
        }

        public static string  ReplaceTags(this string source)
        {
            return source.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }
    }
}
