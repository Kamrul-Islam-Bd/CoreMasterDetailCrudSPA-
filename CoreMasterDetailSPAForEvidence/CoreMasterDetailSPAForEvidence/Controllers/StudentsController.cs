using CoreMasterDetailSPAForEvidence.Models;
using CoreMasterDetailSPAForEvidence.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CoreMasterDetailSPAForEvidence.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _web;

        public StudentsController(AppDbContext db, IWebHostEnvironment web)
        {
            _db = db;
            _web = web;
        }

        public IActionResult Index()
        {
            var students = _db.Students.Include(s => s.Course).Include(s => s.CourseModules).ToList();
            return View(students);
            
        }
        [HttpGet]
        public ActionResult CreatePartial()
        {
            StudentViewModel student = new StudentViewModel();
            student.Courses = _db.Courses.ToList();
            student.CourseModules.Add(new CourseModule() { CourseModuleId = 1 });
            return PartialView("_CreateStudentPartial", student);
            
        }
        [HttpGet]
        public ActionResult Delete(int id) 
        { 
          var student=_db.Students.Find(id);
            if (student != null) 
            { 
               _db.Students.Remove(student);
               _db.SaveChanges();         

            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent(StudentViewModel vobj)
        {
            if (!ModelState.IsValid)
            {
                vobj.Courses = _db.Courses.ToList();
                return View();
            }
            Student student = new Student
            {
                StudentName = vobj.StudentName,
                AdmissionDate = vobj.AdmissionDate,
                MobileNo = vobj.MobileNo,
                CourseId = vobj.CourseId,
                IsEnrolled = vobj.IsEnrolled,
                RegistrationFee = vobj.RegistrationFee,
                CourseModules = vobj.CourseModules
            };

            if (vobj.ProfileFile != null)
            {
                string uniqueFileName = GetFileName(vobj.ProfileFile);
                student.ImageUrl = uniqueFileName;
            }
            else
            {
                student.ImageUrl = "noimage.png";
            }

            _db.Students.Add(student);
            try
            {
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                vobj.Courses = _db.Courses.ToList();
                return View();
            }
        }


        private string GetFileName(IFormFile profileFile)
        {
            string uniqueFileName = null;
            if (profileFile != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profileFile.FileName); ;
                var uploadFolder = Path.Combine(_web.WebRootPath, "images");
                var filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    profileFile.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpGet]
        public ActionResult EditPartial(int id)
        {
            var student = _db.Students
                .Include(a => a.CourseModules)
                .FirstOrDefault(x => x.StudentId == id);
            var vObj = new StudentViewModel
            {
                StudentName = student.StudentName,
                StudentId = student.StudentId,
                AdmissionDate = student.AdmissionDate,
                MobileNo = student.MobileNo,
                CourseId = student.CourseId,
                IsEnrolled = student.IsEnrolled,
                ImageUrl = student.ImageUrl,
                RegistrationFee = student.RegistrationFee,
                CourseModules = student.CourseModules.ToList(),
                Courses = _db.Courses.ToList()
            };
            return PartialView("_EditStudentPartial", vObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(StudentViewModel vobj, string OldImageUrl)
        {
            if (!ModelState.IsValid)
            {
                vobj.Courses = _db.Courses.ToList();
                return Json(new { success = false });
            }
            Student obj = _db.Students.Find(vobj.StudentId);
            if (obj != null)
            {
                obj.StudentName = vobj.StudentName;
                obj.CourseId = vobj.CourseId;
                obj.MobileNo = vobj.MobileNo;
                obj.IsEnrolled = vobj.IsEnrolled;
                obj.AdmissionDate = vobj.AdmissionDate;
                obj.RegistrationFee = vobj.RegistrationFee;
                if (vobj.ProfileFile != null)
                {
                    string uniqueFileName = GetFileName(vobj.ProfileFile);
                    obj.ImageUrl = uniqueFileName;
                }
                else
                {
                    obj.ImageUrl = OldImageUrl;
                }
                var modules = _db.CourseModules.Where(m => m.StudentId == vobj.StudentId).ToList();
                if (modules != null)
                {
                    _db.CourseModules.RemoveRange(modules);
                    //_db.SaveChanges();
                }
                if (vobj.CourseModules != null)
                {
                    foreach (var module in vobj.CourseModules)
                    {
                        module.StudentId = vobj.StudentId;
                        module.ModuleName = module.ModuleName;
                        module.Duration = module.Duration;
                        _db.CourseModules.Add(module);
                    }
                    //_db.SaveChanges();
                }
                _db.Entry(obj).State = EntityState.Modified;
                _db.SaveChanges();
                try
                {

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    vobj.Courses = _db.Courses.ToList();
                    return Json(new { success = false });
                }
            }
            vobj.Courses = _db.Courses.ToList();
            return View(vobj);
        }
    }
}
