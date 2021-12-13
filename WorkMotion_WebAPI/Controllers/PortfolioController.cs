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
using static WorkMotion_WebAPI.Model.PortfolioModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        private IConfiguration _configuration;
        public PortfolioController(ASCCContext dbContext, IConfiguration iconfig)
        {
            _dbContext = dbContext;
            _configuration = iconfig;
        }

        [HttpGet("GetStartUpTable")]
        public async Task<IActionResult> GetStartUpTable()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var StartUp = _dbContext.PORTFOLIO.Where(x => x.Portfolio_Section == 1 && x.ActiveFlag == true);
                    var Industries = _dbContext.INDUSTRIES;

                    var GetDataStartUp = (from data in StartUp
                                          join ind1 in Industries on data.FK_Industries_ID equals ind1.Industries_ID into ind2
                                          from ind in ind2.DefaultIfEmpty()
                                          select new
                                          {
                                              data.Portfolio_ID,
                                              data.Portfolio_Logo_Path,
                                              data.Portfolio_Name,
                                              Industries_ID = ind == null ? 0 : ind.Industries_ID,
                                              Industries_Name = ind == null ? null : ind.Industries_Name,
                                              data.Portfolio_About,
                                              data.Portfolio_Technology,
                                              data.Portfolio_Location,
                                              data.Portfolio_Contact_Website,
                                              data.Portfolio_Contact_LinkedIn
                                          }).ToList();

                    if (GetDataStartUp != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = GetDataStartUp });
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

        [HttpGet("GetFundTable")]
        public async Task<IActionResult> GetFundTable()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var StartUp = _dbContext.PORTFOLIO.Where(x => x.Portfolio_Section == 2 && x.ActiveFlag == true);
                    var Industries = _dbContext.INDUSTRIES;

                    var GetDataStartUp = (from data in StartUp
                                          join ind1 in Industries on data.FK_Industries_ID equals ind1.Industries_ID into ind2
                                          from ind in ind2.DefaultIfEmpty()
                                          select new
                                          {
                                              data.Portfolio_ID,
                                              data.Portfolio_Logo_Path,
                                              data.Portfolio_Name,
                                              Industries_ID = ind == null ? 0 : ind.Industries_ID,
                                              Industries_Name = ind == null ? null : ind.Industries_Name,
                                              data.Portfolio_About,
                                              data.Portfolio_Technology,
                                              data.Portfolio_Location,
                                              data.Portfolio_Contact_Website,
                                              data.Portfolio_Contact_LinkedIn
                                          }).ToList();

                    if (GetDataStartUp != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = GetDataStartUp });
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

        [HttpGet("GetPartnershipTable")]
        public async Task<IActionResult> GetPartnershipTable()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var StartUp = _dbContext.PORTFOLIO.Where(x => x.Portfolio_Section == 3 && x.ActiveFlag == true);
                    var Industries = _dbContext.INDUSTRIES;

                    var GetDataStartUp = (from data in StartUp
                                          join ind1 in Industries on data.FK_Industries_ID equals ind1.Industries_ID into ind2
                                          from ind in ind2.DefaultIfEmpty()
                                          select new
                                          {
                                              data.Portfolio_ID,
                                              data.Portfolio_Logo_Path,
                                              data.Portfolio_Name,
                                              Industries_ID = ind == null ? 0 : ind.Industries_ID,
                                              Industries_Name = ind == null ? null : ind.Industries_Name,
                                              data.Portfolio_About,
                                              data.Portfolio_Technology,
                                              data.Portfolio_Location,
                                              data.Portfolio_Contact_Website,
                                              data.Portfolio_Contact_LinkedIn
                                          }).ToList();

                    if (GetDataStartUp != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = GetDataStartUp });
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

        [HttpPost("UpdateDataPortfolio")]
        public async Task<IActionResult> UpdateDataPortfolio(Request_Portfolio request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (request.Portfolio_ID == null)
                    {
                        PORTFOLIO newPortfolio = new PORTFOLIO();
                        newPortfolio.FK_Industries_ID = request.FK_Industries_ID;
                        newPortfolio.Portfolio_Section = request.Portfolio_Section;
                        newPortfolio.Portfolio_Name = request.Portfolio_Name;
                        newPortfolio.Portfolio_Logo_Path = request.Portfolio_Logo_Path;
                        newPortfolio.Portfolio_About = request.Portfolio_About;
                        newPortfolio.Portfolio_Technology = request.Portfolio_Technology;
                        newPortfolio.Portfolio_Location = request.Portfolio_Location;
                        newPortfolio.Portfolio_Contact_Website = request.Portfolio_Contact_Website;
                        newPortfolio.Portfolio_Contact_LinkedIn = request.Portfolio_Contact_LinkedIn;
                        newPortfolio.ActiveFlag = true;
                        newPortfolio.CreateDate = DateTime.Now;
                        _dbContext.PORTFOLIO.Add(newPortfolio);
                        _dbContext.SaveChanges();

                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                    }
                    else
                    {
                        var dataPortfolio = _dbContext.PORTFOLIO.Where(x => x.Portfolio_ID == request.Portfolio_ID).FirstOrDefault();
                        if (dataPortfolio != null)
                        {
                            dataPortfolio.FK_Industries_ID = request.FK_Industries_ID;
                            dataPortfolio.Portfolio_Section = request.Portfolio_Section;
                            dataPortfolio.Portfolio_Name = request.Portfolio_Name;
                            dataPortfolio.Portfolio_Logo_Path = request.Portfolio_Logo_Path;
                            dataPortfolio.Portfolio_About = request.Portfolio_About;
                            dataPortfolio.Portfolio_Technology = request.Portfolio_Technology;
                            dataPortfolio.Portfolio_Location = request.Portfolio_Location;
                            dataPortfolio.Portfolio_Contact_Website = request.Portfolio_Contact_Website;
                            dataPortfolio.Portfolio_Contact_LinkedIn = request.Portfolio_Contact_LinkedIn;
                            dataPortfolio.UpdateDate = DateTime.Now;
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
