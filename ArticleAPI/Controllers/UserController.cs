using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArticleAPI.Controllers
{
    public class UserController : ApiController
    {
       
 
        [HttpGet]
        public IHttpActionResult GetAllUsers() {

            using (var db = new EntityContext()) {
                var users = db.ArtUsers.ToList<ArtUser>();
                return Ok(users);
            }

        }
        
    
        [HttpGet]
        public IHttpActionResult GetUserById(short id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            using (var db = new EntityContext())
            {
                var user = db.ArtUsers.Find(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
               
        }

      

        [HttpPost]
        public IHttpActionResult PostUser([FromBody] ArtUser user)
        {
            using (var db = new EntityContext()) {
                db.ArtUsers.Add(user);
                db.SaveChanges();
                return Ok(user);
            }

           
        }

       

        [HttpDelete]
        public IHttpActionResult DeleteUserById(short id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            using (var db=new EntityContext()) {
                var user = db.ArtUsers.Find(id);
                if (user == null)
                {
                    return NotFound();
                }
                db.ArtUsers.Remove(user);
                db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Ok(user);
            }
               
        }


        [HttpPut]
        public IHttpActionResult PutUser(ArtUser user)
        {
            using (var db = new EntityContext())
            {
                var u = db.ArtUsers.Find(user.id);
                if (u != null)
                {
                    u.name = user.name;
                    u.email = user.email;
                    u.lastname = user.lastname;
                    u.firstname = user.firstname;
                    u.gender = user.gender;
                    u.passwd = user.passwd;
                    u.role_id = user.role_id;
                    db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Ok(u);
            }

        }


        [HttpGet]
        public IHttpActionResult GetSession([FromBody] string name,[FromBody] string password) {

            return Ok(name);
        }


      
        }
}
