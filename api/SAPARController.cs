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
    public class SAPARController : ApiController
    {
        public string ErrorMessage = "";
        public int SAPStatusCode = 0;
        public string SAPStatus = null;
        public string SAPErrorMessage = null;
        public string TranBy = "SAPApi";
        public string CardCode = "C00001";

        #region --- AR API ---
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
        [Route("api/createinvoice")]
        [AllowAnonymous]
        public object CreateSAPInvoice(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            try
            {
                string OPaymentID = null;

                if (Data != null)
                {
                    CheckRequetParams("invoice", Data);
                    OPaymentID = (string)Data.OPaymentID;
                }

                string Sql = GetSqlCommandListInvoice(OPaymentID);
                DataTable PostData = dbHelp.ExecuteDataTable(Sql);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {
                            if (dr["APIResponseCode"].ToString() == "200")
                            {
                                continue;
                            }

                            try
                            {
                                string DocNum = CreateInvoice(new { OPaymentID = dr["OPaymentID"].ToString() });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["OPaymentID"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new { status = true, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        /// <summary>
        /// ยกเลิก Invoice
        /// </summary>
        /// <param name="data">
        /// {
        ///     "OPaymentID": "0000125-19020001"
        /// }
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cancelinvoice")]
        [AllowAnonymous]
        public object CancelSAPInvoice(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            try
            {
                string OPaymentID = null;

                if (Data != null)
                {
                    CheckRequetParams("invoice", Data);
                    OPaymentID = (string)Data.OPaymentID;
                }

                string Sql = GetSqlCommandListCancelInvoice(OPaymentID);
                DataTable PostData = dbHelp.ExecuteDataTable(Sql);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {
                            if (dr["APIResponseCode"].ToString() == "200")
                            {
                                continue;
                            }

                            try
                            {
                                string DocNum = CancelInvoice(new { OPaymentID = dr["OPaymentID"].ToString() });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["OPaymentID"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new { status = true, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        /// <summary>
        /// จ่าย Invoice
        /// </summary>
        /// <param name="Data">
        /// {
        /// 	"DocEntry": "97",
        /// 	"ReceiptID": "RV-HVC01-19040001"
        /// }
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/paymentinvoice")]
        [AllowAnonymous]
        public object PaymentSAPInvoice(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            try
            {
                string ReceiptID = null;

                if (Data != null)
                {
                    CheckRequetParams("paymentinv", Data);
                    ReceiptID = (string)Data.ReceiptID;
                }

                string Sql = GetSqlCommandListReceipt(ReceiptID);
                DataTable PostData = dbHelp.ExecuteDataTable(Sql);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {
                            if (dr["APIResponseCode"].ToString() == "200")
                            {
                                continue;
                            }

                            try
                            {
                                if (dr["PaymentMethod"].ToString() == "PaymentInvoice")
                                {
                                    string PayDocEntry = PaymentInvoice(new { ReceiptID = dr["ReceiptID"].ToString() });
                                    ResultSuccess.Add(PayDocEntry);
                                }
                                else
                                {
                                    string PayDocEntry = PaymentBeforeInvoice(new { ReceiptID = dr["ReceiptID"].ToString() });
                                    ResultSuccess.Add(PayDocEntry);
                                }
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["ReceiptID"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { DocEntry = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new { status = true, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/cancelpaymentinvoice")]
        [AllowAnonymous]
        public object CancelPaymentSAPInvoice(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            try
            {
                string ReceiptID = null;

                if (Data != null)
                {
                    CheckRequetParams("paymentinv", Data);
                    ReceiptID = (string)Data.ReceiptID;
                }

                string Sql = GetSqlCommandCancelPayment(ReceiptID);
                DataTable PostData = dbHelp.ExecuteDataTable(Sql);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {
                            if (dr["APIResponseCode"].ToString() == "200")
                            {
                                continue;
                            }

                            try
                            {
                                string PayDocEntry = CancelPaymentInvoice(new { ReceiptID = dr["ReceiptID"].ToString(), DocEntry = dr["DocEntry"].ToString(), DBName = dr["DBName"].ToString() });
                                ResultSuccess.Add(PayDocEntry);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["ReceiptID"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { DocEntry = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new { status = true, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message };
            }
        }


        [HttpPost]
        [Route("api/cancelpaymentinvoiceall")]
        [AllowAnonymous]
        public object CancelPaymentSAPInvoiceALL(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            try
            {
                List<Dictionary<string, object>> Receipt = GlobalDatabase.LoadDictByQuery(dbHelp, "SELECT REMRefID FROM SAP_Interface_Log WHERE 1=1 AND MethodName IN ('PaymentInvoice','PaymentBeforeInvoice') AND CreateBy = 'SAPApi' GROUP BY REMRefID");
                foreach (Dictionary<string, object> item in Receipt)
                {
                    string ReceiptID = item["REMRefID"].ToString();

                    string Sql = GetSqlCommandCancelPayment(ReceiptID);
                    DataTable PostData = dbHelp.ExecuteDataTable(Sql);

                    if (PostData.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in PostData.Rows)
                            {
                                if (dr["APIResponseCode"].ToString() == "200")
                                {
                                    continue;
                                }

                                try
                                {
                                    string PayDocEntry = CancelPaymentInvoice(new { ReceiptID = dr["ReceiptID"].ToString(), DocEntry = dr["DocEntry"].ToString(), DBName = dr["DBName"].ToString() });
                                    ResultSuccess.Add(PayDocEntry);
                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["ReceiptID"].ToString() + " : " + ex.Message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }

                return new { status = true, message = "success", result = new { DocEntry = ResultSuccess, ErrorItem = ResultError } };
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        /// <summary>
        /// JournalEntry
        /// </summary>
        /// <param name="Data">
        /// {
        ///     "GLTransacID": ""
        /// }
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/createjournalentry")]
        [AllowAnonymous]
        public object CreateSAPJournalEntry(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            try
            {
                string GLVoucher = null;

                if (Data != null)
                {
                    CheckRequetParams("journalentry", Data);
                    GLVoucher = (string)Data.GLVoucher;
                }
                string Sql = GetSqlCommandListJournalEntry(GLVoucher);
                DataTable PostData = dbHelp.ExecuteDataTable(Sql);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {
                            if (dr["APIResponseCode"].ToString() == "200")
                            {
                                continue;
                            }

                            try
                            {
                                string TranID = CreateJournalEntry(new { GLVoucher = dr["GLVoucher"].ToString() });
                                ResultSuccess.Add(TranID);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["GLVoucher"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { TranID = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new { status = true, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        /// <summary>
        /// Credit Note
        /// </summary>
        /// <param name="Data">
        /// {
        ///     "CreditNoteID": ""
        /// }
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/creditmemo")]
        [AllowAnonymous]
        public object CreateSAPMemo(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            try
            {
                string CreditNoteID = null;

                if (Data != null)
                {
                    CheckRequetParams("creditmemo", Data);
                    CreditNoteID = (string)Data.CreditNoteID;
                }

                string Sql = GetSqlCommandListCreditMemo(CreditNoteID);
                DataTable PostData = dbHelp.ExecuteDataTable(Sql);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {
                            if (dr["APIResponseCode"].ToString() == "200")
                            {
                                continue;
                            }

                            try
                            {
                                string DocNum = CreateCreditMemo(new { CreditNoteID = dr["CreditNoteID"].ToString() });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["CreditNoteID"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new { status = true, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/cancelmemo")]
        [AllowAnonymous]
        public object CancelSAPMemo(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            try
            {
                string CreditNoteID = null;

                if (Data != null)
                {
                    CheckRequetParams("creditmemo", Data);
                    CreditNoteID = (string)Data.CreditNoteID;
                }

                string Sql = GetSqlCommandListCancelMemo(CreditNoteID);
                DataTable PostData = dbHelp.ExecuteDataTable(Sql);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {
                            if (dr["APIResponseCode"].ToString() == "200")
                            {
                                continue;
                            }

                            try
                            {
                                string DocNum = CancelCreditMemo(new { CreditNoteID = dr["CreditNoteID"].ToString() });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["CreditNoteID"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new { status = true, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        /// <summary>
        /// Good Receipt PO
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/creategoodreceipt")]
        [AllowAnonymous]
        public object CreateSAPGoodReceipt(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            try
            {
                string OPaymentID = null;

                if (Data != null)
                {
                    CheckRequetParams("goodreceipt", Data);
                    OPaymentID = (string)Data.OPaymentID;
                }

                string Sql = GetSqlCommandListGoodReceipt(OPaymentID);
                DataTable PostData = dbHelp.ExecuteDataTable(Sql);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {
                            if (dr["APIResponseCode"].ToString() == "200")
                            {
                                continue;
                            }

                            try
                            {
                                string DocNum = CreateGoodReceipt(new { OPaymentID = dr["OPaymentID"].ToString() });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["OPaymentID"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new { status = true, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message };
            }
        }
        #endregion

        #region --- AR Function ---
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
        private void CheckRequetParams(string ApiType, dynamic Data)
        {
            List<string> Errors = new List<string>();

            if (Data == null)
            {
                throw new Exception("missing value for parameter : 'Data'");
            }

            switch (ApiType)
            {
                case "invoice":
                    if (string.IsNullOrEmpty((string)Data.OPaymentID))
                    {
                        Errors.Add("missing value for parameter : 'OPaymentID'");
                    }
                    break;
                case "paymentinv":
                    if (string.IsNullOrEmpty((string)Data.ReceiptID))
                    {
                        Errors.Add("missing value for parameter : 'ReceiptID'");
                    }
                    break;
                case "journalentry":
                    if (string.IsNullOrEmpty((string)Data.GLVoucher))
                    {
                        Errors.Add("missing value for parameter : 'GLVoucher'");
                    }
                    break;
                case "creditmemo":
                    if (string.IsNullOrEmpty((string)Data.CreditNoteID))
                    {
                        Errors.Add("missing value for parameter : 'CreditNoteID'");
                    }
                    break;
                case "goodreceipt":
                    break;
            }

            if (Errors.Count > 0)
            {
                throw new Exception(string.Join("\n", Errors));
            }
        }

        #region ### Invoice ####
        // *** สร้าง ***
        private string GetSqlCommandListInvoice(string OPaymentID)
        {
            string sql = $@"
SELECT 
       OP.OPaymentID, 
       OP.CompanyID,
       ISNULL(SAP.APIResponseCode,'500') APIResponseCode,
       SAP.SAPRefNo AS APIResponseData
FROM 
	Sys_FI_OtherPayment OP
    LEFT JOIN Sys_Master_Projects PJ ON PJ.ProjectID = OP.ProjectID
    LEFT JOIN Sys_Master_Units U ON U.UnitID = OP.UnitID
                                     AND ISNULL(U.IsDelete, 0) = 0
	LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND OP.CompanyID = RE.CompanyID
    LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = OP.OPaymentID 
                                         AND SAP.Module = 'Invoice' 
                                         AND SAP.MethodName = 'CreateInvoice'
WHERE
    1=1
    AND CASE
            WHEN OP.Status = 'A' THEN 1 
            WHEN OP.Status = 'C' AND Approve2Date IS NOT NULL THEN 1 
            ELSE 0
        END = 1
    AND ISNULL(OP.IsInvoice,0) = 1
    AND ISNULL(SAP.APIResponseCode,'500') = '500'
    AND ISNULL(RE.Value,'') <> '' ";

            if (!string.IsNullOrEmpty(OPaymentID))
            {
                sql += $" AND OP.OPaymentID = '{OPaymentID}'";
            }

            sql += " GROUP BY OP.OPaymentID, OP.CompanyID, ISNULL(SAP.APIResponseCode,'500'), SAP.SAPRefNo";
            sql += " ORDER BY OP.CompanyID, OP.OPaymentID";

            return sql;
        }
        private string GetSqlCommandCreateInvoice(string OPaymentID)
        {
            string sql = $@"
SELECT 
       ROW_NUMBER() OVER(Partition By OP.OPaymentID ORDER BY TM.ContractTermID ASC) - 1 AS LineNumber,
       OP.OPaymentID,
       OP.InvoiceNo,
       OP.TransecDate AS DocDate,
       OP.DueDate AS DueDate,
       CASE WHEN ISNULL(TM.VATAmount,0) = 0 THEN 0 ELSE ISNULL(OP.IsDeferVat, 0) END IsDeferVat,
       TM.BaseAmount BaseAmount,
       TM.Amount PriceAfterVAT,
       TX.StartDate,
       TX.EndDate,
       ISNULL(TM.InterfaceVATCode, '') InterfaceVATCode,
       ISNULL(OP.CustomerName, '') CustomerName,
       ISNULL(OP.AddressNo,'') UnitNumber, 
       OP.ProjectID,
       OP.CompanyID,
       RE.Value AS DBName,
       PJ.ProjectName, 
       ISNULL(OP.TaxID, '') TaxID, 
       ISNULL(OP.Address, '') Address, 
       ISNULL(PT.AccountCode, '') AccountCode,
       ISNULL(PT.TermName,'') TermName,
       ISNULL(PT.MaterialCode,'') MaterialCode, 
       ISNULL(SAP.APIResponseCode,'500') APIResponseCode,
       ISNULL(SAP.SAPRefNo,'') APIResponseData
FROM 
	Sys_FI_OtherPayment OP
    INNER JOIN Sys_REM_ContractTerm TM ON OP.OPaymentID = TM.ReferenceID
                                          AND TM.ReferenceType = OP.OPaymentType
    INNER JOIN Sys_REM_ContractTerm_Extension TX ON TX.ContractTermID = TM.ContractTermID
    LEFT JOIN Sys_Master_Projects PJ ON PJ.ProjectID = OP.ProjectID
    LEFT JOIN Sys_Master_Units U ON U.UnitID = OP.UnitID
                                     AND ISNULL(U.IsDelete, 0) = 0
    LEFT JOIN Sys_ACC_PaymentTerm PT ON TM.TermID = PT.TermID
                                         AND TM.TermGroup = pt.TermGroup
                                         AND ISNULL(pt.isdelete, 0) = 0
	LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND OP.CompanyID = RE.CompanyID
    LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = OP.OPaymentID 
                                         AND SAP.Module = 'Invoice' 
                                         AND SAP.MethodName = 'CreateInvoice'
WHERE 
    1=1
    AND CASE
            WHEN OP.Status = 'A' THEN 1 
            WHEN OP.Status = 'C' AND Approve2Date IS NOT NULL THEN 1 
            ELSE 0
        END = 1
    AND ISNULL(SAP.APIResponseCode,'500') = '500'
    AND ISNULL(RE.Value,'') <> '' ";

            if (!string.IsNullOrEmpty(OPaymentID))
            {
                sql += $" AND OP.OPaymentID = '{OPaymentID}'";
            }

            return sql;
        }
        private string CreateInvoice(dynamic Data)
        {
            string OPaymentID = (string)Data.OPaymentID;
            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            string InvNo = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "Invoice",
                    "CreateInvoice",
                    OPaymentID,
                    "Sys_FI_OtherPayment.OPaymentID",
                    DocEntry,
                    "OINV.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                string sql = GetSqlCommandCreateInvoice(OPaymentID);
                DataTable dt = dbHelp.ExecuteDataTable(sql, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document
                {
                    CardCode = CardCode,
                    PostingDate = Convert.ToDateTime(dt.Rows[0]["DocDate"]),
                    DocDueDate = Convert.ToDateTime(dt.Rows[0]["DueDate"]),
                    DocumentDate = Convert.ToDateTime(dt.Rows[0]["DocDate"]),
                    DocType = SAPbobsCOM.BoObjectTypes.oInvoices,
                    UDF_RefNo = dt.Rows[0]["OPaymentID"].ToString(),
                    UDF_CustName = dt.Rows[0]["CustomerName"].ToString(),
                    UDF_UnitRef = dt.Rows[0]["UnitNumber"].ToString(),
                    UDF_Project = dt.Rows[0]["ProjectID"].ToString(),
                    UDF_ProjectName = dt.Rows[0]["ProjectName"].ToString(),
                    UDF_TaxID = dt.Rows[0]["TaxID"].ToString(),
                    UDF_Address = dt.Rows[0]["Address"].ToString(),
                    UDF_InvNo = dt.Rows[0]["InvoiceNo"].ToString(),
                    IsDeferVAT = Convert.ToBoolean(dt.Rows[0]["IsDeferVAT"])
                };

                foreach (DataRow dr in dt.Rows)
                {
                    string ItemDescription = dr["TermName"].ToString();

                    if (!string.IsNullOrEmpty(dr["StartDate"].ToString()) && !string.IsNullOrEmpty(dr["EndDate"].ToString()))
                    {
                        ItemDescription += " (" + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["StartDate"])) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EndDate"])) + ")";
                    }

                    doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        LineNumber = dr["LineNumber"].ToString(),
                        ItemCode = dr["MaterialCode"].ToString(),
                        ItemDescription = ItemDescription,
                        Qty = 1,
                        TaxCode = dr["InterfaceVATCode"].ToString(),
                        UnitPrice = dr["BaseAmount"].ToString(),
                        UnitAfterPrice = Convert.ToDouble(dr["PriceAfterVAT"])
                    });
                }

                try
                {
                    SAPB1.CreateDocument(doc, out DocEntry, out DocNum, out GLDocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                dbHelp.CommitTransaction(trans);
                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message;
                dbHelp.RollbackTransaction(trans);
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                    OPaymentID,
                    DocEntry,
                    DocNum,
                    GLDocNum,
                    (int)ResponseCode,
                    ErrorMessage,
                    SAPStatus,
                    SAPErrorMessage,
                    LogDetail
                    );
            }
        }

        // *** ยกเลิก ***
        private string GetSqlCommandListCancelInvoice(string OPaymentID)
        {
            string sql = $@"
SELECT 
       OP.OPaymentID, 
       OP.CompanyID,
       RE.Value AS DBName,
       ISNULL(SAP1.APIResponseCode,'500') APIResponseCode,
       SAP1.SAPRefNo AS APIResponseData,
       SAP.SAPRefID,
       OP.TransecDate
FROM 
	Sys_FI_OtherPayment OP
    LEFT JOIN Sys_Master_Projects PJ ON PJ.ProjectID = OP.ProjectID
    LEFT JOIN Sys_Master_Units U ON U.UnitID = OP.UnitID
                                     AND ISNULL(U.IsDelete, 0) = 0
	LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND OP.CompanyID = RE.CompanyID
    INNER JOIN SAP_Interface_Log SAP ON SAP.REMRefID = OP.OPaymentID 
                                         AND SAP.Module = 'Invoice' 
                                         AND SAP.MethodName = 'CreateInvoice'
	LEFT JOIN SAP_Interface_Log SAP1 ON SAP1.REMRefID = OP.OPaymentID 
                                         AND SAP1.Module = 'Invoice' 
                                         AND SAP1.MethodName = 'CancelInvoice'
WHERE 
    1=1
    AND OP.Status = 'C'
    AND ISNULL(SAP.APIResponseCode,'500') = '200'
    AND ISNULL(SAP1.APIResponseCode,'500') = '500' 
    AND ISNULL(RE.Value,'') <> ''";

            if (!string.IsNullOrEmpty(OPaymentID))
            {
                sql += $" AND OP.OPaymentID = '{OPaymentID}'";
            }

            sql += " GROUP BY OP.OPaymentID, OP.CompanyID, RE.Value, ISNULL(SAP1.APIResponseCode,'500'), SAP1.SAPRefNo, SAP.SAPRefID, OP.TransecDate";

            return sql;
        }
        private string CancelInvoice(dynamic Data)
        {
            string OPaymentID = (string)Data.OPaymentID;
            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            string InvNo = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "Invoice",
                    "CancelInvoice",
                    OPaymentID,
                    "Sys_FI_OtherPayment.OPaymentID",
                    DocEntry,
                    "OINV.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                string sql = GetSqlCommandListCancelInvoice(OPaymentID);
                DataTable dt = dbHelp.ExecuteDataTable(sql, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();

                Doc.DocEntry = Convert.ToInt32(dt.Rows[0]["SAPRefID"]);
                Doc.DocType = SAPbobsCOM.BoObjectTypes.oInvoices;
                Doc.DocumentDate = Convert.ToDateTime(dt.Rows[0]["TransecDate"]);

                try
                {
                    SAPB1.CancelDocument(Doc, out DocEntry, out DocNum, out GLDocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                dbHelp.CommitTransaction(trans);
                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message + " " + ex.StackTrace;
                dbHelp.RollbackTransaction(trans);
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                    OPaymentID,
                    DocEntry,
                    DocNum,
                    GLDocNum,
                    (int)ResponseCode,
                    ErrorMessage,
                    SAPStatus,
                    SAPErrorMessage,
                    LogDetail
                    );
            }
        }
        #endregion

        #region ### Payment ###
        // *** สร้าง ***
        private string GetSqlCommandListReceipt(string ReceiptID)
        {
            string sql = $@"
SELECT 
       --OP.OPaymentID, 
       ISNULL(PA_SAP.APIResponseCode,'500') APIResponseCode,
       PA_SAP.SAPRefNo AS APIResponseData,
	   P.ReceiptID,
	   P.CompanyID,
       CASE WHEN ISNULL(R.ReceiptDate,P.PaymentDate) >= OP.TransecDate THEN 'PaymentInvoice' ELSE 'PaymentBeforeInvoice' END PaymentMethod
	   --INV_SAP.SAPRefID DocEntry
FROM 
	Sys_FI_OtherPayment OP
	INNER JOIN Sys_FI_Payment P ON P.ReferenceID = OP.OPaymentID
    INNER JOIN SAP_Interface_Log INV_SAP ON INV_SAP.REMRefID = OP.OPaymentID 
                                         AND INV_SAP.Module = 'Invoice' 
                                         AND INV_SAP.MethodName = 'CreateInvoice'
    LEFT JOIN Sys_FI_Receipt R ON P.ReceiptID = R.ReceiptID
	LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND P.CompanyID = RE.CompanyID
    LEFT JOIN SAP_Interface_Log PA_SAP ON PA_SAP.REMRefID = P.ReceiptID 
                                         AND PA_SAP.Module = 'Payment' 
                                         AND PA_SAP.MethodName IN ('PaymentInvoice','PaymentBeforeInvoice')
                                         AND PA_SAP.MethodName =  CASE WHEN ISNULL(R.ReceiptDate,P.PaymentDate) >= OP.TransecDate THEN 'PaymentInvoice' ELSE 'PaymentBeforeInvoice' END
WHERE 
    1=1
    AND OP.Status = 'A'
    AND P.Status = 'P'
    AND ISNULL(P.DepositID, '') <> ''
    AND ISNULL(INV_SAP.APIResponseCode,'200') = '200' 
    AND ISNULL(PA_SAP.APIResponseCode,'500') = '500'
    AND ISNULL(RE.Value,'') <> ''";

            if (!string.IsNullOrEmpty(ReceiptID))
            {
                sql += $" AND P.ReceiptID = '{ReceiptID}'";
            }

            sql += @"
GROUP BY 
    --OP.OPaymentID, 
    ISNULL(PA_SAP.APIResponseCode, '500'),
    PA_SAP.SAPRefNo,
    P.ReceiptID,
	P.CompanyID,
    CASE WHEN ISNULL(R.ReceiptDate,P.PaymentDate) >= OP.TransecDate THEN 'PaymentInvoice' ELSE 'PaymentBeforeInvoice' END
    --INV_SAP.SAPRefID";

            return sql;
        }
        private string GetSqlCommandCreatePayment_BAK(string ReceiptID)
        {
            string sql = $@"
SELECT 
       P.ReferenceID,
	   RE.Value AS DBName,
	   SUM(P.Amount) Amount,
       ISNULL(P.DepositDate,P.PaymentDate) as TransferDate,
       ISNULL(RC.ReceiptDate,P.PaymentDate) as ReceiptDate,
       ISNULL(PT.UnEarnAccountCode,'') as UnEarnAccountCode,
       ISNULL(SAP.APIResponseCode,'500') APIResponseCode,
       SAP.SAPRefNo AS APIResponseData,
       BA.GLAccountCode,
	   INV_SAP.SAPRefID DocEntry
FROM 
	Sys_FI_Payment P
    INNER JOIN Sys_Master_BankAccount BA ON P.BankAccID = BA.BankAccountID
    LEFT JOIN Sys_FI_Receipt RC ON RC.ReceiptID = P.ReceiptID
	INNER JOIN SAP_Interface_Log INV_SAP ON INV_SAP.REMRefID = P.ReferenceID 
                                         AND INV_SAP.Module = 'Invoice' 
                                         AND INV_SAP.MethodName = 'CreateInvoice'
	LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND P.CompanyID = RE.CompanyID
    LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = P.ReceiptID 
                                         AND SAP.Module = 'Payment' 
                                         AND SAP.MethodName = 'PaymentInvoice'
    LEFT JOIN Sys_ACC_PaymentTerm PT ON P.TermID = PT.TermID AND P.TermGroup = PT.TermGroup
WHERE 
    1=1
    AND P.Status = 'P'
    AND ISNULL(P.DepositID, '') <> '' 
    AND ISNULL(SAP.APIResponseCode,'500') = '500'
    AND ISNULL(RE.Value,'') <> ''
    ";

            if (!string.IsNullOrEmpty(ReceiptID))
            {
                sql += $" AND P.ReceiptID = '{ReceiptID}' ";
            }

            sql += @"
GROUP BY
	   P.ReferenceID,
       RE.Value,
	   ISNULL(P.DepositDate,P.PaymentDate),
	   ISNULL(RC.ReceiptDate,P.PaymentDate),
       ISNULL(PT.UnEarnAccountCode,''),
       ISNULL(SAP.APIResponseCode,'500'),
       SAP.SAPRefNo,
       BA.GLAccountCode,
	   INV_SAP.SAPRefID";

            return sql;
        }
        private string GetSqlCommandCreatePayment(string ReceiptID)
        {
            string sql = $@"
SELECT P.ReferenceID
	, RE.Value AS DBName
	, SUM(P.Amount - ISNULL(P.WHTAmount,0)) Amount
	, SUM(ISNULL(P.WHTAmount,0)) WHTAmount
	, CASE WHEN P.PaymentType = 'BP'
		THEN BP.BankMainDate
		ELSE ISNULL(P.DepositDate, P.PaymentDate)
	  END AS TransferDate
	, ISNULL(RC.ReceiptDate, P.PaymentDate) AS ReceiptDate
	, ISNULL(OP.TransecDate,'') TransecDate
	, P.PaymentType
	, ISNULL(PT.UnEarnAccountCode,'') NewIncomingPayment_Before
	, CASE
		WHEN P.TermID = 'Z1' THEN ISNULL(COAZ1.AccountCode,'')
		WHEN P.TermID = 'CR' THEN ISNULL(PT.AccountCode,'')
	  END NewIncomingPayment
	, CR.CardNumber
	, ISNULL(CR.CardExpire, '') CardExpire
	, CQ.ChequeNumber
	, CQ.ChequeDate
	, ISNULL(PT.UnEarnAccountCode, '') AS UnEarnAccountCode
	, ISNULL(SAP.APIResponseCode, '500') APIResponseCode
	, SAP.SAPRefNo AS APIResponseData
	, CASE 
		WHEN P.PaymentType = 'CA'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'CR'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'DB'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'CC'
			THEN ISNULL(COACQ.AccountCode,'')
		WHEN P.PaymentType = 'CQ'
			THEN ISNULL(COACQ.AccountCode,'')
		WHEN P.PaymentType = 'DC'
			THEN ISNULL(COADC.AccountCode,'')
		WHEN P.PaymentType = 'Z1'
			THEN ISNULL(COAZ1.AccountCode,'')
		ELSE BA.GLAccountCode
	  END GLAccountCode
    , ISNULL(COAWHT.AccountCode,'') WHTAccountCode
	, INV_SAP.SAPRefID DocEntry
FROM Sys_FI_Payment P
LEFT JOIN Sys_Master_BankAccount BA ON P.BankAccID = BA.BankAccountID
LEFT JOIN Sys_FI_Receipt RC ON RC.ReceiptID = P.ReceiptID
INNER JOIN SAP_Interface_Log INV_SAP ON INV_SAP.REMRefID = P.ReferenceID
										AND INV_SAP.Module = 'Invoice'
										AND INV_SAP.MethodName = 'CreateInvoice'
LEFT JOIN Sys_FI_CreditCard CR ON CR.PaymentID = P.PayDetailID
LEFT JOIN Sys_FI_Cheque CQ ON CQ.ChequeID = P.PayDetailID
LEFT JOIN Sys_FI_BillPaymentDetail BPD ON BPD.ID = P.PayDetailID
LEFT JOIN Sys_FI_BillPayment BP ON BP.ID = BPD.BillPaymentID
LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND P.CompanyID = RE.CompanyID
LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = P.ReceiptID
									AND SAP.Module = 'Payment'
									AND SAP.MethodName = 'PaymentInvoice'
LEFT JOIN Sys_ACC_PaymentTerm PT ON P.TermID = PT.TermID AND P.TermGroup = PT.TermGroup
LEFT JOIN Sys_FI_OtherPayment OP ON P.ReferenceID = OP.OPaymentID
LEFT JOIN Sys_ACC_ChartOfAccount COACA ON P.CompanyID = COACA.CompanyID AND COACA.AccountKey = 'CashOnHand'
LEFT JOIN Sys_ACC_ChartOfAccount COACQ ON P.CompanyID = COACQ.CompanyID AND COACQ.AccountKey = 'ChequeOnHand'
LEFT JOIN Sys_ACC_ChartOfAccount COADC ON P.CompanyID = COADC.CompanyID AND COADC.AccountKey = 'Promotion'
LEFT JOIN Sys_ACC_ChartOfAccount COAZ1 ON P.CompanyID = COAZ1.CompanyID AND COAZ1.AccountKey = 'ExcessRevenue'
LEFT JOIN Sys_ACC_ChartOfAccount COAWHT ON P.CompanyID = COAWHT.CompanyID AND COAWHT.AccountKey = 'WithHoldingTax'
WHERE 1 = 1
	--AND P.STATUS = 'P'
	--AND ISNULL(P.DepositID, '') <> ''
    AND ISNULL(P.STATUS,'') <> 'C'
    AND CASE WHEN P.STATUS <> 'P' AND PaymentType = 'TR' THEN 0 ELSE 1 END = 1
	AND ISNULL(SAP.APIResponseCode, '500') = '500'
	AND ISNULL(RE.Value, '') <> ''
	AND CASE WHEN ISNULL(RC.ReceiptDate,P.PaymentDate) >= OP.TransecDate THEN 'PaymentInvoice' ELSE 'PaymentBeforeInvoice' END = 'PaymentInvoice'
    ";

            if (!string.IsNullOrEmpty(ReceiptID))
            {
                sql += $" AND P.ReceiptID = '{ReceiptID}' ";
            }

            sql += @"
GROUP BY P.ReferenceID
	, RE.Value
	, CASE WHEN P.PaymentType = 'BP'
		THEN BP.BankMainDate
		ELSE ISNULL(P.DepositDate, P.PaymentDate)
	  END
	, ISNULL(RC.ReceiptDate, P.PaymentDate)
	, ISNULL(OP.TransecDate,'')
	, P.PaymentType
	, ISNULL(PT.UnEarnAccountCode,'')
	, CASE
		WHEN P.TermID = 'Z1' THEN ISNULL(COAZ1.AccountCode,'')
		WHEN P.TermID = 'CR' THEN ISNULL(PT.AccountCode,'')
	  END 
	, CR.CardNumber
	, ISNULL(CR.CardExpire, '')
	, CQ.ChequeNumber
	, CQ.ChequeDate
	, ISNULL(PT.UnEarnAccountCode, '')
	, ISNULL(SAP.APIResponseCode, '500')
	, SAP.SAPRefNo
	, CASE 
		WHEN P.PaymentType = 'CA'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'CR'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'DB'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'CC'
			THEN ISNULL(COACQ.AccountCode,'')
		WHEN P.PaymentType = 'CQ'
			THEN ISNULL(COACQ.AccountCode,'')
		WHEN P.PaymentType = 'DC'
			THEN ISNULL(COADC.AccountCode,'')
		WHEN P.PaymentType = 'Z1'
			THEN ISNULL(COAZ1.AccountCode,'')
		ELSE BA.GLAccountCode
	  END
    , ISNULL(COAWHT.AccountCode,'')
	, INV_SAP.SAPRefID";

            return sql;
        }
        private string GetSqlCommandCreatePaymentBefore(string ReceiptID)
        {
            string sql = $@"
SELECT P.ReferenceID
	, RE.Value AS DBName
	, SUM(P.Amount - ISNULL(P.WHTAmount,0)) Amount
	, SUM(ISNULL(P.WHTAmount,0)) WHTAmount
	, CASE WHEN P.PaymentType = 'BP'
		THEN BP.BankMainDate
		ELSE ISNULL(P.DepositDate, P.PaymentDate)
	  END AS TransferDate
	, ISNULL(RC.ReceiptDate, P.PaymentDate) AS ReceiptDate
	, ISNULL(OP.TransecDate,'') TransecDate
	, P.PaymentType
	, ISNULL(PT.UnEarnAccountCode,'') NewIncomingPayment_Before
	, CASE
		WHEN P.TermID = 'Z1' THEN ISNULL(COAZ1.AccountCode,'')
		WHEN P.TermID = 'CR' THEN ISNULL(PT.AccountCode,'')
	  END NewIncomingPayment
	, CR.CardNumber
	, ISNULL(CR.CardExpire, '') CardExpire
	, CQ.ChequeNumber
	, CQ.ChequeDate
	, ISNULL(PT.UnEarnAccountCode, '') AS UnEarnAccountCode
	, ISNULL(SAP.APIResponseCode, '500') APIResponseCode
	, SAP.SAPRefNo AS APIResponseData
	, CASE 
		WHEN P.PaymentType = 'CA'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'CR'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'DB'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'CC'
			THEN ISNULL(COACQ.AccountCode,'')
		WHEN P.PaymentType = 'CQ'
			THEN ISNULL(COACQ.AccountCode,'')
		WHEN P.PaymentType = 'DC'
			THEN ISNULL(COADC.AccountCode,'')
		WHEN P.PaymentType = 'Z1'
			THEN ISNULL(COAZ1.AccountCode,'')
		ELSE BA.GLAccountCode
	  END GLAccountCode
    , ISNULL(COAWHT.AccountCode,'') WHTAccountCode
	, INV_SAP.SAPRefID DocEntry
FROM Sys_FI_Payment P
LEFT JOIN Sys_Master_BankAccount BA ON P.BankAccID = BA.BankAccountID
LEFT JOIN Sys_FI_Receipt RC ON RC.ReceiptID = P.ReceiptID
INNER JOIN SAP_Interface_Log INV_SAP ON INV_SAP.REMRefID = P.ReferenceID
										AND INV_SAP.Module = 'Invoice'
										AND INV_SAP.MethodName = 'CreateInvoice'
LEFT JOIN Sys_FI_CreditCard CR ON CR.PaymentID = P.PayDetailID
LEFT JOIN Sys_FI_Cheque CQ ON CQ.ChequeID = P.PayDetailID
LEFT JOIN Sys_FI_BillPaymentDetail BPD ON BPD.ID = P.PayDetailID
LEFT JOIN Sys_FI_BillPayment BP ON BP.ID = BPD.BillPaymentID
LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND P.CompanyID = RE.CompanyID
LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = P.ReceiptID
									AND SAP.Module = 'Payment'
									AND SAP.MethodName = 'PaymentBeforeInvoice'
LEFT JOIN Sys_ACC_PaymentTerm PT ON P.TermID = PT.TermID AND P.TermGroup = PT.TermGroup
LEFT JOIN Sys_FI_OtherPayment OP ON P.ReferenceID = OP.OPaymentID
LEFT JOIN Sys_ACC_ChartOfAccount COACA ON P.CompanyID = COACA.CompanyID AND COACA.AccountKey = 'CashOnHand'
LEFT JOIN Sys_ACC_ChartOfAccount COACQ ON P.CompanyID = COACQ.CompanyID AND COACQ.AccountKey = 'ChequeOnHand'
LEFT JOIN Sys_ACC_ChartOfAccount COADC ON P.CompanyID = COADC.CompanyID AND COADC.AccountKey = 'Promotion'
LEFT JOIN Sys_ACC_ChartOfAccount COAZ1 ON P.CompanyID = COAZ1.CompanyID AND COAZ1.AccountKey = 'ExcessRevenue'
LEFT JOIN Sys_ACC_ChartOfAccount COAWHT ON P.CompanyID = COAWHT.CompanyID AND COAWHT.AccountKey = 'WithHoldingTax'
WHERE 1 = 1
	--AND P.STATUS = 'P'
	--AND ISNULL(P.DepositID, '') <> ''
    AND ISNULL(P.STATUS,'') <> 'C'
    AND CASE WHEN P.STATUS <> 'P' AND PaymentType = 'TR' THEN 0 ELSE 1 END = 1
	AND ISNULL(SAP.APIResponseCode, '500') = '500'
	AND ISNULL(RE.Value, '') <> ''
	AND CASE WHEN ISNULL(RC.ReceiptDate,P.PaymentDate) >= OP.TransecDate THEN 'PaymentInvoice' ELSE 'PaymentBeforeInvoice' END = 'PaymentBeforeInvoice'
    ";

            if (!string.IsNullOrEmpty(ReceiptID))
            {
                sql += $" AND P.ReceiptID = '{ReceiptID}' ";
            }

            sql += @"
GROUP BY P.ReferenceID
	, RE.Value
	, CASE WHEN P.PaymentType = 'BP'
		THEN BP.BankMainDate
		ELSE ISNULL(P.DepositDate, P.PaymentDate)
	  END
	, ISNULL(RC.ReceiptDate, P.PaymentDate)
	, ISNULL(OP.TransecDate,'')
	, P.PaymentType
	, ISNULL(PT.UnEarnAccountCode,'')
	, CASE
		WHEN P.TermID = 'Z1' THEN ISNULL(COAZ1.AccountCode,'')
		WHEN P.TermID = 'CR' THEN ISNULL(PT.AccountCode,'')
	  END 
	, CR.CardNumber
	, ISNULL(CR.CardExpire, '')
	, CQ.ChequeNumber
	, CQ.ChequeDate
	, ISNULL(PT.UnEarnAccountCode, '')
	, ISNULL(SAP.APIResponseCode, '500')
	, SAP.SAPRefNo
	, CASE 
		WHEN P.PaymentType = 'CA'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'CR'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'DB'
			THEN ISNULL(COACA.AccountCode,'')
		WHEN P.PaymentType = 'CC'
			THEN ISNULL(COACQ.AccountCode,'')
		WHEN P.PaymentType = 'CQ'
			THEN ISNULL(COACQ.AccountCode,'')
		WHEN P.PaymentType = 'DC'
			THEN ISNULL(COADC.AccountCode,'')
		WHEN P.PaymentType = 'Z1'
			THEN ISNULL(COAZ1.AccountCode,'')
		ELSE BA.GLAccountCode
	  END
    , ISNULL(COAWHT.AccountCode,'')
	, INV_SAP.SAPRefID";

            return sql;
        }
        public string PaymentInvoice(dynamic Data)
        {
            string ReceiptID = (string)Data.ReceiptID;
            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "Payment",
                    "PaymentInvoice",
                    ReceiptID,
                    "Sys_FI_Receipt.ReceiptID",
                    null,
                    "ORCT.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                Sys_FI_Receipt Receipt = new Sys_FI_Receipt();
                Receipt.ExecCommand.Load(dbHelp, trans, ReceiptID);

                Interface.Sys_Master_Projects Project = new Interface.Sys_Master_Projects();
                Project.ExecCommand.Load(dbHelp, trans, Receipt.ProjectID);

                string sql = GetSqlCommandCreatePayment(ReceiptID);
                List<Dictionary<string, object>> PaymentDetailsList = GlobalDatabase.LoadDictByQuery(dbHelp, sql, trans);
                List<Dictionary<string, object>> PaymentDetails = PaymentDetailsList.FindAll(x => x["NewIncomingPayment"] == null || x["NewIncomingPayment"] == DBNull.Value);
                List<Dictionary<string, object>> PaymentDetails_Other = PaymentDetailsList.FindAll(x => x["NewIncomingPayment"] != null && x["NewIncomingPayment"] != DBNull.Value);

                ICON.SAP.API.SAPB1.Payments oPayment = new SAPB1.Payments();
                oPayment = GetPaymentItem(Receipt, Project, PaymentDetails);
                ICON.SAP.API.SAPB1.Payments oPayment_Other = new SAPB1.Payments();
                if (PaymentDetails_Other.Count > 0) oPayment_Other = GetPaymentItem(Receipt, Project, PaymentDetails_Other);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(PaymentDetails.FirstOrDefault()["DBName"].ToString());

                try
                {
                    SAPB1.CreatePayment(PaymentDetails, oPayment, oPayment_Other, out DocEntry, out DocNum, out GLDocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                dbHelp.CommitTransaction(trans);
                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message;
                dbHelp.RollbackTransaction(trans);
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                  Log.TranID,
                  ReceiptID,
                  DocEntry,
                  DocNum,
                  GLDocNum,
                  (int)ResponseCode,
                  ErrorMessage,
                  SAPStatus,
                  SAPErrorMessage,
                  LogDetail
                  );
            }
        }
        public string PaymentBeforeInvoice(dynamic Data)
        {
            string ReceiptID = (string)Data.ReceiptID;
            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "Payment",
                    "PaymentBeforeInvoice",
                    ReceiptID,
                    "Sys_FI_Receipt.ReceiptID",
                    null,
                    "ORCT.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                Sys_FI_Receipt Receipt = new Sys_FI_Receipt();
                Receipt.ExecCommand.Load(dbHelp, trans, ReceiptID);

                Interface.Sys_Master_Projects Project = new Interface.Sys_Master_Projects();
                Project.ExecCommand.Load(dbHelp, trans, Receipt.ProjectID);

                string sql = GetSqlCommandCreatePaymentBefore(ReceiptID);
                List<Dictionary<string, object>> PaymentDetailsList = GlobalDatabase.LoadDictByQuery(dbHelp, sql, trans);
                List<Dictionary<string, object>> PaymentDetails = PaymentDetailsList.FindAll(x => x["NewIncomingPayment"] == null || x["NewIncomingPayment"] == DBNull.Value);
                List<Dictionary<string, object>> PaymentDetails_Other = PaymentDetailsList.FindAll(x => x["NewIncomingPayment"] != null && x["NewIncomingPayment"] != DBNull.Value);

                ICON.SAP.API.SAPB1.Payments oPayment = new SAPB1.Payments();
                oPayment = GetPaymentItem(Receipt, Project, PaymentDetails);
                ICON.SAP.API.SAPB1.Payments oPayment_Other = new SAPB1.Payments();
                if (PaymentDetails_Other.Count > 0) oPayment_Other = GetPaymentItem(Receipt, Project, PaymentDetails_Other);
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(PaymentDetails.FirstOrDefault()["DBName"].ToString());

                try
                {
                    SAPB1.CreatePaymentBeforeInvoice(PaymentDetails, oPayment, oPayment_Other, out DocEntry, out DocNum, out GLDocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                dbHelp.CommitTransaction(trans);
                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message;
                dbHelp.RollbackTransaction(trans);
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                  Log.TranID,
                  ReceiptID,
                  DocEntry,
                  DocNum,
                  GLDocNum,
                  (int)ResponseCode,
                  ErrorMessage,
                  SAPStatus,
                  SAPErrorMessage,
                  LogDetail);
            }
        }
        private ICON.SAP.API.SAPB1.Payments GetPaymentItem(Sys_FI_Receipt Receipt, Interface.Sys_Master_Projects Project, List<Dictionary<string, object>> PaymentDetails)
        {
            Dictionary<string, object> CashDefault = PaymentDetails.Find(p => p["PaymentType"].ToString() == "CA");
            Dictionary<string, object> CreditDefault = PaymentDetails.Find(p => p["PaymentType"].ToString() == "CR" || p["PaymentType"].ToString() == "DB");
            Dictionary<string, object> TransferDefault = PaymentDetails.Find(p => p["PaymentType"].ToString() == "TR" || p["PaymentType"].ToString() == "QR" || p["PaymentType"].ToString() == "BP");
            Dictionary<string, object> ChequeDefault = PaymentDetails.Find(p => p["PaymentType"].ToString() == "CC" || p["PaymentType"].ToString() == "CQ");
            Dictionary<string, object> Z1Default = PaymentDetails.Find(p => p["PaymentType"].ToString() == "Z1");
            Dictionary<string, object> PaymentDefault = PaymentDetails.FirstOrDefault();

            double CashAmount = PaymentDetails.FindAll(p => p["PaymentType"].ToString() == "CA").Sum(p => Convert.ToDouble(p["Amount"]));
            double CreditAmount = PaymentDetails.FindAll(p => p["PaymentType"].ToString() == "CR").Sum(p => Convert.ToDouble(p["Amount"]));
            double TransferAmount = PaymentDetails.FindAll(p => p["PaymentType"].ToString() == "TR" || p["PaymentType"].ToString() == "QR" || p["PaymentType"].ToString() == "BP").Sum(p => Convert.ToDouble(p["Amount"]));
            double ChequeAmount = PaymentDetails.FindAll(p => p["PaymentType"].ToString() == "CC" || p["PaymentType"].ToString() == "CQ").Sum(p => Convert.ToDouble(p["Amount"]));
            double Z1Amount = PaymentDetails.FindAll(p => p["PaymentType"].ToString() == "Z1").Sum(p => Convert.ToDouble(p["Amount"]));
            double WHTAmount = PaymentDetails.Sum(p => Convert.ToDouble(p["WHTAmount"]));

            ICON.SAP.API.SAPB1.Payments oPayment = new ICON.SAP.API.SAPB1.Payments();

            oPayment.AccountCode = PaymentDetails.First()["NewIncomingPayment_Before"].ToString();
            oPayment.CardCode = this.CardCode;
            oPayment.SumPaid = (CashAmount + CreditAmount + TransferAmount + ChequeAmount + Z1Amount + WHTAmount);
            oPayment.ReceiptDate = Convert.ToDateTime(PaymentDetails.FirstOrDefault()["ReceiptDate"]);
            oPayment.TransecDate = Convert.ToDateTime(PaymentDetails.FirstOrDefault()["TransecDate"]);
            oPayment.UDF_RecpRefNo = Receipt.ReceiptID;
            oPayment.UDF_CustName = Receipt.CustomerName;
            oPayment.UDF_UnitRef = Receipt.UnitNumber;
            oPayment.UDF_Project = Receipt.ProjectID;
            oPayment.UDF_ProjectName = Project.ProjectName;

            // CA
            if (CashAmount > 0)
            {
                oPayment.CashAmount = CashAmount;
                oPayment.CashAccount = CashDefault["GLAccountCode"].ToString();
            }

            // CR
            if (CreditAmount > 0)
            {
                List<Dictionary<string, object>> CR_Payments = PaymentDetails.FindAll(p => p["PaymentType"].ToString() == "CR");

                foreach (Dictionary<string, object> CR_Payment in CR_Payments)
                {
                    ICON.SAP.API.SAPB1.CreditCard CR_Item = new ICON.SAP.API.SAPB1.CreditCard();

                    CR_Item.PayDetailID = CR_Payment["PayDetailID"].ToString();
                    CR_Item.Amount = Convert.ToDouble(CR_Payment["Amount"]);
                    CR_Item.CardNumber = CR_Payment["CardNumber"].ToString();
                    CR_Item.CreditType = SAPbobsCOM.BoRcptCredTypes.cr_Regular;
                    CR_Item.Account = CR_Payment["GLAccountCode"].ToString();
                    CR_Item.WHTType = "I";
                    CR_Item.WHTAmount = 0;
                    CR_Item.WHTPercent = 0;

                    if (!string.IsNullOrEmpty(CR_Payment["CardExpire"].ToString()) && CR_Payment["CardExpire"].ToString().IndexOf('/') > -1)
                    {
                        string[] splitStr = CR_Payment["CardExpire"].ToString().Split('/');
                        int Month = Convert.ToInt32(splitStr[0]);
                        int Year = Convert.ToInt32(splitStr[1]);
                        CR_Item.ExpireDate = new DateTime(Year, Month, 1);
                    }
                    else
                    {
                        CR_Item.ExpireDate = DateTime.MaxValue;
                    }

                    oPayment.Credit.Add(CR_Item);
                }
            }

            if (WHTAmount > 0)
            {
                ICON.SAP.API.SAPB1.CreditCard CR_Item = new ICON.SAP.API.SAPB1.CreditCard();

                CR_Item.PayDetailID = "1";
                CR_Item.Amount = WHTAmount;
                CR_Item.CardNumber = "1234";
                CR_Item.CreditType = SAPbobsCOM.BoRcptCredTypes.cr_Regular;
                CR_Item.Account = PaymentDefault["WHTAccountCode"].ToString();
                CR_Item.WHTType = "I";//PaymentDefault["WHTType"].ToString();
                CR_Item.WHTAmount = WHTAmount;
                CR_Item.WHTPercent = 0;//Convert.ToDouble(PaymentDefault["WHTPercent"]);
                CR_Item.ExpireDate = DateTime.MaxValue;

                oPayment.Credit.Add(CR_Item);
            }

            // Z1
            if (Z1Amount > 0)
            {
                List<Dictionary<string, object>> Z1_Payments = PaymentDetails.FindAll(p => p["PaymentType"].ToString() == "Z1");

                foreach (Dictionary<string, object> Z1_Payment in Z1_Payments)
                {
                    ICON.SAP.API.SAPB1.CreditCard Z1_Item = new ICON.SAP.API.SAPB1.CreditCard();

                    Z1_Item.PayDetailID = "1";
                    Z1_Item.Amount = Convert.ToDouble(Z1_Payment["Amount"]);
                    Z1_Item.CardNumber = "1234";
                    Z1_Item.CreditType = SAPbobsCOM.BoRcptCredTypes.cr_Regular;
                    Z1_Item.Account = Z1_Payment["GLAccountCode"].ToString();
                    Z1_Item.WHTType = "I";
                    Z1_Item.WHTAmount = 0;
                    Z1_Item.WHTPercent = 0;
                    Z1_Item.ExpireDate = DateTime.MaxValue;

                    oPayment.Credit.Add(Z1_Item);
                }
            }

            // TR
            if (TransferAmount > 0)
            {
                oPayment.TransferAmount = TransferAmount;
                oPayment.TransferAccount = TransferDefault["GLAccountCode"].ToString();
                oPayment.TransferDate = Convert.ToDateTime(PaymentDetails.FirstOrDefault()["TransferDate"]);
            }

            // CQ
            if (ChequeAmount > 0)
            {
                List<Dictionary<string, object>> CQ_Payments = PaymentDetails.FindAll(p => p["PaymentType"].ToString() == "CC" || p["PaymentType"].ToString() == "CQ");

                foreach (Dictionary<string, object> CQ_Payment in CQ_Payments)
                {
                    //ICON.SAP.API.SAPB1.Cheque CQ_Item = new ICON.SAP.API.SAPB1.Cheque();

                    //CQ_Item.Amount = Convert.ToDouble(CQ_Payment["Amount"]);
                    //CQ_Item.Number = Convert.ToInt32(CQ_Payment["ChequeNumber"]);
                    //CQ_Item.Account = ChequeDefault["GLAccountCode"].ToString();
                    //CQ_Item.Date = Convert.ToDateTime(CQ_Payment["ChequeDate"]);

                    //oPayment.Cheque.Add(CQ_Item);
                    ICON.SAP.API.SAPB1.CreditCard CR_Item = new ICON.SAP.API.SAPB1.CreditCard();

                    CR_Item.PayDetailID = "1";
                    CR_Item.Amount = Convert.ToDouble(CQ_Payment["Amount"]);
                    CR_Item.CardNumber = CQ_Payment["ChequeNumber"].ToString();
                    CR_Item.CreditType = SAPbobsCOM.BoRcptCredTypes.cr_Regular;
                    CR_Item.Account = PaymentDefault["GLAccountCode"].ToString();
                    CR_Item.WHTType = "I";
                    CR_Item.WHTAmount = 0;
                    CR_Item.WHTPercent = 0;
                    CR_Item.ExpireDate = DateTime.MaxValue;

                    oPayment.Credit.Add(CR_Item);
                }
            }

            return oPayment;
        }

        // *** ยกเลิก ***
        private string GetSqlCommandCancelPayment(string ReceiptID)
        {
            string sql = $@"
SELECT 
       --OP.OPaymentID, 
       ISNULL(CN_SAP.APIResponseCode,'500') APIResponseCode,
       CN_SAP.SAPRefNo AS APIResponseData,
	   P.ReceiptID,
	   P.CompanyID,
	   RE.Value AS DBName,
	   PA_SAP.SAPRefID DocEntry
FROM 
	Sys_FI_OtherPayment OP
	INNER JOIN Sys_FI_Payment P ON P.ReferenceID = OP.OPaymentID
    INNER JOIN SAP_Interface_Log PA_SAP ON PA_SAP.REMRefID = P.ReceiptID 
                                         AND PA_SAP.Module = 'Payment' 
                                         AND PA_SAP.MethodName IN ('PaymentInvoice','PaymentBeforeInvoice')
	LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND P.CompanyID = RE.CompanyID
	LEFT JOIN SAP_Interface_Log CN_SAP ON CN_SAP.REMRefID = P.ReceiptID 
                                         AND CN_SAP.Module = 'Payment' 
                                         AND CN_SAP.MethodName = 'CancelPaymentInvoice'
WHERE 
    1=1
    AND OP.Status = 'A'
    --AND P.Status = '-'
    --AND ISNULL(P.DepositID, '') <> ''
    AND CASE
            WHEN ISNULL(P.Status,'') = 'C' THEN 1 
            WHEN P.STATUS <> 'P' AND PaymentType = 'TR' THEN 1 
            ELSE 0 END = 1
    AND PA_SAP.APIResponseCode = '200' 
	AND ISNULL(CN_SAP.APIResponseCode,'500') = '500' 
	AND ISNULL(RE.Value,'') <> ''";

            if (!string.IsNullOrEmpty(ReceiptID))
            {
                sql += $" AND P.ReceiptID = '{ReceiptID}'";
            }

            sql += @"
GROUP BY 
       --OP.OPaymentID, 
       ISNULL(CN_SAP.APIResponseCode,'500'),
       CN_SAP.SAPRefNo,
	   P.ReceiptID,
       P.CompanyID,
       RE.Value,
	   PA_SAP.SAPRefID";

            return sql;
        }
        public string CancelPaymentInvoice(dynamic Data)
        {
            string ReceiptID = (string)Data.ReceiptID;
            string DBName = (string)Data.DBName;
            int PayDocEntry = 0;
            string TranID = string.Empty;
            string PaymentEntryID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "Payment",
                    "CancelPaymentInvoice",
                    ReceiptID,
                    "Sys_FI_Receipt.ReceiptID",
                    null,
                    "ORCT.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(DBName);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();

                try
                {
                    PayDocEntry = Convert.ToInt32(Data.DocEntry);
                }
                catch (Exception ex)
                {
                    throw new Exception("DocEntry : " + ex.Message);
                }

                Doc.DocEntry = Convert.ToInt32(PayDocEntry);
                Doc.DocType = SAPbobsCOM.BoObjectTypes.oIncomingPayments;
                Doc.DocumentDate = DateTime.Now;

                try
                {
                    SAPB1.CancelPayment(PayDocEntry, out PaymentEntryID, out DocNum, out GLDocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                dbHelp.CommitTransaction(trans);
                return PaymentEntryID;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message;
                dbHelp.RollbackTransaction(trans);
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                  Log.TranID,
                  ReceiptID,
                  PaymentEntryID,
                  DocNum,
                  GLDocNum,
                  (int)ResponseCode,
                  ErrorMessage,
                  SAPStatus,
                  SAPErrorMessage,
                  LogDetail
                  );
            }
        }
        #endregion

        #region ### Credit Memo ###
        // *** สร้าง ***
        private string GetSqlCommandListCreditMemo(string CreditNoteID)
        {
            string sql = $@"
SELECT 
       NT.CreditNoteID,
       NT.CreditNoteNumber,
       ISNULL(OP.CompanyID,RC.CompanyID) AS CompanyID,
       ISNULL(SAP.APIResponseCode,'500') APIResponseCode,
       SAP.SAPRefNo AS APIResponseData
FROM 
	Sys_FI_CreditNote NT
    LEFT JOIN Sys_FI_OtherPayment OP ON NT.ReferenceType = 'otherpayment' AND NT.ReferenceID = OP.OPaymentID
	LEFT JOIN Sys_Conf_RealEstate RE1 ON RE1.KEYNAME = 'DBName' AND OP.CompanyID = RE1.CompanyID
    LEFT JOIN Sys_FI_Receipt RC	ON NT.ReferenceType = 'receipt' AND NT.ReferenceID = RC.ReceiptID
	LEFT JOIN Sys_Conf_RealEstate RE2 ON RE2.KEYNAME = 'DBName' AND RC.CompanyID = RE2.CompanyID
    LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = NT.CreditNoteID 
                                         AND SAP.Module = 'CreditMemo' 
                                         AND SAP.MethodName = 'CreateCreditMemo'
WHERE 
	1=1
    AND NT.Status = 'A'  
	AND ISNULL(SAP.APIResponseCode,'500') = '500'
    AND ISNULL(RE1.Value,ISNULL(RE2.Value,'')) <> ''";

            if (!string.IsNullOrEmpty(CreditNoteID))
            {
                sql += $" AND NT.CreditNoteID = '{CreditNoteID}'";
            }

            sql += " GROUP BY NT.CreditNoteID, NT.CreditNoteNumber, ISNULL(OP.CompanyID,RC.CompanyID), ISNULL(SAP.APIResponseCode,'500'), SAP.SAPRefNo";
            sql += " ORDER BY CompanyID, CreditNoteNumber";

            return sql;
        }
        private string GetSqlCommandCreateCreditMemo(string CreditNoteID, string RefType)
        {
            string sql = string.Empty;
            if (RefType == "receipt")
            {
                sql = $@"
DECLARE @TempContractTerm TABLE
(
    LineNumber int,
	TermID nvarchar(100),
	ContractID nvarchar(100),
	ReferenceID nvarchar(100),
	ContractTermID nvarchar(100),
    TermGroup nvarchar(100),
    InterfaceVATCode nvarchar(100)
)
INSERT INTO @TempContractTerm
SELECT ROW_NUMBER() OVER(
       ORDER BY TM.ContractTermID ASC) - 1 AS LineNumber, 
       TM.TermID, 
       TM.ContractID,
       TM.ReferenceID,
       TM.ContractTermID,
       TM.TermGroup,
       ISNULL(TM.InterfaceVATCode, '') InterfaceVATCode
FROM Sys_REM_ContractTerm TM
WHERE 
    EXISTS (
        SELECT *
        FROM Sys_FI_CreditNotePayment CNP
        WHERE CNP.CreditNoteID = '{CreditNoteID}'
            AND CNP.ContractID = TM.ContractID
            AND CNP.ReferenceID = TM.ReferenceID
            AND CNP.ReferenceType = TM.ReferenceType
    )

-----------------------------------------------------------

SELECT 
    TM.LineNumber,
    TM.InterfaceVATCode,
    TX.StartDate,
    TX.EndDate,
	CD.CreditNoteID, 
	CD.CreditNoteDate AS DocDate, 
    CD.InvoiceNo InvoiceNo, 
    CP.CNAmount PriceAfterVAT, 
    CP.CNBaseAmount BaseAmount, 
	ISNULL(CD.CustomerName, '') CustomerName,
    RC.UnitNumber UnitNumber, 
    RC.CompanyID,
	RE.Value AS DBName,
    RC.ProjectID,
    PJ.ProjectName, 
    ISNULL(RC.TaxID, '') TaxID, 
    ISNULL(CD.Address, '') Address, 
    ISNULL(CP.Description2, '') TermName,
    ISNULL(PT.AccountCode, '') AccountCode,
    ISNULL(PT.MaterialCode, '') MaterialCode,
	ISNULL(SAP.APIResponseCode,'500') APIResponseCode,
    ISNULL(SAP.SAPRefNo,'') APIResponseData,
    SAP1.SAPRefID as BaseEntry  
FROM
	Sys_FI_CreditNote CD 
	INNER JOIN Sys_FI_CreditNotePayment CP ON CD.CreditNoteID = CP.CreditNoteID
    INNER JOIN @TempContractTerm TM ON TM.ContractID = CP.ContractID AND TM.ReferenceID = CP.ReferenceID AND TM.TermID = CP.TermID AND TM.ContractTermID = CP.ContractTermID
    INNER JOIN Sys_FI_OtherPayment OP ON OP.OPaymentID = CP.ReferenceID
	INNER JOIN Sys_FI_Receipt RC ON RC.ReceiptID = CD.ReceiptID
	LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND RC.CompanyID = RE.CompanyID
    LEFT JOIN Sys_REM_ContractTerm_Extension TX ON TX.ContractTermID = TM.ContractTermID
    LEFT JOIN Sys_Master_Projects PJ ON PJ.ProjectID = RC.ProjectID
    LEFT JOIN Sys_ACC_PaymentTerm PT ON CP.TermID = PT.TermID
                                         AND TM.TermGroup = pt.TermGroup
                                         AND ISNULL(pt.isdelete, 0) = 0
	LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = CD.CreditNoteID 
                                         AND SAP.Module = 'CreditMemo' 
                                         AND SAP.MethodName = 'CreateCreditMemo'
    LEFT JOIN SAP_Interface_Log SAP1 ON SAP1.REMRefID = OP.OPaymentID 
                                         AND SAP1.Module = 'Invoice' 
                                         AND SAP1.MethodName = 'CreateInvoice'
WHERE 
     1=1
     AND CD.Status = 'A'
     AND CD.ReferenceType = 'receipt' 
     AND ISNULL(SAP.APIResponseCode,'500') = '500'
	 AND ISNULL(RE.Value,'') <> ''";
            }
            else
            {
                sql = $@"
DECLARE @TempContractTerm TABLE
(
    LineNumber int,
	TermID nvarchar(100),
	ContractID nvarchar(100),
	ReferenceID nvarchar(100),
	ContractTermID nvarchar(100),
    TermGroup nvarchar(100),
    InterfaceVATCode nvarchar(100)
)
INSERT INTO @TempContractTerm
SELECT ROW_NUMBER() OVER(
       ORDER BY TM.ContractTermID ASC) - 1 AS LineNumber, 
       TM.TermID, 
       TM.ContractID,
       TM.ReferenceID,
       TM.ContractTermID,
       TM.TermGroup,
       ISNULL(TM.InterfaceVATCode, '') InterfaceVATCode
FROM Sys_REM_ContractTerm TM
WHERE 
    EXISTS (
        SELECT *
        FROM Sys_FI_CreditNotePayment CNP
        WHERE CNP.CreditNoteID = '{CreditNoteID}'
            AND CNP.ContractID = TM.ContractID
            AND CNP.ReferenceID = TM.ReferenceID
            AND CNP.ReferenceType = TM.ReferenceType
    )


SELECT 
       TM.LineNumber,
       TM.InterfaceVATCode,
       TX.StartDate,
       TX.EndDate,
	   CD.CreditNoteDate AS DocDate, 
       CD.InvoiceNo InvoiceNo, 
       CP.CNAmount PriceAfterVAT, 
       CP.CNBaseAmount BaseAmount, 
       ISNULL(CD.CustomerName, '') CustomerName,
       OP.AddressNo UnitNumber,
       OP.CompanyID, 
	   RE.Value AS DBName,
       OP.ProjectID,
       PJ.ProjectName, 
       ISNULL(OP.TaxID, '') TaxID, 
       ISNULL(CD.Address, '') Address, 
       ISNULL(CP.Description2, '') TermName,
       ISNULL(PT.AccountCode, '') AccountCode,
       ISNULL(PT.MaterialCode, '') MaterialCode,
	   ISNULL(SAP.APIResponseCode,'500') APIResponseCode,
       ISNULL(SAP.SAPRefNo,'') APIResponseData,
       SAP1.SAPRefID as BaseEntry,OP.OPaymentID 
FROM
	Sys_FI_CreditNote CD 
    INNER JOIN Sys_FI_CreditNotePayment CP ON CD.CreditNoteID = CP.CreditNoteID
	INNER JOIN Sys_FI_OtherPayment OP ON OP.OPaymentID = CD.ReferenceID
    INNER JOIN @TempContractTerm TM ON TM.ContractID = CP.ContractID AND TM.ReferenceID = CP.ReferenceID AND TM.TermID = CP.TermID AND TM.ContractTermID = CP.ContractTermID
    LEFT JOIN Sys_REM_ContractTerm_Extension TX ON TX.ContractTermID = TM.ContractTermID
    LEFT JOIN Sys_Master_Projects PJ ON PJ.ProjectID = OP.ProjectID
	LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND OP.CompanyID = RE.CompanyID
    LEFT JOIN Sys_ACC_PaymentTerm PT ON TM.TermID = PT.TermID
                                         AND TM.TermGroup = pt.TermGroup
                                         AND ISNULL(pt.isdelete, 0) = 0
	LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = OP.OPaymentID 
                                         AND SAP.Module = 'CreditMemo' 
                                         AND SAP.MethodName = 'CreateCreditMemo'
    LEFT JOIN SAP_Interface_Log SAP1 ON SAP1.REMRefID = OP.OPaymentID 
                                         AND SAP1.Module = 'Invoice' 
                                         AND SAP1.MethodName = 'CreateInvoice'
WHERE 
     1=1
     AND CD.Status = 'A'
     AND CD.ReferenceType = 'otherpayment' 
     AND ISNULL(SAP.APIResponseCode,'500') = '500'
     AND ISNULL(RE.Value,'') <> ''
     AND CD.CreditNoteID = '{CreditNoteID}' ";
            }

            return sql;
        }
        public string CreateCreditMemo(dynamic Data)
        {
            string CreditNoteID = (string)Data.CreditNoteID;
            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();
            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreditMemo",
                    "CreateCreditMemo",
                    CreditNoteID,
                    "Sys_FI_CreditNote.CreditNoteID",
                    DocEntry,
                    "ORIN.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                Sys_FI_CreditNote CN = new Sys_FI_CreditNote();
                CN.ExecCommand.Load(dbHelp, trans, CreditNoteID);

                if (string.IsNullOrEmpty(CN.CreditNoteNumber))
                {
                    throw new Exception("CreditNoteID: \"" + CreditNoteID + "\" not found.");
                }

                string sql = GetSqlCommandCreateCreditMemo(CN.CreditNoteID, CN.ReferenceType);
                DataTable dt = dbHelp.ExecuteDataTable(sql, trans);

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("CreditNoteID: \"" + CN.CreditNoteID + "\" not found.");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document
                {
                    CardCode = CardCode,
                    PostingDate = Convert.ToDateTime(dt.Rows[0]["DocDate"]),
                    DocDueDate = Convert.ToDateTime(dt.Rows[0]["DocDate"]),
                    DocumentDate = Convert.ToDateTime(dt.Rows[0]["DocDate"]),
                    DocType = SAPbobsCOM.BoObjectTypes.oCreditNotes,
                    UDF_RefNo = CN.CreditNoteNumber,
                    UDF_CustName = CN.CustomerName,
                    UDF_UnitRef = dt.Rows[0]["UnitNumber"].ToString(),
                    UDF_Project = dt.Rows[0]["ProjectID"].ToString(),
                    UDF_ProjectName = dt.Rows[0]["ProjectName"].ToString(),
                    UDF_TaxID = dt.Rows[0]["TaxID"].ToString(),
                    UDF_Address = dt.Rows[0]["Address"].ToString(),
                    UDF_InvNo = dt.Rows[0]["InvoiceNo"].ToString()
                };

                foreach (DataRow dr in dt.Rows)
                {
                    ICON.SAP.API.SAPB1.DocumentLine DocLine = new ICON.SAP.API.SAPB1.DocumentLine();

                    if (CN.ReferenceType != "receipt")
                    {
                        DocLine.BaseEntry = dr["BaseEntry"].ToString();
                        DocLine.BaseType = 13;
                        DocLine.LineNumber = dr["LineNumber"].ToString();
                    }

                    DocLine.ItemCode = dr["MaterialCode"].ToString();
                    DocLine.Qty = 1;
                    DocLine.TaxCode = dr["InterfaceVATCode"].ToString();
                    DocLine.UnitPrice = dr["BaseAmount"].ToString();
                    DocLine.UnitAfterPrice = Convert.ToDouble(dr["PriceAfterVAT"]);

                    string ItemDescription = dr["TermName"].ToString();

                    if (!string.IsNullOrEmpty(dr["StartDate"].ToString()) && !string.IsNullOrEmpty(dr["EndDate"].ToString()))
                    {
                        ItemDescription += " (" + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["StartDate"])) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EndDate"])) + ")";
                    }

                    DocLine.ItemDescription = ItemDescription;
                    doc.Lines.Add(DocLine);
                }

                try
                {
                    SAPB1.CreateDocument(doc, out DocEntry, out DocNum, out GLDocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                dbHelp.CommitTransaction(trans);
                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message;
                dbHelp.RollbackTransaction(trans);
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                   Log.TranID,
                   CreditNoteID,
                   DocEntry,
                   DocNum,
                   GLDocNum,
                   (int)ResponseCode,
                   ErrorMessage,
                   SAPStatus,
                   SAPErrorMessage,
                   LogDetail
                   );
            }
        }


        // *** ยกเลิก ***
        private string GetSqlCommandListCancelMemo(string CreditNoteID)
        {
            string sql = @"
SELECT 
       NT.CreditNoteID, 
       NT.ReverseDate AS TransecDate,
       ISNULL(OP.CompanyID,RC.CompanyID) AS CompanyID,
       ISNULL(RE1.Value,RE2.Value) AS DBName,
       ISNULL(SAP1.APIResponseCode,'500') APIResponseCode,
       SAP1.SAPRefNo AS APIResponseData,
	   SAP.SAPRefID
FROM 
	Sys_FI_CreditNote NT
    INNER JOIN SAP_Interface_Log SAP ON SAP.REMRefID = NT.CreditNoteID 
                                            AND SAP.Module = 'CreditMemo' 
	                                        AND SAP.MethodName = 'CreateCreditMemo'
    LEFT JOIN Sys_FI_OtherPayment OP ON NT.ReferenceType = 'otherpayment' AND NT.ReferenceID = OP.OPaymentID
	LEFT JOIN Sys_Conf_RealEstate RE1 ON RE1.KEYNAME = 'DBName' AND OP.CompanyID = RE1.CompanyID
    LEFT JOIN Sys_FI_Receipt RC	ON NT.ReferenceType = 'receipt' AND NT.ReferenceID = RC.ReceiptID
	LEFT JOIN Sys_Conf_RealEstate RE2 ON RE2.KEYNAME = 'DBName' AND RC.CompanyID = RE2.CompanyID
	LEFT JOIN SAP_Interface_Log SAP1 ON SAP1.REMRefID = NT.CreditNoteID 
                                         AND SAP1.Module = 'CreditMemo' 
                                         AND SAP1.MethodName = 'CancelCreditMemo'
WHERE
    1=1
    AND NT.Status = 'B'
	AND SAP.APIResponseCode ='200' 
    AND ISNULL(SAP1.APIResponseCode,'500') = '500'
	AND ISNULL(RE1.Value,ISNULL(RE2.Value,'')) <> ''";

            if (!string.IsNullOrEmpty(CreditNoteID))
            {
                sql += $" AND NT.CreditNoteID = '{CreditNoteID}'";
            }

            sql += " GROUP BY NT.CreditNoteID, NT.ReverseDate, ISNULL(OP.CompanyID,RC.CompanyID), ISNULL(RE1.Value,RE2.Value), ISNULL(SAP1.APIResponseCode,'500'), SAP1.SAPRefNo, SAP.SAPRefID ";

            return sql;
        }
        private string CancelCreditMemo(dynamic Data)
        {
            string CreditNoteID = (string)Data.CreditNoteID;
            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                   "CreditMemo",
                   "CancelCreditMemo",
                   CreditNoteID,
                   "Sys_FI_CreditNote.CreditNoteID",
                   DocEntry,
                   "ORIN.DocEntry",
                   Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                   TranBy);

            try
            {
                string sql = GetSqlCommandListCancelMemo(CreditNoteID);
                DataTable dt = dbHelp.ExecuteDataTable(sql, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();

                Doc.DocEntry = Convert.ToInt32(dt.Rows[0]["SAPRefID"]);
                Doc.DocType = SAPbobsCOM.BoObjectTypes.oCreditNotes;
                Doc.DocumentDate = Convert.ToDateTime(dt.Rows[0]["TransecDate"]);

                try
                {
                    SAPB1.CancelDocument(Doc, out DocEntry, out DocNum, out GLDocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                dbHelp.CommitTransaction(trans);
                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message;
                dbHelp.RollbackTransaction(trans);
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                    CreditNoteID,
                    DocEntry,
                    DocNum,
                    GLDocNum,
                    (int)ResponseCode,
                    ErrorMessage,
                    SAPStatus,
                    SAPErrorMessage,
                    LogDetail
                    );
            }
        }
        #endregion

        #region ### Create Journal Entry ###
        private string GetSqlCommandListJournalEntry(string GLVoucher)
        {
            string sql = $@"
SELECT 
    GL.GLVoucher,
    GL.GLCompanyID AS CompanyID,
	RE.Value AS DBName,
	ISNULL(SAP.APIResponseCode, '500') APIResponseCode,
    SAP.SAPRefNo AS APIResponseData
FROM
    Sys_ACC_GeneralLedgers GL
	LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND GL.GLCompanyID = RE.CompanyID
    LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = GL.GLVoucher
                                         AND SAP.Module = 'JournalEntry'
                                         AND SAP.MethodName = 'CreateJournalEntry'
WHERE
	1=1
    AND GL.PostStatus = 'A' 
	AND ISNULL(RE.Value,'') <> ''";

            if (!string.IsNullOrEmpty(GLVoucher))
            {
                sql += $" AND GL.GLVoucher  = '{GLVoucher}'";
            }

            sql += " GROUP BY GL.GLVoucher, GL.GLCompanyID, RE.Value, ISNULL(SAP.APIResponseCode, '500'), SAP.SAPRefNo";
            sql += " ORDER BY GL.GLCompanyID, GL.GLVoucher";

            return sql;
        }
        private string GetSqlCommandCreateJournalEntry(string GLVoucher)
        {
            string sql = $@"
DECLARE @GLVoucher NVARCHAR(50) = '{GLVoucher}'

CREATE TABLE #TEMP (
	GLDetailID			NVARCHAR(50)
	, GLTransacID		NVARCHAR(50)
	, GLBatchID			NVARCHAR(50)
	, GLVoucher			NVARCHAR(50)
	, GLTransacType		NVARCHAR(50)
	, PostKey			NVARCHAR(10)
	, PostProfile		NVARCHAR(50)
	, GLType			NVARCHAR(2)
	, GLCompanyID		NVARCHAR(50)
	, FrCompanyID		NVARCHAR(50)
	, ToCompanyID		NVARCHAR(50)
	, PaymentID			NVARCHAR(50)
	, SBUID				NVARCHAR(50)
	, BUID				NVARCHAR(50)
	, CostCenter		NVARCHAR(50)
	, ProfitCenter		NVARCHAR(50)
	, ProjectID			NVARCHAR(50)
	, UnitID			NVARCHAR(50)
	, ReferenceID		NVARCHAR(50)
	, ReferenceID2		NVARCHAR(50)
	, ReferenceID3		NVARCHAR(50)
	, PRReference		NVARCHAR(50)
	, CustomerID		NVARCHAR(50)
	, CustomerName		NVARCHAR(255)
	, PaymentDate		DATETIME
	, AccountCode		NVARCHAR(200)
	, AccountName		NVARCHAR(500)
	, Description		NVARCHAR(MAX)
	, BaseAmount		NUMERIC(18,2)
	, VATCode			NVARCHAR(10)
	, DebitAmount		NUMERIC(18,2)
	, CreditAmount		NUMERIC(18,2)
	, PostStatus		NVARCHAR(2)
	, PostDate			DATETIME
	, CreateDate		DATETIME
	, CreateBy			NVARCHAR(50)
	, ModifyDate		DATETIME
	, ModifyBy			NVARCHAR(50)
	, SAPDocID			NVARCHAR(50)
	, Seq				INT
	, Optional1			NVARCHAR(50)
	, Optional2			NVARCHAR(50)
	, ParentGLDetailID	NVARCHAR(50)
	, CompanyID			NVARCHAR(50)
	, CreateByName		NVARCHAR(50)
	, UnitNumber		NVARCHAR(50)
	, ProjectName		NVARCHAR(255)
)

IF EXISTS(SELECT * FROM Sys_Master_Query WHERE 1=1 AND QueryCode = 'SAPB1_JournalEntry')
BEGIN
	DECLARE @SQL NVARCHAR(MAX) = ''
	SET @SQL = 'DECLARE @GLVoucher NVARCHAR(50) = '''' '
	SELECT TOP 1 @SQL = @SQL + DataQuery FROM Sys_Master_Query WHERE 1=1 AND QueryCode = 'SAPB1_JournalEntry' ORDER BY IsUpdateBySTD ASC
	INSERT INTO #TEMP EXEC(@SQL)
END
ELSE
BEGIN
	INSERT INTO #TEMP
	SELECT GL.GLDetailID
		, GL.GLTransacID
		, GL.GLBatchID
		, GL.GLVoucher
		, GL.GLTransacType
		, GL.PostKey
		, GL.PostProfile
		, GL.GLType
		, ISNULL(GL.GLCompanyID, N'') AS GLCompanyID
		, ISNULL(GL.FrCompanyID, N'') AS FrCompanyID
		, ISNULL(GL.ToCompanyID, N'') AS ToCompanyID
		, ISNULL(GL.PaymentID, N'') AS PaymentID
		, ISNULL(GL.SBUID, N'') AS SBUID
		, ISNULL(GL.BUID, N'') AS BUID
		, ISNULL(GL.CostCenter, N'') AS CostCenter
		, ISNULL(GL.ProfitCenter, N'') AS ProfitCenter
		, ISNULL(GL.ProjectID, N'') AS ProjectID
		, ISNULL(GL.UnitID, N'') AS UnitID
		, ISNULL(GL.ReferenceID, N'') AS ReferenceID
		, ISNULL(GL.ReferenceID2, N'') AS ReferenceID2
		, ISNULL(GL.ReferenceID3, N'') AS ReferenceID3
		, ISNULL(GL.PRReference, N'') AS PRReference
		, ISNULL(GL.CustomerID, N'') AS CustomerID
		, ISNULL(GL.CustomerName, N'') AS CustomerName
		, GL.PaymentDate
		, ISNULL(GL.AccountCode, N'') AS AccountCode
		, ISNULL(GL.AccountName, N'') AS AccountName
		, ISNULL(GL.Description, N'') AS Description
		, GL.BaseAmount
		, GL.VATCode
		, GL.DebitAmount
		, GL.CreditAmount
		, GL.PostStatus
		, GL.PostDate
		, GL.CreateDate
		, GL.CreateBy
		, GL.ModifyDate
		, GL.ModifyBy
		, GL.SAPDocID
		, GL.Seq
		, GL.Optional1
		, GL.Optional2
		, GL.ParentGLDetailID
		, ISNULL(GL.GLCompanyID, N'') AS CompanyID
		, ISNULL(US.DisplayName, N'') AS CreateByName
		, ISNULL(UN.UnitNumber, N'') AS UnitNumber
		, ISNULL(PJ.ProjectName, N'') AS ProjectName
	FROM Sys_ACC_GeneralLedgers AS GL
	INNER JOIN Sys_Admin_Users AS US ON GL.CreateBy = US.UserId
	LEFT OUTER JOIN Sys_Master_Units AS UN ON ISNULL(GL.UnitID, N'') = UN.UnitID
	LEFT OUTER JOIN Sys_Master_Projects AS PJ ON ISNULL(GL.ProjectID, N'') = PJ.ProjectID
	WHERE (GL.PostStatus = 'A')
	AND (@GLVoucher = '' OR GLVoucher = @GLVoucher)
	ORDER BY Seq ASC
END

SELECT * FROM #TEMP

DROP TABLE #TEMP";

            return sql;
        }
        public string CreateJournalEntry(dynamic Data)
        {
            string GLVoucher = (string)Data.GLVoucher;
            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "JournalEntry",
                    "CreateJournalEntry",
                    GLVoucher,
                    "Sys_ACC_GeneralLedgers.GLVoucher",
                    null,
                    "OJDT.TranId",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                string sql = GetSqlCommandListJournalEntry(GLVoucher);
                DataTable dt = dbHelp.ExecuteDataTable(sql, trans);

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("Data GeneralLedgers Not Found.");
                }

                foreach (DataRow dr in dt.Rows)
                {
                    sql = GetSqlCommandCreateJournalEntry(dr["GLVoucher"].ToString());
                    DataTable items = dbHelp.ExecuteDataTable(sql, trans);
                    if (items.Rows.Count == 0) throw new Exception("No Record Execute SqlCommandCreateJournalEntry");

                    ICON.SAP.API.SAPB1.Document je = new ICON.SAP.API.SAPB1.Document
                    {
                        DocType = SAPbobsCOM.BoObjectTypes.oJournalEntries,
                        PostingDate = Convert.ToDateTime(items.Rows[0]["PaymentDate"]),                                                     // GL.PaymentDate
                        DocDueDate = Convert.ToDateTime(items.Rows[0]["PaymentDate"]),                                                      // GL.PaymentDate
                        DocumentDate = Convert.ToDateTime(items.Rows[0]["PaymentDate"]),                                                    // GL.PaymentDate
                        //Remark = items.Rows[0]["CreateByName"].ToString() + " " + string.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now),   // CreateBy (DisplayName +' '+ วัน/เดือน/ปี เวลา)
                        Remark = items.Rows[0]["Description"].ToString(),
                        Ref1 = items.Rows[0]["ReferenceID"].ToString(),                                                                     // GL.Ref1
                        Ref2 = items.Rows[0]["ReferenceID2"].ToString(),                                                                    // GL.Ref2
                        Ref3 = items.Rows[0]["ReferenceID3"].ToString(),                                                                    // GL.Ref3
                        Project = items.Rows[0]["ProjectID"].ToString(),                                                                    // GL.ProjectID
                        UDF_Project = items.Rows[0]["ProjectID"].ToString(),                                                                // GL.ProjectID
                        UDF_ProjectName = items.Rows[0]["ProjectName"].ToString(),                                                          // GL.ProjectName
                        UDF_UnitRef = items.Rows[0]["UnitNumber"].ToString(),                                                               // GL.UnitNumber
                        UDF_CustName = items.Rows[0]["CustomerName"].ToString(),                                                            // GL.CustomerName
                        UDF_Ref = dr["GLVoucher"].ToString()                                                                                // GL.GLVoucher
                    };

                    foreach (DataRow item in items.Rows)
                    {
                        Sys_ACC_GeneralLedgers GL_Item = new Sys_ACC_GeneralLedgers();
                        GL_Item.ExecCommand.Load(item);

                        je.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                        {
                            AccountCode = GL_Item.AccountCode,
                            Debit = Convert.ToDouble(GL_Item.DebitAmount),
                            Credit = Convert.ToDouble(GL_Item.CreditAmount),
                            RefID = GL_Item.GLDetailID,
                            LineMemo = GL_Item.Description,
                            ProjectCode = GL_Item.ProjectID,
                            ref1 = je.PostingDate.ToString("yyyy-MM"),
                            ref2 = je.UDF_UnitRef,
                        });
                    }

                    ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dr["DBName"].ToString());
                    try
                    {
                        SAPB1.CreateJournalEntry(je, out TranID, out DocNum, out GLDocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                        SAPStatus = SAPStatusCode.ToString();
                    }
                    catch (Exception ex)
                    {
                        SAPStatus = SAPStatusCode.ToString();
                        throw ex;
                    }

                    if (SAPStatusCode == 0)
                    {
                        sql = $"UPDATE Sys_ACC_GeneralLedgers SET PostStatus = 'P', GLBatchID = '{TranID}', PostDate = GETDATE() WHERE GLVoucher = '{GLVoucher}' AND ISNULL(PostStatus,'') = 'A' ";
                        dbHelp.ExecuteNonQuery(sql, trans);
                    }
                }

                dbHelp.CommitTransaction(trans);
                return TranID;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message;
                dbHelp.RollbackTransaction(trans);
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                   Log.TranID,
                   GLVoucher,
                   TranID,
                   DocNum,
                   GLDocNum,
                   (int)ResponseCode,
                   ErrorMessage,
                   SAPStatus,
                   SAPErrorMessage,
                   LogDetail
                   );
            }
        }
        #endregion

        #region ### Good Receipt PO ####
        private string GetSqlCommandListGoodReceipt(string RefID)
        {
            string sql = $@"
SELECT 
       OP.OPaymentID, 
       ISNULL(SAP.APIResponseCode,'500') APIResponseCode,
       SAP.SAPRefNo AS APIResponseData
FROM 
	Sys_FI_OtherPayment OP
    INNER JOIN Sys_REM_ContractTerm TM ON OP.OPaymentID = TM.ReferenceID
                                          AND TM.ReferenceType = OP.OPaymentType
    LEFT JOIN Sys_Master_Projects PJ ON PJ.ProjectID = OP.ProjectID
    LEFT JOIN Sys_Master_Units U ON U.UnitID = OP.UnitID
                                     AND ISNULL(U.IsDelete, 0) = 0
    LEFT JOIN Sys_ACC_PaymentTerm PT ON TM.TermID = PT.TermID
                                         AND TM.TermGroup = pt.TermGroup
                                         AND ISNULL(pt.isdelete, 0) = 0
    LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = OP.OPaymentID 
                                         AND SAP.Module = 'GoodReceipt' 
                                         AND SAP.MethodName = 'CreateGoodReceipt'
WHERE 
    OP.Status = 'A'  AND ISNULL(SAP.APIResponseCode,'500') = '500' ";

            if (!string.IsNullOrEmpty(RefID))
            {
                sql += $" AND OP.OPaymentID = '{RefID}'";
            }

            sql += " GROUP BY OP.OPaymentID, ISNULL(SAP.APIResponseCode,'500'), SAP.SAPRefNo";

            return sql;
        }
        private string GetSqlCommandCreateGoodReceipt(string RefID)
        {
            string sql = $@"
SELECT OP.OPaymentID, 
       TM.Amount, 
       ISNULL(TM.InterfaceVATCode, '') InterfaceVATCode,
       TX.StartDate,
       TX.EndDate,
       ISNULL(OP.CustomerName, '') CustomerName,
       CASE
           WHEN U.UnitID IS NOT NULL
           THEN ISNULL(U.HouseNumber, '')
           ELSE OP.AddressNo
       END UnitNumber, 
       OP.ProjectID,
       OP.CompanyID,
       RE.Value AS DBName,
       CASE WHEN ISNULL(TM.VATAmount,0) = 0 THEN 0 ELSE ISNULL(OP.IsDeferVat, 0) END IsDeferVat,
       PJ.ProjectName, 
       ISNULL(OP.TaxID, '') TaxID, 
       ISNULL(OP.Address, '') Address, 
       ISNULL(PT.AccountCode, '') AccountCode,
       ISNULL(PT.MaterialCode, '') MaterialCode,
       ISNULL(PT.TermName,'') TermName,
       ISNULL(SAP.APIResponseCode,'500') APIResponseCode,
       ISNULL(SAP.SAPRefNo,'') APIResponseData
FROM 
	Sys_FI_OtherPayment OP
    INNER JOIN Sys_REM_ContractTerm TM ON OP.OPaymentID = TM.ReferenceID
                                          AND TM.ReferenceType = OP.OPaymentType
    INNER JOIN Sys_REM_ContractTerm_Extension TX ON TX.ContractTermID = TM.ContractTermID
    LEFT JOIN Sys_Master_Projects PJ ON PJ.ProjectID = OP.ProjectID
    LEFT JOIN Sys_Master_Units U ON U.UnitID = OP.UnitID
                                     AND ISNULL(U.IsDelete, 0) = 0
    LEFT JOIN Sys_ACC_PaymentTerm PT ON TM.TermID = PT.TermID
                                         AND TM.TermGroup = pt.TermGroup
                                         AND ISNULL(pt.isdelete, 0) = 0
	LEFT JOIN Sys_Conf_RealEstate RE ON RE.KEYNAME = 'DBName' AND OP.CompanyID = RE.CompanyID
    LEFT JOIN SAP_Interface_Log SAP ON SAP.REMRefID = OP.OPaymentID 
                                         AND SAP.Module = 'GoodReceipt' 
                                         AND SAP.MethodName = 'CreateGoodReceipt'
WHERE
    1=1
    AND OP.Status = 'A'
    AND ISNULL(SAP.APIResponseCode,'500') = '500'
    AND ISNULL(RE.Value,'') <> ''";

            if (!string.IsNullOrEmpty(RefID))
            {
                sql += $" AND OP.OPaymentID = '{RefID}'";
            }

            return sql;
        }
        private string CreateGoodReceipt(dynamic Data)
        {
            ;
            string OPaymentID = (string)Data.OPaymentID;
            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodReceipt",
                    "CreateGoodReceipt",
                    OPaymentID,
                    "Sys_FI_OtherPayment.OPaymentID",
                    DocEntry,
                    "OPDN.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                string sql = GetSqlCommandCreateGoodReceipt(OPaymentID);
                DataTable dt = dbHelp.ExecuteDataTable(sql, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document
                {
                    CardCode = "V00001",
                    CardNumber = "99999999",    // Vendor Ref. No.
                    PostingDate = DateTime.Now,
                    DocDueDate = DateTime.Now.AddDays(7),
                    DocumentDate = DateTime.Now,
                    DocType = SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes,
                    UDF_RefNo = dt.Rows[0]["OPaymentID"].ToString(),
                    UDF_CustName = dt.Rows[0]["CustomerName"].ToString(),
                    UDF_UnitRef = dt.Rows[0]["UnitNumber"].ToString(),
                    UDF_Project = dt.Rows[0]["ProjectID"].ToString(),
                    UDF_ProjectName = dt.Rows[0]["ProjectName"].ToString(),
                    UDF_TaxID = dt.Rows[0]["TaxID"].ToString(),
                    UDF_Address = dt.Rows[0]["Address"].ToString(),
                    IsDeferVAT = Convert.ToBoolean(dt.Rows[0]["IsDeferVAT"])
                };

                foreach (DataRow dr in dt.Rows)
                {
                    string ItemDescription = dr["TermName"].ToString();

                    if (!string.IsNullOrEmpty(dr["StartDate"].ToString()) && !string.IsNullOrEmpty(dr["EndDate"].ToString()))
                    {
                        ItemDescription += " (" + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["StartDate"])) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EndDate"])) + ")";
                    }

                    doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        ItemCode = dr["MaterialCode"].ToString(),
                        ItemDescription = ItemDescription,
                        Qty = 1,
                        TaxCode = dr["InterfaceVATCode"].ToString(),
                        UnitPrice = dr["Amount"].ToString()
                    });
                }

                try
                {
                    SAPB1.CreateDocument(doc, out DocEntry, out DocNum, out GLDocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                dbHelp.CommitTransaction(trans);
                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message;
                dbHelp.RollbackTransaction(trans);
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                    OPaymentID,
                    DocEntry,
                    DocNum,
                    GLDocNum,
                    (int)ResponseCode,
                    ErrorMessage,
                    SAPStatus,
                    SAPErrorMessage,
                    LogDetail
                    );
            }
        }
        #endregion
        #endregion


        [HttpPost]
        [Route("api/create_viewinterfacelog")]
        [AllowAnonymous]
        public object Create_ViewInterfaceLog(dynamic Data)
        {
            DBHelper dbHelp = ICON.Configuration.Database.ConnectionPhase2;
            DBHelper dbHelp_log = new DBHelper(ICON.Configuration.Database.Log_ConnectionString, null);
            IDbTransaction dbTrans = null;
            IDbTransaction dbTrans_log = null;
            try
            {
                string UserID = "SAPApi";
                DateTime NOW = DateTime.Now;
                string dbName_log = System.Configuration.ConfigurationManager.AppSettings["log_dbname"].ToString();
                Drop_Create_ViewInterfaceLog(dbHelp, dbTrans, dbName_log);

                return new { status = true, message = "success" };
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error" };
            }
        }

        [HttpPost]
        [Route("api/create_invoice")]
        [AllowAnonymous]
        public object Create_Invoice(dynamic Data)
        {
            DBHelper dbHelp = ICON.Configuration.Database.ConnectionPhase2;
            DBHelper dbHelp_log = new DBHelper(ICON.Configuration.Database.Log_ConnectionString, null);
            IDbTransaction dbTrans = null;
            IDbTransaction dbTrans_log = null;
            try
            {
                string UserID = "SAPApi";
                DateTime NOW = DateTime.Now;
                string dbName_log = System.Configuration.ConfigurationManager.AppSettings["log_dbname"].ToString();
                Drop_Create_ViewInterfaceLog(dbHelp, dbTrans, dbName_log);

                string OPaymentID = Data != null && !(string.IsNullOrEmpty((string)Data.OPaymentID)) ? Data.OPaymentID : "";
                string SQL_Log = $@"EXEC B1_CREATE_INVOICE_LOG @OPaymentID = '{OPaymentID}'";
                string SQL_LogDetail = $@"EXEC B1_CREATE_INVOICE_LOGDETAIL @OPaymentID = '{OPaymentID}'";
                string SQL_DocLine = $@"EXEC B1_CREATE_INVOICE_DOCLINE @OPaymentID = '{OPaymentID}'";
                string SQL_DocSplLine = $@"EXEC B1_CREATE_INVOICE_DOCSPLLINE @OPaymentID = '{OPaymentID}'";

                List<ICON.Interface.InterfaceLog> Log_List = SQL_Log.ToString().LoadListByQuery<ICON.Interface.InterfaceLog>(dbHelp, null);
                List<ICON.Interface.InterfaceLogDetail> LogDetail_List = SQL_LogDetail.ToString().LoadListByQuery<ICON.Interface.InterfaceLogDetail>(dbHelp, null);
                List<Dictionary<string, object>> Doc_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_LogDetail.ToString());
                List<Dictionary<string, object>> DocLine_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_DocLine.ToString());
                List<Dictionary<string, object>> DocSplLine_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_DocSplLine.ToString());

                List<string> LogID_List = new List<string>();
                try
                {
                    dbTrans_log = dbHelp_log.BeginTransaction();
                    dbTrans = dbHelp.BeginTransaction();

                    LogID_List = Process_Documents(dbHelp, dbTrans, dbHelp_log, dbTrans_log, UserID, NOW, Log_List, LogDetail_List, Doc_DicList, DocLine_DicList, DocSplLine_DicList);

                    dbHelp_log.CommitTransaction(dbTrans_log);
                    dbHelp.CommitTransaction(dbTrans);
                }
                catch (Exception)
                {
                    if (dbTrans_log != null) dbHelp_log.RollbackTransaction(dbTrans_log);
                    if (dbTrans != null) dbHelp.RollbackTransaction(dbTrans);
                    throw;
                }
                return new { status = true, message = "success", result = LogID_List };
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/cancel_invoice")]
        [AllowAnonymous]
        public object Cancel_Invoice(dynamic Data)
        {
            DBHelper dbHelp = ICON.Configuration.Database.ConnectionPhase2;
            DBHelper dbHelp_log = new DBHelper(ICON.Configuration.Database.Log_ConnectionString, null);
            IDbTransaction dbTrans = null;
            IDbTransaction dbTrans_log = null;
            try
            {
                string UserID = "SAPApi";
                DateTime NOW = DateTime.Now;
                string dbName_log = System.Configuration.ConfigurationManager.AppSettings["log_dbname"].ToString();
                Drop_Create_ViewInterfaceLog(dbHelp, dbTrans, dbName_log);

                string OPaymentID = Data != null && !(string.IsNullOrEmpty((string)Data.OPaymentID)) ? Data.OPaymentID : "";
                string SQL_Log = $@"EXEC B1_CANCEL_INVOICE_LOG @OPaymentID = '{OPaymentID}'";
                string SQL_LogDetail = $@"EXEC B1_CANCEL_INVOICE_LOGDETAIL @OPaymentID = '{OPaymentID}'";
                string SQL_DocLine = $@"EXEC B1_CANCEL_INVOICE_DOCLINE @OPaymentID = '{OPaymentID}'";
                string SQL_DocSplLine = $@"EXEC B1_CANCEL_INVOICE_DOCSPLLINE @OPaymentID = '{OPaymentID}'";

                List<ICON.Interface.InterfaceLog> Log_List = SQL_Log.ToString().LoadListByQuery<ICON.Interface.InterfaceLog>(dbHelp, null);
                List<ICON.Interface.InterfaceLogDetail> LogDetail_List = SQL_LogDetail.ToString().LoadListByQuery<ICON.Interface.InterfaceLogDetail>(dbHelp, null);
                List<Dictionary<string, object>> Doc_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_LogDetail.ToString());
                List<Dictionary<string, object>> DocLine_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_DocLine.ToString());
                List<Dictionary<string, object>> DocSplLine_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_DocSplLine.ToString());

                List<string> LogID_List = new List<string>();
                try
                {
                    dbTrans_log = dbHelp_log.BeginTransaction();
                    dbTrans = dbHelp.BeginTransaction();

                    LogID_List = Process_Documents(dbHelp, dbTrans, dbHelp_log, dbTrans_log, UserID, NOW, Log_List, LogDetail_List, Doc_DicList, DocLine_DicList, DocSplLine_DicList);

                    dbHelp_log.CommitTransaction(dbTrans_log);
                    dbHelp.CommitTransaction(dbTrans);
                }
                catch (Exception)
                {
                    if (dbTrans_log != null) dbHelp_log.RollbackTransaction(dbTrans_log);
                    if (dbTrans != null) dbHelp.RollbackTransaction(dbTrans);
                    throw;
                }
                return new { status = true, message = "success", result = LogID_List };
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/create_payment")]
        [AllowAnonymous]
        public object Create_Payment(dynamic Data)
        {
            DBHelper dbHelp = ICON.Configuration.Database.ConnectionPhase2;
            DBHelper dbHelp_log = new DBHelper(ICON.Configuration.Database.Log_ConnectionString, null);
            IDbTransaction dbTrans = null;
            IDbTransaction dbTrans_log = null;
            try
            {
                string UserID = "SAPApi";
                DateTime NOW = DateTime.Now;
                string dbName_log = System.Configuration.ConfigurationManager.AppSettings["log_dbname"].ToString();
                Drop_Create_ViewInterfaceLog(dbHelp, dbTrans, dbName_log);

                string ReceiptID = Data != null && !(string.IsNullOrEmpty((string)Data.ReceiptID)) ? Data.ReceiptID : "";
                string SQL_Log = $@"EXEC B1_CREATE_PAYMENT_LOG @ReceiptID = '{ReceiptID}'";
                string SQL_LogDetail = $@"EXEC B1_CREATE_PAYMENT_LOGDETAIL @ReceiptID = '{ReceiptID}'";
                string SQL_PayACC = $@"EXEC B1_CREATE_PAYMENT_PAYACC @ReceiptID = '{ReceiptID}'";
                string SQL_PayINV = $@"EXEC B1_CREATE_PAYMENT_PAYINV @ReceiptID = '{ReceiptID}'";
                string SQL_PayCQ = $@"EXEC B1_CREATE_PAYMENT_PAYCQ @ReceiptID = '{ReceiptID}'";
                string SQL_PayCR = $@"EXEC B1_CREATE_PAYMENT_PAYCR @ReceiptID = '{ReceiptID}'";

                List<Dictionary<string, object>> Pay_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_LogDetail.ToString());
                List<Dictionary<string, object>> PayACC_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_PayACC.ToString());
                List<Dictionary<string, object>> PayINV_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_PayINV.ToString());
                List<Dictionary<string, object>> PayCQ_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_PayCQ.ToString());
                List<Dictionary<string, object>> PayCR_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_PayCR.ToString());
                List<ICON.Interface.InterfaceLog> Log_List = SQL_Log.ToString().LoadListByQuery<ICON.Interface.InterfaceLog>(dbHelp, null);
                List<ICON.Interface.InterfaceLogDetail> LogDetail_List = GlobalDatabase.LoadListByDict<ICON.Interface.InterfaceLogDetail>(Pay_DicList);

                List<string> LogID_List = new List<string>();
                try
                {
                    dbTrans_log = dbHelp_log.BeginTransaction();
                    dbTrans = dbHelp.BeginTransaction();

                    LogID_List = Process_Payments(dbHelp, dbTrans, dbHelp_log, dbTrans_log, UserID, NOW, Log_List, LogDetail_List, Pay_DicList, PayACC_DicList, PayINV_DicList, PayCQ_DicList, PayCR_DicList);

                    dbHelp_log.CommitTransaction(dbTrans_log);
                    dbHelp.CommitTransaction(dbTrans);
                }
                catch (Exception)
                {
                    if (dbTrans_log != null) dbHelp_log.RollbackTransaction(dbTrans_log);
                    if (dbTrans != null) dbHelp.RollbackTransaction(dbTrans);
                    throw;
                }
                return new { status = true, message = "success", result = LogID_List };
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/cancel_payment")]
        [AllowAnonymous]
        public object Cancel_Payment(dynamic Data)
        {
            DBHelper dbHelp = ICON.Configuration.Database.ConnectionPhase2;
            DBHelper dbHelp_log = new DBHelper(ICON.Configuration.Database.Log_ConnectionString, null);
            IDbTransaction dbTrans = null;
            IDbTransaction dbTrans_log = null;
            try
            {
                string UserID = "SAPApi";
                DateTime NOW = DateTime.Now;
                string dbName_log = System.Configuration.ConfigurationManager.AppSettings["log_dbname"].ToString();
                Drop_Create_ViewInterfaceLog(dbHelp, dbTrans, dbName_log);

                string ReceiptID = Data != null && !(string.IsNullOrEmpty((string)Data.ReceiptID)) ? Data.ReceiptID : "";
                string SQL_Log = $@"EXEC B1_CANCEL_PAYMENT_LOG @ReceiptID = '{ReceiptID}'";
                string SQL_LogDetail = $@"EXEC B1_CANCEL_PAYMENT_LOGDETAIL @ReceiptID = '{ReceiptID}'";
                string SQL_PayACC = $@"EXEC B1_CANCEL_PAYMENT_PAYACC @ReceiptID = '{ReceiptID}'";
                string SQL_PayINV = $@"EXEC B1_CANCEL_PAYMENT_PAYINV @ReceiptID = '{ReceiptID}'";
                string SQL_PayCQ = $@"EXEC B1_CANCEL_PAYMENT_PAYCQ @ReceiptID = '{ReceiptID}'";
                string SQL_PayCR = $@"EXEC B1_CANCEL_PAYMENT_PAYCR @ReceiptID = '{ReceiptID}'";

                List<Dictionary<string, object>> Pay_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_LogDetail.ToString());
                List<Dictionary<string, object>> PayACC_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_PayACC.ToString());
                List<Dictionary<string, object>> PayINV_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_PayINV.ToString());
                List<Dictionary<string, object>> PayCQ_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_PayCQ.ToString());
                List<Dictionary<string, object>> PayCR_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_PayCR.ToString());
                List<ICON.Interface.InterfaceLog> Log_List = SQL_Log.ToString().LoadListByQuery<ICON.Interface.InterfaceLog>(dbHelp, null);
                List<ICON.Interface.InterfaceLogDetail> LogDetail_List = GlobalDatabase.LoadListByDict<ICON.Interface.InterfaceLogDetail>(Pay_DicList);

                List<string> LogID_List = new List<string>();
                try
                {
                    dbTrans_log = dbHelp_log.BeginTransaction();
                    dbTrans = dbHelp.BeginTransaction();

                    LogID_List = Process_Payments(dbHelp, dbTrans, dbHelp_log, dbTrans_log, UserID, NOW, Log_List, LogDetail_List, Pay_DicList, PayACC_DicList, PayINV_DicList, PayCQ_DicList, PayCR_DicList);

                    dbHelp_log.CommitTransaction(dbTrans_log);
                    dbHelp.CommitTransaction(dbTrans);
                }
                catch (Exception)
                {
                    if (dbTrans_log != null) dbHelp_log.RollbackTransaction(dbTrans_log);
                    if (dbTrans != null) dbHelp.RollbackTransaction(dbTrans);
                    throw;
                }
                return new { status = true, message = "success", result = LogID_List };
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/create_journalentries")]
        [AllowAnonymous]
        public object Create_JournalEntries(dynamic Data)
        {
            DBHelper dbHelp = ICON.Configuration.Database.ConnectionPhase2;
            DBHelper dbHelp_log = new DBHelper(ICON.Configuration.Database.Log_ConnectionString, null);
            IDbTransaction dbTrans = null;
            IDbTransaction dbTrans_log = null;
            try
            {
                string UserID = "SAPApi";
                DateTime NOW = DateTime.Now;
                string dbName_log = System.Configuration.ConfigurationManager.AppSettings["log_dbname"].ToString();
                Drop_Create_ViewInterfaceLog(dbHelp, dbTrans, dbName_log);

                string GLVoucher = Data != null && !(string.IsNullOrEmpty((string)Data.GLVoucher)) ? Data.GLVoucher : "";
                string SQL_Log = $@"EXEC B1_CREATE_JOURNALENTRIES_LOG @GLVoucher = '{GLVoucher}'";
                string SQL_LogDetail = $@"EXEC B1_CREATE_JOURNALENTRIES_LOGDETAIL @GLVoucher = '{GLVoucher}'";
                string SQL_JELine = $@"EXEC B1_CREATE_JOURNALENTRIES_JELINE @GLVoucher = '{GLVoucher}'";

                List<ICON.Interface.InterfaceLog> Log_List = SQL_Log.ToString().LoadListByQuery<ICON.Interface.InterfaceLog>(dbHelp, null);
                List<ICON.Interface.InterfaceLogDetail> LogDetail_List = SQL_LogDetail.ToString().LoadListByQuery<ICON.Interface.InterfaceLogDetail>(dbHelp, null);
                List<Dictionary<string, object>> JE_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_LogDetail.ToString());
                List<Dictionary<string, object>> JELine_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_JELine.ToString());

                List<string> LogID_List = new List<string>();
                try
                {
                    dbTrans_log = dbHelp_log.BeginTransaction();
                    dbTrans = dbHelp.BeginTransaction();

                    LogID_List = Process_JournalEntries(dbHelp, dbTrans, dbHelp_log, dbTrans_log, UserID, NOW, Log_List, LogDetail_List, JE_DicList, JELine_DicList);
                    List<string> GLVoucherList = Log_List.Select(x => x.REMRefID).ToList();

                    if (GLVoucherList.Count > 0)
                    {
                        string sql = $"UPDATE Sys_ACC_GeneralLedgers SET PostStatus = 'P', PostDate = GETDATE() WHERE GLVoucher IN ('{string.Join("','", GLVoucherList)}') AND ISNULL(PostStatus,'') = 'A' ";
                        dbHelp.ExecuteNonQuery(sql, dbTrans);
                    }

                    dbHelp_log.CommitTransaction(dbTrans_log);
                    dbHelp.CommitTransaction(dbTrans);
                }
                catch (Exception)
                {
                    if (dbTrans_log != null) dbHelp_log.RollbackTransaction(dbTrans_log);
                    if (dbTrans != null) dbHelp.RollbackTransaction(dbTrans);
                    throw;
                }
                return new { status = true, message = "success", result = LogID_List };
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
        }
        
        [HttpPost]
        [Route("api/create_creditmemo")]
        [AllowAnonymous]
        public object Create_CreditMemo(dynamic Data)
        {
            DBHelper dbHelp = ICON.Configuration.Database.ConnectionPhase2;
            DBHelper dbHelp_log = new DBHelper(ICON.Configuration.Database.Log_ConnectionString, null);
            IDbTransaction dbTrans = null;
            IDbTransaction dbTrans_log = null;
            try
            {
                string UserID = "SAPApi";
                DateTime NOW = DateTime.Now;
                string dbName_log = System.Configuration.ConfigurationManager.AppSettings["log_dbname"].ToString();
                Drop_Create_ViewInterfaceLog(dbHelp, dbTrans, dbName_log);

                string CreditNoteID = Data != null && !(string.IsNullOrEmpty((string)Data.CreditNoteID)) ? Data.CreditNoteID : "";
                string SQL_Log = $@"EXEC B1_CREATE_CREDITMEMO_LOG @CreditNoteID = '{CreditNoteID}'";
                string SQL_LogDetail = $@"EXEC B1_CREATE_CREDITMEMO_LOGDETAIL @CreditNoteID = '{CreditNoteID}'";
                string SQL_DocLine = $@"EXEC B1_CREATE_CREDITMEMO_DOCLINE @CreditNoteID = '{CreditNoteID}'";
                string SQL_DocSplLine = $@"EXEC B1_CREATE_CREDITMEMO_DOCSPLLINE @CreditNoteID = '{CreditNoteID}'";

                List<ICON.Interface.InterfaceLog> Log_List = SQL_Log.ToString().LoadListByQuery<ICON.Interface.InterfaceLog>(dbHelp, null);
                List<ICON.Interface.InterfaceLogDetail> LogDetail_List = SQL_LogDetail.ToString().LoadListByQuery<ICON.Interface.InterfaceLogDetail>(dbHelp, null);
                List<Dictionary<string, object>> Doc_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_LogDetail.ToString());
                List<Dictionary<string, object>> DocLine_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_DocLine.ToString());
                List<Dictionary<string, object>> DocSplLine_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_DocSplLine.ToString());

                List<string> LogID_List = new List<string>();
                try
                {
                    dbTrans_log = dbHelp_log.BeginTransaction();
                    dbTrans = dbHelp.BeginTransaction();

                    LogID_List = Process_Documents(dbHelp, dbTrans, dbHelp_log, dbTrans_log, UserID, NOW, Log_List, LogDetail_List, Doc_DicList, DocLine_DicList, DocSplLine_DicList);

                    dbHelp_log.CommitTransaction(dbTrans_log);
                    dbHelp.CommitTransaction(dbTrans);
                }
                catch (Exception)
                {
                    if (dbTrans_log != null) dbHelp_log.RollbackTransaction(dbTrans_log);
                    if (dbTrans != null) dbHelp.RollbackTransaction(dbTrans);
                    throw;
                }
                return new { status = true, message = "success", result = LogID_List };
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/cancel_creditmemo")]
        [AllowAnonymous]
        public object Cancel_CreditMemo(dynamic Data)
        {
            DBHelper dbHelp = ICON.Configuration.Database.ConnectionPhase2;
            DBHelper dbHelp_log = new DBHelper(ICON.Configuration.Database.Log_ConnectionString, null);
            IDbTransaction dbTrans = null;
            IDbTransaction dbTrans_log = null;
            try
            {
                string UserID = "SAPApi";
                DateTime NOW = DateTime.Now;
                string dbName_log = System.Configuration.ConfigurationManager.AppSettings["log_dbname"].ToString();
                Drop_Create_ViewInterfaceLog(dbHelp, dbTrans, dbName_log);

                string CreditNoteID = Data != null && !(string.IsNullOrEmpty((string)Data.CreditNoteID)) ? Data.CreditNoteID : "";
                string SQL_Log = $@"EXEC B1_CANCEL_CREDITMEMO_LOG @CreditNoteID = '{CreditNoteID}'";
                string SQL_LogDetail = $@"EXEC B1_CANCEL_CREDITMEMO_LOGDETAIL @CreditNoteID = '{CreditNoteID}'";
                string SQL_DocLine = $@"EXEC B1_CANCEL_CREDITMEMO_DOCLINE @CreditNoteID = '{CreditNoteID}'";
                string SQL_DocSplLine = $@"EXEC B1_CANCEL_CREDITMEMO_DOCSPLLINE @CreditNoteID = '{CreditNoteID}'";

                List<ICON.Interface.InterfaceLog> Log_List = SQL_Log.ToString().LoadListByQuery<ICON.Interface.InterfaceLog>(dbHelp, null);
                List<ICON.Interface.InterfaceLogDetail> LogDetail_List = SQL_LogDetail.ToString().LoadListByQuery<ICON.Interface.InterfaceLogDetail>(dbHelp, null);
                List<Dictionary<string, object>> Doc_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_LogDetail.ToString());
                List<Dictionary<string, object>> DocLine_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_DocLine.ToString());
                List<Dictionary<string, object>> DocSplLine_DicList = GlobalDatabase.LoadDictByQuery(dbHelp, SQL_DocSplLine.ToString());

                List<string> LogID_List = new List<string>();
                try
                {
                    dbTrans_log = dbHelp_log.BeginTransaction();
                    dbTrans = dbHelp.BeginTransaction();

                    LogID_List = Process_Documents(dbHelp, dbTrans, dbHelp_log, dbTrans_log, UserID, NOW, Log_List, LogDetail_List, Doc_DicList, DocLine_DicList, DocSplLine_DicList);

                    dbHelp_log.CommitTransaction(dbTrans_log);
                    dbHelp.CommitTransaction(dbTrans);
                }
                catch (Exception)
                {
                    if (dbTrans_log != null) dbHelp_log.RollbackTransaction(dbTrans_log);
                    if (dbTrans != null) dbHelp.RollbackTransaction(dbTrans);
                    throw;
                }
                return new { status = true, message = "success", result = LogID_List };
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/posting_tob1")]
        [AllowAnonymous]
        public object Posting_ToB1(dynamic Data)
        {
            DBHelper dbHelp = ICON.Configuration.Database.ConnectionPhase2;
            DBHelper dbHelp_log = new DBHelper(ICON.Configuration.Database.Log_ConnectionString, null);
            IDbTransaction dbTrans = null;
            IDbTransaction dbTrans_log = null;
            try
            {
                string UserID = "SAPApi";
                DateTime NOW = DateTime.Now;
                string dbName_log = System.Configuration.ConfigurationManager.AppSettings["log_dbname"].ToString();
                Drop_Create_ViewInterfaceLog(dbHelp, dbTrans, dbName_log);

                #region SQL
                string SQL_Log = $@"
SELECT LG.*, C.Value AS CompanyDB
FROM vw_B1_InterfaceLog LG
INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
WHERE ISNULL(LG.APIResponseCode, '500') = '500'
	AND LG.MethodName = '{Data.MethodName}'
";

                string SQL_LogDetail = $@"
SELECT LGD.*, C.Value AS CompanyDB
FROM vw_B1_InterfaceLogDetail LGD
INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LGD.REMCompanyID AND ISNULL(C.Value,'') <> ''
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
		WHERE LGD.InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_UField = $@"
SELECT *
FROM vw_B1_Fields DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_JE = $@"
SELECT *
FROM vw_B1_JournalEntries DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_JELine = $@"
SELECT *
FROM vw_B1_JournalEntries_Lines DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_Doc = $@"
SELECT *
FROM vw_B1_Documents DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_DocLine = $@"
SELECT *
FROM vw_B1_Document_Lines DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_DocSpLine = $@"
SELECT *
FROM vw_B1_Document_SpecialLines DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_PayACC = $@"
SELECT *
FROM vw_B1_Payments_Accounts DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_Pay = $@"
SELECT *
FROM vw_B1_Payments DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_PayINV = $@"
SELECT *
FROM vw_B1_Payments_Invoices DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_PayCR = $@"
SELECT *
FROM vw_B1_Payments_CreditCards DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";

                string SQL_PayCQ = $@"
SELECT *
FROM vw_B1_Payments_Checks DOC
WHERE EXISTS (
		SELECT *
		FROM vw_B1_InterfaceLog LG
        INNER JOIN Sys_Conf_RealEstate C ON C.KEYNAME = 'DBName' AND C.GroupName = 'SAPInterface' AND C.CompanyID = LG.REMCompanyID AND ISNULL(C.Value,'') <> ''
		WHERE DOC.L_InterfaceLogID = LG.InterfaceLogID
			AND ISNULL(LG.APIResponseCode, '500') = '500'
			AND LG.MethodName = '{Data.MethodName}'
		)";
                #endregion

                List<ICON.Interface.InterfaceLog_ext> Log_List = SQL_Log.ToString().LoadListByQuery<ICON.Interface.InterfaceLog_ext>(dbHelp, null);
                List<ICON.Interface.InterfaceLogDetail_ext> LogDetail_List = SQL_LogDetail.ToString().LoadListByQuery<ICON.Interface.InterfaceLogDetail_ext>(dbHelp, null);
                List<ICON.Interface.Fields> UField_List = SQL_UField.ToString().LoadListByQuery<ICON.Interface.Fields>(dbHelp, null);
                List<ICON.Interface.JournalEntries> JE_List = SQL_JE.ToString().LoadListByQuery<ICON.Interface.JournalEntries>(dbHelp, null);
                List<ICON.Interface.JournalEntries_Lines> JELine_List = SQL_JELine.ToString().LoadListByQuery<ICON.Interface.JournalEntries_Lines>(dbHelp, null);
                List<ICON.Interface.Documents> Doc_List = SQL_Doc.ToString().LoadListByQuery<ICON.Interface.Documents>(dbHelp, null);
                List<ICON.Interface.Document_Lines> DocLine_List = SQL_DocLine.ToString().LoadListByQuery<ICON.Interface.Document_Lines>(dbHelp, null);
                List<ICON.Interface.Document_SpecialLines> DocSpLine_List = SQL_DocSpLine.ToString().LoadListByQuery<ICON.Interface.Document_SpecialLines>(dbHelp, null);
                List<ICON.Interface.Payments> Pay_List = SQL_Pay.ToString().LoadListByQuery<ICON.Interface.Payments>(dbHelp, null);
                List<ICON.Interface.Payments_Accounts> PayACC_List = SQL_PayACC.ToString().LoadListByQuery<ICON.Interface.Payments_Accounts>(dbHelp, null);
                List<ICON.Interface.Payments_Invoices> PayINV_List = SQL_PayINV.ToString().LoadListByQuery<ICON.Interface.Payments_Invoices>(dbHelp, null);
                List<ICON.Interface.Payments_CreditCards> PayCR_List = SQL_PayCR.ToString().LoadListByQuery<ICON.Interface.Payments_CreditCards>(dbHelp, null);
                List<ICON.Interface.Payments_Checks> PayCQ_List = SQL_PayCQ.ToString().LoadListByQuery<ICON.Interface.Payments_Checks>(dbHelp, null);

                foreach (ICON.Interface.InterfaceLog_ext Log in Log_List)
                {
                    SAP_B1 SAP_B1 = new SAP_B1();
                    SAP_B1Header SAP_B1Header = new SAP_B1Header();

                    List<ICON.Interface.InterfaceLogDetail_ext> Loop_LogDetail_List = LogDetail_List.FindAll(x => x.InterfaceLogID == Log.InterfaceLogID);
                    try
                    {
                        dbTrans_log = dbHelp_log.BeginTransaction();

                        foreach (ICON.Interface.InterfaceLogDetail_ext Loop_LogDetail in Loop_LogDetail_List)
                        {
                            SAP_B1Header.B1Entity = new SAP_B1Entity(Log, Loop_LogDetail);
                            SAP_B1Header.Method = "Error";
                            SAP_B1Header.Event = "Reset";
                            SAP_B1.ProcessSAP_B1(SAP_B1Header);
                        }

                        SAP_B1Header.Method = "Transaction";
                        SAP_B1Header.Event = "Init";
                        SAP_B1.ProcessSAP_B1(SAP_B1Header);

                        SAP_B1Header.Method = "Transaction";
                        SAP_B1Header.Event = "BeginTransaction";
                        SAP_B1.ProcessSAP_B1(SAP_B1Header);

                        foreach (ICON.Interface.InterfaceLogDetail_ext Loop_LogDetail in Loop_LogDetail_List)
                        {
                            SAP_B1Header.B1Entity = new SAP_B1Entity(Log, Loop_LogDetail);
                            SAP_B1Header.B1Entity.UField_List = UField_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.B1Entity.JE_List = JE_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.B1Entity.JELine_List = JELine_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.B1Entity.Doc_List = Doc_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.B1Entity.DocLine_List = DocLine_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.B1Entity.DocSpLine_List = DocSpLine_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.B1Entity.Pay_List = Pay_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.B1Entity.PayACC_List = PayACC_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.B1Entity.PayINV_List = PayINV_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.B1Entity.PayCR_List = PayCR_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.B1Entity.PayCQ_List = PayCQ_List.FindAll(x => x.L_InterfaceLogID == Loop_LogDetail.InterfaceLogID && x.L_InterfaceLogDetailID == Loop_LogDetail.InterfaceLogDetailID);
                            SAP_B1Header.SetByB1Entity(SAP_B1Header.B1Entity);
                            SAP_B1.ProcessSAP_B1(SAP_B1Header);
                        }

                        SAP_B1Header.Method = "Transaction";
                        SAP_B1Header.Event = "CommitTransaction";
                        SAP_B1.ProcessSAP_B1(SAP_B1Header);

                        SAP_B1Header.Method = "Transaction";
                        SAP_B1Header.Event = "Dispose";
                        SAP_B1.ProcessSAP_B1(SAP_B1Header);

                        System.Text.StringBuilder SQL_ForLog = new System.Text.StringBuilder();
                        Log.APIResponseCode = (int)HttpStatusCode.OK;
                        Log.EditAPIErrorMessage = true;
                        Log.APIErrorMessage = null;
                        Log.RetryCount = (Log.RetryCount == null ? 0 : Log.RetryCount + 1);
                        Log.UpdateBy = UserID;
                        Log.UpdateDate = DateTime.Now;
                        SQL_ForLog.Append(Log.REMDatabase_GetUpdateCommandStr());
                        foreach (ICON.Interface.InterfaceLogDetail_ext Loop_LogDetail in Loop_LogDetail_List)
                        {
                            Loop_LogDetail.ModifyBy = UserID;
                            Loop_LogDetail.ModifyDate = DateTime.Now;
                            SQL_ForLog.Append(Loop_LogDetail.REMDatabase_GetUpdateCommandStr());
                        }

                        if (!string.IsNullOrEmpty(SQL_ForLog.ToString())) dbHelp_log.ExecuteNonQuery(SQL_ForLog.ToString(), dbTrans_log);
                        dbHelp_log.CommitTransaction(dbTrans_log);
                    }
                    catch (Exception ex)
                    {
                        SAP_B1Header.Method = "Transaction";
                        SAP_B1Header.Event = "RollbackTransaction";
                        SAP_B1.ProcessSAP_B1(SAP_B1Header);

                        SAP_B1Header.Method = "Transaction";
                        SAP_B1Header.Event = "Dispose";
                        SAP_B1.ProcessSAP_B1(SAP_B1Header);

                        if (dbTrans_log != null) dbHelp_log.RollbackTransaction(dbTrans_log);
                        if (dbTrans != null) dbHelp.RollbackTransaction(dbTrans);

                        System.Text.StringBuilder SQL_ForLog = new System.Text.StringBuilder();
                        Log.APIResponseCode = (int)HttpStatusCode.InternalServerError;
                        Log.APIErrorMessage = ex.Message;
                        Log.RetryCount = (Log.RetryCount == null ? 0 : Log.RetryCount + 1);
                        Log.UpdateBy = UserID;
                        Log.UpdateDate = DateTime.Now;
                        SQL_ForLog.Append(Log.REMDatabase_GetUpdateCommandStr());
                        foreach (ICON.Interface.InterfaceLogDetail_ext Loop_LogDetail in Loop_LogDetail_List)
                        {
                            Loop_LogDetail.ModifyBy = UserID;
                            Loop_LogDetail.ModifyDate = DateTime.Now;
                            SQL_ForLog.Append(Loop_LogDetail.REMDatabase_GetUpdateCommandStr());
                        }

                        if (!string.IsNullOrEmpty(SQL_ForLog.ToString())) dbHelp_log.ExecuteNonQuery(SQL_ForLog.ToString());
                    }

                }

                try
                {
                    dbTrans = dbHelp.BeginTransaction();

                    string SQL_ForREM = $@"
UPDATE REMLG SET REMLG.SAPRefID = LG.SAPRefID
, REMLG.SAPRefDescription = LG.SAPRefDescription
, REMLG.SAPRefNo = LG.SAPRefNo
, REMLG.SAPRefGLNo = LG.SAPRefGLNo
, REMLG.APIRequestData = LG.APIRequestData
, REMLG.APIResponseCode = LG.APIResponseCode
, REMLG.APIErrorMessage = LG.APIErrorMessage
, REMLG.SAPStatusCode = LG.SAPStatusCode
, REMLG.SAPErrorMessage = LG.SAPErrorMessage
, REMLG.UpdateDate = LG.UpdateDate
, REMLG.UpdateBy = LG.UpdateBy
, REMLG.RetryCount = LG.RetryCount
, REMLG.APIResponseData = LG.APIResponseData
FROM SAP_Interface_Log REMLG
INNER JOIN vw_B1_InterfaceLog LG ON REMLG.InterfaceLogID = LG.InterfaceLogID

UPDATE REMLGD SET REMLGD.SAPRefID = LGD.SAPRefID
, REMLGD.SAPRefDescription = LGD.SAPRefDescription
, REMLGD.SAPRefNo = LGD.SAPRefNo
, REMLGD.SAPRefGLNo = LGD.SAPRefGLNo
, REMLGD.ModifyDate = LGD.ModifyDate
, REMLGD.ModifyBy = LGD.ModifyBy
FROM SAP_Interface_Log_Detail REMLGD
INNER JOIN vw_B1_InterfaceLogDetail LGD ON REMLGD.InterfaceLogDetailID = LGD.InterfaceLogID
";

                    if (!string.IsNullOrEmpty(SQL_ForREM.ToString())) dbHelp.ExecuteNonQuery(SQL_ForREM.ToString(), dbTrans);

                    dbHelp.CommitTransaction(dbTrans);
                }
                catch (Exception ex)
                {
                    if (dbTrans_log != null) dbHelp_log.RollbackTransaction(dbTrans_log);
                    if (dbTrans != null) dbHelp.RollbackTransaction(dbTrans);
                }

                List<string> SuccessLogID_List = Log_List.FindAll(x => x.APIResponseCode != (int)HttpStatusCode.InternalServerError).GroupBy(x => x.InterfaceLogID).Select(x => x.Key).ToList();
                List<string> ErrorLogID_List = Log_List.FindAll(x => x.APIResponseCode == (int)HttpStatusCode.InternalServerError).GroupBy(x => x.InterfaceLogID).Select(x => x.Key).ToList();
                return new
                {
                    status = true,
                    message = "success",
                    result = new
                    {
                        Success = SuccessLogID_List,
                        Error = ErrorLogID_List,
                    }
                };
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
        }


        private void Drop_Create_ViewInterfaceLog(DBHelper dbHelp, IDbTransaction dbTrans, string dbName_log)
        {
            string SQL = $@"SELECT name FROM {dbName_log}.sys.tables WHERE type = 'U'";
            List<Dictionary<string, object>> TABLE_List = GlobalDatabase.LoadDictByQuery(dbHelp, SQL, dbTrans);

            foreach (Dictionary<string, object> TABLE in TABLE_List)
            {
                string SQL_Drop = "";
                string SQL_Create = "";
                string TABLE_NAME = TABLE["name"].ToString();
                SQL_Drop += $@"
IF EXISTS(SELECT * FROM sys.views WHERE name = 'vw_B1_{TABLE_NAME}')
BEGIN
DROP VIEW vw_B1_{TABLE_NAME}
END";
                SQL_Create += $@"
CREATE VIEW vw_B1_{TABLE_NAME} as SELECT* FROM  {dbName_log}.dbo.{TABLE_NAME}";

                if (!string.IsNullOrEmpty(SQL_Drop)) dbHelp.ExecuteNonQuery(SQL_Drop, dbTrans);
                if (!string.IsNullOrEmpty(SQL_Create)) dbHelp.ExecuteNonQuery(SQL_Create, dbTrans);
            }
        }

        private List<string> Process_Documents(DBHelper dbHelp, IDbTransaction dbTrans, DBHelper dbHelp_log, IDbTransaction dbTrans_log, string UserID, DateTime NOW
            , List<ICON.Interface.InterfaceLog> _Log_List, List<ICON.Interface.InterfaceLogDetail> _LogDetail_List
            , List<Dictionary<string, object>> _Doc_DicList, List<Dictionary<string, object>> _DocLine_DicList
            , List<Dictionary<string, object>> _DocSplLine_DicList)
        {

            List<ICON.Interface.InterfaceLog> Log_List = _Log_List;
            List<ICON.Interface.InterfaceLogDetail> LogDetail_List = _LogDetail_List;
            List<Dictionary<string, object>> Doc_DicList = _Doc_DicList;
            List<Dictionary<string, object>> DocLine_DicList = _DocLine_DicList;
            List<Dictionary<string, object>> DocSplLine_DicList = _DocSplLine_DicList;

            List<ICON.Interface.Documents> Doc_List = new List<ICON.Interface.Documents>();
            List<ICON.Interface.Document_Lines> DocLine_List = new List<ICON.Interface.Document_Lines>();
            List<ICON.Interface.Document_SpecialLines> DocSplLine_List = new List<ICON.Interface.Document_SpecialLines>();
            List<ICON.Interface.Fields> UField_List = new List<ICON.Interface.Fields>();

            System.Text.StringBuilder SQL_ForREM = new System.Text.StringBuilder();
            System.Text.StringBuilder SQL_ForLog = new System.Text.StringBuilder();
            foreach (ICON.Interface.InterfaceLog Log in Log_List)
            {
                Log.CreateBy = UserID;
                Log.CreateDate = NOW;
                Log.UpdateBy = UserID;
                Log.UpdateDate = NOW;
                // (ForLog) ICON.Interface.InterfaceLog
                SQL_ForLog.Append(Log.REMDatabase_GetInsertCommandStr());

                ICON.Interface.SAP_Interface_Log REMLog = Log.ClassToClass<ICON.Interface.SAP_Interface_Log>(true);
                // (ForREM) ICON.Interface.SAP_Interface_Log
                SQL_ForREM.Append(REMLog.REMDatabase_GetInsertCommandStr(false));

                List<ICON.Interface.InterfaceLogDetail> Loop_LogDetail_List = LogDetail_List.FindAll(x => x.REMRefID == Log.REMRefID);
                foreach (ICON.Interface.InterfaceLogDetail LogDetail in Loop_LogDetail_List)
                {
                    LogDetail.InterfaceLogID = Log.InterfaceLogID;
                    LogDetail.CreateBy = UserID;
                    LogDetail.CreateDate = NOW;
                    LogDetail.ModifyBy = UserID;
                    LogDetail.ModifyDate = NOW;
                    // (ForLog) ICON.Interface.InterfaceLogDetail
                    SQL_ForLog.Append(LogDetail.REMDatabase_GetInsertCommandStr());

                    ICON.Interface.SAP_Interface_Log_Detail REMLogDetail = LogDetail.ClassToClass<ICON.Interface.SAP_Interface_Log_Detail>(true);
                    // (ForREM) ICON.Interface.SAP_Interface_Log_Detail
                    SQL_ForREM.Append(REMLogDetail.REMDatabase_GetInsertCommandStr(false));
                }

                List<Dictionary<string, object>> Loop_Pay_DicList = Doc_DicList.FindAll(x => x["REMRefID"].ToString() == Log.REMRefID);
                foreach (Dictionary<string, object> Item in Loop_Pay_DicList)
                {
                    ICON.Interface.InterfaceLogDetail LogDetail = Loop_LogDetail_List.Find(x => x.InterfaceLogDetailID == Item["InterfaceLogDetailID"].ToString());
                    List<Dictionary<string, object>> Loop_Doc_DicList = Doc_DicList.FindAll(x => x["REMRefID"].ToString() == LogDetail.REMRefID
                                                                                                && x["REMMethodName"].ToString() == LogDetail.REMMethodName
                                                                                                && x["REMEventName"].ToString() == LogDetail.REMEventName
                                                                                                && x["TEMPGroupID"].ToString() == Item["TEMPGroupID"].ToString());
                    List<Dictionary<string, object>> Loop_DocLine_DicList = DocLine_DicList.FindAll(x => x["REMRefID"].ToString() == LogDetail.REMRefID
                                                                                                        && x["REMMethodName"].ToString() == LogDetail.REMMethodName
                                                                                                        && x["REMEventName"].ToString() == LogDetail.REMEventName
                                                                                                        && x["TEMPGroupID"].ToString() == Item["TEMPGroupID"].ToString());
                    List<Dictionary<string, object>> Loop_DocSplLine_DicList = DocSplLine_DicList.FindAll(x => x["REMRefID"].ToString() == LogDetail.REMRefID
                                                                                                            && x["REMMethodName"].ToString() == LogDetail.REMMethodName
                                                                                                            && x["REMEventName"].ToString() == LogDetail.REMEventName
                                                                                                            && x["TEMPGroupID"].ToString() == Item["TEMPGroupID"].ToString());

                    ICON.Interface.Documents Loop_Doc = Item.DictToClass<ICON.Interface.Documents>();
                    List<ICON.Interface.Document_Lines> Loop_DocLine_List = GlobalDatabase.LoadListByDict<ICON.Interface.Document_Lines>(Loop_DocLine_DicList);
                    List<ICON.Interface.Document_SpecialLines> Loop_DocSplLine_List = GlobalDatabase.LoadListByDict<ICON.Interface.Document_SpecialLines>(Loop_DocSplLine_DicList);
                    List<ICON.Interface.Fields> Loop_UField_List = new List<Fields>();
                    Doc_List.Add(Loop_Doc);
                    DocLine_List.AddRange(Loop_DocLine_List);
                    DocSplLine_List.AddRange(Loop_DocSplLine_List);
                    UField_List.AddRange(Loop_UField_List);

                    List<string> DocKeys = new List<string>(Item.Keys).FindAll(x => x.IndexOf("U_") == 0);
                    List<string> DocLineKeys = (Loop_DocLine_DicList.Count > 0 ? new List<string>(Loop_DocLine_DicList[0].Keys) : new List<string>()).FindAll(x => x.IndexOf("U_") == 0);
                    foreach (string KeyName in DocKeys)
                    {
                        Item["L_InterfaceLogID"] = Log.InterfaceLogID;
                        Item["L_InterfaceLogDetailID"] = LogDetail.InterfaceLogDetailID;
                        string L_FromRefID = Item["L_DocumentsID"].ToString();
                        string L_FromTable = nameof(SAPbobsCOM.Documents);
                        ICON.Interface.Fields Loop_UField = GenerateUDF(Item, L_FromRefID, L_FromTable, KeyName, DocKeys.IndexOf(KeyName));
                        Loop_UField_List.Add(Loop_UField);
                    }

                    foreach (Dictionary<string, object> Loop_DocLine_Dic in Loop_DocLine_DicList)
                    {
                        foreach (string KeyName in DocLineKeys)
                        {
                            Loop_DocLine_Dic["L_InterfaceLogID"] = Log.InterfaceLogID;
                            Loop_DocLine_Dic["L_InterfaceLogDetailID"] = LogDetail.InterfaceLogDetailID;
                            string L_FromRefID = Loop_DocLine_Dic["L_Document_LinesID"].ToString();
                            string L_FromTable = nameof(SAPbobsCOM.Document_Lines);
                            ICON.Interface.Fields Loop_UField = GenerateUDF(Loop_DocLine_Dic, L_FromRefID, L_FromTable, KeyName, DocLineKeys.IndexOf(KeyName));
                            Loop_UField_List.Add(Loop_UField);
                        }
                    }

                    Loop_Doc.L_InterfaceLogID = Log.InterfaceLogID;
                    Loop_Doc.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                    // (ForLog) ICON.Interface.Documents
                    SQL_ForLog.Append(Loop_Doc.REMDatabase_GetInsertCommandStr());

                    foreach (ICON.Interface.Document_Lines Loop_DocLine in Loop_DocLine_List)
                    {
                        Loop_DocLine.L_InterfaceLogID = Log.InterfaceLogID;
                        Loop_DocLine.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                        Loop_DocLine.L_DocumentsID = Loop_Doc.L_DocumentsID;
                        // (ForLog) ICON.Interface.Document_Lines
                        SQL_ForLog.Append(Loop_DocLine.REMDatabase_GetInsertCommandStr());
                    }
                    foreach (ICON.Interface.Document_SpecialLines Loop_DocSplLine in Loop_DocSplLine_List)
                    {
                        Loop_DocSplLine.L_InterfaceLogID = Log.InterfaceLogID;
                        Loop_DocSplLine.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                        Loop_DocSplLine.L_DocumentsID = Loop_Doc.L_DocumentsID;
                        // (ForLog) ICON.Interface.Document_SpecialLines
                        SQL_ForLog.Append(Loop_DocSplLine.REMDatabase_GetInsertCommandStr());
                    }
                    foreach (ICON.Interface.Fields Loop_UField in Loop_UField_List)
                    {
                        Loop_UField.L_InterfaceLogID = Log.InterfaceLogID;
                        Loop_UField.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                        // (ForLog) ICON.Interface.Fields
                        SQL_ForLog.Append(Loop_UField.REMDatabase_GetInsertCommandStr());
                    }
                }
            }

            // Validate
            System.Text.StringBuilder SQL_ValidateLog = new System.Text.StringBuilder();
            if (Log_List.Count > 0) SQL_ValidateLog.Append(Log_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (LogDetail_List.Count > 0) SQL_ValidateLog.Append(LogDetail_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (Doc_List.Count > 0) SQL_ValidateLog.Append(Doc_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (DocLine_List.Count > 0) SQL_ValidateLog.Append(DocLine_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (DocSplLine_List.Count > 0) SQL_ValidateLog.Append(DocSplLine_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (UField_List.Count > 0) SQL_ValidateLog.Append(UField_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (!string.IsNullOrEmpty(SQL_ValidateLog.ToString())) throw new Exception(SQL_ValidateLog.ToString());

            if (!string.IsNullOrEmpty(SQL_ForREM.ToString())) dbHelp.ExecuteNonQuery(SQL_ForREM.ToString(), dbTrans);
            if (!string.IsNullOrEmpty(SQL_ForLog.ToString())) dbHelp_log.ExecuteNonQuery(SQL_ForLog.ToString(), dbTrans_log);

            List<string> LogID_List = Log_List.GroupBy(x => x.InterfaceLogID).Select(x => x.Key).ToList();
            return LogID_List;
        }
        
        private List<string> Process_Payments(DBHelper dbHelp, IDbTransaction dbTrans, DBHelper dbHelp_log, IDbTransaction dbTrans_log, string UserID, DateTime NOW
            , List<ICON.Interface.InterfaceLog> _Log_List, List<ICON.Interface.InterfaceLogDetail> _LogDetail_List
            , List<Dictionary<string, object>> _Pay_DicList, List<Dictionary<string, object>> _PayACC_DicList
            , List<Dictionary<string, object>> _PayINV_DicList, List<Dictionary<string, object>> _PayCQ_DicList
            , List<Dictionary<string, object>> _PayCR_DicList)
        {

            List<ICON.Interface.InterfaceLog> Log_List = _Log_List;
            List<ICON.Interface.InterfaceLogDetail> LogDetail_List = _LogDetail_List;
            List<Dictionary<string, object>> Pay_DicList = _Pay_DicList;
            List<Dictionary<string, object>> PayACC_DicList = _PayACC_DicList;
            List<Dictionary<string, object>> PayINV_DicList = _PayINV_DicList;
            List<Dictionary<string, object>> PayCQ_DicList = _PayCQ_DicList;
            List<Dictionary<string, object>> PayCR_DicList = _PayCR_DicList;

            List<ICON.Interface.Payments> Pay_List = new List<ICON.Interface.Payments>();
            List<ICON.Interface.Payments_Accounts> PayACC_List = new List<ICON.Interface.Payments_Accounts>();
            List<ICON.Interface.Payments_Invoices> PayINV_List = new List<ICON.Interface.Payments_Invoices>();
            List<ICON.Interface.Payments_Checks> PayCQ_List = new List<ICON.Interface.Payments_Checks>();
            List<ICON.Interface.Payments_CreditCards> PayCR_List = new List<ICON.Interface.Payments_CreditCards>();
            List<ICON.Interface.Fields> UField_List = new List<ICON.Interface.Fields>();

            System.Text.StringBuilder SQL_ForREM = new System.Text.StringBuilder();
            System.Text.StringBuilder SQL_ForLog = new System.Text.StringBuilder();
            foreach (ICON.Interface.InterfaceLog Log in Log_List)
            {
                Log.CreateBy = UserID;
                Log.CreateDate = NOW;
                Log.UpdateBy = UserID;
                Log.UpdateDate = NOW;
                // (ForLog) ICON.Interface.InterfaceLog
                SQL_ForLog.Append(Log.REMDatabase_GetInsertCommandStr());

                ICON.Interface.SAP_Interface_Log REMLog = Log.ClassToClass<ICON.Interface.SAP_Interface_Log>(true);
                // (ForREM) ICON.Interface.SAP_Interface_Log
                SQL_ForREM.Append(REMLog.REMDatabase_GetInsertCommandStr(false));

                List<ICON.Interface.InterfaceLogDetail> Loop_LogDetail_List = LogDetail_List.FindAll(x => x.REMRefID == Log.REMRefID);
                foreach (ICON.Interface.InterfaceLogDetail LogDetail in Loop_LogDetail_List)
                {
                    LogDetail.InterfaceLogID = Log.InterfaceLogID;
                    LogDetail.CreateBy = UserID;
                    LogDetail.CreateDate = NOW;
                    LogDetail.ModifyBy = UserID;
                    LogDetail.ModifyDate = NOW;
                    // (ForLog) ICON.Interface.InterfaceLogDetail
                    SQL_ForLog.Append(LogDetail.REMDatabase_GetInsertCommandStr());

                    ICON.Interface.SAP_Interface_Log_Detail REMLogDetail = LogDetail.ClassToClass<ICON.Interface.SAP_Interface_Log_Detail>(true);
                    // (ForREM) ICON.Interface.SAP_Interface_Log_Detail
                    SQL_ForREM.Append(REMLogDetail.REMDatabase_GetInsertCommandStr(false));
                }

                List<Dictionary<string, object>> Loop_Pay_DicList = Pay_DicList.FindAll(x => x["REMRefID"].ToString() == Log.REMRefID);
                foreach (Dictionary<string, object> Item in Loop_Pay_DicList)
                {
                    ICON.Interface.InterfaceLogDetail LogDetail = Loop_LogDetail_List.Find(x => x.InterfaceLogDetailID == Item["InterfaceLogDetailID"].ToString());
                    List<Dictionary<string, object>> Loop_PayACC_DicList = PayACC_DicList.FindAll(x => x["REMRefID"].ToString() == LogDetail.REMRefID
                                                                                                        && x["REMMethodName"].ToString() == LogDetail.REMMethodName
                                                                                                        && x["REMEventName"].ToString() == LogDetail.REMEventName
                                                                                                        && x["TEMPGroupID"].ToString() == Item["TEMPGroupID"].ToString());
                    List<Dictionary<string, object>> Loop_PayINV_DicList = PayINV_DicList.FindAll(x => x["REMRefID"].ToString() == LogDetail.REMRefID
                                                                                                        && x["REMMethodName"].ToString() == LogDetail.REMMethodName
                                                                                                        && x["REMEventName"].ToString() == LogDetail.REMEventName
                                                                                                        && x["TEMPGroupID"].ToString() == Item["TEMPGroupID"].ToString());
                    List<Dictionary<string, object>> Loop_PayCQ_DicList = PayCQ_DicList.FindAll(x => x["REMRefID"].ToString() == LogDetail.REMRefID
                                                                                                            && x["REMMethodName"].ToString() == LogDetail.REMMethodName
                                                                                                            && x["REMEventName"].ToString() == LogDetail.REMEventName
                                                                                                            && x["TEMPGroupID"].ToString() == Item["TEMPGroupID"].ToString());
                    List<Dictionary<string, object>> Loop_PayCR_DicList = PayCR_DicList.FindAll(x => x["REMRefID"].ToString() == LogDetail.REMRefID
                                                                                                            && x["REMMethodName"].ToString() == LogDetail.REMMethodName
                                                                                                            && x["REMEventName"].ToString() == LogDetail.REMEventName
                                                                                                            && x["TEMPGroupID"].ToString() == Item["TEMPGroupID"].ToString());

                    ICON.Interface.Payments Loop_Pay = Item.DictToClass<ICON.Interface.Payments>();
                    List<ICON.Interface.Payments_Accounts> Loop_PayACC_List = GlobalDatabase.LoadListByDict<ICON.Interface.Payments_Accounts>(Loop_PayACC_DicList);
                    List<ICON.Interface.Payments_Invoices> Loop_PayINV_List = GlobalDatabase.LoadListByDict<ICON.Interface.Payments_Invoices>(Loop_PayINV_DicList);
                    List<ICON.Interface.Payments_Checks> Loop_PayCQ_List = GlobalDatabase.LoadListByDict<ICON.Interface.Payments_Checks>(Loop_PayCQ_DicList);
                    List<ICON.Interface.Payments_CreditCards> Loop_PayCR_List = GlobalDatabase.LoadListByDict<ICON.Interface.Payments_CreditCards>(Loop_PayCR_DicList);
                    List<ICON.Interface.Fields> Loop_UField_List = new List<Fields>();
                    Pay_List.Add(Loop_Pay);
                    PayACC_List.AddRange(Loop_PayACC_List);
                    PayINV_List.AddRange(Loop_PayINV_List);
                    PayCQ_List.AddRange(Loop_PayCQ_List);
                    PayCR_List.AddRange(Loop_PayCR_List);
                    UField_List.AddRange(Loop_UField_List);

                    List<string> PayKeys = new List<string>(Item.Keys).FindAll(x => x.IndexOf("U_") == 0);
                    List<string> PayACCKeys = (PayACC_DicList.Count > 0 ? new List<string>(PayACC_DicList[0].Keys) : new List<string>()).FindAll(x => x.IndexOf("U_") == 0);
                    List<string> PayINVKeys = (PayINV_DicList.Count > 0 ? new List<string>(PayINV_DicList[0].Keys) : new List<string>()).FindAll(x => x.IndexOf("U_") == 0);
                    List<string> PayCQKeys = (PayCQ_DicList.Count > 0 ? new List<string>(PayCQ_DicList[0].Keys) : new List<string>()).FindAll(x => x.IndexOf("U_") == 0);
                    List<string> PayCRKeys = (PayCR_DicList.Count > 0 ? new List<string>(PayCR_DicList[0].Keys) : new List<string>()).FindAll(x => x.IndexOf("U_") == 0);

                    foreach (string KeyName in PayKeys)
                    {
                        Item["L_InterfaceLogID"] = Log.InterfaceLogID;
                        Item["L_InterfaceLogDetailID"] = LogDetail.InterfaceLogDetailID;
                        string L_FromRefID = Item["L_PaymentsID"].ToString();
                        string L_FromTable = nameof(SAPbobsCOM.Payments);
                        ICON.Interface.Fields Loop_UField = GenerateUDF(Item, L_FromRefID, L_FromTable, KeyName, PayKeys.IndexOf(KeyName));
                        Loop_UField_List.Add(Loop_UField);
                    }

                    foreach (Dictionary<string, object> Loop_PayACC_Dic in Loop_PayACC_DicList)
                    {
                        foreach (string KeyName in PayACCKeys)
                        {
                            Loop_PayACC_Dic["L_InterfaceLogID"] = Log.InterfaceLogID;
                            Loop_PayACC_Dic["L_InterfaceLogDetailID"] = LogDetail.InterfaceLogDetailID;
                            string L_FromRefID = Loop_PayACC_Dic["L_Payments_AccountsID"].ToString();
                            string L_FromTable = nameof(SAPbobsCOM.Payments_Accounts);
                            ICON.Interface.Fields Loop_UField = GenerateUDF(Loop_PayACC_Dic, L_FromRefID, L_FromTable, KeyName, PayACCKeys.IndexOf(KeyName));
                            Loop_UField_List.Add(Loop_UField);
                        }
                    }

                    foreach (Dictionary<string, object> Loop_PayINV_Dic in Loop_PayINV_DicList)
                    {
                        foreach (string KeyName in PayINVKeys)
                        {
                            Loop_PayINV_Dic["L_InterfaceLogID"] = Log.InterfaceLogID;
                            Loop_PayINV_Dic["L_InterfaceLogDetailID"] = LogDetail.InterfaceLogDetailID;
                            string L_FromRefID = Loop_PayINV_Dic["L_Payments_InvoicesID"].ToString();
                            string L_FromTable = nameof(SAPbobsCOM.Payments_Invoices);
                            ICON.Interface.Fields Loop_UField = GenerateUDF(Loop_PayINV_Dic, L_FromRefID, L_FromTable, KeyName, PayINVKeys.IndexOf(KeyName));
                            Loop_UField_List.Add(Loop_UField);
                        }
                    }

                    foreach (Dictionary<string, object> Loop_PayCQ_Dic in Loop_PayCQ_DicList)
                    {
                        foreach (string KeyName in PayCQKeys)
                        {
                            Loop_PayCQ_Dic["L_InterfaceLogID"] = Log.InterfaceLogID;
                            Loop_PayCQ_Dic["L_InterfaceLogDetailID"] = LogDetail.InterfaceLogDetailID;
                            string L_FromRefID = Loop_PayCQ_Dic["L_Payments_ChecksID"].ToString();
                            string L_FromTable = nameof(SAPbobsCOM.Payments_Checks);
                            ICON.Interface.Fields Loop_UField = GenerateUDF(Loop_PayCQ_Dic, L_FromRefID, L_FromTable, KeyName, PayCQKeys.IndexOf(KeyName));
                            Loop_UField_List.Add(Loop_UField);
                        }
                    }

                    foreach (Dictionary<string, object> Loop_PayCR_Dic in Loop_PayCR_DicList)
                    {
                        foreach (string KeyName in PayCRKeys)
                        {
                            Loop_PayCR_Dic["L_InterfaceLogID"] = Log.InterfaceLogID;
                            Loop_PayCR_Dic["L_InterfaceLogDetailID"] = LogDetail.InterfaceLogDetailID;
                            string L_FromRefID = Loop_PayCR_Dic["L_Payments_CreditCardsID"].ToString();
                            string L_FromTable = nameof(SAPbobsCOM.Payments_CreditCards);
                            ICON.Interface.Fields Loop_UField = GenerateUDF(Loop_PayCR_Dic, L_FromRefID, L_FromTable, KeyName, PayCRKeys.IndexOf(KeyName));
                            Loop_UField_List.Add(Loop_UField);
                        }
                    }

                    Loop_Pay.L_InterfaceLogID = Log.InterfaceLogID;
                    Loop_Pay.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                    // (ForLog) ICON.Interface.Payments
                    SQL_ForLog.Append(Loop_Pay.REMDatabase_GetInsertCommandStr());

                    foreach (ICON.Interface.Payments_Accounts Loop_PayACC in Loop_PayACC_List)
                    {
                        Loop_PayACC.L_InterfaceLogID = Log.InterfaceLogID;
                        Loop_PayACC.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                        Loop_PayACC.L_PaymentsID = Loop_Pay.L_PaymentsID;
                        // (ForLog) ICON.Interface.Payments_Accounts
                        SQL_ForLog.Append(Loop_PayACC.REMDatabase_GetInsertCommandStr());
                    }
                    foreach (ICON.Interface.Payments_Invoices Loop_PayINV in Loop_PayINV_List)
                    {
                        Loop_PayINV.L_InterfaceLogID = Log.InterfaceLogID;
                        Loop_PayINV.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                        Loop_PayINV.L_PaymentsID = Loop_Pay.L_PaymentsID;
                        // (ForLog) ICON.Interface.Payments_Invoices
                        SQL_ForLog.Append(Loop_PayINV.REMDatabase_GetInsertCommandStr());
                    }
                    foreach (ICON.Interface.Payments_Checks Loop_PayCQ in Loop_PayCQ_List)
                    {
                        Loop_PayCQ.L_InterfaceLogID = Log.InterfaceLogID;
                        Loop_PayCQ.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                        Loop_PayCQ.L_PaymentsID = Loop_Pay.L_PaymentsID;
                        // (ForLog) ICON.Interface.Payments_Checks
                        SQL_ForLog.Append(Loop_PayCQ.REMDatabase_GetInsertCommandStr());
                    }
                    foreach (ICON.Interface.Payments_CreditCards Loop_PayCR in Loop_PayCR_List)
                    {
                        Loop_PayCR.L_InterfaceLogID = Log.InterfaceLogID;
                        Loop_PayCR.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                        Loop_PayCR.L_PaymentsID = Loop_Pay.L_PaymentsID;
                        // (ForLog) ICON.Interface.Payments_CreditCards
                        SQL_ForLog.Append(Loop_PayCR.REMDatabase_GetInsertCommandStr());
                    }

                    foreach (ICON.Interface.Fields Loop_UField in Loop_UField_List)
                    {
                        Loop_UField.L_InterfaceLogID = Log.InterfaceLogID;
                        Loop_UField.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                        // (ForLog) ICON.Interface.Fields
                        SQL_ForLog.Append(Loop_UField.REMDatabase_GetInsertCommandStr());
                    }
                }
            }

            // Validate
            System.Text.StringBuilder SQL_ValidateLog = new System.Text.StringBuilder();
            if (Log_List.Count > 0) SQL_ValidateLog.Append(Log_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (LogDetail_List.Count > 0) SQL_ValidateLog.Append(LogDetail_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (Pay_List.Count > 0) SQL_ValidateLog.Append(Pay_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (PayACC_List.Count > 0) SQL_ValidateLog.Append(PayACC_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (PayINV_List.Count > 0) SQL_ValidateLog.Append(PayINV_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (PayCQ_List.Count > 0) SQL_ValidateLog.Append(PayCQ_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (PayCR_List.Count > 0) SQL_ValidateLog.Append(PayCR_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (UField_List.Count > 0) SQL_ValidateLog.Append(UField_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (!string.IsNullOrEmpty(SQL_ValidateLog.ToString())) throw new Exception(SQL_ValidateLog.ToString());

            if (!string.IsNullOrEmpty(SQL_ForREM.ToString())) dbHelp.ExecuteNonQuery(SQL_ForREM.ToString(), dbTrans);
            if (!string.IsNullOrEmpty(SQL_ForLog.ToString())) dbHelp_log.ExecuteNonQuery(SQL_ForLog.ToString(), dbTrans_log);

            List<string> LogID_List = Log_List.GroupBy(x => x.InterfaceLogID).Select(x => x.Key).ToList();
            return LogID_List;
        }

        private List<string> Process_JournalEntries(DBHelper dbHelp, IDbTransaction dbTrans, DBHelper dbHelp_log, IDbTransaction dbTrans_log, string UserID, DateTime NOW
           , List<ICON.Interface.InterfaceLog> _Log_List, List<ICON.Interface.InterfaceLogDetail> _LogDetail_List
           , List<Dictionary<string, object>> _JE_DicList, List<Dictionary<string, object>> _JELine_DicList)
        {

            List<ICON.Interface.InterfaceLog> Log_List = _Log_List;
            List<ICON.Interface.InterfaceLogDetail> LogDetail_List = _LogDetail_List;
            List<Dictionary<string, object>> JE_DicList = _JE_DicList;
            List<Dictionary<string, object>> JELine_DicList = _JELine_DicList;

            List<ICON.Interface.JournalEntries> JE_List = new List<ICON.Interface.JournalEntries>();
            List<ICON.Interface.JournalEntries_Lines> JELine_List = new List<ICON.Interface.JournalEntries_Lines>();
            List<ICON.Interface.Fields> UField_List = new List<ICON.Interface.Fields>();

            System.Text.StringBuilder SQL_ForREM = new System.Text.StringBuilder();
            System.Text.StringBuilder SQL_ForLog = new System.Text.StringBuilder();
            foreach (ICON.Interface.InterfaceLog Log in Log_List)
            {
                Log.CreateBy = UserID;
                Log.CreateDate = NOW;
                Log.UpdateBy = UserID;
                Log.UpdateDate = NOW;
                // (ForLog) ICON.Interface.InterfaceLog
                SQL_ForLog.Append(Log.REMDatabase_GetInsertCommandStr());

                ICON.Interface.SAP_Interface_Log REMLog = Log.ClassToClass<ICON.Interface.SAP_Interface_Log>(true);
                // (ForREM) ICON.Interface.SAP_Interface_Log
                SQL_ForREM.Append(REMLog.REMDatabase_GetInsertCommandStr(false));

                List<ICON.Interface.InterfaceLogDetail> Loop_LogDetail_List = LogDetail_List.FindAll(x => x.REMRefID == Log.REMRefID);
                foreach (ICON.Interface.InterfaceLogDetail LogDetail in Loop_LogDetail_List)
                {
                    LogDetail.InterfaceLogID = Log.InterfaceLogID;
                    LogDetail.CreateBy = UserID;
                    LogDetail.CreateDate = NOW;
                    LogDetail.ModifyBy = UserID;
                    LogDetail.ModifyDate = NOW;
                    // (ForLog) ICON.Interface.InterfaceLogDetail
                    SQL_ForLog.Append(LogDetail.REMDatabase_GetInsertCommandStr());

                    ICON.Interface.SAP_Interface_Log_Detail REMLogDetail = LogDetail.ClassToClass<ICON.Interface.SAP_Interface_Log_Detail>(true);
                    // (ForREM) ICON.Interface.SAP_Interface_Log_Detail
                    SQL_ForREM.Append(REMLogDetail.REMDatabase_GetInsertCommandStr(false));
                }

                List<Dictionary<string, object>> Loop_Pay_DicList = JE_DicList.FindAll(x => x["REMRefID"].ToString() == Log.REMRefID);
                foreach (Dictionary<string, object> Item in Loop_Pay_DicList)
                {
                    ICON.Interface.InterfaceLogDetail LogDetail = Loop_LogDetail_List.Find(x => x.InterfaceLogDetailID == Item["InterfaceLogDetailID"].ToString());
                    List<Dictionary<string, object>> Loop_JE_DicList = JE_DicList.FindAll(x => x["REMRefID"].ToString() == LogDetail.REMRefID
                                                                                                && x["REMMethodName"].ToString() == LogDetail.REMMethodName
                                                                                                && x["REMEventName"].ToString() == LogDetail.REMEventName
                                                                                                && x["TEMPGroupID"].ToString() == Item["TEMPGroupID"].ToString());
                    List<Dictionary<string, object>> Loop_JELine_DicList = JELine_DicList.FindAll(x => x["REMRefID"].ToString() == LogDetail.REMRefID
                                                                                                        && x["REMMethodName"].ToString() == LogDetail.REMMethodName
                                                                                                        && x["REMEventName"].ToString() == LogDetail.REMEventName
                                                                                                        && x["TEMPGroupID"].ToString() == Item["TEMPGroupID"].ToString());

                    ICON.Interface.JournalEntries Loop_JE = Item.DictToClass<ICON.Interface.JournalEntries>();
                    List<ICON.Interface.JournalEntries_Lines> Loop_JELine_List = GlobalDatabase.LoadListByDict<ICON.Interface.JournalEntries_Lines>(Loop_JELine_DicList);
                    List<ICON.Interface.Fields> Loop_UField_List = new List<Fields>();
                    JE_List.Add(Loop_JE);
                    JELine_List.AddRange(Loop_JELine_List);
                    UField_List.AddRange(Loop_UField_List);

                    List<string> JEKeys = new List<string>(Item.Keys).FindAll(x => x.IndexOf("U_") == 0);
                    List<string> JELineKeys = (Loop_JELine_DicList.Count > 0 ? new List<string>(Loop_JELine_DicList[0].Keys) : new List<string>()).FindAll(x => x.IndexOf("U_") == 0);
                    foreach (string KeyName in JEKeys)
                    {
                        Item["L_InterfaceLogID"] = Log.InterfaceLogID;
                        Item["L_InterfaceLogDetailID"] = LogDetail.InterfaceLogDetailID;
                        string L_FromRefID = Item["L_JournalEntriesID"].ToString();
                        string L_FromTable = nameof(SAPbobsCOM.JournalEntries);
                        ICON.Interface.Fields Loop_UField = GenerateUDF(Item, L_FromRefID, L_FromTable, KeyName, JEKeys.IndexOf(KeyName));
                        Loop_UField_List.Add(Loop_UField);
                    }

                    foreach (Dictionary<string, object> Loop_JELine_Dic in Loop_JELine_DicList)
                    {
                        foreach (string KeyName in JELineKeys)
                        {
                            Loop_JELine_Dic["L_InterfaceLogID"] = Log.InterfaceLogID;
                            Loop_JELine_Dic["L_InterfaceLogDetailID"] = LogDetail.InterfaceLogDetailID;
                            string L_FromRefID = Loop_JELine_Dic["L_JournalEntries_LinesID"].ToString();
                            string L_FromTable = nameof(SAPbobsCOM.JournalEntries_Lines);
                            ICON.Interface.Fields Loop_UField = GenerateUDF(Loop_JELine_Dic, L_FromRefID, L_FromTable, KeyName, JELineKeys.IndexOf(KeyName));
                            Loop_UField_List.Add(Loop_UField);
                        }
                    }

                    Loop_JE.L_InterfaceLogID = Log.InterfaceLogID;
                    Loop_JE.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                    // (ForLog) ICON.Interface.JournalEntries
                    SQL_ForLog.Append(Loop_JE.REMDatabase_GetInsertCommandStr());

                    foreach (ICON.Interface.JournalEntries_Lines Loop_JELine in Loop_JELine_List)
                    {
                        Loop_JELine.L_InterfaceLogID = Log.InterfaceLogID;
                        Loop_JELine.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                        Loop_JELine.L_JournalEntriesID = Loop_JE.L_JournalEntriesID;
                        // (ForLog) ICON.Interface.JournalEntries_Lines
                        SQL_ForLog.Append(Loop_JELine.REMDatabase_GetInsertCommandStr());
                    }
                    foreach (ICON.Interface.Fields Loop_UField in Loop_UField_List)
                    {
                        Loop_UField.L_InterfaceLogID = Log.InterfaceLogID;
                        Loop_UField.L_InterfaceLogDetailID = LogDetail.InterfaceLogDetailID;
                        // (ForLog) ICON.Interface.Fields
                        SQL_ForLog.Append(Loop_UField.REMDatabase_GetInsertCommandStr());
                    }
                }
            }

            // Validate
            System.Text.StringBuilder SQL_ValidateLog = new System.Text.StringBuilder();
            if (Log_List.Count > 0) SQL_ValidateLog.Append(Log_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (LogDetail_List.Count > 0) SQL_ValidateLog.Append(LogDetail_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (JE_List.Count > 0) SQL_ValidateLog.Append(JE_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (JELine_List.Count > 0) SQL_ValidateLog.Append(JELine_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (UField_List.Count > 0) SQL_ValidateLog.Append(UField_List.REMDatabase_GetOverLimitErrorMsg(dbHelp_log, null));
            if (!string.IsNullOrEmpty(SQL_ValidateLog.ToString())) throw new Exception(SQL_ValidateLog.ToString());

            if (!string.IsNullOrEmpty(SQL_ForREM.ToString())) dbHelp.ExecuteNonQuery(SQL_ForREM.ToString(), dbTrans);
            if (!string.IsNullOrEmpty(SQL_ForLog.ToString())) dbHelp_log.ExecuteNonQuery(SQL_ForLog.ToString(), dbTrans_log);

            List<string> LogID_List = Log_List.GroupBy(x => x.InterfaceLogID).Select(x => x.Key).ToList();
            return LogID_List;
        }

        private ICON.Interface.Fields GenerateUDF(Dictionary<string, object> Item, string L_FromRefID, string L_FromTable, string KeyName, int KeyNameIndex)
        {
            string L_InterfaceLogID = Item["L_InterfaceLogID"].ToString();
            string L_InterfaceLogDetailID = Item["L_InterfaceLogDetailID"].ToString();
            System.Object Value_Object = Item[KeyName];
            Type ObjectType = Value_Object.GetType();
            ObjectType = GlobalDatabaseFunction.IsNullableType(ObjectType) ? Nullable.GetUnderlyingType(ObjectType) : ObjectType;

            ICON.Interface.Fields Loop_UField = new ICON.Interface.Fields();
            Loop_UField.L_FieldsID = "UDF_" + L_FromRefID + "_" + (KeyNameIndex + 1).ToString().PadLeft(3, '0');
            Loop_UField.L_InterfaceLogID = L_InterfaceLogID;
            Loop_UField.L_InterfaceLogDetailID = L_InterfaceLogDetailID;
            Loop_UField.L_FromRefID = L_FromRefID;
            Loop_UField.L_FromTable = L_FromTable;
            Loop_UField.L_Type = ObjectType.Name;
            Loop_UField.L_Name = KeyName;
            Loop_UField.Value = GetValueString(Value_Object);

            return Loop_UField;
        }
        private string GetValueString(System.Object Value_Object, System.String Format_String = "")
        {
            Type ObjectType = Value_Object.GetType();
            ObjectType = GlobalDatabaseFunction.IsNullableType(ObjectType) ? Nullable.GetUnderlyingType(ObjectType) : ObjectType;
            if (Value_Object != null && Value_Object != DBNull.Value)
            {
                if (typeof(System.String) == ObjectType) { return !string.IsNullOrEmpty(Convert.ToString(Value_Object)) ? Value_Object.ToString() : ""; }
                else if (typeof(System.DateTime) == ObjectType) { return Convert.ToDateTime(Value_Object).ToString(!string.IsNullOrEmpty(Format_String) ? Format_String : "yyyy-MM-dd HH:mm:ss.fff"); }
                else if (typeof(System.Boolean) == ObjectType) { return Convert.ToBoolean(Value_Object) ? "TRUE" : "FALSE"; }
                else if (typeof(System.Int16) == ObjectType) { return Convert.ToInt16(Value_Object).ToString(); }
                else if (typeof(System.Int32) == ObjectType) { return Convert.ToInt32(Value_Object).ToString(); }
                else if (typeof(System.Int64) == ObjectType) { return Convert.ToInt64(Value_Object).ToString(); }
                else if (typeof(System.Double) == ObjectType) { return Convert.ToDouble(Value_Object).ToString(!string.IsNullOrEmpty(Format_String) ? Format_String : "N2"); }
                else if (typeof(System.Single) == ObjectType) { return Convert.ToSingle(Value_Object).ToString(!string.IsNullOrEmpty(Format_String) ? Format_String : "N2"); }
                else if (typeof(System.Decimal) == ObjectType) { return Convert.ToDecimal(Value_Object).ToString(!string.IsNullOrEmpty(Format_String) ? Format_String : "N2"); }
                else return "";
            }
            else return "";
        }
    }
}