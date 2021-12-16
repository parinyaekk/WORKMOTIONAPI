using WorkMotion_WebAPI.BaseModel;
using WorkMotion_WebAPI.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WorkMotion_WebAPI.Model.BaseModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        public TestController(ASCCContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpPost("TestHelloWorld")]
        public async Task<IActionResult> TestHelloWorld()
        {
            try
            {
                return Ok(new ResponseModel { Message = "Hello World !", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("TestSendMail")]
        public bool TestSendMail(string SendTo, string SendFrom, string settingsmtp, int portsmtp)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(SendFrom));
                email.To.Add(MailboxAddress.Parse(SendTo.ToString()));
                email.Subject = "ทดสอบ";
                // send email
                SmtpClient smtp = new SmtpClient();
                smtp.Connect(settingsmtp, portsmtp, SecureSocketOptions.None);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
