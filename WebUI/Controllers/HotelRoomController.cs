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

namespace WebUI.Controllers
{
    public class HotelRoomController : ApiController
    {
        public HotelRoomController()
        {
            HotelLogic = UIDependencyResolver<IHotelLogic>.ResolveDependency();
            HotelRoomControllerMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelDTO, HotelModel>();
                cfg.CreateMap<HotelRoomDTO, HotelRoomModel>();
                cfg.CreateMap<HotelModel, HotelDTO>();
                cfg.CreateMap<HotelRoomModel, HotelRoomDTO>();
            }).CreateMapper();
        }
        IHotelLogic HotelLogic;

        IMapper HotelRoomControllerMapper;
        // GET api/<controller>
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST api/<controller>
        public void Post([FromBody]HotelRoomModel HotelRoom)
        {
            int HotelId = HotelRoom.HotelId;
            HotelRoom.HotelId = new int();
            HotelLogic.AddHotelRoom(HotelId, HotelRoomControllerMapper.Map<HotelRoomModel, HotelRoomDTO>(HotelRoom));
        }

        /*// PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }*/
    }
}