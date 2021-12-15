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
    [Route("api/[controller]")]
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
                    var Industries = _dbContext.INDUSTRIES.ToList();

                    if (Industries != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = Industries });
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
    }
}
