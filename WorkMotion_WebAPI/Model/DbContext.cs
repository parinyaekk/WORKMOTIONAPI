using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static WorkMotion_WebAPI.Model.ProvinceModel;
using static WorkMotion_WebAPI.Model.DistrictModel;
using static WorkMotion_WebAPI.Model.SubDistrictModel;
using static WorkMotion_WebAPI.Model.CustomerModel;
using static WorkMotion_WebAPI.Model.EmployeeModel;
using static WorkMotion_WebAPI.Model.WarrantyModel;
using static WorkMotion_WebAPI.Model.ProductModel;
using static WorkMotion_WebAPI.Model.StoreModel;
using static WorkMotion_WebAPI.Model.Product_TypeModel;
using static WorkMotion_WebAPI.Model.ModelModel;
using static WorkMotion_WebAPI.Model.ContentModel;
using static WorkMotion_WebAPI.Model.Content_HeadModel;
using static WorkMotion_WebAPI.Model.Content_FileModel;
using static WorkMotion_WebAPI.Model.MenuModel;
using static WorkMotion_WebAPI.Model.Receipt_FileModel;
using static WorkMotion_WebAPI.Model.ContentDataModel;
using static WorkMotion_WebAPI.Model.SparepartModel;
using static WorkMotion_WebAPI.Model.Product_ClassifiedModel;
using static WorkMotion_WebAPI.Model.Product_InstallationModel;
using static WorkMotion_WebAPI.Model.Product_PictureModel;
using static WorkMotion_WebAPI.Model.Product_SparepartModel;
using static WorkMotion_WebAPI.Model.Installation_FileModel;
using static WorkMotion_WebAPI.Model.ServiceInformationModel;
using static WorkMotion_WebAPI.Model.Care_CenterModel;
using static WorkMotion_WebAPI.Model.Care_AreaModel;
using static WorkMotion_WebAPI.Model.Menu_LinkMenu;
using static WorkMotion_WebAPI.Model.Product_Installation_ModelModel;
using static WorkMotion_WebAPI.Model.Product_Installation_ClassifiedModel;
using static WorkMotion_WebAPI.Model.Product_Installation_PictureModel;
using static WorkMotion_WebAPI.Model.Sparepart_PictureModel;
using static WorkMotion_WebAPI.Model.Product_Match_SparepartModel;
using static WorkMotion_WebAPI.Model.Service_FileModel;
using static WorkMotion_WebAPI.Model.Customer_StatusModel;
using static WorkMotion_WebAPI.Model.Ranning_NumberModel;
using static WorkMotion_WebAPI.Model.LogModel;
using static WorkMotion_WebAPI.Model.Customer_RenewModel;
using static WorkMotion_WebAPI.Model.User_GroupModel;
using static WorkMotion_WebAPI.Model.Menu_AdminModel;
using static WorkMotion_WebAPI.Model.Menu_Admin_DetailModel;
using static WorkMotion_WebAPI.Model.CounterModel;
using static WorkMotion_WebAPI.Model.Customer_Renew_StatusModel;
using static WorkMotion_WebAPI.Model.SettingEmailModel;

namespace WorkMotion_WebAPI.BaseModel
{
    public class ASCCContext : DbContext
    {
        public ASCCContext(DbContextOptions<ASCCContext> options) : base(options) { }
        public DbSet<GROUP_COURSE> GROUP_COURSE { get; set; }
        //public DbSet<Province> CCC_Province { get; set; }
        //public DbSet<District> CCC_District { get; set; }
        //public DbSet<Sub_District> CCC_Sub_District { get; set; }
        //public DbSet<Customer> CCC_Customer { get; set; }
        //public DbSet<Customer_Status> CCC_Customer_Status { get; set; }
        //public DbSet<Employee> CCC_Employee { get; set; }
        //public DbSet<Warranty> CCC_Warranty { get; set; }
        //public DbSet<Product> CCC_Product { get; set; }
        //public DbSet<Models> CCC_Model { get; set; }
        //public DbSet<Store> CCC_Store { get; set; }
        //public DbSet<Product_Type> CCC_Product_Type { get; set; }
        //public DbSet<Content> CCC_Content { get; set; }
        //public DbSet<Content_Head> CCC_Content_Head { get; set; }
        //public DbSet<Content_File> CCC_Content_File { get; set; }
        //public DbSet<Menu> CCC_Menu { get; set; }
        //public DbSet<Receipt_File> CCC_Receipt_File { get; set; }
        //public DbSet<ContentData> CCC_ContentData { get; set; }
        //public DbSet<Sparepart> CCC_Sparepart { get; set; }
        //public DbSet<Product_Match_Sparepart> CCC_Product_Match_Sparepart { get; set; }
        //public DbSet<Sparepart_Picture> CCC_Sparepart_Picture { get; set; }
        //public DbSet<Product_Classified> CCC_Product_Classified { get; set; }
        //public DbSet<Product_Picture> CCC_Product_Picture { get; set; }
        //public DbSet<Product_Sparepart> CCC_Product_Sparepart { get; set; }
        //public DbSet<Installation_File> CCC_Installation_File { get; set; }
        //public DbSet<ServiceInformation> CCC_ServiceInformation { get; set; }
        //public DbSet<Service_File> CCC_Service_File { get; set; }
        //public DbSet<Care_Center> CCC_Care_Center { get; set; }
        //public DbSet<Care_Area> CCC_Care_Area { get; set; }
        //public DbSet<Menu_Link> CCC_Menu_Link { get; set; }
        //public DbSet<Product_Installation_Model> CCC_Product_Installation_Model { get; set; }
        //public DbSet<Product_Installation> CCC_Product_Installation { get; set; }
        //public DbSet<Product_Installation_Classified> CCC_Product_Installation_Classified { get; set; }
        //public DbSet<Product_Installation_Picture> CCC_Product_Installation_Picture { get; set; }
        //public DbSet<Running_Number> CCC_Running_Number { get; set; }
        //public DbSet<Customer_Renew> CCC_Customer_Renew { get; set; }
        //public DbSet<Customer_Renew_Status> CCC_Customer_Renew_Status { get; set; }
        //public DbSet<Log> CCC_Log { get; set; }
        //public DbSet<User_Group> CCC_User_Group { get; set; }
        //public DbSet<Menu_Admin> CCC_Menu_Admin { get; set; }
        //public DbSet<Menu_Admin_Detail> CCC_Menu_Admin_Detail { get; set; }
        //public DbSet<Counters> CCC_Counter { get; set; }
        //public DbSet<SettingEmail> CCC_SettingEmail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Store>()
            //    //.HasAlternateKey(x => new { x.ID, x.Lang_ID, x.FK_Province_ID});
            //    .HasIndex(x => new { x.ID, x.Lang_ID, x.FK_Province_ID});
        }
    }
}