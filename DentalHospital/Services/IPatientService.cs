using DentalHospital.DTOs;
using DentalHospital.Models;

namespace DentalHospital.Services
{
    public interface IPatientService
    {
        Task<Patient?> PatientRegister(ReservationDTO reservationDTO);
        Task<MedicalReport?> Reservation(string SNN);
    }
}