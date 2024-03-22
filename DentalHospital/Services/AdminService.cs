using DentalHospital.Data;
using DentalHospital.Models;

namespace DentalHospital.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext dbContext;

        public AdminService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int AddPermissibleCases(int Cases)
        {

            var Case = dbContext?.Cases?.FirstOrDefault();

            if (Case == null)
            {
                Cases CaseNull = new Cases();
                CaseNull.PermissibleCases = Cases;
                dbContext?.Cases?.Add(CaseNull);
            }
            else
            {
                Case.PermissibleCases = Cases;
                dbContext?.Cases?.Update(Case);
            }

            var result = dbContext?.SaveChanges();

            if (result == 1)
            {
                return 1;
            }

            return 0;
        }
    }
}
