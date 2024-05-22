namespace DentalHospital.Models
{
    public class MedicalReport
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; 
        public int PatientId { get; set; }
        public string PatientSSN { get; set; } = string.Empty;
        public string StudentSSN { get; set; } = string.Empty;
        public int StudentId { get; set; } 
        public string? Clinic { get; set; } 
        public bool IsPayed { get; set; }
        public string MedicalHistory { get; set; } = string.Empty;   
        public string DentalHistory { get; set; } = string.Empty; 
        public string Diagnosis { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public string Treatment { get; set; } = string.Empty;
        public DateTime dateTime { get; set; } = new DateTime();
        public Patient Patient { get; set; } = new Patient();
        public Student Student { get; set; } = new Student();
        public List<Session> Sessions { get; set; } = new List<Session>();

    }
}
