using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using StatisticSPA.Models;

namespace StatisticSPA.Controllers
{
    public class GroupsController : ApiController
    {
        private readonly StatisticContext _db = new StatisticContext();

        // GET api/Group
        public IEnumerable<Group> GetGroup()
        {
          return _db.Group;
          //IEnumerable<Group> groups = GetGroups();
          //IList<Group> distinct = new List<Group>();

          //foreach (var group in groups)
          //{
          //  foreach (var client in group.Client)
          //  {
          //    if (client.Group.Count == 1 && client.Group.FirstOrDefault().Id == group.Id)
          //    {
          //      distinct.Add(group);
          //    }
          //  }
          //}

          //return distinct;
        }

      /// <summary>
      /// Non db example
      /// </summary>
      /// <returns></returns>
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
      public Group GetGroup(int id)
      {
        Group group = _db.Group.Find(id);
        if (group == null)
        {
          throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        return group;
      }

      // PUT api/Group/5
      public HttpResponseMessage PutGroup(int id, Group group)
      {
        if (!ModelState.IsValid)
        {
          return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        if (id != group.Id)
        {
          return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        _db.Entry(group).State = EntityState.Modified;

        try
        {
          _db.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        }

        return Request.CreateResponse(HttpStatusCode.OK);
      }

      // POST api/Group
      public HttpResponseMessage PostGroup(Group group)
      {
        if (ModelState.IsValid)
        {
          _db.Group.Add(group);
          _db.SaveChanges();

          HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, group);
          response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = group.Id }));
          return response;
        }
        else
        {
          return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
      }

      // DELETE api/Group/5
      public HttpResponseMessage DeleteGroup(int id)
      {
        Group group = _db.Group.Find(id);
        if (group == null)
        {
          return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        _db.Group.Remove(group);

        try
        {
          _db.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        }

        return Request.CreateResponse(HttpStatusCode.OK, group);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          _db.Dispose();
        }

        base.Dispose(disposing);
      }

        private bool GroupExists(int id)
        {
            return _db.Group.Count(e => e.Id == id) > 0;
        }
    }
}