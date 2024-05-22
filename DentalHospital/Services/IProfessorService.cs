
using DentalHospital.DTOs;

namespace DentalHospital.Services
{
    public interface IProfessorService
    {
        Task<List<string>> StudentsInSpecificClinic();
        IQueryable<string>? MedicalReportsOfStudent(string StudentName);
        Task<CaseDTO> MedicalReport(string code);
    }
}