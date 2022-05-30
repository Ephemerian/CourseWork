using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using System.Data.Entity;

namespace DAL
{
    public class ManagementContext : DbContext
    {
        public ManagementContext():base("ManagementContext") { }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Tour> TourTemplates { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<TransportPlace> TransportPlaces { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
            .HasMany(r => r.Rooms)
            .WithRequired(h => h.Hotel)
            .HasForeignKey(h => h.HotelId)
            .WillCascadeOnDelete();

            modelBuilder.Entity<Transport>()
           .HasMany(t => t.TransportPlaces)
           .WithRequired(t => t.Transport)
           .HasForeignKey(t => t.TransportId)
           .WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);
        }
    }
}
