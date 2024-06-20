using DentalHospital.Data;
using DentalHospital.DTOs;
using DentalHospital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DentalHospital.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor accessor;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> manager;

        public ProfessorService(ApplicationDbContext dbContext, IHttpContextAccessor accessor, IUserService userService, UserManager<ApplicationUser> manager)
        {
            this.dbContext = dbContext;
            this.accessor = accessor;
            this.userService = userService;
            this.manager = manager;
        }

        public async Task<List<string>> StudentsInSpecificClinic()
        {
            var userId = accessor.HttpContext?.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.PrimarySid)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return new List<string>();
            }

            var user = await userService.GetUserById(userId);

            if (user == null || user.Clinic == null)
            {
                return new List<string>();
            }

            var studentNames = await dbContext.Students
                                              .Where(s => s.ClinicId == user.Clinic)
                                              .Select(s => s.Name)
                                              .ToListAsync();

            return studentNames;
        }


        public IQueryable<string>? MedicalReportsOfStudent(string StudentName)
        {
            var student = dbContext.Students.FirstOrDefault(s => s.Name == StudentName);

            var reports = dbContext.MedicalReports.Where(m => m.StudentSSN == student.SSN);

            if (reports != null)
            {
                return reports.Select(r => r.Code);
            }

            return null;
        }

        public async Task<CaseDTO> MedicalReport(string code)
        {
            var result = await dbContext.MedicalReports.FirstOrDefaultAsync(m => m.Code == code);

            CaseDTO caseDTO = new CaseDTO();

            caseDTO.Diagnosis = result.Diagnosis;
            caseDTO.DentalHistory = result.DentalHistory;
            caseDTO.Treatment = result.Treatment;
            caseDTO.Description = result.Description;
            caseDTO.MedicalHistory = result.MedicalHistory;

            return caseDTO;
        }
    }
}
