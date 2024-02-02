using Microsoft.EntityFrameworkCore;
using R56_M8_Class_07_Work_01.Models;
using R56_M8_Class_07_Work_01.Repositories.Interfaces;

namespace R56_M8_Class_07_Work_01.Repositories
{
    public class BookRepo : IBookRepo
    {
        BookDbContext db;
        public BookRepo(BookDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<Book>> GetAsync(bool include = false)
        {
            var data = db.Books;
            if (include)
            {
                var retData= await data.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).ToListAsync();
                return retData;
            }
            return await data.ToListAsync();
        }

        public async Task<Book?> GetAsync(int id, bool include = false)
        {
            var data = db.Books;
            if (include)
            {
                var retData = await data.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).FirstOrDefaultAsync(x=> x.Id==id);
                return retData;
            }
            return await db.Books.FirstOrDefaultAsync(x => x.Id==id);
        }

        public async Task<bool> InsertAsync(Book book)
        {
            await db.Books.AddAsync(book);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var book = await this.GetAsync(id);
            if(book != null)
            {
                db.Books.Remove(book);
                return await db.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<Author>> GetAuthorOptionsAsync()
        {
            var data = await db.Authors.ToListAsync();
            return data;
        }
    }
}
