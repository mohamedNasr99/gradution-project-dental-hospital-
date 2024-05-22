using DentalHospital.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalHospital.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser?> GetUserById(string? id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
