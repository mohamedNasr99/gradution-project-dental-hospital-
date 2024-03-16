using DentalHospital.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentalHospital.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }

        public ApplicationDbContext()
        {
            
        }

        public virtual DbSet<Admin>? Admins { get; set; }
        public virtual DbSet<Clinic>? Clinics { get; set; }
        public virtual DbSet<MedicalReport>? MedicalReports { get; set; }
        public virtual DbSet<Patient>? Patients { get; set; }
        public virtual DbSet<Professor>? Professors { get; set; }
        public virtual DbSet<Receptionist>? Receptionists { get; set; }
        public virtual DbSet<Session>? Sessions { get; set; }
        public virtual DbSet<Student>? Students { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasKey(e => e.Id);

            modelBuilder.Entity<Admin>().HasKey(a => a.SSN);

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Name)
                .IsRequired();

                entity.Property(e => e.Address)
                .IsRequired();

                entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsRequired();
            });

            modelBuilder.Entity<Clinic>().HasKey(c => c.Id);

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.Property(e => e.Name)
                .HasMaxLength(70)
                .IsRequired();
            });

            modelBuilder.Entity<MedicalReport>()
                .HasOne(e => e.Clinic)
                .WithMany(e => e.MedicalReports)
                .HasForeignKey(e => e.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalReport>().HasKey(c => c.Id);

            modelBuilder.Entity<MedicalReport>()
                .HasOne(e => e.Student)
                .WithMany(e => e.MedicalReports)
                .HasForeignKey(e => e.StudentSSN)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>().HasKey(p => p.SSN);

            modelBuilder.Entity<MedicalReport>()
                .HasOne(e => e.Patient)
                .WithMany(e => e.MedicalReports)
                .HasForeignKey(e => e.PatientSSN)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>().HasKey(s => s.Id);

            modelBuilder.Entity<Session>()
                .HasOne(e => e.MedicalReport)
                .WithMany(e => e.Sessions)
                .HasForeignKey(e => e.MedicalReportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>().HasKey(s => s.SSN);

            modelBuilder.Entity<Student>()
                .HasOne(e => e.Clinic)
                .WithMany(e => e.Students)
                .HasForeignKey(e => e.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Professor>().HasKey(p => p.SSN);

            modelBuilder.Entity<Professor>()
                .HasOne(e => e.Clinic)
                .WithMany(e => e.Professors)
                .HasForeignKey(e => e.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receptionist>().HasKey(s => s.SSN);

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(a => a.BirthDate)
                .HasColumnType("DATE");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(a => a.BirthDate)
                .HasColumnType("DATE");
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.Property(a => a.BirthDate)
                .HasColumnType("DATE");
            });

            modelBuilder.Entity<Receptionist>(entity =>
            {
                entity.Property(a => a.BirthDate)
                .HasColumnType("DATE");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.Property(a => a.Date)
                .HasColumnType("DATE");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(a => a.BirthDate)
                .HasColumnType("DATE");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(a => a.BirthDate)
                .HasColumnType("DATE");
            });
        }

    }
}
