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
        public DbSet<IDS325___Indice_academico.Models.Asignatura> Asignatura { get; set; }
        public DbSet<IDS325___Indice_academico.Models.Persona>? Persona { get; set; }
        
    }
}
