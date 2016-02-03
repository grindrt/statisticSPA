using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StatisticSPA.DAL;
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
        }

        // GET api/Groups/5
        public Group GetGroup(int id)
        {
          var group = _db.Group.Find(id);
          if (group == null)
          {
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
          }

          return group;
        }

        // PUT api/Groups
        public HttpResponseMessage PutGroup(Group group)
        {
          if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
          using (var context = new StatisticContext())
          {
            var addedClients = group.Clients.Select(x => x.Id).ToList();
            group.Clients.Clear();

            foreach (var @client in
              from @client in context.Client
              from added in addedClients.Where(added => @client.Id == added)
              select @client)
            {
              group.Clients.Add(@client);
            }

            try
            {
              context.Group.Add(group);
              context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
              return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
          }

          HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, group);
          response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = group.Id }));
          return response;
        }

        // POST api/Groups/5
        public HttpResponseMessage PostGroup(Group group)
        {
          if (!ModelState.IsValid)
          {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
          }

          if (group == null || group.Id <= 0)
          {
            return Request.CreateResponse(HttpStatusCode.BadRequest);
          }

          var addedClients = group.Clients.Select(x => x.Id).ToList();
          group.Clients.Clear();

          var id = group.Id;
          using (var context = new StatisticContext())
          {
            var dbGroup = context.Group.Include(x => x.Clients).FirstOrDefault(x => x.Id == id && x.Clients.Any());
            if (dbGroup != null)
            {
              dbGroup.Clients.Clear();
              try
              {
                context.SaveChanges();
              }
              catch (DbUpdateConcurrencyException ex)
              {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
              }
            }
          }

          using (var context = new StatisticContext())
          {
            var dbGroup = context.Group.Include(x => x.Clients).FirstOrDefault(x => x.Id == group.Id);
            if (dbGroup == null)
            {
              return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            foreach (var @client in
              from @client in context.Client
              from added in addedClients.Where(added => @client.Id == added)
              select @client)
            {
              dbGroup.Clients.Add(@client);
            }

            dbGroup.Title = group.Title;
            dbGroup.Color = group.Color;

            try
            {
              context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
              return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
          }

          return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/Groups/5
        public HttpResponseMessage DeleteGroup(int id)
        {
          using (var context = new StatisticContext())
          {
            Group group = context.Group.Find(id);
            if (group == null)
            {
              return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            context.Group.Remove(group);

            try
            {
              context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
              return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, group);
          }
        }
    }
}