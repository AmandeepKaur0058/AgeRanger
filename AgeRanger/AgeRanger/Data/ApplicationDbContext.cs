using System.Data.Entity;
using AgeRanger.Models;

namespace AgeRanger.Data
{
    public partial class ApplicationDbContext : DbContext,IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public virtual DbSet<AgeGroup> AgeGroups { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeGroup>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.LastName)
                .IsUnicode(false);
        }
    }
}
