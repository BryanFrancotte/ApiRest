using System;
using System.Collections.Generic;
using System.Linq;
using ApiRest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controllers
{
    
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // POST api/User/Connexion
        [HttpPost("Connexion")]
        public IActionResult Connexion([FromBody]UserTemp userTemp){// 0/100 si je fais cette merde !!!!!!!
            using(var context = new _1718_etu32607_DBContext()){
                User user = context.User.SingleOrDefault(u => u.Email.CompareTo(userTemp.Email) == 0);
                if(user != null){
                    if(user.Password.CompareTo(userTemp.Password) == 0){
                        return Ok(user);
                    }
                }
                return Unauthorized();
            }
        }

        // GET api/User/GetAllCoursier
        [HttpGet("GetAllCoursier")]
        public IActionResult GetAllCoursier(){
            using(var context = new _1718_etu32607_DBContext()){
                var listCoursier = context.User.Select(u => u.CodeRoleUserNavigation.Name.CompareTo("Coursier") == 0).ToList();
                return Ok(listCoursier);
            }
        }

        // GET api/User/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllUser(){
            using(var context = new _1718_etu32607_DBContext()){
                var listUser = context.User.Include(u => u.AddressIdUserNavigation).ThenInclude(a => a.LocalityIdAddressNavigation).ToList();
                return Ok(listUser);
            }
        }
    }
}