using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace R54_Ex_06_Mvc_Evidence.Models
{
    public enum Gender { Male = 1, Female }
    public class Applicant
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
        [Required, StringLength(30)]
        public string Picture { get; set; }
        public bool IsReadyToWorkAnyWhere { get; set; }
        public virtual ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();
    }
    public class Qualification
    {
        public int QualificationId { get; set; }
        [Required, StringLength(30)]
        public string Degree { get; set; }
        [Required, StringLength(40)]
        public string Institute { get; set; }
        [Required]
        public int PassingYear { get; set; }
        [Required, StringLength(20)]
        public string Result { get; set; }
        [Required, ForeignKey("Applicant")]
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
    public class ApplicantDbContext : DbContext
    {
        public ApplicantDbContext()
        {
            Configuration.LazyLoadingEnabled = false; //if you not ommit virtual
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        
    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicantDbContext>
    {
        protected override void Seed(ApplicantDbContext context)
        {
            Applicant a = new Applicant { ApplicantName = "A1", AppliedFor = "P1", BirthDate = DateTime.Parse("2002-07-11"),Gender=Gender.Male, IsReadyToWorkAnyWhere = true, Picture = "e1.jpg" };
            a.Qualifications.Add(new Qualification { Degree = "BSC", Institute = "I1", PassingYear = 2019, Result = "2nd Class" });
            a.Qualifications.Add(new Qualification { Degree = "MSC", Institute = "I1", PassingYear = 2021, Result = "2nd Class" });
            context.Applicants.Add(a);
            context.SaveChanges();
        }
    }
}