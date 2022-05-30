using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic
{
    public interface IUserLogic
    {
        void AddUser(UserDTO NewUser);
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUser(int Id);
        void DeleteUser(int Id);
        void EditUser(int Id, UserDTO User);
        UserDTO Login(string Login, string Password);
        void ReserveTour(int UserId, int TourId);
        void ReserveRoom(int UserId, int HotelId, int HotelRoomId, DateTimeOffset ArrivalDate, DateTimeOffset DepartureDate);
        void ReserveTicket(int UserId, int TransportId, int SeatNumber);
    }
}
