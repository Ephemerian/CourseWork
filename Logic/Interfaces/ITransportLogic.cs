using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic
{
    public interface ITransportLogic
    {
        void AddTransport(TransportDTO NewTransport, int AvailibleSeats, int PriceForTicket);
        IEnumerable<TransportDTO> GetAllTransport();
        TransportDTO GetTransport(int Id);
        void DeleteTransport(int Id);
    }
}
