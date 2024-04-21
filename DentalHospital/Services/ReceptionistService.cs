using DentalHospital.Data;
using DentalHospital.Models;
using Microsoft.EntityFrameworkCore;

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
            MedicalReport? report = dbContext?.MedicalReports?.FirstOrDefault(p => p.Code == code);
            if (report == null)
            {
                return false;
            }
            report.IsPayed = true;
            report.Clinic = "Diagnosis";
            dbContext?.Update(report);
            dbContext?.SaveChanges();
            return true;
        }

        public async Task<string?> CheckCode(string name)
        {
            Patient? patient = await dbContext.Patients.FirstOrDefaultAsync(p => p.Name == name);
            if (patient == null)
            {
                return null;
            }
            return patient.Code;
        }
    }
}
