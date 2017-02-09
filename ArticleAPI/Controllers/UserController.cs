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

        private EntityContext db = new EntityContext();


        [HttpGet]
        public IHttpActionResult Get() {
            var users=db.ArtUsers.ToList<ArtUser>();
            return Ok(users);

        }

        [HttpGet]
        public IHttpActionResult Get(short id) {
            if (id < 0) {
                return BadRequest();
            }
            var user = db.ArtUsers.Find(id);
            if (user == null) {
                return NotFound();
            }
            return Ok(user);
           
        }
        [HttpPost]
        public IHttpActionResult Post(ArtUser user) {
            var obj=db.ArtUsers.Add(user);
            db.Entry(obj).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return Ok(obj);   
        }

        [HttpDelete]
        public IHttpActionResult Delete(short id) {
            if (id <= 0) {
                return BadRequest();
            }
            var user = db.ArtUsers.Find(id);
            if (user == null) {
                return NotFound();
            }
            db.ArtUsers.Remove(user);
            db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok(user);
            
        }

        [HttpPut]
        public IHttpActionResult Put(ArtUser user) {
            var u = db.ArtUsers.Find(user.id);
            if (u != null)
            {
                u.name = user.name;
                u.email = user.email;
                u.lastname = u.lastname;
                u.firstname = user.firstname;
                u.gender = user.gender;
                u.passwd = user.passwd;
                db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            
            }
            return Ok(u);



        }
    }
}
