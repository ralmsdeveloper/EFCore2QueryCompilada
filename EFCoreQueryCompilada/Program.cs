using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace EFCoreQueryCompilada
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new RalmsContext();
            db.Database.EnsureCreated();
            for (int i = 0; i < 20000; i++)
            {
                db.Clientes.Add(new Clientes
                {
                    RazSocial = $"RALMS DEVELOPER {i}",
                    Fantasia = $"RALMS DEVELOPER {i}",
                    Cidade = $"ITABAIANA {i}",
                    Bairro = $"CENTRO {i}",
                    CNPJ = $"035080{i.ToString("0000")}",
                    Email = "ralms@ralms.net",
                    Site = "www.ralms.net",
                    Estado = "SE",
                    Endereco = $"RUA TESTE. {i}",
                    Numero = i,
                    Observacoes = $"TESTE DE OBERVAÇÃO TESTE {i}"
                });
            }
            db.SaveChanges();
            //Ter o primeiro acesso
            var cliente = db.Clientes.First(); 

            TestarAplicacao(
                testeSimples: (accountNumbers) =>
                {
                    using(var context = new RalmsContext())
                        foreach (var id in accountNumbers)
                        {
                            var temp = context.Clientes.Single(c => c.Id == id);
                        }
                   
                },
                testeQueryCompilada: (accountNumbers) =>
                {
                    var query = EF.CompileQuery((RalmsContext banco, int id) =>
                        db.Clientes.Single(c => c.Id == id));

                    using (var context = new RalmsContext())
                        foreach (var id in accountNumbers)
                        {
                            var temp = query(context, id);
                        }
                    
                });

            Console.ReadKey();
        }

        private static void TestarAplicacao(Action<int[]> testeSimples, Action<int[]> testeQueryCompilada)
        {
            var numeroTeste = NumeroDeTeste(200); 
            var monitor = new Stopwatch();
            monitor.Start();
            testeSimples(numeroTeste);
            monitor.Stop(); 
            Console.WriteLine($"Teste Simples........: {monitor.ElapsedMilliseconds.ToString().PadLeft(4)}ms"); 
            monitor.Restart();
            testeQueryCompilada(numeroTeste);
            monitor.Stop(); 
            Console.WriteLine($"Teste Query Compilada: {monitor.ElapsedMilliseconds.ToString().PadLeft(4)}ms");
        }

        private static int[] NumeroDeTeste(int count)
        {
            var arrayContador = new int[count];
            for (int i = 0; i < count; i++) 
                arrayContador[i] = i + 1;
       
            return arrayContador;
        }
    }
}
