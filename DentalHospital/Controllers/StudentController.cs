﻿using DentalHospital.Data;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPatch("AddTreatment")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles ="Student")]
        public IActionResult AddTreatment(TreatmentDTO treatmentDTO)
        {
            if (ModelState.IsValid == true)
            {
                int result =  _studentService.AddTreatment(treatmentDTO);

                if (result == 1)
                {
                    return Ok("عملية العلاج تمت");
                }

                return BadRequest("There is no medical report with this code.");
            }

            return BadRequest(ModelState);
        }


        [HttpPatch("TreatmentInDiagnosis")]
        public async Task<IActionResult> TreatmentInDiagnosis(TreatmentInDiagnosisDTO treatmentInDiagnosisDTO)
        {
            if (ModelState.IsValid == true)
            {
                int result = await _studentService.TreatmentInDiagnosis(treatmentInDiagnosisDTO);

                if (result == 1)
                {
                    return Ok("Medical report is Updated.");
                }

                return BadRequest("Medical report is not Updated.");
            }

            return BadRequest(ModelState);
        }

        [HttpPatch("ConvertToCinic")]
        public IActionResult ConvertToCinic(ConvertToClinicDTO convertToClinicDTO)
        {
            if (ModelState.IsValid == true)
            {
                int result =  _studentService.ConvertToClinic(convertToClinicDTO);

                if (result == 1)
                {
                    return Ok("The conversion process is completed successfully");
                }
                return BadRequest("There is no Medical Report with this code");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddSession")]
        public async Task<IActionResult> AddSession(SessionDTO sessionDTO)
        {
            if (ModelState.IsValid == true)
            {
                int result  = await _studentService.AddSession(sessionDTO);

                if (result == 1)
                {
                    return Ok("تمت عمليه الاعاده بنجاح");
                }

                return BadRequest("لم تتم عمليه الاعاده بنجاح");
            }

            return BadRequest(ModelState);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string name)
        {
            if (name != null)
            {
                var search = await _studentService.Search(name);

                if (search.Any())
                {
                    return Ok(search);
                }

                return NotFound("لا يوجد اسماء");
            }

            return BadRequest("ادخل الاسم من فضلك");
        }

        [HttpGet("Cases")]
        public IActionResult Cases()
        {
            var result = _studentService.Cases();

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("لا يوجد تقارير");
        }

        [HttpGet("SessionsDates")]
        public IActionResult SessionsDates(string MedicalCode)
        {
            if(MedicalCode != null)
            {
                IEnumerable<DateTime> Dates = _studentService.SessionsDates(MedicalCode);
                if (Dates != null)
                {
                    return Ok(Dates.ToList());
                }
                return NotFound("لا يوجد اعادات لهذا الكود");
            }

            return BadRequest("ادخل الكود");
        }

        [HttpGet("SessionData")]
        public async Task<IActionResult> SessionData(DateTime date)
        {
            SessionReturnDTO session = await _studentService.SessionData(date);

            if (session != null)
            {
                return Ok(session);
            }

            return NotFound("لا يوجد بيانات");
        }

        [HttpGet("Clinics")]
        public async Task<IActionResult> Clinics()
        {
            return Ok(await _studentService.clinics());
        }
    }
}
