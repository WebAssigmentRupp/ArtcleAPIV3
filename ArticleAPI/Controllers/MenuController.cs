using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArticleAPI.Controllers
{
    public class MenuController : ApiController
    {
        private EntityContext db = new EntityContext();
        [HttpPost]
        public IHttpActionResult Post(menu menu) {
            var m = db.menus.Add(menu);
            db.Entry(m).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return Ok(m); 
        }

        [HttpGet]
        public IHttpActionResult Get() {
            var menus = db.menus.ToList<menu>();
            if (menus == null) {
                return NotFound();
            }
            return Ok(menus);
        }

        [HttpGet]
        public IHttpActionResult Get(int id) {
            if (id <= 0) {
                return BadRequest();
            }
            var menu = db.menus.Find(id);
            if (menu == null) {
                return NotFound();
            }
            return Ok(menu);
        }

        [HttpPut]
        public IHttpActionResult Put(menu menu) {
            var m = db.menus.Find(menu.id);
            if (m == null) {
                return NotFound();
            }
            m.title = menu.title;
            m.parent_id = menu.parent_id;
            db.Entry(m).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok(m);

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id) {
            if (id <=0) {
                return BadRequest();
            }
            var menu = db.menus.Find(id);
            if (menu == null) {
                return NotFound();
            }
            db.menus.Remove(menu);
            db.Entry(menu).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok("Menu has been deleted!");

        }
    }
}
