using WorkMotion_WebAPI.BaseModel;
using WorkMotion_WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WorkMotion_WebAPI.Model.BaseModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        public CounterController(ASCCContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("SaveDataCounter")]
        public async Task<IActionResult> SaveDataCounter(int Counter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? CounterNumber = 0;
                    var result = _dbContext.CCC_Counter.FirstOrDefault();
                    if(result != null)
                    {
                        result.Counter += Counter;
                        CounterNumber = result.Counter;
                        _dbContext.CCC_Counter.Update(result);
                        _dbContext.SaveChanges();

                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = CounterNumber });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
