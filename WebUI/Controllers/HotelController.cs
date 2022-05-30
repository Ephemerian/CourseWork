using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logic.DTOs;
using Logic;
using WebUI.Models;
using AutoMapper;
using WebUI.Ninject;
using System.Web;
using Microsoft.Owin.Host.SystemWeb;


namespace WebUI.Controllers
{
    public class HotelController : ApiController
    {
        public HotelController()
        {          
            HotelLogic = UIDependencyResolver<IHotelLogic>.ResolveDependency();
        }
        public HotelController(IHotelLogic HotelLogic)
        {
            this.HotelLogic = HotelLogic;
        }

        IHotelLogic HotelLogic;

        IMapper HotelControllerMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelDTO, HotelModel>();
                cfg.CreateMap<HotelRoomDTO, HotelRoomModel>();
                cfg.CreateMap<HotelModel, HotelDTO>();
                cfg.CreateMap<HotelRoomModel, HotelRoomDTO>();
            }).CreateMapper();

        // GET api/<controller>
        public IEnumerable<HotelModel> Get()
        {
            return HotelControllerMapper.Map<IEnumerable<HotelDTO>, IEnumerable<HotelModel>>(HotelLogic.GetAllHotels());
        }

        // GET api/<controller>/5
        public HotelModel Get(int Id)
        {
            return HotelControllerMapper.Map<HotelDTO, HotelModel>(HotelLogic.GetHotel(Id));
        }

        // POST api/<controller>

        public void Post([FromBody]HotelModel Hotel)
        {
            HotelLogic.AddHotel(HotelControllerMapper.Map<HotelModel, HotelDTO>(Hotel));
        }

        // PUT api/<controller>/5
        public void Put(int HotelId, [FromBody]HotelRoomModel HotelRoom)
        {
            HotelLogic.AddHotelRoom(HotelId, HotelControllerMapper.Map<HotelRoomModel, HotelRoomDTO>(HotelRoom));
        }

        // DELETE api/<controller>/5
        public void Delete(int Id)
        {
            HotelLogic.DeleteHotel(Id);
        }
    }
}