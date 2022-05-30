using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Logic;

namespace WebUI
{
    public class Binder : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserLogic>().To<UserLogic>();
            Bind<ITransportLogic>().To<TransportLogic>();
            Bind<ITourLogic>().To<TourLogic>();
            Bind<IHotelLogic>().To<HotelLogic>();
        }
    }
}