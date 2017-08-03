using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
namespace EFCoreQueryCompilada
{
    public class RalmsContext : DbContext
    {
        public RalmsContext()
        {}
        
        protected override void OnConfiguring(DbContextOptionsBuilder opcao)
        {
            opcao.UseSqlite("Data Source=RALMS.DB;");
        }

        public DbSet<Clientes> Clientes { get; set; }
    }
     
    public class Clientes
    {
        [Key]
        public int Id { get; set; } 
        public string RazSocial { get; set; }
        public string Fantasia { get; set; }
        public string CNPJ { get; set; } 
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; } 
        public string Email { get; set; }
        public string Site { get; set; } 
        public string Observacoes { get; set; } 

    }
}
 