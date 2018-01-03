using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.DTO;
using ApiRest.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<ApplicationUser> uMgr, CoursierWallonDBContext context, RoleManager<IdentityRole> roleManager) 
            : base(uMgr, context)
        {
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("UpdateFirebaseToken")]
        public async Task<IActionResult> UpdateFirebaseTokenAsync([FromBody] FirebaseTokenDTO dto){
            ApplicationUser user = await UserManager.FindByIdAsync(dto.UserId);
            if(user != null){
                if(user.AndroidToken != dto.AndroidToken){
                    user.AndroidToken = dto.AndroidToken;
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    return (result.Succeeded)?Ok():(IActionResult)BadRequest();
                }
                return Ok();
            }
            return BadRequest();
        }

        // GET api/User/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllUser(){
            var listUser = Context.AspNetUsers.Include(u => u.AddressIdUserNavigation).ThenInclude(a => a.LocalityIdAddressNavigation).ToList();
            return Ok(listUser);
        }

        // GET api/User/GetAllCoursier
        [HttpGet("GetAllCoursier")]
        public IActionResult GetAllCoursier(){
            var listCouriser = UserManager.GetUsersInRoleAsync("COURSIER");
            return Ok(listCouriser.Result);
        }

        // GET api/User/GetById/{userId}
        [HttpGet("GetById/{userId}")]
        public IActionResult GetUserById(string userId){
            ApplicationUser user = Context.AspNetUsers.SingleOrDefault(u => u.Id.CompareTo(userId) == 0);
            if(user != null){
                return Ok(user);
            }
            return NotFound();
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