﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StatisticSPA.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class StatisticContext : DbContext
    {
        public StatisticContext()
            : base("name=StatisticContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // throw new UnintentionalCodeFirstException();
          modelBuilder.Entity<Client>().HasMany(x => x.Group).WithMany(x => x.Client);
        }
    
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Group> Group { get; set; }
    
        public virtual ObjectResult<Nullable<int>> GetClientsCountPerGroup()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetClientsCountPerGroup");
        }
    }
}