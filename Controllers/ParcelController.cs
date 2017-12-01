using System;
using ApiRest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class ParcelController : BaseController
    {
        public ParcelController(UserManager<ApplicationUser> uMgr, CoursierWallonDBContext context) 
            : base(uMgr, context)
        {
        }

        // POST api/Parcel/Add
        [HttpPost("Add")]
        public IActionResult AddParcel([FromBody] Parcel newParcel){
            if(ModelState.IsValid){
                Context.Parcel.Add(newParcel);
                Context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}