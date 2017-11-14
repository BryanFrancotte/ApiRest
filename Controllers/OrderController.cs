using System;
using System.Linq;
using System.Net;
using ApiRest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        // GET api/Order/GetAllOrderedByDateAsc
        [HttpGet("GetAllOrderedByDateAsc")]
        public IActionResult GetAllOrderedByDateAsc(){
            using(var context = new _1718_etu32607_DBContext()){
                var listOrder = context.Order.Include(o => o.BillingAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                             .Include(o => o.PickUpAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                             .Include(o => o.DepositAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                             .Include(o => o.UserIdOrderNavigation)
                                             .OrderBy(o => o.PickUpDate).ToList();
                return Ok(listOrder);
            }
        }

        // GET api/Order/GetAllByUser/{id}
        [HttpGet("GetAllByUser/{userId}")]
        public IActionResult GetAllOrderByUser(int userId){
            using(var context = new _1718_etu32607_DBContext()){
                if(context.User.Any(u => u.UserId == userId)){
                    var listOrder = context.Order.Where(o => o.UserIdOrder == userId)
                                                 .Include(o => o.BillingAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                 .Include(o => o.PickUpAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                 .Include(o => o.DepositAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                 .ToList();
                    return Ok(listOrder);
                }
                return NotFound();
            }
        }

        // PUT api/Order/Edit
        [HttpPut("Edit")]
        public IActionResult EditOrder([FromBody]Order order){
            using(var context = new _1718_etu32607_DBContext()){
                if(context.Order.Any(o => o.OrderNumber == order.OrderNumber)){
                    if(ModelState.IsValid){
                        context.Attach(order);
                        context.Entry(order).State = EntityState.Modified;
                        try{
                            context.SaveChanges();
                            return Ok();
                        }catch(DbUpdateException e){
                            Console.WriteLine(e.Message);//TODO : Géré les acces concurentielle
                        }
                    }
                }
                return NotFound();
            }
        }
    }
}