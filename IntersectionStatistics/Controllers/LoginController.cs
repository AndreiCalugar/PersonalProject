using IntersectionStatistics.DA;
using IntersectionStatistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntersectionStatistics.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [ActionName ("authenticate")]
        public IHttpActionResult authenticate ([FromBody]User user )
        {

            User dbuser = UserDbHandler.getUsernamePassword(user.username, user.password);
          



            if (dbuser !=null)
            {
                return Ok(new { success = true });
            }
            else
            {
                return Ok(new { success = false , message="Username or password is not correct !" });
            }

        

        }


        [HttpPost]
        [ActionName ("register")]
        public IHttpActionResult register([FromBody] User user)
        {
            Boolean op = UserDbHandler.sendDataRegister(user);

            if (op == false)
            {
                return Ok(new { success = false, message = "User could not be added " });
            }

            return Ok(new { success = true });
        }

        
    }
}
