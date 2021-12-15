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
using static WorkMotion_WebAPI.Model.NewsModel;
using System.Collections.Generic;
using static WorkMotion_WebAPI.Model.NewsFileModel;
using Microsoft.AspNetCore.Http;
using static WorkMotion_WebAPI.Model.LogModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        public NewsController(ASCCContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetNewsTable")]
        public async Task<IActionResult> GetNewsTable()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var News = _dbContext.NEWS.Where(x => x.ActiveFlag == true);
                    var NewsFile = _dbContext.NEWSFILE.Where(x => x.ActiveFlag == true);

                    var GetDataNews = (from news in News
                                          //join newsfile1 in NewsFile on news.News_ID equals newsfile1.FK_News_ID into newsfile2
                                          //from newsfile in newsfile2.DefaultIfEmpty()
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
                                              listImages = NewsFile.Where(x => x.FK_News_ID == news.News_ID).ToList(),
                                          }).ToList();

                    if (GetDataNews != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = GetDataNews });
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

        [HttpDelete("DeleteDataNews")]
        public async Task<IActionResult> DeleteDataNews(int? News_ID, string CreateBy)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (News_ID != null)
                    {
                        var dataNews = _dbContext.NEWS.Where(x => x.News_ID == News_ID).FirstOrDefault();
                        if (dataNews != null)
                        {
                            dataNews.ActiveFlag = false;
                            dataNews.UpdateBy = CreateBy;
                            dataNews.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();

                            Function_Detail += "News_ID: " + News_ID;
                            log.Function_Name = "DeleteDataNews";
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
                log.Function_Name = "Exception |DeleteDataNews";
                log.IP_Address = CreateBy;
                log.Function_Detail = Function_Detail;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("SetDisplayNews")]
        public async Task<IActionResult> SetDisplayNews(int? News_ID, string CreateBy)
        {
            var log = new LOG();
            string Function_Detail = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (News_ID != null)
                    {
                        var dataNews = _dbContext.NEWS.Where(x => x.News_ID == News_ID).FirstOrDefault();
                        if (dataNews != null)
                        {
                            dataNews.Is_Display = !dataNews.Is_Display;
                            dataNews.UpdateBy = CreateBy;
                            dataNews.UpdateDate = DateTime.Now;
                            _dbContext.SaveChanges();

                            Function_Detail += "News_ID: " + News_ID;
                            log.Function_Name = "SetDisplayNews";
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
                log.Function_Name = "Exception |SetDisplayNews";
                log.IP_Address = CreateBy;
                log.Function_Detail = Function_Detail;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        public string getpath { get; set; }

        [HttpPost("UpdateDataNews")]
        public async Task<IActionResult> UpdateDataNews()
        {
            var log = new LOG();
            string Function_Detail = "";
            string CreateBy = "";
            try
            {
                if (ModelState.IsValid)
                {
                    Function_Detail += JsonConvert.SerializeObject(Request.Form["datas"]);
                    dynamic jsonData = JsonConvert.DeserializeObject(Request.Form["datas"]);
                    Request_News request = new Request_News();
                    request.News_ID = jsonData["News_ID"];
                    request.News_Title = jsonData["News_Title"];
                    request.News_Content = jsonData["News_Content"];
                    request.News_Main_Image_Path = jsonData["News_Main_Image_Path"];
                    request.News_Author = jsonData["News_Author"];
                    request.News_Publish_Date = jsonData["News_Publish_Date"];
                    request.News_Tags = jsonData["News_Tags"];
                    request.CreateBy = jsonData["CreateBy"];
                    request.Is_Display = true;

                    CreateBy = request.CreateBy;

                    var FindDataNews = _dbContext.NEWS.Where(x => x.News_ID == request.News_ID && x.ActiveFlag == true).FirstOrDefault();
                    if (FindDataNews != null)
                    {
                        log.Function_Name = "Update |UpdateDataNews";
                        FindDataNews.News_Title = jsonData["News_Title"];
                        FindDataNews.News_Content = jsonData["News_Content"];
                        FindDataNews.News_Main_Image_Path = jsonData["News_Main_Image_Path"];
                        FindDataNews.News_Author = jsonData["News_Author"];
                        FindDataNews.News_Publish_Date = jsonData["News_Publish_Date"];
                        FindDataNews.News_Tags = jsonData["News_Tags"];
                        FindDataNews.Is_Display = true;
                        FindDataNews.UpdateBy = request.CreateBy;
                        FindDataNews.UpdateDate = DateTime.Now;
                        _dbContext.NEWS.Update(FindDataNews);
                        _dbContext.SaveChanges();

                        List<OldFile> OldFile = JsonConvert.DeserializeObject<List<OldFile>>(jsonData["File"].ToString());
                        var GetDataReceipt_File = _dbContext.NEWSFILE.Where(x => x.ActiveFlag == true).ToList();
                        var FileClose = GetDataReceipt_File.Where(x => x.FK_News_ID == FindDataNews.News_ID).ToList();
                        foreach (var item in FileClose)
                        {
                            item.ActiveFlag = false;
                            item.UpdateBy = request.CreateBy;
                            item.UpdateDate = DateTime.Now;
                            _dbContext.NEWSFILE.Update(item);
                            _dbContext.SaveChanges();
                        }

                        foreach (var FileOpen in OldFile)
                        {
                            var TempFile = FileClose.Where(x => x.News_File_ID == FileOpen.id).FirstOrDefault();
                            if (TempFile != null)
                            {
                                TempFile.ActiveFlag = true;
                                TempFile.UpdateBy = request.CreateBy;
                                TempFile.UpdateDate = DateTime.Now;
                                _dbContext.NEWSFILE.Update(TempFile);
                                _dbContext.SaveChanges();
                            }
                        }

                        if (Request.Form.Files.Count() > 0)
                        {
                            foreach (var File in Request.Form.Files)
                            {
                                await UploadFile(File);
                                NEWSFILE NewsFile = new NEWSFILE();
                                NewsFile.FK_News_ID = FindDataNews.News_ID;
                                NewsFile.News_File_Type = Convert.ToString(File.ContentType);
                                NewsFile.News_File_Name = Convert.ToString(File.FileName);
                                NewsFile.News_File_Path = Convert.ToString(getpath);
                                NewsFile.ActiveFlag = true;
                                NewsFile.CreateBy = request.CreateBy;
                                NewsFile.CreateDate = DateTime.Now;
                                _dbContext.NEWSFILE.Add(NewsFile);
                                _dbContext.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        log.Function_Name = "Insert |UpdateDataNews";
                        NEWS AddDataNews = new NEWS();
                        AddDataNews.News_Title = jsonData["News_Title"];
                        AddDataNews.News_Content = jsonData["News_Content"];
                        AddDataNews.News_Main_Image_Path = jsonData["News_Main_Image_Path"];
                        AddDataNews.News_Author = jsonData["News_Author"];
                        AddDataNews.News_Publish_Date = jsonData["News_Publish_Date"];
                        AddDataNews.News_Tags = jsonData["News_Tags"];
                        AddDataNews.Is_Display = true;
                        AddDataNews.ActiveFlag = true;
                        AddDataNews.CreateBy = request.CreateBy;
                        AddDataNews.CreateDate = DateTime.Now;
                        _dbContext.NEWS.Add(AddDataNews);
                        _dbContext.SaveChanges();

                        List<OldFile> OldFile = JsonConvert.DeserializeObject<List<OldFile>>(jsonData["File"].ToString());
                        var GetDataReceipt_File = _dbContext.NEWSFILE.Where(x => x.ActiveFlag == true).ToList();
                        var FileClose = GetDataReceipt_File.Where(x => x.FK_News_ID == AddDataNews.News_ID).ToList();
                        foreach (var item in FileClose)
                        {
                            item.ActiveFlag = false;
                            item.UpdateBy = request.CreateBy;
                            item.UpdateDate = DateTime.Now;
                            _dbContext.NEWSFILE.Update(item);
                            _dbContext.SaveChanges();
                        }

                        foreach (var FileOpen in OldFile)
                        {
                            var TempFile = FileClose.Where(x => x.News_File_ID == FileOpen.id).FirstOrDefault();
                            if (TempFile != null)
                            {
                                TempFile.ActiveFlag = true;
                                TempFile.UpdateBy = request.CreateBy;
                                TempFile.UpdateDate = DateTime.Now;
                                _dbContext.NEWSFILE.Update(TempFile);
                                _dbContext.SaveChanges();
                            }
                        }

                        if (Request.Form.Files.Count() > 0)
                        {
                            foreach (var File in Request.Form.Files)
                            {
                                await UploadFile(File);
                                NEWSFILE NewsFile = new NEWSFILE();
                                NewsFile.FK_News_ID = AddDataNews.News_ID;
                                NewsFile.News_File_Type = Convert.ToString(File.ContentType);
                                NewsFile.News_File_Name = Convert.ToString(File.FileName);
                                NewsFile.News_File_Path = Convert.ToString(getpath);
                                NewsFile.ActiveFlag = true;
                                NewsFile.CreateBy = request.CreateBy;
                                NewsFile.CreateDate = DateTime.Now;
                                _dbContext.NEWSFILE.Add(NewsFile);
                                _dbContext.SaveChanges();
                            }
                        }
                    }
                    log.IP_Address = request.CreateBy;
                    log.Function_Detail = Function_Detail;
                    log.Log_Date = DateTime.Now;
                    _dbContext.LOG.Add(log);
                    _dbContext.SaveChanges();

                    return Ok(new ResponseModel { Message = Message.Successfully, Status = APIStatus.Successful });
                }
                return Ok(new ResponseModel { Message = Message.SystemError, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                log.Function_Name = "Exception |UpdateDataNews";
                log.IP_Address = CreateBy;
                log.Function_Detail = Function_Detail;
                log.Error_Detail = ex.Message;
                log.Log_Date = DateTime.Now;
                _dbContext.LOG.Add(log);
                _dbContext.SaveChanges();

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

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

        [HttpGet("GetDataSEONews")]
        public async Task<IActionResult> GetDataSEONews()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetDataMenuBanner = _dbContext.MENU.Where(x => x.Menu_ID == 4).ToList();

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
