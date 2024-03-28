using DentalHospital.Data;
using DentalHospital.DTOs;
using DentalHospital.Models;
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
            if (code != null)
            {
                MedicalReport? medicalReport = await studentService.ViewDiagnosis(code);

                if (medicalReport != null)
                {
                    return Ok(new
                    {
                        Medical = medicalReport.MedicalHistory,
                        Dental = medicalReport.DentalHistory,
                        Diagnosis = medicalReport.Diagnosis
                    });
                }
                return BadRequest("The medical report is null");
            }

            return BadRequest("code is null.");
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

        [HttpGet("CheckCode")]
        public async Task<IActionResult> CheckCode(string code)
        {
            if (code != null)
            {
                int result = await studentService.CheckCode(code);

                if (result == 1)
                {
                    return RedirectToAction("TreatmentInDiagnosis");
                }

                return BadRequest("This patient did not pay money.");
            }

            return BadRequest("code is null.");
        }

        [HttpPost("TreatmentInDiagnosis")]
        public async Task<IActionResult> TreatmentInDiagnosis(TreatmentInDiagnosisDTO treatmentInDiagnosisDTO)
        {
            if (ModelState.IsValid == true)
            {
                int result = await studentService.TreatmentInDiagnosis(treatmentInDiagnosisDTO);

                if (result == 1)
                {
                    return Ok("Medical report is Added.");
                }

                return BadRequest("Medical report is not Added.");
            }

            return BadRequest(ModelState);
        }

        [HttpPatch("ConvertToCinic")]
        public async Task<IActionResult> ConvertToCinic(ConvertToClinicDTO convertToClinicDTO)
        {
            if (ModelState.IsValid == true)
            {
                int result = await studentService.ConvertToClinic(convertToClinicDTO);

                if (result == 1)
                {
                    return Ok("The conversion process is completed successfully");
                }
                return BadRequest("There is no patient with this code");
            }
            return BadRequest(ModelState);
        }
    }
}
