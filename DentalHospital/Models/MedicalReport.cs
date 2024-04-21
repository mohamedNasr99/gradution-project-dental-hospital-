namespace DentalHospital.Models
{
    public class MedicalReport
    {
        public string Code { get; set; } = string.Empty; 
        public string PatientCode { get; set; } = string.Empty;
        public string StudentSSN { get; set; } = string.Empty;
        public string Clinic { get; set; } = string.Empty;
        public bool IsPayed { get; set; }
        public string MedicalHistory { get; set; } = string.Empty;   
        public string DentalHistory { get; set; } = string.Empty; 
        public string Diagnosis { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public string Treatment { get; set; } = string.Empty;
        public Patient Patient { get; set; } = new Patient();
        public Student Student { get; set; } = new Student();
        public List<Session> Sessions { get; set; } = new List<Session>();

    }
}
