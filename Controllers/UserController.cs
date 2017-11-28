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
    public class UserController : BaseController
    {
        public UserController(UserManager<ApplicationUser> uMgr, CoursierWallonDBContext context) 
            : base(uMgr, context)
        {
        }

        // POST api/User/Connexion
        // [HttpPost("Connexion")]
        // public IActionResult Connexion([FromBody]UserTemp userTemp){// 0/100 si je fais cette merde !!!!!!!
        //     using(var context = new CoursierWallonDBContext()){
        //         User user = context.User.SingleOrDefault(u => u.Email.CompareTo(userTemp.Email) == 0);
        //         if(user != null){
        //             if(user.Password.CompareTo(userTemp.Password) == 0){
        //                 return Ok(user);
        //             }
        //         }
        //         return Unauthorized();
        //     }
        // }

        // GET api/User/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllUser(){
            var listUser = Context.AspNetUsers.Include(u => u.AddressIdUserNavigation).ThenInclude(a => a.LocalityIdAddressNavigation).ToList();
            return Ok(listUser);
        }

        // GET api/User/GetAllCoursier
        // [HttpGet("GetAllCoursier")]
        // public IActionResult GetAllCoursier(){
        //     using(var context = new CoursierWallonDBContext()){
        //         var listCoursier = context.ApplicationUser.Select(u => u.CodeRoleUserNavigation.Name.CompareTo("Coursier") == 0).ToList();
        //         return Ok(listCoursier);
        //     }
        // }

        // GET api/User/GetById/{userId}
        [HttpGet("GetById/{userId}")]
        public IActionResult GetUserById(string userId){
            ApplicationUser user = Context.AspNetUsers.SingleOrDefault(u => u.Id.CompareTo(userId) == 0);
            if(user != null){
                return Ok(user);
            }
            return NotFound();
        }

        // POST api/User/Add
        [HttpPost("Add")]
        public IActionResult AddUser([FromBody]ApplicationUser user){
            if(ModelState.IsValid){
                if(!Context.AspNetUsers.Any(u => u.Email.ToLower().CompareTo(user.Email.ToLower())==0)){
                    Context.AspNetUsers.Add(user);
                    Context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }

        // PUT api/User/Edit
        [HttpPut("Edit")]
        public IActionResult EditUser([FromBody]ApplicationUser user){
            if(ModelState.IsValid){
                if(Context.AspNetUsers.Any(u => u.Id.CompareTo(user.Id) == 0)){
                    Context.Attach(user);
                    Context.Entry(user).State = EntityState.Modified;
                    try{
                        Context.SaveChanges();
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