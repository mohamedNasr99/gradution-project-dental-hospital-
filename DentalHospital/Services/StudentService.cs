using DentalHospital.Data;
using DentalHospital.DTOs;
using DentalHospital.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalHospital.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext dbContext;

        public StudentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int AddTreatment(TreatmentDTO treatmentDTO)
        {
            if (treatmentDTO != null)
            {
                var medicalreport = dbContext.MedicalReports.FirstOrDefault(m => m.Code == treatmentDTO.Code);

                if (medicalreport == null)
                {
                    return 0;
                }

                medicalreport.Description = treatmentDTO.Description;
                medicalreport.Treatment = treatmentDTO.Treatment;
                medicalreport.StudentSSN = treatmentDTO.StudentSSN;

                 dbContext.Update(medicalreport);
                 dbContext.SaveChanges();

                return 1;
            }

            return 0;
        }


        public async Task<int> TreatmentInDiagnosis(TreatmentInDiagnosisDTO treatmentInDiagnosisDTO)
        {
            MedicalReport? medicalReport = await dbContext.MedicalReports.FirstOrDefaultAsync(m => m.Code == treatmentInDiagnosisDTO.Code);

            medicalReport.MedicalHistory = treatmentInDiagnosisDTO.MedicalHistory;
            medicalReport.DentalHistory = treatmentInDiagnosisDTO.DentalHistory;
            medicalReport.Diagnosis = treatmentInDiagnosisDTO.Diagnosis;

            dbContext.MedicalReports.Update(medicalReport);
           int result = dbContext.SaveChanges();

            if (result == 1)
            {
                return 1;
            }
            return 0;
        }

        public int ConvertToClinic(ConvertToClinicDTO convertToClinicDTO)
        {
            MedicalReport? report = dbContext.MedicalReports.FirstOrDefault(m => m.Code == convertToClinicDTO.Code);

            if (report == null)
            {
                return 0;
            }

            report.Clinic = convertToClinicDTO.ClinicName;

            dbContext.Update(report);
            dbContext.SaveChanges();

            return 1;
        }

        public async Task<int> AddSession(SessionDTO sessionDTO)
        {
            Session session = new Session();
            session.Treatment = sessionDTO.Treatment;
            session.session = session.session;
            session.MedicalReportCode = sessionDTO.MedicalReportCode;
            session.Date = DateTime.Now;

            await dbContext.Sessions.AddAsync(session);
            int result = await dbContext.SaveChangesAsync();

            if (result == 1)
            {
                return 1;
            }

            return 0;
        }

        public async Task<IEnumerable<string>> Search(string name)
        {
            IQueryable<string> query = dbContext.Students.Select(s => s.Name);

            query = query.Where(n => n.Contains(name));

            return await query.ToListAsync();
        }

        public IEnumerable<string> Cases(string SSN)
        {
            IQueryable<MedicalReport> reports = dbContext.MedicalReports;

            reports = reports.Where(m => m.StudentSSN == SSN);

            return reports.Select(m => m.Code);
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

        public IEnumerable<DateTime> SessionsDates(string MedicalCode)
        {
            var sessions = dbContext.Sessions.Where(s => s.MedicalReportCode == MedicalCode).ToList();

            return sessions.Select(s => s.Date);
        }

        public async Task<SessionReturnDTO?> SessionData(DateTime date)
        {
            Session? session = await dbContext.Sessions.FirstOrDefaultAsync(s => s.Date == date);

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
            var clinics = await dbContext.Clinics.ToListAsync();
            return clinics.Select(c => c.Name);
        }

    }
}
