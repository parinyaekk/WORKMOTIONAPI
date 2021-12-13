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
    public class FrontendController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        private IConfiguration _configuration;
        public FrontendController(ASCCContext dbContext, IConfiguration iconfig)
        {
            _dbContext = dbContext;
            _configuration = iconfig;
        }

        [HttpGet("GetAllBanner")]
        public async Task<IActionResult> GetBannerTable()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetDataMainMenu = _dbContext.BANNER.Where(x => x.ActiveFlag == true && x.Is_Display == true).ToList();

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

        [HttpGet("GetCountOurActivities")]
        public async Task<IActionResult> GetCountOurActivities()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataPortfolio = _dbContext.PORTFOLIO.Where(x => x.ActiveFlag == true);

                    var GetCountOurActivities = new 
                    {
                        count_startup = dataPortfolio.Where(x => x.Portfolio_Section == 1).Count(),
                        count_fund = dataPortfolio.Where(x => x.Portfolio_Section == 2).Count(),
                        count_partnership = dataPortfolio.Where(x => x.Portfolio_Section == 3).Count(),
                    };

                    if (GetCountOurActivities != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = GetCountOurActivities });
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
