using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelFinder.Business.Abstract;
using HotelFinder.Business.Contrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]  // ApiController varlığı ile Validation için if döngüsü oluşturmaya veya hata döndürmeye gerek yoktur.
    public class HotelsController : ControllerBase
    {

        
        private IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

     
        

        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels); //200+Data
        }
        /// <summary>
        /// Get Hotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotels = await _hotelService.GetHotelById(id);
            if (hotels != null)
            {
                return Ok(hotels);
            }
            return NotFound(); //404
        }
        [HttpGet]
        [Route("/{name}")]
        public async Task<IActionResult> GetHotelByName(string name)
            
        {
            var hotel = await _hotelService.GetHotelByName(name);
            if (hotel!=null)
            {
                return Ok(hotel);
            }
          return NotFound();
        }

        [HttpGet]
        [Route( "{tiyatroname}")]
        public IActionResult Tiyatro(string tiyatroname)
        {
            var tiyatro = _hotelService.GetHotelByTiyatro(tiyatroname);
            if (tiyatro!=null)
            {
                return Ok(tiyatro);
            }
            return NotFound();
        }

        public async Task<IActionResult> GetHotelByNameandID(string name,int id)
        {
            {
                return Ok();
            }
          
        }


        /// <summary>
        /// Post New Hotels (Create)
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        
        public async Task<IActionResult> Post([FromBody] Hotel hotel) //ModelState.IsValid işlemine gerek duyulmadı Çünkü  [ApiController] var
        {
            //if (ModelState.IsValid)
            //{
            //    var createdHotel1 = _hotelService.CreateHotel(hotel);
            //    return CreatedAtAction("Get", new { id = createdHotel1.Id }, createdHotel1);
            //}
            //return BadRequest(ModelState);   Eğer [ApiController] olmasaydı bu işlem ile validation işlemlerini yapıyor olucaktık.
            //ModelState.IsValid => validation da belirtilen tüm required şıklarını uygulayanlara true, eksik olanlara false döner.
            var createdHotel= await _hotelService.CreateHotel(hotel);
            return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel); //201+Data
        }
        /// <summary>
        /// Update Hotels
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] Hotel hotel)
        {
            if (await _hotelService.GetHotelById(hotel.Id)!=null)
            {
                return Ok(await _hotelService.UpdateHotel(hotel));
            }
            return NotFound() ;
        }
        /// <summary>
        /// Remove Hotels
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]  // parametre önemli
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (await _hotelService.GetHotelById(id) != null)
            {
               await _hotelService.DeleteHotel(id);
                return Ok(); //200
            }
            return NotFound();
        }
        //public ActionResult<string> Delete(int id)
        //{

        //    if (_hotelService.GetHotelById(id) != null)
        //    {
        //        _hotelService.DeleteHotel(id);
        //        return "Başarılı"; //200
        //    }
        //    return "Başarısız";
        //}

    }
}
