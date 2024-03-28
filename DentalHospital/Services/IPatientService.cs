using DentalHospital.DTOs;
using DentalHospital.Models;

namespace DentalHospital.Services
{
    public interface IPatientService
    {
        Task<Patient?> Reservation(ReservationDTO reservationDTO);
    }
}