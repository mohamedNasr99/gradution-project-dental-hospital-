using DentalHospital.DTOs;
using DentalHospital.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalHospital.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;

        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        [HttpPost("Reservation")]
        public async Task<IActionResult> Reservation(ReservationDTO reservationDTO)
        {
            if(ModelState.IsValid == true)
            {
                var result = await patientService.Reservation(reservationDTO);
                return Ok(result);
            }

            return BadRequest(ModelState);
        }
    }
}
