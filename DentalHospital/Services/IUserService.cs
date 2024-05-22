using DentalHospital.Data;

namespace DentalHospital.Services
{
    public interface IUserService
    {
        Task<ApplicationUser?> GetUserById(string? id);
    }
}