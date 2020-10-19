using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AURA.Models;
using Microsoft.EntityFrameworkCore;

namespace AURA.Data
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options)
            : base(options)
        {

        }

        public DbSet<PostZero> PostZeros { get; set; }
        public DbSet<PostOne> PostOnes { get; set; }
        public DbSet<PostThr> PostThrs { get; set; }
        public DbSet<PostFou> PostFous { get; set; }
        public DbSet<PostFiv> PostFivs { get; set; }
        public DbSet<PostSix> PostSixs { get; set; }
        public DbSet<PostSev> PostSevs { get; set; }
        public DbSet<PostEig> PostEigs { get; set; }
        public DbSet<PostNin> PostNins { get; set; }
        
        //Accounting Tables
        public DbSet<AURA.Models.ProductList> ProductList { get; set; }
        public DbSet<AURA.Models.BookAccounts> BookAccounts { get; set; }
        public DbSet<AURA.Models.FractionList> FractionLists { get; set; }
        public DbSet<AURA.Models.Journal> Journals { get; set; }
        
        //Employee tables
        public DbSet<AURA.Models.Agents> Agents { get; set; }
        public DbSet<AURA.Models.AgentsAddress> AgentsAddresses { get; set; }
        public DbSet<AURA.Models.AgentsPhone> AgentsPhones { get; set; }
        public DbSet<AURA.Models.AgentsEmail> AgentsEmails { get; set; }
        public DbSet<AURA.Models.AgentsRoles> AgentsRoles { get; set; }
        public DbSet<AURA.Models.AgentsRemarks> AgentsRemarks { get; set; }
        public DbSet<AURA.Models.AgentsPayment> AgentsPayments { get; set; }

        //test
        //public DbSet<AURA.Models.Test> tests { get; set; }

    }
}
