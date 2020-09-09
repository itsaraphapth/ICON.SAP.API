using ICON.Framework.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICON.Interface
{
    public class Transaction
    {
        public static SAP_Interface_Log CreateSAPLog(
           string Module,
           string MethodName,
           string REMRefID,
           string REMRefDescription,
           string SAPRefID,
           string SAPRefDescription,
           string APIRequestData,
           string TranBy
           )
        {
            SAP_Interface_Log Log = new SAP_Interface_Log(ICON.Configuration.Database.REM_ConnectionString);

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            string Sql = $"select * from SAP_Interface_Log  where Module = '{Module}' and MethodName = '{MethodName}' and REMRefID = '{REMRefID}'";
            System.Data.DataTable DT = dbHelp.ExecuteDataTable(Sql);

            if (DT.Rows.Count > 0)
            {
                Log.ExecCommand.Load(DT.Rows[0]);
                Log.UpdateDate = DateTime.Now;
                Log.UpdateBy = Log.CreateBy;
                Log.ExecCommand.Update();
            }
            else
            {
                Log.Module = Module;
                Log.MethodName = MethodName;
                Log.REMRefID = REMRefID;
                Log.REMRefDescription = REMRefDescription;
                Log.SAPRefID = SAPRefID;
                Log.SAPRefDescription = SAPRefDescription;
                Log.APIRequestData = APIRequestData;
                Log.APIResponseCode = 500;
                Log.SAPStatusCode = null;
                Log.CreateDate = DateTime.Now;
                Log.CreateBy = TranBy;
                Log.UpdateDate = DateTime.Now;
                Log.UpdateBy = Log.CreateBy;
                Log.RetryCount = null;
                Log.ExecCommand.Insert();
            }

            return Log;
        }

        public static void UpdateSAPLog(int TranId, string REMRefID, string SAPRefID, string SAPRefNo, string SAPRefGLNo, int APIResponseCode,
            string APIErrorMessage, string SAPStatusCode, string SAPErrorMessage, List<SAP_Interface_Log_Detail> LogDetail)
        {
            SAP_Interface_Log Log = new SAP_Interface_Log(ICON.Configuration.Database.REM_ConnectionString, TranId);

            Log.REMRefID = !string.IsNullOrEmpty(REMRefID) ? REMRefID : Log.REMRefID;
            Log.SAPRefID = !string.IsNullOrEmpty(SAPRefID) ? SAPRefID : Log.SAPRefID;
            Log.SAPRefNo = !string.IsNullOrEmpty(SAPRefNo) ? SAPRefNo : Log.SAPRefNo;
            Log.SAPRefGLNo = !string.IsNullOrEmpty(SAPRefGLNo) ? SAPRefGLNo : Log.SAPRefGLNo;
            Log.APIResponseCode = APIResponseCode;
            Log.APIErrorMessage = !string.IsNullOrEmpty(APIErrorMessage) ? APIErrorMessage : Log.APIErrorMessage;
            Log.SAPStatusCode = !string.IsNullOrEmpty(SAPStatusCode) ? SAPStatusCode : Log.SAPStatusCode;
            Log.SAPErrorMessage = !string.IsNullOrEmpty(SAPErrorMessage) ? SAPErrorMessage : Log.SAPErrorMessage;
            Log.RetryCount = (Log.RetryCount == null ? 0 : Log.RetryCount + 1);

            Log.ExecCommand.Update();

            foreach (SAP_Interface_Log_Detail Item in LogDetail)
            {
                Item.InitCommand(ICON.Configuration.Database.REM_ConnectionString);
                Item.Module = Log.Module;
                Item.MethodName = Log.MethodName;
                Item.REMRefID = Log.REMRefID;
                Item.REMRefDescription = Log.REMRefDescription;
                Item.SAPRefDescription = Log.SAPRefDescription;
                Item.CreateBy = Log.UpdateBy;
                Item.CreateDate = Log.UpdateDate;
                Item.ModifyBy = Log.UpdateBy;
                Item.ModifyDate = Log.UpdateDate;
                Item.ExecCommand.Insert();
            }
        }

        public static void UpdateSAPLog(int TranId, string APIResponseData, int APIResponseCode, string APIErrorMessage)
        {
            SAP_Interface_Log Log = new SAP_Interface_Log(ICON.Configuration.Database.REM_ConnectionString, TranId);

            Log.APIResponseData = !string.IsNullOrEmpty(APIResponseData) ? APIResponseData : Log.APIResponseData;
            Log.APIResponseCode = APIResponseCode;
            Log.APIErrorMessage = !string.IsNullOrEmpty(APIErrorMessage) ? APIErrorMessage : Log.APIErrorMessage;

            Log.ExecCommand.Update();
        }


        public static AP_Interface_Log CreateAPLog(string Module, string MethodName, string APIRequestData, string TranBy)
        {
            AP_Interface_Log Log = new AP_Interface_Log(ICON.Configuration.Database.REM_ConnectionString);

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            Log.Module = Module;
            Log.MethodName = MethodName;
            Log.APIRequestData = APIRequestData;
            Log.APIResponseCode = 500;
            Log.CreateDate = DateTime.Now;
            Log.CreateBy = TranBy;
            Log.UpdateDate = DateTime.Now;
            Log.UpdateBy = Log.CreateBy;
            Log.ExecCommand.Insert();

            return Log;
        }

        public static void UpdateAPLog(int TranId, string APIResponseData, int APIResponseCode, string APIErrorMessage)
        {
            AP_Interface_Log Log = new AP_Interface_Log(ICON.Configuration.Database.REM_ConnectionString, TranId);

            Log.APIResponseData = !string.IsNullOrEmpty(APIResponseData) ? APIResponseData : Log.APIResponseData;
            Log.APIResponseCode = APIResponseCode;
            Log.APIErrorMessage = !string.IsNullOrEmpty(APIErrorMessage) ? APIErrorMessage : Log.APIErrorMessage;

            Log.ExecCommand.Update();
        }
    }
}