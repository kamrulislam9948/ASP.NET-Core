using R54_M8_Class_09_Work_01.Models;
using System.ComponentModel.DataAnnotations;

namespace R54_M8_Class_09_Work_01.ViewModels.Input
{
    public class JobProviderInputModel
    {
        public string? Id { get; set; }
        [Required, StringLength(100)]
        public string? CompanyName { get; set; }
        [Required, StringLength(450)]
        public string? CompanyAddress { get; set; }
       
    }
}
