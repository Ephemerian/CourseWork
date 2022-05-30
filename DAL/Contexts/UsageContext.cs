using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using System.Data.Entity;

namespace DAL
{
    public class UsageContext : DbContext
    {
        public UsageContext():base("UsageContext") { }
        public DbSet<User> Users { get; set; }
        public DbSet<HotelRoomReservation> HotelRoomReservations { get; set; }
        public DbSet<TransportTicket> TransportTickets { get; set; }
        public DbSet<Tour> OrderedTours { get; set; }
    }
}
