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

    }
}
