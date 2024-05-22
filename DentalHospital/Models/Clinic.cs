using System.ComponentModel.DataAnnotations;

namespace DentalHospital.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Professor> Professors { get; set; } = new List<Professor>();
    }
}
