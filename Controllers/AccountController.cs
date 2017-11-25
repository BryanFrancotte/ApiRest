using System.Threading.Tasks;
using ApiRest.DTO;
using ApiRest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this._userManager=userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewUserDTO dto)
        {
            
                var newUser=new ApplicationUser{
                        UserName=dto.UserName,
                        Email = dto.Email
                        
                };
                IdentityResult result = await _userManager.CreateAsync(newUser,dto.Password);
                // TODO: retourner un Created à la place du Ok;
                return (result.Succeeded)?Ok():(IActionResult)BadRequest();
        }
    }
}