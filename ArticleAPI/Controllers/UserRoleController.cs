using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArticleAPI.Controllers
{
    
    public class UserRoleController : ApiController
    {
  
        [HttpGet]
        public IHttpActionResult Get() {
            using (var db = new EntityContext()) {
                var user = db.UserRoles.ToList<UserRole>();
                return Json(user);
            }
              
        }
        [HttpGet]
        public IHttpActionResult Get(int id) {
            if (id <= 0) {
                return BadRequest();
            }
            using (var db=new EntityContext()) {
                var userrole = db.UserRoles.Find(id);
                if (userrole == null)
                {
                    return NotFound();
                }

                return Ok(userrole);
            }
              
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody] UserRole userole) {
            using (var db = new EntityContext()) {
                db.UserRoles.Add(userole);
                db.SaveChanges();
                return Ok(userole);
            }
               
          
        }

        [HttpDelete]
        public IHttpActionResult Delete(short id) {
            if (id <= 0) {
                return BadRequest();
            }
            using (var db = new EntityContext()) {
                var userrol = db.UserRoles.Find(id);
                if (userrol == null)
                {
                    return NotFound();
                }
                db.UserRoles.Remove(userrol);
                db.Entry(userrol).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Ok("Record has been deleted!");
            }
           
        }

        [HttpPut]
        public IHttpActionResult Put(UserRole userrole) {
            using (var db = new EntityContext()) {
                var useroleupdate = db.UserRoles.Find(userrole.id);
                if (useroleupdate != null)
                {
                    useroleupdate.name = userrole.name;
                    useroleupdate.description = userrole.description;
                    db.SaveChanges();
                }
                return Ok(useroleupdate);
            }
           
        }


    }
}
