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
using static WorkMotion_WebAPI.Model.LogModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("API/[controller]")]
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
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    Function_Detail += JsonConvert.SerializeObject(request);
                    if (request.Portfolio_ID == null)
                    {
                        log.Function_Name = "Insert |UpdateDataPortfolio";
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
                        newPortfolio.CreateBy = request.CreateBy;
                        newPortfolio.CreateDate = DateTime.Now;
                        _dbContext.PORTFOLIO.Add(newPortfolio);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        var dataPortfolio = _dbContext.PORTFOLIO.Where(x => x.Portfolio_ID == request.Portfolio_ID).FirstOrDefault();
                        if (dataPortfolio != null)
                        {
                            log.Function_Name = "Insert |UpdateDataPortfolio";
                            dataPortfolio.FK_Industries_ID = request.FK_Industries_ID;
                            dataPortfolio.Portfolio_Section = request.Portfolio_Section;
                            dataPortfolio.Portfolio_Name = request.Portfolio_Name;
                            dataPortfolio.Portfolio_Logo_Path = request.Portfolio_Logo_Path;
                            dataPortfolio.Portfolio_About = request.Portfolio_About;
                            dataPortfolio.Portfolio_Technology = request.Portfolio_Technology;
                            dataPortfolio.Portfolio_Location = request.Portfolio_Location;
                            dataPortfolio.Portfolio_Contact_Website = request.Portfolio_Contact_Website;
                            dataPortfolio.Portfolio_Contact_LinkedIn = request.Portfolio_Contact_LinkedIn;
                            dataPortfolio.UpdateBy = request.CreateBy;
                            dataPortfolio.UpdateDate = DateTime.Now;
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
                log.Function_Detail = Function_Detail;
                log.IP_Address = request.CreateBy;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("DeleteDataPortfolio")]
        public async Task<IActionResult> DeleteDataPortfolio(int? Portfolio_ID, string CreateBy)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (Portfolio_ID != null)
                    {
                        var dataPortfolio = _dbContext.PORTFOLIO.Where(x => x.Portfolio_ID == Portfolio_ID).FirstOrDefault();
                        if (dataPortfolio != null)
                        {
                            dataPortfolio.ActiveFlag = false;
                            dataPortfolio.UpdateBy = CreateBy;
                            dataPortfolio.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();

                            Function_Detail += "Portfolio_ID: " + Portfolio_ID;
                            log.Function_Name = "DeleteDataPortfolio";
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
                log.Function_Name = "Exception |DeleteDataPortfolio";
                log.IP_Address = CreateBy;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetDataSEOPortfolio")]
        public async Task<IActionResult> GetDataSEOPortfolio()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetDataMenuBanner = _dbContext.MENU.Where(x => x.Menu_ID == 2).ToList();

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
