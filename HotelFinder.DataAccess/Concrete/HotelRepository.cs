using HotelFinder.DataAccess.Abstract;
using HotelFinder.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinder.DataAccess.Concrete
{
   public class HotelRepository : IHotelRepository
    {
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                hotelDbContext.Hotels.Add(hotel);
                await hotelDbContext.SaveChangesAsync();
                return hotel;
               
            }
        }
                    
        public async Task DeleteHotel(int id)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                var deletehotelDbContext = await GetHotelById(id); // hotelDbContext.Hotels.Find(id) yerine direkt GetHotelById() methodunu da yazabilirdik [GetAllHotels(id)]
                hotelDbContext.Hotels.Remove(deletehotelDbContext);
               await hotelDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            using (var hotelDbContext= new HotelDbContext())
            {
              return await  hotelDbContext.Hotels.ToListAsync();
            }
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                return await hotelDbContext.Hotels.FindAsync(id);  // id primary key olduğundan direkt yazabildik diğer türlü FirstOrDefault kullanılacaktı.
            }
        }

        public async Task< Hotel> GetHotelByName(string name)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                return await hotelDbContext.Hotels.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            }
        }

        public Hotel GetHotelByTiyatro(string tiyatroname)
        {
            using ( var urlTiyatro= new HotelDbContext())
            {
                return urlTiyatro.Hotels.FirstOrDefault(x => x.Name == tiyatroname);
            }
        }
       

        public async  Task< Hotel> UpdateHotel(Hotel hotel)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                hotelDbContext.Update(hotel);
              await  hotelDbContext.SaveChangesAsync();
                return hotel;
            }
        }

     
    }
}
