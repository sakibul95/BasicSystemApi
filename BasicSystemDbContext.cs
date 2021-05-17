using BasicSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicSystem
{
    public class BasicSystemDbContext : DbContext
    {
        public BasicSystemDbContext(DbContextOptions<BasicSystemDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        public virtual DbSet<TblItem> TblItem { get; set; }
        public virtual DbSet<TblUserAccess> TblUserAccess { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
    }
}
