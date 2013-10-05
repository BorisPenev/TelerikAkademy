namespace CatalogOfFreeContent
{
    public enum CommandTypes
    {
        AddBook,
        AddMovie,
        AddSong,
        AddApplication,
        Update,
        Find,
    }

    public enum ContentTypes
    {
        Book,
        Movie,
        Song,
        Application,
    }

    public enum ContentItemTypes
    {
        Title,
        Author,
        Size,
        Url,
    }
}