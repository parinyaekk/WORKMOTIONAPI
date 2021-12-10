using WorkMotion_WebAPI.BaseModel;
using WorkMotion_WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WorkMotion_WebAPI.Model.BaseModel;
using static WorkMotion_WebAPI.Model.Menu_Admin_DetailModel;
using static WorkMotion_WebAPI.Model.Permission;
using static WorkMotion_WebAPI.Model.User_GroupModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        public PermissionController(ASCCContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("GetDataPermission")]
        public async Task<IActionResult> GetDataPermission()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = (from ma in _dbContext.CCC_Menu_Admin.Where(x=>x.Is_Active == 1)
                                  join m in _dbContext.CCC_Menu_Admin_Detail on ma.ID equals m.FK_Menu_ID
                                  select new
                                  {
                                      ID = ma.ID,
                                      user_group = m.FK_User_Group_ID,
                                      menu_name = ma.Menu_Name.ToLower(),
                                      fk_Menu_ID = m.FK_Menu_ID,
                                      Permission = m.Permission,
                                      Is_Active = ma.Is_Active
                                  }).ToList();

                    if (result.Count() > 0)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
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

        [HttpPost("GetDataPermissionList")]
        public async Task<IActionResult> GetDataPermissionList(PermissionModel inputData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Start = Convert.ToInt32((inputData.Page - 1) * inputData.PerPage);
                    var End = Convert.ToInt32(inputData.PerPage);

                    var temp = (from userg in _dbContext.CCC_User_Group
                                  select new 
                                  {
                                      ID = userg.ID,
                                      GroupName = userg.Groupname,
                                      Is_Active = userg.Is_Active
                                  }).ToList();

                    var result = temp.OrderBy(x => x.ID).Skip(Start).Take(End).ToList();
                    var ResponseData = new
                    {
                        result,
                        Total = temp.Count()
                    };
                    if (result.Count() > 0)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = ResponseData });
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

        [HttpPost("GetDataPermissionByID")]
        public async Task<IActionResult> GetDataPermissionByID(int ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = (from ma in _dbContext.CCC_Menu_Admin.Where(x => x.Is_Active == 1)
                                  join m in _dbContext.CCC_Menu_Admin_Detail on ma.ID equals m.FK_Menu_ID
                                  where m.FK_User_Group_ID == ID
                                  select new
                                  {
                                      ID = ma.ID,
                                      user_group = m.FK_User_Group_ID,
                                      menu_name = ma.Menu_Name.ToLower(),
                                      fk_Menu_ID = m.FK_Menu_ID,
                                      Permission = m.Permission,
                                      Is_Active = ma.Is_Active
                                  }).ToList();

                    if (result.Count() > 0)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
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

        [HttpPost("GetAllMenu")]
        public async Task<IActionResult> GetAllMenu()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Menu = _dbContext.CCC_Menu_Admin.Where(x => x.Is_Active == 1).ToList();

                    if (Menu.Count() > 0)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = Menu });
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

        [HttpPost("UpdateDataPermission")]
        public async Task<IActionResult> UpdateDataPermission(UpdateDatPermissionModel TempData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var GetData = _dbContext.CCC_Menu_Admin_Detail.Where(x => x.FK_Menu_ID == TempData.ID).ToList();
                    if (GetData.Count() == 14)
                    {
                        GetData[0].Permission = TempData.PermissionWarranty;
                        GetData[1].Permission = TempData.PermissionMenu;
                        GetData[2].Permission = TempData.PermissionContent;
                        GetData[3].Permission = TempData.PermissionSparepartAndInstallation;
                        GetData[4].Permission = TempData.PermissionModelSparepart;
                        GetData[5].Permission = TempData.PermissionClassifiedSparepart;
                        GetData[6].Permission = TempData.PermissionModelInstallation;
                        GetData[7].Permission = TempData.PermissionClassifiedInstallation;
                        GetData[8].Permission = TempData.PermissionManageProduct;
                        GetData[9].Permission = TempData.PermissionMembershipRegistration;
                        GetData[10].Permission = TempData.PermissionManageMembership;
                        GetData[11].Permission = TempData.PermissionMembershipRenew;
                        GetData[12].Permission = TempData.PermissionServiceMembershipInformation;
                        GetData[13].Permission = TempData.PermissionManageEmployee;
                        GetData[14].Permission = TempData.PermissionManageSatisfaction;

                        _dbContext.CCC_Menu_Admin_Detail.UpdateRange(GetData);
                        _dbContext.SaveChanges();
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
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

        [HttpPost("AddPermissionMenu")]
        public async Task<IActionResult> AddPermissionMenu(int? GroupID, string jsondata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrWhiteSpace(jsondata))
                    {
                        int parsecatch = 0;
                        var checkData = _dbContext.CCC_Menu_Admin_Detail.Where(x => x.FK_User_Group_ID == GroupID).ToList();
                        if(checkData.Count() > 0)
                        {
                            foreach(var items in checkData)
                            {
                                _dbContext.CCC_Menu_Admin_Detail.Remove(items);
                                _dbContext.SaveChanges();
                            }
                        }

                        var datatemp = jsondata.Substring(0,jsondata.Length-1).Split('|');
                        foreach (var items in datatemp)
                        {
                            var splitcomma = items.Split(',');
                            var fkmenuid = int.TryParse(splitcomma[0], out parsecatch);
                            Menu_Admin_Detail temp = new Menu_Admin_Detail()
                            {
                                FK_Menu_ID = parsecatch,
                                FK_User_Group_ID = GroupID,
                                Permission = splitcomma[1]
                            };
                            _dbContext.CCC_Menu_Admin_Detail.Add(temp);
                            _dbContext.SaveChanges();
                        }

                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
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
