﻿using Microsoft.EntityFrameworkCore;
using HueFesAPI.Data;
using HueFesAPI.Models;

namespace HueFesAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        public DbSet<HueFesAPI.Data.Account>? Account { get; set; } 
        public DbSet<HueFesAPI.Data.Role>? Role { get; set; }
        public DbSet<HueFesAPI.Data.LocationType>? LocationType { get; set; }
        public DbSet<HueFesAPI.Data.Location>? Location { get; set; }
        public DbSet<HueFesAPI.Data.TicketType>? TicketType { get; set; }
        public DbSet<HueFesAPI.Data.Ticket>? Ticket { get; set; }
        public DbSet<HueFesAPI.Data.EventType>? EventType { get; set; }
        public DbSet<HueFesAPI.Data.Event>? Event { get; set; }
        public DbSet<HueFesAPI.Data.Support>? Support { get; set; }
        public DbSet<HueFesAPI.Models.User>? User { get; set; }
    }
}
