using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static WorkMotion_WebAPI.Model.BannerModel;
using static WorkMotion_WebAPI.Model.MenuModel;
using static WorkMotion_WebAPI.Model.PortfolioModel;
using static WorkMotion_WebAPI.Model.IndustriesModel;
using static WorkMotion_WebAPI.Model.Categories_OptionModel;
using static WorkMotion_WebAPI.Model.NewsModel;
using static WorkMotion_WebAPI.Model.NewsFileModel;
using static WorkMotion_WebAPI.Model.HDYH_OptionModel;
using static WorkMotion_WebAPI.Model.Startup_OptionModel;
using static WorkMotion_WebAPI.Model.TeamModel;
using static WorkMotion_WebAPI.Model.ContactUsModel;
using static WorkMotion_WebAPI.Model.LogModel;
using static WorkMotion_WebAPI.Model.InformationModel;
using static WorkMotion_WebAPI.Model.InformationFileModel;

namespace WorkMotion_WebAPI.BaseModel
{
    public class ASCCContext : DbContext
    {
        public ASCCContext(DbContextOptions<ASCCContext> options) : base(options) { }
        public DbSet<BANNER> BANNER { get; set; }
        public DbSet<MENU> MENU { get; set; }
        public DbSet<PORTFOLIO> PORTFOLIO { get; set; }
        public DbSet<INDUSTRIES> INDUSTRIES { get; set; }
        public DbSet<CATEGORIES_OPTION> CATEGORIES_OPTION { get; set; }
        public DbSet<NEWS> NEWS { get; set; }
        public DbSet<NEWSFILE> NEWSFILE { get; set; }
        public DbSet<HDYH_OPTION> HDYH_OPTION { get; set; }
        public DbSet<STARTUP_OPTION> STARTUP_OPTION { get; set; }
        public DbSet<TEAM> TEAM { get; set; }
        public DbSet<CONTACT_US> CONTACT_US { get; set; }
        public DbSet<LOG> LOG { get; set; }
        public DbSet<INFORMATION> INFORMATION { get; set; }
        public DbSet<INFORMATIONFILE> INFORMATIONFILE { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Store>()
            //    //.HasAlternateKey(x => new { x.ID, x.Lang_ID, x.FK_Province_ID});
            //    .HasIndex(x => new { x.ID, x.Lang_ID, x.FK_Province_ID});
        }
    }
}