using System.ComponentModel.DataAnnotations;

namespace R54_M8_Class_09_Work_01.ViewModels.Input
{
    public class LoginInputModel
    {
        [Required, Display(Name = "Username")]
        public string? UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
