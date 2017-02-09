using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArticleAPI.Controllers
{
    public class PageController : ApiController
    {
        private EntityContext db = new EntityContext();
        [HttpPost]
        public IHttpActionResult Post(page page) {
            var p = db.pages.Add(page);
            db.Entry(p).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return Ok(p);
        }

        [HttpGet]
        public IHttpActionResult Get() {
            var pages = db.pages.ToList<page>();
            if (pages == null) {
                return NotFound();
            }
            return Ok(pages);
        }

        [HttpGet]
        public IHttpActionResult Get(int id) {
            if (id <= 0) {
                return BadRequest();
            }
            var page = db.pages.Find(id);
            if (page == null) {
                return NotFound();
            }
            return Ok(page);
        }

        [HttpPut]
        public IHttpActionResult Put(page page) {
            var p = db.pages.Find(page.id);
            if (p == null) {
                return NotFound();
            }
            p.url = page.url;
            p.title = page.title;
            p.contents = page.contents;
            p.user_id = page.user_id;
            p.created_date = page.created_date;
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok(p);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id) {
            if (id <= 0) {
                return BadRequest();
            }
            var page = db.pages.Find(id);
            if (page == null) {
                return NotFound();
            }

            db.pages.Remove(page);
            db.Entry(page).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Ok("Page has been deleted!");
        }

    }
}
