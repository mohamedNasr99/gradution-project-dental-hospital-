using System.ComponentModel.DataAnnotations;

namespace DentalHospital.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Student>? Students { get; set; } 
        public List<Professor>? Professors { get; set; }
    }
}
