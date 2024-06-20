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
        Task<IQueryable<string>> Cases();
        IEnumerable<DateTime> SessionsDates(string MedicalCode);
        Task<SessionReturnDTO> SessionData(DateTime date);
        Task<IEnumerable<string>> clinics();
        Task<int> CheckPatient(string Code);

    }
}