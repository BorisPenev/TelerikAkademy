using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DocumentSystem
{
    public interface IDocument
    {
        Dictionary<string, string> Properties { get; set; }
        string Name { get; }
        string Content { get; }
        void LoadProperty(string key, string value);
        void SaveAllProperties(IList<KeyValuePair<string, object>> output);
        string ToString();
    }

    public interface IEditable
    {
        void ChangeContent(string newContent);
    }

    public interface IEncryptable
    {
        bool IsEncrypted { get; }
        void Encrypt();
        void Decrypt();
    }

    public class DocumentSystem
    {
        static List<Document> documents = new List<Document>();

        static void Main()
        {
            IList<string> allCommands = ReadAllCommands();
            ExecuteCommands(allCommands);
        }

        private static void AddDocument(Document td)
        {
            if (documents.AddSafe(td))
                Console.WriteLine("Document added: {0}", td.Name);
            else
                Console.WriteLine("Document has no name");
        }

        private static IList<string> ReadAllCommands()
        {
            List<string> commands = new List<string>();
            while (true)
            {
                string commandLine = Console.ReadLine();
                if (commandLine == "")
                {
                    // End of commands
                    break;
                }
                commands.Add(commandLine);
            }
            return commands;
        }

        private static void ExecuteCommands(IList<string> commands)
        {
            foreach (var commandLine in commands)
            {
                int paramsStartIndex = commandLine.IndexOf("[");
                string cmd = commandLine.Substring(0, paramsStartIndex);
                int paramsEndIndex = commandLine.IndexOf("]");
                string parameters = commandLine.Substring(
                    paramsStartIndex + 1, paramsEndIndex - paramsStartIndex - 1);
                ExecuteCommand(cmd, parameters);
            }
        }

        private static void ExecuteCommand(string cmd, string parameters)
        {
            string[] cmdAttributes = parameters.Split(
                new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (cmd == "AddTextDocument")
            {
                AddTextDocument(cmdAttributes);
            }
            else if (cmd == "AddPDFDocument")
            {
                AddPdfDocument(cmdAttributes);
            }
            else if (cmd == "AddWordDocument")
            {
                AddWordDocument(cmdAttributes);
            }
            else if (cmd == "AddExcelDocument")
            {
                AddExcelDocument(cmdAttributes);
            }
            else if (cmd == "AddAudioDocument")
            {
                AddAudioDocument(cmdAttributes);
            }
            else if (cmd == "AddVideoDocument")
            {
                AddVideoDocument(cmdAttributes);
            }
            else if (cmd == "ListDocuments")
            {
                ListDocuments();
            }
            else if (cmd == "EncryptDocument")
            {
                EncryptDocument(parameters);
            }
            else if (cmd == "DecryptDocument")
            {
                DecryptDocument(parameters);
            }
            else if (cmd == "EncryptAllDocuments")
            {
                EncryptAllDocuments();
            }
            else if (cmd == "ChangeContent")
            {
                ChangeContent(cmdAttributes[0], cmdAttributes[1]);
            }
            else
            {
                throw new InvalidOperationException("Invalid command: " + cmd);
            }
        }

        private static void AddTextDocument(string[] attributes)
        {
            AddDocument(new TextDocument(attributes));
        }

        private static void AddPdfDocument(string[] attributes)
        {
            AddDocument(new PDFDocument(attributes));
        }

        private static void AddWordDocument(string[] attributes)
        {
            AddDocument(new WordDocument(attributes));
        }

        private static void AddExcelDocument(string[] attributes)
        {
            AddDocument(new ExcelDocument(attributes));
        }

        private static void AddAudioDocument(string[] attributes)
        {
            AddDocument(new AudioDocument(attributes));
        }

        private static void AddVideoDocument(string[] attributes)
        {
            AddDocument(new VideoDocument(attributes));
        }

        private static void ListDocuments()
        {
            StringBuilder sb = new StringBuilder();
            if (documents.Count == 0)
                Console.WriteLine("No documents found");
            else
            {
                documents.ForEach(d =>
                    {
                        sb.AppendLine(d.ToString());
                    });
                Console.WriteLine(sb.ToString().Trim());
            }
        }

        private static void EncryptDocument(string name)
        {
            StringBuilder sb = new StringBuilder();
            var document = documents.Where(d => d.Name == name).ToList();
            if (document.Count == 0)
                sb.AppendLine(string.Format("Document not found: {0}", name));
            else
            {
                document.ForEach(d =>
                {
                    if (d is IEncryptable)
                    {
                        (d as IEncryptable).Encrypt();
                        sb.AppendLine(string.Format("Document encrypted: {0}", name));
                    }
                    else
                        sb.AppendLine(string.Format("Document does not support encryption: {0}", name));
                });
            }
            Console.WriteLine(sb.ToString().Trim());
        }

        private static void DecryptDocument(string name)
        {
            StringBuilder sb = new StringBuilder();
            var document = documents.Where(d => d.Name == name).ToList();
            if (document.Count == 0)
                sb.AppendLine(string.Format("Document not found: {0}", name));
            else
            {
                document.ForEach(d =>
                    {
                        if (d is IEncryptable)
                        {
                            (d as IEncryptable).Decrypt();
                            sb.AppendLine(string.Format("Document decrypted: {0}", name));
                        }
                        else
                            sb.AppendLine(string.Format("Document does not support decryption: {0}", name));
                    });
            }
            Console.WriteLine(sb.ToString().Trim());
        }

        private static void EncryptAllDocuments()
        {
            StringBuilder sb = new StringBuilder();
            var encryptableDocuments = documents.Where(d => d is IEncryptable).ToList();
            if (encryptableDocuments.Count == 0)
                sb.Append("No encryptable documents found");
            else
            {
                encryptableDocuments.ForEach(d =>
                    {
                        (d as IEncryptable).Encrypt();
                    });
                sb.Append("All documents encrypted");
            }
            Console.WriteLine(sb.ToString());
        }

        private static void ChangeContent(string name, string content)
        {
            StringBuilder sb = new StringBuilder();
            var document = documents.Where(d => d.Name == name).ToList();
            if (document.Count == 0)
                sb.AppendLine(string.Format("Document not found: {0}", name));
            else
            {
                document.ForEach(d =>
                {
                    if (d is IEditable)
                    {
                        (d as IEditable).ChangeContent(content);
                        sb.AppendLine(string.Format("Document content changed: {0}", name));
                    }
                    else
                        sb.AppendLine(string.Format("Document is not editable: {0}", name));
                });
            }
            Console.WriteLine(sb.ToString().Trim());
        }
    }
}