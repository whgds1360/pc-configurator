using Microsoft.EntityFrameworkCore;
using PcConfigurator.shared.DataBase;

namespace PcConfigurator.helpers.TableStructures;

public class ApplicationContext : DbContext
{
    public DbSet<Config> Configs { get; set; }
    public DbSet<Cpu> Cpus { get; set; }
    public DbSet<Gpu> Gpus { get; set; }
    public DbSet<Ram> Rams { get; set; }
    public DbSet<Motherboard> Motherboards { get; set; }
    public DbSet<PowerUnit> PowerUnits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (DataBaseManager.GetStatus() == OrderStatus.Connect)
        {
            optionsBuilder.UseMySQL(connectionString: DataBaseManager.GetConnectionUrl());
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Ошибка при создании структуры таблиц!");
        }
    }
}
 