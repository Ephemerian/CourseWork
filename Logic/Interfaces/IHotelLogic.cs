using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.DTOs;
namespace Logic
{
    public interface IHotelLogic
    {
        void AddHotel(HotelDTO NewHotel);
        void AddHotelRoom(int HotelId, HotelRoomDTO NewHotelRoom);
        IEnumerable<HotelDTO> GetAllHotels();
        HotelDTO GetHotel(int Id);
        void DeleteHotel(int Id);
    }
}
