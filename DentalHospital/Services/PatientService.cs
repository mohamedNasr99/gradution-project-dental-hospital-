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

            var counter = dbContext.Patients.Count() + 1;
            var codenumber = $"{counter:00}24{DateTime.Now.Month:00}{DateTime.Now.Day:00}";

            Patient patient = new Patient();
            patient.Name = reservationDTO.Name;
            patient.SSN = reservationDTO.PatientSSN;
            patient.BirthDate = reservationDTO.BirthDate;
            patient.PhoneNumber = reservationDTO.PatientNumber;
            patient.Gender = reservationDTO.Gender;
            patient.CreatedAt = DateTime.Now;
            patient.Code = codenumber;

            await dbContext.Patients.AddAsync(patient);
            await dbContext.SaveChangesAsync();


            return patient;

        }


        public async Task<MedicalReport?> Reservation(string SNN)
        {
            Patient? patient = await dbContext.Patients.FirstOrDefaultAsync(p => p.SSN == SNN);

            if (patient == null)
            {
                return null;
            }

            var counter = dbContext.MedicalReports.Where(m => m.dateTime.Day == DateTime.Now.Day).Count() + 1;
            var codenumber = $"{counter:00}24{DateTime.Now.Month:00}{DateTime.Now.Day:00}";

            MedicalReport medicalReport = new MedicalReport();
            medicalReport.PatientSSN = patient.SSN;
            medicalReport.Code = codenumber;
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
