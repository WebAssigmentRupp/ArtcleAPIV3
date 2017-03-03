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
        internal class mappingData{

            public short id { get; set; }
            public string url { get; set; }
            public string title { get; set; }
            public string contents { get; set; }
            public short user_id { get; set; }
            public string user_name { get; set;}
            public DateTime created_date { get; set; }
        }
      
        [HttpPost]
        public IHttpActionResult Post(page page) {
            using (var db=new EntityContext()) {
                var p = db.pages.Add(page);
                db.Entry(p).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return Ok(p);
            }
            
        }

        [HttpGet]
        public IHttpActionResult Get() {
            using (var db=new EntityContext()) {

                string sql = @"SELECT p.id,p.url,p.title,p.contents,u.id As user_id,u.name As user_name 
                              FROM page p INNER JOIN ArtUser u ON p.user_id=u.id";
                var pages= db.Database.SqlQuery<mappingData>(sql).ToList();
                return Ok(pages);
            }
            
        }

        [HttpGet]
        public IHttpActionResult Get(int id) {
            if (id <= 0) {
                return BadRequest();
            }
            using (var db=new EntityContext()) {

                try {
                    string sql = @"SELECT p.id,p.url,p.title,p.contents,u.id As user_id,u.name As user_name 
                              FROM page p INNER JOIN ArtUser u ON p.user_id=u.id WHERE p.id=" + id;
                    var page = db.Database.SqlQuery<mappingData>(sql).Single();
                    return Ok(page);
                }
                catch (Exception ex) {
                    return NotFound();
                }
                

           
            }
               
        }

        [HttpPut]
        public IHttpActionResult Put(page page) {
            using (var db=new EntityContext()) {
                var p = db.pages.Find(page.id);
                if (p == null)
                {
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
              
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id) {
            if (id <= 0) {
                return BadRequest();
            }
            using (var db =new EntityContext()) {
                var page = db.pages.Find(id);
                if (page == null)
                {
                    return NotFound();
                }

                db.pages.Remove(page);
                db.Entry(page).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

                return Ok("Page has been deleted!");
            }
               
        }

    }
}
