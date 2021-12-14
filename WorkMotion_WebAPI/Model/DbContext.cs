using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static WorkMotion_WebAPI.Model.BannerModel;
using static WorkMotion_WebAPI.Model.MenuModel;
using static WorkMotion_WebAPI.Model.PortfolioModel;
using static WorkMotion_WebAPI.Model.IndustriesModel;
using static WorkMotion_WebAPI.Model.CategoriesModel;
using static WorkMotion_WebAPI.Model.NewsModel;
using static WorkMotion_WebAPI.Model.NewsFileModel;

namespace WorkMotion_WebAPI.BaseModel
{
    public class ASCCContext : DbContext
    {
        public ASCCContext(DbContextOptions<ASCCContext> options) : base(options) { }
        public DbSet<BANNER> BANNER { get; set; }
        public DbSet<MENU> MENU { get; set; }
        public DbSet<PORTFOLIO> PORTFOLIO { get; set; }
        public DbSet<INDUSTRIES> INDUSTRIES { get; set; }
        public DbSet<CATEGORIES> CATEGORIES { get; set; }
        public DbSet<NEWS> NEWS { get; set; }
        public DbSet<NEWSFILE> NEWSFILE { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Store>()
            //    //.HasAlternateKey(x => new { x.ID, x.Lang_ID, x.FK_Province_ID});
            //    .HasIndex(x => new { x.ID, x.Lang_ID, x.FK_Province_ID});
        }
    }
}