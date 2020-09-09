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


namespace ICON.SAP.API.api
{
    public class REMController : ApiController
    {
        // GET: REM
        public string TranBy = "SAPApi";


        /// <summary>
        /// Sync User From REM to AP
        /// </summary>
        /// <param name="Data">Username</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/syncprojects")]
        public object SyncProjects(dynamic Data)
        {
            DateTime TranDate = DateTime.Now;
            DBHelper dbHelpRE = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            AP_Interface_Log Log = ICON.Interface.Transaction.CreateAPLog("REM", "SyncProjects", null, TranBy);

            int ResponseCode = 500;
            object ResponseData = null;
            string ErrrorMessage = string.Empty;

            try
            {
                string sql = string.Empty;

                sql = @"
SELECT ProjectID, 
       ProjectName, 
       ISNULL(IsDelete,0) IsDelete
FROM Sys_Master_Projects";

                DataTable DT_REMProjects = dbHelpRE.ExecuteDataTable(sql);

                DBHelper dbHelpAP = new DBHelper(ICON.Configuration.Database.AP_ConnectionString, null);
                System.Data.IDbTransaction APTran = dbHelpAP.BeginTransaction();
                try
                {
                    foreach (DataRow DR in DT_REMProjects.Rows)
                    {
                        sql = $"SELECT * FROM REM_Master_Project WHERE Code = '{DR["ProjectID"].ToString()}'";
                        DataTable DT_APProject = dbHelpAP.ExecuteDataTable(sql, APTran);

                        REM_Master_Project AP_Project = new REM_Master_Project();

                        if (DT_APProject.Rows.Count > 0)
                        {
                            AP_Project.ExecCommand.Load(DT_APProject.Rows[0]);

                            AP_Project.EditId = false;
                            AP_Project.Code = DR["ProjectID"].ToString();
                            AP_Project.Name = DR["ProjectName"].ToString();
                            AP_Project.Status = !Convert.ToBoolean(DR["IsDelete"]) ? "Active" : "InActive";
                            AP_Project.ModifyById = 0;
                            AP_Project.ModifyBy = TranBy;
                            AP_Project.ModifyDate = TranDate;
                            AP_Project.ExecCommand.Update(dbHelpAP, APTran);
                        }
                        else
                        {
                            AP_Project.Code = DR["ProjectID"].ToString();
                            AP_Project.Name = DR["ProjectName"].ToString();
                            AP_Project.Status = !Convert.ToBoolean(DR["IsDelete"]) ? "Active" : "InActive";
                            AP_Project.CreateById = 0;
                            AP_Project.CreateBy = TranBy;
                            AP_Project.CreateDate = TranDate;
                            AP_Project.ModifyById = 0;
                            AP_Project.ModifyBy = TranBy;
                            AP_Project.ModifyDate = TranDate;
                            AP_Project.ExecCommand.Insert(dbHelpAP, APTran);
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
                ResponseData =  new { status = true, message = "success" };
            }
            catch (Exception ex)
            {
                ErrrorMessage = ex.Message;
                ResponseData =  new { status = false, message = ex.Message };
            }
            finally
            {
                ICON.Interface.Transaction.UpdateAPLog(Log.TranID, Newtonsoft.Json.JsonConvert.SerializeObject(ResponseData), ResponseCode, ErrrorMessage);
            }
            return ResponseData;
        }
    }
}