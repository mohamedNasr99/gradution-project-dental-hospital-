
namespace DentalHospital.Services
{
    public interface IProfessorService
    {
        List<string> StudentsInSpecificClinic(string ClinicName);
    }
}