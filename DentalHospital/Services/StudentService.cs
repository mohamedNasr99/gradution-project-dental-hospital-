using DentalHospital.Data;
using DentalHospital.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DentalHospital.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext dbContext;

        public StudentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<object> ViewDiagnosis(string code)
        {
            var medicalreport = await dbContext.MedicalReports.FirstOrDefaultAsync(m => m.PatientCode == code);
            if (medicalreport == null)
            {
                return new
                {
                    Message = "There is no medical report with this code."
                };
            }

            return new
            {
                Medical = medicalreport.MedicalHistory,
                Dental = medicalreport.DentalHistory,
                Diagnosis = medicalreport.Diagnosis
            };
        }

        public async Task<int> AddTreatment(TreatmentDTO treatmentDTO)
        {
            if (treatmentDTO != null)
            {
                var medicalreport = await dbContext.MedicalReports.FirstOrDefaultAsync(m => m.PatientCode == treatmentDTO.Code);

                if (medicalreport == null)
                {
                    return 0;
                }

                medicalreport.Description = treatmentDTO.Description;
                medicalreport.Treatment = treatmentDTO.Treatment;

                await dbContext.AddAsync(medicalreport);
                await dbContext.SaveChangesAsync();

                return 1;
            }

            return 0;
        }
    }
}
