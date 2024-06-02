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
        public IActionResult StudentsInSpecificClinic()
        {
            return Ok(professorService.StudentsInSpecificClinic());
        }


        [HttpGet("MedicalReportsOfStudent")]
        public IActionResult MedicalReportsOfStudent(string StudentName)
        {
            if (StudentName != null)
            {
                var result = professorService.MedicalReportsOfStudent(StudentName);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound("لا يوجد تقارير لهذا الطالب");
            }

            return BadRequest("ادخل اسم الطالب");
        }

        [HttpGet("MedicalReport")]
        public async Task<IActionResult> MedicalReport(string code)
        {
            if (code != null)
            {
                var result = await professorService.MedicalReport(code);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound("لا يوجد تقرير بهذا الكود");
            }

            return BadRequest("من فضلك ادخل الكود");
        }
    }
}
