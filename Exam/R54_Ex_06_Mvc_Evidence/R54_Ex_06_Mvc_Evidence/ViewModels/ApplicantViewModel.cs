using R54_Ex_06_Mvc_Evidence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R54_Ex_06_Mvc_Evidence.ViewModels
{
    public class ApplicantViewModel
    {
        public int ApplicantId { get; set; }
        [Required, StringLength(40)]
        public string ApplicantName { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, StringLength(30)]
        public string AppliedFor { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        public bool IsReadyToWorkAnyWhere { get; set; }
        public virtual List<Qualification> Qualifications { get; set; } = new List<Qualification>();
    }
}