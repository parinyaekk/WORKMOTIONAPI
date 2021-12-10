using WorkMotion_WebAPI.BaseModel;
using WorkMotion_WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WorkMotion_WebAPI.Model.BaseModel;
using static WorkMotion_WebAPI.Model.LoginModel;
using static WorkMotion_WebAPI.Model.LogModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        public LoginController(ASCCContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin(InputLoginModel inputModel)
        {
            string msglog = "";
            try
            {
                if (ModelState.IsValid)
                {
                    msglog += "Start";
                    if (!String.IsNullOrWhiteSpace(inputModel.Username) && !String.IsNullOrWhiteSpace(inputModel.Password))
                    {
                        msglog += "Have Username and password";
                        var ResponseData = (from emp in _dbContext.CCC_Employee
                                            where emp.Username == inputModel.Username && emp.Password == inputModel.Password
                                            select new
                                            {
                                                ID = emp.ID,
                                                Name = emp.Employee_Name + " " + emp.Employee_Surname,
                                                UserGroup = emp.FK_UserGroup_ID
                                            }).FirstOrDefault();

                        if (ResponseData != null)
                        {
                            try
                            {
                                msglog += "Success";
                                Log log = new Log();
                                log.Function = "AdminLogin";
                                log.Message = msglog;
                                log.DateTime = DateTime.Now;
                                _dbContext.CCC_Log.Add(log);
                                _dbContext.SaveChanges();
                            }
                            catch
                            {

                            }
                            return Ok(new ResponseModel { Message = Message.LoginSuccess, Status = APIStatus.Successful, Data = ResponseData });
                        }
                        return Ok(new ResponseModel { Message = Message.LoginFailed, Status = APIStatus.Error });
                    }
                    return Ok(new ResponseModel { Message = Message.LoginFailed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                try
                {
                    msglog += "Fail";
                    Log log = new Log();
                    log.Function = "AdminLogin";
                    log.Message = ex.Message + ";" + msglog;
                    log.DateTime = DateTime.Now;
                    _dbContext.CCC_Log.Add(log);
                    _dbContext.SaveChanges();
                }
                catch
                {

                }
                throw ex;
            }
        }

        [HttpPost("CustomerLogin")]
        public async Task<IActionResult> CustomerLogin(CustomerLoginModel inputModel)
        {
            string msglog = "";
            try
            {
                if (ModelState.IsValid)
                {
                    msglog += "Start";
                    if (!String.IsNullOrWhiteSpace(inputModel.Username) && !String.IsNullOrWhiteSpace(inputModel.Password))
                    {
                        msglog += "Have Username and password";
                        var ResponseData = (from cus in _dbContext.CCC_Customer
                                            where cus.Username.ToLower() == inputModel.Username.ToLower() && cus.Password == inputModel.Password
                                            && cus.Is_Active == 1
                                            select new
                                            {
                                                CustomerID = cus.ID
                                            }).LastOrDefault();
                        if (ResponseData != null)
                        {
                            try
                            {
                                msglog += "Success";
                                Log log = new Log();
                                log.Function = "CustomerLogin";
                                log.Message = msglog;
                                log.DateTime = DateTime.Now;
                                _dbContext.CCC_Log.Add(log);
                                _dbContext.SaveChanges();
                            }
                            catch
                            {

                            }
                            return Ok(new ResponseModel { Message = Message.LoginSuccess, Status = APIStatus.Successful, Data = ResponseData });
                        }
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                try
                {
                    msglog += "Fail";
                    Log log = new Log();
                    log.Function = "CustomerLogin";
                    log.Message = ex.Message + ";" + msglog;
                    log.DateTime = DateTime.Now;
                    _dbContext.CCC_Log.Add(log);
                    _dbContext.SaveChanges();
                }
                catch
                {

                }
                throw ex;
            }
        }
    }
}
