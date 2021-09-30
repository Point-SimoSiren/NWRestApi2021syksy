using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthwAPI2021syksy.Models;

namespace NorthwAPI2021syksy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly NorthwindContext db = new NorthwindContext();

        // Haku sukunimellä tai 10 annoksina
        [HttpGet]
        [Route("R")]
        public ActionResult GetSomeUsers(int offset, int limit)
        {

            try
            {
               /* if (lastname != null) // Jos lastname on annettu hakuehdoksi
                {
                    List<User> users = db.Users.Where(u => u.Lastname.ToLower().Contains(lastname)).Take(limit).ToList();
                    return Ok(users);
                }
                else // Ilman lastname tietoa
                {*/
                    List<User> users = db.Users.Skip(offset).Take(limit).ToList();
                    return Ok(users);
                
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }

        // Uuden lisääminen
        [HttpPost]
        public ActionResult AddUser([FromBody] User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Ok("Lisättiin käyttäjä id:llä: " + user.UserId);
            }
            catch (Exception e)
            {
                return BadRequest("Virhe. Lue lisää tästä: " + e);
            }
        }
    }
}
