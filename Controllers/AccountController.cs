using System.Diagnostics;
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
        private RoleManager<IdentityRole> _roleManager;
        private CoursierWallonDBContext _context;
        public AccountController(UserManager<ApplicationUser> userManager,  RoleManager<IdentityRole> roleManager, CoursierWallonDBContext context)
        {
            this._userManager=userManager;
            this._roleManager = roleManager;
            this._context=context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewUserDTO dto)
        {
                var newUser=new ApplicationUser{
                        UserName=dto.UserName,
                        Email = dto.Email
                        
                };
                IdentityResult result = await _userManager.CreateAsync(newUser,dto.Password);
                bool userExist = await _roleManager.RoleExistsAsync("USER");
                if(!userExist){
                    await _roleManager.CreateAsync(new IdentityRole("USER"));
                }
                await _userManager.AddToRoleAsync(newUser, "USER");
                // TODO: retourner un Created à la place du Ok;
                return (result.Succeeded)?Ok():(IActionResult)BadRequest();
        }

        [HttpPost("AddCoursier")]
        public async Task<IActionResult> AddCoursier([FromBody]NewUserDTO dto){
            var newUser = new ApplicationUser{
                UserName = dto.UserName,
                Email = dto.Email
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, dto.Password);
            bool coursierExist = await _roleManager.RoleExistsAsync("COURSIER");
            if(!coursierExist){
                await _roleManager.CreateAsync(new IdentityRole("COURSIER"));
            }
            await _userManager.AddToRoleAsync(newUser, "COURSIER");
            // TODO: retourner un Created à la place du Ok;
            return (result.Succeeded)?Ok():(IActionResult)BadRequest();
        }

        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin([FromBody]NewUserDTO dto)
        {
            var newUser=new ApplicationUser{
                        UserName = dto.UserName,
                        Email = dto.Email,
                };
                IdentityResult result = await _userManager.CreateAsync(newUser,dto.Password);
                bool adminExist = await _roleManager.RoleExistsAsync("ADMIN");
                if(!adminExist){
                    await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
                }
                await _userManager.AddToRoleAsync(newUser,"ADMIN");

                // TODO: retourner un Created à la place du Ok;
                return (result.Succeeded)?Ok():(IActionResult)BadRequest();
        }
    }
}