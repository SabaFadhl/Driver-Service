using DeliveryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Xml;

namespace DeliveryService.Infrastructure
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options) : base(options)
        {
        }

        public virtual DbSet<Delivery> Deliveries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("uuid_generate_v4()");
            });



        }
    }
}