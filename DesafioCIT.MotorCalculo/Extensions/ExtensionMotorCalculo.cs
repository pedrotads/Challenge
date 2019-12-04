using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DesafioCIT.MotorCalculo.Interfaces;
using DesafioCIT.MotorCalculo.Implementation;
using DesafioCIT.MotorCalculo.DataAccessLayer;
using DesafioCIT.MotorCalculo.DataAccessLayer.Models;

public static class DesafioExtensionMotorCalculo
{
    private static List<Cobertura> ListaDeCoberturas()
    {
        return new List<Cobertura>(){
            new Cobertura
            {
                Id = "01",
                Nome = "Morte Acidental",
                Premio = 100.00m,
                Valor = 50000.00m,
                Obrigatorio = true
            },
            new Cobertura
            {
                Id = "02",
                Nome = "Quebra de Ossos",
                Premio = 30.00m,
                Valor = 5000.00m,
                Obrigatorio = false
            },
            new Cobertura
            {
                Id = "03",
                Nome = "Internacao Hospitalar",
                Premio = 50.00m,
                Valor = 10000.00m,
                Obrigatorio = false
            },
            new Cobertura
            {
                Id = "04",
                Nome = "Assistencia Funeraria",
                Premio = 10.00m,
                Valor = 2500.00m,
                Obrigatorio = false
            },
            new Cobertura
            {
                Id = "05",
                Nome = "Invalidez Permanente",
                Premio = 130.00m,
                Valor = 90000.00m,
                Obrigatorio = true
            },
            new Cobertura
            {
                Id = "06",
                Nome = "Assistencia Odontologia Emergencial",
                Premio = 10.00m,
                Valor = 2500.00m,
                Obrigatorio = false
            },
            new Cobertura
            {
                Id = "07",
                Nome = "Diária Incapacidade Temporária",
                Premio = 30.00m,
                Valor = 5000.00m,
                Obrigatorio = false
            },
            new Cobertura
            {
                Id = "08",
                Nome = "Invalidez Funcional",
                Premio = 80.00m,
                Valor = 40000.00m,
                Obrigatorio = true
            },
            new Cobertura
            {
                Id = "09",
                Nome = "Doenças Graves",
                Premio = 100.00m,
                Valor = 50000.00m,
                Obrigatorio = false
            },
            new Cobertura
            {
                Id = "10",
                Nome = "Diagnostico de Cancer",
                Premio = 50.00m,
                Valor = 10000.00m,
                Obrigatorio = false
            }
        };
    }

    private static void MockDataBaseParaDesafio()
    {
        var options = new DbContextOptionsBuilder<DataBaseContext>()
                   .UseInMemoryDatabase("BandoDeDadosDoDesafio");

        var context = new DataBaseContext(options.Options);

        var coberturas = ListaDeCoberturas();

        coberturas.ForEach(Cobertura => context.Coberturas.Add(Cobertura));

        context.SaveChanges();
    }

    //Extensão para injeção das dependências, ela também "gera o banco"
    public static IServiceCollection RegistrarDependenciasMotorCalculo(this IServiceCollection services)
    {
        MockDataBaseParaDesafio();

        //Cria o banco em memória
        services.AddDbContext<DataBaseContext>(opt =>
                      opt.UseInMemoryDatabase("BandoDeDadosDoDesafio"));

        //Faz ineção de dependencia utilizando factory
        services.AddScoped<IMotorCalculo>(provider =>
        {
            var dbContext = provider.GetRequiredService<DataBaseContext>();

            return MotorCalculoFactory.RecuperarInstancia(dbContext);
        });

        return services;
    }
}
