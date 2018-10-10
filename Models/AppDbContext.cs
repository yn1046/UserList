using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using UserList.Models.DatabaseModels;

namespace UserList.Models
{
    public class AppDbContext : DbContext
    {
        private const string ConnectionString =
            @"Host=localhost;
            Port=5432;
            Database=UserDB;
            Username=sky;
            Password=password";

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }

        public DbSet<User> Users { get; set; }
    }
}
