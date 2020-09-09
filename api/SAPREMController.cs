using ICON.Framework.Provider;
using ICON.Interface;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace ICON.SAP.API.api
{
    public class SAPREMController : ApiController
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

        #region #### REM API ####
        /// <summary>
        /// REM Create Goods Receipt PO
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/rem/creategoodsreceiptpo")]
        [AllowAnonymous]
        public object REMCreateGoodsReceiptPO(dynamic Data)
        {
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            dynamic header = Data.header;
            dynamic details = Data.detail;

            string TableName = "SAP_RW_Header";

            try
            {

                if (header != null)
                {
                    try
                    {

                        try
                        {
                            string DocNum = CreateREMGoodReceiptPO(new
                            {
                                HeaderID = (string)header.HeaderId,
                                DocDate = (DateTime)header.DocDate,
                                DocDueDate = (DateTime)header.DocDueDate,
                                TaxDate = (DateTime)header.TaxDate,
                                CardCode = (string)header.CardCode,
                                CardName = (string)header.CardName,
                                Address = (string)header.Address,
                                NumAtCard = (string)header.NumAtCard,
                                VatPercent = (string)header.VatPercent,
                                VatSum = (string)header.VatSum,
                                DiscPrcnt = (string)header.DiscPrcnt,
                                DocCur = (string)header.DocCur,
                                DocRate = (string)header.DocRate,
                                DocTotal = (string)header.DocTotal,
                                Comments = (string)header.Comments,
                                JrnlMemo = (string)header.JrnlMemo,
                                GroupNum = (string)header.GroupNum,
                                SlpCode = (string)header.SlpCode,
                                Address2 = (string)header.Address2,
                                LicTradNum = (string)header.LecTradNum,
                                DeferrTax = (string)header.DeferrTax,
                                OwnerCode = (string)header.OwnerCode,
                                ICON_RefNo = (string)header.ICON_RefNo,
                                VatBrance = (string)header.VatBranch,
                                TAX_PECL = (string)header.TAX_PECL,
                                Module = "REMCreateGoodsReceiptPO",
                                MethodName = "REMCreateGoodsReceiptPO",
                                RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID",
                                TableName = TableName,
                                IsService = false,
                                Details = details
                            });
                            ResultSuccess.Add(DocNum);

                            return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                        }
                        catch (Exception ex)
                        {
                            ResultError.Add((string)header.HeaderId + " : " + ex.Message);

                            return new { status = true, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                        }



                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new { status = false, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
            //finally { conn.Dispose(); }
        }

        /// <summary>
        /// REM Create Journal Voucher
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/rem/createjournalvoucher")]
        [AllowAnonymous]
        public object REMCreateJournalVoucher(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<object> ResultSuccess = new List<object>();
            List<string> ResultError = new List<string>();

            string TableName = "SAPB1JV1";
            string Remark = "";
            try
            {

                DataTable dtJV0 = dbHelp.ExecuteDataTable(GetSqlSAPB1JV());

                if (dtJV0.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow drJV0 in dtJV0.Rows)
                        {
                            //if(drJV0["GLVoucher"].ToString() == "R01-19070001")
                            //{

                            DataTable dtJV1 = dbHelp.ExecuteDataTable(GetSqlSAPB1JV(drJV0["GLVoucher"].ToString(), TableName));

                            if (!string.IsNullOrEmpty(drJV0["Remarks"].ToString()) && string.IsNullOrEmpty((string)dtJV1.Rows[0]["Remarks"]))
                            {
                                Remark = drJV0["Remarks"].ToString();
                            }
                            else if (string.IsNullOrEmpty(drJV0["Remarks"].ToString()) && !string.IsNullOrEmpty((string)dtJV1.Rows[0]["Remarks"]))
                            {
                                Remark = (string)dtJV1.Rows[0]["Remarks"];
                            }
                            else if (!string.IsNullOrEmpty(drJV0["Remarks"].ToString()) && !string.IsNullOrEmpty((string)dtJV1.Rows[0]["Remarks"]))
                            {
                                Remark = drJV0["Remarks"].ToString() + " " + (string)dtJV1.Rows[0]["Remarks"];
                            }

                            try
                            {
                                string DocNum = CreateREMJournalVoucher(new
                                {
                                    GLVoucherID = drJV0["GLVoucher"].ToString(),
                                    PostingDate = (DateTime)drJV0["DateID"],
                                    DocDueDate = (DateTime)dtJV1.Rows[0]["DueDate"],
                                    TaxDate = (DateTime)dtJV1.Rows[0]["TaxDate"],
                                    Remark = Remark,
                                    ICON_RefNo = (string)dtJV1.Rows[0]["ICON_REF"],
                                    //IsAutoReverse = (string)header.IsAutoReverse,
                                    //StornoDate = (DateTime)header.StornoDate,
                                    Module = "REMCreateJournalVoucher",
                                    MethodName = "REMCreateJournalVoucher",
                                    RefDescription = "Store => SAPB1JV0,SAPB1JV1,SAPB1JV2",
                                    TableName = TableName,
                                    Project = drJV0["Project"].ToString(),
                                    Details = "",
                                    Ref1 = dtJV1.Rows[0]["Ref1"].ToString(),
                                });

                                ResultSuccess.Add(new { RefID = drJV0["GLVoucher"].ToString(), DocNum = DocNum });
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(drJV0["GLVoucher"].ToString() + " : " + ex.Message);
                                return new { status = true, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                            }
                            //}
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
                    return new { status = false, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
            //finally { conn.Dispose(); }
        }


        /// <summary>
        /// REM Create Journal Entry
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/rem/createjournalentry")]
        [AllowAnonymous]
        public object REMCreateJournalEntry(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            List<object> ResultSuccess = new List<object>();
            List<string> ResultError = new List<string>();

            string TableName = "SAPB1JV1";
            string Remark = "";
            try
            {

                DataTable dtJV0 = dbHelp.ExecuteDataTable(GetSqlSAPB1JV());

                if (dtJV0.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow drJV0 in dtJV0.Rows)
                        {
                            //if(drJV0["GLVoucher"].ToString() == "R01-19070001")
                            //{

                            DataTable dtJV1 = dbHelp.ExecuteDataTable(GetSqlSAPB1JV(drJV0["GLVoucher"].ToString(), TableName));

                            if (!string.IsNullOrEmpty(drJV0["Remarks"].ToString()) && string.IsNullOrEmpty((string)dtJV1.Rows[0]["Remarks"]))
                            {
                                Remark = drJV0["Remarks"].ToString();
                            }
                            else if (string.IsNullOrEmpty(drJV0["Remarks"].ToString()) && !string.IsNullOrEmpty((string)dtJV1.Rows[0]["Remarks"]))
                            {
                                Remark = (string)dtJV1.Rows[0]["Remarks"];
                            }
                            else if (!string.IsNullOrEmpty(drJV0["Remarks"].ToString()) && !string.IsNullOrEmpty((string)dtJV1.Rows[0]["Remarks"]))
                            {
                                Remark = drJV0["Remarks"].ToString() + " " + (string)dtJV1.Rows[0]["Remarks"];
                            }

                            try
                            {
                                string DocNum = CreateREMJournalEntry(new
                                {
                                    GLVoucherID = drJV0["GLVoucher"].ToString(),
                                    PostingDate = (DateTime)drJV0["DateID"],
                                    DocDueDate = (DateTime)dtJV1.Rows[0]["DueDate"],
                                    TaxDate = (DateTime)dtJV1.Rows[0]["TaxDate"],
                                    Remark = Remark,
                                    ICON_RefNo = (string)dtJV1.Rows[0]["ICON_REF"],
                                    //IsAutoReverse = (string)header.IsAutoReverse,
                                    //StornoDate = (DateTime)header.StornoDate,
                                    Module = "REMCreateJournalEntry",
                                    MethodName = "REMCreateJournalEntry",
                                    RefDescription = "Store => SAPB1JV0,SAPB1JV1,SAPB1JV2",
                                    TableName = TableName,
                                    Project = drJV0["Project"].ToString(),
                                    Details = "",
                                    Ref1 = dtJV1.Rows[0]["Ref1"].ToString(),
                                    GLType = drJV0["GLType"].ToString(),
                                });

                                ResultSuccess.Add(new { RefID = drJV0["GLVoucher"].ToString(), DocNum = DocNum });
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(drJV0["GLVoucher"].ToString() + " : " + ex.Message);
                                return new { status = true, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                            }
                            //}
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
                    return new { status = false, message = "success", result = "No data interface." };
                }
            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", result = ex.Message };
            }
            //finally { conn.Dispose(); }
        }
        #endregion

        #region ##### Interface REM to SAP ####
        private string GetSqlSAPB1JV(string GLVoucher = "", string StoreName = "SAPB1JV0")
        {
            string sql = $@"
declare @GLVoucher nvarchar(50) = '{GLVoucher}'

exec {StoreName}";

            if (!string.IsNullOrEmpty(GLVoucher))
            {
                sql += $" @GLVoucher";
            }

            return sql;
        }
        private string GetSqlREMHeader(string RefID, string TableName = "SAP_RW_Header", string RefName = "HeaderID")
        {
            string sql = $@"
SELECT * FROM {TableName}";

            if (!string.IsNullOrEmpty(RefID))
            {
                sql += $" where {RefName} = '{RefID}'";
            }

            return sql;
        }
        private string GetSqlREMDetail(string RefID, string TableName = "SAP_RW_Detail", string RefName = "HeaderID", string REDB = "STD_PM_Production")
        {
            string sql = $@"
select 
	pmd.* 
    ,re.[Value] DBName
  from {TableName} pmd
  inner join {REDB}.dbo.Sys_Master_Projects pj on pmd.Project = pj.ProjectID
  inner join {REDB}.dbo.SYs_Conf_RealEstate re on re.CompanyID = pj.CompanyID";

            if (!string.IsNullOrEmpty(RefID))
            {
                sql += $" where {RefName} = '{RefID}'";
            }

            //sql += " GROUP BY OP.OPaymentID, ISNULL(SAP.APIResponseCode,'500'), SAP.SAPRefNo";

            return sql;
        }
        private string GetSqlInterfaceLog(string REMRefID, string MethodName = "")
        {
            string sql = $@"
select * from sap_interface_log where remrefid = '{REMRefID}'";

            if (!string.IsNullOrEmpty(MethodName))
            {
                sql += $" and methodname like '%{MethodName}%'";
            }

            return sql;
        }
        private string GetSqlREMDBConnect(string ProjectID,string GroupName)
        {
            string SQL = $@"
select 
    re.[Value] DBName
from sys_conf_realestate re
inner join sys_master_projects pj on re.CompanyID = pj.CompanyID
where keyname = 'dbname'
and pj.SAPWBSCode = '{ProjectID}'
and re.GroupName = '{GroupName}'";

            return SQL;
        }

        #region #### REM Goods Receipt PO ####
        /****create****/
        private string CreateREMGoodReceiptPO(dynamic Data)
        {
            string HeaderID = (string)Data.HeaderID;
            string MethodName = "REMCreateGoodsReceiptPO";
            string RefDescription = "SAP_RW_Header";
            string VatCode = "NOG";
            bool IsService = false;
            bool IsDeferVat = true;
            
            if (!string.IsNullOrEmpty((string)Data.MethodName))
            {
                MethodName = (string)Data.MethodName;
            }
            if (!string.IsNullOrEmpty((string)Data.RefDescription))
            {
                RefDescription = (string)Data.RefDescription;
            }
            //if (!string.IsNullOrEmpty((string)Data.VatCode))
            //{
            //    VatCode = (string)Data.VatCode;
            //}
            if (Data.IsService != null)
            {
                IsService = (bool)Data.IsService;
            }

            string SiteType = "";
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SiteType"]))
            {
                SiteType = System.Configuration.ConfigurationManager.AppSettings["SiteType"].ToString();
            }
            else
            {
                throw new Exception("SiteType Invalid!!!!");
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodReceiptPO_REM",
                    MethodName,
                    (string)Data.ICON_RefNo,
                    RefDescription,
                    DocEntry,
                    "OPDN.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                var details = (dynamic)Data.Details;
                string SQL = GetSqlREMDBConnect((string)details[0].Project,SiteType);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());

                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    //CardCode = (string)Data.vendercode,
                    //NumAtCard = (string)Data.taxno,    // Vendor Ref. No.
                    //PostingDate = (DateTime)Data.po_recdate,
                    //DocDueDate = (DateTime)Data.po_recdate,
                    //DocumentDate = (DateTime)Data.po_recdate,
                    DocType = SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes,
                    UDF_RefNo = (string)Data.ICON_RefNo,
                    UDF_TAX_PECL = (string)Data.TAX_PECL,
                    //UDF_CustName = dt.Rows[0]["CustomerName"].ToString(),
                    //UDF_UnitRef = dt.Rows[0]["UnitNumber"].ToString(),
                    //UDF_Project = dt.Rows[0]["ProjectID"].ToString(),
                    //UDF_ProjectName = dt.Rows[0]["ProjectName"].ToString(),
                    //UDF_TaxID = dt.Rows[0]["TaxID"].ToString(),
                    //UDF_Address = dt.Rows[0]["Address"].ToString(),
                    //IsDeferVAT = Convert.ToBoolean(dt.Rows[0]["IsDeferVAT"])


                    HeaderID = HeaderID,
                    PostingDate = (DateTime)Data.DocDate,
                    DocDueDate = (DateTime)Data.DocDueDate,
                    DocumentDate = (DateTime)Data.TaxDate,
                    CardCode = (string)Data.CardCode,
                    CardName = (string)Data.CardName,
                    //Address = (string)Data.Address,
                    //NumAtCard = (string)Data.NumAtCard,
                    VatPercent = (string)Data.VatPercent,
                    VatSum = (string)Data.VatSum,
                    DiscPrcnt = (string)Data.DiscPrcnt,
                    DocCur = (string)Data.DocCur,
                    //DocRate = (string)Data.DocRate,
                    DocTotal = (string)Data.DocTotal,
                    Remark = (string)Data.Comments,
                    //JrnlMemo = (string)Data.JrnlMemo,
                    //GroupNum = (string)Data.GroupNum,
                    //SlpCode = (string)Data.SlpCode,
                    //Address2 = (string)Data.Address2,
                    //LicTradNum = (string)Data.LecTradNum,
                    //DeferrTax = (string)Data.DeferrTax,
                    //OwnerCode = (string)Data.OwnerCode,
                    //VatBrance = (string)Data.VatBranch,
                    //Module = "REMCreateGoodsReceiptPO",
                    //MethodName = "REMCreateGoodsReceiptPO",
                    //RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID",
                    //TableName = TableName,
                    //IsService = false,

                };

                foreach (var d in (dynamic)Data.Details)
                {
                    string itemName = (string)d.Dscription;

                    //ICON.SAP.API.SAPTABLE Items = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oItems));
                    //itemName += " " + SAPB1.GetDataColumn("ItemName", Items, "ItemCode", dr["poi_matcode"].ToString());
                    //check item that service or not
                    if (string.IsNullOrEmpty((string)d.VatGroup) || Convert.ToDecimal((string)d.VatGroup) == 0)
                    {
                        VatCode = "NOG";
                    }
                    else
                    {
                        IsDeferVat = IsService ? true : false; //ถ้าไอเทมเป็นบริการจะกำหนดให้ defervat = 'Y'
                        VatCode = "OG";
                    }

                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        ItemCode = (string)d.ItemCode,
                        ItemDescription = itemName,
                        Qty = Convert.ToDouble((string)d.Quantity),
                        TaxCode = VatCode,
                        UnitPrice = (string)d.Price,
                        LineTotal = (string)d.LineTotal,
                        TotalFrgn = (string)d.TotalFrgn,
                        WhsCode = (string)d.WhsCode,
                        IsDeferVAT = IsDeferVat,
                        VatSum = (string)d.VatSum,
                        ProjectCode = string.IsNullOrEmpty((string)d.Project) ? (string)d.WhsCode : (string)d.Project,
                        OcrCode = string.IsNullOrEmpty((string)d.Ocrcode) ? "" : (string)d.Ocrcode,
                        OcrCode2 = string.IsNullOrEmpty((string)d.Ocrcode2) ? "" : (string)d.Ocrcode2,
                        OcrCode3 = string.IsNullOrEmpty((string)d.Ocrcode3) ? "" : (string)d.Ocrcode3,
                        OcrCode4 = string.IsNullOrEmpty((string)d.Ocrcode4) ? "" : (string)d.Ocrcode4,
                        OcrCode5 = string.IsNullOrEmpty((string)d.Ocrcode5) ? "" : (string)d.Ocrcode5,
                    });


                }

                try
                {

                    SAPB1.CreateDocumentAP(Doc, out DocEntry, out DocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message + " " + ex.StackTrace;
                throw new Exception(ErrorMessage);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     HeaderID,
                    DocEntry,
                    DocNum,
                    "",
                    (int)ResponseCode,
                    ErrorMessage,
                    SAPStatus,
                    SAPErrorMessage,
                    LogDetail
                    );
            }
        }
        /****cancel****/
        private string CancelREMGoodReceiptPO(dynamic Data)
        {
            string po_reccode = (string)Data.po_reccode;
            string methodName = "CancelGoodReceiptPOCM";
            string refDescription = "masteri1_icon.grch.po_reccode";
            if (!string.IsNullOrEmpty((string)Data.methodName))
            {
                methodName = (string)Data.methodName;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodReceiptPO_CM",
                    methodName,
                    po_reccode,
                    refDescription,
                    DocEntry,
                    "OPDN.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                string sql = GetSqlInterfaceLog((string)Data.po_reccode, "create");
                DataTable dt = dbHelp.ExecuteDataTable(sql);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();

                if (!string.IsNullOrEmpty(dt.Rows[0]["SAPRefID"].ToString()) && !string.IsNullOrEmpty(dt.Rows[0]["SAPRefNo"].ToString()))
                {
                    Doc.DocEntry = Convert.ToInt32(dt.Rows[0]["SAPRefID"]);
                }
                else
                {
                    throw new Exception("SAP Ref No. is require");
                }

                Doc.DocType = SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes;
                Doc.DocumentDate = (DateTime)Data.canceldate;

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

                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message + " " + ex.StackTrace;
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     po_reccode,
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

        #region #### REM Journal Voucher ####
        private string CreateREMJournalVoucher(dynamic Data)
        {
            string GLVoucherID = (string)Data.GLVoucherID;
            string methodName = "REMCreateJournalVoucher";
            string refDescription = "SAPB1JV2";
            string VatCode = "NOG";
            string dbName = "";
            string series = "";

            string SiteType = "";
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SiteType"]))
            {
                SiteType = System.Configuration.ConfigurationManager.AppSettings["SiteType"].ToString();
            }
            else
            {
                throw new Exception("SiteType Invalid!!!!");
            }

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "JournalVoucher_REM",
                    methodName,
                    GLVoucherID,
                    refDescription,
                    null,
                    "BOTF.BatchNum",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {
                List<dynamic> details = new List<dynamic>();

                DataTable dtDetails = dbHelp.ExecuteDataTable(GetSqlSAPB1JV(Data.GLVoucherID, refDescription));

                string SQL = GetSqlREMDBConnect((string)Data.Project, SiteType);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL);

                if (dt.Rows.Count > 0)
                {
                    dbName = dt.Rows[0]["DBName"].ToString();
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }
                DateTime PostingDate = (DateTime)Data.PostingDate;
                string PostingDateString = PostingDate.ToString("yyyy-MM");
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                series = SAPB1.GetSeries("30", PostingDateString);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    PostingDate = (DateTime)Data.PostingDate, //DateID
                    DocDueDate = (DateTime)Data.DocDueDate,   //DueDate
                    TaxDate = (DateTime)Data.TaxDate,         //TaxDate
                    DocType = SAPbobsCOM.BoObjectTypes.oJournalVouchers,
                    UDF_RefNo = (string)Data.ICON_RefNo,
                    //IsAutoReverse = (string)Data.IsAutoReverse,
                    //StornoDate = (DateTime)Data.StornoDate,
                    Remark = (string)Data.Remark,
                    Project = (string)Data.Project,
                    Series = series,
                    Ref1 = (string)Data.Ref1
                };

                foreach (dynamic d in dtDetails.Rows)
                {
                    //check item that service or not

                    if (string.IsNullOrEmpty(d["VatPercent"].ToString()) || Convert.ToDecimal(d["VatPercent"].ToString()) == 0)
                    {
                        VatCode = "NIG";
                    }
                    else
                    {
                        VatCode = "OG";
                    }

                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        //AccountCode = (string)d["Account"],
                        ShortName = (string)d["Account"],
                        TaxCode = VatCode,
                        Credit = string.IsNullOrEmpty(d["Credit"].ToString()) ? 0 : Convert.ToDouble(d["Credit"].ToString()),
                        Debit = string.IsNullOrEmpty(d["Debit"].ToString()) ? 0 : Convert.ToDouble(d["Debit"].ToString()),
                        LineMemo = string.IsNullOrEmpty(d["LineMemo"].ToString()) ? "" : d["LineMemo"].ToString().Length > 50 ? d["LineMemo"].ToString().Substring(0, 50) : d["LineMemo"].ToString(),
                        ProjectCode = string.IsNullOrEmpty((string)Data.Project) ? "" : (string)Data.Project,
                        OcrCode = string.IsNullOrEmpty((string)d["Ref1"].ToString()) ? "" : (string)d["Ref1"].ToString(),
                        OcrCode2 = string.IsNullOrEmpty((string)d["Ref2"].ToString()) ? "" : (string)d["Ref2"].ToString(),
                        OcrCode3 = string.IsNullOrEmpty((string)d["Ref3"].ToString()) ? "" : (string)d["Ref3"].ToString(),
                        OcrCode4 = string.IsNullOrEmpty((string)d["Ref4"].ToString()) ? "" : (string)d["Ref4"].ToString(),
                        OcrCode5 = string.IsNullOrEmpty((string)d["Ref5"].ToString()) ? "" : (string)d["Ref5"].ToString(),
                        //WhsCode = (string)d.WhsCode,

                        //TAX_BASE = (string)d.TAX_BASE,
                        //TAX_NO = (string)d.TAX_NO,
                        //TAX_REFNO = (string)d.TAX_REFNO,
                        //TAX_PECL = (string)d.TAX_PECL,
                        //TAX_TYPE = (string)d.TAX_TYPE,
                        //TAX_BOOKNO = (string)d.TAX_BOOKNO,
                        //TAX_CARDNAME = (string)d.TAX_CARDNAME,
                        //TAX_ADDRESS = (string)d.TAX_ADDRESS,
                        //TAX_TAXID = (string)d.TAX_TAXID,
                        ////TAX_DATE = Doc.TaxDate,
                        //TAX_CODE = (string)d.TAX_CODE,
                        //TAX_CODENAME = (string)d.TAX_CODENAME,
                        //TAX_RATE = (string)d.TAX_RATE,
                        ////TAX_DEDUCT = string.IsNullOrEmpty((string)d.TAX_DEDUCT) ? "1" : (string)d.TAX_DEDUCT,
                        //TAX_OTHER = (string)d.TAX_OTHER,
                    });

                }

                try
                {
                    SAPB1.CreateJournalVoucher(Doc, out TranID, out DocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();

                    dbHelp.ExecuteNonQuery("update Sys_ACC_GeneralLedgers set poststatus = 'P', modifydate = getdate() where glvoucher = '" + GLVoucherID + "'");
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message + " " + ex.StackTrace;
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     Data.GLVoucherID,
                    DocNum,
                    TranID,
                    GLDocNum,
                    (int)ResponseCode,
                    (int)ResponseCode == 200 ? "" : ErrorMessage,
                    SAPStatus,
                    (int)ResponseCode == 200 ? "" : SAPErrorMessage,
                    LogDetail
                    );
            }
        }
        #endregion

        #region #### REM Journal Entry ####
        private string CreateREMJournalEntry(dynamic Data)
        {
            string GLVoucherID = (string)Data.GLVoucherID;
            string methodName = "REMCreateJournalEntry";
            string refDescription = "SAPB1JV2";
            string VatCode = "NOG";
            string dbName = "";
            string series = "";

            string SiteType = "";
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SiteType"]))
            {
                SiteType = System.Configuration.ConfigurationManager.AppSettings["SiteType"].ToString();
            }
            else
            {
                throw new Exception("SiteType Invalid!!!!");
            }

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "JournalEntry_REM",
                    methodName,
                    GLVoucherID,
                    refDescription,
                    null,
                    "OJDT.TransId",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {
                List<dynamic> details = new List<dynamic>();

                DataTable dtDetails = dbHelp.ExecuteDataTable(GetSqlSAPB1JV(Data.GLVoucherID, refDescription));

                string SQL = GetSqlREMDBConnect((string)Data.Project,SiteType);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL);

                if (dt.Rows.Count > 0)
                {
                    dbName = dt.Rows[0]["DBName"].ToString();
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                string GLType = "";
                //if ((string)Data.GLVoucherID.Substring(0, 2).ToLower() == "jv" || (string)Data.GLVoucherID.Substring(0, 2).ToLower() == "rv")
                //{
                //    GLType = (string)Data.GLVoucherID.Substring(0, 2);
                //}
                if(!string.IsNullOrEmpty(Data.GLType))
                {
                    GLType = (string)Data.GLType;
                }


                DateTime PostingDate = (DateTime)Data.PostingDate;
                string PostingDateString = PostingDate.ToString("yyyy-MM");
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                series = SAPB1.GetSeries("30", PostingDateString, GLType);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    PostingDate = (DateTime)Data.PostingDate, //DateID
                    DocDueDate = (DateTime)Data.DocDueDate,   //DueDate
                    TaxDate = (DateTime)Data.TaxDate,         //TaxDate
                    DocType = SAPbobsCOM.BoObjectTypes.oJournalVouchers,
                    UDF_RefNo = (string)Data.ICON_RefNo,
                    //IsAutoReverse = (string)Data.IsAutoReverse,
                    //StornoDate = (DateTime)Data.StornoDate,
                    Remark = (string)Data.Remark,
                    Project = (string)Data.Project,
                    Series = series,
                    Ref1 = (string)Data.Ref1
                };

                foreach (dynamic d in dtDetails.Rows)
                {
                    //check item that service or not

                    if (string.IsNullOrEmpty(d["VatPercent"].ToString()) || Convert.ToDecimal(d["VatPercent"].ToString()) == 0)
                    {
                        VatCode = "NIG";
                    }
                    else
                    {
                        VatCode = "OG";
                    }

                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        AccountCode = (string)d["Account"],
                        //ShortName = "",
                        TaxCode = VatCode,
                        Credit = string.IsNullOrEmpty(d["Credit"].ToString()) ? 0 : Convert.ToDouble(d["Credit"].ToString()),
                        Debit = string.IsNullOrEmpty(d["Debit"].ToString()) ? 0 : Convert.ToDouble(d["Debit"].ToString()),
                        LineMemo = string.IsNullOrEmpty(d["LineMemo"].ToString()) ? "" : d["LineMemo"].ToString().Length > 50 ? d["LineMemo"].ToString().Substring(0, 50) : d["LineMemo"].ToString(),
                        ProjectCode = string.IsNullOrEmpty((string)Data.Project) ? "" : (string)Data.Project,
                        OcrCode = string.IsNullOrEmpty((string)d["Ref1"].ToString()) ? "" : (string)d["Ref1"].ToString(),
                        OcrCode2 = string.IsNullOrEmpty((string)d["Ref2"].ToString()) ? "" : (string)d["Ref2"].ToString(),
                        OcrCode3 = string.IsNullOrEmpty((string)d["Ref3"].ToString()) ? "" : (string)d["Ref3"].ToString(),
                        OcrCode4 = string.IsNullOrEmpty((string)d["Ref4"].ToString()) ? "" : (string)d["Ref4"].ToString(),
                        OcrCode5 = string.IsNullOrEmpty((string)d["Ref5"].ToString()) ? "" : (string)d["Ref5"].ToString(),

                        //Reference1 = string.IsNullOrEmpty((string)d["Ref1"].ToString()) ? "" : (string)d["Ref1"].ToString(),
                        //Reference2 = string.IsNullOrEmpty((string)d["Ref2"].ToString()) ? "" : (string)d["Ref2"].ToString(),
                        //Reference3 = string.IsNullOrEmpty((string)d["Ref3"].ToString()) ? "" : (string)d["Ref3"].ToString(),
                        //WhsCode = (string)d.WhsCode,

                        //TAX_BASE = (string)d.TAX_BASE,
                        //TAX_NO = (string)d.TAX_NO,
                        //TAX_REFNO = (string)d.TAX_REFNO,
                        //TAX_PECL = (string)d.TAX_PECL,
                        //TAX_TYPE = (string)d.TAX_TYPE,
                        //TAX_BOOKNO = (string)d.TAX_BOOKNO,
                        //TAX_CARDNAME = (string)d.TAX_CARDNAME,
                        //TAX_ADDRESS = (string)d.TAX_ADDRESS,
                        //TAX_TAXID = (string)d.TAX_TAXID,
                        ////TAX_DATE = Doc.TaxDate,
                        //TAX_CODE = (string)d.TAX_CODE,
                        //TAX_CODENAME = (string)d.TAX_CODENAME,
                        //TAX_RATE = (string)d.TAX_RATE,
                        ////TAX_DEDUCT = string.IsNullOrEmpty((string)d.TAX_DEDUCT) ? "1" : (string)d.TAX_DEDUCT,
                        //TAX_OTHER = (string)d.TAX_OTHER,
                    });

                }

                try
                {
                    SAPB1.CreateJournalEntry(Doc, out TranID, out DocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();

                    dbHelp.ExecuteNonQuery("update Sys_ACC_GeneralLedgers set poststatus = 'P', modifydate = getdate() where (glvoucher = '" + GLVoucherID + "' or GLVoucher in (select GLVoucher from Sys_ACC_PostGLManualGroup where GroupID = '" + GLVoucherID + "'))");
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }

                return DocNum;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message + " " + ex.StackTrace;
                throw new Exception(ex.Message);
            }
            finally
            {
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     Data.GLVoucherID,
                    DocNum,
                    TranID,
                    GLDocNum,
                    (int)ResponseCode,
                    (int)ResponseCode == 200 ? "" : ErrorMessage,
                    SAPStatus,
                    (int)ResponseCode == 200 ? "" : SAPErrorMessage,
                    LogDetail
                    );
            }
        }
        #endregion
        #endregion
    }

}
