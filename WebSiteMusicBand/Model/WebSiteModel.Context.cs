﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSiteMusicBand.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MusicBandSiteDB : DbContext
    {
        public MusicBandSiteDB()
            : base("name=MusicBandSiteDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CustomUsers> CustomUsers { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsComments> NewsComments { get; set; }
        public virtual DbSet<NewsLikes> NewsLikes { get; set; }
        public virtual DbSet<NewsSection> NewsSection { get; set; }
        public virtual DbSet<NewsAdditionalInfo> NewsAdditionalInfo { get; set; }

        public System.Data.Entity.DbSet<WebSiteMusicBand.Model.Track> Tracks { get; set; }

        public System.Data.Entity.DbSet<WebSiteMusicBand.Model.Album> Albums { get; set; }
    }
}
