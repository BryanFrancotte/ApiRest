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
                    .ThenInclude(a => a.LocalityIdAddressNavigation)
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
                var listAddress = Context.Order
                    .Include(a => a.PickUpAddressNavigation)
                    .ThenInclude(a => a.LocalityIdAddressNavigation)
                    .Where(o => o.UserIdOrder == userId)
                    .Select(o=>o.PickUpAddress).ToList();
                return Ok(listAddress);
            }
            return NotFound();
        }

        // POST api/Address/addressExists bouger et le mettre en business
        [HttpPost("addressExists")]
        public IActionResult addressExists([FromBody]Address newAddress){
            if(ModelState.IsValid){
                Address address = Context.Address.SingleOrDefault(a => String.Equals(a.Street, newAddress.Street, StringComparison.OrdinalIgnoreCase) 
                                                            && String.Equals(a.HouseNumber, newAddress.HouseNumber, StringComparison.OrdinalIgnoreCase) 
                                                            && String.Equals(a.BoxNumber, newAddress.BoxNumber, StringComparison.OrdinalIgnoreCase)
                                                            && String.Equals(a.LocalityIdAddressNavigation.Name, newAddress.LocalityIdAddressNavigation.Name, StringComparison.OrdinalIgnoreCase) 
                                                            && a.LocalityIdAddressNavigation.PostalCode == newAddress.LocalityIdAddressNavigation.PostalCode);
                if(address != null){
                    return Ok(address);
                }else {
                    Locality locality = Context.Locality.SingleOrDefault(l => String.Equals(l.Name, newAddress.LocalityIdAddressNavigation.Name, StringComparison.OrdinalIgnoreCase)
                                                                        && l.PostalCode == newAddress.LocalityIdAddressNavigation.PostalCode);
                    if(locality != null){
                        newAddress.LocalityIdAddress = locality.LocalityId;
                        newAddress.LocalityIdAddressNavigation = null;
                    }
                    return Ok(newAddress);
                }
            }
            return BadRequest();
        }
    }
}