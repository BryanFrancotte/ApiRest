using System;
using System.Linq;
using ApiRest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        // GET api/Address/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllAddress(){
            using(var context = new _1718_etu32607_DBContext()){
                var listAddress = context.Address.Include(a => a.LocalityIdAddressNavigation).ToList();
                return Ok(listAddress);
            }
        }

        // GET api/Address/GetAllPickUpByUser/{userId}
        [HttpGet("GetAllPickUpByUser/{userId}")]
        public IActionResult GetAllPickUpAddressByUser(int userId){
            using(var context = new _1718_etu32607_DBContext()){
                if(context.User.Any(u => u.UserId == userId)){
                    var listAddress = context.Address.Include(a => a.LocalityIdAddressNavigation)
                                                    .Where(a => a.AddressId == context.Order.Where(o => o.UserIdOrder == userId)
                                                                                            .Single().PickUpAddress)
                                                    .ToList();
                    return Ok(listAddress);
                }
                return NotFound();
            }
        }

        // GET api/Address/GetAllDepositByUser/{userId}
        [HttpGet("GetAllDepositByUser/{userId}")]
        public IActionResult GetAllDepositAddressByUser(int userId){
            using(var context = new _1718_etu32607_DBContext()){
                if(context.User.Any(u => u.UserId == userId)){
                    var listAddress = context.Address.Include(a => a.LocalityIdAddressNavigation)
                                                    .Where(a => a.AddressId == context.Order.Where(o => o.UserIdOrder == userId)
                                                                                            .Single().DepositAddress)
                                                    .ToList();
                    return Ok(listAddress);
                }
                return NotFound();
            }
        }

        // PUT api/Address/Add
        [HttpPut("Add")]
        public IActionResult AddAddress([FromBody]Address address){
            using(var context = new _1718_etu32607_DBContext()){
                if(ModelState.IsValid){
                    context.Address.Add(address);
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
        }
    }
}