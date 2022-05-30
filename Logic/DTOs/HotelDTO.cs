using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTOs
{
    public class HotelDTO
    {
        public HotelDTO() { }
        public HotelDTO(string Name, int Stars, string Address)
        {
            this.Name = Name;
            this.Stars = Stars;
            this.Address = Address;
            Rooms = new List<HotelRoomDTO>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public virtual List<HotelRoomDTO> Rooms { get; set; }
        public string Address { get; set; }
    }
}
