using WorkMotion_WebAPI.BaseModel;
using WorkMotion_WebAPI.Model;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static WorkMotion_WebAPI.Model.BaseModel;
using static WorkMotion_WebAPI.Model.EmployeeModel;
using static WorkMotion_WebAPI.Model.LogModel;
using static WorkMotion_WebAPI.Model.Menu_Admin_DetailModel;

namespace WorkMotion_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ASCCContext _dbContext;
        public EmployeeController(ASCCContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("GetEmployeePerPage")]
        public async Task<IActionResult> GetEmployeePerPage(EmployeePaginationModel input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var start = Convert.ToInt32((input.Page - 1) * input.PerPage);
                    var end = Convert.ToInt32(input.PerPage);
                    DateTime? StartDate = null;
                    if (!String.IsNullOrEmpty(input.Start))
                    {
                        StartDate = Convert.ToDateTime(input.Start).Date;
                    }
                    DateTime? EndDate = null;
                    if (!String.IsNullOrEmpty(input.End))
                    {
                        EndDate = Convert.ToDateTime(input.End).Date;
                    }

                    var CCC_Employee = _dbContext.CCC_Employee
                        .Where(x => x.Is_Active == 1 &&
                        (Convert.ToDateTime(x.Create_Date).Date >= StartDate || StartDate == null) &&
                        (Convert.ToDateTime(x.Create_Date).Date <= EndDate || EndDate == null));
                    var dataEmployee = CCC_Employee.Where(x => ((x.Employee_Code ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Name ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Surname ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Tel ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Phone ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Email ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Address ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_ZIP_Code ?? "").Contains(input.Search)));
                    var result = (from e in dataEmployee
                                  select new
                                  {
                                      id = e.ID,
                                      username = e.Username,
                                      employee_code = e.Employee_Code,
                                      employee_name = e.Employee_Name,
                                      employee_surname = e.Employee_Surname,
                                      employee_tel = e.Employee_Tel,
                                      employee_phone = e.Employee_Phone,
                                      employee_email = e.Employee_Email,
                                      employee_address = e.Employee_Address,
                                      employee_zip_code = e.Employee_ZIP_Code,
                                      usergroup = e.FK_UserGroup_ID,
                                      is_active = e.Is_Active,
                                      Create_Date = e.Create_Date,
                                  }).OrderBy(x => x.id).ToList();

                    var ResponseData = new
                    {
                        result = result.Skip(start).Take(end).ToList(),
                        Total = result.Count()
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

        [HttpPost("ExportExcel")]
        public async Task<IActionResult> ExportExcel(EmployeePaginationModel input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime? StartDate = null;
                    if (!String.IsNullOrEmpty(input.Start))
                    {
                        StartDate = Convert.ToDateTime(input.Start).Date;
                    }
                    DateTime? EndDate = null;
                    if (!String.IsNullOrEmpty(input.End))
                    {
                        EndDate = Convert.ToDateTime(input.End).Date;
                    }
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = string.Concat(string.Format("{0:yyyyMMddHHmm}", DateTime.Now), "_DataEmployee", ".xlsx");
                    string.Concat(Path.GetFileNameWithoutExtension(fileName), string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now), Path.GetExtension(fileName)
                    var CCC_Employee = _dbContext.CCC_Employee
                        .Where(x => x.Is_Active == 1 &&
                        (Convert.ToDateTime(x.Create_Date).Date >= StartDate || StartDate == null) &&
                        (Convert.ToDateTime(x.Create_Date).Date <= EndDate || EndDate == null));
                    var dataEmployee = CCC_Employee.Where(x => ((x.Employee_Code ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Name ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Surname ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Tel ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Phone ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Email ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_Address ?? "").Contains(input.Search)) ||
                                                    ((x.Employee_ZIP_Code ?? "").Contains(input.Search)));
                    var result = (from e in dataEmployee
                                  select new
                                  {
                                      id = e.ID,
                                      username = e.Username,
                                      employee_code = e.Employee_Code,
                                      employee_name = e.Employee_Name,
                                      employee_surname = e.Employee_Surname,
                                      employee_tel = e.Employee_Tel,
                                      employee_phone = e.Employee_Phone,
                                      employee_email = e.Employee_Email,
                                      employee_address = e.Employee_Address,
                                      employee_zip_code = e.Employee_ZIP_Code,
                                      usergroup = e.FK_UserGroup_ID,
                                      is_active = e.Is_Active,
                                      Create_Date = e.Create_Date,
                                  }).OrderBy(x => x.id).ToList();

                    string ResultBase64 = "";
                    try
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Excel");
                            var currentRow = 1;
                            Header
                            worksheet.Cell(currentRow, 1).Value = "Data Employee ข้อมูลพนักงาน" + (String.IsNullOrEmpty(input.Start) ? "" : "ตั้งแต่วันที่ " + input.Start + " ถึง " + input.End);
                            worksheet.Cell(1, 1).Style.Font.Bold = true;
                            worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            worksheet.Range("A1:H1").Merge();
                            currentRow++;
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = "ลำดับ";
                            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 2).Value = "UserID";
                            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 3).Value = "ชื่อ";
                            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 4).Value = "โทรศัพท์";
                            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 5).Value = "โทรศัพท์มือถือ";
                            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 6).Value = "อีเมล";
                            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "ศูนย์บริการ";
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "สถานะ";
                            worksheet.Range(string.Format("A{0}:H{0}", currentRow)).Style.Font.Bold = true;
                            worksheet.Range(string.Format("A{0}:H{0}", currentRow)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            int index = 1;
                            var lst = new List<EmployeeExport>();
                            foreach (var emp in result)
                            {
                                lst.Add(new EmployeeExport()
                                {

                                    ลำดับ = index++,
                                    UserID = emp.username,
                                    ชื่อ = emp.employee_name + " " + emp.employee_surname,
                                    โทรศัพท์ = emp.employee_tel,
                                    โทรศัพท์มือถือ = emp.employee_phone,
                                    อีเมล = emp.employee_email,
                                    ศูนย์บริการ = emp.employee_address,
                                    สถานะ = emp.is_active == 1 ? "Active" : "InActive"
                                });
                            }

                            Data
                            foreach (var emp in result)
                            {
                                currentRow++;
                                worksheet.Cell(currentRow, 1).Value = index++;
                                worksheet.Cell(currentRow, 2).Value = emp.username;
                                worksheet.Cell(currentRow, 3).Value = emp.employee_name + " " + emp.employee_surname;
                                worksheet.Cell(currentRow, 4).Value = emp.employee_tel;
                                worksheet.Cell(currentRow, 5).Value = emp.employee_phone + "";
                                worksheet.Cell(currentRow, 6).Value = emp.employee_email;
                                worksheet.Cell(currentRow, 7).Value = emp.employee_address;
                                worksheet.Cell(currentRow, 8).Value = emp.is_active == 1 ? "Active" : "InActive";
                            }

                            worksheet.Range(3, 1, 7, 4).Merge().AddToNamed("Titles");
                            var tableWithData = worksheet.Cell(3, 1).InsertTable(lst.AsEnumerable());

                            using (var stream = new MemoryStream())
                            {
                                worksheet.Columns().AdjustToContents();
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                ResultBase64 = Convert.ToBase64String(stream.ToArray());

                                return File(content, contentType, fileName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
                    }
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("GetDataCareCenter")]
        public async Task<IActionResult> GetDataCareCenter()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _dbContext.CCC_Care_Center.Where(x => x.Is_Active == 1 && x.Lang_ID == 2).OrderBy(x => x.Code).ToList();
                    if (result.Count() > 0)
                    {
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = result });
                    }
                    return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
                }
                return Ok(new ResponseModel { Message = Message.InvalidPostedData, Status = APIStatus.SystemError });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int ID)
        {
            string msglog = "";
            try
            {
                msglog += "Start";
                if (ModelState.IsValid)
                {
                    var Data_Employee = _dbContext.CCC_Employee.Where(x => x.ID == ID).FirstOrDefault();
                    Data_Employee.Is_Active = 0;
                    Data_Employee.Update_By = "admin";
                    Data_Employee.Update_Date = DateTime.Now;
                    _dbContext.CCC_Employee.Update(Data_Employee);
                    _dbContext.SaveChanges();
                    msglog += "DeleteEmployeeSuccess";
                    if (Data_Employee != null)
                    {
                        try
                        {
                            msglog += "Success";
                            Log log = new Log();
                            log.Function = "DeleteEmployee";
                            log.Message = msglog;
                            log.DateTime = DateTime.Now;
                            _dbContext.CCC_Log.Add(log);
                            _dbContext.SaveChanges();
                        }
                        catch
                        {

                        }
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
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
                    log.Function = "DeleteEmployee";
                    log.Message = ex.Message + ";" + msglog;
                    log.DateTime = DateTime.Now;
                    _dbContext.CCC_Log.Add(log);
                    _dbContext.SaveChanges();
                }
                catch
                {

                }
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("AddDataEmployee")]
        public async Task<IActionResult> AddDataEmployee(UpdateEmployee input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrWhiteSpace(input.Username) && !String.IsNullOrWhiteSpace(input.Password))
                    {
                        Employee temp = new Employee()
                        {
                            Username = input.Username,
                            Password = input.Password,
                            Employee_Code = input.Employee_Code,
                            Employee_Name = input.Employee_Name,
                            Employee_Surname = input.Employee_Surname,
                            Employee_Tel = input.Employee_Tel,
                            Employee_Phone = input.Employee_Phone,
                            Employee_Email = input.Employee_Email,
                            Employee_Address = input.Employee_Address,
                            Employee_ZIP_Code = input.Employee_ZIP_Code,
                            FK_UserGroup_ID = input.UserGroup,
                            ServiceCenter = input.ServiceCenter,
                            FK_Manager_ID = null,
                            Is_Active = 1,
                            Create_By = "Admin",
                            Create_Date = DateTime.Now
                        };
                        _dbContext.CCC_Employee.Add(temp);
                        _dbContext.SaveChanges();

                        var datapermis = _dbContext.CCC_Menu_Admin_Detail.ToList();
                        var dataEmp = _dbContext.CCC_Employee.Where(x => x.FK_UserGroup_ID == input.UserGroup).FirstOrDefault();
                        var tempdatagroup = datapermis.Where(x => x.FK_Employee_ID == temp.ID).ToList();
                        List<Menu_Admin_Detail> tempmenuadmin = new List<Menu_Admin_Detail>();
                        for (int i = 0; i < 15; i++)
                        {
                            var tempdata = new Menu_Admin_Detail();
                            tempdata.FK_Employee_ID = temp.ID;
                            tempdata.FK_Menu_ID = i + 1;
                            tempdata.Permission = tempdatagroup[i].Permission;
                            tempmenuadmin.Add(tempdata);
                        }
                        _dbContext.CCC_Menu_Admin_Detail.AddRange(tempmenuadmin);
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

        [HttpPost("UpdateDataEmployee")]
        public async Task<IActionResult> UpdateDataEmployee(UpdateEmployee input)
        {
            string msglog = "";
            try
            {
                if (ModelState.IsValid)
                {
                    msglog += "Start";
                    var Data_Employee = _dbContext.CCC_Employee.Where(x => x.ID == input.ID).FirstOrDefault();
                    if (Data_Employee != null)
                    {
                        Data_Employee.Username = input.Username;
                        Data_Employee.Password = input.Password;
                        Data_Employee.Employee_Code = input.Employee_Code;
                        Data_Employee.Employee_Name = input.Employee_Name;
                        Data_Employee.Employee_Surname = input.Employee_Surname;
                        Data_Employee.Employee_Tel = input.Employee_Tel;
                        Data_Employee.Employee_Phone = input.Employee_Phone;
                        Data_Employee.Employee_Email = input.Employee_Email;
                        Data_Employee.Employee_Address = input.Employee_Address;
                        Data_Employee.Employee_ZIP_Code = input.Employee_ZIP_Code;
                        Data_Employee.Is_Active = input.Is_Active;
                        Data_Employee.ServiceCenter = input.ServiceCenter;
                        Data_Employee.Update_By = "admin";
                        Data_Employee.Update_Date = DateTime.Now;
                        Data_Employee.FK_UserGroup_ID = input.UserGroup;
                        _dbContext.CCC_Employee.Update(Data_Employee);
                        _dbContext.SaveChanges();
                        msglog += "UpdateDataEmployeeSuccess";
                    }
                    if (Data_Employee != null)
                    {
                        try
                        {
                            msglog += "Fail";
                            Log log = new Log();
                            log.Function = "UpdateDataEmployee";
                            log.Message = msglog;
                            log.DateTime = DateTime.Now;
                            _dbContext.CCC_Log.Add(log);
                            _dbContext.SaveChanges();
                        }
                        catch
                        {

                        }
                        return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
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
                    log.Function = "UpdateDataEmployee";
                    log.Message = ex.Message + ";" + msglog;
                    log.DateTime = DateTime.Now;
                    _dbContext.CCC_Log.Add(log);
                    _dbContext.SaveChanges();
                }
                catch
                {

                }
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("GetDataEmployeeByID")]
        public async Task<IActionResult> GetDataEmployeeByID(int ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ID > 0)
                    {
                        var GetDataEmployee = _dbContext.CCC_Employee.Where(x => x.Is_Active == 1 && x.ID == Convert.ToInt32(ID)).ToList();
                        var Response = (from emp in GetDataEmployee
                                        select new
                                        {
                                            id = emp.ID,
                                            username = emp.Username,
                                            password = emp.Password,
                                            employeeCode = emp.Employee_Code,
                                            employeeName = emp.Employee_Name,
                                            employeeSurName = emp.Employee_Surname,
                                            employeeTel = emp.Employee_Tel,
                                            employeePhone = emp.Employee_Phone,
                                            employeeEmail = emp.Employee_Email,
                                            employeeAddress = emp.Employee_Address,
                                            employeeZipcode = emp.Employee_ZIP_Code,
                                            usergroup = emp.FK_UserGroup_ID,
                                            servicecenter = emp.ServiceCenter,
                                            is_Active = emp.Is_Active
                                        }).LastOrDefault();
                        if (Response != null)
                        {
                            return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful, Data = Response });
                        }
                        return Ok(new ResponseModel { Message = Message.Failed, Status = APIStatus.Error, Data = null });
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

        [HttpPost("GetDataUserGroup")]
        public async Task<IActionResult> GetDataUserGroup()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataGroup = _dbContext.CCC_User_Group;
                    var result = (from g in dataGroup
                                  select new
                                  {
                                      id = g.ID,
                                      Groupname = g.Groupname,
                                      is_active = g.Is_Active,
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

        [HttpPost("TempAdData")]
        public async Task<IActionResult> TempAdData()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataemp = _dbContext.CCC_Employee.Where(x => x.Is_Active == 1).ToList();
                    for (int k = 22; k < 36; k++)
                    {
                        List<Menu_Admin_Detail> temp = new List<Menu_Admin_Detail>();
                        for (int i = 0; i < 15; i++)
                        {
                            var gid = dataemp.Where(x => x.ID == 7).Select(x => x.FK_UserGroup_ID).FirstOrDefault();
                            var tempdata = new Menu_Admin_Detail();
                            tempdata.FK_Employee_ID = k;
                            tempdata.FK_Menu_ID = i + 1;
                            tempdata.Permission = "W";
                            temp.Add(tempdata);
                        }
                        _dbContext.CCC_Menu_Admin_Detail.AddRange(temp);
                        _dbContext.SaveChanges();
                    }
                    return Ok(new ResponseModel { Message = Message.Success, Status = APIStatus.Successful });
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
