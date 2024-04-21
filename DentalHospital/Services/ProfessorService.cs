using DentalHospital.Data;
using DentalHospital.Models;

namespace DentalHospital.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly ApplicationDbContext dbContext;

        public ProfessorService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<string> StudentsInSpecificClinic(string ClinicName)
        {
            List<string> StudentNames = new List<string>();
            var students = dbContext.Students.Where(s => s.ClinicName == ClinicName);
            foreach (var student in students)
            {
                StudentNames.Add(student.Name);
            }
            return StudentNames;
        }
    }
}
