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
        public IHttpActionResult GetAllUser() {

            using (var db = new EntityContext()) {
                var users = db.ArtUsers.ToList<ArtUser>();
                return Ok(users);
            }

        }

        [HttpGet]
        public IHttpActionResult Get(short id)
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
        public IHttpActionResult Delete(short id)
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
        public IHttpActionResult Put(ArtUser user)
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

        }
}
