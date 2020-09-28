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

    }
}
