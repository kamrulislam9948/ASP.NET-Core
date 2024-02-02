using R56_M8_Class_07_Work_01.Models;

namespace R56_M8_Class_07_Work_01.Repositories.Interfaces
{
    public interface IAuthorRepo
    {
        Task<IEnumerable<Author>> GetAsync(bool include = false);
        Task<Author?> GetAsync(int id, bool include = false);
        Task<bool> InsertAsync(Author author);
        Task<bool> UpdateAsync(Author author);
        Task<bool> DeleteAsync(int id);
    }
}
