
namespace NoSQL.Infrastructure.Service.Interfaces
{
    public interface ICollectionService
    {
        string Insert(string json);
        string Update(string json, string id);
        void Delete(string id);
        string GetById(string id);
    }
}
