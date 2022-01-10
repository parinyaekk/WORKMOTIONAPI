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
using static WorkMotion_WebAPI.Model.InformationModel;
using System.Collections.Generic;
using static WorkMotion_WebAPI.Model.InformationFileModel;
using Microsoft.AspNetCore.Http;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("API/[controller]")]
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

        [HttpGet("GetOptionHDYH")]
        public async Task<IActionResult> GetOptionHDYH()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataOptionHDYH = _dbContext.HDYH_OPTION.Where(x => x.ActiveFlag == true);

                    if (dataOptionHDYH != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = dataOptionHDYH });
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

        [HttpGet("GetOptionStartUp")]
        public async Task<IActionResult> GetOptionStartUp()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataOptionStartUp = _dbContext.STARTUP_OPTION.Where(x => x.ActiveFlag == true);

                    if (dataOptionStartUp != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = dataOptionStartUp });
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

        [HttpGet("GetDataIndustries")]
        public async Task<IActionResult> GetDataIndustries()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Industries = _dbContext.INDUSTRIES.Where(x => x.ActiveFlag == true).ToList();

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

        [HttpGet("GetOptionCategoriesByIndustyID")]
        public async Task<IActionResult> GetOptionCategoriesByIndustyID(int? IndustyID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.CATEGORIES.Where(x => x.FK_Industries_ID == IndustyID && x.ActiveFlag == true).OrderBy(x => x.FK_Industries_ID).ThenBy(x => x.Group_Number).ThenByDescending(x => x.Is_TopGroup).ToList();

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

        [HttpGet("GetDataNewsAndActivities")]
        public async Task<IActionResult> GetDataNewsAndActivities()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataNews = _dbContext.NEWS.Where(x => x.ActiveFlag == true);
                    var dataNewsFile = _dbContext.NEWSFILE.Where(x => x.ActiveFlag == true);

                    var results = (from news in dataNews
                                   select new
                                   {
                                       news.News_ID,
                                       news.News_Title,
                                       news.News_Content,
                                       news.News_Main_Image_Path,
                                       news.News_Author,
                                       news.News_Tags,
                                       news.News_Publish_Date,
                                       news.Is_Display,
                                       listImages = dataNewsFile.Where(x => x.FK_News_ID == news.News_ID).ToList(),
                                   }).OrderByDescending(x => x.News_ID).ToList();

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

        [HttpGet("GetDataLastestNews")]
        public async Task<IActionResult> GetDataLastestNews(int? News_ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataNews = _dbContext.NEWS.Where(x => x.ActiveFlag == true && x.News_ID != News_ID);
                    var dataNewsFile = _dbContext.NEWSFILE.Where(x => x.ActiveFlag == true);

                    var results = (from news in dataNews
                                   select new
                                   {
                                       news.News_ID,
                                       news.News_Title,
                                       news.News_Content,
                                       news.News_Main_Image_Path,
                                       news.News_Author,
                                       news.News_Tags,
                                       news.News_Publish_Date,
                                       news.Is_Display,
                                       listImages = dataNewsFile.Where(x => x.FK_News_ID == news.News_ID).ToList(),
                                   }).OrderByDescending(x => x.News_ID).ToList();

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

        [HttpGet("GetDataContactUs")]
        public async Task<IActionResult> GetDataContactUs()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.CONTACT_US.Where(x => x.ActiveFlag == true).ToList();

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

        [HttpGet("GetDataSEOByMenuID")]
        public async Task<IActionResult> GetDataSEOByMenuID(int? Menu_ID)
        {
            try
            {
                //Menu_ID Menu_Name
                //1   Home
                //2   Portfolio
                //3   Team
                //4   News
                //5   Contact Us
                if (ModelState.IsValid)
                {
                    var results = _dbContext.MENU.Where(x => x.ActiveFlag == true && x.Menu_ID == Menu_ID).ToList();

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

        [HttpGet("GetDataTeam")]
        public async Task<IActionResult> GetDataTeam()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.TEAM.Where(x => x.ActiveFlag == true).OrderBy(x => x.Team_Sequence).ToList();

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

        [HttpGet("GetDataStartUp")]
        public async Task<IActionResult> GetDataStartUp()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.PORTFOLIO.Where(x => x.ActiveFlag == true && x.Portfolio_Section == 1).ToList();

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

        [HttpGet("GetDataFund")]
        public async Task<IActionResult> GetDataFund()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.PORTFOLIO.Where(x => x.ActiveFlag == true && x.Portfolio_Section == 2).ToList();

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

        [HttpGet("GetDataPartnership")]
        public async Task<IActionResult> GetDataPartnership()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var results = _dbContext.PORTFOLIO.Where(x => x.ActiveFlag == true && x.Portfolio_Section == 3).ToList();

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

        [HttpPost("AddDataCompany")]
        public async Task<IActionResult> AddDataCompany()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dynamic jsonData = JsonConvert.DeserializeObject(Request.Form["datas"]);
                    Request_Information request = new Request_Information();
                    //request.Information_ID = jsonData["Information_ID"];
                    request.FK_Startup_Option_ID = jsonData["FK_Startup_Option_ID"];
                    request.Information_Startup_Option_Text = jsonData["Information_Startup_Option_Text"];
                    request.FK_Industries_ID = jsonData["FK_Industries_ID"];
                    request.Information_Industries_Text = jsonData["Information_Industries_Text"];
                    request.FK_Categories_ID = jsonData["FK_Categories_ID"];
                    request.Information_Categories_Text = jsonData["Information_Categories_Text"];
                    request.FK_HDYH_Option_ID = jsonData["FK_HDYH_Option_ID"];
                    request.Information_HDYH_Text = jsonData["Information_HDYH_Text"];
                    request.Information_HDYH_Other = jsonData["Information_HDYH_Other"];
                    request.Information_Company_Name = jsonData["Information_Company_Name"];
                    request.Information_Email = jsonData["Information_Email"];
                    request.Information_Country_ID = jsonData["Information_Country_ID"];
                    request.Information_Country_Name = jsonData["Information_Country_Name"];
                    request.Information_Profile = jsonData["Information_Profile"];
                    request.Information_Detail = jsonData["Information_Detail"];
                    request.Information_Looking_For = jsonData["Information_Looking_For"];
                    request.Information_Looking_For_Other = jsonData["Information_Looking_For_Other"];

                    //JS INSERT
                    //var Temp = {
                    //FK_Startup_Option_ID: null,
                    //Information_Startup_Option_Text: null,
                    //FK_Industries_ID: null,
                    //Information_Industries_Text: null,
                    //FK_Categories_ID: null,
                    //Information_Categories_Text: null,
                    //FK_HDYH_Option_ID: null,
                    //Information_HDYH_Text: null,
                    //Information_HDYH_Other: null,
                    //Information_Company_Name: null,
                    //Information_Email: null,
                    //Information_Country_ID: null,
                    //Information_Country_Name: null,
                    //Information_Profile: null,
                    //Information_Detail: null,
                    //Information_Looking_For: null,
                    //Information_Looking_For_Other: null
                    //};

                    INFORMATION AddDataInformation = new INFORMATION();
                    AddDataInformation.FK_Startup_Option_ID = jsonData["FK_Startup_Option_ID"];
                    AddDataInformation.Information_Startup_Option_Text = jsonData["Information_Startup_Option_Text"];
                    AddDataInformation.FK_Industries_ID = jsonData["FK_Industries_ID"];
                    AddDataInformation.Information_Industries_Text = jsonData["Information_Industries_Text"];
                    AddDataInformation.FK_Categories_ID = jsonData["FK_Categories_ID"];
                    AddDataInformation.Information_Categories_Text = jsonData["Information_Categories_Text"];
                    AddDataInformation.FK_HDYH_Option_ID = jsonData["FK_HDYH_Option_ID"];
                    AddDataInformation.Information_HDYH_Text = jsonData["Information_HDYH_Text"];
                    AddDataInformation.Information_HDYH_Other = jsonData["Information_HDYH_Other"];
                    AddDataInformation.Information_Company_Name = jsonData["Information_Company_Name"];
                    AddDataInformation.Information_Email = jsonData["Information_Email"];
                    AddDataInformation.Information_Country_ID = jsonData["Information_Country_ID"];
                    AddDataInformation.Information_Country_Name = jsonData["Information_Country_Name"];
                    AddDataInformation.Information_Profile = jsonData["Information_Profile"];
                    AddDataInformation.Information_Detail = jsonData["Information_Detail"];
                    AddDataInformation.Information_Looking_For = jsonData["Information_Looking_For"];
                    AddDataInformation.Information_Looking_For_Other = jsonData["Information_Looking_For_Other"];
                    AddDataInformation.CreateBy = "Frontend";
                    AddDataInformation.CreateDate = DateTime.Now;
                    _dbContext.INFORMATION.Add(AddDataInformation);
                    _dbContext.SaveChanges();

                    if (Request.Form.Files.Count() > 0)
                    {
                        foreach (var File in Request.Form.Files)
                        {
                            await UploadFile(File);
                            INFORMATIONFILE InformationFile = new INFORMATIONFILE();
                            InformationFile.FK_Information_ID = AddDataInformation.Information_ID;
                            InformationFile.Information_File_Type = Convert.ToString(File.ContentType);
                            InformationFile.Information_File_Name = Convert.ToString(File.FileName);
                            InformationFile.Information_File_Path = Convert.ToString(getpath);
                            InformationFile.ActiveFlag = true;
                            InformationFile.CreateBy = "Frontend";
                            InformationFile.CreateDate = DateTime.Now;
                            _dbContext.INFORMATIONFILE.Add(InformationFile);
                            _dbContext.SaveChanges();
                        }
                    }

                    return Ok(new ResponseModel { Message = Message.Successfully, Status = APIStatus.Successful });
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        public string getpath { get; set; }
        [HttpPost("UploadFile"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(IFormFile File)
        {
            try
            {
                ////var file = Request.Form.Files[0];
                var file = File;
                var folderName = Path.Combine("Resources", "File");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileName = string.Concat(
                        Path.GetFileNameWithoutExtension(fileName),
                        string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now),
                        Path.GetExtension(fileName)
                    );
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    getpath = dbPath;
                    // Determine whether the directory exists.
                    if (!Directory.Exists(pathToSave))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(pathToSave);
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
