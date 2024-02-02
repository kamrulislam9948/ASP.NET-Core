using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace R54_M8_Class_09_Work_01.ViewModels.Input
{
    public class JobSeekerInputModel
    {
        public string? Id { get; set; }
        [Required, StringLength(50)]
        public string? FullName { get; set; }
        [DataType(DataType.Date), Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        [Required, StringLength(20)]
        public string? ContactNumber { get; set; }
    }
}
