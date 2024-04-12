using Delivery_Service.Domain;
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

        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<RequestForDelivery> RequestForDelivery { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Driver>(entity =>
            {
                //entity.Property(e => e.Id)
                //    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AvailabilityStatus)
                  .HasDefaultValueSql("'offline'");

                entity.Property(e => e.CreateTime)
                   .HasColumnType("timestamp without time zone")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdateTime)
                   .HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<RequestForDelivery>(entity =>
            {
                //entity.Property(e => e.Id)
                //    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreateTime)
                   .HasColumnType("timestamp without time zone")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Status)
                 .HasDefaultValueSql("'pickedup'");

                entity.Property(e => e.OnwayTime)
                 .HasColumnType("timestamp without time zone");

                entity.Property(e => e.DeliveredTime)
                .HasColumnType("timestamp without time zone");

                entity.Property(e => e.DriverId)
                        .IsRequired(false);
            });
        }
    }
}