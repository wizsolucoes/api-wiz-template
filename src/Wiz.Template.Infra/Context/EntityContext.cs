using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Wiz.Template.Domain.Models;
using Wiz.Template.Infra.Mappings;

namespace Wiz.Template.Infra.Context
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
             : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new AddressMap());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entity => entity.Entity.GetType().GetProperty("DateCreated") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateCreated").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateCreated").IsModified = false;
                }
            }

            return base.SaveChanges();
        }
    }
}
