using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using StatisticSPA.Models;

namespace StatisticSPA.DAL
{
  public class StatisticInitializer : DropCreateDatabaseIfModelChanges<StatisticContext>
  {
    protected override void Seed(StatisticContext context)
    {
      var groups = new List<Group>
      {
        new Group
        {
          Title = "Tax Accountant",
          Color = "#e2b92c"
        },
        new Group
        {
          Title = "Software Consultant",
          Color = "#4f5c94"
        },
        new Group
        {
          Title = "Software Test Engineer III",
          Color = "#47c353"
        },
        new Group
        {
          Title = "Physical Therapy Assistant",
          Color = "#58e30b"
        },
        new Group
        {
          Title = "Chemical Engineer",
          Color = "#3645a4"
        }
      };

      groups.ForEach(x => context.Group.Add(x));
      context.SaveChanges();

      var clients = new List<Client>
      {
        new Client
        {
          FirstName = "Donald",
          LastName = "Murray",
          BirthDate = DateTime.Parse("1990-02-28"),
          Email = "dmurray0@bloglines.com",
          City = "Platagata",
          Gender = "Male",
          Groups = new List<Group>
          {
            context.Group.FirstOrDefault()
          }
        },new Client
        {
          FirstName = "Brenda",
          LastName = "Hall",
          BirthDate = DateTime.Parse("1997-05-06"),
          Email = "bhall1@studiopress.com",
          City = "Songwon",
          Gender = "Female",
          Groups = new List<Group>
          {
            context.Group.FirstOrDefault(x=>x.Id==2)
          }
        },new Client
        {
          FirstName = "Gregory",
          LastName = "Hayes",
          BirthDate = DateTime.Parse("1985-12-03"),
          Email = "ghayes2@bbc.co.uk",
          City = "Platagata",
          Gender = "Male",
          Groups = new List<Group>
          {
            context.Group.FirstOrDefault(x=>x.Id==5),
            context.Group.FirstOrDefault(x=>x.Id==1),
            context.Group.FirstOrDefault(x=>x.Id==3),
            context.Group.FirstOrDefault(x=>x.Id==2)
          }
        },new Client
        {
          FirstName = "Shirley",
          LastName = "Hawkins",
          BirthDate = DateTime.Parse("1984-01-07"),
          Email = "shawkins1@ustream.tv",
          City = "Yarumela",
          Gender = "Female",
          Groups = new List<Group>
          {
            context.Group.FirstOrDefault(x=>x.Id==4)
          }
        }
      };
      clients.ForEach(x => context.Client.Add(x));
      context.SaveChanges();
    }
  }
}