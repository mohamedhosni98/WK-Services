using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WK_Services.Domain.Entities;

namespace WK_Services.Infrastructure.Presistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "AM", Description = "Air Mail service - sample description." },
                new Service { Id = 2, Name = "LL", Description = "Land Line service - sample description." },
                new Service { Id = 3, Name = "FB", Description = "Fast Box service - sample description." },
                new Service { Id = 4, Name = "Old LL", Description = "Legacy Land Line service - sample description." }
            );

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Name = "client1", Phone = "01000000001", Shipment_Address = "Address 1", Email = "info@client1.com", Country = "Egypt", City = "Cairo" },
                new Client { Id = 2, Name = "client2", Phone = "01000000002", Shipment_Address = "Address 2", Email = "info@client2.com", Country = "Egypt", City = "Giza" }
            );

            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, ClientId = 1, Name = "ahmed", Mobile = "01005500111", Email = "ahmed@client1.com", User_Name = "contact1_1", Password = Hash("pws123") },
                new Contact { Id = 2, ClientId = 2, Name = "sara", Mobile = "01005500222", Email = "sara@client2.com", User_Name = "contact2_1", Password = Hash("pws123") },
                new Contact { Id = 3, ClientId = 2, Name = "mona", Mobile = "01005500333", Email = "mona@client2.com", User_Name = "contact2_2", Password = Hash("pws123") }
            );
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }

        private static string Hash(string input)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}