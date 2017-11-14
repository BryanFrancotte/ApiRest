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

        // GET api/User/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllUser(){
            using(var context = new _1718_etu32607_DBContext()){
                var listUser = context.User.Include(u => u.AddressIdUserNavigation).ThenInclude(a => a.LocalityIdAddressNavigation).ToList();
                return Ok(listUser);
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

        // GET api/User/GetById/{userId}
        [HttpGet("GetById/{userId}")]
        public IActionResult GetUserById(int userId){
            using(var context = new _1718_etu32607_DBContext()){
                User user = context.User.SingleOrDefault(u => u.UserId == userId);
                if(user != null){
                    return Ok(user);
                }
                return NotFound();
            }
        }

        // POST api/User/Add
        [HttpPost("Add")]
        public IActionResult AddUser([FromBody]User user){
            using(var context = new _1718_etu32607_DBContext()){
                if(ModelState.IsValid){
                    if(!context.User.Any(u => u.Email.ToLower().CompareTo(user.Email.ToLower())==0)){
                        context.User.Add(user);
                        context.SaveChanges();
                        return Ok();
                    }
                }
                return BadRequest();
            }
        }

        // PUT api/User/Edit
        [HttpPut("Edit")]
        public IActionResult EditUser([FromBody]User user){
            using(var context = new _1718_etu32607_DBContext()){
                if(ModelState.IsValid){
                    if(context.User.Any(u => u.UserId == user.UserId)){
                        context.Attach(user);
                        context.Entry(user).State = EntityState.Modified;
                        try{
                            context.SaveChanges();
                            return Ok();
                        }catch(DbUpdateConcurrencyException e){
                            Console.WriteLine(e.Message);//TODO : Géré les acces concurentielle
                        }
                    }
                    return NotFound();
                }
                return BadRequest();
            }
        }
    }
}