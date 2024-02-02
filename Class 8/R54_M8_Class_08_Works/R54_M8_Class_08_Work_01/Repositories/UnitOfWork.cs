using R54_M8_Class_08_Work_01.Models;
using R54_M8_Class_08_Work_01.Repositories.Intefaces;

namespace R54_M8_Class_08_Work_01.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookDbContext db;
        public UnitOfWork(BookDbContext db)
        {
            this.db = db;
        }
        public IGenericRepo<T> GetRepo<T>() where T : class, new()
        {
            return new GenericRepo<T>(db);
        }

        public async Task<bool> SaveAsync()
        {
            int r= await this.db.SaveChangesAsync();
            return r > 0;
        }
    }
}
