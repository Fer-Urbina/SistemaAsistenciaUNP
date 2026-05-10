using Microsoft.EntityFrameworkCore;
using SistemaAsistenciaUNP.Models;

namespace SistemaAsistenciaUNP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Carrera> Carreras { get; set; }

        public DbSet<Asignatura> Asignaturas { get; set; }

        public DbSet<Docente> Docentes { get; set; }

        public DbSet<Estudiante> Estudiantes { get; set; }

        public DbSet<Grupo> Grupos { get; set; }

        public DbSet<Matricula> Matriculas { get; set; }

        public DbSet<SesionClase> SesionesClase { get; set; }

        public DbSet<Asistencia> Asistencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Estudiante)
                .WithMany()
                .HasForeignKey(m => m.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Grupo)
                .WithMany()
                .HasForeignKey(m => m.GrupoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Estudiante)
                .WithMany()
                .HasForeignKey(a => a.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.SesionClase)
                .WithMany()
                .HasForeignKey(a => a.SesionClaseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}