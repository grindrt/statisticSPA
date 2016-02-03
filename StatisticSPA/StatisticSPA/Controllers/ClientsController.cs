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
  public class ClientsController : ApiController
  {
    readonly StatisticContext _db = new StatisticContext();

    // GET api/Client
    public IEnumerable<Client> GetClients()
    {
      return _db.Client;
    }

    // GET api/Clients/5
    public Client GetClient(int id)
    {
      var client = _db.Client.Find(id);
      if (client == null)
      {
        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
      }

      return client;
    }

    // PUT api/Clients
    public HttpResponseMessage PutClient(Client client)
    {
      if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
      using (var context = new StatisticContext())
      {
        var addedGroups = client.Groups.Select(x => x.Id).ToList();
        client.Groups.Clear();

        foreach (var @group in
          from @group in context.Group
          from added in addedGroups.Where(added => @group.Id == added)
          select @group)
        {
          client.Groups.Add(@group);
        }

        try
        {
          context.Client.Add(client);
          context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        }
      }

      HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, client);
      response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = client.Id }));
      return response;
    }

    // POST api/Clients/5
    public HttpResponseMessage PostClient(Client client)
    {
      if (!ModelState.IsValid)
      {
        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
      }

      if (client == null || client.Id <= 0)
      {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }

      var addedGroups = client.Groups.Select(x => x.Id).ToList();
      client.Groups.Clear();

      var id = client.Id;
      using (var context = new StatisticContext())
      {
        var dbClient = context.Client.Include(x => x.Groups).FirstOrDefault(x => x.Id == id && x.Groups.Any());
        if (dbClient != null)
        {
          dbClient.Groups.Clear();
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
        var dbClient = context.Client.Include(x => x.Groups).FirstOrDefault(x => x.Id == client.Id);
        if (dbClient == null)
        {
          return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        foreach (var @group in
          from @group in context.Group
          from added in addedGroups.Where(added => @group.Id == added)
          select @group)
        {
          dbClient.Groups.Add(@group);
        }

        dbClient.FirstName = client.FirstName;
        dbClient.LastName = client.LastName;
        dbClient.Email = client.Email;
        dbClient.BirthDate = client.BirthDate;
        dbClient.City = client.City;
        dbClient.Gender = client.Gender;

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

    // DELETE api/Clients/5
    public HttpResponseMessage DeleteClient(int id)
    {
      using (var context = new StatisticContext())
      {
        Client client = context.Client.Find(id);
        if (client == null)
        {
          return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        context.Client.Remove(client);

        try
        {
          context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        }

        return Request.CreateResponse(HttpStatusCode.OK, client);
      }
    }
  }
}