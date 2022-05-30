using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logic;

namespace WebUI.Ninject
{
    public static class UIDependencyResolver<T>
    {
        public static dynamic ResolveDependency()
        {
            if (typeof(T) == typeof(IUserLogic))
                return new UserLogic();
            else if (typeof(T) == typeof(ITransportLogic))
                return new TransportLogic();
            else if (typeof(T) == typeof(ITourLogic))
                return new TourLogic();
            else if (typeof(T) == typeof(IHotelLogic))
                return new HotelLogic();
            else return null;
        }
    }
}