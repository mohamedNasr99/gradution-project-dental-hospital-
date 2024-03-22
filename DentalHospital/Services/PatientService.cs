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

        public async Task<object> Reservation(ReservationDTO reservationDTO)
        {
            string code = GenerateUniqueCode();

            if (dbContext == null)
            {
                return new
                {
                    Message = "DbContext is null."
                };
            }

            if (reservationDTO != null)
            {
                Cases? CaseObject = await dbContext.Cases.FirstOrDefaultAsync();

                if(CaseObject == null || CaseObject.PermissibleCases == 0)
                {
                    return new 
                    {
                        Message = "There is no reservation today."
                    };
                }

                int ReservationCount = await dbContext.Patients.CountAsync(p => p.CreatedAt.Day == DateTime.Now.Day);

                if (ReservationCount > CaseObject.PermissibleCases)
                {
                    return new 
                    {
                        Message = "Reservation limit reached for today."
                    };
                }

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

                return new
                {
                    Message = "Ok.",
                    Code = patient.Code
                };
            }

            return new
            {
                Message = "ReservationDTO is null."
            };
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

    }
}
