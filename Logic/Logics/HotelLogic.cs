using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Logic.DTOs;
using AutoMapper;
using Logic.Exceptions;

namespace Logic
{
    public class HotelLogic : IHotelLogic
    {
         IUnitOfWork UoW;

        public HotelLogic(IUnitOfWork UoW)
        {
            HotelLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelDTO, Hotel>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<Hotel, HotelDTO>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            }).CreateMapper();
            this.UoW = UoW;
        }

        IMapper HotelLogicMapper;
    
        public HotelLogic()
        {
            HotelLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelDTO, Hotel>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<Hotel, HotelDTO>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO > ();
            }).CreateMapper();
            UoW = LogicDependencyResolver.ResolveUoW();
        }

        public void AddHotel(HotelDTO NewHotel)
        {
            if (UserLogic.CurrentUser == null || UserLogic.CurrentUser.UserType != DTOs.UserType.Manager)
                throw new WrongUserException("Function availible only for managers");
            UoW.Hotels.Add(HotelLogicMapper.Map<HotelDTO, Hotel>(NewHotel));
        }

        public void DeleteHotel(int Id)
        {
            if (UserLogic.CurrentUser == null || UserLogic.CurrentUser.UserType != DTOs.UserType.Manager)
                throw new WrongUserException("Function availible only for managers");
            UoW.Hotels.Delete(Id);
        }

        public void AddHotelRoom(int HotelId, HotelRoomDTO NewHotelRoom)
        {
            if (UserLogic.CurrentUser == null || UserLogic.CurrentUser.UserType != DTOs.UserType.Manager)
                throw new WrongUserException("Function availible only for managers");
            Hotel hotel = UoW.Hotels.GetAll(x => x.Id == HotelId, x => x.Rooms).FirstOrDefault();
            HotelRoom room = HotelLogicMapper.Map<HotelRoomDTO, HotelRoom>(NewHotelRoom);
            room.Hotel = hotel;
            hotel.Rooms.Add(room);
            UoW.Hotels.Modify(hotel.Id, hotel);
        }

        public IEnumerable<HotelDTO> GetAllHotels()
        {
            return HotelLogicMapper.Map<IEnumerable<Hotel>, List<HotelDTO>>(UoW.Hotels.GetAll(h => h.Rooms));
        }

        public HotelDTO GetHotel(int Id)
        {
            return HotelLogicMapper.Map<Hotel, HotelDTO>(UoW.Hotels.GetAll(x => x.Id == Id, x => x.Rooms).FirstOrDefault());
        }
    }
}
