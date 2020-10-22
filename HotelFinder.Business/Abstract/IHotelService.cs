using HotelFinder.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinder.Business.Abstract
{
    public interface IHotelService
    {
        Task<List<Hotel>> GetAllHotels();
        Task<Hotel> GetHotelById(int id);
        Task<Hotel> GetHotelByName(string name);
        Hotel GetHotelByTiyatro(string tiyatroname);

        Task<Hotel>CreateHotel(Hotel hotel);
        Task<Hotel> UpdateHotel(Hotel hotel);

        Task DeleteHotel(int id);
    }
}
