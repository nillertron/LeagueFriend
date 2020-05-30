using EFLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFLibrary.DataAcces
{
    public class DbCon : DbContext, IDbCon
    {
        public DbCon() : base()
        {
            OnConfiguring(new DbContextOptionsBuilder());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder db)
        {
            db.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LeagueFriend;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            db.EnableSensitiveDataLogging();
        }
        public DbSet<Player> Player { get; set; }
        public DbSet<Match> Match { get; set; }
        public DbSet<Champion> Champion { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Participant> Participant {get;set;}
        public DbSet<Stats> Stats { get; set; }
        public DbSet<TimeLine> TimeLine { get; set; }

    }
}
