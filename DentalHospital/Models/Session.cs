namespace DentalHospital.Models
{
    public class Session
    {
        public string Id { get; set; } = string.Empty;
        public string MedicalReportId { get; set; } = string.Empty;
        public string session { get; set; } = string.Empty; 
        public string Treatment { get; set; } = string.Empty; 
        public DateTime Date { get; set; } = new DateTime();
        public MedicalReport MedicalReport { get; set; } = new MedicalReport();
    }
}
