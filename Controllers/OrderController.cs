using System;
using System.Linq;
using ApiRest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        // GET api/Order/GetAllOrderByDateAsc
        [HttpGet("GetAllOrderByDateAsc")]
        public IActionResult GetAllOrderByDateAsc(){
            using(var context = new _1718_etu32607_DBContext()){
                var listOrder = context.Order.Include(o => o.PickUpAddressNavigation)
                                             .Include(o => o.DepositAddressNavigation)
                                             .Include(o => o.UserIdOrderNavigation)
                                             .OrderBy(o => o.PickUpDate).ToList();
                return Ok(listOrder);
            }
        }
    }
}