using Microsoft.EntityFrameworkCore.Metadata.Internal;
using R56_M8_Class_07_Work_01.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace R56_M8_Class_07_Work_01.ViewModels.Input
{
    public class BookInputModel
    {
        public int Id { get; set; }

        [Required, StringLength(40)]
        public string Title { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime PublishDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal CoverPrice { get; set; }
        public List<int> Authors { get; set; }=new List<int>();

    }
}
