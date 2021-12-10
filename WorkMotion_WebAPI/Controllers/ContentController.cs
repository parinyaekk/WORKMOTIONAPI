using WorkMotion_WebAPI.BaseModel;
using WorkMotion_WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static WorkMotion_WebAPI.Model.BaseModel;
using static WorkMotion_WebAPI.Model.Content_FileModel;
using static WorkMotion_WebAPI.Model.ContentDataModel;
using static WorkMotion_WebAPI.Model.ContentModel;
using static WorkMotion_WebAPI.Model.LogModel;
using static WorkMotion_WebAPI.Model.Receipt_FileModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        private IConfiguration _configuration;
        public ContentController(ASCCContext dbContext, IConfiguration iconfig)
        {
            _dbContext = dbContext;
            _configuration = iconfig;
        }

        [HttpPost("GetMenuAll")]
        public async Task<IActionResult> GetMenuAll(int? LangID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (LangID > 0)
                    {
                        var GetDataMainMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1 && x.Lang_ID == Convert.ToInt32(LangID)).ToList();
                        var GetDataMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1).ToList();
                        var GetDataMenuLink = _dbContext.CCC_Menu_Link.ToList();

                        var result = (from mm in GetDataMainMenu
                                      join m in GetDataMenu on mm.FK_Menu_ID equals m.ID into m2
                                      from m3 in m2.DefaultIfEmpty()
                                      join ml in GetDataMenuLink on mm.ID equals (LangID == 1 ? ml.FK_MENU_TH_ID : ml.FK_MENU_EN_ID) into ml2
                                      from ml3 in ml2.DefaultIfEmpty()
                                      select new
                                      {
                                          id_main_menu = mm.ID,
                                          id_menu = m3 == null ? 0 : m3.ID,
                                          mm.Lang_ID,
                                          ml3?.FK_MENU_EN_ID,
                                          ml3?.FK_MENU_TH_ID,
                                          menu = mm.Menu_Name,
                                          menu_link = mm.Menu_Link,
                                          main_menu = m3 == null ? null : m3.Menu_Name,
                                          hide_header = mm.Hide_Header,
                                          hide_Footer = mm.Hide_Footer,
                                          main_order = mm.Menu_Order,
                                          order = m3 == null ? null : m3.Menu_Order
                                          //main_menu = mm.Menu_Name,
                                          //menu = m3 == null ? null : m3.Menu_Name,
                                      }).OrderBy(x => x.main_order).ThenBy(x => x.order).ToList();

                        if (result != null)
                        {
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
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

        [HttpPost("GetMenuById")]
        public async Task<IActionResult> GetMenuById(int? id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id > 0)
                    {
                        var result = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1 && x.ID == Convert.ToInt32(id)).FirstOrDefault();

                        if (result != null)
                        {
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
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

        [HttpPost("GetMenuOptions")]
        public async Task<IActionResult> GetMenuOptions()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetDataMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1).ToList();

                    var result = (from m in GetDataMenu
                                    select new
                                    {
                                        value = m.ID,
                                        label = m.Menu_Name,
                                    }).ToList();

                    if (result != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
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

        [HttpPost("GetDataContent")]
        public async Task<IActionResult> GetDataContent(GetDataContentModel inputModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (inputModel.LangID > 0 && inputModel.ID > 0)
                    {
                        //var GetDataContentData = _dbContext.ContentData/*.Where(x => x.Is_Active == 1)*/.ToList();
                        var GetDataContent = _dbContext.CCC_Content.Where(x => x.Is_Active == 1).ToList();
                        //var GetDataContent_Head = _dbContext.Content_Head.Where(x => x.Is_Active == 1).ToList();
                        var GetDataMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1 && x.ID == Convert.ToInt32(inputModel.ID) && x.Lang_ID == Convert.ToInt32(inputModel.LangID)).ToList();
                        var GetDataContent_File = _dbContext.CCC_Content_File.Where(x => x.Is_Active == 1).ToList();

                        var result1 = (from menu in GetDataMenu
                                      join con in GetDataContent on menu.ID equals con.FK_Menu_ID //into con2
                                      //from con3 in con2.DefaultIfEmpty()
                                      select new
                                      {
                                          ID = con == null ? 0 : con.ID,
                                          lang = con == null ? 0 : con.Lang_ID,
                                          line_status = con.Line_Status,
                                          ContentTitle = con == null ? null : con.Content_Title,
                                          Detail = con == null ? null : con.Content_Body,
                                          Status = con == null ? null : con.Is_Active,
                                          menu_id = menu == null ? "" : Convert.ToString(menu.ID),
                                          menu = menu.Menu_Name,
                                          menu_link = menu.Menu_Link,
                                          con.Content_Title,
                                          con.Content_Desc,
                                          con.Content_Type,
                                          con.Content_Col,
                                          con.Content_Body,
                                          con.Content_Col1,
                                          con.Content_Col2,
                                          con.Content_Col3,
                                          con.Content_Col4,
                                          con.Content_Order,
                                      }).Distinct().ToList();

                        var result2 = (from condata in result1
                                            select new
                                            {
                                                ID = condata == null ? 0 : condata.ID,
                                                lang = condata == null ? 0 : condata.lang,
                                                line_status = condata.line_status,
                                                ContentTitle = condata == null ? null : condata.ContentTitle,
                                                Detail = condata == null ? null : condata.Detail,
                                                Status = condata == null ? null : condata.Status,
                                                condata.menu_id,
                                                menu = condata.menu,
                                                condata.Content_Title,
                                                condata.Content_Type,
                                                content_col = condata.Content_Col,
                                                content_body = condata.Content_Body,
                                                condata.Content_Col1,
                                                condata.Content_Col2,
                                                condata.Content_Col3,
                                                condata.Content_Col4,
                                                condata.Content_Desc,
                                                condata.Content_Order,
                                                file = (from f in GetDataContent_File.Where(x => x.FK_Content_ID == condata.ID).OrderBy(x => x.File_Order)
                                                        select new
                                                        {
                                                            id = f.ID,
                                                            name = f.File_Name,
                                                            type = f.File_Type,
                                                            path = f.File_Path,
                                                            link = f.Href_Link,
                                                            description = f.Description,
                                                            link_download = f.Link_Download,
                                                            file_order = f.File_Order,
                                                            flag_button = f.Flag_Button,
                                                            coverimage_path = f.CoverImage_Path,
                                                        }).ToList(),
                                                Image = GetDataContent_File.Where(x => x.FK_Content_ID == condata.ID).FirstOrDefault()?.File_Path,
                                                //file = (from f in GetDataContent_File.Where(x => x.FK_Content_ID == condata.ID)
                                                //        select new
                                                //        {
                                                //            id = f.ID,
                                                //            name = f.File_Name,
                                                //            type = f.File_Type,
                                                //            path = f.File_Path,
                                                //            link = f.Href_Link,
                                                //        }).FirstOrDefault(),
                                            }).ToList().OrderBy(x => x.Content_Order);

                        //var result = (from main in result2
                        //              select new
                        //              {
                        //                  ID = main == null ? 0 : main.ID,
                        //                  lang = main == null ? 0 : main.lang,
                        //                  Image = main.file == null ? null : main.file.path,
                        //                  ContentTitle = main == null ? null : main.ContentTitle,
                        //                  Detail = main == null ? null : main.Detail,
                        //                  Status = main == null ? null : main.Status,
                        //                  menu = main.menu,
                        //                  main.Content_Type,
                        //                  main.Content_Col1,
                        //                  main.Content_Col2,
                        //                  main.Content_Col3,
                        //                  main.Content_Col4,
                        //                  main.Content_Desc,
                        //                  main.Content_Order,
                        //              }).Distinct().ToList().OrderBy(x => x.ID);
                        if (result2 != null)
                        {
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result2 });
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

        [HttpPost("GetAllDataContentPaging")]
        public async Task<IActionResult> GetAllDataContentPaging(ContentPaginationModel input)
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

                    //var GetDataContentData = _dbContext.ContentData/*.Where(x => x.Is_Active == 1)*/.ToList();
                    var GetDataContent = _dbContext.CCC_Content.Where(x => x.Is_Active == 1 &&
                        (Convert.ToDateTime(x.Create_Date).Date >= StartDate || StartDate == null) &&
                        (Convert.ToDateTime(x.Create_Date).Date <= EndDate || EndDate == null)).ToList();
                    //var GetDataContent_Head = _dbContext.Content_Head.Where(x => x.Is_Active == 1).ToList();
                    var GetDataMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1 && x.ID == Convert.ToInt32(input.ID)).ToList();
                    var GetDataContent_File = _dbContext.CCC_Content_File.Where(x => x.Is_Active == 1).ToList();

                    var result2 = (from m in GetDataMenu
                                   join con1 in GetDataContent on m.ID equals con1.FK_Menu_ID into con2
                                   from con in con2.DefaultIfEmpty()
                                   where ((con?.Content_Title ?? "").Contains(input.Search)) || ((con?.Content_Body ?? "").Contains(input.Search))
                                   || ((m.Menu_Name ?? "").Contains(input.Search))
                                   select new
                                    {
                                        id = con == null ? 0 : con.ID,
                                        lang = con == null ? 0 : con.Lang_ID,
                                        content_title = con == null ? "" : con.Content_Title,
                                        content_detail = con == null ? "" : con.Content_Body,
                                        content_order = con == null ? 0 : con.Content_Order,
                                        Create_Date =  con == null ? DateTime.Now : con.Create_Date,
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
                                                }).FirstOrDefault(),
                                    }).OrderBy(x => x.content_order).ThenByDescending(x => x.Create_Date).ToList();
                    //
                    var ResponseData = new
                    {
                        result = result2.Skip(start).Take(end).ToList(),
                        Total = result2.Count()
                    };

                    if (result2 != null)
                    {
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

        [HttpPost("GetDataContentByID")]
        public async Task<IActionResult> GetDataContentByID(GetDataContentModel inputModel/*int ID*/)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (inputModel.ID > 0)
                    {
                        var GetDataContentData = _dbContext.CCC_Content.Where(x => x.Is_Active == 1).ToList();
                        var GetDataContent_File = _dbContext.CCC_Content_File.Where(x => x.Is_Active == 1).ToList();

                        var ResponseData = (from condata in GetDataContentData
                                            join menu in _dbContext.CCC_Menu on condata.FK_Menu_ID equals menu.ID into menu2
                                            from menu3 in menu2.DefaultIfEmpty()
                                            where condata.ID == inputModel.ID
                                            select new
                                            {
                                                id = condata.ID,
                                                lang_id = condata.Lang_ID,
                                                line_status = condata.Line_Status,
                                                menu_id = menu3 == null ? "" : Convert.ToString(menu3.ID),
                                                menu_name = menu3 == null ? "" : Convert.ToString(menu3.Menu_Name),
                                                menu_link= menu3 == null ? "" : Convert.ToString(menu3.Menu_Link),
                                                content_title = condata.Content_Title,
                                                content_desc = condata.Content_Desc,
                                                content_type = condata.Content_Type,
                                                content_col = condata.Content_Col,
                                                content_body = condata.Content_Body,
                                                content_col1 = condata.Content_Col1,
                                                content_col2 = condata.Content_Col2,
                                                content_col3 = condata.Content_Col3,
                                                content_col4 = condata.Content_Col4,
                                                content_col5 = condata.Content_Col5,
                                                content_col6 = condata.Content_Col6,
                                                content_order = condata.Content_Order,
                                                file = (from f in GetDataContent_File.Where(x => x.FK_Content_ID == condata.ID)
                                                        select new
                                                        {
                                                            id = f.ID,
                                                            name = f.File_Name,
                                                            type = f.File_Type,
                                                            path = f.File_Path,
                                                            link = f.Href_Link,
                                                            description = f.Description,
                                                            link_download = f.Link_Download,
                                                            file_order = f.File_Order,
                                                            flag_button = f.Flag_Button,
                                                            coverimage_path = f.CoverImage_Path,
                                                            f.Create_Date
                                                        }).OrderBy(x => x.Create_Date).ToList(),
                                            }).FirstOrDefault();

                        if (ResponseData != null)
                        {
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
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

        [HttpPost("DeleteContentById")]
        public async Task<IActionResult> DeleteContentById(GetDataContentModel inputModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _dbContext.CCC_Content.Where(x => x.Is_Active == 1 && x.ID == inputModel.ID).FirstOrDefault();
                    result.Is_Active = 0;
                    _dbContext.CCC_Content.Update(result);
                    _dbContext.SaveChanges();

                    if (result != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful});
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetAllDataContent")]
        public async Task<IActionResult> GetAllDataContent(GetDataContentModel inputModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (inputModel.ID > 0)
                    {
                        //var GetDataContentData = _dbContext.ContentData/*.Where(x => x.Is_Active == 1)*/.ToList();
                        var GetDataContent = _dbContext.CCC_Content.Where(x => x.Is_Active == 1).ToList();
                        //var GetDataContent_Head = _dbContext.Content_Head.Where(x => x.Is_Active == 1).ToList();
                        var GetDataMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1 && x.ID == Convert.ToInt32(inputModel.ID)).ToList();
                        var GetDataContent_File = _dbContext.CCC_Content_File.Where(x => x.Is_Active == 1).ToList();

                        var result1 = (from menu in GetDataMenu
                                       join con in GetDataContent on menu.ID equals con.FK_Menu_ID //into con2
                                                                                                   //from con3 in con2.DefaultIfEmpty()
                                       select new
                                       {
                                           ID = con == null ? 0 : con.ID,
                                           lang = con == null ? 0 : con.Lang_ID,
                                           line_status = con.Line_Status,
                                           ContentTitle = con == null ? null : con.Content_Title,
                                           Detail = con == null ? null : con.Content_Body,
                                           Status = con == null ? null : con.Is_Active,
                                           menu_id = menu == null ? "" : Convert.ToString(menu.ID),
                                           menu = menu.Menu_Name,
                                           con.Content_Title,
                                           con.Content_Desc,
                                           con.Content_Type,
                                           con.Content_Col,
                                           con.Content_Body,
                                           con.Content_Col1,
                                           con.Content_Col2,
                                           con.Content_Col3,
                                           con.Content_Col4,
                                           con.Content_Col5,
                                           con.Content_Col6,
                                           con.Content_Order,
                                           con.Is_Active
                                       }).Distinct().ToList();

                        var result2 = (from condata in result1
                                       select new
                                       {
                                           ID = condata == null ? 0 : condata.ID,
                                           lang = condata == null ? 0 : condata.lang,
                                           line_status = condata.line_status,
                                           ContentTitle = condata == null ? null : condata.ContentTitle,
                                           Detail = condata == null ? null : condata.Detail,
                                           Status = condata == null ? null : condata.Status,
                                           condata.menu_id,
                                           menu = condata.menu,
                                           condata.Content_Title,
                                           condata.Content_Type,
                                           content_col = condata.Content_Col,
                                           content_body = condata.Content_Body,
                                           condata.Content_Col1,
                                           condata.Content_Col2,
                                           condata.Content_Col3,
                                           condata.Content_Col4,
                                           condata.Content_Col5,
                                           condata.Content_Col6,
                                           condata.Content_Desc,
                                           condata.Content_Order,
                                           condata.Is_Active,
                                           file = (from f in GetDataContent_File.Where(x => x.FK_Content_ID == condata.ID)
                                                   select new
                                                   {
                                                       id = f.ID,
                                                       name = f.File_Name,
                                                       type = f.File_Type,
                                                       path = f.File_Path,
                                                       link = f.Href_Link,
                                                       description = f.Description,
                                                       link_download = f.Link_Download,
                                                       file_order = f.File_Order,
                                                       flag_button = f.Flag_Button,
                                                       coverimage_path = f.CoverImage_Path,
                                                   }).ToList(),
                                           Image = GetDataContent_File.Where(x => x.FK_Content_ID == condata.ID).FirstOrDefault()?.File_Path,
                                       }).OrderBy(x => x.Content_Order).ToList();

                        if (result2 != null)
                        {
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result2 });
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

        [HttpPost("GetaAllDataMenu")]
        public async Task<IActionResult> GetaAllDataMenu()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetDataMainMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1).ToList();
                    var GetDataMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1).ToList();

                    var result = (from mm in GetDataMainMenu
                                  join m in GetDataMenu on mm.FK_Menu_ID equals m.ID into m2
                                  from m3 in m2.DefaultIfEmpty()
                                  select new
                                  {
                                      id_main_menu = mm.ID,
                                      id_menu = m3 == null ? 0 : m3.ID,
                                      menu_id = m3 == null ? 0 : m3.ID,
                                      menu_link = mm.Menu_Link,
                                      mm.Lang_ID,
                                      menu = mm.Menu_Name,
                                      main_menu = m3 == null ? null : m3.Menu_Name,

                                  }).ToList().OrderBy(x => x.id_main_menu);

                    if (result != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetaAllDataMenuPagination")]
        public async Task<IActionResult> GetaAllDataMenuPagination(ContentPaginationModel input)
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
                    var GetDataMainMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1 &&
                                            (StartDate == null ? true : x.Create_Date == null ? true : Convert.ToDateTime(x.Create_Date).Date >= StartDate) &&
                                            (EndDate == null ? true : x.Create_Date == null ? true : Convert.ToDateTime(x.Create_Date).Date <= EndDate));

                    var GetDataMenu = _dbContext.CCC_Menu.Where(x => x.Is_Active == 1).ToList();

                    //var allpage = (from mm in GetDataMainMenu
                    //              join m in GetDataMenu on mm.FK_Menu_ID equals m.ID into m2
                    //              from m3 in m2.DefaultIfEmpty()
                    //              select new
                    //              {
                    //                  id_main_menu = mm.ID,
                    //                  id_menu = m3 == null ? 0 : m3.ID,
                    //                  mm.Lang_ID,
                    //                  menu = mm.Menu_Name,
                    //                  main_menu = m3 == null ? null : m3.Menu_Name,

                    //              }).ToList().Count();

                    var TempData = (from mm in GetDataMainMenu
                                  join m in GetDataMenu on mm.FK_Menu_ID equals m.ID into m2
                                  from m3 in m2.DefaultIfEmpty()
                                  select new
                                  {
                                      id_main_menu = mm.ID,
                                      id_menu = m3 == null ? 0 : m3.ID,
                                      mm.Lang_ID,
                                      lang = mm.Lang_ID == 1 ? "ภาษาไทย" : "ภาษาอังกฤษ",
                                      menu = mm.Menu_Name,
                                      main_menu = m3 == null ? null : m3.Menu_Name,
                                      menu_link = mm.Menu_Link
                                  });

                    TempData = TempData.Where(x => ((x.menu ?? "").Contains(input.Search)) ||
                               ((x.lang ?? "").Contains(input.Search)) ||
                               ((x.main_menu ?? "").Contains(input.Search)));

                    var result = TempData.OrderBy(x => x.id_main_menu).Skip(start).Take(end).ToList();

                    var ResponseData = new
                    {
                        result,
                        Total = TempData.Count()
                    };

                    if (result != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("AddDataContent")]
        public async Task<IActionResult> AddDataContent(InputContent inputdata)
        {
            string msglog = "";
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        msglog += "Start";
                        var ContentBodyData = inputdata.Content_Body?.ToString().Replace("\n", "<br>");
                        var ContentDescription = inputdata.Content_Desc?.ToString().Replace("\n", "<br>");
                        var ContentTitle = inputdata.Content_Title?.ToString().Replace("\n", "<br>");
                        if (inputdata.Content_ID == null)
                        {
                            msglog += "NoContent_IDCase";
                            Content condata = new Content();
                            condata.FK_Menu_ID = Convert.ToInt32(inputdata.Menu);
                            condata.Line_Status = Convert.ToInt32(inputdata.Line_Status);
                            condata.Lang_ID = Convert.ToInt32(inputdata.LangID);
                            condata.Content_Title = ContentTitle;
                            //condata.Content_Title = Convert.ToString(inputdata.Content_Title);
                            condata.Content_Desc = ContentDescription;
                            //condata.Content_Desc = Convert.ToString(inputdata.Content_Desc);                        
                            condata.Content_Body = ContentBodyData;
                            //condata.Content_Body = Convert.ToString(inputdata.Content_Body);
                            condata.Content_Col1 = Convert.ToString(inputdata.Content_Col1);
                            condata.Content_Col2 = Convert.ToString(inputdata.Content_Col2);
                            condata.Content_Col3 = Convert.ToString(inputdata.Content_Col3);
                            condata.Content_Col4 = Convert.ToString(inputdata.Content_Col4);
                            condata.Content_Col5 = Convert.ToString(inputdata.Content_Col5);
                            condata.Content_Col6 = Convert.ToString(inputdata.Content_Col6);
                            condata.Content_Type = Convert.ToInt32(inputdata.Content_Type);
                            condata.Content_Col = Convert.ToInt32(inputdata.Content_Col);
                            condata.Content_Order = Convert.ToInt32(inputdata.Content_Order);
                            condata.Is_Active = 1;
                            condata.Create_By = "Admin";
                            condata.Create_Date = DateTime.Now;
                            _dbContext.CCC_Content.Add(condata);
                            _dbContext.SaveChanges();
                            msglog += "AddDatacontentSuccess";

                            if (inputdata.File != null && inputdata.File.Count() > 0)
                            {
                                for (int i = 0; i < inputdata.File.Count(); i++)
                                {
                                    int id = Convert.ToInt32(inputdata.File[i].id);
                                    var ModelFile = _dbContext.CCC_Content_File.Where(x => x.Is_Active == 1 && x.ID == id).FirstOrDefault();
                                    ModelFile.FK_Content_ID = condata.ID;
                                    _dbContext.CCC_Content_File.Update(ModelFile);
                                    _dbContext.SaveChanges();
                                    msglog += "Addfilecontentsuccess";
                                }
                            }
                            msglog += "AddcontentSuccess";
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                        }
                        else
                        {
                            msglog += "haveContent_ID";
                            var condata = _dbContext.CCC_Content.Where(x => x.Is_Active == 1 && x.ID == Convert.ToInt32(inputdata.Content_ID)).FirstOrDefault();
                            condata.FK_Menu_ID = Convert.ToInt32(inputdata.Menu);
                            condata.Line_Status = Convert.ToInt32(inputdata.Line_Status);
                            condata.Lang_ID = Convert.ToInt32(inputdata.LangID);
                            condata.Content_Title = ContentTitle;
                            //condata.Content_Title = Convert.ToString(inputdata.Content_Title);
                            //condata.Content_Desc = Convert.ToString(inputdata.Content_Desc);
                            condata.Content_Desc = ContentDescription;
                            condata.Content_Body = ContentBodyData;
                            //GetDataContentData.Content_Body = Convert.ToString(inputdata.Content_Body);
                            condata.Content_Col1 = Convert.ToString(inputdata.Content_Col1);
                            condata.Content_Col2 = Convert.ToString(inputdata.Content_Col2);
                            condata.Content_Col3 = Convert.ToString(inputdata.Content_Col3);
                            condata.Content_Col4 = Convert.ToString(inputdata.Content_Col4);
                            condata.Content_Col5 = Convert.ToString(inputdata.Content_Col5);
                            condata.Content_Col6 = Convert.ToString(inputdata.Content_Col6);
                            condata.Content_Type = Convert.ToInt32(inputdata.Content_Type);
                            condata.Content_Col = Convert.ToInt32(inputdata.Content_Col);
                            condata.Content_Order = Convert.ToInt32(inputdata.Content_Order);
                            condata.Is_Active = 1;
                            condata.Update_By = "Admin";
                            condata.Update_Date = DateTime.Now;
                            _dbContext.CCC_Content.Update(condata);
                            _dbContext.SaveChanges();
                            msglog += "AddcontentSuccess";

                            var ModelFileClose = _dbContext.CCC_Content_File.Where(x => x.Is_Active == 1 && x.FK_Content_ID == inputdata.Content_ID).ToList();
                            foreach (var item in ModelFileClose)
                            {
                                item.Is_Active = 0;
                                item.Update_By = "Admin";
                                item.Update_Date = DateTime.Now;
                                _dbContext.CCC_Content_File.Update(item);
                                _dbContext.SaveChanges();
                            }

                            if (inputdata.File.Count() > 0 && inputdata.File != null)
                            {
                                for (int i = 0; i < inputdata.File.Count(); i++)
                                {
                                    int id = Convert.ToInt32(inputdata.File[i].id);
                                    var ModelFile = _dbContext.CCC_Content_File.Where(x => x.ID == id).FirstOrDefault();
                                    ModelFile.FK_Content_ID = condata.ID;
                                    ModelFile.Is_Active = 1;
                                    _dbContext.CCC_Content_File.Update(ModelFile);
                                    _dbContext.SaveChanges();
                                    msglog += "AddcontentfileSuccess";
                                }
                            }
                            try
                            {
                                msglog += "Success";
                                Log log = new Log();
                                log.Function = "AddDataContent";
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
                    catch (Exception ex)
                    {
                        msglog += "catch" + ex;
                        throw ex;
                    }
                }
                try
                {
                    msglog += "Fail";
                    Log log = new Log();
                    log.Function = "AddDataContent";
                    log.Message = msglog;
                    log.DateTime = DateTime.Now;
                    _dbContext.CCC_Log.Add(log);
                    _dbContext.SaveChanges();
                }
                catch
                {

                }
                return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
            }
            catch (Exception ex)
            {
                msglog += "catch" + ex;

                try
                {
                    Log log = new Log();
                    log.Function = "AddDataContent";
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

        [HttpPost("UploadFile"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(/*IFormFile File*/)
        {
            string msglog = "";
            try
            {
                string id = Convert.ToString(Request.Form["id"]);
                string HrefLink = Convert.ToString(Request.Form["HrefLink"]);
                string Description_Image = Convert.ToString(Request.Form["Description_Image"]);
                string Link_Download = Convert.ToString(Request.Form["Link_Download"]);
                string Order = Convert.ToString(Request.Form["Order"]);
                string Status_Image = Convert.ToString(Request.Form["Status_Image"]);
                //var file = Request.Form.Files[0];
                List<IFormFile> file = new List<IFormFile>();
                foreach (var item in Request.Form.Files)
                {
                    file.Add(item);
                }
                //var file = File;
                var folderName = Path.Combine("Resources", "File");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (id != null && id != "null") //Edit Save
                {
                    if (file.Count() == 0)
                    {
                        var DataFile = _dbContext.CCC_Content_File.Where(x => x.ID == Convert.ToInt32(id)).FirstOrDefault();
                        DataFile.Href_Link = Convert.ToString(HrefLink == "null" || HrefLink == "undefined" ? null : HrefLink);
                        DataFile.Description = Convert.ToString(Description_Image == "null" || Description_Image == "undefined" ? null : Description_Image);
                        DataFile.Link_Download = Convert.ToString(Link_Download == "null" || Link_Download == "undefined" ? null : Link_Download);
                        DataFile.File_Order = Convert.ToInt32(Order == "null" || Order == "undefined" ? "0" : Order);
                        DataFile.Flag_Button = Convert.ToInt32(Status_Image == "null" || Status_Image == "undefined" ? "0" : Status_Image);
                        _dbContext.CCC_Content_File.Update(DataFile);
                        _dbContext.SaveChanges();

                        ResponseContent_File ResponseData = new ResponseContent_File();
                        ResponseData.id = DataFile.ID;
                        ResponseData.file_path = DataFile.File_Path;
                        ResponseData.file_name = DataFile.File_Name;
                        ResponseData.file_type = DataFile.File_Type;
                        ResponseData.link = DataFile.Href_Link;
                        ResponseData.description = DataFile.Description;
                        ResponseData.link_download = DataFile.Link_Download;
                        ResponseData.flag_button = DataFile.Flag_Button;
                        ResponseData.file_order = DataFile.File_Order;
                        ResponseData.coverimage_path = DataFile.CoverImage_Path;
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
                    }
                    else if (file.Count() == 1)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                        fileName = string.Concat(
                            Path.GetFileNameWithoutExtension(fileName),
                            string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now),
                            Path.GetExtension(fileName)
                        );
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        // Determine whether the directory exists.
                        if (!Directory.Exists(pathToSave))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(pathToSave);
                        }

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file[0].CopyTo(stream);
                        }

                        if (file[0].Name == "CoverImage")
                        {
                            var DataFile = _dbContext.CCC_Content_File.Where(x => x.ID == Convert.ToInt32(id)).FirstOrDefault();
                            DataFile.CoverImage_Path = dbPath;
                            _dbContext.CCC_Content_File.Update(DataFile);
                            _dbContext.SaveChanges();

                            ResponseContent_File ResponseData = new ResponseContent_File();
                            ResponseData.id = DataFile.ID;
                            ResponseData.file_path = DataFile.File_Path;
                            ResponseData.file_name = DataFile.File_Name;
                            ResponseData.file_type = DataFile.File_Type;
                            ResponseData.link = DataFile.Href_Link;
                            ResponseData.description = DataFile.Description;
                            ResponseData.link_download = DataFile.Link_Download;
                            ResponseData.flag_button = DataFile.Flag_Button;
                            ResponseData.file_order = DataFile.File_Order;
                            ResponseData.coverimage_path = DataFile.CoverImage_Path;
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
                        }
                        else if (file[0].Name == "files")
                        {
                            var DataFile = _dbContext.CCC_Content_File.Where(x => x.ID == Convert.ToInt32(id)).FirstOrDefault();
                            DataFile.File_Path = dbPath;
                            DataFile.File_Type = file[0].ContentType;
                            DataFile.File_Name = file[0].FileName;
                            DataFile.Href_Link = Convert.ToString(HrefLink == "null" || HrefLink == "undefined" ? null : HrefLink);
                            DataFile.Description = Convert.ToString(Description_Image == "null" || Description_Image == "undefined" ? null : Description_Image);
                            DataFile.Link_Download = Convert.ToString(Link_Download == "null" || Link_Download == "undefined" ? null : Link_Download);
                            DataFile.File_Order = Convert.ToInt32(Order == "null" || Order == "undefined" ? "0" : Order);
                            DataFile.Flag_Button = Convert.ToInt32(Status_Image == "null" || Status_Image == "undefined" ? "0" : Status_Image);
                            _dbContext.CCC_Content_File.Update(DataFile);
                            _dbContext.SaveChanges();

                            ResponseContent_File ResponseData = new ResponseContent_File();
                            ResponseData.id = DataFile.ID;
                            ResponseData.file_path = DataFile.File_Path;
                            ResponseData.file_name = DataFile.File_Name;
                            ResponseData.file_type = DataFile.File_Type;
                            ResponseData.link = DataFile.Href_Link;
                            ResponseData.description = DataFile.Description;
                            ResponseData.link_download = DataFile.Link_Download;
                            ResponseData.flag_button = DataFile.Flag_Button;
                            ResponseData.file_order = DataFile.File_Order;
                            ResponseData.coverimage_path = DataFile.CoverImage_Path;
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
                        }
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    else if (file.Count() == 2)
                    {
                        ResponseContent_File ResponseData = new ResponseContent_File();
                        foreach (var item in file)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                            fileName = string.Concat(
                                Path.GetFileNameWithoutExtension(fileName),
                                string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now),
                                Path.GetExtension(fileName)
                            );
                            var fullPath = Path.Combine(pathToSave, fileName);
                            var dbPath = Path.Combine(folderName, fileName);
                            // Determine whether the directory exists.
                            if (!Directory.Exists(pathToSave))
                            {
                                // Try to create the directory.
                                DirectoryInfo di = Directory.CreateDirectory(pathToSave);
                            }

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);
                            }

                            if (item.Name == "CoverImage")
                            {
                                ResponseData.coverimage_path = dbPath;
                            }
                            else if (item.Name == "files")
                            {
                                ResponseData.file_path = dbPath;
                                ResponseData.file_type = item.ContentType;
                                ResponseData.file_name = item.FileName;
                                ResponseData.link = Convert.ToString(HrefLink == "null" || HrefLink == "undefined" ? null : HrefLink);
                                ResponseData.description = Convert.ToString(Description_Image == "null" || Description_Image == "undefined" ? null : Description_Image);
                                ResponseData.link_download = Convert.ToString(Link_Download == "null" || Link_Download == "undefined" ? null : Link_Download);
                                ResponseData.file_order = Convert.ToInt32(Order == "null" || Order == "undefined" ? "0" : Order);
                                ResponseData.flag_button = Convert.ToInt32(Status_Image == "null" || Status_Image == "undefined" ? "0" : Status_Image);
                            }
                        }
                        Content_File ModelFile = new Content_File();
                        ModelFile.File_Type = Convert.ToString(ResponseData.file_type);
                        ModelFile.File_Name = Convert.ToString(ResponseData.file_name);
                        ModelFile.Href_Link = Convert.ToString(ResponseData.link);
                        ModelFile.Description = Convert.ToString(ResponseData.description);
                        ModelFile.Link_Download = Convert.ToString(ResponseData.link_download);
                        ModelFile.File_Order = Convert.ToInt32(ResponseData.file_order);
                        ModelFile.Flag_Button = Convert.ToInt32(ResponseData.flag_button);
                        ModelFile.File_Path = Convert.ToString(ResponseData.file_path);
                        ModelFile.CoverImage_Path = Convert.ToString(ResponseData.coverimage_path);
                        ModelFile.Is_Active = 1;
                        ModelFile.Create_By = "Admin";
                        ModelFile.Create_Date = DateTime.Now;
                        _dbContext.CCC_Content_File.Add(ModelFile);
                        _dbContext.SaveChanges();

                        ResponseData.id = ModelFile.ID;

                        if (ResponseData != null)
                        {
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
                        }
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                }
                else //New Save
                {
                    if (file.Count() > 0)
                    {

                        ResponseContent_File ResponseData = new ResponseContent_File();

                        foreach (var item in file)
                        {
                            //string user = _configuration.GetValue<string>("userftp"), pass = _configuration.GetValue<string>("passftp"), ftpport = _configuration.GetValue<string>("ftp");
                            //var fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                            //fileName = string.Concat(
                            //    Path.GetFileNameWithoutExtension(fileName),
                            //    string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now),
                            //    Path.GetExtension(fileName)
                            //);

                            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpport + fileName);
                            //request.Credentials = new NetworkCredential(user, pass);
                            //request.Method = WebRequestMethods.Ftp.UploadFile;

                            //using (Stream ftpStream = request.GetRequestStream())
                            //{
                            //    item.CopyTo(ftpStream);
                            //}

                            var fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                            fileName = string.Concat(
                                Path.GetFileNameWithoutExtension(fileName),
                                string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now),
                                Path.GetExtension(fileName)
                            );
                            var fullPath = Path.Combine(pathToSave, fileName);
                            var dbPath = Path.Combine(folderName, fileName);
                            // Determine whether the directory exists.
                            if (!Directory.Exists(pathToSave))
                            {
                                // Try to create the directory.
                                DirectoryInfo di = Directory.CreateDirectory(pathToSave);
                            }

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);
                            }

                            if (item.Name == "CoverImage")
                            {
                                ResponseData.coverimage_path = dbPath;
                            }
                            else if (item.Name == "files")
                            {
                                ResponseData.file_path = dbPath;
                                ResponseData.file_type = item.ContentType;
                                ResponseData.file_name = item.FileName;
                                ResponseData.link = Convert.ToString(HrefLink == "null" || HrefLink == "undefined" ? null : HrefLink);
                                ResponseData.description = Convert.ToString(Description_Image == "null" || Description_Image == "undefined" ? null : Description_Image);
                                ResponseData.link_download = Convert.ToString(Link_Download == "null" || Link_Download == "undefined" ? null : Link_Download);
                                ResponseData.file_order = Convert.ToInt32(Order == "null" || Order == "undefined" ? "0" : Order);
                                ResponseData.flag_button = Convert.ToInt32(Status_Image == "null" || Status_Image == "undefined" ? "0" : Status_Image);
                            }
                        }

                        Content_File ModelFile = new Content_File();
                        ModelFile.File_Type = Convert.ToString(ResponseData.file_type);
                        ModelFile.File_Name = Convert.ToString(ResponseData.file_name);
                        ModelFile.Href_Link = Convert.ToString(ResponseData.link);
                        ModelFile.Description = Convert.ToString(ResponseData.description);
                        ModelFile.Link_Download = Convert.ToString(ResponseData.link_download);
                        ModelFile.File_Order = Convert.ToInt32(ResponseData.file_order);
                        ModelFile.Flag_Button = Convert.ToInt32(ResponseData.flag_button);
                        ModelFile.File_Path = Convert.ToString(ResponseData.file_path);
                        ModelFile.CoverImage_Path = Convert.ToString(ResponseData.coverimage_path);
                        ModelFile.Is_Active = 1;
                        ModelFile.Create_By = "Admin";
                        ModelFile.Create_Date = DateTime.Now;
                        _dbContext.CCC_Content_File.Add(ModelFile);
                        _dbContext.SaveChanges();

                        ResponseData.id = ModelFile.ID;

                        if (ResponseData != null)
                        {
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
                        }
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        //[HttpPost("UploadFile"), DisableRequestSizeLimit]
        //public async Task<IActionResult> UploadFile(/*IFormFile File*/)
        //{
        //    try
        //    {
        //        string id = Convert.ToString(Request.Form["id"]);
        //        string HrefLink = Convert.ToString(Request.Form["HrefLink"]);
        //        string Description_Image = Convert.ToString(Request.Form["Description_Image"]);
        //        string Link_Download = Convert.ToString(Request.Form["Link_Download"]);
        //        string Order = Convert.ToString(Request.Form["Order"]);
        //        string Status_Image = Convert.ToString(Request.Form["Status_Image"]);
        //        //var file = Request.Form.Files[0];
        //        List<IFormFile> file = new List<IFormFile>();
        //        foreach (var item in Request.Form.Files)
        //        {
        //            file.Add(item);
        //        }
        //        //var file = File;
        //        //var folderName = Path.Combine("Resources", "File");
        //        //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //        if (id != null && id != "null") //Edit Save
        //        {
        //            if (file.Count() == 0)
        //            {
        //                var DataFile = _dbContext.Content_File.Where(x => x.ID == Convert.ToInt32(id)).FirstOrDefault();
        //                DataFile.Href_Link = Convert.ToString(HrefLink == "null" || HrefLink == "undefined" ? null : HrefLink);
        //                DataFile.Description = Convert.ToString(Description_Image == "null" || Description_Image == "undefined" ? null : Description_Image);
        //                DataFile.Link_Download = Convert.ToString(Link_Download == "null" || Link_Download == "undefined" ? null : Link_Download);
        //                DataFile.File_Order = Convert.ToInt32(Order == "null" || Order == "undefined" ? "0" : Order);
        //                DataFile.Flag_Button = Convert.ToInt32(Status_Image == "null" || Status_Image == "undefined" ? "0" : Status_Image);
        //                _dbContext.Content_File.Update(DataFile);
        //                _dbContext.SaveChanges();

        //                ResponseContent_File ResponseData = new ResponseContent_File();
        //                ResponseData.id = DataFile.ID;
        //                ResponseData.file_path = DataFile.File_Path;
        //                ResponseData.file_name = DataFile.File_Name;
        //                ResponseData.file_type = DataFile.File_Type;
        //                ResponseData.link = DataFile.Href_Link;
        //                ResponseData.description = DataFile.Description;
        //                ResponseData.link_download = DataFile.Link_Download;
        //                ResponseData.flag_button = DataFile.Flag_Button;
        //                ResponseData.file_order = DataFile.File_Order;
        //                ResponseData.coverimage_path = DataFile.CoverImage_Path;
        //                return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
        //            }
        //            else if (file.Count() == 1)
        //            {
        //                string user = _configuration.GetValue<string>("userftp"), pass = _configuration.GetValue<string>("passftp"), ftpport = _configuration.GetValue<string>("ftp");
        //                var fileName = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
        //                fileName = string.Concat(
        //                    Path.GetFileNameWithoutExtension(fileName),
        //                    string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now),
        //                    Path.GetExtension(fileName)
        //                );

        //                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpport + fileName);
        //                request.Credentials = new NetworkCredential(user, pass);
        //                request.Method = WebRequestMethods.Ftp.UploadFile;

        //                using (Stream ftpStream = request.GetRequestStream())
        //                {
        //                    file[0].CopyTo(ftpStream);
        //                }
        //                var dbPath = ftpport + fileName;

        //                if (file[0].Name == "CoverImage")
        //                {
        //                    var DataFile = _dbContext.Content_File.Where(x => x.ID == Convert.ToInt32(id)).FirstOrDefault();
        //                    DataFile.CoverImage_Path = dbPath;
        //                    _dbContext.Content_File.Update(DataFile);
        //                    _dbContext.SaveChanges();

        //                    ResponseContent_File ResponseData = new ResponseContent_File();
        //                    ResponseData.id = DataFile.ID;
        //                    ResponseData.file_path = DataFile.File_Path;
        //                    ResponseData.file_name = DataFile.File_Name;
        //                    ResponseData.file_type = DataFile.File_Type;
        //                    ResponseData.link = DataFile.Href_Link;
        //                    ResponseData.description = DataFile.Description;
        //                    ResponseData.link_download = DataFile.Link_Download;
        //                    ResponseData.flag_button = DataFile.Flag_Button;
        //                    ResponseData.file_order = DataFile.File_Order;
        //                    ResponseData.coverimage_path = DataFile.CoverImage_Path;
        //                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
        //                }
        //                else if (file[0].Name == "files")
        //                {
        //                    var DataFile = _dbContext.Content_File.Where(x => x.ID == Convert.ToInt32(id)).FirstOrDefault();
        //                    DataFile.File_Path = dbPath;
        //                    DataFile.File_Type = file[0].ContentType;
        //                    DataFile.File_Name = file[0].FileName;
        //                    DataFile.Href_Link = Convert.ToString(HrefLink == "null" || HrefLink == "undefined" ? null : HrefLink);
        //                    DataFile.Description = Convert.ToString(Description_Image == "null" || Description_Image == "undefined" ? null : Description_Image);
        //                    DataFile.Link_Download = Convert.ToString(Link_Download == "null" || Link_Download == "undefined" ? null : Link_Download);
        //                    DataFile.File_Order = Convert.ToInt32(Order == "null" || Order == "undefined" ? "0" : Order);
        //                    DataFile.Flag_Button = Convert.ToInt32(Status_Image == "null" || Status_Image == "undefined" ? "0" : Status_Image);
        //                    _dbContext.Content_File.Update(DataFile);
        //                    _dbContext.SaveChanges();

        //                    ResponseContent_File ResponseData = new ResponseContent_File();
        //                    ResponseData.id = DataFile.ID;
        //                    ResponseData.file_path = DataFile.File_Path;
        //                    ResponseData.file_name = DataFile.File_Name;
        //                    ResponseData.file_type = DataFile.File_Type;
        //                    ResponseData.link = DataFile.Href_Link;
        //                    ResponseData.description = DataFile.Description;
        //                    ResponseData.link_download = DataFile.Link_Download;
        //                    ResponseData.flag_button = DataFile.Flag_Button;
        //                    ResponseData.file_order = DataFile.File_Order;
        //                    ResponseData.coverimage_path = DataFile.CoverImage_Path;
        //                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
        //                }
        //                return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
        //            }
        //            else if (file.Count() == 2)
        //            {
        //                ResponseContent_File ResponseData = new ResponseContent_File();
        //                foreach (var item in file)
        //                {
        //                    string user = _configuration.GetValue<string>("userftp"), pass = _configuration.GetValue<string>("passftp"), ftpport = _configuration.GetValue<string>("ftp");
        //                    var fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
        //                    fileName = string.Concat(
        //                        Path.GetFileNameWithoutExtension(fileName),
        //                        string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now),
        //                        Path.GetExtension(fileName)
        //                    );

        //                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpport + fileName);
        //                    request.Credentials = new NetworkCredential(user, pass);
        //                    request.Method = WebRequestMethods.Ftp.UploadFile;

        //                    using (Stream ftpStream = request.GetRequestStream())
        //                    {
        //                        item.CopyTo(ftpStream);
        //                    }
        //                    var dbPath = ftpport + fileName;

        //                    if (item.Name == "CoverImage")
        //                    {
        //                        ResponseData.coverimage_path = dbPath;
        //                    }
        //                    else if (item.Name == "files")
        //                    {
        //                        ResponseData.file_path = dbPath;
        //                        ResponseData.file_type = item.ContentType;
        //                        ResponseData.file_name = item.FileName;
        //                        ResponseData.link = Convert.ToString(HrefLink == "null" || HrefLink == "undefined" ? null : HrefLink);
        //                        ResponseData.description = Convert.ToString(Description_Image == "null" || Description_Image == "undefined" ? null : Description_Image);
        //                        ResponseData.link_download = Convert.ToString(Link_Download == "null" || Link_Download == "undefined" ? null : Link_Download);
        //                        ResponseData.file_order = Convert.ToInt32(Order == "null" || Order == "undefined" ? "0" : Order);
        //                        ResponseData.flag_button = Convert.ToInt32(Status_Image == "null" || Status_Image == "undefined" ? "0" : Status_Image);
        //                    }
        //                }
        //                Content_File ModelFile = new Content_File();
        //                ModelFile.File_Type = Convert.ToString(ResponseData.file_type);
        //                ModelFile.File_Name = Convert.ToString(ResponseData.file_name);
        //                ModelFile.Href_Link = Convert.ToString(ResponseData.link);
        //                ModelFile.Description = Convert.ToString(ResponseData.description);
        //                ModelFile.Link_Download = Convert.ToString(ResponseData.link_download);
        //                ModelFile.File_Order = Convert.ToInt32(ResponseData.file_order);
        //                ModelFile.Flag_Button = Convert.ToInt32(ResponseData.flag_button);
        //                ModelFile.File_Path = Convert.ToString(ResponseData.file_path);
        //                ModelFile.CoverImage_Path = Convert.ToString(ResponseData.coverimage_path);
        //                ModelFile.Is_Active = 1;
        //                ModelFile.Create_By = "Admin";
        //                ModelFile.Create_Date = DateTime.Now;
        //                _dbContext.Content_File.Add(ModelFile);
        //                _dbContext.SaveChanges();

        //                ResponseData.id = ModelFile.ID;

        //                if (ResponseData != null)
        //                {
        //                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
        //                }
        //                return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
        //            }
        //            return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
        //        }
        //        else //New Save
        //        {
        //            if (file.Count() > 0)
        //            {

        //                ResponseContent_File ResponseData = new ResponseContent_File();

        //                foreach (var item in file)
        //                {
        //                    string user = _configuration.GetValue<string>("userftp"), pass = _configuration.GetValue<string>("passftp"), ftpport = _configuration.GetValue<string>("ftp");
        //                    var fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
        //                    fileName = string.Concat(
        //                        Path.GetFileNameWithoutExtension(fileName),
        //                        string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now),
        //                        Path.GetExtension(fileName)
        //                    );

        //                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpport + fileName);
        //                    request.Credentials = new NetworkCredential(user, pass);
        //                    request.Method = WebRequestMethods.Ftp.UploadFile;

        //                    using (Stream ftpStream = request.GetRequestStream())
        //                    {
        //                        item.CopyTo(ftpStream);
        //                    }
        //                    var dbPath = ftpport + fileName;

        //                    if (item.Name == "CoverImage")
        //                    {
        //                        ResponseData.coverimage_path = dbPath;
        //                    }
        //                    else if (item.Name == "files")
        //                    {
        //                        ResponseData.file_path = dbPath;
        //                        ResponseData.file_type = item.ContentType;
        //                        ResponseData.file_name = item.FileName;
        //                        ResponseData.link = Convert.ToString(HrefLink == "null" || HrefLink == "undefined" ? null : HrefLink);
        //                        ResponseData.description = Convert.ToString(Description_Image == "null" || Description_Image == "undefined" ? null : Description_Image);
        //                        ResponseData.link_download = Convert.ToString(Link_Download == "null" || Link_Download == "undefined" ? null : Link_Download);
        //                        ResponseData.file_order = Convert.ToInt32(Order == "null" || Order == "undefined" ? "0" : Order);
        //                        ResponseData.flag_button = Convert.ToInt32(Status_Image == "null" || Status_Image == "undefined" ? "0" : Status_Image);
        //                    }
        //                }

        //                Content_File ModelFile = new Content_File();
        //                ModelFile.File_Type = Convert.ToString(ResponseData.file_type);
        //                ModelFile.File_Name = Convert.ToString(ResponseData.file_name);
        //                ModelFile.Href_Link = Convert.ToString(ResponseData.link);
        //                ModelFile.Description = Convert.ToString(ResponseData.description);
        //                ModelFile.Link_Download = Convert.ToString(ResponseData.link_download);
        //                ModelFile.File_Order = Convert.ToInt32(ResponseData.file_order);
        //                ModelFile.Flag_Button = Convert.ToInt32(ResponseData.flag_button);
        //                ModelFile.File_Path = Convert.ToString(ResponseData.file_path);
        //                ModelFile.CoverImage_Path = Convert.ToString(ResponseData.coverimage_path);
        //                ModelFile.Is_Active = 1;
        //                ModelFile.Create_By = "Admin";
        //                ModelFile.Create_Date = DateTime.Now;
        //                _dbContext.Content_File.Add(ModelFile);
        //                _dbContext.SaveChanges();

        //                ResponseData.id = ModelFile.ID;

        //                if (ResponseData != null)
        //                {
        //                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
        //                }
        //                return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
        //            }
        //            else
        //            {
        //                return BadRequest();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}
    }
}
