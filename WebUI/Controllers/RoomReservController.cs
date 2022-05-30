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
    public class RoomReservController : ApiController
    {
        public RoomReservController()
        {
            this.UserLogic = UIDependencyResolver<IUserLogic>.ResolveDependency();
        }
        IUserLogic UserLogic;

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]Wrap wrap)
        {
            UserLogic.ReserveRoom(wrap.UserId, wrap.HotelId, wrap.RoomId, DateTimeOffset.Parse(wrap.Arrival), DateTimeOffset.Parse(wrap.Departure));
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        public class Wrap
        {
            public int UserId { get; set; }
            public int HotelId { get; set; }
            public int RoomId { get; set; }
            public string Arrival { get; set; }
            public string Departure { get; set; }
        }
    }
}