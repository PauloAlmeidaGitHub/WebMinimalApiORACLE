using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebMinimalApiORACLE.Models;

namespace WebMinimalApiORACLE.Config
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            Database.EnsureCreated();  // Se o Banco não existir será criado
        }

        public DbSet<Produto> PRODUTO{get; set;}
        public DbSet<OrgaFotrSub> ORGA_FOTR_SUB { get; set; }
    }
}
