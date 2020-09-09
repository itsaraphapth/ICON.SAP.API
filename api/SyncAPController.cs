using ICON.Framework.Provider;
using ICON.Interface;
using ICON.REM.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ICON.SAP.API
{
    public class SyncAPController : ApiController
    {
        public string ErrorMessage = "";
        public int SAPStatusCode = 0;
        public string SAPStatus = null;
        public string SAPErrorMessage = null;
        public string TranBy = "SAPApi";
        public string CardCode = "C00001";
        private ICON.SAP.API.SAPB1 oSAPB1(string CompanyDB = "")
        {
            ICON.Configuration.SAPServer SAPServer = ICON.Configuration.Database.SAP_Server;
            ICON.SAP.API.SAPB1 SAPB1 = new ICON.SAP.API.SAPB1(
                SAPServer.DBServer,
                SAPServer.DBServerType,
                string.IsNullOrEmpty(CompanyDB) ? SAPServer.CompanyDB : CompanyDB,
                SAPServer.UserName,
                SAPServer.Password,
                SAPServer.LicenseServer,
                SAPServer.SLDAddress,
                SAPServer.Language
                );
            return SAPB1;
        }

        /// <summary>
        /// Check Lock Date
        /// </summary>
        /// <param name="data">
        /// {
        ///     "ProjectID": "CRR",
        ///     "DocDate": "2019-01-13"
        /// }
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/checklockperiod")]
        [AllowAnonymous]

        public object CheckLockPeriod(dynamic data)
        {
            DateTime TranDate = DateTime.Now;
            DBHelper DBHelper = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog("AP_REM", "CheckLockPeriod", TranDate.ToString("yyyyMMdd_HHmmssfff"), "DATETIME", null, null, Newtonsoft.Json.JsonConvert.SerializeObject(data), TranBy);

            bool IsConnectSAP = false;
            int ResponseCode = 500;
            object ResponseData = null;
            string ErrrorMessage = string.Empty;
            ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();

            try
            {
                List<string> Errors = new List<string>();
                string ProjectID = (string)data.project_id;
                string DocDateStr = (string)data.doc_date;

                DateTime DocDate = DateTime.Now;
                DateTime.TryParse(DocDateStr, out DocDate);

                if (string.IsNullOrEmpty(ProjectID)) Errors.Add("missing value for parameter : 'project_id'");
                if (string.IsNullOrEmpty(DocDateStr)) Errors.Add("missing value for parameter : 'doc_date'");
                if (DocDate.ToString("yyyy-MM-dd") != DocDateStr) Errors.Add("invalid format for parameter : 'doc_date'");
                if (Errors.Count > 0) throw new Exception(string.Join("\n", Errors));

                System.Text.StringBuilder SQL = new System.Text.StringBuilder();
                SQL.AppendLine("SELECT");
                SQL.AppendLine("	C.CompanyID			    AS CompanyID");
                SQL.AppendLine("	, PJ.ProjectID			AS ProjectID");
                SQL.AppendLine("	, ISNULL(C.Value, '')	AS DBName");
                SQL.AppendLine("FROM");
                SQL.AppendLine("	Sys_Conf_RealEstate C");
                SQL.AppendLine("	LEFT JOIN Sys_Master_Projects PJ ON C.CompanyID = PJ.CompanyID");
                SQL.AppendLine("WHERE 1 = 1");
                SQL.AppendLine("	AND C.CompanyID IS NOT NULL");
                SQL.AppendLine("	AND C.CompanyID <> '0'");
                SQL.AppendLine("	AND C.KEYNAME = 'DBName'");
                SQL.AppendLine($"	AND PJ.ProjectID = '{ProjectID}'");
                DataTable DT_Company = DBHelper.ExecuteDataTable(SQL.ToString());
                if (DT_Company.Rows.Count == 0) throw new Exception("none config for connecting to SAP of project " + ProjectID);

                SAPB1 = oSAPB1(DT_Company.Rows[0]["DBName"].ToString());
                SAPB1.ConnectCompanyDB();
                IsConnectSAP = true;

                bool IsLock = SAPB1.GetIsLock(DocDate);
                
                object resp = new
                {
                    project_id = ProjectID,
                    doc_date = DocDateStr,
                    is_lock = IsLock
                };

                ResponseCode = 200;
                ResponseData = new { status = true, message = "success", data = resp };
            }
            catch (Exception ex)
            {
                ErrrorMessage = ex.Message;
                ResponseData = new { status = false, message = ex.Message };
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(Log.TranID, Newtonsoft.Json.JsonConvert.SerializeObject(ResponseData), ResponseCode, ErrrorMessage);
                if(IsConnectSAP) SAPB1.DisConnectCompanyDB();
            }
            return ResponseData;
        }
        
        [HttpPost]
        [Route("api/calculate_allocatepercent")]
        [AllowAnonymous]
        public object Calculate_AllocatePercent(dynamic data)
        {
            bool IsConnectSAP = false;
            int ResponseCode = 500;
            object ResponseData = null;
            string ErrrorMessage = string.Empty;
            ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();

            DateTime TranDate = DateTime.Now;
            DBHelper DBHelper = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog("AP_REM", "Calculate_AllocatePercent", TranDate.ToString("yyyyMMdd_HHmmssfff"), "DATETIME", null, null, Newtonsoft.Json.JsonConvert.SerializeObject(data), TranBy);
            
            try
            {
                List<string> Errors = new List<string>();
                int Year = 0;
                int Month = 0;
                string ProjectID = (string)data.project_id;

                Int32.TryParse((string)data.year, out Year);
                Int32.TryParse((string)data.month, out Month);
                
                //if (string.IsNullOrEmpty(ProjectID)) Errors.Add("missing value for parameter : 'project_id'");
                if (Year.ToString() != (string)data.year) Errors.Add("invalid format for parameter : 'year'");
                if (Month.ToString() != (string)data.month) Errors.Add("invalid format for parameter : 'month'");
                if (Errors.Count > 0) throw new Exception(string.Join("\n", Errors));


                DateTime NOW = DateTime.Today;
                DateTime SendDate = new DateTime(Year, Month, 1);
                int Start = ((SendDate.Year - NOW.Year) * 12) + SendDate.Month - NOW.Month;

                #region SQL
                System.Text.StringBuilder SQL = new System.Text.StringBuilder();
                SQL.AppendLine($"DECLARE @Start INT = {Start}");
                SQL.AppendLine($"DECLARE @End INT = {Start}");
                SQL.AppendLine("");
                SQL.AppendLine("CREATE TABLE #Temp (");
                SQL.AppendLine("	[ProjectID]				NVARCHAR(50)");
                SQL.AppendLine("	 ,[ProjectName]			NVARCHAR(MAX)");
                SQL.AppendLine("	 ,[TotalArea]			NUMERIC(18,2)");
                SQL.AppendLine("	 ,[CalculateArea]		NUMERIC(18,2)");
                SQL.AppendLine("	 ,[Ratio]				NUMERIC(18,2)");
                SQL.AppendLine("	 ,[CalculatePeriodM]	INT");
                SQL.AppendLine("	 ,[CalculatePeriodY]	INT");
                SQL.AppendLine("	 ,[CalculateDate]		DATETIME");
                SQL.AppendLine("	 ,[UsePeriodM]			INT");
                SQL.AppendLine("	 ,[UsePeriodY]			INT");
                SQL.AppendLine("	 ,[TransferInPeriod]	NUMERIC(18,2)");
                SQL.AppendLine("CONSTRAINT [PK_Temp] PRIMARY KEY CLUSTERED ");
                SQL.AppendLine("(");
                SQL.AppendLine("	[ProjectID] ASC,");
                SQL.AppendLine("	CalculateDate ASC");
                SQL.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                SQL.AppendLine(") ON [PRIMARY]");
                SQL.AppendLine("");
                SQL.AppendLine(";WITH ROWYear AS (");
                SQL.AppendLine("  SELECT DATEADD(MONTH,@Start - 1 ,DATEADD(DAY,1,EOMONTH(GETDATE())))  as n");
                SQL.AppendLine("");
                SQL.AppendLine("  UNION ALL");
                SQL.AppendLine("");
                SQL.AppendLine("  SELECT DATEADD(MONTH,1,n) FROM ROWYear WHERE n < DATEADD(MONTH,@End - 1,DATEADD(DAY,1,EOMONTH(GETDATE())))");
                SQL.AppendLine(")");
                SQL.AppendLine("");
                SQL.AppendLine("INSERT INTO #Temp(ProjectID,ProjectName,CalculatePeriodM,CalculatePeriodY,CalculateDate,UsePeriodM,UsePeriodY)");
                SQL.AppendLine("SELECT PJ.ProjectID,PJ.ProjectName, MONTH(R.n), YEAR(R.n), EOMONTH(R.n), MONTH(DATEADD(MONTH,1,R.n)), YEAR(DATEADD(MONTH,1,R.n))");
                SQL.AppendLine("FROM ROWYear R, Sys_Master_Projects PJ");
                SQL.AppendLine("WHERE 1=1");
                if (!string.IsNullOrEmpty(ProjectID))
                {
                    SQL.AppendLine($"    AND PJ.ProjectID = '{ProjectID}'");
                }
                SQL.AppendLine("option (maxrecursion 0)");
                SQL.AppendLine("");
                SQL.AppendLine("");
                SQL.AppendLine("UPDATE TMP SET TMP.TotalArea = PJ.TotalArea");
                SQL.AppendLine("FROM #Temp TMP");
                SQL.AppendLine("INNER JOIN ");
                SQL.AppendLine("(");
                SQL.AppendLine("	SELECT PJ.ProjectID");
                SQL.AppendLine("	, SUM(UN.TitledeedArea) TotalArea");
                SQL.AppendLine("	FROM");
                SQL.AppendLine("		Sys_Master_Projects	PJ");
                SQL.AppendLine("		INNER JOIN Sys_Master_Units UN ON UN.ProjectID = PJ.ProjectID");
                SQL.AppendLine("	WHERE");
                SQL.AppendLine("		1=1");
                SQL.AppendLine("		AND ISNULL(UN.isDelete,0) = 0");
                SQL.AppendLine("	GROUP BY PJ.ProjectID");
                SQL.AppendLine(") PJ ON TMP.ProjectID = PJ.ProjectID");
                SQL.AppendLine("");
                SQL.AppendLine("--SELECT TEMP1.ProjectID, TEMP1.CalculateDate");
                SQL.AppendLine("--, ISNULL(TEMP2.TitledeedArea,0) AS CalculateArea");
                SQL.AppendLine("--, ISNULL(TEMP2.TitledeedArea,0) / TEMP1.TotalArea * 100 AS Ratio");
                SQL.AppendLine("--, ISNULL(TEMP2.TitledeedArea,0) - ISNULL(TEMP2.TitledeedAreaOld,0) AS TransferInPeriod");
                SQL.AppendLine("UPDATE TEMP1 SET");
                SQL.AppendLine("	TEMP1.CalculateArea = ISNULL(TEMP2.TitledeedArea,0)");
                SQL.AppendLine("	, Ratio = ISNULL(TEMP2.TitledeedArea,0) / TEMP1.TotalArea * 100");
                SQL.AppendLine("	, TransferInPeriod = ISNULL(TEMP2.TitledeedArea,0) - ISNULL(TEMP2.TitledeedAreaOld,0)");
                SQL.AppendLine("FROM ");
                SQL.AppendLine("#Temp TEMP1");
                SQL.AppendLine("LEFT JOIN (");
                SQL.AppendLine("	SELECT TMP.ProjectID, TMP.CalculateDate");
                SQL.AppendLine("	, SUM(CASE WHEN TMP.CalculateDate >= ISNULL(CO.TransferDate,CO.ContractDate) THEN UN.TitledeedArea ELSE 0 END) TitledeedArea");
                SQL.AppendLine("	, SUM(CASE WHEN EOMONTH(DATEADD(MONTH,-1,TMP.CalculateDate)) >= ISNULL(CO.TransferDate,CO.ContractDate) THEN UN.TitledeedArea ELSE 0 END) TitledeedAreaOld");
                SQL.AppendLine("	FROM #Temp TMP");
                SQL.AppendLine("	INNER JOIN Sys_REM_Contracts CO ON TMP.ProjectID = CO.ProjectID ");
                SQL.AppendLine("	INNER JOIN Sys_Master_Units UN ON TMP.ProjectID = UN.ProjectID ");
                SQL.AppendLine("	WHERE ");
                SQL.AppendLine("		1=1");
                SQL.AppendLine("		AND ISNULL(CO.isTmp,0) = 0");
                SQL.AppendLine("		AND CO.ContractID IS NOT NULL");
                SQL.AppendLine("		AND UN.UnitID IS NOT NULL");
                SQL.AppendLine("		AND CO.SBUID = 'PM001'");
                SQL.AppendLine("		AND CO.UnitID = UN.UnitID");
                SQL.AppendLine("		AND ISNULL(UN.isDelete,0) = 0");
                SQL.AppendLine("		AND ISNULL(CO.SaleOrderStatus,'') NOT IN ('C')");
                SQL.AppendLine("		AND TMP.CalculateDate >= ISNULL(CO.TransferDate,CO.ContractDate)");
                SQL.AppendLine("	GROUP BY TMP.ProjectID, TMP.CalculateDate");
                SQL.AppendLine(") TEMP2 ON TEMP1.ProjectID = TEMP2.ProjectID AND TEMP1.CalculateDate = TEMP2.CalculateDate");
                SQL.AppendLine("");
                SQL.AppendLine("UPDATE TMP SET TMP.Ratio = 100");
                SQL.AppendLine("FROM #Temp TMP");
                SQL.AppendLine("INNER JOIN Sys_Master_Projects PJ ON TMP.ProjectID = PJ.ProjectID AND ISNULL(PJ.ProjectType,'') = 'C'");
                SQL.AppendLine("WHERE 1=1");
                SQL.AppendLine("    AND ISNULL(PJ.JuristicStatus,'') = 'A'");
                SQL.AppendLine("");
                SQL.AppendLine("SELECT [ProjectID]          AS project_id");
                SQL.AppendLine("	 ,[ProjectName]         AS project_name");
                SQL.AppendLine("	 ,[TotalArea]           AS total_area");
                SQL.AppendLine("	 ,[CalculateArea]       AS calculate_area");
                SQL.AppendLine("	 ,[Ratio]               AS ratio");
                SQL.AppendLine("	 ,[CalculatePeriodM]    AS calculate_period_m");
                SQL.AppendLine("	 ,[CalculatePeriodY]    AS calculate_period_y");
                SQL.AppendLine("	 ,[UsePeriodM]          AS use_period_m");
                SQL.AppendLine("	 ,[UsePeriodY]          AS use_period_y");
                SQL.AppendLine("	 ,[TransferInPeriod]    AS transfer_in_period");
                SQL.AppendLine("FROM #Temp");
                SQL.AppendLine("ORDER BY");
                SQL.AppendLine("	ProjectID, CalculateDate");
                SQL.AppendLine("");
                SQL.AppendLine("DROP TABLE #Temp");
                #endregion

                List<Dictionary<string,object>> AllocateList = GlobalDatabase.LoadDictByQuery(DBHelper, SQL.ToString());
                List<string> ProjectList = AllocateList.GroupBy(x => x["project_id"].ToString()).Select(x => x.Key).ToList();
                
                System.Text.StringBuilder SQL_Company = new System.Text.StringBuilder();
                SQL_Company.AppendLine("SELECT");
                SQL_Company.AppendLine("	C.CompanyID			    AS CompanyID");
                SQL_Company.AppendLine("	, PJ.ProjectID			AS ProjectID");
                SQL_Company.AppendLine("	, ISNULL(C.Value, '')	AS DBName");
                SQL_Company.AppendLine("FROM");
                SQL_Company.AppendLine("	Sys_Conf_RealEstate C");
                SQL_Company.AppendLine("	LEFT JOIN Sys_Master_Projects PJ ON C.CompanyID = PJ.CompanyID");
                SQL_Company.AppendLine("WHERE 1 = 1");
                SQL_Company.AppendLine("	AND C.CompanyID IS NOT NULL");
                SQL_Company.AppendLine("	AND C.CompanyID <> '0'");
                SQL_Company.AppendLine("	AND C.KEYNAME = 'DBName'");
                SQL_Company.AppendLine($"	AND PJ.ProjectID IN ('{string.Join("','", ProjectList)}')");
                List<Dictionary<string, object>> CompanyConfigList = GlobalDatabase.LoadDictByQuery(DBHelper, SQL_Company.ToString());
                List<string> CompanyList = CompanyConfigList.GroupBy(x => x["CompanyID"].ToString()).Select(x => x.Key).ToList();

                foreach (string Company in CompanyList)
                {
                    Dictionary<string, object> CompanyConfig = CompanyConfigList.Find(x => x["CompanyID"].ToString() == Company);
                    if (CompanyConfig == null || CompanyConfig["DBName"].ToString() == "")
                    {
                        bool IsLock = true;
                        foreach (Dictionary<string, object> Item in CompanyConfigList.FindAll(x => x["CompanyID"].ToString() == Company))
                        {
                            Item["IsLock"] = IsLock;
                        }
                    }
                    else
                    {
                        string DBName = CompanyConfig["DBName"].ToString();

                        SAPB1 = oSAPB1(DBName);
                        SAPB1.ConnectCompanyDB();
                        IsConnectSAP = true;

                        bool IsLock = SAPB1.GetIsLock(SendDate);

                        SAPB1.DisConnectCompanyDB();
                        IsConnectSAP = false;

                        foreach (Dictionary<string, object> Item in CompanyConfigList.FindAll(x => x["CompanyID"].ToString() == Company))
                        {
                            Item["IsLock"] = IsLock;
                        }
                    }
                }

                foreach (Dictionary<string, object> Allocate in AllocateList)
                {
                    string Project = Allocate["project_id"].ToString();
                    Dictionary<string, object> CompanyConfig = CompanyConfigList.Find(x => x["ProjectID"].ToString() == Project);
                    if (CompanyConfig == null) Allocate["is_lock"] = false;
                    else Allocate["is_lock"] = Convert.ToBoolean(CompanyConfig["IsLock"]);
                }


                object resp = new
                {
                    year = Year,
                    month = Month,
                    allocate_percent = AllocateList,
                };

                ResponseCode = 200;
                ResponseData = new { status = true, message = "success", data = resp };
            }
            catch (Exception ex)
            {
                ErrrorMessage = ex.Message;
                ResponseData = new { status = false, message = ex.Message };
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(Log.TranID, Newtonsoft.Json.JsonConvert.SerializeObject(ResponseData), ResponseCode, ErrrorMessage);
                if (IsConnectSAP) SAPB1.DisConnectCompanyDB();
            }
            return ResponseData;
        }
        

    }
}