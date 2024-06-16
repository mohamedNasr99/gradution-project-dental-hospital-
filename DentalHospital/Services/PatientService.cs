using DentalHospital.Data;
using DentalHospital.DTOs;
using DentalHospital.Models;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;

namespace DentalHospital.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext dbContext;

        public PatientService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Patient?> PatientRegister(ReservationDTO reservationDTO)
        {
            string code = GenerateUniqueCode();

            Patient patient = new Patient();
            patient.Name = reservationDTO.Name;
            patient.SSN = reservationDTO.PatientSSN;
            patient.BirthDate = reservationDTO.BirthDate;
            patient.PhoneNumber = reservationDTO.PatientNumber;
            patient.Gender = reservationDTO.Gender;
            patient.CreatedAt = DateTime.Now;
            patient.Code = code;

            await dbContext.Patients.AddAsync(patient);
            await dbContext.SaveChangesAsync();


            return patient;

        }

        private string GenerateUniqueCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string code;

            do
            {
                code = new string(Enumerable.Repeat(chars, 8) 
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (dbContext.Patients.Any(p => p.Code == code)); 

            return code;
        }

        public async Task<MedicalReport?> Reservation(string SNN)
        {
            Patient? patient = await dbContext.Patients.FirstOrDefaultAsync(p => p.SSN == SNN);

            if (patient == null)
            {
                return null;
            }

            string code = GenerateUniqueCode();
            MedicalReport medicalReport = new MedicalReport();
            medicalReport.PatientSSN = patient.SSN;
            medicalReport.Code = code;
            medicalReport.PatientId = patient.Id;
            medicalReport.dateTime = DateTime.Now;

            await dbContext.MedicalReports.AddAsync(medicalReport);
            await dbContext.SaveChangesAsync();

            return medicalReport;
        }

        public async Task<Patient?> IsExist(string Name)
        {
            Patient? patient = await dbContext.Patients.FirstOrDefaultAsync(p => p.Name == Name);
            return patient;
        }

    }
}
