namespace CoreMasterDetailSPAForEvidence.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
