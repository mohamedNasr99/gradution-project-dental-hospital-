using DentalHospital.DTOs;
using DentalHospital.Models;

namespace DentalHospital.Services
{
    public interface IStudentService
    {
        int AddTreatment(TreatmentDTO treatmentDTO);
        Task<int> TreatmentInDiagnosis(TreatmentInDiagnosisDTO treatmentInDiagnosisDTO);
        int ConvertToClinic(ConvertToClinicDTO convertToClinicDTO);
        Task<int> AddSession(SessionDTO sessionDTO);
        Task<IEnumerable<string>> Search(string name);
        IEnumerable<string> Cases(string SSN);
        Task<CaseDTO> MedicalReport(string code);
        IEnumerable<DateTime> SessionsDates(string MedicalCode);
        Task<SessionReturnDTO> SessionData(DateTime date);
        Task<IEnumerable<string>> clinics();

    }
}