namespace Musicstore.Server.Data.Interfaces
{
    public interface IService
    {
        T PostService<T>(T value);
        T PutService<T>(T value);
    }
}
