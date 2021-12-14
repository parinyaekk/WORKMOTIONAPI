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
using static WorkMotion_WebAPI.Model.TeamModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        private IConfiguration _configuration;
        public TeamController(ASCCContext dbContext, IConfiguration iconfig)
        {
            _dbContext = dbContext;
            _configuration = iconfig;
        }

        [HttpGet("GetTeamTable")]
        public async Task<IActionResult> GetTeamTable()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetDataTeam = _dbContext.TEAM.Where(x => x.ActiveFlag == true).ToList();

                    if (GetDataTeam != null)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = GetDataTeam });
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

        [HttpPost("UpdateDataTeam")]
        public async Task<IActionResult> UpdateDataTeam(Request_Team request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (request.Team_ID == null)
                    {
                        TEAM newTeam = new TEAM();
                        newTeam.Team_Name = request.Team_Name;
                        newTeam.Team_Position = request.Team_Position;
                        newTeam.Team_Personal_Story = request.Team_Personal_Story;
                        newTeam.Team_Education = request.Team_Education;
                        newTeam.Team_Interest = request.Team_Interest;
                        newTeam.Team_Contact_Channels = request.Team_Contact_Channels;
                        newTeam.Team_Image_Path = request.Team_Image_Path;
                        newTeam.ActiveFlag = true;
                        newTeam.CreateDate = DateTime.Now;
                        _dbContext.TEAM.Add(newTeam);
                        _dbContext.SaveChanges();

                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
                    }
                    else
                    {
                        var dataTeam = _dbContext.TEAM.Where(x => x.Team_ID == request.Team_ID).FirstOrDefault();
                        if (dataTeam != null)
                        {
                            dataTeam.Team_Name = request.Team_Name;
                            dataTeam.Team_Position = request.Team_Position;
                            dataTeam.Team_Personal_Story = request.Team_Personal_Story;
                            dataTeam.Team_Education = request.Team_Education;
                            dataTeam.Team_Interest = request.Team_Interest;
                            dataTeam.Team_Contact_Channels = request.Team_Contact_Channels;
                            dataTeam.Team_Image_Path = request.Team_Image_Path;
                            dataTeam.UpdateDate = DateTime.Now;
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

        [HttpDelete("DeleteDataTeam")]
        public async Task<IActionResult> DeleteDataTeam(int? Team_ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Team_ID != null)
                    {
                        var dataTeam = _dbContext.TEAM.Where(x => x.Team_ID == Team_ID).FirstOrDefault();
                        if (dataTeam != null)
                        {
                            dataTeam.ActiveFlag = false;
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
