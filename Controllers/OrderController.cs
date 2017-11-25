using System;
using System.Linq;
using System.Net;
using ApiRest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        public OrderController(UserManager<ApplicationUser> uMgr, CoursierWallonDBContext context) 
            : base(uMgr, context)
        {
        }

        // GET api/Order/GetAllOrderedByDateAsc
        [HttpGet("GetAllOrderedByDateAsc")]
        public IActionResult GetAllOrderedByDateAsc(){
            var listOrder = Context.Order.Include(o => o.BillingAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.PickUpAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.DepositAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.UserIdOrderNavigation)
                                            .OrderBy(o => o.PickUpDate).ToList();
            return Ok(listOrder);
        }

        // GET api/Order/GetAllByUser/{id}
        [HttpGet("GetAllByUser/{userId}")]
        public IActionResult GetAllOrderByUser(string userId){
            if(Context.ApplicationUser.Any(u => u.Id == userId)){
                var listOrder = Context.Order.Where(o => o.UserIdOrder == userId)
                                                .Include(o => o.BillingAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                .Include(o => o.PickUpAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                .Include(o => o.DepositAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                .ToList();
                return Ok(listOrder);
            }
            return NotFound();
        }

        // PUT api/Order/Edit
        [HttpPut("Edit")]
        public IActionResult EditOrder([FromBody]Order order){
            if(Context.Order.Any(o => o.OrderNumber == order.OrderNumber)){
                if(ModelState.IsValid){
                    Context.Attach(order);
                    Context.Entry(order).State = EntityState.Modified;
                    try{
                        Context.SaveChanges();
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