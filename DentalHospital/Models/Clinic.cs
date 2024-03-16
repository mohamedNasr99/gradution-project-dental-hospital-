using System.ComponentModel.DataAnnotations;

namespace DentalHospital.Models
{
    public class Clinic
    {
        public string Id { get; set; } = string.Empty;
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only letters and spaces are allowed")]
        public string Name { get; set; } = string.Empty;
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Professor> Professors { get; set; } = new List<Professor>();
        public List<MedicalReport> MedicalReports { get; set; } = new List<MedicalReport>();
    }
}
