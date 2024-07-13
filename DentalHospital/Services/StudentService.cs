using DentalHospital.Data;
using DentalHospital.DTOs;
using DentalHospital.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DentalHospital.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserService _userService;

        public StudentService(ApplicationDbContext dbContext, IHttpContextAccessor accessor, IUserService userService)
        {
            _dbContext = dbContext;
            _accessor = accessor;
            _userService = userService;
        }

        public int AddTreatment(TreatmentDTO treatmentDTO)
        {
            if (treatmentDTO != null)
            {
                var medicalreport = _dbContext.MedicalReports.FirstOrDefault(m => m.Code == treatmentDTO.Code);

                if (medicalreport == null)
                {
                    return 0;
                }

                medicalreport.Description = treatmentDTO.Description;
                medicalreport.Treatment = treatmentDTO.Treatment;
                medicalreport.StudentSSN = treatmentDTO.StudentSSN;

                 _dbContext.Update(medicalreport);
                 _dbContext.SaveChanges();

                return 1;
            }

            return 0;
        }


        public async Task<int> TreatmentInDiagnosis(TreatmentInDiagnosisDTO treatmentInDiagnosisDTO)
        {
            MedicalReport? medicalReport = await _dbContext.MedicalReports.FirstOrDefaultAsync(m => m.Code == treatmentInDiagnosisDTO.Code);

            medicalReport.MedicalHistory = treatmentInDiagnosisDTO.MedicalHistory;
            medicalReport.DentalHistory = treatmentInDiagnosisDTO.DentalHistory;
            medicalReport.Diagnosis = treatmentInDiagnosisDTO.Diagnosis;
            medicalReport.Clinic = treatmentInDiagnosisDTO.Clinic;

            _dbContext.MedicalReports.Update(medicalReport);
           int result = _dbContext.SaveChanges();

            if (result == 1)
            {
                return 1;
            }
            return 0;
        }

        public int ConvertToClinic(ConvertToClinicDTO convertToClinicDTO)
        {
            MedicalReport? report = _dbContext.MedicalReports.FirstOrDefault(m => m.Code == convertToClinicDTO.Code);

            if (report == null)
            {
                return 0;
            }

            report.Clinic = convertToClinicDTO.ClinicName;

            _dbContext.Update(report);
            _dbContext.SaveChanges();

            return 1;
        }

        public async Task<int> AddSession(SessionDTO sessionDTO)
        {
            Session session = new Session();
            session.Treatment = sessionDTO.Treatment;
            session.session = session.session;
            session.MedicalReportCode = sessionDTO.MedicalReportCode;
            session.Date = DateTime.Now;

            await _dbContext.Sessions.AddAsync(session);
            int result = await _dbContext.SaveChangesAsync();

            if (result == 1)
            {
                return 1;
            }

            return 0;
        }

        public async Task<IEnumerable<string>> Search(string name)
        {
            IQueryable<string> query = _dbContext.Students.Select(s => s.Name);

            query = query.Where(n => n.Contains(name));

            return await query.ToListAsync();
        }

        public async Task<IQueryable<string>> Cases()
        {
            var userid = _accessor.HttpContext?.User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.PrimarySid))?.Value;

            if (userid == null)
            {
                throw new InvalidOperationException("User ID claim not found.");
            }

            var user = await _userService.GetUserById(userid);

            var reports = _dbContext.MedicalReports
    .Where(m => m.StudentSSN == user.SSN);

            return reports.Select(m => m.Code);
        }


        public IEnumerable<DateTime> SessionsDates(string MedicalCode)
        {
            var sessions = _dbContext.Sessions.Where(s => s.MedicalReportCode == MedicalCode).ToList();

            return sessions.Select(s => s.Date);
        }

        public async Task<SessionReturnDTO?> SessionData(DateTime date)
        {
            Session? session = await _dbContext.Sessions.FirstOrDefaultAsync(s => s.Date == date);

            if (session == null)
            {
                return null;
            }

            SessionReturnDTO sessionReturnDTO = new SessionReturnDTO();

            sessionReturnDTO.Session = session.session;
            sessionReturnDTO.Treatment = session.Treatment;

            return sessionReturnDTO;
        }

        public async Task<IEnumerable<string>> clinics()
        {
            var clinics = await _dbContext.Clinics.ToListAsync();
            return clinics.Select(c => c.Name);
        }

        public async Task<int> CheckPatient(string Code)
        {
           var report = await _dbContext.MedicalReports.FirstOrDefaultAsync(m => m.Code == Code);
            if (report != null)
            {
                return 1;
            }
            return 0;
        }

        public async Task<MedicalReport?> GetMedicalReport(string Code)
        {
            var report = await _dbContext.MedicalReports.FirstOrDefaultAsync(m => m.Code == Code);
            if (report != null)
            {
                return report;
            }
            return null;
        }

        public int CheckFinish(string Code)
        {
            var report =  _dbContext.MedicalReports.FirstOrDefault(m => m.Code == Code);
            report.IsFinish = true;
            _dbContext.MedicalReports.Update(report);
             var res =  _dbContext.SaveChanges();
            if (res == 1)
            {
                return 1;
            }
            return 0;

        }



    }
}
