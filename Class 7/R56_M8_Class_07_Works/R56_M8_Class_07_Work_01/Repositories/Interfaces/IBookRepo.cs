using R56_M8_Class_07_Work_01.Models;

namespace R56_M8_Class_07_Work_01.Repositories.Interfaces
{
    public interface IBookRepo
    {
        Task<IEnumerable<Book>> GetAsync(bool include=false);
        Task<Book?> GetAsync(int id, bool include = false);
        Task<bool> InsertAsync(Book book);
        Task<bool> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Author>> GetAuthorOptionsAsync();
    }
}
