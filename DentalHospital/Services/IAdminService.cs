using DentalHospital.DTOs;

namespace DentalHospital.Services
{
    public interface IAdminService
    {
        int AddPermissibleCases(int Cases);
        Task<bool> StudentConvert(StudentConvertDTO studentConvertDTO);
    }
}