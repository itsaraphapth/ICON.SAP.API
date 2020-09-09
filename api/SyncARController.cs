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
    public class SyncARController : ApiController
    {
        public string ErrorMessage = "";
        public int SAPStatusCode = 0;
        public string SAPStatus = null;
        public string SAPErrorMessage = null;
        public string TranBy = "SAPApi";
        public string CardCode = "C00001";

        /// <summary>
        /// สร้าง Invoice
        /// </summary>
        /// <param name="data">
        /// {
        ///     "OPaymentID": "0000125-19020001"
        /// }
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/syncmasterbankaccount")]
        [AllowAnonymous]

        public object SyncBankAccount(dynamic Data)
        {
            DateTime TranDate = DateTime.Now;
            DBHelper DBHelper = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog("AR_REM", "SyncBankAccount", TranDate.ToString("yyyyMMdd_HHmmssfff"), "DATETIME", null, null, null, TranBy);

            int ResponseCode = 500;
            object ResponseData = null;
            string ErrrorMessage = string.Empty;

            try
            {
                string SQL = @"SELECT CompanyID,ISNULL(Value,'') DBName FROM Sys_Conf_RealEstate WHERE 1=1 AND CompanyID IS NOT NULL AND CompanyID <> '0' AND ISNULL(KEYNAME,'') = 'DBName' AND ISNULL(Value,'') <> ''";
                DataTable DT_Company = DBHelper.ExecuteDataTable(SQL);

                System.Data.IDbTransaction Tran = DBHelper.BeginTransaction();
                try
                {
                    string sql = "";
                    foreach (DataRow DR in DT_Company.Rows)
                    {
                        sql += $@"
UPDATE BA SET BA.GLAccountCode = SAPBA.GLAccount
FROM Sys_Master_BankAccount BA
INNER JOIN [{ DR["DBName"].ToString() }]..DSC1 SAPBA ON CONVERT(NUMERIC(18, 0), REPLACE(REPLACE(BA.BankAccount, '-', ''), ' ', '')) = CONVERT(NUMERIC(18, 0), REPLACE(REPLACE(SAPBA.Account, '-', ''), ' ', ''))
WHERE 1 = 1
";
                    }

                    DBHelper.ExecuteNonQuery(sql, Tran);

                    DBHelper.CommitTransaction(Tran);
                }
                catch (Exception ex)
                {
                    DBHelper.RollbackTransaction(Tran);
                    throw ex;
                }

                ResponseCode = 200;
                ResponseData = new { status = true, message = "success", data = new { TranID = Log.TranID } };
            }
            catch (Exception ex)
            {
                ErrrorMessage = ex.Message;
                ResponseData = new { status = false, message = ex.Message, data = new { TranID = Log.TranID } };
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(Log.TranID, Newtonsoft.Json.JsonConvert.SerializeObject(ResponseData), ResponseCode, ErrrorMessage);
            }
            return ResponseData;
        }

        [HttpPost]
        [Route("api/syncchartofaccount")]
        [AllowAnonymous]

        public object SyncChartOfAccount(dynamic Data)
        {
            DateTime TranDate = DateTime.Now;
            DBHelper DBHelper = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog("AR_REM", "SyncChartOfAccount", TranDate.ToString("yyyyMMdd_HHmmssfff"), "DATETIME", null, null, null, TranBy);

            int ResponseCode = 500;
            object ResponseData = null;
            string ErrrorMessage = string.Empty;

            try
            {
                string SQL = @"SELECT CompanyID,ISNULL(Value,'') DBName FROM Sys_Conf_RealEstate WHERE 1=1 AND CompanyID IS NOT NULL AND CompanyID <> '0' AND ISNULL(KEYNAME,'') = 'DBName' AND ISNULL(Value,'') <> ''";
                DataTable DT_Company = DBHelper.ExecuteDataTable(SQL);

                System.Data.IDbTransaction Tran = DBHelper.BeginTransaction();
                try
                {
                    string sql = "TRUNCATE TABLE Sys_ACC_ChartOfAccount";
                    DBHelper.ExecuteNonQuery(sql, Tran);

                    foreach (DataRow DR in DT_Company.Rows)
                    {
                        sql += $@"
INSERT INTO Sys_ACC_ChartOfAccount (CompanyID, AccountCode, AccountName, AccountType, Description, DebitCode, CreditCode, SAPWBSCode, COMWBSCode, ProfitCenter, CostCenter, SuffixText, TaxCode, isDelete, ModifyDate, ModifyBy, 
                         SpecialCode)
SELECT '{ DR["CompanyID"].ToString() }' CompanyID,FormatCode AccountCode,AcctName AccountName,'AR' AccountType,AcctName Description,'40' DebitCode,'50' CreditCode,'' SAPWBSCode,ISNULL(Accntntcod,'') COMWBSCode,'COMPANY' ProfitCenter,'' CostCenter,NULL SuffixText,'' TaxCode,'0' isDelete,GETDATE() ModifyDate,'SAP' ModifyBy,'' SpecialCode
FROM [{ DR["DBName"].ToString() }]..OACT WHERE FormatCode IS NOT NULL
";
                    }


                    sql += @"

UPDATE C SET C.AccountKey = 'CashOnHand' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'เงินสดในมือ'
UPDATE C SET C.AccountKey = 'ChequeOnHand' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'เช็ครับรอนำฝาก'
UPDATE C SET C.AccountKey = 'BankFee' FROM Sys_ACC_ChartOfAccount C  WHERE AccountName = 'ค่าธรรมเนียมธนาคาร'
UPDATE C SET C.AccountKey = 'OutputVAT' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'ภาษีขาย'
UPDATE C SET C.AccountKey = 'DeferVAT' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'ภาษีขายยังไม่ถึงกำหนด'
UPDATE C SET C.AccountKey = 'ClearingVAT' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'บัญชีพักภาษีขาย'
UPDATE C SET C.AccountKey = 'WithHoldingTax' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'บัญชีพักสินทรัพย์อื่น'
UPDATE C SET C.AccountKey = 'ExcessRevenue' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'รายได้อื่น'
UPDATE C SET C.AccountKey = 'ExcessUnearn' FROM Sys_ACC_ChartOfAccount C WHERE AccountName IN ('เงินรับล่วงหน้า','เงินรับล่วงหน้า/บัญชีพักอื่นๆ')
UPDATE C SET C.AccountKey = 'BankInterest' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'รายได้ดอกเบี้ยรับ'
UPDATE C SET C.AccountKey = 'PayInTransfer' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'เงินรับไม่ทราบผู้โอน'
UPDATE C SET C.AccountKey = 'WaitPayOut' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'บัญชีพักรอจ่ายคืน'
UPDATE C SET C.AccountKey = 'WaitPayIn' FROM Sys_ACC_ChartOfAccount C WHERE AccountName = 'บัญชีพักรอรับคืน'
";

                    DBHelper.ExecuteNonQuery(sql, Tran);

                    DBHelper.CommitTransaction(Tran);
                }
                catch (Exception ex)
                {
                    DBHelper.RollbackTransaction(Tran);
                    throw ex;
                }

                ResponseCode = 200;
                ResponseData = new { status = true, message = "success", data = new { TranID = Log.TranID } };
            }
            catch (Exception ex)
            {
                ErrrorMessage = ex.Message;
                ResponseData = new { status = false, message = ex.Message, data = new { TranID = Log.TranID } };
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(Log.TranID, Newtonsoft.Json.JsonConvert.SerializeObject(ResponseData), ResponseCode, ErrrorMessage);
            }
            return ResponseData;
        }


        [HttpPost]
        [Route("api/syncprofitcenter")]
        [AllowAnonymous]

        public object SyncProfitCenter(dynamic Data)
        {
            DateTime TranDate = DateTime.Now;
            DBHelper DBHelper = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog("AR_REM", "SyncProfitCenter", TranDate.ToString("yyyyMMdd_HHmmssfff"), "DATETIME", null, null, null, TranBy);

            int ResponseCode = 500;
            object ResponseData = null;
            string ErrrorMessage = string.Empty;

            try
            {
                string SQL = @"SELECT CompanyID,ISNULL(Value,'') DBName FROM Sys_Conf_RealEstate WHERE 1=1 AND CompanyID IS NOT NULL AND CompanyID <> '0' AND ISNULL(KEYNAME,'') = 'DBName' AND ISNULL(Value,'') <> ''";
                DataTable DT_Company = DBHelper.ExecuteDataTable(SQL);

                System.Data.IDbTransaction Tran = DBHelper.BeginTransaction();
                try
                {
                    string sql = "";
                    foreach (DataRow DR in DT_Company.Rows)
                    {
                        sql += $@"
UPDATE PJ SET PJ.ProfitCenter = '1' + PRC.PrcCode
FROM 
SC_Project PJ
INNER JOIN Sys_Master_Projects MPJ ON MPJ.ProjectID = PJ.ProjectID
LEFT JOIN [{ DR["DBName"].ToString() }]..OPRC PRC ON CONVERT(NVARCHAR(50),PJ.ProjectID) COLLATE Thai_CI_AS = CONVERT(NVARCHAR(50),PRC.PrcName) COLLATE Thai_CI_AS AND Prc.DimCode = 1
WHERE 1=1 AND MPJ.CompanyID = '{ DR["CompanyID"].ToString() }'
";
                    }

                    DBHelper.ExecuteNonQuery(sql, Tran);

                    DBHelper.CommitTransaction(Tran);
                }
                catch (Exception ex)
                {
                    DBHelper.RollbackTransaction(Tran);
                    throw ex;
                }

                ResponseCode = 200;
                ResponseData = new { status = true, message = "success", data = new { TranID = Log.TranID } };
            }
            catch (Exception ex)
            {
                ErrrorMessage = ex.Message;
                ResponseData = new { status = false, message = ex.Message, data = new { TranID = Log.TranID } };
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(Log.TranID, Newtonsoft.Json.JsonConvert.SerializeObject(ResponseData), ResponseCode, ErrrorMessage);
            }
            return ResponseData;
        }

        [HttpPost]
        [Route("api/syncpaymentterm")]
        [AllowAnonymous]

        public object SyncPaymentTerm(dynamic Data)
        {
            DateTime TranDate = DateTime.Now;
            DBHelper DBHelper = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog("AR_REM", "SyncPaymentTerm", TranDate.ToString("yyyyMMdd_HHmmssfff"), "DATETIME", null, null, null, TranBy);

            int ResponseCode = 500;
            object ResponseData = null;
            string ErrrorMessage = string.Empty;

            try
            {
                System.Text.StringBuilder SQL = new System.Text.StringBuilder();
                SQL.AppendLine("SELECT");
                SQL.AppendLine(" ITM.ItemCode");
                SQL.AppendLine(" , GAR.ARCMAct AS RevenuesAc");
                SQL.AppendLine(" , PT.AccountCode");
                SQL.AppendLine(" , ITM.ItemName");
                SQL.AppendLine(" , ITM.InvntItem");
                SQL.AppendLine(" , 'UPDATE PT SET PT.AccountCode = ''' + GAR.ARCMAct + ''', PT.RefAccountCode = ''' + ISNULL(ACT.Accntntcod,'') + ''' FROM Sys_ACC_PaymentTerm PT WHERE TermItemID = ''' + CONVERT(NVARCHAR(50),PT.TermItemID) + ''''");
                SQL.AppendLine("FROM");
                SQL.AppendLine(" SBO_ORIGINAL..OITM ITM");
                SQL.AppendLine(" LEFT JOIN SBO_ORIGINAL..OGAR GAR ON GAR.ItemCode = ITM.ItemCode");
                SQL.AppendLine(" LEFT JOIN CRMRE_PRD..Sys_ACC_PaymentTerm PT ON CONVERT(NVARCHAR(50),PT.MaterialCode) COLLATE Thai_CI_AS = CONVERT(NVARCHAR(50),ITM.ItemCode) COLLATE Thai_CI_AS");
                SQL.AppendLine(" LEFT JOIN SBO_ORIGINAL..OACT ACT ON GAR.ARCMAct = ACT.AcctCode");
                SQL.AppendLine("WHERE");
                SQL.AppendLine(" ITM.SellItem = 'Y'");

                // SBO_ORIGINAL
                DataTable DT_Company = DBHelper.ExecuteDataTable(SQL.ToString());

                System.Data.IDbTransaction Tran = DBHelper.BeginTransaction();
                try
                {
                    foreach (DataRow DR in DT_Company.Rows)
                    {

                    }

                    DBHelper.CommitTransaction(Tran);
                }
                catch (Exception ex)
                {
                    DBHelper.RollbackTransaction(Tran);
                    throw ex;
                }

                ResponseCode = 200;
                ResponseData = new { status = true, message = "success", data = new { TranID = Log.TranID } };
            }
            catch (Exception ex)
            {
                ErrrorMessage = ex.Message;
                ResponseData = new { status = false, message = ex.Message, data = new { TranID = Log.TranID } };
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(Log.TranID, Newtonsoft.Json.JsonConvert.SerializeObject(ResponseData), ResponseCode, ErrrorMessage);
            }
            return ResponseData;
        }
    }
}