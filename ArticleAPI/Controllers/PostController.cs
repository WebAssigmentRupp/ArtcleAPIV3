using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
namespace ArticleAPI.Controllers
{
    public class PostController : ApiController
    {
        internal class mappingData {
            public short id { get; set; }
            public string title { get; set; }
            public string texts { get; set; }

            public string image { get; set; }
 
            public DateTime post_date { get; set; }
            public string author { get; set; }
            public short cat_id { get; set; }
            public string category_name { get; set; }
            public String username { get; set; }


        }


        [HttpPost]
        public IHttpActionResult PostArticle(post post) {
            post.post_date = DateTime.Now;
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
              
              string sql= @"SELECT a.id,
                                  a.title,
                                  a.texts,
                                  a.image,
                                  a.post_date,
                                  a.category_id As cat_id,
                                  b.name As category_name,
                                  c.name As username,
                                  a.author 
                                  FROM post a 
                                       INNER JOIN category b ON a.category_id = b.id 
                                       INNER JOIN ArtUser c ON a.user_id=c.id ORDER By a.post_date DESC";

               var posts= db.Database.SqlQuery<mappingData>(sql).ToList();

                return Ok(posts);
            }
               
        }

        [HttpGet]
        public IHttpActionResult Get(int id) {
            
            if (id <= 0) {
                return BadRequest();
            }
            using (var db = new EntityContext()) {
                string sql = @"SELECT a.id,
                                  a.title,
                                  a.texts,
                                  a.image,
                                  a.post_date,
                                  a.category_id As cat_id,
                                  b.name As category_name,
                                  c.name As username,
                                  a.author 
                                  FROM post a 
                                       INNER JOIN category b ON a.category_id = b.id 
                                       INNER JOIN ArtUser c ON a.user_id=c.id WHERE a.id=" + id;

                var posts = db.Database.SqlQuery<mappingData>(sql).Single();
                if (posts == null)
                {
                    return NotFound();
                }
                return Ok(posts);
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
                p.category_id = post.category_id;
                p.user_id = post.user_id;
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
      
                return Ok("Post has been deleted!");
            }

        }

        [HttpGet]
        public IHttpActionResult GetPostByCatId(int id) {
            if (id <= 0)
            {
                return BadRequest();
            }
            using (var db = new EntityContext())
            {
                string sql = @"SELECT a.id,
                                  a.title,
                                  a.texts,
                                  a.image,
                                  a.post_date,
                                  a.category_id As cat_id,
                                  b.name As category_name,
                                  c.name As username,
                                  a.author 
                                  FROM post a 
                                       INNER JOIN category b ON a.category_id = b.id 
                                       INNER JOIN ArtUser c ON a.user_id=c.id WHERE b.id=" + id;

                var posts = db.Database.SqlQuery<mappingData>(sql).ToList();
                if (posts == null)
                {
                    return NotFound();
                }
                return Ok(posts);
            }
        }

        [HttpGet]
        public IHttpActionResult getOldPosts() {
            using (var db = new EntityContext())
            {

                string sql = @"SELECT TOP 5
                                  a.id,
                                  a.title,
                                  a.texts,
                                  a.image,
                                  a.post_date,
                                  a.category_id As cat_id,
                                  b.name As category_name,
                                  c.name As username,
                                  a.author 
                                  FROM post a 
                                       INNER JOIN category b ON a.category_id = b.id 
                                       INNER JOIN ArtUser c ON a.user_id=c.id WHERE
                                       a.post_date >= dateadd(d, datediff(d, 0, getdate())-1, 0) and
                                       a.post_date < dateadd(d, datediff(d, 0, getdate()), 0)
                                       ORDER BY a.post_date DESC";

                var posts = db.Database.SqlQuery<mappingData>(sql).ToList();

                return Ok(posts);
            }

        }
    }
}
