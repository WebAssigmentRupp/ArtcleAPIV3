using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace ArticleAPI.Controllers
{
    public class MenuController : ApiController
    {
        internal class MenuMap
        {
            public short id { get; set; }

            public string title { get; set; }

            public short? parent_id { get; set; }

            public short user_id { get; set; }

            public short page_id { get; set; }
            public string parent_name { get; set; }

            public string user { get; set; }

            public string page { get; set; }
        }
       
        [HttpPost]
        public IHttpActionResult Post(menu menu) {
            using (var db=new EntityContext()) {

                var m = db.menus.Add(menu);
                db.Entry(m).State = System.Data.Entity.EntityState.Added;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Ok("Failed to saved");
                    throw;
                }
                
                return Ok(m);
            }

        }
        private void addSub(EntityContext context, menu parent)
        {
            var ms = context.menus.Where(sm => sm.parent_id == parent.id).ToList();
            if (ms == null)
            {
                return;
            }
            foreach (var sm in ms)
            {
                parent.submenu.Add(sm);
            }
            foreach (var m in parent.submenu)
            {
                m.page = getPage(m.page_id);
                addSub(context, m);
            }
        }
        private page getPage(int page_id)
        {
            try
            {
                string sql = @"SELECT * FROM page WHERE id = " + page_id;
                using (var db = new EntityContext())
                {
                    var p = db.Database.SqlQuery<page>(sql).Single();
                    return p;
                }
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
           
        }
        [HttpGet]
        public IHttpActionResult Get() {
            using (var db = new EntityContext()) {
                var menus = db.menus.Where(m => m.parent_id == null).ToList();
                if (menus == null)
                {
                    return NotFound();
                }
                foreach (var m in menus)
                {
                    m.page = getPage(m.page_id);
                    addSub(db, m);
                }
                return Ok(menus);
            }
            
        }
        public IHttpActionResult GetList()
        {
            using (var db = new EntityContext())
            {
                String sqlQuery = @"SELECT M.id, 
                                    M.title,  
                                    P.title As page,
                                    M.page_id,
                                    U.name As [user],
                                    M.parent_id,
                                    MP.title as parent_name
                                    FROM menu M INNER JOIN ArtUser U ON M.user_id = U.id
                                    LEFT JOIN page P ON M.page_id = P.id
                                    LEFT JOIN menu MP ON M.parent_id = MP.id
                                     ";
                var menus = db.Database.SqlQuery<MenuMap>(sqlQuery).ToList();
                return Ok(menus);
            }
            
        }

        [HttpGet]
        public IHttpActionResult Get(int id) {
            if (id <= 0) {
                return BadRequest();
            }
            using (var db = new EntityContext()) {
                var menu = db.menus.Find(id);
                if (menu == null)
                {
                    return NotFound();
                }
                return Ok(menu);
            }
            
        }

        [HttpPut]
        public IHttpActionResult Put(menu menu) {
            using (var db = new EntityContext()) {
                var m = db.menus.Find(menu.id);
                if (m == null)
                {
                    return NotFound();
                }
                try
                {
                    m.title = menu.title;
                    m.parent_id = menu.parent_id;
                    db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    return Ok("Failed");
                }
                
                return Ok(m);
            }
           

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id) {
            if (id <=0) {
                return BadRequest();
            }
            using (var db = new EntityContext()) {
                var menu = db.menus.Find(id);
                if (menu == null)
                {
                    return NotFound();
                }
                try
                {
                    db.menus.Remove(menu);
                    db.Entry(menu).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Ok("Failed!");
                    throw;
                }
                
                return Ok("Menu has been deleted!");
            }
            

        }
    }
}
