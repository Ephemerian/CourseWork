using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Logic.DTOs;
using AutoMapper;
using Logic.Exceptions;

namespace Logic
{
    public class UserLogic : IUserLogic
    {
        static UserLogic()
        {
            CurrentUser = null;
        }

        IUnitOfWork UoW;

        public UserLogic(IUnitOfWork UoW)
        {
            UserLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Transport, TransportDTO>();
                cfg.CreateMap<TransportDTO, Transport>();
                cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
                cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
                cfg.CreateMap<TourDTO, Tour>();
                cfg.CreateMap<Tour, TourDTO>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
                cfg.CreateMap<HotelRoomReservationDTO, HotelRoomReservation>();
                cfg.CreateMap<HotelRoomReservation, HotelRoomReservationDTO>();

            }).CreateMapper();
            this.UoW = UoW;
            HotelRoomToDto = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            }).CreateMapper();
        }

        IMapper UserLogicMapper;
        IMapper HotelRoomToDto;
        public static UserDTO CurrentUser;
        public UserLogic()
        {
            UserLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Transport, TransportDTO>();
                cfg.CreateMap<TransportDTO, Transport>();
                cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
                cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
                cfg.CreateMap<TourDTO, Tour>();
                cfg.CreateMap<Tour, TourDTO>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
                cfg.CreateMap<HotelRoomReservationDTO, HotelRoomReservation>();
                cfg.CreateMap<HotelRoomReservation, HotelRoomReservationDTO>();
            }).CreateMapper();
            HotelRoomToDto = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            }).CreateMapper();
            UoW = LogicDependencyResolver.ResolveUoW();
        }

        public void AddUser(UserDTO NewUser)
        {
            if (UoW.Users.GetAll(u => u.Login == NewUser.Login).Count() != 0)
                throw new InvalidLoginPasswordCombinationException("Entered login is already taken");
            UoW.Users.Add(UserLogicMapper.Map<UserDTO, User>(NewUser));
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            UoW.DeleteDB();
            return UserLogicMapper.Map<IEnumerable<User>,List<UserDTO>>(UoW.Users.GetAll(u => u.HotelRoomReservations, u => u.TransportTickets, u => u.Tours));
        }

        public UserDTO GetUser(int Id)
        {
            return UserLogicMapper.Map<User, UserDTO>(UoW.Users.GetAll(u => u.Id == Id, u => u.HotelRoomReservations, u => u.TransportTickets, u => u.Tours).FirstOrDefault());
        }

        public void EditUser(int Id, UserDTO User)
        {
            UoW.Users.Modify(Id, UserLogicMapper.Map<UserDTO, User>(User));
        }

        public void DeleteUser(int Id)
        {
            UoW.Users.Delete(Id);
        }

        public UserDTO Login(string Login, string Password)
        {
            UserDTO user = UserLogicMapper.Map<User, UserDTO>(UoW.Users.GetAll(u => u.Login == Login && u.Password == Password, u => u.TransportTickets, u => u.Tours, u => u.HotelRoomReservations).FirstOrDefault());
            if (user == null)
                throw new InvalidLoginPasswordCombinationException("Invalid login password combination");
            CurrentUser = user;
            return user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public void ReserveTour(int UserId, int TourId)
        {
            if (CurrentUser == null)
                throw new WrongUserException("Login to reserve tour");
            Tour tour = UoW.ToursTemplates.Get(TourId);
            User user = UoW.Users.GetAll(u => u.Id == UserId, u => u.Tours).FirstOrDefault();
            user.Tours.Add(tour);
            UoW.Users.Modify(user.Id, user);
        }

        public void ReserveRoom(int UserId, int HotelId, int HotelRoomId, DateTimeOffset ArrivalDate, DateTimeOffset DepartureDate)
        {
            if (CurrentUser == null)
                throw new WrongUserException("Login to reserve room");
            User user = UoW.Users.GetAll(u => u.Id == UserId, u => u.HotelRoomReservations).FirstOrDefault();
            HotelRoom hotelroom = UoW.Hotels.GetAll(h => h.Id == HotelId, h => h.Rooms).FirstOrDefault().Rooms.FirstOrDefault(r => r.Id == HotelRoomId);

            foreach (DateTimeOffset d in hotelroom.BookedDays)
            {
                DateTimeOffset FakeArrival = ArrivalDate;
                DateTimeOffset FakeDeparture = DepartureDate;
                while (FakeArrival.CompareTo(FakeDeparture) < 0)
                {
                    if ((d.Date.CompareTo(FakeArrival.Date) == 0))
                        throw new AlreadyBookedItemException("Room is not availible for " + d.Day + "." + d.Month + "." + d.Year);
                    FakeArrival = FakeArrival.AddDays(1);
                }
            }
            var reserv = new HotelRoomReservation(hotelroom, user.Name, user.Surname, ArrivalDate.Date, DepartureDate.Date);
            while (ArrivalDate.CompareTo(DepartureDate) < 0)
            {
                hotelroom.BookedDays.Add(ArrivalDate.Date);
                ArrivalDate = ArrivalDate.AddDays(1);
            }
            UoW.HotelsRooms.Modify(hotelroom.Id, hotelroom);
            UoW.HotelsRoomsReservations.Add(reserv);
            user.HotelRoomReservations.Add(reserv);
            UoW.Users.Modify(user.Id, user);
        }

        public void ReserveTicket(int UserId, int TransportId, int SeatNumber)
        {
            if (CurrentUser == null)
                throw new WrongUserException("Login to reserve ticket");
            User user = UoW.Users.Get(UserId);
            Transport transport = UoW.Transports.GetAll(t => t.Id == TransportId, t => t.TransportPlaces).FirstOrDefault();
            TransportPlace transportplace = transport.TransportPlaces.FirstOrDefault(p => p.Number == SeatNumber);
            if (transportplace.IsBooked)
                throw new AlreadyBookedItemException("Transport place is already booked");
            else
            {
                transportplace.IsBooked = true;
                UoW.Transports.Modify(transport.Id, transport);
                user.TransportTickets.Add(new TransportTicket(transportplace, user.Name, user.Surname));
                UoW.Users.Modify(user.Id, user);
            }
        }
    }
}
