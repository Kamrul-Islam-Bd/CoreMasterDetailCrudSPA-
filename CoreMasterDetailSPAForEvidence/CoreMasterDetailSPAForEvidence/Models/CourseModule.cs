namespace CoreMasterDetailSPAForEvidence.Models
{
    public class CourseModule
    {
        public int CourseModuleId { get; set; }

        public string ModuleName { get; set; } = null!;

        public int Duration { get; set; }

        public int StudentId { get; set; }

        public virtual Student? Student { get; set; } = null!;
    }
}
