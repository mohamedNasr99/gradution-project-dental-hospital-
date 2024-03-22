using DentalHospital.Data;
using DentalHospital.Models;

namespace DentalHospital.Services
{
    public class ReceptionistService : IReceptionistService
    {
        private readonly ApplicationDbContext dbContext;

        public ReceptionistService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool CheckPay(string code)
        {
            Patient? patient = dbContext?.Patients?.FirstOrDefault(p => p.Code == code);
            if (patient == null)
            {
                return false;
            }
            patient.IsPayed = true;
            patient.Clinic = "Diagnosis";
            dbContext?.SaveChanges();
            return true;
        }
    }
}
