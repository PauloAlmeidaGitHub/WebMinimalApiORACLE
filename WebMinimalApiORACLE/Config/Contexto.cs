using Microsoft.EntityFrameworkCore;
using WebMinimalApiORACLE.Models;

namespace WebMinimalApiORACLE.Config
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            Database.EnsureCreated();  // Se o Banco não existe será criado
        }

        public DbSet<Produto> Produto {get; set;}
    }
}
