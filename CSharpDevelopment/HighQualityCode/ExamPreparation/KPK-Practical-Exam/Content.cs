using System;

namespace CatalogOfFreeContent
{
    public class Content : IContent
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public Int64 Size { get; set; }
        
        private string url;
        

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        public ContentTypes Types { get; set; }

        public string TextRepresentation
        {
            get
            {
                return this.ToString();
            }
            set { }
        }

        public Content(ContentTypes types, string[] commandParams)
        {
            this.Types = types;
            this.Title = commandParams[(int)ContentItemTypes.Title];
            this.Author = commandParams[(int)ContentItemTypes.Author];
            this.Size = Int64.Parse(commandParams[(int)ContentItemTypes.Size]);
            this.Url = commandParams[(int)ContentItemTypes.Url];
        }

        public int CompareTo(object obj)
        {
            if (null == obj)
                return 1;

            Content otherContent = obj as Content;
            if (otherContent != null)
            {
                int comparisonResul = this.TextRepresentation.CompareTo(otherContent.TextRepresentation);
                return comparisonResul;
            }
            
            throw new ArgumentException("Object is not a Content");
        }

        public override string ToString()
        {
            var output = String.Format("{0}: {1}; {2}; {3}; {4}",
                                        this.Types.ToString(), this.Title,
                                        this.Author, this.Size, this.Url);
            return output;
        }
    }
}