using System;
using System.Linq;
using ApiRest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        // GET api/Address/GetAllByUser/{userId}
        [HttpGet("GetAllByUser/{userId}")]
        public IActionResult GetAllByUser(int userId){
            using(var context = new _1718_etu32607_DBContext()){
                if(context.User.Any(u => u.UserId == userId)){
                    var listAddress = context.Address.Where(a => a.AddressId == context.Order.Where(o => o.UserIdOrder == userId).Single().PickUpAddress);
                    //fontionne pas :/
                    return Ok(listAddress);
                }
                return NotFound();
            }
        }
    }
}