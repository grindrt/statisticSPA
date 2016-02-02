using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using StatisticSPA.Models;

namespace StatisticSPA.Controllers
{
  public class ClientsController : ApiController
  {
    private readonly StatisticContext _db = new StatisticContext();

    // GET api/Client
    public IEnumerable<Client> GetClients()
    {
      return _db.Client; //.Where(c => c.Group.Any(g => g.Id == c.Id));
    }

    // GET api/Client/5
    public Client GetClient(int id)
    {
      Client client = _db.Client.Find(id);
      if (client == null)
      {
        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
      }

      return client;
    }

    // PUT api/Client/5
    public HttpResponseMessage PutClient(int id, Client client)
    {
      if (!ModelState.IsValid)
      {
        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
      }

      if (id != client.Id)
      {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }

      _db.Entry(client).State = EntityState.Modified;

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

    // POST api/Client
    public HttpResponseMessage PostClient(Client client)
    {
      if (ModelState.IsValid)
      {
        _db.Client.Add(client);
        _db.SaveChanges();

        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, client);
        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = client.Id }));
        return response;
      }
      else
      {
        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
      }
    }

    // DELETE api/Client/5
    public HttpResponseMessage DeleteClient(int id)
    {
      Client client = _db.Client.Find(id);
      if (client == null)
      {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      _db.Client.Remove(client);

      try
      {
        _db.SaveChanges();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
      }

      return Request.CreateResponse(HttpStatusCode.OK, client);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        _db.Dispose();
      }

      base.Dispose(disposing);
    }

    private bool ClientExists(int id)
    {
      return _db.Client.Count(e => e.Id == id) > 0;
    }
  }
}