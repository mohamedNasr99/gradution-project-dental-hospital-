namespace DentalHospital.Models
{
    public class Session
    {
        public int Id { get; set; } 
        public int MedicalReportId { get; set; } 
        public string MedicalReportCode { get; set; } = string.Empty;
        public string session { get; set; } = string.Empty; 
        public string Treatment { get; set; } = string.Empty; 
        public DateTime Date { get; set; } = new DateTime();
        public MedicalReport MedicalReport { get; set; } = new MedicalReport();
    }
}
