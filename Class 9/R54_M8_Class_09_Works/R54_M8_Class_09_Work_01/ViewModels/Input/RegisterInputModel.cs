using System.ComponentModel.DataAnnotations;

namespace R54_M8_Class_09_Work_01.ViewModels.Input
{
    public class RegisterInputModel
    {

        [Required, Display(Name ="Username")]
        public string? UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password"), Display(Name ="Confirm password")]
        public string? ConfirmPassword { get; set; }
        public bool IsJobProvider { get; set; }
    }
}
