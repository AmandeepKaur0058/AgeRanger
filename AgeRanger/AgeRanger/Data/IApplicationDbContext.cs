using System.Data.Entity;
using AgeRanger.Models;

namespace AgeRanger.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Person> People { get; set; }
        DbSet<AgeGroup> AgeGroups { get; set; }

        int SaveChanges();

    }
}