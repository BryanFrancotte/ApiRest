using System;
using System.Linq;
using System.Net;
using ApiRest.Models;
using ApiRest.Service;
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
        FirebaseService _service;
        public OrderController(UserManager<ApplicationUser> uMgr, CoursierWallonDBContext context) 
            : base(uMgr, context)
        {
            _service = new FirebaseService();
        }

        // GET api/Order/GetAllWithNbItems
        [HttpGet("GetAllWithNbItems")]
        public IActionResult GetAllOrderWithNbItems(){
            var listOrderWithNbItems = Context.Order.Include(o => o.BillingAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                            .Include(o => o.PickUpAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.DepositAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.UserIdOrderNavigation)
                                            .Include(o => o.CoursierIdOrderNavigation)
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
            var listOrderNotConfirmWithNbItems = Context.Order.Include(o => o.BillingAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                            .Include(o => o.PickUpAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.DepositAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.UserIdOrderNavigation)
                                            .Include(o => o.CoursierIdOrderNavigation)
                                            .Include(o => o.Parcel)
                                            .Include(o => o.Letter)
                                            .Select( o => new OrderWithNumberItems(){
                                                Order = o,
                                                NbItems = (o.Parcel.Count() + o.Letter.Count())
                                            })
                                            .Where(o => o.Order.State == "NOT_ACCEPTED")
                                            .ToList();
            return Ok(listOrderNotConfirmWithNbItems);
        }
        
        // GET api/Order/GetAllComfirmWithNbItems
        [HttpGet("GetAllComfirmWithNbItems")]
        public IActionResult GetAllOrderConfirmWithNbItems(){
            var listOrderConfirmWithNbItems = Context.Order.Include(o => o.BillingAddressNavigation).ThenInclude(a => a.LocalityIdAddressNavigation)
                                            .Include(o => o.PickUpAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.DepositAddressNavigation).ThenInclude(a=>a.LocalityIdAddressNavigation)
                                            .Include(o => o.UserIdOrderNavigation)
                                            .Include(o => o.CoursierIdOrderNavigation)
                                            .Include(o => o.Parcel)
                                            .Include(o => o.Letter)
                                            .Select( o => new OrderWithNumberItems(){
                                                Order = o,
                                                NbItems = (o.Parcel.Count() + o.Letter.Count())
                                            })
                                            .Where(o => o.Order.State == "ACCEPTED")
                                            .ToList();
            return Ok(listOrderConfirmWithNbItems);
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

        [AllowAnonymous]
        // PUT api/Order/Edit
        [HttpPut("Edit")]
        public IActionResult EditOrder([FromBody]Order order){
            if(Context.Order.Any(o => o.OrderNumber == order.OrderNumber)){
                if(ModelState.IsValid){
                    Context.Attach(order);
                    Context.Entry(order).State = EntityState.Modified;
                    try{
                        Context.SaveChanges();
                        string androidToken = order.UserIdOrderNavigation.AndroidToken;
                        if(androidToken != null){
                            _service.sendFireBaseNotification(order.UserIdOrderNavigation.AndroidToken, "Commande acceptée", "livraison par : " + order.CoursierIdOrderNavigation.UserName);
                        }
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
            var orderToDelete = Context.Order.Include(o => o.UserIdOrderNavigation).SingleOrDefault(o => o.OrderNumber == numOrder);
            if(orderToDelete != null){
                Context.Order.Remove(orderToDelete);
                Context.SaveChanges();
                string androidToken = orderToDelete.UserIdOrderNavigation.AndroidToken;
                if(androidToken != null){
                    _service.sendFireBaseNotification(androidToken, "Commande refusée", "Malheureusement nous ne pouvons prendre en charge le colis.");
                }
                return Ok();
            }
            return NotFound(numOrder);
        }
    }
}