using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentSystem
{
    public abstract class Document : IDocument
    {
        private Dictionary<string, string> properties = new Dictionary<string, string>();
        public Dictionary<string, string> Properties
        {
            get { return properties; }
            set { properties = value; }
        }

        private readonly string name = string.Empty;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this.name))
                    return GetProperty("name");
                return name;
            }
        }

        public string Content
        {
            get
            {
                return GetProperty("content");
            }
        }

        public Document(string[] attributes)
        {
            foreach (var attribut in attributes)
            {
                var result = attribut.Split('=');
                this.Properties.Add(result[0], result[1]);
            }
        }

        internal string GetProperty(string key)
        {
            if (this.Properties.ContainsKey(key))
                return this.Properties[key];
            else
                return string.Empty;
        }

        internal void SetProperty(string key, string value)
        {
            if (this.Properties.ContainsKey(key))
                this.Properties[key] = value;
        }

        public void LoadProperty(string key, string value)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void SaveAllProperties(IList<KeyValuePair<string, object>> output)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name);
            sb.Append("[");
            if (this is IEncryptable && (this as IEncryptable).IsEncrypted)
                sb.Append("encrypted");
            else
                this.Properties.Where(p => !string.IsNullOrEmpty(p.Value)).OrderBy(p => p.Key).ToList().ForEach(p =>
                    {
                        sb.AppendFormat("{0}={1};", p.Key, p.Value);
                    });
            if (sb.ToString().EndsWith(";"))
                sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString().Trim();
        }
    }
}
