using System.Collections.Generic;

namespace CatalogOfFreeContent
{
    public interface ICatalog
    {
        void Add(IContent content);

        IEnumerable<IContent> GetContentByTitle(string title, int maxResult);

        int UpdateUrl(string oldUrl, string newUrl);
    }
}
