using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using StatisticSPA.Models;

namespace StatisticSPA.Controllers
{
    public class GroupsController : ApiController
    {
        private StatisticContext db = new StatisticContext();

        // GET api/Group
        public IEnumerable<Group> GetGroup()
        {
          //return db.Group;
          IEnumerable<Group> groups = GetGroups();
          IList<Group> distinct = new List<Group>();

          foreach (var group in groups)
          {
            foreach (var client in group.Client)
            {
              if (client.Group.Count == 1 && client.Group.FirstOrDefault().Id == group.Id)
              {
                distinct.Add(group);
              }
            }
          }

          return distinct;
        }

      //[HttpGet]
      //public IEnumerable<Group> GetUnique()
      //{
      //  var groups = GetGroups();
      //  var distinct = groups.DistinctBy(x => x.Client);
      //  return distinct;
      //} 

      private static List<Group> GetGroups()
      {
        var first = new Client { Id = 1 };
        var second = new Client { Id = 2 };
        var third = new Client { Id = 3 };


        return new List<Group>
        {
          new Group
          {
            Color = "#7cb5ec",
            Title = "Group1",
            Client = new List<Client> {first, second, third, new Client()}
          },
          new Group
          {
            Color = "#f45b5b",
            Title = "Group2",
            Client = new List<Client> {first, second}
          },
          new Group
          {
            Color = "#91e8e1",
            Title = "Group3",
            Client = new List<Client> {first, second, third, new Client(), new Client()}
          }
        };
      }

      // GET api/Group/5
        [ResponseType(typeof(Group))]
        public IHttpActionResult GetGroup(int id)
        {
            Group group = db.Group.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        // PUT api/Group/5
        public IHttpActionResult PutGroup(int id, Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.Id)
            {
                return BadRequest();
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Group
        [ResponseType(typeof(Group))]
        public IHttpActionResult PostGroup(Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Group.Add(group);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = group.Id }, group);
        }

        // DELETE api/Group/5
        [ResponseType(typeof(Group))]
        public IHttpActionResult DeleteGroup(int id)
        {
            Group group = db.Group.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            db.Group.Remove(group);
            db.SaveChanges();

            return Ok(group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(int id)
        {
            return db.Group.Count(e => e.Id == id) > 0;
        }
    }
}