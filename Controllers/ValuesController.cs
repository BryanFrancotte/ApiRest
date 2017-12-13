using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.Service;
using FirebaseNet.Messaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        FirebaseService service;
        public ValuesController(){
            service = new FirebaseService();
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        // [AllowAnonymous]
        // [HttpGet("Notification")]
        // public async Task<IActionResult> Notification(){
        //     await service.sendFireBaseNotification("cLCshfMyiPQ:APA91bGUBQX06LHQ55MTVfFLrT2_WjIlBGodZ00LbZFCdHC0EzNS3dY-pSi81a3BVYeD0MZwGn4JTAUPm1GohIsZHkUdsWw1TzKhm_t-G8zbOdunanFOq0SrDcD3X-skshyRLWCw2Rym", "coucou", "coucou");
        //     return Ok();
        // }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
