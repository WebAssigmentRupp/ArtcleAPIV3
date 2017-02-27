using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace ArticleAPI.Controllers
{
    public class UserController : ApiController
    {
        internal class MappingData
        {
            public short Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Gender { get; set; }

            public string Passwd { get; set; }
            public String RoleName { get; set; }
            public String RoleDescription { get; set; }
        }


        [HttpGet]
        public IHttpActionResult GetAllUsers() {

            using (var db = new EntityContext())
            {
                //var users = db.ArtUsers.ToList();
                //return Ok(users);

                String sqlQuery = @"SELECT A.id, 
                                           A.email, 
                                           A.firstname, 
                                           A.lastname, 
                                           A.name, 
                                           A.passwd,
                                           A.gender, 
                                           B.name AS RoleName,
                                           B.description As RoleDescription 
                                    FROM ArtUser A 
                                    INNER JOIN UserRole B ON A.role_id = B.id";
                var users = db.Database.SqlQuery<MappingData>(sqlQuery).ToList();

                return Ok(users);
            }


        }


    
        
        [HttpGet]
        public IHttpActionResult GetUserById(short id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            using (var db = new EntityContext())
            {
                String sqlQuery = @"SELECT A.id, 
                                           A.email, 
                                           A.firstname, 
                                           A.lastname, 
                                           A.name, 
                                           A.passwd,
                                           A.gender, 
                                           B.name AS RoleName,
                                           B.description As RoleDescription 
                                    FROM ArtUser A 
                                    INNER JOIN UserRole B ON A.role_id = B.id WHERE A.id="+id;
                var users = db.Database.SqlQuery<MappingData>(sqlQuery).ToList();
                if (users == null)
                {
                    return NotFound();
                }
                return Ok(users);
            }
               
        }

      

        [HttpPost]
        public IHttpActionResult PostUser([FromBody] ArtUser user)
        {
            using (var db = new EntityContext()) {
                db.ArtUsers.Add(user);
                db.SaveChanges();
                return Ok(user);
            }

           
        }

      
        [HttpDelete]
        public IHttpActionResult DeleteUserById(short id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            using (var db=new EntityContext()) {
                var user = db.ArtUsers.Find(id);
                if (user == null)
                {
                    return NotFound();
                }
                db.ArtUsers.Remove(user);
                db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Ok(user);
            }
               
        }


        [HttpPut]
        public IHttpActionResult PutUser(ArtUser user)
        {
            using (var db = new EntityContext())
            {
                var u = db.ArtUsers.Find(user.id);
                if (u != null)
                {
                    u.name = user.name;
                    u.email = user.email;
                    u.lastname = user.lastname;
                    u.firstname = user.firstname;
                    u.gender = user.gender;
                    u.passwd = user.passwd;
                    u.role_id = user.role_id;
                    db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Ok(u);
            }

        }


        [HttpGet]
        public IHttpActionResult GetSession(string name,string password) {

            System.Diagnostics.Debug.WriteLine(name + "|" + password);

            using (var db=new EntityContext()) {
                string sql = @"
                               SELECT a.id,a.name,b.name as RoleName 
                               FROM ArtUser a INNER JOIN UserRole b ON a.role_id=b.id WHERE a.name='" + name+"' and a.passwd='"+password+"'";
                try
                {
                    var userSession = db.Database.SqlQuery<MappingData>(sql).Single();
                    return Ok(userSession);
                }
                catch(Exception ex)
                {
                    return NotFound();
                }
                
            }

               
        }


      
        }
}
