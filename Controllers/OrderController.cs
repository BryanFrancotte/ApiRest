using System;
using System.Linq;
using System.Net;
using ApiRest.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        // GET api/Order/GetAllWithNbItems
        [HttpGet("GetAllWithNbItems")]
        public IActionResult GetAllOrderWithNbItems(){
            var listOrderWithNbItems = Context.Order.Include(o => o.BillingAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                            .Include(o => o.PickUpAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.DepositAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.UserIdOrderNavigation)
                                            .Include(o => o.Parcel)
                                            .Include(o => o.Letter)
                                            .Select( o => new OrderWithNumberItems(){
                                                Order = o,
                                                NbItems = (o.Parcel.Count() + o.Letter.Count())
                                            }).ToList();
            return Ok(listOrderWithNbItems);
        }

        // GET api/Order/GetAllNotComfirmWithNbItems
        [HttpGet("GetAllNotComfirmWithNbItems")]
        public IActionResult GetAllOrderNotConfirmWithNbItems(){
            var listOrderNotConfirmWithNbItems = Context.Order.Include()
        }

        // GET api/Order/GetAllByUser/{id}
        [HttpGet("GetAllByUser/{userId}")]
        public IActionResult GetAllOrderByUser(string userId){
            if(Context.AspNetUsers.Any(u => u.Id == userId)){
                var listOrder = Context.Order.Where(o => o.UserIdOrder == userId)
                                                .Include(o => o.BillingAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                .Include(o => o.PickUpAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                .Include(o => o.DepositAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                .ToList();
                return Ok(listOrder);
            }
            return NotFound();
        }

        // GET api/Order/GetAllStatedByCoursierOrderedByPickUpTime/{id}?{state}
        //[HttpGet("GetAllStatedByCoursierOrderedByPickUpTime/{userId:string}/{state:string}")] on peut faire ça aussi mais deux slash alors 
        [HttpGet("GetAllStatedByCoursierOrderedByPickUpTime/{userId}")]
        public IActionResult GetAllStatedOrderByCoursierOrderedByPickUpTime(string userId, string state){
            if(Context.AspNetUsers.Any(u => u.Id == userId)){
                var listOrder = Context.Order.Where(o => o.CoursierIdOrder == userId && o.State == state)
                                                .Include(o => o.PickUpAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                .Include(o => o.DepositAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                                .Include(o => o.UserIdOrderNavigation)
                                                .ToList();
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

        // POST api/Order/Add
        [HttpPost("Add")]
        public IActionResult AddOrder([FromBody]Order newOrder){
            if(ModelState.IsValid){
                Context.Order.Add(newOrder);
                Context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        //DELETE api/Order/DeleteById/{numOrder}
        [HttpDelete("DeleteById/{numOrder}")]
        public IActionResult DeleteOrderById(long numOrder){
            var orderToDelete = Context.Order.SingleOrDefault(o => o.OrderNumber == numOrder);
            if(orderToDelete != null){
                Context.Order.Remove(orderToDelete);
                Context.SaveChanges();
                return Ok();
            }
            return NotFound(numOrder);
        }
    }
}