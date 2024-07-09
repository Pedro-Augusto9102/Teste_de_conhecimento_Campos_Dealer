using Microsoft.EntityFrameworkCore;
using Teste_de_conhecimento_Campos_Dealer.Models.Entities;

namespace Teste_de_conhecimento_Campos_Dealer.Data
{
    public class AppBdContext: DbContext
    {
        public AppBdContext(DbContextOptions<AppBdContext> options) : base(options)
        {      
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
