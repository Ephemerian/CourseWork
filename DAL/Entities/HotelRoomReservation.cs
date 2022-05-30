using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HotelRoomReservation
    {
        public HotelRoomReservation() { }
        public HotelRoomReservation(HotelRoom HotelRoom, string ClientName, string ClientSurname, DateTimeOffset ArrivalDate, DateTimeOffset DepartureDate)
        {
            HotelName = HotelRoom.Hotel.Name;
            HotelStars = HotelRoom.Hotel.Stars;
            HotelAddress = HotelRoom.Hotel.Address;
            HotelRoomNumber = HotelRoom.Number;
            HotelRoomSleepingPlaces = HotelRoom.SleepingPlaces;
            HotelRoomPrice = HotelRoom.Price;
            this.ClientName = ClientName;
            this.ClientSurname = ClientSurname;
            this.ArrivalDate = ArrivalDate;
            this.DepartureDate = DepartureDate;
        }

        public int Id { get; set; }
        public string HotelName { get; set; }
        public int HotelStars { get; set; }
        public string HotelAddress { get; set; }
        public int HotelRoomNumber { get; set; }
        public int HotelRoomSleepingPlaces { get; set; }
        public int HotelRoomPrice { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public DateTimeOffset ArrivalDate { get; set; }
        public DateTimeOffset DepartureDate { get; set; }

    }
}
