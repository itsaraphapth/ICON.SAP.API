using ICON.Framework.Provider;
using ICON.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICON.SAP.API.api
{
    public class SAPAPController : ApiController
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

        #region #### PM API ####

        #region --- Get Master From SAP ---
        [HttpPost]
        [Route("api/getmasteritem")]
        [AllowAnonymous]
        public object GetSAPMasterItem(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.AP_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();
            DateTime TranDate = DateTime.Now;

            string dbName = "";
            string companyId = "";
            if (!string.IsNullOrEmpty((string)Data.companyId))
            {
                companyId = (string)Data.companyId;
            }

            try
            {
                //DBHelper dbHelpREM = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
                //string SQL = GetSqlPMDBConnectWithCompany(companyId);
                //System.Data.DataTable dt = dbHelpREM.ExecuteDataTable(SQL);

                //if (dt.Rows.Count > 0)
                //{
                //    dbName = dt.Rows[0]["DBName"].ToString();
                //}

                //ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                //ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                //List<Dictionary<string, object>> MasterItems = SAPB1.GetMasterItem(out SAPStatusCode, out SAPErrorMessage);

                //string sql = string.Empty;

                //foreach (Dictionary<string, object> MasterItem in MasterItems)
                //{
                //    sql = $"select * from AP_Master_Item where Code = '{MasterItem["ItemCode"].ToString()}' and companyid = '{companyId}'";

                //    DataTable DT = dbHelp.ExecuteDataTable(sql, trans);

                //    if (DT.Rows.Count > 0)
                //    {
                //        AP_Master_Item Item = new AP_Master_Item();
                //        Item.ExecCommand.Load(DT.Rows[0]);

                //        Item.EditId = false;
                //        Item.CompanyId = companyId;
                //        Item.Name = MasterItem["ItemName"].ToString();
                //        Item.Description = MasterItem["FrgnName"].ToString();
                //        Item.Type = MasterItem["ItemType"].ToString();
                //        Item.StartDate = DateTime.Now;
                //        Item.ToDate = DateTime.Now;
                //        Item.UnitCode = MasterItem["BuyUnitMsr"] != null ? MasterItem["BuyUnitMsr"].ToString() : null;
                //        Item.PriceListType = null;
                //        //Item.BarCode = MasterItem["CodeBars"] != null ? MasterItem["CodeBars"].ToString() : null;
                //        //Item.LeadTime = MasterItem["LeadTime"] != null ? Convert.ToInt32(MasterItem["LeadTime"]) : 1;
                //        Item.Status = "Active";
                //        Item.ModifyById = 0;
                //        Item.ModifyBy = TranBy;
                //        Item.ModifyDate = TranDate;
                //        Item.ExecCommand.Update(dbHelp, trans);
                //    }
                //    else
                //    {
                //        AP_Master_Item Item = new AP_Master_Item();

                //        Item.Code = MasterItem["ItemCode"].ToString();
                //        Item.CompanyId = companyId;
                //        Item.Name = MasterItem["ItemName"].ToString();
                //        Item.Description = MasterItem["FrgnName"].ToString();
                //        Item.Type = MasterItem["ItemType"].ToString();
                //        Item.StartDate = DateTime.Now;
                //        Item.ToDate = DateTime.Now;
                //        Item.UnitCode = MasterItem["BuyUnitMsr"] != null ? MasterItem["BuyUnitMsr"].ToString() : null;
                //        Item.PriceListType = null;
                //        //Item.BarCode = MasterItem["CodeBars"] != null ? MasterItem["CodeBars"].ToString() : null;
                //        //Item.LeadTime = MasterItem["LeadTime"] != null ? Convert.ToInt32(MasterItem["LeadTime"]) : 1;
                //        Item.Status = "Active";
                //        Item.CreateById = 0;
                //        Item.CreateBy = TranBy;
                //        Item.CreateDate = TranDate;
                //        Item.ModifyById = 0;
                //        Item.ModifyBy = TranBy;
                //        Item.ModifyDate = TranDate;
                //        Item.ExecCommand.Insert(dbHelp, trans);
                //    }
                //}

                //dbHelp.CommitTransaction(trans);
                dbHelp.ExecuteNonQuery("exec Sync_Item");
                dbHelp.CommitTransaction(trans);
                return new { status = true, message = "success", };

            }
            catch (Exception ex)
            {
                dbHelp.RollbackTransaction(trans);
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/getmastersupplier")]
        [AllowAnonymous]
        public object GetSAPMasterSupplier(dynamic Data)
        {
            return new { status = false, message = "error", result = "Function close" };
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.AP_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();
            DateTime TranDate = DateTime.Now;

            string dbName = "";
            string companyId = "";
            if (!string.IsNullOrEmpty((string)Data.companyId))
            {
                companyId = (string)Data.companyId;
            }
            else
            {
                return new { status = false, message = "error", result = "companyId is require" };
            }

            try
            {
                DBHelper dbHelpREM = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
                string SQL = GetSqlPMDBConnectWithCompany(companyId);
                System.Data.DataTable dt = dbHelpREM.ExecuteDataTable(SQL);

                if (dt.Rows.Count > 0)
                {
                    dbName = dt.Rows[0]["DBName"].ToString();
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetMasterSupplier(out SAPStatusCode, out SAPErrorMessage);

                string sql = string.Empty;

                foreach (Dictionary<string, object> MasterItem in MasterItems)
                {
                    sql = $"select * from AP_Master_Supplier where Code = '{MasterItem["CardCode"].ToString()}' and CompanyId = '{companyId}'";

                    DataTable DT = dbHelp.ExecuteDataTable(sql, trans);

                    if (DT.Rows.Count > 0)
                    {
                        AP_Master_Supplier Item = new AP_Master_Supplier();
                        Item.ExecCommand.Load(DT.Rows[0]);

                        Item.Name = MasterItem["CardName"].ToString();
                        Item.ForeignName = MasterItem["CardFName"] != null ? MasterItem["CardFName"].ToString() : null;
                        Item.TaxID = MasterItem["LicTradNum"] != null ? MasterItem["LicTradNum"].ToString() : null;
                        Item.Currency = MasterItem["Currency"].ToString();
                        Item.Tel1 = MasterItem["Phone1"] != null ? MasterItem["Phone1"].ToString() : null;
                        Item.Tel2 = MasterItem["Phone2"] != null ? MasterItem["Phone2"].ToString() : null;
                        Item.MobilePhone = MasterItem["Cellular"] != null ? MasterItem["Cellular"].ToString() : null;
                        Item.Fax = MasterItem["Fax"] != null ? MasterItem["Fax"].ToString() : null;
                        Item.Email = MasterItem["E_Mail"] != null ? MasterItem["E_Mail"].ToString() : null;
                        Item.WebSite = MasterItem["IntrntSite"] != null ? MasterItem["IntrntSite"].ToString() : null;
                        Item.Remark = MasterItem["Notes"] != null ? MasterItem["Notes"].ToString() : null;
                        Item.PaymentTerms = MasterItem["PaymentTerms"] != null ? MasterItem["PaymentTerms"].ToString() : null;
                        Item.Status = "Active";
                        Item.ModifyById = 0;
                        Item.ModifyBy = TranBy;
                        Item.ModifyDate = TranDate;
                        Item.ExecCommand.Update(dbHelp, trans);
                    }
                    else
                    {
                        AP_Master_Supplier Item = new AP_Master_Supplier();

                        Item.Code = MasterItem["CardCode"].ToString();
                        Item.CompanyId = companyId;
                        Item.Name = MasterItem["CardName"].ToString();
                        Item.ForeignName = MasterItem["CardFName"] != null ? MasterItem["CardFName"].ToString() : null;
                        Item.TaxID = MasterItem["LicTradNum"] != null ? MasterItem["LicTradNum"].ToString() : null;
                        Item.Currency = MasterItem["Currency"].ToString();
                        Item.Tel1 = MasterItem["Phone1"] != null ? MasterItem["Phone1"].ToString() : null;
                        Item.Tel2 = MasterItem["Phone2"] != null ? MasterItem["Phone2"].ToString() : null;
                        Item.MobilePhone = MasterItem["Cellular"] != null ? MasterItem["Cellular"].ToString() : null;
                        Item.Fax = MasterItem["Fax"] != null ? MasterItem["Fax"].ToString() : null;
                        Item.Email = MasterItem["E_Mail"] != null ? MasterItem["E_Mail"].ToString() : null;
                        Item.WebSite = MasterItem["IntrntSite"] != null ? MasterItem["IntrntSite"].ToString() : null;
                        Item.Remark = MasterItem["Notes"] != null ? MasterItem["Notes"].ToString() : null;
                        Item.PaymentTerms = MasterItem["PaymentTerms"] != null ? MasterItem["PaymentTerms"].ToString() : null;
                        Item.Status = "Active";
                        Item.ModifyById = 0;
                        Item.ModifyBy = TranBy;
                        Item.ModifyDate = TranDate;
                        Item.ExecCommand.Insert(dbHelp, trans);
                    }


                    #region Supplier Address
                    List<Dictionary<string, object>> MasterSuppliersAddress = SAPB1.GetmasterSupplier_Address(out SAPStatusCode, out SAPErrorMessage, MasterItem["CardCode"].ToString());
                    foreach (Dictionary<string, object> MasterSupplierAddress in MasterSuppliersAddress)
                    {

                        if (DT.Rows.Count > 0)
                        {
                            string sqlD = $"delete from AP_Master_Supplier_Address where SupplierId = '{DT.Rows[0]["Id"].ToString()}'";
                            dbHelp.ExecuteNonQuery(sqlD, trans);

                            AP_Master_Supplier_Address Item = new AP_Master_Supplier_Address();

                            Item.SupplierId = Convert.ToInt32(DT.Rows[0]["Id"].ToString());
                            Item.AddressType = MasterSupplierAddress["AddressType"].ToString();
                            Item.Address = MasterSupplierAddress["Address"] != null ? MasterSupplierAddress["Address"].ToString() : null;
                            Item.PostCode = MasterSupplierAddress["PostCode"] != null ? MasterSupplierAddress["PostCode"].ToString() : null;
                            Item.BranchCode = MasterSupplierAddress["BranchCode"] != null ? MasterSupplierAddress["BranchCode"].ToString() : null;
                            Item.BranchName = MasterSupplierAddress["BranchName"] != null ? MasterSupplierAddress["BranchName"].ToString() : null;
                            Item.IsDefault = MasterSupplierAddress["IsDefault"].ToString() == "0" ? false : true;
                            Item.CreateBy = TranBy;
                            Item.CreateDate = TranDate;
                            Item.ModifyBy = TranBy;
                            Item.ModifyDate = TranDate;
                            Item.ExecCommand.Insert(dbHelp, trans);
                        }
                    }
                    #endregion

                    #region Supplier Contact

                    List<Dictionary<string, object>> MasterSuppliersContact = SAPB1.GetmasterSupplier_Contact(out SAPStatusCode, out SAPErrorMessage, MasterItem["CardCode"].ToString());

                    foreach (Dictionary<string, object> MasterSupplierContact in MasterSuppliersContact)
                    {

                        if (DT.Rows.Count > 0)
                        {
                            string sqlD = $"delete from AP_Master_Supplier_Contact where SupplierId = '{DT.Rows[0]["Id"].ToString()}'";
                            dbHelp.ExecuteNonQuery(sqlD, trans);

                            AP_Master_Supplier_Contact Item = new AP_Master_Supplier_Contact();
                            Item.SupplierCode = MasterSupplierContact["SupplierCode"].ToString();
                            Item.ContactName = MasterSupplierContact["ContactName"].ToString();
                            Item.IsDefault = Convert.ToInt32(MasterSupplierContact["IsDefault"].ToString());
                            Item.SupplierId = Convert.ToInt32(DT.Rows[0]["Id"].ToString());
                            Item.Tel1 = MasterSupplierContact["Tel1"] != null ? MasterSupplierContact["Tel1"].ToString() : null;
                            Item.Tel2 = MasterSupplierContact["Tel2"] != null ? MasterSupplierContact["Tel2"].ToString() : null;
                            Item.Mobile = MasterSupplierContact["Mobile"] != null ? MasterSupplierContact["Mobile"].ToString() : null;
                            Item.Email = MasterSupplierContact["Email"] != null ? MasterSupplierContact["Email"].ToString() : null;
                            Item.Fax = MasterSupplierContact["Fax"] != null ? MasterSupplierContact["Fax"].ToString() : null;
                            Item.ExecCommand.Insert(dbHelp, trans);
                        }
                    }
                    #endregion
                }

                dbHelp.CommitTransaction(trans);
                //dbHelp.RollbackTransaction(trans);
                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                dbHelp.RollbackTransaction(trans);
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/getmastersupplieraddress")]
        [AllowAnonymous]
        public object GetSAPMasterSupplierAddress(dynamic Data)
        {
            return new { status = false, message = "error", result = "Function close" };
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.AP_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();
            DateTime TranDate = DateTime.Now;

            string dbName = "";
            string companyId = "";
            if (!string.IsNullOrEmpty((string)Data.companyId))
            {
                companyId = (string)Data.companyId;
            }
            else
            {
                return new { status = false, message = "error", result = "companyId is require" };
            }

            try
            {
                DBHelper dbHelpREM = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
                string SQL = GetSqlPMDBConnectWithCompany(companyId);
                System.Data.DataTable dt = dbHelpREM.ExecuteDataTable(SQL);

                if (dt.Rows.Count > 0)
                {
                    dbName = dt.Rows[0]["DBName"].ToString();
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetmasterSupplier_Address(out SAPStatusCode, out SAPErrorMessage);

                string sql = string.Empty;

                foreach (Dictionary<string, object> MasterItem in MasterItems)
                {
                    sql = $"select * from AP_Master_Supplier where Code = '{MasterItem["CardCode"].ToString()}' and CompanyId = '{companyId}'";

                    DataTable DT = dbHelp.ExecuteDataTable(sql, trans);

                    if (DT.Rows.Count > 0)
                    {
                        string sqlD = $"delete from AP_Master_Supplier_Address where SupplierId = '{DT.Rows[0]["Id"].ToString()}'";
                        dbHelp.ExecuteNonQuery(sqlD, trans);

                        AP_Master_Supplier_Address Item = new AP_Master_Supplier_Address();

                        Item.SupplierId = Convert.ToInt32(DT.Rows[0]["Id"].ToString());
                        Item.AddressType = MasterItem["AddressType"].ToString();
                        Item.Address = MasterItem["Address"] != null ? MasterItem["Address"].ToString() : null;
                        Item.PostCode = MasterItem["PostCode"] != null ? MasterItem["PostCode"].ToString() : null;
                        Item.BranchCode = MasterItem["BranchCode"] != null ? MasterItem["BranchCode"].ToString() : null;
                        Item.BranchName = MasterItem["BranchName"] != null ? MasterItem["BranchName"].ToString() : null;
                        Item.IsDefault = MasterItem["IsDefault"].ToString() == "0" ? false : true;
                        Item.CreateBy = TranBy;
                        Item.CreateDate = TranDate;
                        Item.ModifyBy = TranBy;
                        Item.ModifyDate = TranDate;
                        Item.ExecCommand.Insert(dbHelp, trans);
                    }
                }

                dbHelp.CommitTransaction(trans);

                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                dbHelp.RollbackTransaction(trans);
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/getmastersuppliercontact")]
        [AllowAnonymous]
        public object GetSAPMasterSupplierContact(dynamic Data)
        {
            return new { status = false, message = "error", result = "Function close" };
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.AP_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();
            DateTime TranDate = DateTime.Now;

            string dbName = "";
            string companyId = "";
            if (!string.IsNullOrEmpty((string)Data.companyId))
            {
                companyId = (string)Data.companyId;
            }
            else
            {
                return new { status = false, message = "error", result = "companyId is require" };
            }

            try
            {
                DBHelper dbHelpREM = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
                string SQL = GetSqlPMDBConnectWithCompany(companyId);
                System.Data.DataTable dt = dbHelpREM.ExecuteDataTable(SQL);

                if (dt.Rows.Count > 0)
                {
                    dbName = dt.Rows[0]["DBName"].ToString();
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetmasterSupplier_Contact(out SAPStatusCode, out SAPErrorMessage);

                string sql = string.Empty;

                foreach (Dictionary<string, object> MasterItem in MasterItems)
                {
                    sql = $"select * from AP_Master_Supplier where Code = '{MasterItem["CardCode"].ToString()}' and CompanyId = '{companyId}'";

                    DataTable DT = dbHelp.ExecuteDataTable(sql, trans);

                    if (DT.Rows.Count > 0)
                    {
                        string sqlD = $"delete from AP_Master_Supplier_Contact where SupplierId = '{DT.Rows[0]["Id"].ToString()}'";
                        dbHelp.ExecuteNonQuery(sqlD, trans);

                        AP_Master_Supplier_Contact Item = new AP_Master_Supplier_Contact();
                        Item.SupplierCode = MasterItem["SupplierCode"].ToString();
                        Item.ContactName = MasterItem["ContactName"].ToString();
                        Item.IsDefault = Convert.ToInt32(MasterItem["IsDefault"].ToString());
                        Item.SupplierId = Convert.ToInt32(DT.Rows[0]["Id"].ToString());
                        Item.Tel1 = MasterItem["Tel1"] != null ? MasterItem["Tel1"].ToString() : null;
                        Item.Tel2 = MasterItem["Tel2"] != null ? MasterItem["Tel2"].ToString() : null;
                        Item.Mobile = MasterItem["Mobile"] != null ? MasterItem["Mobile"].ToString() : null;
                        Item.Email = MasterItem["Email"] != null ? MasterItem["Email"].ToString() : null;
                        Item.Fax = MasterItem["Fax"] != null ? MasterItem["Fax"].ToString() : null;
                        Item.ExecCommand.Insert(dbHelp, trans);
                    }
                }

                dbHelp.CommitTransaction(trans);

                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                dbHelp.RollbackTransaction(trans);
                return new { status = false, message = "error", Result = ex.Message };
            }
        }

        [HttpPost]
        [Route("api/getpettycashbalance")]
        [AllowAnonymous]
        public object GetSAPPettyCaseBalance(dynamic Data)
        {
            string project_code = "";
            string dbName = "";
            if (!string.IsNullOrEmpty((string)Data.project_code))
            {
                project_code = (string)Data.project_code;
            }
            else
            {
                //throw new Exception( "po_reccode is require" );
                return new { status = false, message = "error", result = "project code is require" };
            }

            try
            {
                DBHelper dbHelpREM = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
                string SQL = GetSqlPMDBConnect(project_code);
                System.Data.DataTable dt = dbHelpREM.ExecuteDataTable(SQL);

                if (dt.Rows.Count > 0)
                {
                    dbName = dt.Rows[0]["DBName"].ToString();
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);

                List<Dictionary<string, object>> MasterItems = SAPB1.GetPettyCashBalance(project_code, out SAPStatusCode, out SAPErrorMessage);


                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message };
            }
        }
        #endregion

        /// <summary>
        /// PM Create Goods Receipt PO
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/pm/creategoodsreceiptpo")]
        [AllowAnonymous]
        public object PMCreateGoodsReceiptPO(dynamic Data)
        {
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            dynamic header = Data.header;
            dynamic details = Data.detail;

            string TableName = "SAP_RW_Header";
            bool InterfaceFlag = false;
            try
            {

                if (header != null)
                {
                    //if (!string.IsNullOrEmpty((string)header.InterfaceFlag))
                    //{
                    //    InterfaceFlag = (string)header.InterfaceFlag == "0" ? false : true;
                    //}
                    //else
                    //{
                    //    return new { status = false, message = "error", result = "This document interface to SAP already!!!!" };
                    //}

                    try
                    {

                        try
                        {
                            string DocNum = CreatePMGoodsReceiptPO(new
                            {
                                HeaderID = (string)header.HeaderId,
                                DocDate = (DateTime)header.DocDate,
                                DocDueDate = (DateTime)header.DocDueDate,
                                TaxDate = header.TaxDate == null ? DateTime.MinValue : (DateTime)header.TaxDate,
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
                                TAX_PECL = String.IsNullOrEmpty((string)header.TAX_PECL) ? (string)header.VATPeriod : (string)header.TAX_PECL,
                                Module = "PMCreateGoodsReceiptPO",
                                MethodName = "PMCreateGoodsReceiptPO",
                                RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID",
                                TableName = TableName,
                                IsService = false,
                                Details = details,
                                InterfaceFlag = InterfaceFlag
                            });
                            ResultSuccess.Add(DocNum);

                            return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                        }
                        catch (Exception ex)
                        {
                            ResultError.Add((string)header.HeaderId + " : " + ex.Message);

                            return new { status = false, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
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
        /// PM Cancel Goods Receipt PO
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/pm/cancelgoodreceiptpo")]
        [AllowAnonymous]
        public object PMCancelGoodsReceiptPO(dynamic Data)
        {
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            dynamic header = Data.header;

            string TableName = "SAP_RW_Header";
            if (header != null)
            {
                if (string.IsNullOrEmpty((string)header.SAPCode))
                {
                    return new { status = false, message = "error", result = "SAPCode is require" };
                }
                try
                {

                    try
                    {
                        string DocNum = CancelPMGoodsReceiptPO(new
                        {
                            HeaderID = (string)header.HeaderID,
                            CancelDate = string.IsNullOrEmpty((string)header.CancelDate) ? DateTime.Now : (DateTime)header.CancelDate,
                            SAPCode = (string)header.SAPCode,
                            Project = (string)header.Project,
                            Module = "PMCancelGoodsReceiptPO",
                            MethodName = "PMCancelGoodsReceiptPO",
                            RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID",
                            TableName = TableName,
                        });
                        ResultSuccess.Add(DocNum);

                        return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        ResultError.Add((string)header.HeaderId + " : " + ex.Message);

                        return new { status = false, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
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

        /// <summary>
        /// PM Create Goods Receipt 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/pm/creategoodsreceipt")]
        [AllowAnonymous]
        public object PMCreateGoodsReceipt(dynamic Data)
        {
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            dynamic header = Data.header;
            dynamic details = Data.detail;

            string TableName = "SAP_RW_Header";
            bool InterfaceFlag = false;
            try
            {

                if (header != null)
                {
                    //if (!string.IsNullOrEmpty((string)header.InterfaceFlag))
                    //{
                    //    InterfaceFlag = (string)header.InterfaceFlag == "0" ? false : true;
                    //}
                    //else
                    //{
                    //    return new { status = false, message = "error", result = "This document interface to SAP already!!!!" };
                    //}

                    try
                    {

                        try
                        {
                            string DocNum = CreatePMGoodsReceipt(new
                            {
                                HeaderID = (string)header.HeaderId,
                                DocDate = (DateTime)header.DocDate,
                                DocDueDate = (DateTime)header.DocDueDate,
                                //TaxDate = (DateTime)header.TaxDate,
                                //CardCode = (string)header.CardCode,
                                //CardName = (string)header.CardName,
                                //Address = (string)header.Address,
                                //NumAtCard = (string)header.NumAtCard,
                                //VatPercent = (string)header.VatPercent,
                                //VatSum = (string)header.VatSum,
                                DiscPrcnt = (string)header.DiscPrcnt,
                                //DocCur = (string)header.DocCur,
                                //DocRate = (string)header.DocRate,
                                DocTotal = (string)header.DocTotal,
                                //Comments = (string)header.Comments,
                                //JrnlMemo = (string)header.JrnlMemo,
                                //GroupNum = (string)header.GroupNum,
                                //SlpCode = (string)header.SlpCode,
                                //Address2 = (string)header.Address2,
                                //LicTradNum = (string)header.LecTradNum,
                                //DeferrTax = (string)header.DeferrTax,
                                //OwnerCode = (string)header.OwnerCode,
                                ICON_RefNo = (string)header.ICON_RefNo,
                                U_VatPeriod = (string)header.VATPeriod,
                                U_ICON_Project = (string)header.ICON_Project,
                                //VatBrance = (string)header.VatBranch,
                                //TAX_PECL = (string)header.TAX_PECL,
                                Module = "PMCreateGoodsReceipt",
                                MethodName = "PMCreateGoodsReceipt",
                                RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID",
                                TableName = TableName,
                                IsService = false,
                                Details = details,
                                InterfaceFlag = InterfaceFlag
                            });
                            ResultSuccess.Add(DocNum);

                            return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                        }
                        catch (Exception ex)
                        {
                            ResultError.Add((string)header.HeaderId + " : " + ex.Message);

                            return new { status = false, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
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
        /// PM Create Goods Receipt 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/pm/creategoodsissue")]
        [AllowAnonymous]
        public object PMCreateGoodsIssue(dynamic Data)
        {
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            dynamic header = Data.header;
            dynamic details = Data.detail;

            string TableName = "SAP_RW_Header";
            bool InterfaceFlag = false;
            try
            {

                if (header != null)
                {
                    //if (!string.IsNullOrEmpty((string)header.InterfaceFlag))
                    //{
                    //    InterfaceFlag = (string)header.InterfaceFlag == "0" ? false : true;
                    //}
                    //else
                    //{
                    //    return new { status = false, message = "error", result = "This document interface to SAP already!!!!" };
                    //}

                    try
                    {

                        try
                        {
                            string DocNum = CreatePMGoodsIssue(new
                            {
                                HeaderID = (string)header.HeaderId,
                                DocDate = (DateTime)header.DocDate,
                                DocDueDate = (DateTime)header.DocDueDate,
                                //TaxDate = (DateTime)header.TaxDate,
                                //CardCode = (string)header.CardCode,
                                //CardName = (string)header.CardName,
                                //Address = (string)header.Address,
                                //NumAtCard = (string)header.NumAtCard,
                                //VatPercent = (string)header.VatPercent,
                                //VatSum = (string)header.VatSum,
                                DiscPrcnt = (string)header.DiscPrcnt,
                                //DocCur = (string)header.DocCur,
                                //DocRate = (string)header.DocRate,
                                DocTotal = (string)header.DocTotal,
                                //Comments = (string)header.Comments,
                                //JrnlMemo = (string)header.JrnlMemo,
                                //GroupNum = (string)header.GroupNum,
                                //SlpCode = (string)header.SlpCode,
                                //Address2 = (string)header.Address2,
                                //LicTradNum = (string)header.LecTradNum,
                                //DeferrTax = (string)header.DeferrTax,
                                //OwnerCode = (string)header.OwnerCode,
                                ICON_RefNo = (string)header.ICON_RefNo,
                                U_VatPeriod = (string)header.VATPeriod,
                                U_ICON_Project = (string)header.ICON_Project,
                                //VatBrance = (string)header.VatBranch,
                                //TAX_PECL = (string)header.TAX_PECL,
                                Module = "PMCreateGoodsIssue",
                                MethodName = "PMCreateGoodsIssue",
                                RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID",
                                TableName = TableName,
                                IsService = false,
                                Details = details,
                                InterfaceFlag = InterfaceFlag
                            });
                            ResultSuccess.Add(DocNum);

                            return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                        }
                        catch (Exception ex)
                        {
                            ResultError.Add((string)header.HeaderId + " : " + ex.Message);

                            return new { status = false, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
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
        /// PM Cancel Goods Receipt 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/pm/cancelgoodreceipt")]
        [AllowAnonymous]
        public object PMCancelGoodsReceipt(dynamic Data)
        {
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            dynamic header = Data.header;

            string TableName = "SAP_RW_Header";
            if (header != null)
            {
                if (string.IsNullOrEmpty((string)header.SAPCode))
                {
                    return new { status = false, message = "error", result = "SAPCode is require" };
                }
                try
                {

                    try
                    {
                        string DocNum = CancelPMGoodsReceipt(new
                        {
                            HeaderID = (string)header.HeaderID,
                            CancelDate = string.IsNullOrEmpty((string)header.CancelDate) ? DateTime.Now : (DateTime)header.CancelDate,
                            SAPCode = (string)header.SAPCode,
                            Project = (string)header.Project,
                            Module = "PMCancelGoodsReceipt",
                            MethodName = "PMCancelGoodsReceipt",
                            RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID",
                            TableName = TableName,
                        });
                        ResultSuccess.Add(DocNum);

                        return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        ResultError.Add((string)header.HeaderId + " : " + ex.Message);

                        return new { status = false, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
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
        [HttpPost]
        [Route("api/pm/cancelapinvoice")]
        [AllowAnonymous]
        public object PMCancelAPInvoice(dynamic Data)
        {
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            dynamic header = Data.header;

            string TableName = "SAP_RW_Header";
            if (header != null)
            {
                if (string.IsNullOrEmpty((string)header.SAPCode))
                {
                    return new { status = false, message = "error", result = "SAPCode is require" };
                }
                try
                {

                    try
                    {
                        string DocNum = CancelPMAPInvoice(new
                        {
                            HeaderID = (string)header.HeaderID,
                            CancelDate = string.IsNullOrEmpty((string)header.CancelDate) ? DateTime.Now : (DateTime)header.CancelDate,
                            SAPCode = (string)header.SAPCode,
                            Project = (string)header.Project,
                            Module = "PMCancelGoodsReceiptPO",
                            MethodName = "PMCancelGoodsReceiptPO",
                            RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID",
                            TableName = TableName,
                        });
                        ResultSuccess.Add(DocNum);

                        return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        ResultError.Add((string)header.HeaderId + " : " + ex.Message);

                        return new { status = false, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
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

        /// <summary>
        /// PM Create Goods Return
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/pm/creategoodsreturn")]
        [AllowAnonymous]
        public object PMCreateGoodsReturn(dynamic Data)
        {
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            dynamic header = Data.header;
            dynamic details = Data.detail;

            string TableName = "SAP_RT_Header";
            bool InterfaceFlag = false;
            try
            {

                if (header != null)
                {
                    //if (!string.IsNullOrEmpty((string)header.InterfaceFlag))
                    //{
                    //    InterfaceFlag = (string)header.InterfaceFlag == "0" ? false : true;
                    //}
                    //else
                    //{
                    //    return new { status = false, message = "error", result = "This document interface to SAP already!!!!" };
                    //}
                    try
                    {

                        try
                        {
                            string DocNum = CreatePMGoodsReturn(new
                            {
                                HeaderID = (string)header.HeaderId,
                                DocDate = (DateTime)header.DocDate,
                                DocDueDate = (DateTime)header.DocDueDate,
                                //TaxDate = (DateTime)header.TaxDate,
                                CardCode = (string)header.CardCode,
                                CardName = (string)header.CardName,
                                NumAtCard = (string)header.NumAtCard,
                                DocTotal = (string)header.DocTotal,
                                DiscPrcnt = (string)header.DiscPrcnt,
                                U_ICON_RefNo = (string)header.ICON_RefNo,
                                U_VatPeriod = (string)header.VATPeriod,
                                Module = "PMCreateGoodsReturn",
                                MethodName = "PMCreateGoodsReturn",
                                RefDescription = "SAP_RT_Header.HeaderID",
                                TableName = TableName,
                                IsService = false,
                                Details = details,
                                Datas = Data
                            });
                            ResultSuccess.Add(DocNum);

                            return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                        }
                        catch (Exception ex)
                        {
                            ResultError.Add((string)header.HeaderId + " : " + ex.Message);

                            return new { status = false, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
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
        /// PM Cancel Goods Return
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/pm/cancelgoodsreturn")]
        [AllowAnonymous]
        public object PMCancelGoodsReturn(dynamic Data)
        {
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            dynamic header = Data.header;

            string TableName = "SAP_RW_Header";
            if (header != null)
            {
                if (string.IsNullOrEmpty((string)header.SAPCode))
                {
                    return new { status = false, message = "error", result = "SAPCode is require" };
                }
                try
                {

                    try
                    {
                        string DocNum = CancelPMGoodsReturn(new
                        {
                            HeaderID = (string)header.HeaderID,
                            CancelDate = string.IsNullOrEmpty((string)header.CancelDate) ? DateTime.Now : (DateTime)header.CancelDate,
                            SAPCode = (string)header.SAPCode,
                            Project = (string)header.Project,
                            Module = "PMCancelGoodsReturn",
                            MethodName = "PMCancelGoodsReturn",
                            RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID",
                            TableName = TableName,
                        });
                        ResultSuccess.Add(DocNum);

                        return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                    }
                    catch (Exception ex)
                    {
                        ResultError.Add((string)header.HeaderId + " : " + ex.Message);

                        return new { status = false, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
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

        /// <summary>
        /// PM Create Journal Voucher
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/pm/createjournalvoucher")]
        [AllowAnonymous]
        public object PMCreateJournalVoucher(dynamic Data)
        {
            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.AP_ConnectionString, null);
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            dynamic header = Data.header;
            dynamic details = Data.detail;
            string stornoDate = string.Empty;

            if (!string.IsNullOrEmpty((string)header.StornoDate))
            {
                stornoDate = (string)header.StornoDate;
            }

            string TableName = "SAP_ACCRU_Header";
            bool InterfaceFlag = false;
            try
            {

                if (header != null)
                {
                    //if (!string.IsNullOrEmpty((string)header.InterfaceFlag))
                    //{
                    //    InterfaceFlag = (string)header.InterfaceFlag == "0" ? false : true;
                    //}
                    //else
                    //{
                    //    return new { status = false, message = "error", result = "This document interface to SAP already!!!!" };
                    //}
                    try
                    {

                        try
                        {
                            string DocNum = CreatePMJournalVoucher(new
                            {
                                HeaderID = (string)header.HeaderId,
                                Project = string.IsNullOrEmpty((string)header.ProjectCode) ? string.IsNullOrEmpty((string)header.Project) ? "" : (string)header.Project : (string)header.ProjectCode,
                                Remark = (string)header.Remarks,
                                PostingDate = (DateTime)header.RefDate,
                                DocDueDate = string.IsNullOrEmpty((string)header.DueDate) ? DateTime.MinValue : (DateTime)header.DueDate,
                                TaxDate = string.IsNullOrEmpty((string)header.TaxDate) ? DateTime.MinValue : (DateTime)header.TaxDate,
                                ICON_RefNo = (string)header.ICON_Ref,
                                IsAutoReverse = string.IsNullOrEmpty((string)header.IsAutoReverse) ? "" : (string)header.IsAutoReverse,
                                StornoDate = stornoDate,
                                Module = "PMCreateJournalVoucher",
                                MethodName = "PMCreateJournalVoucher",
                                RefDescription = "A_NEXT_AP.SAP_ACCRU_Header.HeaderID",
                                TableName = TableName,
                                Details = details,
                                Datas = Data
                            });
                            ResultSuccess.Add(DocNum);
                            return new { status = true, message = "success", result = new { DocNum = ResultSuccess, ErrorItem = ResultError } };
                        }
                        catch (Exception ex)
                        {
                            ResultError.Add((string)header.HeaderId + " : " + ex.Message);
                            return new { status = false, message = "error", result = new { DocNum = ResultSuccess, ErrorItem = ResultError }, data = Data };
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


            #region ##### Interface PM to SAP ####
        private string GetSqlPMHeader(string RefID, string TableName = "SAP_RW_Header", string RefName = "HeaderID")
        {
            string sql = $@"
SELECT * FROM {TableName}";

            if (!string.IsNullOrEmpty(RefID))
            {
                sql += $" where {RefName} = '{RefID}'";
            }

            return sql;
        }
        private string GetSqlPMDetail(string RefID, string TableName = "SAP_RW_Detail", string RefName = "HeaderID", string REDB = "STD_PM_Production")
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
        private string GetSqlPMDBConnect(string ProjectID)
        {
            string SQL = $@"
select 
    re.[Value] DBName
from sys_conf_realestate re
inner join sys_master_projects pj on re.CompanyID = pj.CompanyID
where keyname = 'dbname'
and pj.projectid = '{ProjectID}'";

            return SQL;
        }
        private string GetSqlPMDBConnectWithCompany(string CompanyID)
        {
            string SQL = $@"
select 
    re.[Value] DBName
from sys_conf_realestate re
inner join sys_master_projects pj on re.CompanyID = pj.CompanyID
where keyname = 'dbname'
and re.companyid = '{CompanyID}'";

            return SQL;
        }

        #region #### PM Goods Receipt PO ####
        /****create****/
        private string CreatePMGoodsReceiptPO(dynamic Data)
        {
            string HeaderID = (string)Data.HeaderID;
            string MethodName = "PMCreateGoodsReceipt";
            string RefDescription = "SAP_RW_Header";
            string VatCode = "NIG";
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

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodReceipt_PM",
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
                string SQL = GetSqlPMDBConnect((string)details[0].Project);
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
                    DocumentDate = (DateTime)Data.TaxDate == DateTime.MinValue ? (DateTime)Data.DocDate : (DateTime)Data.TaxDate,
                    CardCode = (string)Data.CardCode,
                    CardName = (string)Data.CardName,
                    Address = (string)Data.Address,
                    NumAtCard = (string)Data.NumAtCard,
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
                    //Module = "PMCreateGoodsReceiptPO",
                    //MethodName = "PMCreateGoodsReceiptPO",
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
                    if (string.IsNullOrEmpty((string)d.VatGroup))
                    {
                        VatCode = (string)d.VatGroup;
                    }
                    else
                    {
                        IsDeferVat = IsService ? true : false; //ถ้าไอเทมเป็นบริการจะกำหนดให้ defervat = 'Y'
                        VatCode = (string)d.VatGroup;
                    }

                    string baseEntry = ""; 
                    if (!string.IsNullOrEmpty((string)d.BaseEntry))
                    {
                        string sqlS = string.Format(@"

select 
    saprefid 
from sap_interface_log 
where methodname = 'PMCreateGoodsReturn'
and saprefno = '{0}'
", (string)d.BaseEntry);

                        System.Data.DataTable dtS = dbHelp.ExecuteDataTable(sqlS, trans);

                        if(dtS.Rows.Count > 0)
                        {
                            baseEntry = dtS.Rows[0]["saprefid"].ToString();
                        }
                        else
                        {
                            throw new Exception("SAPRefID NOT FOUND!!!!");
                        }
                    }



                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        LineNumber = string.IsNullOrEmpty((string)d.LineNum) ? string.IsNullOrEmpty((string)d.BaseLine) ? "" : (string)d.BaseLine : (string)d.LineNum,
                        ItemCode = (string)d.ItemCode,
                        ItemDescription = itemName,
                        Qty = Convert.ToDouble((string)d.Quantity),
                        TaxCode = VatCode,
                        UnitPrice = (string)d.Price,
                        PriceAfVAT = (string)d.PriceAFVAT,
                        LineTotal = (string)d.LineTotal,
                        TaxPercentagePerRow = (string)d.VatPrcnt,
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
                        DiscountPerRow = string.IsNullOrEmpty((string)d.DiscountPerRow) ? "" : (string)d.DiscountPerRow,
                        BaseType = string.IsNullOrEmpty((string)d.BaseType) ? 0 : Convert.ToInt32((string)d.BaseType),
                        BaseEntry = baseEntry,
                        
                    });


                }

                try
                {

                    SAPB1.CreateDocumentPMAP(Doc, out DocEntry, out DocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
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
                    LogDetail);
            }
        }
        /****cancel****/
        private string CancelPMGoodsReceiptPO(dynamic Data)
        {
            string HeaderID = (string)Data.HeaderID;
            string SAPCode = (string)Data.SAPCode;
            string MethodName = "PMCancelGoodsReceiptPO";
            string RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID";
            if (!string.IsNullOrEmpty((string)Data.MethodName))
            {
                MethodName = (string)Data.MethodName;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodReceiptPO_CM",
                    MethodName,
                    HeaderID,
                    RefDescription,
                    DocEntry,
                    "OPDN.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                string SQL = GetSqlPMDBConnect((string)Data.Project);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();

                Doc.DocType = SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes;
                ICON.SAP.API.SAPTABLE SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));
                Doc.DocEntry = Convert.ToInt32(SAPB1.GetDocEntry(SAPTABLE, SAPCode));
                Doc.DocumentDate = (DateTime)Data.CancelDate;

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
                     HeaderID,
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
        private string CancelPMAPInvoice(dynamic Data)
        {
            string HeaderID = (string)Data.HeaderID;
            string SAPCode = (string)Data.SAPCode;
            string MethodName = "PMCancelAPInvoice";
            string RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID";
            if (!string.IsNullOrEmpty((string)Data.MethodName))
            {
                MethodName = (string)Data.MethodName;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "APInvoice",
                    MethodName,
                    HeaderID,
                    RefDescription,
                    DocEntry,
                    "OPCH.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                string SQL = GetSqlPMDBConnect((string)Data.Project);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();

                if (dt.Rows.Count > 0)
                {
                    SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                }
                else
                {
                    throw new Exception("Company NOT FOUND!!!!");
                }
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();

                Doc.DocType = SAPbobsCOM.BoObjectTypes.oPurchaseInvoices;
                ICON.SAP.API.SAPTABLE SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));
                Doc.DocEntry = Convert.ToInt32(SAPB1.GetDocEntry(SAPTABLE, SAPCode));
                //Doc.DocumentDate = (DateTime)Data.CancelDate;

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
                     HeaderID,
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

        #region #### PM Goods Receipt ####
        /****create****/
        private string CreatePMGoodsReceipt(dynamic Data)
        {
            string HeaderID = (string)Data.HeaderID;
            string MethodName = "PMCreateGoodsReceipt";
            string RefDescription = "SAP_RW_Header";
            string VatCode = "NIG";
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

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodReceipt_PM",
                    MethodName,
                    (string)Data.ICON_RefNo,
                    RefDescription,
                    DocEntry,
                    "OIGN.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                var details = (dynamic)Data.Details;
                string SQL = GetSqlPMDBConnect((string)details[0].Project);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                if (dt.Rows.Count > 0)
                {
                    SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                }
                else
                {
                    throw new Exception("Company NOT FOUND!!!!");
                }

                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    //CardCode = (string)Data.vendercode,
                    //NumAtCard = (string)Data.taxno,    // Vendor Ref. No.
                    //PostingDate = (DateTime)Data.po_recdate,
                    //DocDueDate = (DateTime)Data.po_recdate,
                    //DocumentDate = (DateTime)Data.po_recdate,
                    DocType = SAPbobsCOM.BoObjectTypes.oInventoryGenEntry,
                    UDF_RefNo = (string)Data.ICON_RefNo,
                    UDF_TAX_PECL = (string)Data.U_VatPeriod,
                    //UDF_CustName = dt.Rows[0]["CustomerName"].ToString(),
                    //UDF_UnitRef = dt.Rows[0]["UnitNumber"].ToString(),
                    UDF_Project = (string)Data.U_ICON_Project,
                    //UDF_ProjectName = dt.Rows[0]["ProjectName"].ToString(),
                    //UDF_TaxID = dt.Rows[0]["TaxID"].ToString(),
                    //UDF_Address = dt.Rows[0]["Address"].ToString(),
                    //IsDeferVAT = Convert.ToBoolean(dt.Rows[0]["IsDeferVAT"])

                    HeaderID = HeaderID,
                    PostingDate = (DateTime)Data.DocDate,
                    DocDueDate = (DateTime)Data.DocDueDate,
                    DocumentDate = (DateTime)Data.DocDate,
                    //CardName = (string)Data.CardName,
                    //Address = (string)Data.Address,
                    //NumAtCard = (string)Data.NumAtCard,
                    // VatPercent = (string)Data.VatPercent,
                    //VatSum = (string)Data.VatSum,
                    DiscPrcnt = (string)Data.DiscPrcnt,
                    //DocCur = (string)Data.DocCur,
                    //DocRate = (string)Data.DocRate,
                    DocTotal = (string)Data.DocTotal,
                    //Remark = (string)Data.Comments,
                    //JrnlMemo = (string)Data.JrnlMemo,
                    //GroupNum = (string)Data.GroupNum,
                    //SlpCode = (string)Data.SlpCode,
                    //Address2 = (string)Data.Address2,
                    //LicTradNum = (string)Data.LecTradNum,
                    //DeferrTax = (string)Data.DeferrTax,
                    //OwnerCode = (string)Data.OwnerCode,
                    //VatBrance = (string)Data.VatBranch,
                    //Module = "PMCreateGoodsReceiptPO",
                    //MethodName = "PMCreateGoodsReceiptPO",
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
                    if (!string.IsNullOrEmpty((string)d.VatGroup))
                    {
                        VatCode = (string)d.VatGroup;
                    }
                    //else
                    //{
                    //    IsDeferVat = IsService ? true : false; //ถ้าไอเทมเป็นบริการจะกำหนดให้ defervat = 'Y'
                    //    VatCode = (string)d.VatGroup;
                    //}

                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        LineNumber = (string)d.LineNum,
                        ItemCode = (string)d.ItemCode,
                        ItemDescription = itemName,
                        Qty = Convert.ToDouble((string)d.Quantity),
                        TaxCode = VatCode,
                        UnitPrice = (string)d.Price,
                        PriceAfVAT = (string)d.PriceAfVAT,
                        DiscountPerRow = (string)d.DiscPrcnt,
                        LineTotal = (string)d.LineTotal,
                        //TaxPercentagePerRow = (string)d.VatPrcnt,
                        //TotalFrgn = (string)d.TotalFrgn,
                        WhsCode = (string)d.WhsCode,
                        AccountCode = (string)d.AcctCode,
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

                    SAPB1.CreateDocumentPMAP(Doc, out DocEntry, out DocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
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
                    LogDetail);
            }
        }
        /****cancel****/
        private string CancelPMGoodsReceipt(dynamic Data)
        {
            string HeaderID = (string)Data.HeaderID;
            string SAPCode = (string)Data.SAPCode;
            string MethodName = "PMCancelGoodsReceipt";
            string RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID";
            if (!string.IsNullOrEmpty((string)Data.MethodName))
            {
                MethodName = (string)Data.MethodName;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodReceipt_PM",
                    MethodName,
                    HeaderID,
                    RefDescription,
                    DocEntry,
                    "OIGN.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                string SQL = GetSqlPMDBConnect((string)Data.Project);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                if (dt.Rows.Count > 0)
                {
                    SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                }
                else
                {
                    throw new Exception("Company NOT FOUND!!!!");
                }
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();

                Doc.DocType = SAPbobsCOM.BoObjectTypes.oInventoryGenEntry;
                ICON.SAP.API.SAPTABLE SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));
                Doc.DocEntry = Convert.ToInt32(SAPB1.GetDocEntry(SAPTABLE, SAPCode));
                Doc.DocumentDate = (DateTime)Data.CancelDate;

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
                     HeaderID,
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

        #region #### PM Goods Issue ####
        /****create****/
        private string CreatePMGoodsIssue(dynamic Data)
        {
            string HeaderID = (string)Data.HeaderID;
            string MethodName = "PMCreateGoodsIssue";
            string RefDescription = "SAP_RW_Header";
            string VatCode = "NIG";
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

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodsIssue_PM",
                    MethodName,
                    (string)Data.ICON_RefNo,
                    RefDescription,
                    DocEntry,
                    "OIGE.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                var details = (dynamic)Data.Details;
                string SQL = GetSqlPMDBConnect((string)details[0].Project);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                if (dt.Rows.Count > 0)
                {
                    SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                }
                else
                {
                    throw new Exception("Company NOT FOUND!!!!");
                }

                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    //CardCode = (string)Data.vendercode,
                    //NumAtCard = (string)Data.taxno,    // Vendor Ref. No.
                    //PostingDate = (DateTime)Data.po_recdate,
                    //DocDueDate = (DateTime)Data.po_recdate,
                    //DocumentDate = (DateTime)Data.po_recdate,
                    DocType = SAPbobsCOM.BoObjectTypes.oInventoryGenExit,
                    UDF_RefNo = (string)Data.ICON_RefNo,
                    UDF_TAX_PECL = (string)Data.U_VatPeriod,
                    //UDF_CustName = dt.Rows[0]["CustomerName"].ToString(),
                    //UDF_UnitRef = dt.Rows[0]["UnitNumber"].ToString(),
                    UDF_Project = (string)Data.U_ICON_Project,
                    //UDF_ProjectName = dt.Rows[0]["ProjectName"].ToString(),
                    //UDF_TaxID = dt.Rows[0]["TaxID"].ToString(),
                    //UDF_Address = dt.Rows[0]["Address"].ToString(),
                    //IsDeferVAT = Convert.ToBoolean(dt.Rows[0]["IsDeferVAT"])

                    HeaderID = HeaderID,
                    PostingDate = (DateTime)Data.DocDate,
                    DocDueDate = (DateTime)Data.DocDueDate,
                    DocumentDate = (DateTime)Data.DocDate,
                    //CardName = (string)Data.CardName,
                    //Address = (string)Data.Address,
                    //NumAtCard = (string)Data.NumAtCard,
                    // VatPercent = (string)Data.VatPercent,
                    //VatSum = (string)Data.VatSum,
                    DiscPrcnt = (string)Data.DiscPrcnt,
                    //DocCur = (string)Data.DocCur,
                    //DocRate = (string)Data.DocRate,
                    DocTotal = (string)Data.DocTotal,
                    //Remark = (string)Data.Comments,
                    //JrnlMemo = (string)Data.JrnlMemo,
                    //GroupNum = (string)Data.GroupNum,
                    //SlpCode = (string)Data.SlpCode,
                    //Address2 = (string)Data.Address2,
                    //LicTradNum = (string)Data.LecTradNum,
                    //DeferrTax = (string)Data.DeferrTax,
                    //OwnerCode = (string)Data.OwnerCode,
                    //VatBrance = (string)Data.VatBranch,
                    //Module = "PMCreateGoodsReceiptPO",
                    //MethodName = "PMCreateGoodsReceiptPO",
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
                    if (!string.IsNullOrEmpty((string)d.VatGroup))
                    {
                        VatCode = (string)d.VatGroup;
                    }
                    //else
                    //{
                    //    IsDeferVat = IsService ? true : false; //ถ้าไอเทมเป็นบริการจะกำหนดให้ defervat = 'Y'
                    //    VatCode = (string)d.VatGroup;
                    //}

                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        LineNumber = (string)d.LineNum,
                        ItemCode = (string)d.ItemCode,
                        ItemDescription = itemName,
                        Qty = Convert.ToDouble((string)d.Quantity),
                        TaxCode = VatCode,
                        UnitPrice = (string)d.Price,
                        PriceAfVAT = (string)d.PriceAfVAT,
                        DiscountPerRow = (string)d.DiscPrcnt,
                        LineTotal = (string)d.LineTotal,
                        //TaxPercentagePerRow = (string)d.VatPrcnt,
                        //TotalFrgn = (string)d.TotalFrgn,
                        WhsCode = (string)d.WhsCode,
                        AccountCode = (string)d.AcctCode,
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

                    SAPB1.CreateDocumentPMAP(Doc, out DocEntry, out DocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
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
                    LogDetail);
            }
        }
        #endregion

        #region #### PM Goods Return ####
        private string CreatePMGoodsReturn(dynamic Data)
        {
            string HeaderID = (string)Data.HeaderID;
            string MethodName = "PMCreateGoodsReturn";
            string RefDescription = "SAP_RT_Header";
            string VatCode = "NIG";
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

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodsReturn_PM",
                    MethodName,
                    (string)Data.U_ICON_RefNo,
                    RefDescription,
                    DocEntry,
                    "ORPD.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                var details = (dynamic)Data.Details;
                string SQL = GetSqlPMDBConnect((string)details[0].Project);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                if (dt.Rows.Count > 0)
                {
                    SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                }
                else
                {
                    throw new Exception("Company NOT FOUND!!!!");
                }

                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    DocType = SAPbobsCOM.BoObjectTypes.oPurchaseReturns,

                    PostingDate = (DateTime)Data.DocDate,
                    DocDueDate = (DateTime)Data.DocDueDate,
                    DocumentDate = (DateTime)Data.DocDate,
                    CardCode = (string)Data.CardCode,
                    CardName = (string)Data.CardName,
                    NumAtCard = (string)Data.NumAtCard,
                    DocTotal = (string)Data.DocTotal,
                    DiscPrcnt = (string)Data.DiscPrcnt,
                    UDF_RefNo = (string)Data.U_ICON_RefNo,
                    UDF_TAX_PECL = (string)Data.U_VatPeriod,
                };

                foreach (var d in (dynamic)Data.Details)
                {
                    string itemName = (string)d.Dscription;

                    //ICON.SAP.API.SAPTABLE Items = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oItems));
                    //itemName += " " + SAPB1.GetDataColumn("ItemName", Items, "ItemCode", dr["poi_matcode"].ToString());
                    //check item that service or not
                    //if (string.IsNullOrEmpty((string)d.VatGroup) || Convert.ToDecimal((string)d.VatGroup) == 0)
                    //{
                    //    VatCode = "NIG";
                    //}
                    //else
                    //{
                    IsDeferVat = IsService ? true : false; //ถ้าไอเทมเป็นบริการจะกำหนดให้ defervat = 'Y'
                    //    VatCode = "IG";
                    //}

                    VatCode = (string)d.VatGroup;

                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        LineNumber = (string)d.LineNum,
                        ItemCode = (string)d.ItemCode,
                        ItemDescription = itemName,
                        Qty = Convert.ToDouble((string)d.Quantity),
                        UnitPrice = (string)d.Price,
                        PriceAfVAT = (string)d.PriceAfVAT,
                        DiscountPerRow = Data.DiscPrcnt,
                        WhsCode = (string)d.WhsCode,
                        AccountCode = (string)d.AcctCode,
                        ProjectCode = string.IsNullOrEmpty((string)d.Project) ? (string)d.WhsCode : (string)d.Project,
                        TaxCode = VatCode,
                        LineTotal = (string)d.LineTotal,
                        //TotalFrgn = (string)d.TotalFrgn,
                        VatSum = (string)d.VatSum,
                        IsDeferVAT = IsDeferVat,
                        OcrCode = string.IsNullOrEmpty((string)d.Ocrcode) ? "" : (string)d.Ocrcode,
                        OcrCode2 = string.IsNullOrEmpty((string)d.Ocrcode2) ? "" : (string)d.Ocrcode2,
                        OcrCode3 = string.IsNullOrEmpty((string)d.Ocrcode3) ? "" : (string)d.Ocrcode3,
                        OcrCode4 = string.IsNullOrEmpty((string)d.Ocrcode4) ? "" : (string)d.Ocrcode4,
                        OcrCode5 = string.IsNullOrEmpty((string)d.Ocrcode5) ? "" : (string)d.Ocrcode5,
                        BaseType = string.IsNullOrEmpty((string)d.BaseType) ? 0 : Convert.ToInt32((string)d.BaseType),
                        BaseEntry = (string)d.BaseEntry,

                    });


                }

                try
                {

                    SAPB1.CreateDocumentPMAP(Doc, out DocEntry, out DocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
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
        private string CancelPMGoodsReturn(dynamic Data)
        {
            string HeaderID = (string)Data.HeaderID;
            string SAPCode = (string)Data.SAPCode;
            string MethodName = "PMCancelGoodsReturn";
            string RefDescription = "A_NEXT_AP.SAP_RW_Header.HeaderID";
            if (!string.IsNullOrEmpty((string)Data.MethodName))
            {
                MethodName = (string)Data.MethodName;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);
            IDbTransaction trans = dbHelp.BeginTransaction();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodsReturn_PM",
                    MethodName,
                    HeaderID,
                    RefDescription,
                    DocEntry,
                    "ORPD.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                string SQL = GetSqlPMDBConnect((string)Data.Project);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL, trans);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                if (dt.Rows.Count > 0)
                {
                    SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                }
                else
                {
                    throw new Exception("Company NOT FOUND!!!!");
                }
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();

                Doc.DocType = SAPbobsCOM.BoObjectTypes.oPurchaseReturns;
                ICON.SAP.API.SAPTABLE SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));
                Doc.DocEntry = Convert.ToInt32(SAPB1.GetDocEntry(SAPTABLE, SAPCode));
                Doc.DocumentDate = (DateTime)Data.CancelDate;

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
                     HeaderID,
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

        #region #### PM Journal Voucher ####
        private string CreatePMJournalVoucher(dynamic Data)
        {
            string HeaderID = (string)Data.HeaderID;
            string methodName = "PMCreateJournalVoucher";
            string refDescription = "SAP_ACCRU_Header";
            //string VatCode = "NIG";
            string dbName = "";

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "JournalVoucher_PM",
                    methodName,
                    HeaderID,
                    refDescription,
                    null,
                    "BOTF.BatchNum",
                    Newtonsoft.Json.JsonConvert.SerializeObject((dynamic)Data.Datas),
                    TranBy);
            try
            {
                var details = (dynamic)Data.Details;
                string SQL = GetSqlPMDBConnect((string)details[0].Project);
                System.Data.DataTable dt = dbHelp.ExecuteDataTable(SQL);

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();

                if (dt.Rows.Count > 0)
                {
                    SAPB1 = oSAPB1(dt.Rows[0]["DBName"].ToString());
                }
                else
                {
                    throw new Exception("Company NOT FOUND!!!!");
                }

                if (string.IsNullOrEmpty((string)Data.ICON_RefNo))
                {
                    throw new Exception("ICON_Ref is require");
                }

                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    PostingDate = (DateTime)Data.PostingDate,
                    DocDueDate = (DateTime)Data.DocDueDate,
                    TaxDate = (DateTime)Data.TaxDate,
                    DocType = SAPbobsCOM.BoObjectTypes.oJournalVouchers,
                    UDF_RefNo = (string)Data.ICON_RefNo,
                    IsAutoReverse = (string)Data.IsAutoReverse,
                    StornoDate = (string)Data.StornoDate,
                    Project = (string)Data.Project,
                    JrnlMemo = (string)Data.Remark
                };

                foreach (var d in (dynamic)Data.Details)
                {
                    //check item that service or not

                    //if (string.IsNullOrEmpty((string)d.VatGroup))
                    //{
                    //    VatCode = (string)d.VatGroup;
                    //}
                    //else
                    //{
                    //    VatCode = (string)d.VatGroup;
                    //}

                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        AccountCode = (string)d.Account,
                        TaxGroup = (string)d.VatGroup,
                        Credit = string.IsNullOrEmpty((string)d.Credit) ? 0 : Convert.ToDouble((string)d.Credit),
                        Debit = string.IsNullOrEmpty((string)d.Debit) ? 0 : Convert.ToDouble((string)d.Debit),
                        ShortName = string.IsNullOrEmpty((string)d.ShortName) ? "" : (string)d.ShortName,
                        ContraAccount = string.IsNullOrEmpty((string)d.ContraAct) ? "" : (string)d.ContraAct,
                        LineMemo = (string)d.LineMemo,
                        Reference1 = (string)d.Ref1,
                        Reference2 = (string)d.Ref2,
                        CostCode = string.IsNullOrEmpty((string)d.ProfitCode) ? "" : (string)d.ProfitCode,
                        ProjectCode = string.IsNullOrEmpty((string)d.Project) ? "" : (string)d.Project,
                        OcrCode = string.IsNullOrEmpty((string)d.Ocrcode) ? "" : (string)d.Ocrcode,
                        OcrCode2 = string.IsNullOrEmpty((string)d.Ocrcode2) ? "" : (string)d.Ocrcode2,
                        OcrCode3 = string.IsNullOrEmpty((string)d.Ocrcode3) ? "" : (string)d.Ocrcode3,
                        OcrCode4 = string.IsNullOrEmpty((string)d.Ocrcode4) ? "" : (string)d.Ocrcode4,
                        OcrCode5 = string.IsNullOrEmpty((string)d.Ocrcode5) ? "" : (string)d.Ocrcode5,
                        //WhsCode = (string)d.WhsCode,
                        U_VATBRANCH = string.IsNullOrEmpty((string)d.VATBRANCH) ? "" : (string)d.VATBRANCH,
                        U_BPBRANCH = string.IsNullOrEmpty((string)d.BPBRANCH) ? "" : (string)d.BPBRANCH,
                        TAX_BASE = (string)d.TAX_BASE,
                        TAX_NO = string.IsNullOrEmpty((string)d.TAX_NO) ? "" : (string)d.TAX_NO,
                        TAX_REFNO = (string)d.TAX_REFNO,
                        TAX_PECL = (string)d.TAX_PECL,
                        TAX_TYPE = (string)d.TAX_TYPE,
                        TAX_BOOKNO = (string)d.TAX_BOOKNO,
                        TAX_CARDNAME = (string)d.TAX_CARDNAME,
                        TAX_ADDRESS = (string)d.TAX_ADDRESS,
                        TAX_TAXID = (string)d.TAX_TAXID,
                        TAX_CODE = (string)d.TAX_CODE,
                        TAX_CODENAME = (string)d.TAX_CODENAME,
                        TAX_RATE = (string)d.TAX_RATE,
                        TAX_DEDUCT = string.IsNullOrEmpty((string)d.TAX_DEDUCT) ? "" : (string)d.TAX_DEDUCT,
                        TAX_DATE = string.IsNullOrEmpty((string)d.TAX_DATE) ? DateTime.MinValue : (DateTime)d.TAX_DATE,
                        TAX_OTHER = string.IsNullOrEmpty((string)d.TAX_OTHER) ? "" : (string)d.TAX_OTHER,
                    });

                }

                try
                {
                    SAPB1.CreateJournalVoucher(Doc, out TranID, out DocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
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
                     TranID,
                    DocNum,
                    TranID,
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


    }
}
