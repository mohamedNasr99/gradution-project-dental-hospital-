using DentalHospital.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalHospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Professor")]
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorService professorService;

        public ProfessorController(ProfessorService professorService)
        {
            this.professorService = professorService;
        }

        [HttpGet("StudentsInSpecificClinic")]
        public IActionResult StudentsInSpecificClinic(string ClinicName)
        {
            if (ClinicName != null)
            {
                return Ok(professorService.StudentsInSpecificClinic(ClinicName));
            }
            return BadRequest("ادخل اسم العياده");
        }
    }
}
