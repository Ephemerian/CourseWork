using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class HotelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public virtual List<HotelRoomModel> Rooms { get; set; }
        public string Address { get; set; }
    }
}
