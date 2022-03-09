using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Infra.Mapping;

namespace Wiz.Template.Infra.Context
{
    public class EntityContext : DbContext
    {
        #pragma warning disable CS8618
        public EntityContext(DbContextOptions<EntityContext> options)
             : base(options) { }

        public DbSet<Example> Examples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExampleMap());
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
