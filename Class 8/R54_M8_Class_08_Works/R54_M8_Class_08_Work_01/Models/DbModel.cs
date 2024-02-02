using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace R54_M8_Class_08_Work_01.Models
{
   
    public class Book
    {
       public int BookId { get; set; }
        [Required, StringLength(40)]
        public string Title { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime PublishDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal CoverPrice { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
    public class Author
    {
       public int AuthorId { get; set; }
        [Required, StringLength(40)]
        public string Name { get; set; } = default!;
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
    //[PrimaryKey("BookId", "AuthorId")]
    public class BookAuthor
    {
        [Required, ForeignKey("Book")]
        public int BookId { get; set; }
        [Required, ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Book Book { get; set; } = default!;
        public virtual Author Author { get; set; } = default!;
    }
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<BookAuthor> BookAuthors { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(
                ba => new { ba.BookId, ba.AuthorId }
                );
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "PK Halder", Email = "pk@gmail.pk" },
                new Author { AuthorId = 2, Name = "RK Murad", Email = "rk@gmail.pk" }
                );
            modelBuilder.Entity<Book>().HasData(
               new Book { BookId = 1, Title = "My Life", CoverPrice = 68.95M, PublishDate = DateTime.Now.AddMonths(-27) }

               );
            modelBuilder.Entity<BookAuthor>().HasData(
              new BookAuthor { BookId = 1, AuthorId = 1 },
              new BookAuthor { BookId = 1, AuthorId = 2 }
               );
        }
    }
}
