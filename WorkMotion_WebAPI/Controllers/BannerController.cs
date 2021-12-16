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

namespace WorkMotion_WebAPI.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        private IConfiguration _configuration;
        public BannerController(ASCCContext dbContext, IConfiguration iconfig)
        {
            _dbContext = dbContext;
            _configuration = iconfig;
        }

        [HttpGet("GetBannerTable")]
        public async Task<IActionResult> GetBannerTable()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetDataMainMenu = _dbContext.BANNER.Where(x => x.ActiveFlag == true).ToList();

                    if (GetDataMainMenu != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = GetDataMainMenu });
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

        [HttpPost("UpdateDataBanner")]
        public async Task<IActionResult> UpdateDataBanner(Request_Banner request)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    Function_Detail += JsonConvert.SerializeObject(request);
                    if (request.Banner_ID == null)
                    {
                        log.Function_Name = "Insert |UpdateDataBanner";
                        BANNER newBanner = new BANNER();
                        newBanner.Banner_Name = request.Banner_Name;
                        newBanner.Banner_Topic = request.Banner_Topic;
                        newBanner.Banner_Description = request.Banner_Description;
                        newBanner.Banner_Image_Path = request.Banner_Image_Path;
                        newBanner.Is_Display = true;
                        newBanner.ActiveFlag = true;
                        newBanner.CreateBy = request.CreateBy;
                        newBanner.CreateDate = DateTime.Now;
                        _dbContext.BANNER.Add(newBanner);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        log.Function_Name = "Update |UpdateDataBanner";
                        var dataBanner = _dbContext.BANNER.Where(x => x.Banner_ID == request.Banner_ID).FirstOrDefault();
                        if (dataBanner != null)
                        {
                            dataBanner.Banner_Name = request.Banner_Name;
                            dataBanner.Banner_Topic = request.Banner_Topic;
                            dataBanner.Banner_Description = request.Banner_Description;
                            dataBanner.Banner_Image_Path = request.Banner_Image_Path;
                            dataBanner.UpdateBy = request.CreateBy;
                            dataBanner.UpdateDate = DateTime.Now;
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
                log.Function_Name = "Exception |UpdateDataBanner";
                log.IP_Address = request.CreateBy;
                log.Function_Detail = Function_Detail;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("DeleteDataBanner")]
        public async Task<IActionResult> DeleteDataBanner(int? Banner_ID, string CreateBy)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (Banner_ID != null)
                    {
                        var dataBanner = _dbContext.BANNER.Where(x => x.Banner_ID == Banner_ID).FirstOrDefault();
                        if (dataBanner != null)
                        {
                            dataBanner.UpdateBy = CreateBy;
                            dataBanner.UpdateDate = DateTime.Now;
                            dataBanner.ActiveFlag = false;
                            _dbContext.SaveChanges();

                            Function_Detail += "Banner_ID: " + Banner_ID;
                            log.Function_Name = "DeleteDataBanner";
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
                log.Function_Name = "Exception |DeleteDataBanner";
                log.IP_Address = CreateBy;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("SetDisplayBanner")]
        public async Task<IActionResult> SetDisplayBanner(int? Banner_ID, string CreateBy)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (Banner_ID != null)
                    {
                        var dataBanner = _dbContext.BANNER.Where(x => x.Banner_ID == Banner_ID).FirstOrDefault();
                        if (dataBanner != null)
                        {
                            dataBanner.Is_Display = !dataBanner.Is_Display;
                            dataBanner.UpdateBy = CreateBy;
                            dataBanner.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();

                            Function_Detail += "Banner_ID: " + Banner_ID;
                            log.Function_Name = "SetDisplayBanner";
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
                log.Function_Name = "Exception |SetDisplayBanner";
                log.IP_Address = CreateBy;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetDataSEOBanner")]
        public async Task<IActionResult> GetDataSEOBanner()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetDataMenuBanner = _dbContext.MENU.Where(x => x.Menu_ID == 1).ToList();

                    if (GetDataMenuBanner != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = GetDataMenuBanner });
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
