using Microsoft.EntityFrameworkCore;
using DesafioCIT.MotorCalculo.DataAccessLayer.Models;

namespace DesafioCIT.MotorCalculo.DataAccessLayer
{
    //Classe de contexto do banco do Entity
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {

        }

        public DbSet<Cobertura> Coberturas { get; set; }
    }
}

