using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CoreMasterDetailSPAForEvidence.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required, Display(Name = "Student Name")]
        public string StudentName { get; set; } = null!;
        [Required, Display(Name = "Admission Date"), DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime AdmissionDate { get; set; }

        public string MobileNo { get; set; } = null!;

        public bool IsEnrolled { get; set; }

        public string? ImageUrl { get; set; }

        public int CourseId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal RegistrationFee { get; set; }

        public virtual Course Course { get; set; } = null!;

        public virtual ICollection<CourseModule> CourseModules { get; set; } = new List<CourseModule>();
    }
}
