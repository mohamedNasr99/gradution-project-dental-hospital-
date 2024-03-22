using DentalHospital.DTOs;

namespace DentalHospital.Services
{
    public interface IPatientService
    {
        Task<object> Reservation(ReservationDTO reservationDTO);
    }
}