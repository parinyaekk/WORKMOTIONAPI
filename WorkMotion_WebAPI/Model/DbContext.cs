using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static WorkMotion_WebAPI.Model.BannerModel;

namespace WorkMotion_WebAPI.BaseModel
{
    public class ASCCContext : DbContext
    {
        public ASCCContext(DbContextOptions<ASCCContext> options) : base(options) { }
        public DbSet<BANNER> BANNER { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Store>()
            //    //.HasAlternateKey(x => new { x.ID, x.Lang_ID, x.FK_Province_ID});
            //    .HasIndex(x => new { x.ID, x.Lang_ID, x.FK_Province_ID});
        }
    }
}