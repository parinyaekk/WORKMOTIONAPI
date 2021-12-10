using WorkMotion_WebAPI.BaseModel;
using WorkMotion_WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static WorkMotion_WebAPI.Model.BaseModel;
using static WorkMotion_WebAPI.Model.CustomerModel;
using static WorkMotion_WebAPI.Model.LogModel;
using static WorkMotion_WebAPI.Model.Menu_LinkMenu;
using static WorkMotion_WebAPI.Model.MenuModel;
using static WorkMotion_WebAPI.Model.ProductModel;
using static WorkMotion_WebAPI.Model.ProvinceModel;
using static WorkMotion_WebAPI.Model.SettingEmailModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        public MasterController(ASCCContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("GetServiceCenter")]
        public async Task<IActionResult> GetServiceCenter(RequiredLanguageID inputdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? Lang_ID = Convert.ToInt32(inputdata.Lang_ID);
                    if (Lang_ID > 0)
                    {
                        var dataCare = _dbContext.CCC_Care_Center.Where(x => x.Lang_ID == Lang_ID).ToList();
                        var result = (from c in dataCare
                                      select new
                                      {
                                          value = c.Code,
                                          label = c.Name
                                      }).Distinct().ToList();
                        if (result.Count > 0)
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                        else
                            return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filepath)
        {
            try
            {
                var net = new System.Net.WebClient();
                var filedata = filepath.Split('\\');
                var folderName = Path.Combine(filedata[0], filedata[1]);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), filepath);
                var data = net.DownloadData(pathToSave);
                var content = new System.IO.MemoryStream(data);
                var contentType = "APPLICATION/octet-stream";
                var fileName = filedata[2];

                return File(content, contentType, fileName);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("GetProvince")]
        public async Task<IActionResult> GetProvince(RequiredLanguageID inputdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? Lang_ID = Convert.ToInt32(inputdata.Lang_ID);
                    if (Lang_ID > 0)
                    {
                        var result = _dbContext.CCC_Province.Where(x => x.Lang_ID == Lang_ID).ToList();
                        if (result.Count > 0)
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                        else
                            return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetDistrict")]
        public async Task<IActionResult> GetDistrict(RequiredLanguageID inputdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? Lang_ID = Convert.ToInt32(inputdata.Lang_ID);
                    if (Lang_ID > 0)
                    {
                        var result = _dbContext.CCC_District.Where(x => x.Lang_ID == Lang_ID).ToList();
                        if (result.Count > 0)
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                        else
                            return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetSubDistrict")]
        public async Task<IActionResult> GetSubDistrict(RequiredLanguageID inputdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? Lang_ID = Convert.ToInt32(inputdata.Lang_ID);
                    if (Lang_ID > 0)
                    {
                        var result = _dbContext.CCC_Sub_District.Where(x => x.Lang_ID == Lang_ID).ToList();
                        if (result.Count > 0)
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                        else
                            return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetStore")]
        public async Task<IActionResult> GetStore(RequiredLanguageID inputdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? Lang_ID = Convert.ToInt32(inputdata.Lang_ID);
                    if (Lang_ID > 0)
                    {
                        //var result = _dbContext.CCC_Store.Where(x => x.Lang_ID == Lang_ID).ToList();
                        var result = _dbContext.CCC_Store.Where(x => x.Lang_ID == Lang_ID).GroupBy(x => new { x.ID, x.Lang_ID, x.Store_Name, x.Store_Branch, x.FK_Province_ID})
                          .Select(x => new {
                            x.Key.ID,
                            x.Key.Lang_ID,
                            x.Key.Store_Name,
                            x.Key.Store_Branch,
                            x.Key.FK_Province_ID
                          }).ToList();
                        if (result.Count() > 0)
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                        else
                            return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetAllDataContentHead")]
        public async Task<IActionResult> GetAllDataContentHead(RequiredLanguageID inputdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? Lang_ID = Convert.ToInt32(inputdata.Lang_ID);
                    if (Lang_ID > 0)
                    {
                        var result = _dbContext.CCC_Content_Head.OrderBy(x => x.ID).ToList();
                        if (result.Count > 0)
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                        else
                            return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetDataContentHeadByID")]
        public async Task<IActionResult> GetDataContentHeadByID(int ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ID > 0)
                    {
                        var Response = _dbContext.CCC_Content_Head.Where(x => x.ID == ID).FirstOrDefault();
                        if (Response != null)
                        {
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = Response });
                        }
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetAllDataMenu")]
        public async Task<IActionResult> GetAllDataMenu(RequiredLanguageID inputdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? Lang_ID = Convert.ToInt32(inputdata.Lang_ID);
                    if (Lang_ID > 0)
                    {
                        var result = _dbContext.CCC_Menu.OrderBy(x => x.ID).ToList();
                        if (result.Count > 0)
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                        else
                            return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetAllMenu")]
        public async Task<IActionResult> GetAllMenu()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _dbContext.CCC_Menu.OrderBy(x => x.ID).ToList();
                    if (result.Count > 0)
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                    else
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public int ID { get; set; }
        //public int? Lang_ID { get; set; }
        //public string Menu_Name { get; set; }
        //public string Menu_Desc { get; set; }
        //public string Menu_Link { get; set; }
        //public int? Menu_Order { get; set; }
        //public int? FK_Menu_ID { get; set; }
        //public int? Is_Active { get; set; }


        [HttpPost("GetAllMenuAdmin")]
        public async Task<IActionResult> GetAllMenuAdmin()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tempMenu = _dbContext.CCC_Menu;
                    var result = (from menu in tempMenu
                                  where menu.Is_Active == 1
                                  select new
                                  { 
                                      menu.ID,
                                      menu.Lang_ID,
                                      menu.Menu_Name,
                                      menu.Menu_Desc,
                                      menu.Menu_Link,
                                      menu.Menu_Order,
                                      menu.FK_Menu_ID,
                                      menu.Is_Active,
                                      fk_menu_name = menu.FK_Menu_ID != 0 ? tempMenu.Where(x => x.ID  == menu.FK_Menu_ID).FirstOrDefault().Menu_Name : ""
                                  }).OrderBy(x => x.ID).ToList();
                    if (result.Count > 0)
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                    else
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetAllMenulink")]
        public async Task<IActionResult> GetAllMenulink()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _dbContext.CCC_Menu_Link.ToList();
                    if (result.Count > 0)
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                    else
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetAllMenuPerPage")]
        public async Task<IActionResult> GetAllMenuPerPage(MenuPaginationModel input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var start = Convert.ToInt32((input.Page - 1) * input.PerPage);
                    var end = Convert.ToInt32(input.PerPage);
                    DateTime? StartDate = null;
                    if (!String.IsNullOrEmpty(input.Start))
                    {
                        StartDate = Convert.ToDateTime(input.Start).Date;
                    }
                    DateTime? EndDate = null;
                    if (!String.IsNullOrEmpty(input.End))
                    {
                        EndDate = Convert.ToDateTime(input.End).Date;
                    }
                    var tempAllMenu = _dbContext.CCC_Menu;
                    var tempMenu = tempAllMenu.Where(x => x.Is_Active == 1 &&
                                            (StartDate == null ? true : x.Create_Date == null ? true : Convert.ToDateTime(x.Create_Date).Date >= StartDate) &&
                                            (EndDate == null ? true : x.Create_Date == null ? true : Convert.ToDateTime(x.Create_Date).Date <= EndDate));

                    var TempData = (from menu in tempMenu
                                    select new
                                    {
                                        menu.ID,
                                        menu.Lang_ID,
                                        menu.Menu_Name,
                                        menu.Menu_Desc,
                                        menu.Menu_Link,
                                        menu.Menu_Order,
                                        menu.FK_Menu_ID,
                                        menu.Is_Active,
                                        fk_menu_name = menu.FK_Menu_ID == 0 ? "" : tempAllMenu.Where(x => x.ID == menu.FK_Menu_ID).Select(x => x.Menu_Name).FirstOrDefault(),
                                    });
                    TempData = TempData.Where(x => ((x.Menu_Name ?? "").Contains(input.Search)) ||
                               ((x.fk_menu_name ?? "").Contains(input.Search)) ||
                               ((x.Menu_Link ?? "").Contains(input.Search)));

                    var result = TempData.Skip(start).Take(end).OrderBy(x => x.ID).ToList();

                    var ResponseData = new
                    {
                        result,
                        Total = TempData.Count()
                    };
                    if (result.Count > 0)
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
                    else
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("AddDataMenu")]
        public async Task<IActionResult> AddDataMenu(AddDataMenuModel inputModel)
        {
            string msglog = "";
            try
            {
                if (ModelState.IsValid)
                {
                    msglog += "Start";
                    if (!String.IsNullOrWhiteSpace(inputModel.MenuName))
                    {
                        msglog += "Have a MenuName";
                        if (inputModel.Status == "Add")
                        {
                            msglog += "StatusAdd Case";
                            if (inputModel.Lang_ID == 1 || inputModel.Lang_ID == null)
                            {
                                var DataLinkMenu = _dbContext.CCC_Menu_Link.Where(x => x.FK_MENU_EN_ID == inputModel.Link_Menu).FirstOrDefault();
                                if(DataLinkMenu != null)
                                {
                                    Menu data = new Menu()
                                    {
                                        Lang_ID = inputModel.Lang_ID == null ? 1 : inputModel.Lang_ID,
                                        Menu_Name = inputModel.MenuName,
                                        Menu_Desc = inputModel.DescriptionMenu,
                                        FK_Menu_ID = inputModel.MainMenuName == null ? 0 : inputModel.MainMenuName,
                                        Menu_Link = inputModel.LinkMenu,
                                        Menu_Order = inputModel.OrderMenu,
                                        Hide_Header = inputModel.HeaderStatus,
                                        Hide_Footer = inputModel.FooterStatus,
                                        Is_Active = 1,
                                        Create_By = "Admin",
                                        Create_Date = DateTime.Now
                                    };
                                    _dbContext.CCC_Menu.Add(data);
                                    _dbContext.SaveChanges();

                                    DataLinkMenu.FK_MENU_EN_ID = inputModel.Link_Menu;
                                    DataLinkMenu.FK_MENU_TH_ID = data.ID;
                                    _dbContext.SaveChanges();
                                    try
                                    {
                                        msglog += "Success";
                                        Log log = new Log();
                                        log.Function = "AddDataMenu";
                                        log.Message = msglog;
                                        log.DateTime = DateTime.Now;
                                        _dbContext.CCC_Log.Add(log);
                                        _dbContext.SaveChanges();
                                    }
                                    catch
                                    {

                                    }
                                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                                }
                                else
                                {
                                    Menu data = new Menu()
                                    {
                                        Lang_ID = inputModel.Lang_ID == null ? 1 : inputModel.Lang_ID,
                                        Menu_Name = inputModel.MenuName,
                                        Menu_Desc = inputModel.DescriptionMenu,
                                        FK_Menu_ID = inputModel.MainMenuName == null ? 0 : inputModel.MainMenuName,
                                        Menu_Link = inputModel.LinkMenu,
                                        Menu_Order = inputModel.OrderMenu,
                                        Hide_Header = inputModel.HeaderStatus,
                                        Hide_Footer = inputModel.FooterStatus,
                                        Is_Active = 1,
                                        Create_By = "Admin",
                                        Create_Date = DateTime.Now
                                    };
                                    _dbContext.CCC_Menu.Add(data);
                                    _dbContext.SaveChanges();

                                    Menu_Link temp = new Menu_Link()
                                    {
                                        FK_MENU_TH_ID = data.ID,
                                        FK_MENU_EN_ID = inputModel.Link_Menu
                                    };
                                    _dbContext.CCC_Menu_Link.Add(temp);
                                    _dbContext.SaveChanges();
                                    try
                                    {
                                        msglog += "Success";
                                        Log log = new Log();
                                        log.Function = "AddDataMenu";
                                        log.Message = msglog;
                                        log.DateTime = DateTime.Now;
                                        _dbContext.CCC_Log.Add(log);
                                        _dbContext.SaveChanges();
                                    }
                                    catch
                                    {

                                    }
                                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                                }
                            }
                            else
                            {
                                msglog += "StatusUpdate Case";
                                var DataLinkMenu = _dbContext.CCC_Menu_Link.Where(x => x.FK_MENU_TH_ID == inputModel.Link_Menu).FirstOrDefault();
                                if (DataLinkMenu != null)
                                {
                                    msglog += "HaveDataLinkMenu";
                                    Menu data = new Menu()
                                    {
                                        Lang_ID = inputModel.Lang_ID == null ? 1 : inputModel.Lang_ID,
                                        Menu_Name = inputModel.MenuName,
                                        Menu_Desc = inputModel.DescriptionMenu,
                                        FK_Menu_ID = inputModel.MainMenuName == null ? 0 : inputModel.MainMenuName,
                                        Menu_Link = inputModel.LinkMenu,
                                        Menu_Order = inputModel.OrderMenu,
                                        Hide_Header = inputModel.HeaderStatus,
                                        Hide_Footer = inputModel.FooterStatus,
                                        Is_Active = 1,
                                        Create_By = "Admin",
                                        Create_Date = DateTime.Now
                                    };
                                    _dbContext.CCC_Menu.Add(data);
                                    _dbContext.SaveChanges();

                                    DataLinkMenu.FK_MENU_EN_ID = data.ID;
                                    DataLinkMenu.FK_MENU_TH_ID = inputModel.Link_Menu;
                                    _dbContext.SaveChanges();
                                    try
                                    {
                                        msglog += "Success";
                                        Log log = new Log();
                                        log.Function = "AddDataMenu";
                                        log.Message = msglog;
                                        log.DateTime = DateTime.Now;
                                        _dbContext.CCC_Log.Add(log);
                                        _dbContext.SaveChanges();
                                    }
                                    catch
                                    {

                                    }
                                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                                }
                                else
                                {
                                    msglog += "NotHaveDataLinkMenu";
                                    Menu data = new Menu()
                                    {
                                        Lang_ID = inputModel.Lang_ID == null ? 1 : inputModel.Lang_ID,
                                        Menu_Name = inputModel.MenuName,
                                        Menu_Desc = inputModel.DescriptionMenu,
                                        FK_Menu_ID = inputModel.MainMenuName == null ? 0 : inputModel.MainMenuName,
                                        Menu_Link = inputModel.LinkMenu,
                                        Menu_Order = inputModel.OrderMenu,
                                        Hide_Header = inputModel.HeaderStatus,
                                        Hide_Footer = inputModel.FooterStatus,
                                        Is_Active = 1,
                                        Create_By = "Admin",
                                        Create_Date = DateTime.Now
                                    };
                                    _dbContext.CCC_Menu.Add(data);
                                    _dbContext.SaveChanges();

                                    Menu_Link temp = new Menu_Link()
                                    {
                                        FK_MENU_TH_ID = inputModel.Link_Menu,
                                        FK_MENU_EN_ID = data.ID
                                    };
                                    _dbContext.CCC_Menu_Link.Add(temp);
                                    _dbContext.SaveChanges();
                                    try
                                    {
                                        msglog += "Success";
                                        Log log = new Log();
                                        log.Function = "AddDataMenu";
                                        log.Message = msglog;
                                        log.DateTime = DateTime.Now;
                                        _dbContext.CCC_Log.Add(log);
                                        _dbContext.SaveChanges();
                                    }
                                    catch
                                    {

                                    }
                                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                                }
                            }                            
                        }
                        else
                        {
                            msglog += "Update";
                            if (inputModel.ID != null)
                            {
                                if(inputModel.Lang_ID == 1 && inputModel.Lang_ID != null)
                                {
                                    var FindData = _dbContext.CCC_Menu.Where(x => x.ID == inputModel.ID).FirstOrDefault();
                                    if (FindData != null)
                                    {
                                                                            
                                        FindData.Lang_ID = inputModel.Lang_ID == null ? 1 : inputModel.Lang_ID;
                                        FindData.Menu_Name = inputModel.MenuName;
                                        FindData.Menu_Order = inputModel.OrderMenu;
                                        FindData.Menu_Link = inputModel.LinkMenu;
                                        FindData.FK_Menu_ID = inputModel.MainMenuName == null ? 0 : inputModel.MainMenuName;
                                        FindData.Is_Active = inputModel.Active == 2 ? 0 : 1;
                                        FindData.Menu_Desc = inputModel.DescriptionMenu;
                                        FindData.Hide_Header = inputModel.HeaderStatus;
                                        FindData.Hide_Footer = inputModel.FooterStatus;
                                        FindData.Update_By = "Admin";
                                        FindData.Update_Date = DateTime.Now;
                                        _dbContext.CCC_Menu.Update(FindData);
                                        _dbContext.SaveChanges();

                                        var LinkMenuRemove = _dbContext.CCC_Menu_Link.Where(x => x.FK_MENU_TH_ID == FindData.ID || x.FK_MENU_EN_ID == FindData.ID).ToList();
                                        _dbContext.CCC_Menu_Link.RemoveRange(LinkMenuRemove);
                                        _dbContext.SaveChanges();
                                        //foreach (var item in LinkMenuRemove)
                                        //{
                                        //    _dbContext.CCC_Menu_Link.Remove(item);
                                        //    _dbContext.SaveChanges();
                                        //}

                                        var DataLinkMenu = _dbContext.CCC_Menu_Link.Where(x => x.FK_MENU_TH_ID == FindData.ID).LastOrDefault();
                                        if (DataLinkMenu != null)
                                        {
                                            DataLinkMenu.FK_MENU_EN_ID = inputModel.Link_Menu;
                                            DataLinkMenu.FK_MENU_TH_ID = FindData.ID;
                                            _dbContext.CCC_Menu_Link.Update(DataLinkMenu);
                                            _dbContext.SaveChanges();
                                        }
                                        else
                                        {
                                            Menu_Link data = new Menu_Link()
                                            {
                                                FK_MENU_EN_ID = inputModel.Link_Menu,
                                                FK_MENU_TH_ID = FindData.ID
                                            };
                                            _dbContext.CCC_Menu_Link.Add(data);
                                            _dbContext.SaveChanges();
                                        }
                                        try
                                        {
                                            msglog += "Success";
                                            Log log = new Log();
                                            log.Function = "AddDataMenu";
                                            log.Message = msglog;
                                            log.DateTime = DateTime.Now;
                                            _dbContext.CCC_Log.Add(log);
                                            _dbContext.SaveChanges();
                                        }
                                        catch
                                        {

                                        }
                                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                                    }
                                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                                }
                                else
                                {
                                    var FindData = _dbContext.CCC_Menu.Where(x => x.ID == inputModel.ID).FirstOrDefault();
                                    if (FindData != null)
                                    {
                                        FindData.Lang_ID = inputModel.Lang_ID == null ? 1 : inputModel.Lang_ID;
                                        FindData.Menu_Name = inputModel.MenuName;
                                        FindData.Menu_Order = inputModel.OrderMenu;
                                        FindData.Menu_Link = inputModel.LinkMenu;
                                        FindData.FK_Menu_ID = inputModel.MainMenuName == null ? 0 : inputModel.MainMenuName;
                                        FindData.Is_Active = inputModel.Active == 2 ? 0 : 1;
                                        FindData.Menu_Desc = inputModel.DescriptionMenu;
                                        FindData.Hide_Header = inputModel.HeaderStatus;
                                        FindData.Hide_Footer = inputModel.FooterStatus;
                                        FindData.Update_By = "Admin";
                                        FindData.Update_Date = DateTime.Now;

                                        _dbContext.CCC_Menu.Update(FindData);
                                        _dbContext.SaveChanges();

                                        var LinkMenuRemove = _dbContext.CCC_Menu_Link.Where(x => x.FK_MENU_EN_ID == FindData.ID || x.FK_MENU_TH_ID == FindData.ID).ToList();
                                        _dbContext.CCC_Menu_Link.RemoveRange(LinkMenuRemove);
                                        _dbContext.SaveChanges();
                                        //foreach (var item in LinkMenuRemove)
                                        //{
                                        //    _dbContext.CCC_Menu_Link.Remove(item);
                                        //    _dbContext.SaveChanges();
                                        //}

                                        var DataLinkMenu = _dbContext.CCC_Menu_Link.Where(x => x.FK_MENU_EN_ID == FindData.ID).LastOrDefault();
                                        if (DataLinkMenu != null)
                                        {
                                            DataLinkMenu.FK_MENU_TH_ID = inputModel.Link_Menu;
                                            DataLinkMenu.FK_MENU_EN_ID = FindData.ID;
                                            _dbContext.CCC_Menu_Link.Update(DataLinkMenu);
                                            _dbContext.SaveChanges();
                                        }
                                        else
                                        {
                                            Menu_Link data = new Menu_Link()
                                            {
                                                FK_MENU_EN_ID = FindData.ID,
                                                FK_MENU_TH_ID = inputModel.Link_Menu
                                            };
                                            _dbContext.CCC_Menu_Link.Add(data);
                                            _dbContext.SaveChanges();
                                        }
                                        try
                                        {
                                            msglog += "Success";
                                            Log log = new Log();
                                            log.Function = "AddDataMenu";
                                            log.Message = msglog;
                                            log.DateTime = DateTime.Now;
                                            _dbContext.CCC_Log.Add(log);
                                            _dbContext.SaveChanges();
                                        }
                                        catch
                                        {

                                        }
                                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                                    }
                                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                                }                               
                            }
                            return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                        }
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });                    
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                try
                {
                    msglog += "Fail";
                    Log log = new Log();
                    log.Function = "AddDataMenu";
                    log.Message = ex.Message + ";" + msglog;
                    log.DateTime = DateTime.Now;
                    _dbContext.CCC_Log.Add(log);
                    _dbContext.SaveChanges();
                }
                catch
                {

                }
                throw ex;
            }
        }

        [HttpPost("GetDataMenuByID")]
        public async Task<IActionResult> GetDataMenuByID(int ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(ID > 0)
                    {
                        var GetDataMainMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1 && x.ID == Convert.ToInt32(ID)).ToList();
                        var Response = (from menu in GetDataMainMenu
                                      select new
                                      {
                                          id = menu.ID,
                                          fK_Menu_ID = menu.FK_Menu_ID,
                                          menu_Name = menu.Menu_Name,
                                          menu_Link = menu.Menu_Link,
                                          menu_Order = menu.Menu_Order,
                                          menu_Desc = menu.Menu_Desc,
                                          lang_ID = menu.Lang_ID,
                                          is_Active = menu.Is_Active,
                                          hide_Header = menu.Hide_Header,
                                          hide_Footer = menu.Hide_Footer,
                                          menulink = menu.Lang_ID == 1 ? _dbContext.CCC_Menu_Link.Where(x => x.FK_MENU_TH_ID == menu.ID).Select(x => x.FK_MENU_EN_ID).FirstOrDefault() : _dbContext.CCC_Menu_Link.Where(x => x.FK_MENU_EN_ID == menu.ID).Select(x => x.FK_MENU_TH_ID).FirstOrDefault(),
                                          menulink_lang = menu.Lang_ID == 1 ? "TH" : "EN"
                                      }).LastOrDefault();
                        if (Response != null)
                        {
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = Response });
                        }
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("InActiveDataMenu")]
        public async Task<IActionResult> InActiveDataMenu(int ID)
        {
            string msglog = "";
            try
            {
                if (ModelState.IsValid)
                {
                    msglog += "Start";
                    if (ID > 0)
                    {
                        msglog += "ID != 0";
                        var Response = _dbContext.CCC_Menu.Where(x => x.ID == ID).FirstOrDefault();
                        if (Response != null)
                        {
                            Response.Is_Active = 0;
                            _dbContext.CCC_Menu.Update(Response);
                            _dbContext.SaveChanges();
                            try
                            {
                                msglog += "Success";
                                Log log = new Log();
                                log.Function = "InActiveDataMenu";
                                log.Message = msglog;
                                log.DateTime = DateTime.Now;
                                _dbContext.CCC_Log.Add(log);
                                _dbContext.SaveChanges();
                            }
                            catch
                            {

                            }
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = Response });
                        }
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                try
                {
                    msglog += "Fail";
                    Log log = new Log();
                    log.Function = "InActiveDataMenu";
                    log.Message = ex.Message + ";" + msglog;
                    log.DateTime = DateTime.Now;
                    _dbContext.CCC_Log.Add(log);
                    _dbContext.SaveChanges();
                }
                catch
                {

                }
                throw ex;
            }
        }

        [HttpPost("GetAllDataASCC")]
        public async Task<IActionResult> GetAllDataASCC(string Search)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrWhiteSpace(Search))
                    {
                        #region Care
                        var DataCareArea = _dbContext.CCC_Care_Area.Where(x => x.Is_Active == 1);
                        var DataCareCenter = _dbContext.CCC_Care_Center.Where(x => x.Is_Active == 1);

                        var Care = (from c in DataCareCenter
                                      join a in DataCareArea on c.Code equals a.Code
                                      where ((a.Province_Name ?? "").Contains(Search)) || ((c.Code ?? "").Contains(Search))
                                        || ((c.Name ?? "").Contains(Search)) || ((c.Address ?? "").Contains(Search)) || ((c.Phone ?? "").Contains(Search))
                                        || ((c.Office_Hour ?? "").Contains(Search)) || ((c.Map_Href ?? "").Contains(Search)) || ((c.Latitude ?? "").Contains(Search)) || ((c.Longitude ?? "").Contains(Search))
                                      select new
                                      {
                                          ID = c.ID,
                                          Lang_ID = c.Lang_ID,
                                          Province_ID = a.Province_ID,
                                          Province_Name = a.Province_Name,
                                          Code = c.Code,
                                          Name = c.Name,
                                          Address = c.Address,
                                          Phone = c.Phone,
                                          Office_Hour = c.Office_Hour,
                                          Map_Href = c.Map_Href,
                                          Latitude = c.Latitude,
                                          Longitude = c.Longitude,
                                          Is_Active = c.Is_Active
                                      }).OrderBy(x => x.ID).Take(100).ToList();
                        #endregion

                        #region Content
                        var GetDataContent = _dbContext.CCC_Content.Where(x => x.Is_Active == 1).ToList();
                        var GetDataMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1).ToList();
                        var GetDataContent_File = _dbContext.CCC_Content_File.Where(x => x.Is_Active == 1).ToList();

                        var Content = (from m in GetDataMenu
                                       join con1 in GetDataContent on m.ID equals con1.FK_Menu_ID into con2
                                       from con in con2.DefaultIfEmpty()
                                       where ((con?.Content_Title ?? "").Contains(Search)) || ((con?.Content_Body ?? "").Contains(Search))
                                       || ((m.Menu_Name ?? "").Contains(Search))
                                       select new
                                       {
                                           id = con == null ? 0 : con.ID,
                                           lang = con == null ? 0 : con.Lang_ID,
                                           content_title = con == null ? "" : con.Content_Title,
                                           content_detail = con == null ? "" : con.Content_Body,
                                           content_order = con == null ? 0 : con.Content_Order,
                                           Create_Date = con == null ? DateTime.Now : con.Create_Date,
                                           menu_name = m.Menu_Name,
                                           status = con == null ? 0 : con.Is_Active,
                                           file = (from f in GetDataContent_File.Where(x => con != null && x.FK_Content_ID == con.ID)
                                                   select new
                                                   {
                                                       id = f.ID,
                                                       name = f.File_Name,
                                                       type = f.File_Type,
                                                       path = f.File_Path,
                                                       link = f.Href_Link,
                                                   }).ToList(),
                                       }).OrderBy(x => x.content_order).ThenByDescending(x => x.Create_Date).Take(100).ToList();
                        #endregion

                        #region Customer_Renew
                        var DataCustomer = _dbContext.CCC_Customer.Where(x => x.Flag_Member == 0 || x.Flag_Member == null);
                        var DataCustomer_Status = _dbContext.CCC_Customer_Status;
                        var DataCustomer_Renew = _dbContext.CCC_Customer_Renew;
                        var Customer_Renew1 = (from re in DataCustomer_Renew
                                       join co1 in DataCustomer on re.FK_Customer_ID equals co1.ID into Temp_co
                                       from co in Temp_co.DefaultIfEmpty()
                                       where co != null
                                       select new
                                       {
                                           id = re.ID,
                                           customer_Code = co.Customer_Code,
                                           customer_Name = co.Customer_Name,
                                           customer_Surname = co.Customer_Surname,
                                           customer_Tel = co.Customer_Tel,
                                           customer_Email = co.Customer_Email,
                                           customer_Address = co.Customer_Address,
                                           customer_Type = co.Customer_Type,
                                           is_Active = co.Is_Active,
                                           create_Date = co.Create_Date,
                                       });
                        var Customer_Renew = (from co in Customer_Renew1
                                              where ((co.customer_Code ?? "").Contains(Search)) ||
                                              (((co.customer_Name ?? "") + " " + (co.customer_Surname ?? "")).Contains(Search)) ||
                                              ((co.customer_Tel ?? "").Contains(Search)) ||
                                              ((co.customer_Address ?? "").Contains(Search))
                                              select new
                                              {
                                                  id = co.id,
                                                  customer_Code = co.customer_Code,
                                                  customer_Name = co.customer_Name,
                                                  customer_Tel = co.customer_Tel,
                                                  customer_Email = co.customer_Email,
                                                  customer_Address = co.customer_Address,
                                                  customer_Type = co.customer_Type,
                                                  is_Active = co.is_Active,
                                                  create_Date = co.create_Date,
                                              }).OrderByDescending(x => x.create_Date).Take(100).ToList();
                        #endregion

                        #region Installation_Model
                        var Installation_Model1 = _dbContext.CCC_Product_Installation_Model;

                        var Installation_Model = (from m in Installation_Model1.Where(x => (x.Model_Name ?? "").Contains(Search))
                                                  select new
                                                  {
                                                      id = m.ID,
                                                      lang = m.Lang_ID,
                                                      model_name = m.Model_Name,
                                                      is_active = m.Is_Active,
                                                  }).OrderBy(x => x.id).Take(100).ToList();
                        #endregion

                        #region Installation_Classified
                        var DataInstallation_Classified = _dbContext.CCC_Product_Installation_Classified;
                        var DataInstallation_Model1 = _dbContext.CCC_Product_Installation_Model;

                        var Installation_Classified1 = (from f in DataInstallation_Classified
                                                        join m1 in DataInstallation_Model1 on f.FK_Model_ID equals m1.ID into t_m
                                                       from m in t_m.DefaultIfEmpty()
                                                       join ff1 in DataInstallation_Classified on f.FK_Classified_ID equals ff1.ID into t_ff
                                                       from ff in t_ff.DefaultIfEmpty()
                                                       select new
                                                       {
                                                           id = f.ID,
                                                           classified_name = ff == null ? f.Classified_Name : ff.Classified_Name,
                                                           sub_classified_name = ff == null ? "" : f.Classified_Name,
                                                           model_name = m == null ? "" : m.Model_Name,
                                                           is_active = f.Is_Active,
                                                       });

                        var Installation_Classified = (from r in Installation_Classified1
                                                      where (r.classified_name ?? "").Contains(Search) || (r.sub_classified_name ?? "").Contains(Search) ||
                                                      (r.model_name ?? "").Contains(Search)
                                                      select new
                                                      {
                                                          id = r.id,
                                                          classified_name = r.classified_name,
                                                          sub_classified_name = r.sub_classified_name,
                                                          model_name = r.model_name,
                                                          is_active = r.is_active,
                                                      }).OrderBy(x => x.id).Take(100).ToList();
                        #endregion

                        #region Menu
                        var tempMenu = _dbContext.CCC_Menu;
                        var Menu = (from menu in tempMenu.Where(x => ((x.Menu_Name ?? "").Contains(Search)) || ((x.Menu_Desc ?? "").Contains(Search)) || ((x.Menu_Link ?? "").Contains(Search)))
                                      select new
                                      {
                                          menu.ID,
                                          menu.Lang_ID,
                                          menu.Menu_Name,
                                          menu.Menu_Desc,
                                          menu.Menu_Link,
                                          menu.Menu_Order,
                                          menu.FK_Menu_ID,
                                          menu.Is_Active,
                                          fk_menu_name = menu.FK_Menu_ID != 0 ? tempMenu.Where(x => x.ID == menu.FK_Menu_ID).FirstOrDefault().Menu_Name : "",
                                      }).Take(100).OrderBy(x => x.ID).ToList();
                        #endregion

                        #region Product
                        var TempProduct = _dbContext.CCC_Product.Where(x => ((x.Product_Code ?? "").Contains(Search)) ||
                                                   ((x.Product_Old_Code ?? "").Contains(Search)) ||
                                                   ((x.Product_Name ?? "").Contains(Search)) ||
                                                   ((x.Product_Barcode ?? "").Contains(Search)));
                        var Product = (from pro in TempProduct
                                       select new
                                      {
                                          pro
                                      }).OrderBy(x => x.pro.ID).Take(100).ToList();
                        #endregion

                        #region ServiceInformation
                        var TempServiceInformation = _dbContext.CCC_ServiceInformation.Where(x => x.Is_Active == 1 && (((x.Service_Number ?? "").Contains(Search)) ||
                                                    ((x.Customer_Name ?? "").Contains(Search)) ||
                                                    ((x.Customer_Address ?? "").Contains(Search)) ||
                                                    ((x.Service_Center ?? "").Contains(Search))));
                        var ServiceInformation = TempServiceInformation.Where(x => x.Is_Active == 1).OrderBy(x => x.ID).Take(100).ToList();
                        #endregion

                        #region SparePart_Model
                        var DataSparePartModel1 = _dbContext.CCC_Model;

                        var SparePart_Model = (from m in DataSparePartModel1.Where(x => (x.Model_Name ?? "").Contains(Search))
                                      select new
                                      {
                                          id = m.ID,
                                          lang = m.Lang_ID,
                                          model_name = m.Model_Name,
                                          is_active = m.Is_Active,
                                      }).OrderBy(x => x.id).Take(100).ToList();
                        #endregion

                        #region SparePart_Classified
                        var DataSparePartClassified = _dbContext.CCC_Product_Classified;
                        var DataSparePartModel = _dbContext.CCC_Model;

                        var SparePart_Classified1 = (from f in DataSparePartClassified
                                       join m1 in DataSparePartModel on f.FK_Model_ID equals m1.ID into t_m
                                       from m in t_m.DefaultIfEmpty()
                                       join ff1 in DataSparePartClassified on f.FK_Classified_ID equals ff1.ID into t_ff
                                       from ff in t_ff.DefaultIfEmpty()
                                       select new
                                       {
                                           id = f.ID,
                                           classified_name = ff == null ? f.Classified_Name : ff.Classified_Name,
                                           sub_classified_name = ff == null ? "" : f.Classified_Name,
                                           model_name = m == null ? "" : m.Model_Name,
                                           is_active = f.Is_Active,
                                       });

                        var SparePart_Classified = (from r in SparePart_Classified1
                                                    where (r.classified_name ?? "").Contains(Search) || (r.sub_classified_name ?? "").Contains(Search) ||
                                                  (r.model_name ?? "").Contains(Search)
                                                  select new
                                                  {
                                                      id = r.id,
                                                      classified_name = r.classified_name,
                                                      sub_classified_name = r.sub_classified_name,
                                                      model_name = r.model_name,
                                                      is_active = r.is_active,
                                                  }).OrderBy(x => x.id).Take(100).ToList();
                        #endregion

                        #region Sparepart_And_Installation
                        var DataProduct_Sparepart = _dbContext.CCC_Product_Sparepart.Where(x => x.Is_Active == 1).ToList();
                        var DataProduct_Installation = _dbContext.CCC_Product_Installation.Where(x => x.Is_Active == 1).ToList();
                        var DataSparepart = _dbContext.CCC_Sparepart.Where(x => x.Is_Active == 1).ToList();
                        var DataProduct_Classified = _dbContext.CCC_Product_Classified.Where(x => x.Is_Active == 1).ToList();
                        var DataProduct_Picture = _dbContext.CCC_Product_Picture.Where(x => x.Is_Active == 1).ToList();
                        var DataModel = _dbContext.CCC_Model.Where(x => x.Is_Active == 1).ToList();
                        var DataProduct_Installation_Picture = _dbContext.CCC_Product_Installation_Picture.Where(x => x.Is_Active == 1).ToList();
                        var DataInstallation_File = _dbContext.CCC_Installation_File.Where(x => x.Is_Active == 1).ToList();
                        var DataProduct_Installation_Model = _dbContext.CCC_Product_Installation_Model.Where(x => x.Is_Active == 1).ToList();
                        var DataProduct_Installation_Classified = _dbContext.CCC_Product_Installation_Classified.Where(x => x.Is_Active == 1).ToList();

                        var leftOuterJoin = (from pro in DataProduct_Sparepart
                                             join Spa1 in DataSparepart on pro.ID equals Spa1.FK_Product_ID into Spa2
                                             from Spa in Spa2.DefaultIfEmpty()
                                             join model1 in DataModel on pro.FK_Model_ID equals model1.ID into model2
                                             from model in model2.DefaultIfEmpty()
                                             join Class1 in DataProduct_Classified on pro.FK_Classified_ID equals Class1.ID into Class2
                                             from Class in Class2.DefaultIfEmpty()
                                             join subClass1 in DataProduct_Classified on pro.FK_Classified_ID2 equals subClass1.ID into subClass2
                                             from subClass in subClass2.DefaultIfEmpty()
                                             join ins2 in DataProduct_Installation on pro.Product_Old_Code equals ins2.Product_Old_Code into ins3
                                             from ins in ins3.DefaultIfEmpty()
                                             select new
                                             {
                                                 Type = pro.ID > 0 && ins == null ? 1 : pro.ID > 0 && ins.ID > 0 ? 0 : 2, //0มีทั้งคู่, 1 แค่อะไหล่, 2 แค่ติดตั้ง
                                                 pro.Product_Old_Code,
                                                 pro.Product_Name,
                                                 product_model = model == null ? "" : model.Model_Name,
                                                 product_class = Class == null ? "" : Class.Classified_Name,
                                                 product_sub_class = subClass == null ? "" : subClass.Classified_Name,
                                                 pro.Is_Active,
                                                 Is_Continue = Convert.ToInt32(pro.Is_Continue),
                                                 pro.Lang_ID,
                                                 pro.Create_Date,
                                                 picture = DataProduct_Picture.Where(x => x.FK_Product_ID == pro.ID).FirstOrDefault()?.File_Path
                                             });

                        var rightOuterJoin = (from ins in DataProduct_Installation
                                              join model1 in DataProduct_Installation_Model on ins.FK_Model_ID equals model1.ID into model2
                                              from model in model2.DefaultIfEmpty()
                                              join Class1 in DataProduct_Installation_Classified on ins.FK_Classified_ID equals Class1.ID into Class2
                                              from Class in Class2.DefaultIfEmpty()
                                              join subClass1 in DataProduct_Installation_Classified on ins.FK_Classified_ID2 equals subClass1.ID into subClass2
                                              from subClass in subClass2.DefaultIfEmpty()
                                              join pro2 in DataProduct_Sparepart on ins.Product_Old_Code equals pro2.Product_Old_Code into pro3
                                              from pro in pro3.DefaultIfEmpty()
                                              select new
                                              {
                                                  Type = ins.ID > 0 && pro == null ? 2 : pro.ID > 0 && ins.ID > 0 ? 0 : 1, //0มีทั้งคู่, 1 แค่อะไหล่, 2 แค่ติดตั้ง
                                                  ins.Product_Old_Code,
                                                  ins.Product_Name,
                                                  product_model = model == null ? "" : model.Model_Name,
                                                  product_class = Class == null ? "" : Class.Classified_Name,
                                                  product_sub_class = subClass == null ? "" : subClass.Classified_Name,
                                                  ins.Is_Active,
                                                  Is_Continue = 1,
                                                  ins.Lang_ID,
                                                  ins.Create_Date,
                                                  picture = DataProduct_Installation_Picture.Where(x => x.FK_Product_ID == ins.ID).FirstOrDefault()?.File_Path
                                              });


                        var Tempdate = leftOuterJoin.Union(rightOuterJoin);

                        Tempdate = Tempdate.Where(x => (x.Product_Old_Code ?? "").Contains(Search) || (x.Product_Name ?? "").Contains(Search) || (x.Product_Old_Code ?? "").Contains(Search) || (x.product_model ?? "").Contains(Search) || (x.product_class ?? "").Contains(Search) || (x.product_sub_class ?? "").Contains(Search)).OrderByDescending(x => x.Create_Date)
                            .GroupBy(x => new { x.Product_Old_Code, x.Lang_ID }).Select(x => x.FirstOrDefault());

                        var Sparepart_And_Installation = (from res in Tempdate.Where(x => (x.Product_Old_Code ?? "").Contains(Search) || (x.Product_Name ?? "").Contains(Search) || (x.Product_Old_Code ?? "").Contains(Search) || (x.product_model ?? "").Contains(Search) || (x.product_class ?? "").Contains(Search) || (x.product_sub_class ?? "").Contains(Search))
                                                          select new
                                                          {
                                                              res.Type, //0มีทั้งคู่, 1 แค่อะไหล่, 2 แค่ติดตั้ง
                                                              res.Product_Old_Code,
                                                              res.Product_Name,
                                                              res.product_model,
                                                              res.product_class,
                                                              res.product_sub_class,
                                                              res.Is_Active,
                                                              res.Is_Continue,
                                                              res.Lang_ID,
                                                              res.picture,
                                                          }).OrderBy(x => x.Product_Name).Take(100).ToList();
                        #endregion

                        #region Warranty
                        CultureInfo culture = new CultureInfo("pt-BR");
                        var GetDataProvince = _dbContext.CCC_Province.ToList();
                        var GetDataDistrict = _dbContext.CCC_District.ToList();
                        var GetDataSub_District = _dbContext.CCC_Sub_District.ToList();
                        var GetDataModel = _dbContext.CCC_Model.Where(x => x.Is_Active == 1).ToList();
                        var GetDataProduct = _dbContext.CCC_Product.Where(x => x.Is_Active == 1).ToList();
                        var GetDataStore = _dbContext.CCC_Store.Where(x => x.Is_Active == 1).ToList();
                        var CustomerData = _dbContext.CCC_Customer/*.Where(x => x.Is_Active == 1)*/.ToList();
                        var GetDataProduct_Type = _dbContext.CCC_Product_Type.Where(x => x.Is_Active == 1).ToList();
                        var GetDataReceipt_File = _dbContext.CCC_Receipt_File.Where(x => x.Is_Active == 1).ToList();

                        var TempProduct1 = (from p in GetDataProduct
                                           //join m in GetDataModel on p.FK_Model_ID equals m.ID into m2
                                           //from m3 in m2
                                           //join t in GetDataProduct_Type on p.FK_Type_ID equals t.ID into t2
                                           //from t3 in t2
                                       select new
                                       {
                                           ID = p.ID,
                                           Product_Name = p.Product_Name,
                                           Product_Code = p.Product_Code,
                                           Model_ID = p.FK_Model_ID == null ? null : GetDataModel.Where(x => x.ID == p.FK_Model_ID).FirstOrDefault()?.ID,
                                           Model_Name = p.FK_Model_ID == null ? null : GetDataModel.Where(x => x.ID == p.FK_Model_ID).FirstOrDefault()?.Model_Name,
                                           Type_ID = p.FK_Type_ID == null ? null : GetDataProduct_Type.Where(x => x.ID == p.FK_Type_ID).FirstOrDefault()?.ID,
                                           Type_Name = p.FK_Type_ID == null ? null : GetDataProduct_Type.Where(x => x.ID == p.FK_Type_ID).FirstOrDefault()?.Type_Name,
                                       }).ToList();

                        var Warranty = (from warr in _dbContext.CCC_Warranty.Where(x => x.Is_Active == 1)
                                            join cus in CustomerData on warr.FK_Customer_ID equals cus.ID
                                            join pro1 in TempProduct1 on warr.FK_Product_ID equals pro1.ID into pro2
                                            from pro in pro2
                                            where ((cus.Customer_Code ?? "").Contains(Search)) || ((cus.Customer_Phone ?? "").Contains(Search)) ||
                                            ((pro.Type_Name ?? "").Contains(Search)) || ((pro.Product_Code ?? "").Contains(Search)) || ((cus.Customer_Address ?? "").Contains(Search))
                                            select new
                                            {
                                                ID = warr.ID,
                                                //warranty_Date_Format = warr.Warranty_Date == null ? "" : String.Format("{0:dd/MM/yyyy}", warr.Warranty_Date),
                                                warranty_Date_Format = warr.Warranty_Date == null ? "" : Convert.ToDateTime(warr.Warranty_Date).ToString("d", culture),
                                                Warranty_Date = warr.Warranty_Date,
                                                Store_ID = warr.FK_Store_ID,
                                                Store_Name = GetDataStore.Where(x => x.ID == warr.FK_Store_ID).FirstOrDefault() == null ? "" :
                                                ((GetDataStore.Where(x => x.ID == warr.FK_Store_ID).Select(x => x.Store_Name).FirstOrDefault()) + " (" + (GetDataStore.Where(x => x.ID == warr.FK_Store_ID).Select(x => x.Store_Branch).FirstOrDefault()) + ")"
                                                ),
                                                Province_ID = warr.FK_Province_ID,
                                                Province_Name = GetDataProvince.Where(x => x.ID == warr.FK_Province_ID).FirstOrDefault() == null ? "" : GetDataProvince.Where(x => x.ID == warr.FK_Province_ID).Select(x => x.Province_Name).FirstOrDefault(),
                                                Quota = "1",
                                                Product_ID = pro.ID,
                                                Product_Name = pro.Product_Name,
                                                ProductCode = pro.Product_Code,
                                                Model_ID = pro.Model_ID,
                                                Model_Name = pro.Model_Name,
                                                Type_ID = pro.Type_ID,
                                                Type_Name = pro.Type_Name,
                                                Customer_Address = cus.Customer_Address,
                                                Custommer_ID = cus.ID,
                                                Customer_Name = cus.Customer_Name,
                                                Customer_Surname = cus.Customer_Surname,
                                                Customer_ZipCode = cus.Customer_ZIP_Code,
                                                Customer_Tel = cus.Customer_Tel,
                                                Customer_Mobile = cus.Customer_Phone,
                                                Customer_Email = cus.Customer_Email,
                                                Customer_Type = cus.Customer_Type == null ? null : cus.Customer_Type.ToString(),
                                                Customer_Code = cus.Customer_Code,
                                                ServiceCenter = cus.Service_Center,
                                                Receipt_Number = warr.Receipt_Number,
                                                Barcode_No = warr.Barcode_No,
                                                Store_Other_Name = warr.Store_Other_Name,
                                                Warranty_No = warr.Warranty_No,
                                                Product_Code_Other = warr.Product_Code_Other,
                                                Product_QTY = warr.Product_QTY,
                                                Score = warr.Score,
                                                Description = warr.Description,
                                                customer_province_ID = cus.FK_Province_ID,
                                                customer_province_name = GetDataProvince.Where(x => x.ID == cus.FK_Province_ID).FirstOrDefault() == null ? "" : GetDataProvince.Where(x => x.ID == cus.FK_Province_ID).Select(x => x.Province_Name).FirstOrDefault(),
                                                sub_District_ID = cus.FK_Sub_District_ID,
                                                sub_district_Name = GetDataSub_District.Where(x => x.ID == cus.FK_Sub_District_ID).FirstOrDefault() == null ? "" : GetDataSub_District.Where(x => x.ID == cus.FK_Sub_District_ID).Select(x => x.Sub_District_Name).FirstOrDefault(),
                                                district_ID = cus.FK_District_ID,
                                                district_Name = GetDataDistrict.Where(x => x.ID == cus.FK_District_ID).FirstOrDefault() == null ? "" : GetDataDistrict.Where(x => x.ID == cus.FK_District_ID).Select(x => x.District_Name).FirstOrDefault(),
                                                File = (from f in GetDataReceipt_File.Where(x => x.FK_Warranty_ID == warr.ID)
                                                        select new
                                                        {
                                                            id = f.ID,
                                                            name = f.File_Name,
                                                            path = f.File_Path
                                                        }).ToList(),
                                            }).OrderByDescending(x => x.ID).Take(100).ToList();
                        #endregion

                        #region CustomerRegister
                        var DataCustomerRegister = _dbContext.CCC_Customer.Where(x => x.Flag_Member == 0 || x.Flag_Member == null);
                        var DataCustomerRegister_Status = _dbContext.CCC_Customer_Status;
                        var CustomerRegister = (from co in DataCustomerRegister
                                      where ((co.Customer_Code ?? "").Contains(Search)) ||
                                      (((co.Customer_Name ?? "") + " " + (co.Customer_Surname ?? "")).Contains(Search)) ||
                                      ((co.Customer_Tel ?? "").Contains(Search)) ||
                                      ((co.Customer_Address ?? "").Contains(Search))
                                      select new
                                      {
                                          id = co.ID,
                                          customer_Code = co.Customer_Code,
                                          customer_Name = co.Customer_Name,
                                          customer_Tel = co.Customer_Tel,
                                          customer_Email = co.Customer_Email,
                                          customer_Address = co.Customer_Address,
                                          customer_Type = co.Customer_Type,
                                          status = DataCustomerRegister_Status.Where(x => x.FK_Customer_ID == co.ID && x.Is_Active == 1).OrderBy(x => x.Status).Select(x => x.Status).LastOrDefault() == null ? 0 : DataCustomer_Status.Where(x => x.FK_Customer_ID == co.ID && x.Is_Active == 1).OrderBy(x => x.Status).Select(x => x.Status).LastOrDefault(),
                                          is_Active = co.Is_Active,
                                          create_Date = co.Create_Date,
                                          is_Image = DataCustomerRegister_Status.Where(x => x.FK_Customer_ID == co.ID && x.Is_Active == 1 && x.Status == 3).FirstOrDefault() == null ? false : true,
                                      }).OrderByDescending(x => x.create_Date).Take(100).ToList();
                        #endregion

                        #region CustomerMember
                        var DataCustomerMember = _dbContext.CCC_Customer.Where(x => x.Flag_Member == 1);
                        var DataCustomerMember_Status = _dbContext.CCC_Customer_Status;
                        var CustomerMember = (from co in DataCustomerMember
                                      where ((co.Customer_Code ?? "").Contains(Search)) ||
                                      (((co.Customer_Name ?? "") + " " + (co.Customer_Surname ?? "")).Contains(Search)) ||
                                      ((co.Customer_Tel ?? "").Contains(Search)) ||
                                      ((co.Customer_Address ?? "").Contains(Search))
                                      select new
                                      {
                                          id = co.ID,
                                          customer_Code = co.Customer_Code,
                                          customer_Name = co.Customer_Name,
                                          customer_Tel = co.Customer_Tel,
                                          customer_Email = co.Customer_Email,
                                          customer_Address = co.Customer_Address,
                                          customer_Type = co.Customer_Type,
                                          status = DataCustomerMember_Status.Where(x => x.FK_Customer_ID == co.ID && x.Is_Active == 1).OrderBy(x => x.Status).Select(x => x.Status).LastOrDefault(),
                                          is_Active = co.Is_Active,
                                          create_Date = co.Create_Date,
                                          is_Image = DataCustomerMember_Status.Where(x => x.FK_Customer_ID == co.ID && x.Is_Active == 1 && x.Status == 3).FirstOrDefault() == null ? false : true,
                                      }).OrderByDescending(x => x.create_Date).Take(100).ToList();
                        #endregion

                        var ResponseData = new
                        {
                            Care = Care,
                            Content = Content,
                            Customer_Renew = Customer_Renew,
                            Installation_Model = Installation_Model,
                            Installation_Classified = Installation_Classified,
                            Menu = Menu,
                            Product = Product,
                            ServiceInformation = ServiceInformation,
                            SparePart_Model = SparePart_Model,
                            SparePart_Classified = SparePart_Classified,
                            Sparepart_And_Installation = Sparepart_And_Installation,
                            Warranty = Warranty,
                            CustomerRegister = CustomerRegister,
                            CustomerMember = CustomerMember,
                        };

                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetAllDataSettingEmailByCareCenterCode")]
        public async Task<IActionResult> GetAllDataSettingEmailByCareCenterCode(string CareCenterCode, string Search)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrWhiteSpace(CareCenterCode))
                    {
                        CultureInfo culture = new CultureInfo("pt-BR");
                        var DataSettingEmail = _dbContext.CCC_SettingEmail.Where(x => x.Is_Active == 1 && x.Care_Center_Code == CareCenterCode);
                        var DataEmployee = _dbContext.CCC_Employee;

                        var ResponseData = (from c in DataSettingEmail
                                            where c.Email.Contains(Search ?? "")
                                            select new
                                            {
                                                id = c.ID,
                                                c.Email,
                                                Update_Date = c.Update_Date == null ? "" : Convert.ToDateTime(c.Update_Date).ToString("d", culture),
                                                Update_By = DataEmployee.Where(x => x.Username == c.Update_By).Select(y => y.Employee_Name + " " + y.Employee_Surname).FirstOrDefault(),
                                            }).OrderByDescending(x => x.id).ToList();

                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("AddorDeleteDataSettingSendMail")]
        public async Task<IActionResult> AddorDeleteDataSettingSendMail(RequestSettingEmail input)
        {
            string msglog = "";
            try
            {
                //input.Type = 1 : Add, 2 : Del
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrWhiteSpace(input.Care_Center_Code))
                    {
                        if (input.Type == 1)
                        {
                            var delData = _dbContext.CCC_SettingEmail.Where(x => x.Is_Active == 1 && x.Care_Center_Code == input.Care_Center_Code && x.Email == input.Email).FirstOrDefault();
                            if (delData == null)
                            {
                                SettingEmail DataSettingEmail = new SettingEmail();
                                DataSettingEmail.Care_Center_Code = input.Care_Center_Code;
                                DataSettingEmail.Email = input.Email;
                                DataSettingEmail.Is_Active = 1;
                                DataSettingEmail.Create_By = input.Create_By;
                                DataSettingEmail.Create_Date = DateTime.Now;
                                DataSettingEmail.Update_By = input.Create_By;
                                DataSettingEmail.Update_Date = DateTime.Now;
                                _dbContext.CCC_SettingEmail.Add(DataSettingEmail);
                                _dbContext.SaveChanges();

                                return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                            }
                            else
                            {
                                return Ok(new ResponseModel { Message = "มีอีเมลนี้ในระบบแล้ว", Status = APIStatus.SystemError });
                            }
                        }
                        else
                        {
                            var delData = _dbContext.CCC_SettingEmail.Where(x => x.ID == input.ID).FirstOrDefault();
                            if(delData != null)
                            {
                                delData.Is_Active = 0;
                                delData.Update_By = input.Create_By;
                                delData.Update_Date = DateTime.Now;
                                _dbContext.CCC_SettingEmail.Update(delData);
                                _dbContext.SaveChanges();
                                return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                            }
                            return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
                        }
                    }
                    return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                try
                {
                    msglog += "Fail";
                    Log log = new Log();
                    log.Function = "SaveRequestCustomerRenew";
                    log.Message = ex.Message + ";" + msglog;
                    log.DateTime = DateTime.Now;
                    _dbContext.CCC_Log.Add(log);
                    _dbContext.SaveChanges();
                }
                catch
                {

                }
                throw ex;
            }
        }

    }
}
