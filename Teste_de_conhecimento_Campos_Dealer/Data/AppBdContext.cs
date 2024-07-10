using Microsoft.EntityFrameworkCore;
using Teste_de_conhecimento_Campos_Dealer.Models.Entities;

namespace Teste_de_conhecimento_Campos_Dealer.Data
{
    public class AppBdContext: DbContext
    {
        public AppBdContext(DbContextOptions<AppBdContext> options) : base(options)
        {      
            
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Venda> Venda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasKey(c => c.Id);
            modelBuilder.Entity<Produto>().HasKey(p => p.Id);
            modelBuilder.Entity<Venda>().HasKey(v => v.Id);

            modelBuilder.Entity<Venda>()
                .HasOne(v => v.cliente)
                .WithMany()
                .HasForeignKey(v => v.clienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Venda>()
                .HasOne(v => v.produto)
                .WithMany()
                .HasForeignKey(v => v.produtoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
