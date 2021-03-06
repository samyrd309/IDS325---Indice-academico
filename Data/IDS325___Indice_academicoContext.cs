using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IDS325___Indice_academico.Models;

namespace IDS325___Indice_academico.Data
{
    public class IDS325___Indice_academicoContext : DbContext
    {
        public IDS325___Indice_academicoContext (DbContextOptions<IDS325___Indice_academicoContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>()
                .HasKey(p => new { p.Matricula, p.Contraseña });
                
            modelBuilder.Entity<Seccion>()
                .HasKey(s => new { s.IdSeccion, s.CodigoAsignatura });

            modelBuilder.Entity<Calificacion>()
                .HasKey(c => new { c.CodigoAsignatura, c.Matricula });
                
        }

        public DbSet<IDS325___Indice_academico.Models.Asignatura> Asignatura { get; set; }
        public DbSet<IDS325___Indice_academico.Models.Persona>? Persona { get; set; }
        public DbSet<IDS325___Indice_academico.Models.Seccion>? Seccion { get; set; }
        public DbSet<IDS325___Indice_academico.Models.Calificacion>? Calificacion { get; set; }

        
    }
}
