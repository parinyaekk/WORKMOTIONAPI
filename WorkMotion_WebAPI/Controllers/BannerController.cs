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

namespace WorkMotion_WebAPI.Controllers
{
    [Route("api/[controller]")]
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
            try
            {
                if (ModelState.IsValid)
                {
                    if (request.Banner_ID == null)
                    {
                        BANNER newBanner = new BANNER();
                        newBanner.Banner_Name = request.Banner_Name;
                        newBanner.Banner_Topic = request.Banner_Topic;
                        newBanner.Banner_Description = request.Banner_Description;
                        newBanner.Banner_Image_Path = request.Banner_Image_Path;
                        newBanner.Is_Display = true;
                        newBanner.ActiveFlag = true;
                        newBanner.CreateDate = DateTime.Now;
                        _dbContext.BANNER.Add(newBanner);
                        _dbContext.SaveChanges();

                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                    }
                    else
                    {
                        var dataBanner = _dbContext.BANNER.Where(x => x.Banner_ID == request.Banner_ID).FirstOrDefault();
                        if (dataBanner != null)
                        {
                            dataBanner.Banner_Name = request.Banner_Name;
                            dataBanner.Banner_Topic = request.Banner_Topic;
                            dataBanner.Banner_Description = request.Banner_Description;
                            dataBanner.Banner_Image_Path = request.Banner_Image_Path;
                            dataBanner.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                        }
                        return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
                    }
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("DeleteDataBanner")]
        public async Task<IActionResult> DeleteDataBanner(int? Banner_ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Banner_ID != null)
                    {
                        var dataBanner = _dbContext.BANNER.Where(x => x.Banner_ID == Banner_ID).FirstOrDefault();
                        if (dataBanner != null)
                        {
                            dataBanner.ActiveFlag = false;
                            _dbContext.SaveChanges();
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                        }
                    }
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("SetDisplayBanner")]
        public async Task<IActionResult> SetDisplayBanner(int? Banner_ID)
        {
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
                            dataBanner.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                        }
                    }
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
