using DentalHospital.Data;
using DentalHospital.DTOs;
using DentalHospital.Models;
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

        public async Task<MedicalReport?> ViewDiagnosis(string code)
        {
            var medicalreport = await dbContext.MedicalReports.FirstOrDefaultAsync(m => m.PatientCode == code);
            if (medicalreport != null)
            {
                return medicalreport;
            }

            return null;
            
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

        public async Task<int> CheckCode(string code)
        {
            if (code != null)
            {
                Patient? patient = await dbContext.Patients.FirstOrDefaultAsync(p => p.Code == code);
                if (patient == null)
                {
                    return 0;
                }
                else if(patient.IsPayed == true)
                {
                    return 1;
                }
                else if (patient.IsPayed == false)
                {
                    return 0;
                }

            }

            return 0;
        }

        public async Task<int> TreatmentInDiagnosis(TreatmentInDiagnosisDTO treatmentInDiagnosisDTO)
        {
            MedicalReport medicalReport = new MedicalReport();

            medicalReport.PatientCode = treatmentInDiagnosisDTO.Code;
            medicalReport.MedicalHistory = treatmentInDiagnosisDTO.MedicalHistory;
            medicalReport.DentalHistory = treatmentInDiagnosisDTO.DentalHistory;
            medicalReport.Diagnosis = treatmentInDiagnosisDTO.Diagnosis;

            await dbContext.MedicalReports.AddAsync(medicalReport);
           int result = await dbContext.SaveChangesAsync();

            if (result == 1)
            {
                return 1;
            }
            return 0;
        }

        public async Task<int> ConvertToClinic(ConvertToClinicDTO convertToClinicDTO)
        {
            Patient? patient = await dbContext.Patients.FirstOrDefaultAsync(p => p.Code ==  convertToClinicDTO.Code);
            if (patient != null)
            {
                patient.Clinic = convertToClinicDTO.ClinicName;
                dbContext.Update(patient);
                await dbContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
    }
}
