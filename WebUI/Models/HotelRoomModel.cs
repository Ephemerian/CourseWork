using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class HotelRoomModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int Number { get; set; }
        public int SleepingPlaces { get; set; }
        public int Price { get; set; }
        public List<DateTimeOffset> BookedDays { get; set; }
    }
}
