using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArticleAPI.Controllers
{
    public class PostController : ApiController
    {
       

        [HttpPost]
        public IHttpActionResult PostArticle(post post) {
            using (var db=new EntityContext()) {
                var p = db.posts.Add(post);
                db.Entry(p).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return Ok(p);
            }
              
        }

        [HttpGet]
        public IHttpActionResult GetAllPosts() {
            using (var db = new EntityContext()) {
                var p = db.posts.ToList<post>();
                if (p == null)
                {
                    return NotFound();
                }
                return Ok(p);
            }
               
        }

        [HttpGet]
        public IHttpActionResult Get(int id) {
            
            if (id <= 0) {
                return BadRequest();
            }
            using (var db = new EntityContext()) {
                var post = db.posts.Find(id);
                if (post == null)
                {
                    return NotFound();
                }
                return Ok(post);
            }
              
        }

        [HttpPut]
        public IHttpActionResult Put(post post) {
            using (var db = new EntityContext()) {
                var p = db.posts.Find(post.id);
                if (p == null)
                {
                    return NotFound();
                }
                p.title = post.title;
                p.texts = post.texts;
                p.image = post.image;
                p.post_date = post.post_date;
                p.author = post.author;
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
            using (var db=new EntityContext()) {

                var post = db.posts.Find(id);
                if (post == null)
                {
                    return NotFound();
                }
                db.posts.Remove(post);
                db.Entry(post).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                db.SaveChanges();
                return Ok("Post has been deleted!");
            }

        }
    }
}
