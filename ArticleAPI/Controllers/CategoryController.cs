using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArticleAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private EntityContext db = new EntityContext();

        [HttpPost]
        public IHttpActionResult Post(category category) {
            var cat = db.categories.Add(category);
            db.Entry(cat).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return Ok(cat);
        }

        [HttpGet]
        public IHttpActionResult Get() {
            var cat = db.categories.ToList<category>();
            return Ok(cat);
        }
        [HttpGet]
        public IHttpActionResult Get(int id) {
            if (id <= 0) {
                return BadRequest();
            }
            var cat = db.categories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
            
        }

        [HttpPut]
        public IHttpActionResult Put(category category)
        {
            var cat = db.categories.Find(category.id);
            if (cat==null) {
                return NotFound();
            }
            cat.name = category.name;
            cat.description = category.description;
            db.Entry(cat).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok(cat);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id) {
            if (id <= 0)
            {
                return BadRequest();
            }
            var cat = db.categories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            db.categories.Remove(cat);
            db.SaveChanges();
            return Ok("Category has been deleted!");
        }
    }
}
