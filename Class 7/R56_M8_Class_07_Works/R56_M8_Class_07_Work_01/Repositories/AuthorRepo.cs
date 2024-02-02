using Microsoft.EntityFrameworkCore;
using R56_M8_Class_07_Work_01.Models;
using R56_M8_Class_07_Work_01.Repositories.Interfaces;

namespace R56_M8_Class_07_Work_01.Repositories
{
    public class AuthorRepo : IAuthorRepo
    {

        BookDbContext db;
        public AuthorRepo(BookDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<Author>> GetAsync(bool include = false)
        {
            var data = db.Authors;
            if (include)
            {
                var retData = await data.Include(b => b.BookAuthors).ThenInclude(ba => ba.Book).ToListAsync();
                return retData;
            }
            return await data.ToListAsync();
        }

        public async Task<Author?> GetAsync(int id, bool include = false)
        {
            var data = db.Authors;
            if (include)
            {
                var retData = await data.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).FirstOrDefaultAsync(x => x.Id == id);
                return retData;
            }
            return await db.Authors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> InsertAsync(Author author)
        {
            await db.Authors.AddAsync(author);
            return await db.SaveChangesAsync() > 0;
            //return false;
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            db.Entry(author).State = EntityState.Modified;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var author = await this.GetAsync(id);
            if (author != null)
            {
                db.Authors.Remove(author);
                return await db.SaveChangesAsync() > 0;
                
            }
            return false;
        }
    }
}
