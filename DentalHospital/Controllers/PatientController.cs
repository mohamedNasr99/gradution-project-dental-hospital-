using DentalHospital.Data;
using DentalHospital.DTOs;
using DentalHospital.Models;
using DentalHospital.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalHospital.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly ApplicationDbContext dbContext;

        public PatientController(IPatientService patientService, ApplicationDbContext dbContext)
        {
            this.patientService = patientService;
            this.dbContext = dbContext;
        }

        [HttpPost("PatientRegister")]
        public async Task<IActionResult> PatientRegister(ReservationDTO reservationDTO)
        {
            if(ModelState.IsValid == true)
            {
                Patient? patientExist = await patientService.IsExist(reservationDTO.Name);
                if(patientExist != null)
                {
                    return BadRequest("A patient with the same Name already exists.");
                }

                Patient? patient = await patientService.PatientRegister(reservationDTO);

                if (patient != null)
                {
                    return Ok(patient.Code);
                }
                return BadRequest("عملية التسجيل لم تنجح");
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Reservation")]
        public async Task<IActionResult> Reservation(string SNN)
        {
            if (SNN != null)
            {
                Cases? cases = await dbContext.Cases.FirstOrDefaultAsync();
                int reports = await dbContext.MedicalReports.CountAsync(p => p.dateTime.Day == DateTime.Now.Day);
                int HalfPermissible = cases.PermissibleCases / 2;
                if (cases != null)
                {
                    if(cases.PermissibleCases != 0)
                    {
                        if (reports < cases.PermissibleCases)
                        {

                            if (reports < HalfPermissible)
                            {

                                MedicalReport? medicalReport = await patientService.Reservation(SNN);
                                if (medicalReport != null)
                                {
                                    return Ok(new
                                    {
                                        Code = medicalReport.Code,
                                        Duration = "You are from 8 am to 11 am"
                                    });
                                }
                                return BadRequest("من فضلك سجل بياناتك الاول ثم احجز");
                            }
                            else
                            {
                                MedicalReport? medicalReport = await patientService.Reservation(SNN);
                                if (medicalReport != null)
                                {
                                    return Ok(new
                                    {
                                        Code = medicalReport.Code,
                                        Duration = "You are from 11 am to 2 pm"
                                    });
                                }
                                return BadRequest("من فضلك سجل بياناتك الاول ثم احجز");

                            }

                        }
                        return BadRequest("معذرة الحجز اكتمل انهردا");
                    }
                    return BadRequest("لا يوجد حجز اليوم");
                }

                return BadRequest("من فضلك ي ادمن ادخل قيمه للحالات المسموحه");
            }

            return BadRequest("دخل قيمه جدع حبيبي");
        }
    }
}
