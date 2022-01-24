using WorkMotion_WebAPI.BaseModel;
using WorkMotion_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using static WorkMotion_WebAPI.Model.BaseModel;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using static WorkMotion_WebAPI.Model.BannerModel;
using static WorkMotion_WebAPI.Model.MenuModel;
using static WorkMotion_WebAPI.Model.LogModel;
using static WorkMotion_WebAPI.Model.HDYH_OptionModel;
using static WorkMotion_WebAPI.Model.IndustriesModel;
using static WorkMotion_WebAPI.Model.Startup_OptionModel;
using static WorkMotion_WebAPI.Model.Categories_OptionModel;
using ClosedXML.Excel;
using static WorkMotion_WebAPI.Model.InformationModel;
using System.Collections.Generic;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        private IConfiguration _configuration;
        public MasterController(ASCCContext dbContext, IConfiguration iconfig)
        {
            _dbContext = dbContext;
            _configuration = iconfig;
        }

        [HttpGet("GetOptionsIndustries")]
        public async Task<IActionResult> GetOptionsIndustries()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.INDUSTRIES.Where(x => x.ActiveFlag == true).ToList();

                    if (results != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = results });
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

        [HttpPut("UpdateDataIndustries")]
        public async Task<IActionResult> UpdateDataIndustries(Request_Industries request)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    Function_Detail += JsonConvert.SerializeObject(request);

                    log.Function_Name = "Update |UpdateDataIndustries";
                    var OldData = _dbContext.INDUSTRIES.Where(x => x.Industries_ID == request.Industries_ID).FirstOrDefault();
                    if (OldData != null)
                    {
                        OldData.Industries_Name = request.Industries_Name;
                        OldData.Industries_Image_Path = request.Industries_Image_Path;
                        OldData.Industries_Description = request.Industries_Description;
                        OldData.UpdateBy = request.CreateBy;
                        OldData.UpdateDate = DateTime.Now;
                        _dbContext.SaveChanges();
                    }

                    log.IP_Address = request.CreateBy;
                    log.Function_Detail = Function_Detail;
                    log.Log_Date = DateTime.Now;
                    _dbContext.LOG.Add(log);
                    _dbContext.SaveChanges();

                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                log.Function_Name = "Exception |UpdateDataIndustries";
                log.IP_Address = request.CreateBy;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetOptionsHDYH")]
        public async Task<IActionResult> GetOptionsHDYH()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.HDYH_OPTION.Where(x => x.ActiveFlag == true).ToList();

                    if (results != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = results });
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

        [HttpPost("UpdateDataHDYH")]
        public async Task<IActionResult> UpdateDataHDYH(Request_HDYH_Option request)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    Function_Detail += JsonConvert.SerializeObject(request);
                    if (request.HDYH_Option_ID == null)
                    {
                        log.Function_Name = "Insert |UpdateDataHDYH";
                        HDYH_OPTION newData = new HDYH_OPTION();
                        newData.HDYH_Option_Name = request.HDYH_Option_Name;
                        newData.ActiveFlag = true;
                        newData.CreateBy = request.CreateBy;
                        newData.CreateDate = DateTime.Now;
                        _dbContext.HDYH_OPTION.Add(newData);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        log.Function_Name = "Update |UpdateDataHDYH";
                        var OldData = _dbContext.HDYH_OPTION.Where(x => x.HDYH_Option_ID == request.HDYH_Option_ID).FirstOrDefault();
                        if (OldData != null)
                        {
                            OldData.HDYH_Option_Name = request.HDYH_Option_Name;
                            OldData.UpdateBy = request.CreateBy;
                            OldData.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();
                        }
                    }
                    log.IP_Address = request.CreateBy;
                    log.Function_Detail = Function_Detail;
                    log.Log_Date = DateTime.Now;
                    _dbContext.LOG.Add(log);
                    _dbContext.SaveChanges();

                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                log.Function_Name = "Exception |UpdateDataHDYH";
                log.IP_Address = request.CreateBy;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("DeleteDataHDYH")]
        public async Task<IActionResult> DeleteDataHDYH(int? HDYH_Option_ID, string CreateBy)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (HDYH_Option_ID != null)
                    {
                        var data= _dbContext.HDYH_OPTION.Where(x => x.HDYH_Option_ID == HDYH_Option_ID).FirstOrDefault();
                        if (data != null)
                        {
                            data.ActiveFlag = false;
                            data.UpdateBy = CreateBy;
                            data.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();

                            Function_Detail += "HDYH_Option_ID: " + HDYH_Option_ID;
                            log.Function_Name = "DeleteDataHDYH";
                            log.IP_Address = CreateBy;
                            log.Function_Detail = Function_Detail;
                            log.Log_Date = DateTime.Now;
                            _dbContext.LOG.Add(log);
                            _dbContext.SaveChanges();

                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                        }
                    }
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                log.Function_Name = "Exception |DeleteDataHDYH";
                log.IP_Address = CreateBy;
                log.Function_Detail = Function_Detail;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetOptionStartUp")]
        public async Task<IActionResult> GetOptionStartUp()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.STARTUP_OPTION.Where(x => x.ActiveFlag == true).ToList();

                    if (results != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = results });
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

        [HttpPost("UpdateDataStartUp")]
        public async Task<IActionResult> UpdateDataStartUp(Request_STARTUP_Option request)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    Function_Detail += JsonConvert.SerializeObject(request);
                    if (request.Startup_Option_ID == null)
                    {
                        log.Function_Name = "Insert |UpdateDataStartUp";
                        STARTUP_OPTION newData = new STARTUP_OPTION();
                        newData.Startup_Option_Name = request.Startup_Option_Name;
                        newData.ActiveFlag = true;
                        newData.CreateBy = request.CreateBy;
                        newData.CreateDate = DateTime.Now;
                        _dbContext.STARTUP_OPTION.Add(newData);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        log.Function_Name = "Update |UpdateDataStartUp";
                        var OldData = _dbContext.STARTUP_OPTION.Where(x => x.Startup_Option_ID == request.Startup_Option_ID).FirstOrDefault();
                        if (OldData != null)
                        {
                            OldData.Startup_Option_Name = request.Startup_Option_Name;
                            OldData.UpdateBy = request.CreateBy;
                            OldData.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();
                        }
                    }
                    log.IP_Address = request.CreateBy;
                    log.Function_Detail = Function_Detail;
                    log.Log_Date = DateTime.Now;
                    _dbContext.LOG.Add(log);
                    _dbContext.SaveChanges();

                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                log.Function_Name = "Exception |UpdateDataStartUp";
                log.IP_Address = request.CreateBy;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("DeleteDataStartUp")]
        public async Task<IActionResult> DeleteDataStartUp(int? Startup_Option_ID, string CreateBy)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (Startup_Option_ID != null)
                    {
                        var dataNews = _dbContext.STARTUP_OPTION.Where(x => x.Startup_Option_ID == Startup_Option_ID).FirstOrDefault();
                        if (dataNews != null)
                        {
                            dataNews.ActiveFlag = false;
                            dataNews.UpdateBy = CreateBy;
                            dataNews.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();

                            Function_Detail += "Startup_Option_ID: " + Startup_Option_ID;
                            log.Function_Name = "DeleteDataStartUp";
                            log.IP_Address = CreateBy;
                            log.Function_Detail = Function_Detail;
                            log.Log_Date = DateTime.Now;
                            _dbContext.LOG.Add(log);
                            _dbContext.SaveChanges();

                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                        }
                    }
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                log.Function_Name = "Exception |DeleteDataStartUp";
                log.IP_Address = CreateBy;
                log.Function_Detail = Function_Detail;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetOptionCategories")]
        public async Task<IActionResult> GetOptionCategories()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var datacategories = _dbContext.CATEGORIES.Where(x => x.ActiveFlag == true).ToList();
                    var Industries = _dbContext.INDUSTRIES;

                    var results = (from data in datacategories
                                          join ind1 in Industries on data.FK_Industries_ID equals ind1.Industries_ID into ind2
                                          from ind in ind2.DefaultIfEmpty()
                                          select new
                                          {
                                              data.Categories_ID,
                                              Industries_ID = ind == null ? 0 : ind.Industries_ID,
                                              Industries_Name = ind == null ? null : ind.Industries_Name,
                                              Group_Number = data.Group_Number == null ? 0 : data.Group_Number,
                                              data.Is_TopGroup,
                                              data.Categories_Name
                                          }).OrderBy(x => x.Industries_ID).ThenBy(x => x.Group_Number).ThenByDescending(x => x.Is_TopGroup).ToList();

                    if (results != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = results });
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

        [HttpPost("UpdateDataCategories")]
        public async Task<IActionResult> UpdateDataCategories(request_Categories request)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    Function_Detail += JsonConvert.SerializeObject(request);
                    if (request.Categories_ID == null)
                    {
                        log.Function_Name = "Insert |UpdateDataCategories";
                        CATEGORIES newData = new CATEGORIES();
                        newData.Categories_Name = request.Categories_Name;
                        newData.FK_Industries_ID = request.FK_Industries_ID;
                        newData.Group_Number = request.Group_Number;
                        newData.Is_TopGroup = request.Is_TopGroup;
                        newData.ActiveFlag = true;
                        newData.CreateBy = request.CreateBy;
                        newData.CreateDate = DateTime.Now;
                        _dbContext.CATEGORIES.Add(newData);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        log.Function_Name = "Update |UpdateDataCategories";
                        var OldData = _dbContext.CATEGORIES.Where(x => x.Categories_ID == request.Categories_ID).FirstOrDefault();
                        if (OldData != null)
                        {
                            OldData.Categories_Name = request.Categories_Name;
                            OldData.FK_Industries_ID = request.FK_Industries_ID;
                            OldData.Group_Number = request.Group_Number;
                            OldData.Is_TopGroup = request.Is_TopGroup;
                            OldData.UpdateBy = request.CreateBy;
                            OldData.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();
                        }
                    }
                    log.IP_Address = request.CreateBy;
                    log.Function_Detail = Function_Detail;
                    log.Log_Date = DateTime.Now;
                    _dbContext.LOG.Add(log);
                    _dbContext.SaveChanges();

                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                log.Function_Name = "Exception |UpdateDataCategories";
                log.IP_Address = request.CreateBy;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("DeleteDataCategories")]
        public async Task<IActionResult> DeleteDataCategories(int? Categories_ID, string CreateBy)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (Categories_ID != null)
                    {
                        var dataNews = _dbContext.CATEGORIES.Where(x => x.Categories_ID == Categories_ID).FirstOrDefault();
                        if (dataNews != null)
                        {
                            dataNews.ActiveFlag = false;
                            dataNews.UpdateBy = CreateBy;
                            dataNews.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();

                            Function_Detail += "Categories_ID: " + Categories_ID;
                            log.Function_Name = "DeleteDataCategories";
                            log.IP_Address = CreateBy;
                            log.Function_Detail = Function_Detail;
                            log.Log_Date = DateTime.Now;
                            _dbContext.LOG.Add(log);
                            _dbContext.SaveChanges();

                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                        }
                    }
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                log.Function_Name = "Exception |DeleteDataCategories";
                log.IP_Address = CreateBy;
                log.Function_Detail = Function_Detail;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("UploadImage"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var item = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "File");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                fileName = string.Concat(
                    Path.GetFileNameWithoutExtension(fileName),
                    string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now),
                    Path.GetExtension(fileName)
                );
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                if (!Directory.Exists(pathToSave))
                {
                    DirectoryInfo di = Directory.CreateDirectory(pathToSave);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    item.CopyTo(stream);
                }

                if (dbPath != null)
                {
                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = dbPath });
                }
                return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("UpdateDataSEOMenu")]
        public async Task<IActionResult> UpdateDataSEOMenu(Request_Menu request)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    Function_Detail += JsonConvert.SerializeObject(request);
                    var dataMenuBanner = _dbContext.MENU.Where(x => x.Menu_ID == request.Menu_ID).FirstOrDefault();
                    if (dataMenuBanner != null)
                    {
                        log.Function_Name = "Update |UpdateDataSEOMenu";
                        dataMenuBanner.Meta_Title = request.Meta_Title;
                        dataMenuBanner.Meta_Keyword = request.Meta_Keyword;
                        dataMenuBanner.Meta_Description = request.Meta_Description;
                        dataMenuBanner.UpdateBy = request.CreateBy;
                        dataMenuBanner.UpdateDate = DateTime.Now;
                        _dbContext.SaveChanges();

                        log.IP_Address = request.CreateBy;
                        log.Function_Detail = Function_Detail;
                        log.Log_Date = DateTime.Now;
                        _dbContext.LOG.Add(log);
                        _dbContext.SaveChanges();

                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                    }
                    return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                log.Function_Name = "Exception |UpdateDataSEOMenu";
                log.IP_Address = request.CreateBy;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetDataInformation")]
        public async Task<IActionResult> GetDataInformation()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.INFORMATION.Where(x => x.ActiveFlag == true).OrderByDescending(x => x.Information_ID).ToList();

                    if (results != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = results });
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

        [HttpPost("ExportExcelDataInformation")]
        public async Task<IActionResult> ExportExcelDataInformation()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.INFORMATION.Where(x => x.ActiveFlag == true).OrderByDescending(x => x.Information_ID).ToList();

                    string ResultBase64 = "";
                    try
                    {
                        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        string fileName = string.Concat(string.Format("{0:yyyyMMddHHmm}", DateTime.Now), "_DataTellUsMore", ".xlsx");

                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("DataTellUsMore");
                            var currentRow = 1;
                            //Header
                            worksheet.Cell(currentRow, 1).Value = "Data Tell Us More";
                            worksheet.Cell(1, 1).Style.Font.Bold = true;
                            worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            int index = 1;
                            var lst = new List<Export_INFORMATION>();
                            foreach (var re in results)
                            {
                                lst.Add(new Export_INFORMATION()
                                {
                                    SEQ = index++,
                                    Information_Categories_Text = re.Information_Categories_Text,
                                    Information_Industries_Text = re.Information_Industries_Text,
                                    Information_HDYH_Text = re.Information_HDYH_Text,
                                    Information_HDYH_Other = re.Information_HDYH_Other,
                                    Information_Looking_For = re.Information_Looking_For,
                                    Information_Looking_For_Other = re.Information_Looking_For_Other,
                                    Information_Startup_Option_Text = re.Information_Startup_Option_Text,
                                    Information_Company_Name = re.Information_Company_Name,
                                    Information_Email = re.Information_Email,
                                    Information_Profile =re.Information_Profile,
                                    Information_Detail = re.Information_Detail,
                                    Information_Country_Name = re.Information_Country_Name,
                                    Information_Phone_Number = re.Information_Phone_Number,
                                });
                            }

                            var tableWithData = worksheet.Cell(3, 1).InsertTable(lst.AsEnumerable());

                            using (var stream = new MemoryStream())
                            {
                                worksheet.Columns().AdjustToContents();
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                ResultBase64 = Convert.ToBase64String(stream.ToArray());

                                return File(content, contentType, fileName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
                    }
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                //throw ex;
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpGet("GetDataLogSuccess")]
        public async Task<IActionResult> GetDataLogSuccess()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.LOG.Where(x => x.Error_Detail == null).OrderByDescending(x => x.Log_ID).ToList();

                    if (results != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = results });
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

        [HttpGet("GetDataLogException")]
        public async Task<IActionResult> GetDataLogException()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.LOG.Where(x => x.Error_Detail != null).OrderByDescending(x => x.Log_ID).ToList();

                    if (results != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = results });
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
    }
}
