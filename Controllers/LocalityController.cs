using System;
using System.Linq;
using ApiRest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class LocalityController : Controller
    {
        // GET api/Locality/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllLocality(){
            using(var context = new _1718_etu32607_DBContext()){
                var listLocality = context.Locality.ToList();
                return Ok(listLocality);
            }
        }

        // PUT api/Locality/Add
        [HttpPut("Add")]
        public IActionResult AddLocality([FromBody]Locality locality){
            using(var context = new _1718_etu32607_DBContext()){
                if(ModelState.IsValid){
                    context.Locality.Add(locality);
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
        }
    }
}