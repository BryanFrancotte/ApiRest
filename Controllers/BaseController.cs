using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiRest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    public abstract class BaseController : Controller
    {
        private UserManager<ApplicationUser> _uMgr;
        public UserManager<ApplicationUser> UserManager 
        {
            get{ return _uMgr;}
        }
        private CoursierWallonDBContext _context;
        
        public CoursierWallonDBContext Context
        {
            get { return _context;}
            set { _context = value;}
        }
        
        public BaseController(UserManager<ApplicationUser> uMgr, CoursierWallonDBContext context)
        {
            _uMgr=uMgr;
            _context = context;
        }
       protected async Task<ApplicationUser> GetCurrentUserAsync()
       {
            if(this.HttpContext.User==null)
                throw new Exception("L'utilisateur n'est pas identifié");
            Claim userNameClaim=this.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type==ClaimTypes.NameIdentifier);
            if(userNameClaim==null)
                throw new Exception("Le token JWT semble ne pas avoir été interprété correctement");
            return await _uMgr.FindByNameAsync(userNameClaim.Value);
       }
    }
}