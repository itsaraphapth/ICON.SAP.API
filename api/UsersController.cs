using ICON.Framework.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using ICON.Interface;

namespace ICON.SAP.API
{
    public class UsersController : ApiController
    {
        public string TranBy = "SAPApi";


        /// <summary>
        /// Sync User From REM to AP
        /// </summary>
        /// <param name="Data">Username</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/syncuser")]
        public object SyncUser(dynamic Data)
        {
            DateTime TranDate = DateTime.Now;
            DBHelper dbHelpRE = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            AP_Interface_Log Log = ICON.Interface.Transaction.CreateAPLog("User", "SyncUser", null, TranBy);

            int ResponseCode = 500;
            object ResponseData = null;
            string ErrrorMessage = string.Empty;

            try
            {
                string sql = string.Empty;

                sql = @"
SELECT
	RoleID,
	RoleName,
	Description
FROM 
    Sys_Admin_Roles";
                DataTable DT_Role = dbHelpRE.ExecuteDataTable(sql);

                sql = @"
SELECT 
    DepartmentID, 
    DepartmentName, 
    Description, 
    isDelete
FROM Sys_Master_Department;";
                DataTable DT_Departments = dbHelpRE.ExecuteDataTable(sql);

                List<HR_Master_Department> ListDepartments = new List<HR_Master_Department>();
                List<HR_Master_Position> ListPositions = new List<HR_Master_Position>();

                DBHelper dbHelpAP = new DBHelper(ICON.Configuration.Database.AP_ConnectionString, null);
                System.Data.IDbTransaction APTran = dbHelpAP.BeginTransaction();
                try
                {

                    foreach (DataRow DR in DT_Departments.Rows)
                    {
                        sql = $"SELECT * FROM HR_Master_Department WHERE Code = '{DR["DepartmentID"].ToString()}'";
                        DataTable DT_APDepartments = dbHelpAP.ExecuteDataTable(sql, APTran);

                        HR_Master_Department Department = new HR_Master_Department();

                        if (DT_APDepartments.Rows.Count > 0)
                        {
                            Department.EditId = false;
                            Department.Code = DR["DepartmentID"].ToString();
                            Department.Name = DR["DepartmentName"].ToString();
                            Department.ModifyById = 0;
                            Department.ModifyBy = TranBy;
                            Department.ModifyDate = TranDate;
                            Department.ExecCommand.Update(dbHelpAP, APTran);
                        }
                        else
                        {
                            Department.Code = DR["DepartmentID"].ToString();
                            Department.Name = DR["DepartmentName"].ToString();
                            Department.CreateById = 0;
                            Department.CreateBy = TranBy;
                            Department.CreateDate = TranDate;
                            Department.ModifyById = 0;
                            Department.ModifyBy = TranBy;
                            Department.ModifyDate = TranDate;
                            Department.ExecCommand.Insert(dbHelpAP, APTran);
                        }

                        ListDepartments.Add(Department);
                    }

                    foreach (DataRow DR in DT_Role.Rows)
                    {
                        sql = $"SELECT * FROM HR_Master_Position WHERE Code = '{DR["RoleID"].ToString()}'";
                        DataTable DT_Position = dbHelpAP.ExecuteDataTable(sql, APTran);

                        HR_Master_Position Position = new HR_Master_Position();

                        if (DT_Position.Rows.Count > 0)
                        {
                            Position.ExecCommand.Load(DT_Position.Rows[0]);
                            Position.Name = Position.Name = DR["RoleName"].ToString();
                            Position.ModifyById = 0;
                            Position.ModifyBy = TranBy;
                            Position.ModifyDate = TranDate;
                            Position.ExecCommand.Update(dbHelpAP, APTran);
                        }
                        else
                        {
                            Position.Code = DR["RoleID"].ToString();
                            Position.Name = DR["RoleName"].ToString();
                            Position.CreateById = 0;
                            Position.CreateBy = TranBy;
                            Position.CreateDate = TranDate;
                            Position.ModifyById = 0;
                            Position.ModifyBy = TranBy;
                            Position.ModifyDate = TranDate;
                            Position.ExecCommand.Insert(dbHelpAP, APTran);
                        }

                        ListPositions.Add(Position);
                    }

                    sql = @"
SELECT 
    US.Username, 
    US.Password, 
    US.FirstName, 
    US.LastName, 
    US.Email, 
    UP.BirthDate, 
    UP.Mobile, 
    ISNULL(US.IsDelete, 0) IsDelete,
	R.RoleId,
    ISNULL(US.DepartmentID,'') DepartmentID
FROM Sys_Admin_Users US
    LEFT JOIN Sys_Admin_UserProfile UP ON US.UserId = UP.UserId
    LEFT JOIN Sys_Admin_UsersInBU IBU ON US.UserId = IBU.UserId
                                         AND isDefault = 1
    LEFT JOIN Sys_Admin_BU bu ON BU.BUId = IBU.BUId
    LEFT JOIN Sys_Admin_Roles R ON R.RoleId = BU.RoleId 
WHERE
    1 = 1 ";

                    DataTable DT_Users = dbHelpRE.ExecuteDataTable(sql);

                    foreach (DataRow DR in DT_Users.Rows)
                    {
                        sql = $"SELECT * FROM HR_Master_User WHERE Username = '{DR["Username"].ToString()}' ";
                        DataTable DT_APUser = dbHelpAP.ExecuteDataTable(sql, APTran);

                        if (DT_APUser.Rows.Count > 0)
                        {
                            HR_Master_User User = new HR_Master_User();
                            User.ExecCommand.Load(DT_APUser.Rows[0]);
                            User.EditId = false;
                            User.Username = DR["Username"].ToString();
                            User.Password = DR["Password"].ToString();
                            User.FirstName = DR["FirstName"] != null && DR["FirstName"].ToString() != "" ? DR["FirstName"].ToString() : null;
                            User.LastName = DR["LastName"] != null && DR["LastName"].ToString() != "" ? DR["LastName"].ToString() : null;
                            User.Email = DR["Email"] != null && DR["Email"].ToString() != "" ? DR["Email"].ToString() : null;

                            HR_Master_Position Position = ListPositions.Find(p => p.Code == DR["RoleId"].ToString());

                            if (Position != null)
                            {
                                User.PositionId = ListPositions.Find(p => p.Code == DR["RoleId"].ToString()).Id;
                            }
                            else
                            {
                                User.PositionId = null;
                            }

                            HR_Master_Department Department = ListDepartments.Find(p => p.Code == DR["DepartmentID"].ToString());
                            if (Department != null)
                            {
                                User.DepartmentId = ListDepartments.Find(p => p.Code == DR["DepartmentID"].ToString()).Id;
                            }
                            else
                            {
                                User.DepartmentId = null;
                            }

                            if (DR["BirthDate"] != null && DR["BirthDate"].ToString() != "")
                            {
                                User.BirthDate = Convert.ToDateTime(DR["BirthDate"]);
                            }
                            else
                            {
                                User.BirthDate = null;
                            }

                            User.Telephone = DR["Mobile"] != null && DR["Mobile"].ToString() != "" ? DR["Mobile"].ToString() : null;
                            User.Status = !Convert.ToBoolean(DR["IsDelete"]) ? "Active" : "InActive";
                            User.ModifyById = 0;
                            User.ModifyBy = TranBy;
                            User.ModifyDate = TranDate;
                            User.ExecCommand.Update(dbHelpAP, APTran);
                        }
                        else
                        {
                            HR_Master_User User = new HR_Master_User();
                            User.Username = DR["Username"].ToString();
                            User.Password = DR["Password"].ToString();
                            User.FirstName = DR["FirstName"] != null && DR["FirstName"].ToString() != "" ? DR["FirstName"].ToString() : null;
                            User.LastName = DR["LastName"] != null && DR["LastName"].ToString() != "" ? DR["LastName"].ToString() : null;
                            User.Email = DR["Email"] != null && DR["Email"].ToString() != "" ? DR["Email"].ToString() : null;

                            HR_Master_Position Position = ListPositions.Find(p => p.Code == DR["RoleId"].ToString());

                            if (Position != null)
                            {
                                User.PositionId = ListPositions.Find(p => p.Code == DR["RoleId"].ToString()).Id;
                            }
                            else
                            {
                                User.PositionId = null;
                            }

                            HR_Master_Department Department = ListDepartments.Find(p => p.Code == DR["DepartmentID"].ToString());
                            if (Department != null)
                            {
                                User.DepartmentId = ListDepartments.Find(p => p.Code == DR["DepartmentID"].ToString()).Id;
                            }
                            else
                            {
                                User.DepartmentId = null;
                            }

                            if (DR["BirthDate"] != null && DR["BirthDate"].ToString() != "")
                            {
                                User.BirthDate = Convert.ToDateTime(DR["BirthDate"]);
                            }
                            else
                            {
                                User.BirthDate = null;
                            }

                            User.Telephone = DR["Mobile"] != null && DR["Mobile"].ToString() != "" ? DR["Mobile"].ToString() : null;
                            User.Status = !Convert.ToBoolean(DR["IsDelete"]) ? "Active" : "InActive";
                            User.CreateById = 0;
                            User.CreateBy = TranBy;
                            User.CreateDate = TranDate;
                            User.ModifyById = 0;
                            User.ModifyBy = TranBy;
                            User.ModifyDate = TranDate;
                            User.ExecCommand.Insert(dbHelpAP, APTran);
                        }
                    }

                    dbHelpAP.CommitTransaction(APTran);
                }
                catch (Exception ex)
                {
                    dbHelpAP.RollbackTransaction(APTran);
                    throw ex;
                }

                ResponseCode = 200;
                ResponseData = new { status = true, message = "success" };
            }
            catch (Exception ex)
            {
                ErrrorMessage = ex.Message;
                ResponseData = new { status = false, message = ex.Message };
            }
            finally
            {
                ICON.Interface.Transaction.UpdateAPLog(Log.TranID, Newtonsoft.Json.JsonConvert.SerializeObject(ResponseData), ResponseCode, ErrrorMessage);
            }

            return ResponseData;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/testgettoken")]
        public object GenerateToken()
        {
            string username = "admin";
            int expireMinutes = 120;
            return Jwt.JwtManager.GenerateToken(username, expireMinutes);
        }
    }
}