using R54_Ex_06_Mvc_Evidence.Models;
using R54_Ex_06_Mvc_Evidence.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace R54_Ex_06_Mvc_Evidence.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly ApplicantDbContext db = new ApplicantDbContext();
        // GET: Applicants
        public ActionResult Index()
        {
            //Eager loading
            var data = db.Applicants.Include(x => x.Qualifications).AsNoTracking().ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            ApplicantViewModel a =new ApplicantViewModel();
            a.Qualifications.Add(new Qualification { });
            return View(a);

        }
        [HttpPost]
        public ActionResult Create(ApplicantViewModel data, string act="")
        {
            if(act == "add")
            {
                data.Qualifications.Add(new Qualification { });
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring( act.IndexOf("_")+1));
                data.Qualifications.RemoveAt(index);
            }
            if(act=="insert")
            {
                if(ModelState.IsValid)
                {
                    var a = new  Applicant
                    {
                        ApplicantName = data.ApplicantName,
                        BirthDate = data.BirthDate,
                        AppliedFor = data.AppliedFor,
                        Gender = data.Gender,
                        IsReadyToWorkAnyWhere= data.IsReadyToWorkAnyWhere
                        
                    };
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Server.MapPath("~/Pictures/") + fileName;
                    data.Picture.SaveAs(savePath);
                    a.Picture = fileName;
                    foreach(var q in data.Qualifications)
                    {
                        a.Qualifications.Add(q);
                    }
                    db.Applicants.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(data);
        }
        public ActionResult Edit(int id)
        {
            //explicit

            var x = db.Applicants.FirstOrDefault(a => a.ApplicantId == id);
            if (x == null) return new HttpNotFoundResult();
            db.Entry(x).Collection(q => q.Qualifications).Load();

            var data = new ApplicantEditModel
            {
                ApplicantId = x.ApplicantId,
                ApplicantName = x.ApplicantName,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                AppliedFor = x.AppliedFor,
                IsReadyToWorkAnyWhere = x.IsReadyToWorkAnyWhere,
                Qualifications = x.Qualifications.ToList()

            };
            ViewBag.CurrentPic = db.Applicants.FirstOrDefault(o => o.ApplicantId == id).Picture;

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(ApplicantEditModel data, string act = "")
        {
            if (act == "add")
            {
                data.Qualifications.Add(new Qualification { });
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                data.Qualifications.RemoveAt(index);
            }
            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    var a = db.Applicants.FirstOrDefault(x => x.ApplicantId == data.ApplicantId);

                    a.ApplicantName = data.ApplicantName;
                    a.BirthDate = data.BirthDate;
                    a.AppliedFor = data.AppliedFor;
                    a.Gender = data.Gender;
                    a.IsReadyToWorkAnyWhere = data.IsReadyToWorkAnyWhere;


                    if (data.Picture != null)
                    {
                        string ext = Path.GetExtension(data.Picture.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Server.MapPath("~/Pictures/") + fileName;
                        data.Picture.SaveAs(savePath);
                        a.Picture = fileName;
                    }
                    db.Qualifications.RemoveRange(db.Qualifications.Where(x => x.ApplicantId == data.ApplicantId).ToList());
                    foreach (var item in data.Qualifications)
                    {
                        a.Qualifications.Add(new Qualification
                        {
                            ApplicantId = data.ApplicantId,
                            Degree = item.Degree,
                            Institute = item.Institute,
                            PassingYear = item.PassingYear,
                            Result = item.Result
                        });
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var sql = $"SELECT * FROM Applicants WHERE ApplicantId={id}";
            var a= db.Applicants.SqlQuery(sql).FirstOrDefault();
            sql = $"SELECT * FROM Qualifications WHERE ApplicantId={id}";
            a.Qualifications = db.Qualifications.SqlQuery(sql).ToList();

            return View(a);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            var sql = $"DELETE  FROM Applicants WHERE ApplicantId={id}";
            int ra = db.Database.ExecuteSqlCommand(sql);

            return RedirectToAction("Index");
        }
        
    }
}