using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiRest.DTO;
using ApiRest.Infrastructure;
using ApiRest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class JwtController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public JwtController(IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtOptions = jwtOptions.Value;
        ThrowIfInvalidOptions(_jwtOptions);
        _logger = loggerFactory.CreateLogger<JwtController>();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] LoginDTO loginInfo)
        {
            var user=await _userManager.FindByEmailAsync(loginInfo.Email);
            //var user=await _userManager.FindByNameAsync(loginInfo.UserName);
            if(user==null)
            {
                return Unauthorized();
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await  _signInManager.PasswordSignInAsync(user,loginInfo.Password,true,false);
            if(!result.Succeeded)
            {
                _logger.LogInformation($"Invalid username ({loginInfo.Email}) or password ({loginInfo.Password})");
                return BadRequest("Invalid credentials");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, 
                        ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), 
                        ClaimValueTypes.Integer64),
            };
            IEnumerable<string> roles=await _userManager.GetRolesAsync(user);
            IEnumerable<Claim> allClaimsWithRoles = roles.Select(roleName=>new Claim("Role",roleName))
            .Union(claims);

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: allClaimsWithRoles,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };
            return Ok(response);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
        if (options == null) throw new ArgumentNullException(nameof(options));

        if (options.ValidFor <= TimeSpan.Zero)
        {
            throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
        }

        if (options.SigningCredentials == null)
        {
            throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
        }

        if (options.JtiGenerator == null)
        {
            throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - 
                            new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                            .TotalSeconds);
    }
}