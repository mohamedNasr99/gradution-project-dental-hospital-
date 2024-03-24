using DentalHospital.DTOs;

namespace DentalHospital.Services
{
    public interface IStudentService
    {
        Task<int> AddTreatment(TreatmentDTO treatmentDTO);
        Task<object> ViewDiagnosis(string code);
    }
}