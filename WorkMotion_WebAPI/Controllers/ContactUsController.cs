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
using static WorkMotion_WebAPI.Model.ContactUsModel;
using static WorkMotion_WebAPI.Model.LogModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        private IConfiguration _configuration;
        public ContactUsController(ASCCContext dbContext, IConfiguration iconfig)
        {
            _dbContext = dbContext;
            _configuration = iconfig;
        }

        [HttpGet("GetContactUsTable")]
        public async Task<IActionResult> GetContactUsTable()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetDataContactUs = _dbContext.CONTACT_US.Where(x => x.ActiveFlag == true).ToList();

                    if (GetDataContactUs != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = GetDataContactUs });
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

        [HttpPut("UpdateDataContactUs")]
        public async Task<IActionResult> UpdateDataContactUs(Request_ContactUs request)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    Function_Detail += JsonConvert.SerializeObject(request);
                    if (request.ContactUs_ID == null)
                    {
                        return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
                    }
                    else
                    {
                        var dataContactUs = _dbContext.CONTACT_US.Where(x => x.ContactUs_ID == request.ContactUs_ID).FirstOrDefault();
                        if (dataContactUs != null)
                        {
                            dataContactUs.ContactUs_Address = request.ContactUs_Address;
                            dataContactUs.ContactUs_Email = request.ContactUs_Email;
                            dataContactUs.ContactUs_Phone = request.ContactUs_Phone;
                            dataContactUs.ContactUs_Latitude = request.ContactUs_Latitude;
                            dataContactUs.ContactUs_Longitude = request.ContactUs_Longitude;
                            dataContactUs.UpdateBy = request.CreateBy;
                            dataContactUs.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();

                            log.Function_Name = "Update |UpdateDataContactUs";
                            log.IP_Address = request.CreateBy;
                            log.Function_Detail = Function_Detail;
                            log.Log_Date = DateTime.Now;
                            _dbContext.LOG.Add(log);
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
                log.Function_Name = "Exception |DeleteDataBanner";
                log.IP_Address = request.CreateBy;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetDataSEOContactUs")]
        public async Task<IActionResult> GetDataSEOContactUs()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetDataMenuBanner = _dbContext.MENU.Where(x => x.Menu_ID == 5).ToList();

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
