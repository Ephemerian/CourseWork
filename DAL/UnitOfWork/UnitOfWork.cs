using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private ManagementContext ManagementContext;
        private UsageContext UsageContext;

        public UnitOfWork()
        {
            ManagementContext = new ManagementContext();
            UsageContext = new UsageContext();
        }

        public UnitOfWork(ManagementContext ManagementContext, UsageContext UsageContext)
        {
            this.ManagementContext = ManagementContext;
            this.UsageContext = UsageContext;
        }


        private IRepository<Hotel> _hotels;
        private IRepository<HotelRoom> _hotelsrooms;
        private IRepository<Tour> _tourstemplates;
        private IRepository<Transport> _transports;

        private IRepository<TransportPlace> _transportsplases;
        private IRepository<User> _users;
        private IRepository<Tour> _orderedtours;
        private IRepository<HotelRoomReservation> _hotelsroomsreservations;

        public IRepository<Hotel> Hotels { get { if (_hotels == null) _hotels = new GenericRepository<Hotel>(ManagementContext); return _hotels; } }
        public IRepository<HotelRoom> HotelsRooms { get { if (_hotelsrooms == null) _hotelsrooms = new GenericRepository<HotelRoom>(ManagementContext); return _hotelsrooms; } }
        public IRepository<Tour> ToursTemplates { get { if (_tourstemplates == null) _tourstemplates = new GenericRepository<Tour>(ManagementContext); return _tourstemplates; } }
        public IRepository<Transport> Transports { get { if (_transports == null) _transports = new GenericRepository<Transport>(ManagementContext); return _transports; } }

        public IRepository<User> Users { get { if (_users == null) _users = new GenericRepository<User>(UsageContext); return _users; } }
        public IRepository<Tour> OrderedTours { get { if (_orderedtours == null) _orderedtours = new GenericRepository<Tour>(UsageContext); return _orderedtours; } }
        public IRepository<HotelRoomReservation> HotelsRoomsReservations { get { if (_hotelsroomsreservations == null) _hotelsroomsreservations = new GenericRepository<HotelRoomReservation>(UsageContext); return _hotelsroomsreservations; } }
        public IRepository<TransportPlace> TransportsPlace { get { if (_transportsplases == null) _transportsplases = new GenericRepository<TransportPlace>(UsageContext); return _transportsplases; } }

        public void DeleteDB()
        {
            ManagementContext.Database.Delete();
            UsageContext.Database.Delete();
            ManagementContext.Database.Create();
            UsageContext.Database.Create();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ManagementContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
