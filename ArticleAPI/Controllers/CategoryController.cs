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
        
       
        [HttpPost]
        public IHttpActionResult PostCategory(category category) {
            using (var db= new EntityContext()) {
                var cat = db.categories.Add(category);
                db.Entry(cat).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return Ok(cat);
            }
              
        }

        [HttpGet]
        public IHttpActionResult GetAllCategories() {
            using (var db = new EntityContext()) {
                var cat = db.categories.ToList<category>();
                return Ok(cat);
            }
              
        }
        [HttpGet]
        public IHttpActionResult GetCategoryById(int id) {
            if (id <= 0) {
                return BadRequest();
            }
            using (var db = new EntityContext()) {
                var cat = db.categories.Find(id);
                if (cat == null)
                {
                    return NotFound();
                }
                return Ok(cat);
            }
               
            
        }

        [HttpPut]
        public IHttpActionResult Put(category category)
        {
            using (var db = new EntityContext()) {
                var cat = db.categories.Find(category.id);
                if (cat == null)
                {
                    return NotFound();
                }
                cat.name = category.name;
                cat.description = category.description;
                db.Entry(cat).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok(cat);
            }
                
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategoryByID(int id) {
            if (id <= 0)
            {
                return BadRequest();
            }
            using (var db=new EntityContext()) {
                var cat = db.categories.Find(id);
                if (cat == null)
                {
                    return NotFound();
                }
                try
                {
                    db.categories.Remove(cat);
                    db.Entry(cat).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return Ok("Category has been deleted!");
                }
                catch (Exception ex) {
                    return Json(new { DATA=0});
                }
               
            }
               
        }
    }
}
