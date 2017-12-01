using System;
using System.Collections.Generic;
using System.Linq;
using ApiRest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : BaseController
    {
        public AddressController(UserManager<ApplicationUser> uMgr, CoursierWallonDBContext context) : base(uMgr, context)
        {
        }

        // GET api/Address/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllAddress(){
            var listAddress = Context.Address.Include(a => a.LocalityIdAddressNavigation).ToList();
            return Ok(listAddress);
        }

        // GET api/Address/GetAllPickUpByUser/{userId}
        [HttpGet("GetAllPickUpByUser/{userId}")]
        public IActionResult GetAllPickUpAddressByUser(string userId){
            if(Context.AspNetUsers.Any(u => u.Id == userId)){
                var listAddress = Context.Order
                    .Include(a => a.PickUpAddressNavigation)
                    .Where(o => o.UserIdOrder == userId)
                    .Select(o=>o.PickUpAddressNavigation).ToList();
                // var listAddress = new List<Address>();
                // foreach(Order order in listOrderFromUser){
                //     var address = context.Address.Include(a => a.LocalityIdAddressNavigation).Single(a => a.AddressId == order.PickUpAddress);
                //     listAddress.Add(address);
                // } => pas bon car je fais N+1 requête (N = nombre de commande de l'utilisateur)
                return Ok(listAddress);
            }
            return NotFound();
        }

        // GET api/Address/GetAllDepositByUser/{userId}
        [HttpGet("GetAllDepositByUser/{userId}")]
        public IActionResult GetAllDepositAddressByUser(string userId){
            if(Context.AspNetUsers.Any(u => u.Id == userId)){
                var listOrderFromUser = Context.Order.Where(o => o.UserIdOrder == userId).ToList();
                var listAddress = new List<Address>();
                foreach(Order order in listOrderFromUser){
                    var address = Context.Address.Include(a => a.LocalityIdAddressNavigation).Single(a => a.AddressId == order.DepositAddress);
                    listAddress.Add(address);
                }
                return Ok(listAddress);
            }
            return NotFound();
        }

        // PUT api/Address/Add
        [HttpPut("Add")]
        public IActionResult AddAddress([FromBody]Address address){
            if(ModelState.IsValid){
                Context.Address.Add(address);
                Context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}