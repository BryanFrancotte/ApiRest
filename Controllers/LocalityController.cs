using System;
using System.Linq;
using ApiRest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class LocalityController : BaseController
    {
        public LocalityController(UserManager<ApplicationUser> uMgr, CoursierWallonDBContext context) 
            : base(uMgr, context)
        {
        }

        // GET api/Locality/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllLocality(){
            var listLocality = Context.Locality.ToList();
            return Ok(listLocality);
        }

        // PUT api/Locality/Add
        [HttpPut("Add")]
        public IActionResult AddLocality([FromBody]Locality locality){
            if(ModelState.IsValid){
                Context.Locality.Add(locality);
                Context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}