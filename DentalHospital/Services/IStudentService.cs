using DentalHospital.DTOs;
using DentalHospital.Models;

namespace DentalHospital.Services
{
    public interface IStudentService
    {
        Task<int> AddTreatment(TreatmentDTO treatmentDTO);
        Task<MedicalReport?> ViewDiagnosis(string code);
        Task<int> CheckCode(string code);
        Task<int> TreatmentInDiagnosis(TreatmentInDiagnosisDTO treatmentInDiagnosisDTO);
        Task<int> ConvertToClinic(ConvertToClinicDTO convertToClinicDTO);
    }
}