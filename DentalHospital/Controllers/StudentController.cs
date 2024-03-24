using DentalHospital.Data;
using DentalHospital.DTOs;
using DentalHospital.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalHospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly ApplicationDbContext dbContext;

        public StudentController(IStudentService studentService, ApplicationDbContext dbContext)
        {
            this.studentService = studentService;
            this.dbContext = dbContext;
        }

        [HttpGet("ViewDiagnosis")]
        public async Task<IActionResult> ViewDiagnosis(string code)
        {
           var result = await studentService.ViewDiagnosis(code);
            return Ok(result);
        }

        [HttpPost("AddTreatment")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles ="Student")]
        public async Task<IActionResult> AddTreatment(TreatmentDTO treatmentDTO)
        {
            if (ModelState.IsValid == true)
            {
                var medicalreport = await dbContext.MedicalReports.FirstOrDefaultAsync(m => m.PatientCode == treatmentDTO.Code);

                if (medicalreport != null)
                {
                    int result = await studentService.AddTreatment(treatmentDTO);

                    if (result == 1)
                    {
                        return Ok("عملية العلاج تمت");
                    }

                    return BadRequest("عملية العلاج لم تكتمل ");
                }

                return BadRequest("There is no medical report with this code.");
            }

            return BadRequest(ModelState);
        }
    }
}
