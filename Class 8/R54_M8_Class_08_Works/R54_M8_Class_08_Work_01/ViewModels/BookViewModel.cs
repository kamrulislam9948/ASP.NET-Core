using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace R54_M8_Class_08_Work_01.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(40)]
        public string Title { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime PublishDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal CoverPrice { get; set; }
        public string Authors { get; set; }=default!;
    }
}
