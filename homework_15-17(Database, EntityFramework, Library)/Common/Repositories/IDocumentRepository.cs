using AccessToDB;
namespace Common.Repositories
{
    public interface IDocumentRepository
    {   
        Task<Document?> GetDocumentByType(string type);

        Task<List<Document>> GetDocuments();
    }
}