using DentalHospital.DTOs;
using DentalHospital.Models;
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
                Patient? patient = await patientService.Reservation(reservationDTO);

                if (patient != null)
                {
                    return Ok(patient.Code);
                }
                return BadRequest("قد يكون مفيش حجز انهردا او العدد اكتمل");
            }

            return BadRequest(ModelState);
        }
    }
}
