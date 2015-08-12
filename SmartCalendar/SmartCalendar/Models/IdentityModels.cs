﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SmartCalendar.Models.Abstracts;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartCalendar.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual List<Event> Events { get; set; }

    }

    public class Event
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Description { get; set; }

        [JsonIgnore]
        public DateTime DateAdd { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public DateTime DateStart { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public DateTime DateEnd { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "category")]
        public Category Category { get; set; }

        [JsonIgnore]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [JsonIgnore]
        public ApplicationUser User { get; set; }
    }

    public enum Category { Home = 1, Business, Study, Fun }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IStoreAppContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Event> Events { get; set; }

        public void MarkAsModified(Event item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}