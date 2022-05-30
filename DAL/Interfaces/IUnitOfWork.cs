using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Hotel> Hotels { get; }
        IRepository<HotelRoom> HotelsRooms { get; }
        IRepository<Tour> ToursTemplates { get; }
        IRepository<Transport> Transports { get; }

        IRepository<User> Users { get; }
        IRepository<Tour> OrderedTours { get; }
        IRepository<HotelRoomReservation> HotelsRoomsReservations { get; }
        IRepository<TransportPlace> TransportsPlace { get; }

        void DeleteDB();
    }
}
