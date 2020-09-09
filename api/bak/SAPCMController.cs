using ICON.Framework.Provider;
using ICON.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Http;

namespace ICON.SAP.API.api
{
    public class SAPCMController : ApiController
    {
        public string ErrorMessage = "";
        public int SAPStatusCode = 0;
        public string SAPStatus = null;
        public string SAPErrorMessage = null;
        public string TranBy = "SAPApi";
        public string CardCode = "C00001";
        public static string mySQLConn = ICON.Configuration.Database.CM_ConnectionString;

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

        #region #### CM API ####

        #region #### Get Master From SAP ####
        /****item master****/
        [HttpPost]
        [Route("api/cm/getmasteritem")]
        [AllowAnonymous]
        public object GetSAPMasterItem(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            MySqlCommand myCommand = conn.CreateCommand();

            MySqlTransaction myTrans;
            // start a local transaction
            myTrans = conn.BeginTransaction();
            // must assign both transaction object and connection
            // to command object for a pending local transaction
            myCommand.Connection = conn;
            myCommand.Transaction = myTrans;

            List<string> resultsuccess = new List<string>();
            List<string> resulterror = new List<string>();
            DateTime trandate = DateTime.Now;

            try
            {
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetMasterItem(out SAPStatusCode, out SAPErrorMessage);

                string sql = string.Empty;

                foreach (Dictionary<string, object> MasterItem in MasterItems)
                {
                    sql = $"select * from mat_unit where materialcode = '{MasterItem["ItemCode"].ToString()}'";

                    MySqlDataAdapter da = default(MySqlDataAdapter);
                    System.Data.DataTable DataGet = new System.Data.DataTable();
                    da = new MySqlDataAdapter(sql, conn);
                    da.Fill(DataGet);

                    if (DataGet.Rows.Count > 0)
                    {
                        myCommand.CommandText = $@"update mat_unit set 
                                                                        materialname = '{MasterItem["ItemName"].ToString()}', 
                                                                        mat_type = '{MasterItem["Mat_Type"].ToString()}', 
                                                                        matunit = '',
                                                                        pac_expen = '{MasterItem["DfltExpn"].ToString()}',
                                                                        invntitem = '{MasterItem["InvntItem"].ToString()}'
                                                                  where materialcode = '{MasterItem["ItemCode"].ToString()}'";
                        myCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        myCommand.CommandText = $@"insert into mat_unit (materialcode, materialname,mat_type,matunit,pac_expen,invntitem) 
                                                                VALUES ('{MasterItem["ItemCode"].ToString()}', 
                                                                        '{MasterItem["ItemName"].ToString()}',
                                                                        '{MasterItem["Mat_Type"].ToString()}',
                                                                        '{MasterItem["BuyUnitMsr"].ToString()}',
                                                                        '{MasterItem["DfltExpn"].ToString()}',
                                                                        '{MasterItem["InvntItem"].ToString()}')";
                        myCommand.ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
                //myTrans.Rollback();
                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                return new { status = false, message = "error", Result = ex.Message + " " + ex.StackTrace };
            }
            finally
            {
                conn.Close();
            }
        }

        /****supplier master****/
        [HttpPost]
        [Route("api/cm/getmastersupplier")]
        [AllowAnonymous]
        public object GetSAPMasterSupplier(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            MySqlCommand myCommand = conn.CreateCommand();

            MySqlTransaction myTrans;
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = conn;
            myCommand.Transaction = myTrans;

            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();
            DateTime TranDate = DateTime.Now;

            try
            {
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetMasterSupplier(out SAPStatusCode, out SAPErrorMessage);

                string sql = string.Empty;

                foreach (Dictionary<string, object> MasterItem in MasterItems)
                {
                    sql = $"select * from vender where vender_code = '{MasterItem["CardCode"].ToString()}'";

                    MySqlDataAdapter da = default(MySqlDataAdapter);
                    System.Data.DataTable DataGet = new System.Data.DataTable();
                    da = new MySqlDataAdapter(sql, conn);
                    da.Fill(DataGet);

                    if (DataGet.Rows.Count > 0)
                    {
                        myCommand.CommandText = $@"update vender set 
                                                                    vender_name = '{MasterItem["CardName"].ToString()}',
                                                                    vender_tel = '{MasterItem["Phone1"].ToString()}',
                                                                    vender_fax = '{MasterItem["Fax"].ToString()}',
                                                                    vender_tax = '{MasterItem["LicTradNum"].ToString()}',
                                                                    vender_credit = '{MasterItem["PaymentTerms"].ToString()}',
                                                                    vender_sale = '{MasterItem["E_Mail"].ToString()}',
                                                                    vender_address = '{MasterItem["Address"].ToString()}'
                                                  where vender_code = '{MasterItem["CardCode"].ToString()}'";
                        myCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        myCommand.CommandText = $@"insert into vender (vender_code, vender_name,vender_tel,vender_fax,vender_tax,vender_credit,vender_sale,vender_address) 
                                                              VALUES('{MasterItem["CardCode"].ToString()}',
                                                                      '{MasterItem["CardName"].ToString()}',
                                                                      '{MasterItem["Phone1"].ToString()}',
                                                                      '{MasterItem["Fax"].ToString()}',
                                                                      '{MasterItem["LicTradNum"].ToString()}',
                                                                      '{MasterItem["PaymentTerms"].ToString()}',
                                                                      '{MasterItem["E_Mail"].ToString()}',
                                                                      '{MasterItem["Address"].ToString()}')";
                        myCommand.ExecuteNonQuery();
                    }
                }

                myTrans.Commit();

                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                return new { status = false, message = "error", Result = ex.Message + " " + ex.StackTrace };
            }
            finally
            {
                conn.Close();
            }
        }

        /****cost code master****/
        [HttpPost]
        [Route("api/cm/getmastercostcode")]
        [AllowAnonymous]
        public object GetSAPMasterCostCode(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            MySqlCommand myCommand = conn.CreateCommand();

            MySqlTransaction myTrans;
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = conn;
            myCommand.Transaction = myTrans;

            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();
            DateTime TranDate = DateTime.Now;

            try
            {
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetMasterCostCode(out SAPStatusCode, out SAPErrorMessage);

                string sql = string.Empty;
                foreach (Dictionary<string, object> MasterItem in MasterItems)
                {
                    sql = $"select * from cost_subgroup where costcode_d = '{MasterItem["PrcCode"].ToString()}'";

                    MySqlDataAdapter da = default(MySqlDataAdapter);
                    System.Data.DataTable DataGet = new System.Data.DataTable();
                    da = new MySqlDataAdapter(sql, conn);
                    da.Fill(DataGet);

                    if (DataGet.Rows.Count > 0)
                    {
                        myCommand.CommandText = $@"update cost_subgroup set 
                                                                    cgroup_code = '{MasterItem["PrcCode"].ToString()}',
                                                                    cgroup_name = '{MasterItem["PrcName"].ToString()}',
                                                                    costhead = 'D',
                                                                    cost_status = 'N',
                                                                    ctype_name = '{MasterItem["DimDesc"].ToString()}',
                                                                    ctype_code = '{MasterItem["DimCode"].ToString()}'
                                                  where costcode_d = '{MasterItem["PrcCode"].ToString()}'";
                        myCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        myCommand.CommandText = $@"insert into cost_subgroup (costcode_d,cgroup_code, cgroup_name, ctype_name, costhead,cost_status,ctype_code) 
                                                              VALUES('{MasterItem["PrcCode"].ToString()}',
                                                                      '{MasterItem["PrcCode"].ToString()}',
                                                                      '{MasterItem["PrcName"].ToString()}',
                                                                      '{MasterItem["DimDesc"].ToString()}',
                                                                      'D',
                                                                      'N',
                                                                      '{MasterItem["DimCode"].ToString()}')";
                        myCommand.ExecuteNonQuery();
                    }
                }

                myTrans.Commit();

                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                return new { status = false, message = "error", Result = ex.Message + " " + ex.StackTrace };
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpPost]
        [Route("api/cm/getpettycashbalance")]
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
                return new { status = false, message = "error", result = "project_code is require" };
            }

            try
            {
                MySqlConnection conn = new MySqlConnection(mySQLConn);
                conn.Open();
                string SQL = GetSqlCMDBConnect(project_code);
                MySqlDataAdapter daConf = default(MySqlDataAdapter);
                System.Data.DataTable dtConf = new System.Data.DataTable();
                daConf = new MySqlDataAdapter(SQL, conn);
                daConf.Fill(dtConf);

                if (dtConf.Rows.Count > 0)
                {
                    dbName = dtConf.Rows[0]["DBName"].ToString();
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetPettyCashBalance(project_code, out SAPStatusCode, out SAPErrorMessage);


                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message + " " + ex.StackTrace };
            }
        }

        [HttpPost]
        [Route("api/cm/getmasterpettycashbalance")]
        [AllowAnonymous]
        public object GetSAPMasterPettyCaseBalance(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            MySqlCommand myCommand = conn.CreateCommand();

            MySqlTransaction myTrans;
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = conn;
            myCommand.Transaction = myTrans;

            try
            {
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetMasterPettyCashBalance(out SAPStatusCode, out SAPErrorMessage);

                string sql = string.Empty;

                foreach (Dictionary<string, object> MasterItem in MasterItems)
                {
                    sql = $"select * from master_payto where bd_code = '{MasterItem["CardCode"].ToString()}' and project = '{MasterItem["U_Project"].ToString()}'";

                    MySqlDataAdapter da = default(MySqlDataAdapter);
                    System.Data.DataTable DataGet = new System.Data.DataTable();
                    da = new MySqlDataAdapter(sql, conn);
                    da.Fill(DataGet);

                    if (DataGet.Rows.Count > 0)
                    {
                        myCommand.CommandText = $@"update master_payto set 
                                                                    bd_code = '{MasterItem["CardCode"].ToString()}',
                                                                    bd_name = '{MasterItem["CardName"].ToString()}',
                                                                    project = '{MasterItem["U_Project"].ToString()}'
                                                  where bd_code = '{MasterItem["CardCode"].ToString()}' and  project = '{MasterItem["U_Project"].ToString()}'";
                        myCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        myCommand.CommandText = $@"insert into master_payto (bd_code, bd_name,status,project) 
                                                              VALUES('{MasterItem["CardCode"].ToString()}',
                                                                      '{MasterItem["CardName"].ToString()}',
                                                                      'Y',
                                                                      '{MasterItem["U_Project"].ToString()}')";
                        myCommand.ExecuteNonQuery();
                    }
                }

                myTrans.Commit();

                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message + " " + ex.StackTrace };
            }
        }

        [HttpPost]
        [Route("api/cm/getmasteraccountcode")]
        [AllowAnonymous]
        public object GetSAPMasterAccountCode(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            MySqlCommand myCommand = conn.CreateCommand();

            MySqlTransaction myTrans;
            // start a local transaction
            myTrans = conn.BeginTransaction();
            // must assign both transaction object and connection
            // to command object for a pending local transaction
            myCommand.Connection = conn;
            myCommand.Transaction = myTrans;

            List<string> resultsuccess = new List<string>();
            List<string> resulterror = new List<string>();
            DateTime trandate = DateTime.Now;

            try
            {
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetMasterAccountCode(out SAPStatusCode, out SAPErrorMessage);

                string sql = string.Empty;

                foreach (Dictionary<string, object> MasterItem in MasterItems)
                {
                    sql = $"select * from account_total where act_code = '{MasterItem["Code"].ToString()}'";

                    MySqlDataAdapter da = default(MySqlDataAdapter);
                    System.Data.DataTable DataGet = new System.Data.DataTable();
                    da = new MySqlDataAdapter(sql, conn);
                    da.Fill(DataGet);

                    if (DataGet.Rows.Count > 0)
                    {
                        myCommand.CommandText = $@"update account_total set 
                                                                        act_name = '{MasterItem["Name"].ToString()}',
                                                                        act_status = '{MasterItem["Postable"].ToString()}'
                                                                  where act_code = '{MasterItem["Code"].ToString()}'";
                        myCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        myCommand.CommandText = $@"insert into account_total (act_code, act_name, act_status) 
                                                                VALUES ('{MasterItem["Code"].ToString()}',
                                                                        '{MasterItem["Name"].ToString()}',
                                                                        '{MasterItem["Postable"].ToString()}')";
                        myCommand.ExecuteNonQuery();
                    }
                }

                myTrans.Commit();

                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                return new { status = false, message = "error", Result = ex.Message + " " + ex.StackTrace };
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpPost]
        [Route("api/cm/getstockbalance")]
        [AllowAnonymous]
        public object GetSAPStockBalance(dynamic Data)
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
                return new { status = false, message = "error", result = "project_code is require" };
            }

            try
            {
                MySqlConnection conn = new MySqlConnection(mySQLConn);
                conn.Open();
                string SQL = GetSqlCMDBConnect(project_code);
                MySqlDataAdapter daConf = default(MySqlDataAdapter);
                System.Data.DataTable dtConf = new System.Data.DataTable();
                daConf = new MySqlDataAdapter(SQL, conn);
                daConf.Fill(dtConf);

                if (dtConf.Rows.Count > 0)
                {
                    dbName = dtConf.Rows[0]["DBName"].ToString();
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetStockBalance("", project_code, out SAPStatusCode, out SAPErrorMessage);


                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message + " " + ex.StackTrace };
            }
        }

        [HttpGet]
        [Route("api/cm/getstockbalancetest")]
        [AllowAnonymous]
        public object GetSAPStockBalanceTest()
        {
            string project_code = "01-MRP";
            try
            {

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1("SBO_MNP");

                List<Dictionary<string, object>> MasterItems = SAPB1.GetStockBalance("", project_code, out SAPStatusCode, out SAPErrorMessage);


                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message + " " + ex.StackTrace };
            }
            //finally
            //{
            //    ICON.Interface.Transaction.UpdateSAPLog(
            //        Log.TranID,
            //         po_reccode,
            //        DocEntry,
            //        DocNum,
            //        "",
            //        (int)ResponseCode,
            //        ErrorMessage,
            //        SAPStatus,
            //        SAPErrorMessage
            //        );
            //}
        }

        [HttpPost]
        [Route("api/cm/getissuereason")]
        [AllowAnonymous]
        public object GetSAPIssueReason(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            MySqlCommand myCommand = conn.CreateCommand();

            MySqlTransaction myTrans;
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = conn;
            myCommand.Transaction = myTrans;

            try
            {
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1();
                ICON.SAP.API.SAPB1.Document doc = new ICON.SAP.API.SAPB1.Document();

                List<Dictionary<string, object>> MasterItems = SAPB1.GetIssueReason(out SAPStatusCode, out SAPErrorMessage);

                string sql = string.Empty;

                foreach (Dictionary<string, object> MasterItem in MasterItems)
                {

                    myCommand.CommandText = $@"insert into ic_issue_description (remark, acccode) 
                                                              VALUES('{MasterItem["Name"].ToString()}',
                                                                      '{MasterItem["U_ICON_AcctCode"].ToString()}')";
                    myCommand.ExecuteNonQuery();

                }

                myTrans.Commit();

                return new { status = true, message = "success", data = MasterItems };

            }
            catch (Exception ex)
            {
                return new { status = false, message = "error", Result = ex.Message + " " + ex.StackTrace };
            }
        }

        //[HttpPost]
        //[Route("api/cm/getstockbalance")]
        //[AllowAnonymous]
        //public object GetSAPStockBalance(dynamic Data)
        //{
        //    MySqlConnection conn = new MySqlConnection(mySQLConn);
        //    conn.Open();
        //    MySqlCommand myCommand = conn.CreateCommand();

        //    //MySqlTransaction myTrans;
        //    // Start a local transaction
        //    //myTrans = conn.BeginTransaction();
        //    // Must assign both transaction object and connection
        //    // to Command object for a pending local transaction
        //    //myCommand.Connection = conn;
        //    //myCommand.Transaction = myTrans;

        //    try
        //    {
        //        string dbName = "";
        //        string sql = string.Empty;
        //        sql = $"select * from store order by wh,store_matcode";

        //        MySqlDataAdapter da = default(MySqlDataAdapter);
        //        System.Data.DataTable DataGet = new System.Data.DataTable();
        //        da = new MySqlDataAdapter(sql, conn);
        //        da.Fill(DataGet);

        //        if (DataGet.Rows.Count > 0)
        //        {
        //            foreach (System.Data.DataRow dr in DataGet.Rows)
        //            {

        //                string SQL = GetSqlCMDBConnect(dr["wh"].ToString());
        //                MySqlDataAdapter daConf = default(MySqlDataAdapter);
        //                System.Data.DataTable dtConf = new System.Data.DataTable();
        //                daConf = new MySqlDataAdapter(SQL, conn);
        //                daConf.Fill(dtConf);

        //                if (dtConf.Rows.Count > 0)
        //                {

        //                    dbName = dtConf.Rows[0]["DBName"].ToString();

        //                    ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);

        //                    List<Dictionary<string, object>> MasterItems = SAPB1.GetStockBalance(dr["store_matcode"].ToString(), dr["wh"].ToString(), out SAPStatusCode, out SAPErrorMessage);

        //                    foreach (Dictionary<string, object> MasterItem in MasterItems)
        //                    {
        //                        myCommand.CommandText = $@"update store set 
        //                                                            store_total = @store_total
        //                                          where store_matcode = '{MasterItem["ItemCode"].ToString()}' and  wh = '{MasterItem["WhsCode"].ToString()}'";
        //                        myCommand.Parameters.AddWithValue("@store_toal", MasterItem["OnHand"].ToString());
        //                        myCommand.ExecuteNonQuery();

        //                    }
        //                }
        //                else
        //                {
        //                    throw new Exception("company_value not found!!!!");
        //                }
        //            }

        //        }
        //        //myTrans.Rollback();
        //        //myTrans.Commit();

        //        return new { status = true, message = "success", data = "" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new { status = false, message = "error", Result = ex.Message + " " + ex.StackTrace };
        //    }
        //    finally { conn.Close(); }
        //}
        #endregion

        #region Interface Transection

        /// <summary>
        /// CM Create Goods Receipt PO
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/creategoodsreceiptpo")]
        [AllowAnonymous]
        public object CMCreateGoodsReceiptPO(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            string tableName = "grch";
            if (!string.IsNullOrEmpty((string)Data.po_reccode))
            {
                refid = (string)Data.po_reccode;
            }
            else
            {
                //throw new Exception( "po_reccode is require" );
                return new { status = false, message = "error", result = "po_reccode is require" };
            }

            string doctype = "i";
            if (!string.IsNullOrEmpty((string)Data.doctype))
            {
                doctype = (string)Data.doctype.ToLowerCase();
            }


            try
            {

                string sql = GetSqlCMHeader(refid);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {

                            try
                            {
                                string DocNum = CreateCMGoodReceiptPO(new
                                {
                                    po_reccode = dr["po_reccode"].ToString(),
                                    vendercode = dr["vender_code"].ToString(),
                                    //vendername = dr["vendername"].ToString(),
                                    taxno = dr["taxno"].ToString(),
                                    po_recdate = dr["po_recdate"],
                                    duedate = dr["duedate"],
                                    doctype = doctype,
                                    module = "CMCreateGoodsReceiptPO",
                                    methodName = "CMCreateGoodsReceiptPO",
                                    refDescription = "grch.po_reccode",
                                    tableName = tableName,
                                    vatCode = dr["vatcode"].ToString(),
                                    isService = false,
                                    project_code = dr["project_code"].ToString(),
                                    refsap_return = dr["refsap_return"].ToString(),
                                    icon_deliveryNo = dr["docode"].ToString(),
                                    u_icon_cmpo = dr["po_pono"].ToString(),
                                    u_icon_cmwo = dr["wo"].ToString(),
                                    sub_remark = "",
                                    discount = "",
                                    acccode = "",
                                    company_value = dr["company_value"].ToString(),
                                });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["po_reccode"].ToString() + " : " + ex.Message);
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Cancel Goods Receipt PO
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/cancelgoodreceiptpo")]
        [AllowAnonymous]
        public object CMCancelGoodsReceiptPO(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.po_reccode))
            {
                refid = (string)Data.po_reccode;
            }
            else
            {
                return new { status = false, message = "error", result = "po_reccode is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid);
                MySqlDataAdapter da = new MySqlDataAdapter();
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            try
                            {
                                string DocNum = CancelCMGoodReceiptPO(new
                                {
                                    po_reccode = dr["po_reccode"].ToString(),
                                    canceldate = dr["po_canceldate"] == null ? DateTime.Now : dr["po_canceldate"],
                                    methodName = "CancelGoodReceiptPOCM",
                                    project_code = dr["project_code"].ToString(),
                                    sap_receive_code = dr["sap_receive_code"].ToString(),
                                    refDescription = "sc_h.po_reccode",
                                    isService = false,
                                    company_value = dr["company_value"].ToString(),
                                });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["po_reccode"].ToString() + " : " + ex.Message);
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { }


        }

        /// <summary>
        /// CM Create Service Goods Receipt PO
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/service/creategoodsreceiptpo")]
        [AllowAnonymous]
        public object CMCreateGoodsReceiptPO_Service(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            string tableName = "sc_h";
            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.po_reccode))
            {
                refid = (string)Data.po_reccode;
            }
            else
            {
                //throw new Exception( "po_reccode is require" );
                return new { status = false, message = "error", result = "po_reccode is require" };
            }

            string doctype = "s";
            try
            {

                string sql = GetSqlCMHeader(refid, tableName);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {

                            try
                            {
                                string DocNum = CreateCMGoodReceiptPO(new
                                {
                                    po_reccode = dr["po_reccode"].ToString(),
                                    vendercode = dr["vender_code"].ToString(),
                                    taxno = dr["taxno"].ToString(),
                                    po_recdate = dr["po_recdate"],
                                    duedate = dr["duedate"],
                                    doctype = doctype,
                                    module = "CMCreateGoodsReceiptPO_Service",
                                    methodName = "CMCreateGoodsReceiptPO_Service",
                                    refDescription = "sc_h.po_reccode",
                                    tableName = tableName,
                                    vatCode = dr["vatcode"].ToString(),
                                    isService = true,
                                    project_code = dr["project_code"].ToString(),
                                    refsap_return = "",
                                    icon_deliveryNo = "",
                                    u_icon_cmpo = "",
                                    u_icon_cmwo = dr["rt_doccode"].ToString(),
                                    sub_remark = dr["sub_remark"].ToString(),
                                    discount = dr["discount"].ToString(),
                                    acccode = dr["acccode"].ToString(),
                                    company_value = dr["company_value"].ToString(),
                                });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["po_reccode"].ToString() + " : " + ex.Message);
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Cancel Service Goods Receipt PO
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/service/cancelgoodsreceiptpo")]
        [AllowAnonymous]
        public object CMCancelGoodsReceiptPO_Service(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.po_reccode))
            {
                refid = (string)Data.po_reccode;
            }
            else
            {
                return new { status = false, message = "error", result = "po_reccode is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "sc_h");
                MySqlDataAdapter da = new MySqlDataAdapter();
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            try
                            {
                                string DocNum = CancelCMGoodReceiptPO(new
                                {
                                    po_reccode = dr["po_reccode"].ToString(),
                                    canceldate = dr["po_canceldate"].ToString() == null ? DateTime.Now : dr["po_canceldate"],
                                    methodName = "CMCancelGoodsReceiptPO_Service",
                                    refDescription = "sc_h.po_reccode",
                                    project_code = dr["project_code"].ToString(),
                                    isService = true,
                                    sap_receive_code = dr["sap_receive_code"].ToString(),
                                    company_value = dr["company_value"].ToString(),
                                });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["po_reccode"].ToString() + " : " + ex.Message);
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { }


        }

        /// <summary>
        /// CM Create Goods Receipt
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/creategoodsreceipt")]
        [AllowAnonymous]
        public object CMCreateGoodsReceipt(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            string tableName = "grch";
            if (!string.IsNullOrEmpty((string)Data.po_reccode))
            {
                refid = (string)Data.po_reccode;
            }
            else
            {
                //throw new Exception( "po_reccode is require" );
                return new { status = false, message = "error", result = "po_reccode is require" };
            }

            try
            {

                string sql = GetSqlCMHeader(refid);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dr in PostData.Rows)
                        {

                            try
                            {
                                string DocNum = CreateCMGoodsReceipt(new
                                {
                                    po_reccode = dr["po_reccode"].ToString(),
                                    vendercode = dr["vender_code"].ToString(),
                                    taxno = dr["taxno"].ToString(),
                                    po_recdate = dr["po_recdate"],
                                    duedate = dr["duedate"],
                                    taxdate = dr["taxdate"],
                                    module = "CMCreateGoodsReceipt",
                                    methodName = "CMCreateGoodsReceipt",
                                    refDescription = "grch.po_reccode",
                                    tableName = tableName,
                                    vatCode = dr["vatcode"].ToString(),
                                    isService = false,
                                    createuser = dr["createuser"].ToString(),
                                    project_code = dr["project_code"].ToString(),
                                    remark = dr["remark"].ToString(),
                                    acccode = dr["acccode"].ToString(),
                                    company_value = dr["company_value"].ToString(),
                                });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["po_reccode"].ToString() + " : " + ex.Message);
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Cancel Goods Receipt
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/cancelgoodsreceipt")]
        [AllowAnonymous]
        public object CMCancelGoodsReceipt(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSuccess = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.po_reccode))
            {
                refid = (string)Data.po_reccode;
            }
            else
            {
                return new { status = false, message = "error", result = "po_reccode is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid);
                MySqlDataAdapter da = new MySqlDataAdapter();
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {
                    try
                    {
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            try
                            {
                                string DocNum = CancelCMGoodReceipt(new
                                {
                                    po_reccode = dr["po_reccode"].ToString(),
                                    canceldate = dr["po_canceldate"].ToString() == null ? DateTime.Now : dr["po_canceldate"],
                                    project_code = dr["project_code"].ToString(),
                                    sap_receive_code = dr["sap_receive_code"].ToString(),
                                    methodName = "CMCancelGoodsReceipt",
                                    refDescription = "sc_h.po_reccode",
                                    company_value = dr["company_value"].ToString(),
                                });
                                ResultSuccess.Add(DocNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["po_reccode"].ToString() + " : " + ex.Message);
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { }


        }

        /// <summary>
        /// CM Goods Issue
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/creategoodsissue")]
        [AllowAnonymous]
        public object CMCreateGoodsIssue(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSucces = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            string tableName = "gih";
            string refName = "is_doccode";
            if (!string.IsNullOrEmpty((string)Data.is_doccode))
            {
                refid = (string)Data.is_doccode;
            }
            else
            {
                //throw new Exception( "po_reccode is require" );
                return new { status = false, message = "error", result = "is_doccode is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, tableName, refName);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            try
                            {
                                string docNum = CreateGoodsIssueCM(new
                                {
                                    is_doccode = dr["is_doccode"].ToString(),
                                    is_reqname = dr["is_reqname"].ToString(),
                                    remark = dr["remark"].ToString(),
                                    acccode = dr["acccode"].ToString(),
                                    project_code = dr["projectcode"].ToString(),
                                    company_value = dr["company_value"].ToString(),
                                    is_place = dr["is_place"].ToString(),
                                });

                                ResultSucces.Add(docNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["is_doccode"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = ResultSucces, error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Goods Return
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/creategoodsreturn")]
        [AllowAnonymous]
        public object CMCreateGoodsReturn(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSucces = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.po_reccode))
            {
                refid = (string)Data.po_reccode;
            }
            else
            {
                //throw new Exception( "po_reccode is require" );
                return new { status = false, message = "error", result = "po_reccode is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "grth");
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            try
                            {
                                string docNum = CreateGoodsReturnCM(new
                                {
                                    po_reccode = dr["po_reccode"].ToString(),
                                    vendercode = dr["vender_code"].ToString(),
                                    taxno = dr["taxno"].ToString(),
                                    po_recdate = dr["po_recdate"],
                                    duedate = dr["duedate"],
                                    vatCode = dr["vatcode"].ToString(),
                                    project_code = dr["project_code"].ToString(),
                                    company_value = dr["company_value"].ToString(),
                                });

                                ResultSucces.Add(docNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["po_reccode"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = ResultSucces, error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Cancel Goods Return
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/cancelgoodsreturn")]
        [AllowAnonymous]
        public object CMCancelGoodsReturn(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSucces = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.po_reccode))
            {
                refid = (string)Data.po_reccode;
            }
            else
            {
                //throw new Exception( "po_reccode is require" );
                return new { status = false, message = "error", result = "po_reccode is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "grth");
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            try
                            {
                                string docNum = CancelGoodsReturnCM(new
                                {
                                    po_reccode = dr["po_reccode"].ToString(),
                                    canceldate = dr["po_canceldate"] == null ? DateTime.Now : dr["po_canceldate"],
                                    project_code = dr["project_code"].ToString(),
                                    sap_return_code = dr["sap_return_code"].ToString(),
                                    company_value = dr["company_value"].ToString(),
                                });

                                ResultSucces.Add(docNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["po_reccode"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = ResultSucces, error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }

        }

        /// <summary>
        /// CM Create Journal Voucher
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createjournalvoucher")]
        [AllowAnonymous]
        public object CMCreateJournalVoucher(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSucces = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.docno))
            {
                refid = (string)Data.docno;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "docno is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "pcadvh", "docno");
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            try
                            {
                                string docNum = CreateJournalVoucher(new
                                {
                                    docno = dr["docno"].ToString(),
                                    docdate = dr["docdate"] == null ? DateTime.Now : dr["docdate"],
                                    //project_code = dr["project_code"].ToString(),
                                    vender_name = dr["vender_name"].ToString(),
                                    vender_address = dr["vender_address"].ToString(),
                                    vender_tax = dr["vender_tax"].ToString(),
                                    petty_cash_cardcode = dr["bd_code"].ToString(),
                                    petty_cash_amt = dr["bpamount"].ToString(),
                                    memo = dr["description"].ToString(),
                                    //u_icon_pt = dr["phase_name"].ToString(),
                                    //u_icon_bf = dr["block_name"].ToString(),
                                    company_value = dr["company_value"].ToString(),
                                });

                                ResultSucces.Add(docNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["docno"].ToString() + " : " + ex.Message);
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = ResultSucces, error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Send GV Allocate
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/sendgvallocate")]
        [AllowAnonymous]
        public object CMSendGVAllocate(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSucces = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.Transld))
            {
                refid = (string)Data.Transld;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "Transld is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "pas_h", "Transld");
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            try
                            {
                                string docNum = SendGVAllocate(new
                                {
                                    Transld = dr["Transld"].ToString(),
                                    RefDate = dr["RefDate"] == null ? DateTime.Now : dr["RefDate"],
                                    DueDate = dr["DueDate"] == null ? DateTime.Now : dr["DueDate"],
                                    TaxDate = dr["TaxDate"] == null ? DateTime.Now : dr["TaxDate"],
                                    Project = dr["Project"].ToString(),
                                    Remarks = dr["Remarks"].ToString(),
                                    Icon_ref = dr["Icon_ref"].ToString(),
                                    StornoDate = dr["StornoDate"] == null ? "" : dr["StornoDate"],
                                    company_value = dr["company_value"].ToString(),
                                });

                                ResultSucces.Add(docNum);
                            }
                            catch (Exception ex)
                            {
                                ResultError.Add(dr["Transld"].ToString() + " : " + ex.Message + " " + ex.StackTrace);
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = ResultSucces, error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }
        #endregion

        #region Interface Master

        /// <summary>
        /// CM Create Update Cost Center
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatechartofaccounts")]
        [AllowAnonymous]
        public object CMCreateUpdateChartofAccounts(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultSucces = new List<string>();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.act_code))
            {
                refid = (string)Data.act_code;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "act_code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "acc_code", "act_code", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;

                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateChartofAccounts(new
                                    {
                                        act_code = dr["act_code"].ToString(),
                                        act_name = dr["act_name"].ToString(),
                                        act_name_eng = dr["act_name_eng"].ToString(),
                                        act_status = dr["act_status"].ToString(),
                                        accntntcod = dr["accntntcod"].ToString(),
                                        act_header = dr["act_header"].ToString(),
                                        act_curr = dr["act_curr"].ToString(),
                                        father_num = dr["father_num"].ToString(),
                                        locmantran = dr["locmantran"].ToString(),
                                        finanse = dr["finanse"].ToString(),
                                        cfw_rlvnt = dr["cfw_rlvnt"].ToString(),
                                        proj_relvnt = dr["proj_relvnt"].ToString(),
                                        dim1_relvnt = dr["dim1_relvnt"].ToString(),
                                        dim2_relvnt = dr["dim2_relvnt"].ToString(),
                                        dim3_relvnt = dr["dim3_relvnt"].ToString(),
                                        dim4_relvnt = dr["dim4_relvnt"].ToString(),
                                        dim5_relvnt = dr["dim5_relvnt"].ToString(),
                                        blocmanpos = dr["blocmanpos"].ToString(),
                                        valid_for = dr["valid_for"].ToString(),
                                        frozen_for = dr["frozen_for"].ToString(),
                                        company_value = dr["company_value"].ToString(),
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["act_code"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }


                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Cost Center
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatecostcenter")]
        [AllowAnonymous]
        public object CMCreateUpdateCostCenter(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.cgroup_code))
            {
                refid = (string)Data.cgroup_code;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "cgroup code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "costcode", "cgroup_code", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateCostCenter(new
                                    {
                                        cgroup_code = dr["cgroup_code"].ToString(),
                                        cgroup_name = dr["cgroup_name"].ToString(),
                                        costcode_d = dr["costcode_d"].ToString(),
                                        grp_code = dr["grp_code"].ToString(),
                                        valid_from = string.IsNullOrEmpty(dr["valid_from"].ToString()) || dr["valid_from"].ToString() == "0/0/0000" ? DateTime.Now : dr["valid_from"],
                                        valid_to = string.IsNullOrEmpty(dr["valid_to"].ToString()) || dr["valid_to"].ToString() == "0/0/0000" ? "" : dr["valid_to"],
                                        locked = dr["locked"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["cgroup_code"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }

                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Project
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdateproject")]
        [AllowAnonymous]
        public object CMCreateUpdateProject(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.project_code))
            {
                refid = (string)Data.project_code;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "project code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "pj", "project_code", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateProject(new
                                    {
                                        project_code = dr["project_code"].ToString(),
                                        project_name = dr["project_name"].ToString(),
                                        //project_start = string.IsNullOrEmpty(dr["project_start"].ToString()) || dr["project_start"].ToString() == "0/0/0000" ? DateTime.Now : dr["project_start"],
                                        //project_stop = string.IsNullOrEmpty(dr["project_stop"].ToString()) || dr["project_stop"].ToString() == "0/0/0000" ? "" : dr["project_stop"],
                                        project_status = dr["project_status"].ToString(),
                                        company_value = company_value,

                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["project_code"].ToString() + " : " + ex.Message);
                                }

                                cnt++;
                            }

                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Vat Groups
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatevatgroups")]
        [AllowAnonymous]
        public object CMCreateUpdateVatGroups(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.code_vat))
            {
                refid = (string)Data.code_vat;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "code_vat is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "vat", "code_vat", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateVatGroups(new
                                    {
                                        code_vat = dr["code_vat"].ToString(),
                                        vat_name = dr["vat_name"].ToString(),
                                        category = dr["category"].ToString(),
                                        account = dr["account"].ToString(),
                                        deferracc = dr["deferracc"].ToString(),
                                        m_vat = dr["m_vat"].ToString(),
                                        status = dr["status"].ToString(),
                                        effec_date = string.IsNullOrEmpty(dr["effec_date"].ToString()) || dr["effec_date"].ToString() == "0/0/0000" ? "" : dr["effec_date"].ToString(),
                                        rate = dr["rate"].ToString(),
                                        company_value = company_value,

                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["code_vat"].ToString() + " : " + ex.Message);
                                }

                                cnt++;
                            }

                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Withholding Tax
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatewithholdingtax")]
        [AllowAnonymous]
        public object CMCreateUpdateWithholdingTax(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.WTax_Code))
            {
                refid = (string)Data.WTax_Code;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "WTax_Code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "m_wt", "WTax_Code", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateWithholdingTax(new
                                    {
                                        WTax_Code = dr["WTax_Code"].ToString(),
                                        name_wt = dr["name_wt"].ToString(),
                                        category = dr["category"].ToString(),
                                        Base_Type = dr["Base_Type"].ToString(),
                                        Base_Amount = dr["Base_Amount"].ToString(),
                                        offclcode = dr["offclcode"].ToString(),
                                        Account = dr["Account"].ToString(),
                                        Inactive = dr["Inactive"].ToString(),
                                        effec_date = string.IsNullOrEmpty(dr["effec_date"].ToString()) || dr["effec_date"].ToString() == "0/0/0000" ? "" : dr["effec_date"].ToString(),
                                        rate = dr["st_rate"].ToString(),
                                        company_value = company_value,

                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["WTax_Code"].ToString() + " : " + ex.Message);
                                }

                                cnt++;
                            }

                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Payment Terms
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatepaymentterms")]
        [AllowAnonymous]
        public object CMCreateUpdatePaymentTerms(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.group_num))
            {
                refid = (string)Data.group_num;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "group_num is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "payM", "group_num", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdatePaymentTerms(new
                                    {
                                        group_num = dr["group_num"].ToString(),
                                        pymnt_group = dr["pymnt_group"].ToString(),
                                        bsline_date = dr["bsline_date"].ToString(),
                                        pay_du_month = dr["pay_du_month"].ToString(),
                                        extra_month = dr["extra_month"],
                                        extra_days = dr["extra_days"],
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["group_num"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Warehouse
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatewarehouse")]
        [AllowAnonymous]
        public object CMCreateUpdateWarehouse(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.whcode))
            {
                refid = (string)Data.whcode;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "warehouse code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "wh", "whcode", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateWarehouse(new
                                    {
                                        whcode = dr["whcode"].ToString(),
                                        whname = dr["whname"].ToString(),
                                        type_store = dr["type_store"].ToString(),
                                        inactive = dr["inactive"].ToString(),
                                        block = dr["block"].ToString(),
                                        zip_code = dr["zip_code"].ToString(),
                                        city = dr["city"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["whcode"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Item Master
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdateitemmaster")]
        [AllowAnonymous]
        public object CMCreateUpdateItemMaster(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.materialcode))
            {
                refid = (string)Data.materialcode;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "material code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "matunit ", "materialcode", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateItemMaster(new
                                    {
                                        series = dr["series"].ToString(),
                                        materialcode = dr["materialcode"].ToString(),
                                        materialname = dr["materialname"].ToString(),
                                        materialname_en = dr["materialname_en"].ToString(),
                                        mat_type = dr["mat_type"].ToString(),
                                        matgroup_code = dr["matgroup_code"].ToString(),
                                        user_text = dr["user_text"].ToString(),
                                        wt_liable = dr["wt_liable"].ToString(),
                                        prchse_item = dr["prchse_item"].ToString(),
                                        sell_item = dr["sell_item"].ToString(),
                                        invnt_item = dr["invnt_item"].ToString(),
                                        man_ser_num = dr["man_ser_num"].ToString(),
                                        man_btch_num = dr["man_btch_num"].ToString(),
                                        mng_method = dr["mng_method"].ToString(),
                                        valid_for = dr["valid_for"].ToString(),
                                        frozen_for = dr["frozen_for"].ToString(),
                                        card_code = dr["card_code"].ToString(),
                                        supp_cat_num = dr["supp_cat_num"].ToString(),
                                        buy_unit_msr = dr["buy_unit_msr"].ToString(),
                                        num_in_buy = dr["num_in_buy"].ToString(),
                                        pur_pack_msr = dr["pur_pack_msr"].ToString(),
                                        pur_packun = dr["pur_packun"].ToString(),
                                        vat_gourp_pu = dr["vat_gourp_pu"].ToString(),
                                        sales_tax = dr["sales_tax"].ToString(),
                                        sales_uom = dr["sales_uom"].ToString(),
                                        num_sale = dr["num_sale"].ToString(),
                                        sales_pack = dr["sales_pack"].ToString(),
                                        qty_pack = dr["qty_pack"].ToString(),
                                        gl_method = dr["gl_method"].ToString(),
                                        invntry_uom = dr["invntry_uom"].ToString(),
                                        ic_weight = dr["ic_weight"].ToString(),
                                        by_wh = dr["by_wh"].ToString(),
                                        eval_system = dr["eval_system"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["materialcode"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Item Groups
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdateitemgroups")]
        [AllowAnonymous]
        public object CMCreateUpdateItemGroups(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.matgroup_code))
            {
                refid = (string)Data.matgroup_code;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "item groups is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "mat_gr", "matgroup_code", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        string docNum = string.Empty;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    docNum = CreateUpdateItemGroups(new
                                    {
                                        matgroup_code = dr["matgroup_code"].ToString(),
                                        matgroup_name = dr["matgroup_name"].ToString(),
                                        ic_acct = dr["ic_acct"].ToString(),
                                        sale_cost_acct = dr["sale_cost_acct"].ToString(),
                                        transfer_acct = dr["transfer_acct"].ToString(),
                                        price_dif_acct = dr["price_dif_acct"].ToString(),
                                        sap_code = string.IsNullOrEmpty(dr["sap_code"].ToString()) ? "0" : dr["sap_code"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["matgroup_code"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = docNum, error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Business Partner
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatebusinesspartner")]
        [AllowAnonymous]
        public object CMCreateUpdateBusinessPartner(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.vender_code))
            {
                refid = (string)Data.vender_code;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "vender_code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "business", "vender_code", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateBusinessPartner(new
                                    {
                                        Series = (string)dr["series"].ToString(),
                                        vender_code = (string)dr["vender_code"].ToString(),
                                        vender_name = (string)dr["vender_name"].ToString(),
                                        cardfnme = (string)dr["cardfnme"].ToString(),
                                        vender_type = (string)dr["vender_type"].ToString(),
                                        cmpprivate = (string)dr["cmpprivate"].ToString(),
                                        groupcode = (string)dr["groupcode"].ToString(),
                                        currency = (string)dr["currency"].ToString(),
                                        vender_tax = (string)dr["vender_tax"].ToString(),
                                        remarks = (string)dr["remarks"].ToString(),
                                        vender_tel = (string)dr["vender_tel"].ToString(),
                                        vender_tel2 = (string)dr["vender_tel2"].ToString(),
                                        vender_fax = (string)dr["vender_fax"].ToString(),
                                        e_mail = (string)dr["e_mail"].ToString(),
                                        website = (string)dr["website"].ToString(),
                                        alias_name = (string)dr["alias_name"].ToString(),
                                        cntct_prsn = (string)dr["cntct_prsn"].ToString(),
                                        territory = (string)dr["territory"].ToString(),
                                        glbl_loc_num = (string)dr["glbl_loc_num"].ToString(),
                                        valid_for = (string)dr["valid_for"].ToString(),
                                        frozen_for = (string)dr["frozen_for"].ToString(),
                                        payment_id = (string)dr["payment_id"].ToString(),
                                        payment_discount = (string)dr["payment_discount"].ToString(),
                                        ar_payable = (string)dr["ar_payable"].ToString(),
                                        vat_status = (string)dr["vat_status"].ToString(),
                                        tax_group = (string)dr["tax_group"].ToString(),
                                        wt_liable = (string)dr["wt_liable"].ToString(),
                                        typwt_reprt = (string)dr["typwt_reprt"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["vender_code"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Contact Persons
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatecontactpersons")]
        [AllowAnonymous]
        public object CMCreateUpdateContactPersons(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.whcode))
            {
                refid = (string)Data.whcode;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "warehouse code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "wh", "whcode", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateWarehouse(new
                                    {
                                        whcode = dr["whcode"].ToString(),
                                        whname = dr["whname"].ToString(),
                                        type_store = dr["type_store"].ToString(),
                                        inactive = dr["inactive"].ToString(),
                                        block = dr["block"].ToString(),
                                        zip_code = dr["zip_code"].ToString(),
                                        city = dr["city"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["whcode"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Vender Address
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatebusinesspartneraddress")]
        [AllowAnonymous]
        public object CMCreateUpdateBusinessPartnerAdress(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.vender_code))
            {
                refid = (string)Data.vender_code;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "vender_code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "vender_a", "vender_code", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateBusinessAddress(new
                                    {
                                        vender_code = dr["vender_code"].ToString(),
                                        vender_address = dr["vender_address"].ToString(),
                                        address_type = dr["address_type"].ToString(),
                                        street = dr["street"].ToString(),
                                        block = dr["block"].ToString(),
                                        city = dr["city"].ToString(),
                                        county = dr["county"].ToString(),
                                        country = dr["country"].ToString(),
                                        zip_code = dr["zip_code"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["vender_code"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Business Partner Groups
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatebusinesspartnergroups")]
        [AllowAnonymous]
        public object CMCreateUpdateBusinessPartnerGroups(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.group_code))
            {
                refid = (string)Data.group_code;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "group_code code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "business_g", "group_code", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        string docNum = string.Empty;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    docNum = CreateUpdateBusinessPartnerGroups(new
                                    {
                                        group_code = dr["group_code"].ToString(),
                                        group_num = dr["group_num"].ToString(),
                                        group_type = dr["group_type"].ToString(),
                                        sap_code = string.IsNullOrEmpty(dr["sap_code"].ToString()) ? "0" : dr["sap_code"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["group_code"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = docNum, error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Business Bank
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatebusinessbank")]
        [AllowAnonymous]
        public object CMCreateUpdateBusinessBank(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.vender_code))
            {
                refid = (string)Data.vender_code;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "vender_code code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "business_b", "vender_code", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateBusinessBank(new
                                    {
                                        vender_code = dr["vender_code"].ToString(),
                                        bank_country = string.IsNullOrEmpty(dr["bank_country"].ToString()) ? "TH" : dr["bank_country"].ToString(),
                                        bank_code = dr["bank_code"].ToString(),
                                        acc_code = dr["acc_code"].ToString(),
                                        acc_name = dr["acc_name"].ToString(),
                                        branch_code = dr["branch_code"].ToString(),
                                        swift_code = dr["swift_code"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["vender_code"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        /// <summary>
        /// CM Create Update Vat Branch
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cm/createupdatevatbranch")]
        [AllowAnonymous]
        public object CMCreateUpdateVatBranch(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.whcode))
            {
                refid = (string)Data.whcode;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "warehouse code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "wh", "whcode", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateWarehouse(new
                                    {
                                        whcode = dr["whcode"].ToString(),
                                        whname = dr["whname"].ToString(),
                                        type_store = dr["type_store"].ToString(),
                                        inactive = dr["inactive"].ToString(),
                                        block = dr["block"].ToString(),
                                        zip_code = dr["zip_code"].ToString(),
                                        city = dr["city"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["whcode"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        [HttpPost]
        [Route("api/cm/createupdatebanks")]
        [AllowAnonymous]
        public object CMCreateUpdateBanks(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.bank_code))
            {
                refid = (string)Data.bank_code;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "bank code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "s_bank", "bank_code", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateBanks(new
                                    {
                                        bank_code = dr["bank_code"].ToString(),
                                        bank_name = dr["bank_name"].ToString(),
                                        bank_country = dr["bank_country"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["bank_code"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }

        [HttpPost]
        [Route("api/cm/createupdateHouseBankAccounts")]
        [AllowAnonymous]
        public object CMCreateUpdateHouseBankAccounts(dynamic Data)
        {
            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();
            List<string> ResultError = new List<string>();

            string refid = "";
            if (!string.IsNullOrEmpty((string)Data.whcode))
            {
                refid = (string)Data.whcode;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "warehouse code is require" };
            }
            string company_value = "";
            if (!string.IsNullOrEmpty((string)Data.company_value))
            {
                company_value = (string)Data.company_value;
            }
            else
            {
                //throw new Exception( "docno is require" );
                return new { status = false, message = "error", result = "company_value is require" };
            }

            try
            {
                string sql = GetSqlCMHeader(refid, "wh", "whcode", company_value);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable PostData = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(PostData);

                if (PostData.Rows.Count > 0)
                {

                    try
                    {
                        var cnt = 0;
                        foreach (System.Data.DataRow dr in PostData.Rows)
                        {
                            if (cnt == 0)
                            {
                                try
                                {
                                    string docNum = CreateUpdateWarehouse(new
                                    {
                                        whcode = dr["whcode"].ToString(),
                                        whname = dr["whname"].ToString(),
                                        type_store = dr["type_store"].ToString(),
                                        inactive = dr["inactive"].ToString(),
                                        block = dr["block"].ToString(),
                                        zip_code = dr["zip_code"].ToString(),
                                        city = dr["city"].ToString(),
                                        company_value = company_value
                                    });

                                }
                                catch (Exception ex)
                                {
                                    ResultError.Add(dr["whcode"].ToString() + " : " + ex.Message);
                                }
                                cnt++;
                            }
                        }

                        return new { status = true, message = "success", result = new { docNum = "", error = ResultError } };
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
                return new { status = false, message = "error", result = ex.Message };
            }
            finally { conn.Dispose(); }
        }
        #endregion

        #region #### Interfacee CM To SAP ####

        private string GetSqlCMHeader(string RefID, string TableName = "grch", string RefName = "po_reccode", string CompanyValue = "")
        {
            string sql = $@"
SELECT * FROM {TableName}";

            if (!string.IsNullOrEmpty(RefID))
            {
                sql += $" where {RefName} = '{RefID}'";
            }
            if (!string.IsNullOrEmpty(CompanyValue))
            {
                sql += $"and company_value = '{CompanyValue}'";
            }

            return sql;
        }
        private string GetSqlCMDetail(string RefID, string TableName = "grcd", string RefName = "poi_ref")
        {
            string sql = $@"
SELECT * FROM {TableName}";

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
inner join sys_master_projects pj on re.CompanyID = pj.CompanyID and re.ProjectID = pj.ProjectID
where keyname = 'dbname'
and re.projectid = '{ProjectID}'";

            return SQL;
        }
        private string GetSqlCMDBConnect(string ProjectID)
        {
            string SQL = $@"
select
	company_value DBName
from company com 
inner join project pj on com.compcode = pj.compcode
where pj.project_code = '{ProjectID}'";

            return SQL;
        }
        private string GetSqlCMStoreDBConnect()
        {
            string SQL = $@"
select
	com.company_value DBName
from company com 
inner join project pj on com.compcode = pj.compcode
where pj.project_code in ((SELECT wh FROM store
group by wh)
)
group by DBName";

            return SQL;
        }
        private string GetSqlCMGetDBSAP()
        {
            string SQL = $@"
select
	company_value DBName
from company 
where (company_value is not null or company_value <> '')
order by company_value";

            return SQL;
        }

        #region ### Goods Receipt PO ####
        /****create****/
        private string CreateCMGoodReceiptPO(dynamic Data)
        {
            string po_reccode = (string)Data.po_reccode;
            string methodName = "CMCreateGoodsReceiptPO";
            string refDescription = "grch.po_reccode";
            string vatCode = "NIG";
            bool isService = false;
            bool isDeferVat = true;
            string project_code = "";
            string dbName = "";
            string icon_deliveryNo = "";
            string u_icon_cmpo = "";
            string u_icon_cmwo = "";
            if (!string.IsNullOrEmpty((string)Data.methodName))
            {
                methodName = (string)Data.methodName;
            }
            if (!string.IsNullOrEmpty((string)Data.refDescription))
            {
                refDescription = (string)Data.refDescription;
            }
            if (!string.IsNullOrEmpty((string)Data.vatCode))
            {
                vatCode = (string)Data.vatCode;
            }
            if (Data.isService != null)
            {
                isService = (bool)Data.isService;
            }
            if (!string.IsNullOrEmpty((string)Data.project_code))
            {
                project_code = (string)Data.project_code;
            }
            if (!string.IsNullOrEmpty((string)Data.icon_deliveryNo))
            {
                icon_deliveryNo = (string)Data.icon_deliveryNo;
            }
            if (!string.IsNullOrEmpty((string)Data.u_icon_cmpo))
            {
                u_icon_cmpo = (string)Data.u_icon_cmpo;
            }
            if (!string.IsNullOrEmpty((string)Data.u_icon_cmwo))
            {
                u_icon_cmwo = (string)Data.u_icon_cmwo;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

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
                string sql = GetSqlCMDetail(po_reccode, isService ? "sc_d" : "grcd");
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable dt = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);

                //string SQL = GetSqlCMDBConnect(project_code);
                //MySqlDataAdapter daConf = default(MySqlDataAdapter);
                //System.Data.DataTable dtConf = new System.Data.DataTable();
                //daConf = new MySqlDataAdapter(SQL, conn);
                //daConf.Fill(dtConf);

                //if (dtConf.Rows.Count > 0)
                if (!string.IsNullOrEmpty((string)Data.company_value))
                {
                    //dbName = dtConf.Rows[0]["DBName"].ToString();
                    dbName = (string)Data.company_value;
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    CardCode = (string)Data.vendercode,
                    NumAtCard = (string)Data.taxno,    // Vendor Ref. No.
                    PostingDate = (DateTime)Data.po_recdate,
                    DocDueDate = (DateTime)Data.po_recdate,
                    DocumentDate = (DateTime)Data.po_recdate,
                    DocType = SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes,
                    UDF_RefNo = po_reccode,
                    UDF_VatPeriodDate = (DateTime)Data.duedate,
                    ICON_DeliveryNo = icon_deliveryNo,
                    U_ICON_CMPO = u_icon_cmpo,
                    U_ICON_CMWO = u_icon_cmwo,
                    U_ICON_IssueReason = (string)Data.acccode,
                    //UDF_CustName = dt.Rows[0]["CustomerName"].ToString(),
                    //UDF_UnitRef = dt.Rows[0]["UnitNumber"].ToString(),
                    //UDF_Project = dt.Rows[0]["ProjectID"].ToString(),
                    //UDF_ProjectName = dt.Rows[0]["ProjectName"].ToString(),
                    //UDF_TaxID = dt.Rows[0]["TaxID"].ToString(),
                    //UDF_Address = dt.Rows[0]["Address"].ToString(),
                    //IsDeferVAT = Convert.ToBoolean(dt.Rows[0]["IsDeferVAT"])
                    Remark = (string)Data.sub_remark,
                    DocTotal = (string)Data.discount,
                };

                foreach (DataRow dr in dt.Rows)
                {
                    string itemName = dr["poi_matname"].ToString();
                    if (isService)
                    {
                        //ICON.SAP.API.SAPTABLE Items = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oItems));
                        //itemName += " " + SAPB1.GetDataColumn("ItemName", Items, "ItemCode", dr["poi_matcode"].ToString());
                        //check item that service or not
                        if (string.IsNullOrEmpty(vatCode))
                        {
                            if (string.IsNullOrEmpty(dr["poi_vat"].ToString()) || Convert.ToDecimal(dr["poi_vat"].ToString()) == 0)
                            {
                                vatCode = "NIG";
                            }
                            else
                            {
                                isDeferVat = isService ? true : false; //ถ้าไอเทมเป็นบริการจะกำหนดให้ defervat = 'Y'
                                vatCode = "IG";
                            }
                        }

                        Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                        {
                            ItemCode = dr["poi_matcode"].ToString(),
                            ItemDescription = itemName,
                            Qty = Convert.ToDouble(dr["poi_qty"]),
                            TaxCode = vatCode,
                            UnitPrice =dr["poi_priceunit"].ToString(),
                            WhsCode = dr["ic_warehouse"].ToString(),
                            IsDeferVAT = isDeferVat,
                            VatSum = dr["poi_vat"].ToString(),
                            ProjectCode = string.IsNullOrEmpty(project_code) ? dr["ic_warehouse"].ToString() : project_code,
                            OcrCode = string.IsNullOrEmpty(dr["ocrcode"].ToString()) ? "" : dr["ocrcode"].ToString(),
                            OcrCode2 = string.IsNullOrEmpty(dr["ocrcode2"].ToString()) ? "" : dr["ocrcode2"].ToString(),
                            OcrCode3 = string.IsNullOrEmpty(dr["ocrcode3"].ToString()) ? "" : dr["ocrcode3"].ToString(),
                            OcrCode4 = string.IsNullOrEmpty(dr["ocrcode4"].ToString()) ? "" : dr["ocrcode4"].ToString(),
                            OcrCode5 = string.IsNullOrEmpty(dr["ocrcode5"].ToString()) ? "" : dr["ocrcode5"].ToString(),
                            BaseEntry = string.IsNullOrEmpty((string)Data.refsap_return) ? "" : (string)Data.refsap_return,
                            BaseType = Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oPurchaseReturns),
                            AccountCode = string.IsNullOrEmpty(dr["account_code"].ToString()) ? "" : dr["account_code"].ToString(),
                            //LineNumber = string.IsNullOrEmpty(dr["id_sap"].ToString()) ? "" : dr["id_sap"].ToString(),
                            U_ICON_IssueReason = dr["acccode"].ToString(),
                            U_ICON_PT = dr["phase_name"].ToString(),
                            U_ICON_BF = dr["block_name"].ToString(),
                            LineTotal = dr["LineTotal"].ToString(),
                            U_ICON_PRNumber = string.IsNullOrEmpty(dr["pr_number"].ToString()) ? "" : dr["pr_number"].ToString(),
                        });
                    }
                    else
                    {
                        //check item that service or not
                        if (string.IsNullOrEmpty(vatCode))
                        {
                            if (string.IsNullOrEmpty(dr["poi_vat"].ToString()) || Convert.ToDecimal(dr["poi_vat"].ToString()) == 0)
                            {
                                vatCode = "NIG";
                            }
                            else
                            {
                                isDeferVat = isService ? true : false; //ถ้าไอเทมเป็นบริการจะกำหนดให้ defervat = 'Y'
                                vatCode = "IG";
                            }
                        }


                        Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                        {
                            ItemCode = dr["poi_matcode"].ToString(),
                            ItemDescription = dr["poi_remark"].ToString(),
                            Qty = Convert.ToDouble(dr["poi_qty"]),
                            TaxCode = vatCode,
                            UnitPrice = dr["poi_priceunit"].ToString(),
                            WhsCode = dr["ic_warehouse"].ToString(),
                            IsDeferVAT = isDeferVat,
                            VatSum = dr["poi_vat"].ToString(),
                            //ProjectCode = string.IsNullOrEmpty(project_code) ? dr["project_code"].ToString() : project_code,
                            ProjectCode = dr["project_code"].ToString(),
                            OcrCode = string.IsNullOrEmpty(dr["ocrcode"].ToString()) ? "" : dr["ocrcode"].ToString(),
                            OcrCode2 = string.IsNullOrEmpty(dr["ocrcode2"].ToString()) ? "" : dr["ocrcode2"].ToString(),
                            OcrCode3 = string.IsNullOrEmpty(dr["ocrcode3"].ToString()) ? "" : dr["ocrcode3"].ToString(),
                            OcrCode4 = string.IsNullOrEmpty(dr["ocrcode4"].ToString()) ? "" : dr["ocrcode4"].ToString(),
                            OcrCode5 = string.IsNullOrEmpty(dr["ocrcode5"].ToString()) ? "" : dr["ocrcode5"].ToString(),
                            BaseEntry = string.IsNullOrEmpty((string)Data.refsap_return) ? "" : (string)Data.refsap_return,
                            BaseType = Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oPurchaseReturns),
                            LineNumber = string.IsNullOrEmpty(dr["id_sap"].ToString()) ? "" : dr["id_sap"].ToString(),
                            U_ICON_PT = dr["phase_name"].ToString(),
                            U_ICON_BF = dr["block_name"].ToString(),
                            LineTotal = dr["LineTotal"].ToString(),
                            U_ICON_PRNumber = string.IsNullOrEmpty(dr["pr_number"].ToString()) ? "" : dr["pr_number"].ToString(),
                        });
                    }
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
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     po_reccode,
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
        private string CancelCMGoodReceiptPO(dynamic Data)
        {
            string po_reccode = (string)Data.po_reccode;
            string methodName = "CancelGoodReceiptPOCM";
            string refDescription = "grch.po_reccode";
            string project_code = "";
            string dbName = "";
            string sap_receive_code = "";
            bool isService = false;

            if (!string.IsNullOrEmpty((string)Data.methodName))
            {
                methodName = (string)Data.methodName;
            }
            if (!string.IsNullOrEmpty((string)Data.refDescription))
            {
                refDescription = (string)Data.refDescription;
            }
            if (!string.IsNullOrEmpty((string)Data.project_code))
            {
                project_code = (string)Data.project_code;
            }
            if (Data.isService != null)
            {
                isService = (bool)Data.isService;
            }
            if (!string.IsNullOrEmpty((string)Data.sap_receive_code))
            {
                sap_receive_code = (string)Data.sap_receive_code;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

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
                //                string sql = $@"select * from sap_interface_log where remrefid = '{po_reccode}'
                //and methodname like '%create%'";
                //                DataTable dt = dbHelp.ExecuteDataTable(sql);

                //string SQL = GetSqlCMDBConnect(project_code);
                //MySqlDataAdapter daConf = default(MySqlDataAdapter);
                //System.Data.DataTable dtConf = new System.Data.DataTable();
                //daConf = new MySqlDataAdapter(SQL, conn);
                //daConf.Fill(dtConf);

                //if (dtConf.Rows.Count > 0)
                if (!string.IsNullOrEmpty((string)Data.company_value))
                {
                    //dbName = dtConf.Rows[0]["DBName"].ToString();
                    dbName = (string)Data.company_value;
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();
                //if (!string.IsNullOrEmpty(dt.Rows[0]["SAPRefID"].ToString()) && !string.IsNullOrEmpty(dt.Rows[0]["SAPRefNo"].ToString()))
                //{
                //    Doc.DocEntry = Convert.ToInt32(dt.Rows[0]["SAPRefID"]);
                //}
                //else
                //{
                //    throw new Exception("SAP Ref No. is require");
                //}

                Doc.DocType = SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes;
                Doc.DocumentDate = Convert.ToDateTime(Data.canceldate);

                ICON.SAP.API.SAPTABLE SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));

                if (!string.IsNullOrEmpty(sap_receive_code) && sap_receive_code != "undefined")
                {
                    Doc.DocEntry = Convert.ToInt32(SAPB1.GetDocEntry(SAPTABLE, sap_receive_code));
                }
                else
                {
                    throw new Exception("SAP Receive Code. is require");
                }


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

        #region #### Goods Return ####
        /****create****/
        private string CreateGoodsReturnCM(dynamic Data)
        {
            string po_reccode = (string)Data.po_reccode;
            string vatCode = "NIG";
            string project_code = "";
            string dbName = "";
            if (!string.IsNullOrEmpty((string)Data.project_code))
            {
                project_code = (string)Data.project_code;
            }
            if (!string.IsNullOrEmpty((string)Data.vatCode))
            {
                vatCode = (string)Data.vatCode;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                "GoodsReturn_CM",
                "CMCreateGoodsReturn",
                po_reccode,
                "grth.po_reccode",
                DocEntry,
                "ORPD.DocEntry",
                Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                TranBy);

            try
            {
                string sql = GetSqlCMDetail(po_reccode, "grtd");
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable dt = new DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);

                //string SQL = GetSqlCMDBConnect(project_code);
                //MySqlDataAdapter daConf = default(MySqlDataAdapter);
                //System.Data.DataTable dtConf = new System.Data.DataTable();
                //daConf = new MySqlDataAdapter(SQL, conn);
                //daConf.Fill(dtConf);

                //if (dtConf.Rows.Count > 0)
                if (!string.IsNullOrEmpty((string)Data.company_value))
                {
                    //dbName = dtConf.Rows[0]["DBName"].ToString();
                    dbName = (string)Data.company_value;
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    CardCode = (string)Data.vendercode,
                    NumAtCard = (string)Data.taxno,    // Vendor Ref. No.
                    //PostingDate = DateTime.Now,
                    //DocumentDate = DateTime.Now,
                    PostingDate = (DateTime)Data.po_recdate,
                    DocumentDate = (DateTime)Data.po_recdate,
                    DocDueDate = (DateTime)Data.po_recdate,
                    DocType = SAPbobsCOM.BoObjectTypes.oPurchaseReturns,
                    UDF_RefNo = po_reccode,
                    UDF_VatPeriodDate = (DateTime)Data.duedate,
                };

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    //if (Convert.ToDecimal(dr["poi_vat"].ToString()) > 0) vatCode = "IG";
                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        ItemCode = dr["poi_matcode"].ToString(),
                        Qty = Convert.ToDouble(dr["return_qty"]),
                        UnitPrice = dr["sap_unitprice"].ToString(),
                        WhsCode = dr["ic_warehouse"].ToString(),
                        ProjectCode = (string)Data.project_code,
                        OcrCode = string.IsNullOrEmpty(dr["ocrcode"].ToString()) ? "" : dr["ocrcode"].ToString(),
                        OcrCode2 = string.IsNullOrEmpty(dr["ocrcode2"].ToString()) ? "" : dr["ocrcode2"].ToString(),
                        OcrCode3 = string.IsNullOrEmpty(dr["ocrcode3"].ToString()) ? "" : dr["ocrcode3"].ToString(),
                        OcrCode4 = string.IsNullOrEmpty(dr["ocrcode4"].ToString()) ? "" : dr["ocrcode4"].ToString(),
                        OcrCode5 = string.IsNullOrEmpty(dr["ocrcode5"].ToString()) ? "" : dr["ocrcode5"].ToString(),
                        LineTotal = dr["LineTotal"].ToString(),
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
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                    po_reccode,
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
        private string CancelGoodsReturnCM(dynamic Data)
        {
            string po_reccode = (string)Data.po_reccode;
            string project_code = "";
            string dbName = "";
            string sap_return_code = "";
            if (!string.IsNullOrEmpty((string)Data.project_code))
            {
                project_code = (string)Data.project_code;
            }
            if (!string.IsNullOrEmpty((string)Data.sap_return_code))
            {
                sap_return_code = (string)Data.sap_return_code;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                "GoodsReturn_CM",
                "CMCancelGoodsReturn",
                po_reccode,
                "grth.po_reccode",
                DocEntry,
                "ORPD.DocEntry",
                Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                TranBy);

            try
            {
                //string sql = GetSqlInterfaceLog((string)Data.po_reccode, "create");
                //DataTable dt = dbHelp.ExecuteDataTable(sql);

                //string SQL = GetSqlCMDBConnect(project_code);
                //MySqlDataAdapter daConf = default(MySqlDataAdapter);
                //System.Data.DataTable dtConf = new System.Data.DataTable();
                //daConf = new MySqlDataAdapter(SQL, conn);
                //daConf.Fill(dtConf);

                //if (dtConf.Rows.Count > 0)
                if (!string.IsNullOrEmpty((string)Data.company_value))
                {
                    //dbName = dtConf.Rows[0]["DBName"].ToString();
                    dbName = (string)Data.company_value;
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();

                //if (!string.IsNullOrEmpty(dt.Rows[0]["SAPRefID"].ToString()) && !string.IsNullOrEmpty(dt.Rows[0]["SAPRefNo"].ToString()))
                //{
                //    Doc.DocEntry = Convert.ToInt32(dt.Rows[0]["SAPRefID"]);
                //}
                //else
                //{
                //    throw new Exception("SAP Ref No. is require");
                //}

                Doc.DocType = SAPbobsCOM.BoObjectTypes.oPurchaseReturns;
                Doc.DocumentDate = (DateTime)Data.canceldate;

                ICON.SAP.API.SAPTABLE SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));

                if (!string.IsNullOrEmpty(sap_return_code) && sap_return_code != "undefined")
                {
                    Doc.DocEntry = Convert.ToInt32(SAPB1.GetDocEntry(SAPTABLE, sap_return_code));
                }
                else
                {
                    throw new Exception("SAP Return Code. is require");
                }

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

        #region #### Goods Receipt ####
        /****create****/
        private string CreateCMGoodsReceipt(dynamic Data)
        {
            string po_reccode = (string)Data.po_reccode;
            string methodName = "CMCreateGoodsReceipt";
            string refDescription = "grch.po_reccode";
            string vatCode = "NIG";
            bool isService = false;
            string project_code = "";
            string dbName = "";
            if (!string.IsNullOrEmpty((string)Data.methodName))
            {
                methodName = (string)Data.methodName;
            }
            if (!string.IsNullOrEmpty((string)Data.refDescription))
            {
                refDescription = (string)Data.refDescription;
            }
            if (!string.IsNullOrEmpty((string)Data.vatCode))
            {
                vatCode = (string)Data.vatCode;
            }
            if (Data.isService != null)
            {
                isService = (bool)Data.isService;
            }
            if (!string.IsNullOrEmpty((string)Data.project_code))
            {
                project_code = (string)Data.project_code;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodReceipt_CM",
                    methodName,
                    po_reccode,
                    refDescription,
                    DocEntry,
                    "OIGN.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {
                string sql = GetSqlCMDetail(po_reccode);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable dt = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);

                //string SQL = GetSqlCMDBConnect(project_code);
                //MySqlDataAdapter daConf = default(MySqlDataAdapter);
                //System.Data.DataTable dtConf = new System.Data.DataTable();
                //daConf = new MySqlDataAdapter(SQL, conn);
                //daConf.Fill(dtConf);

                //if (dtConf.Rows.Count > 0)
                if (!string.IsNullOrEmpty((string)Data.company_value))
                {
                    //dbName = dtConf.Rows[0]["DBName"].ToString();
                    dbName = (string)Data.company_value;
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    DocumentDate = (DateTime)Data.po_recdate,
                    TaxDate = (DateTime)Data.taxdate,
                    DocType = SAPbobsCOM.BoObjectTypes.oInventoryGenEntry,
                    UDF_RefNo = po_reccode,
                    UDF_VatPeriodDate = (DateTime)Data.duedate,
                    UDF_CustName = (string)Data.createuser,
                    U_ICON_IssueReason = (string)Data.remark,
                };

                foreach (DataRow dr in dt.Rows)
                {
                    string itemName = dr["poi_matname"].ToString();
                    //check item that service or not
                    if (string.IsNullOrEmpty(dr["poi_vat"].ToString()) || Convert.ToDecimal(dr["poi_vat"].ToString()) == 0)
                    {
                        vatCode = "NIG";
                    }
                    else
                    {
                        vatCode = "IG";
                    }

                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        ItemCode = dr["poi_matcode"].ToString(),
                        ItemDescription = itemName,
                        Qty = Convert.ToDouble(dr["poi_qty"]),
                        //TaxCode = vatCode,
                        UnitPrice = dr["poi_priceunit"].ToString(),
                        WhsCode = dr["ic_warehouse"].ToString(),
                        ProjectCode = string.IsNullOrEmpty(dr["projectcode"].ToString()) ? dr["ic_warehouse"].ToString() : dr["projectcode"].ToString(),
                        AccountCode = (string)Data.acccode,
                        OcrCode = string.IsNullOrEmpty(dr["ocrcode"].ToString()) ? "" : dr["ocrcode"].ToString(),
                        OcrCode2 = string.IsNullOrEmpty(dr["ocrcode2"].ToString()) ? "" : dr["ocrcode2"].ToString(),
                        OcrCode3 = string.IsNullOrEmpty(dr["ocrcode3"].ToString()) ? "" : dr["ocrcode3"].ToString(),
                        OcrCode4 = string.IsNullOrEmpty(dr["ocrcode4"].ToString()) ? "" : dr["ocrcode4"].ToString(),
                        OcrCode5 = string.IsNullOrEmpty(dr["ocrcode5"].ToString()) ? "" : dr["ocrcode5"].ToString(),
                        LineTotal = dr["LineTotal"].ToString(),
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
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     po_reccode,
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
        private string CancelCMGoodReceipt(dynamic Data)
        {
            string po_reccode = (string)Data.po_reccode;
            string methodName = "CMCancelGoodsReceipt";
            string refDescription = "grch.po_reccode";
            string project_code = "";
            string dbName = "";
            string sap_receive_code = "";

            if (!string.IsNullOrEmpty((string)Data.project_code))
            {
                project_code = (string)Data.project_code;
            }
            if (!string.IsNullOrEmpty((string)Data.sap_receive_code))
            {
                sap_receive_code = (string)Data.sap_receive_code;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            DBHelper dbHelp = new DBHelper(ICON.Configuration.Database.REM_ConnectionString, null);

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "GoodReceipt_CM",
                    methodName,
                    po_reccode,
                    refDescription,
                    DocEntry,
                    "OIGN.DocEntry",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {
                //string sql = GetSqlInterfaceLog((string)Data.po_reccode, "create");
                //DataTable dt = dbHelp.ExecuteDataTable(sql);

                //string SQL = GetSqlCMDBConnect(project_code);
                //MySqlDataAdapter daConf = default(MySqlDataAdapter);
                //System.Data.DataTable dtConf = new System.Data.DataTable();
                //daConf = new MySqlDataAdapter(SQL, conn);
                //daConf.Fill(dtConf);

                //if (dtConf.Rows.Count > 0)
                if (!string.IsNullOrEmpty((string)Data.company_value))
                {
                    //dbName = dtConf.Rows[0]["DBName"].ToString();
                    dbName = (string)Data.company_value;
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document();

                //if (!string.IsNullOrEmpty(dt.Rows[0]["SAPRefID"].ToString()) && !string.IsNullOrEmpty(dt.Rows[0]["SAPRefNo"].ToString()))
                //{
                //    Doc.DocEntry = Convert.ToInt32(dt.Rows[0]["SAPRefID"]);
                //}
                //else
                //{
                //    throw new Exception("SAP Ref No. is require");
                //}

                Doc.DocType = SAPbobsCOM.BoObjectTypes.oInventoryGenEntry;
                Doc.DocumentDate = (DateTime)Data.canceldate;

                ICON.SAP.API.SAPTABLE SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));

                if (!string.IsNullOrEmpty(sap_receive_code) && sap_receive_code != "undefined")
                {
                    Doc.DocEntry = Convert.ToInt32(SAPB1.GetDocEntry(SAPTABLE, sap_receive_code));
                }
                else
                {
                    throw new Exception("SAP Receive Code. is require");
                }

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

        #region #### Goods Issue ####
        /****create****/
        private string CreateGoodsIssueCM(dynamic Data)
        {
            string Is_Doccode = (string)Data.is_doccode;
            string project_code = "";
            string dbName = "";
            if (!string.IsNullOrEmpty((string)Data.project_code))
            {
                project_code = (string)Data.project_code;
            }

            string DocEntry = string.Empty;
            string DocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                "GoodsIssue_CM",
                "CreateGoodsIssueCM",
                Is_Doccode,
                "gih.is_doccode",
                DocEntry,
                "OIGE.DocEntry",
                Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                TranBy);

            try
            {
                string sql = GetSqlCMDetail(Is_Doccode, "gid", "isi_doccode");
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable dt = new DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);

                //string SQL = GetSqlCMDBConnect(project_code);
                //MySqlDataAdapter daConf = default(MySqlDataAdapter);
                //System.Data.DataTable dtConf = new System.Data.DataTable();
                //daConf = new MySqlDataAdapter(SQL, conn);
                //daConf.Fill(dtConf);

                //if (dtConf.Rows.Count > 0)
                if (!string.IsNullOrEmpty((string)Data.company_value))
                {
                    //dbName = dtConf.Rows[0]["DBName"].ToString();
                    dbName = (string)Data.company_value;
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }
                
                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    PostingDate = DateTime.Now,
                    DocumentDate = DateTime.Now,
                    DocType = SAPbobsCOM.BoObjectTypes.oInventoryGenExit,
                    UDF_RefNo = Is_Doccode,
                    UDF_CustName = (string)Data.is_reqname,
                    //Remark = (string)Data.is_remark,
                    U_ICON_IssueReason = (string)Data.remark,
                    JrnlMemo = (string)Data.is_place,
                };

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        ItemCode = dr["isi_matcode"].ToString(),
                        //ItemCode = "0001-ALM-00023",
                        Qty = Convert.ToDouble(dr["isi_xqty"]),
                        WhsCode = dr["isi_whcode"].ToString(),
                        ProjectCode = string.IsNullOrEmpty(project_code) ? dr["isi_whcode"].ToString() : project_code,
                        //CostCode = dr["isi_costcode"].ToString(),
                        AccountCode = (string)Data.acccode,
                        OcrCode = string.IsNullOrEmpty(dr["ocrcode"].ToString()) ? "" : dr["ocrcode"].ToString(),
                        OcrCode2 = string.IsNullOrEmpty(dr["ocrcode2"].ToString()) ? "" : dr["ocrcode2"].ToString(),
                        OcrCode3 = string.IsNullOrEmpty(dr["ocrcode3"].ToString()) ? "" : dr["ocrcode3"].ToString(),
                        OcrCode4 = string.IsNullOrEmpty(dr["ocrcode4"].ToString()) ? "" : dr["ocrcode4"].ToString(),
                        OcrCode5 = string.IsNullOrEmpty(dr["ocrcode5"].ToString()) ? "" : dr["ocrcode5"].ToString(),
                    });
                }

                try
                {
                    SAPB1.CreateGoodsIssueCM(Doc, out DocEntry, out DocNum, out SAPStatusCode, out SAPErrorMessage, out LogDetail);
                    SAPStatus = SAPStatusCode.ToString();
                }
                catch (Exception ex)
                {
                    SAPStatus = SAPStatusCode.ToString();
                    throw ex;
                }
                return DocNum;
                //return DocEntry;
            }
            catch (Exception ex)
            {
                ResponseCode = HttpStatusCode.InternalServerError;
                ErrorMessage = ex.Message + " " + ex.StackTrace;
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                    Is_Doccode,
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
        #endregion

        #region #### Journal Voucher #####
        private string CreateJournalVoucher(dynamic Data)
        {
            string docNo = (string)Data.docno;
            string methodName = "CMCreateJournalVoucher";
            string refDescription = "pcavdh";
            string vatCode = "NIG";
            string project_code = "";
            //string petty_cash_acc = "";
            string petty_cash_cardcode = (string)Data.petty_cash_cardcode;
            string petty_cash_amt = (string)Data.petty_cash_amt;
            string dbName = "";
            string u_tax_cardname = "";
            string memo = "";
            //if (!string.IsNullOrEmpty((string)Data.methodName))
            //{
            //    methodName = (string)Data.methodName;
            //}
            //if (!string.IsNullOrEmpty((string)Data.refDescription))
            //{
            //    refDescription = (string)Data.refDescription;
            //}

            //if (!string.IsNullOrEmpty((string)Data.project_code))
            //{
            //    project_code = (string)Data.project_code;
            //}
            if (!string.IsNullOrEmpty((string)Data.memo))
            {
                memo = (string)Data.memo;
            }

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "JournalVoucher_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "BOTF.BatchNum",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {
                string sql = GetSqlCMDetail(docNo, "pcadvd", "pettycashi_ref");
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable dt = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);

                //string SQL = GetSqlCMDBConnect(project_code);
                //MySqlDataAdapter daConf = default(MySqlDataAdapter);
                //System.Data.DataTable dtConf = new System.Data.DataTable();
                //daConf = new MySqlDataAdapter(SQL, conn);
                //daConf.Fill(dtConf);

                //if (dtConf.Rows.Count > 0)
                if (!string.IsNullOrEmpty((string)Data.company_value))
                {
                    //dbName = dtConf.Rows[0]["DBName"].ToString();
                    dbName = (string)Data.company_value;
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    PostingDate = Convert.ToDateTime((string)Data.docdate),
                    DocDueDate = Convert.ToDateTime((string)Data.docdate),
                    DocumentDate = Convert.ToDateTime((string)Data.docdate),
                    DocType = SAPbobsCOM.BoObjectTypes.oJournalVouchers,
                    UDF_RefNo = docNo,
                    JrnlMemo = memo,
                    //U_ICON_PT = (string)Data.u_icon_pt,
                    //U_ICON_BF = (string)Data.u_icon_bf,
                };

                foreach (DataRow dr in dt.Rows)
                {
                    //if (string.IsNullOrEmpty(dr["pettycashi_vatt"].ToString()) || Convert.ToDecimal(dr["pettycashi_vatt"].ToString()) == 0)
                    //{

                    //    vatCode = "NIG";
                    //}
                    //else
                    //{
                    //    vatCode = "IG";


                    //}

                    if (!string.IsNullOrEmpty(dr["vatcode"].ToString()))
                    {
                        vatCode = dr["vatcode"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dr["project_code"].ToString()))
                    {
                        project_code = dr["project_code"].ToString();
                    }

                    u_tax_cardname = dr["pettycashi_vender"].ToString();

                    DateTime Tax_Date = DateTime.Now;
                    DateTime.TryParse(dr["pettycashi_taxdate"].ToString(), out Tax_Date);

                    Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                    {
                        AccountCode = dr["DR_Expense_ACC"].ToString(),
                        ShortName = dr["DR_Expense_ACC"].ToString(),
                        TaxCode = vatCode,
                        Debit = Convert.ToDouble(dr["DR_Expense_amt"]),
                        LineMemo = string.IsNullOrEmpty(dr["pettycashi_description"].ToString()) ? "" : dr["pettycashi_description"].ToString().Length > 50 ? dr["pettycashi_description"].ToString().Substring(0, 50) : dr["pettycashi_description"].ToString(),
                        ProjectCode = string.IsNullOrEmpty(project_code) ? "" : project_code,
                        OcrCode = string.IsNullOrEmpty(dr["ocrcode"].ToString()) ? "" : dr["ocrcode"].ToString(),
                        OcrCode2 = string.IsNullOrEmpty(dr["ocrcode2"].ToString()) ? "" : dr["ocrcode2"].ToString(),
                        OcrCode3 = string.IsNullOrEmpty(dr["ocrcode3"].ToString()) ? "" : dr["ocrcode3"].ToString(),
                        OcrCode4 = string.IsNullOrEmpty(dr["ocrcode4"].ToString()) ? "" : dr["ocrcode4"].ToString(),
                        OcrCode5 = string.IsNullOrEmpty(dr["ocrcode5"].ToString()) ? "" : dr["ocrcode5"].ToString(),
                        TAX_CARDNAME = u_tax_cardname,
                        U_ICON_PT = dr["phase_name"].ToString(),
                        U_ICON_BF = dr["block_name"].ToString()
                    });

                    if (Convert.ToDouble(dr["DR_VAT_Amt"]) > 0)
                    {
                        Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                        {
                            AccountCode = dr["DR_VAT_Acc"].ToString(),
                            ShortName = dr["DR_VAT_Acc"].ToString(),
                            TaxCode = vatCode,
                            Debit = Convert.ToDouble(dr["DR_VAT_Amt"]),
                            LineMemo = string.IsNullOrEmpty(dr["pettycashi_description"].ToString()) ? "" : dr["pettycashi_description"].ToString().Length > 50 ? dr["pettycashi_description"].ToString().Substring(0, 50) : dr["pettycashi_description"].ToString(),
                            ProjectCode = string.IsNullOrEmpty(project_code) ? "" : project_code,
                            OcrCode = string.IsNullOrEmpty(dr["ocrcode"].ToString()) ? "" : dr["ocrcode"].ToString(),
                            OcrCode2 = string.IsNullOrEmpty(dr["ocrcode2"].ToString()) ? "" : dr["ocrcode2"].ToString(),
                            OcrCode3 = string.IsNullOrEmpty(dr["ocrcode3"].ToString()) ? "" : dr["ocrcode3"].ToString(),
                            OcrCode4 = string.IsNullOrEmpty(dr["ocrcode4"].ToString()) ? "" : dr["ocrcode4"].ToString(),
                            OcrCode5 = string.IsNullOrEmpty(dr["ocrcode5"].ToString()) ? "" : dr["ocrcode5"].ToString(),
                            //WhsCode = dr["ic_warehouse"].ToString(),

                            //TAX_BASE = dr["pettycashi_unitprice"].ToString(),
                            TAX_NO = "",
                            TAX_REFNO = dr["pettycashi_invno"].ToString(),
                            TAX_PECL = Doc.PostingDate.ToString("yyyy-MM"),
                            //TAX_TYPE = dr["tax_type"].ToString(),
                            TAX_BOOKNO = "",
                            TAX_CARDNAME = u_tax_cardname,
                            TAX_ADDRESS = dr["pettycashi_addrvender"].ToString(),
                            TAX_TAXID = (string)Data.vender_tax,
                            TAX_DATE = Tax_Date,
                            //TAX_CODE = dr["WTax_Code"].ToString(),
                            //TAX_CODENAME = dr["name_wt"].ToString(),
                            //TAX_RATE = dr["per_wt"].ToString(),
                            //TAX_DEDUCT = string.IsNullOrEmpty(dr["tax_deduct"].ToString()) ? "1" : dr["tax_deduct"].ToString(),
                            //TAX_OTHER = dr["tax_other"].ToString(),
                            U_ICON_PT = dr["phase_name"].ToString(),
                            U_ICON_BF = dr["block_name"].ToString()
                        });
                    }


                    //if (!string.IsNullOrEmpty(project_code))
                    //{
                    //    List<Dictionary<string, object>> PettyCashData = SAPB1.GetPettyCashBalance(project_code, out SAPStatusCode, out SAPErrorMessage);

                    //    if (PettyCashData.Count > 0)
                    //    {
                    //        petty_cash_acc = PettyCashData[0]["DebPayAcct"].ToString();
                    //        //petty_cash_cardcode = PettyCashData[0]["CardCode"].ToString();
                    //    }
                    //    else
                    //    {
                    //        throw new Exception("No Petty Cash Data. for this Project : " + project_code);
                    //    }
                    //}

                    if (Convert.ToDouble(dr["CR_WT_Amt"]) > 0)
                    {
                        Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                        {
                            AccountCode = dr["CR_WT_Acc"].ToString(),
                            ShortName = dr["CR_WT_Acc"].ToString(),
                            TaxCode = vatCode,
                            Credit = Convert.ToDouble(dr["CR_WT_Amt"]),
                            LineMemo = string.IsNullOrEmpty(dr["pettycashi_description"].ToString()) ? "" : dr["pettycashi_description"].ToString().Length > 50 ? dr["pettycashi_description"].ToString().Substring(0, 50) : dr["pettycashi_description"].ToString(),
                            ProjectCode = string.IsNullOrEmpty(project_code) ? "" : project_code,
                            OcrCode = string.IsNullOrEmpty(dr["ocrcode"].ToString()) ? "" : dr["ocrcode"].ToString(),
                            OcrCode2 = string.IsNullOrEmpty(dr["ocrcode2"].ToString()) ? "" : dr["ocrcode2"].ToString(),
                            OcrCode3 = string.IsNullOrEmpty(dr["ocrcode3"].ToString()) ? "" : dr["ocrcode3"].ToString(),
                            OcrCode4 = string.IsNullOrEmpty(dr["ocrcode4"].ToString()) ? "" : dr["ocrcode4"].ToString(),
                            OcrCode5 = string.IsNullOrEmpty(dr["ocrcode5"].ToString()) ? "" : dr["ocrcode5"].ToString(),
                            //WhsCode = dr["ic_warehouse"].ToString(),

                            TAX_BASE = dr["pettycashi_unitprice"].ToString(),
                            TAX_NO = "",
                            TAX_REFNO = dr["pettycashi_invno"].ToString(),
                            TAX_PECL = Doc.PostingDate.ToString("yyyy-MM"),
                            TAX_TYPE = dr["tax_type"].ToString(),
                            TAX_BOOKNO = "",
                            TAX_CARDNAME = u_tax_cardname,
                            TAX_ADDRESS = dr["pettycashi_addrvender"].ToString(),
                            TAX_TAXID = (string)Data.vender_tax,
                            TAX_DATE = Tax_Date,
                            TAX_CODE = dr["WTax_Code"].ToString(),
                            TAX_CODENAME = dr["name_wt"].ToString(),
                            TAX_RATE = dr["per_wt"].ToString(),
                            TAX_DEDUCT = string.IsNullOrEmpty(dr["tax_deduct"].ToString()) ? "1" : dr["tax_deduct"].ToString(),
                            TAX_OTHER = dr["tax_other"].ToString(),
                            U_ICON_PT = dr["phase_name"].ToString(),
                            U_ICON_BF = dr["block_name"].ToString()
                        });
                    }

                }

                Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                {
                    AccountCode = "",
                    ContraAccount = petty_cash_cardcode,
                    ShortName = petty_cash_cardcode,
                    TaxCode = vatCode,
                    Credit = Convert.ToDouble(petty_cash_amt),
                    ProjectCode = string.IsNullOrEmpty(project_code) ? "" : project_code,
                    //OcrCode = string.IsNullOrEmpty(dr["ocrcode"].ToString()) ? "" : dr["ocrcode"].ToString(),
                    //OcrCode2 = string.IsNullOrEmpty(dr["ocrcode2"].ToString()) ? "" : dr["ocrcode2"].ToString(),
                    //OcrCode3 = string.IsNullOrEmpty(dr["ocrcode3"].ToString()) ? "" : dr["ocrcode3"].ToString(),
                    //OcrCode4 = string.IsNullOrEmpty(dr["ocrcode4"].ToString()) ? "" : dr["ocrcode4"].ToString(),
                    //OcrCode5 = string.IsNullOrEmpty(dr["ocrcode5"].ToString()) ? "" : dr["ocrcode5"].ToString(),
                    //WhsCode = dr["ic_warehouse"].ToString(),
                    //TAX_CARDNAME = u_tax_cardname,
                });

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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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
        private string SendGVAllocate(dynamic Data)
        {
            string docNo = (string)Data.Transld;
            string methodName = "CMSendGVAllocate";
            string refDescription = "pas_h";
            string vatCode = "NIG";
            string project_code = "";
            string dbName = "";
            //if (!string.IsNullOrEmpty((string)Data.methodName))
            //{
            //    methodName = (string)Data.methodName;
            //}
            //if (!string.IsNullOrEmpty((string)Data.refDescription))
            //{
            //    refDescription = (string)Data.refDescription;
            //}

            if (!string.IsNullOrEmpty((string)Data.Project))
            {
                project_code = (string)Data.Project;
            }

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "JournalVoucher_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "BOTF.BatchNum",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {
                string sql = GetSqlCMDetail(docNo, "pas_d", "Transld");
                MySqlDataAdapter da = default(MySqlDataAdapter);
                System.Data.DataTable dt = new System.Data.DataTable();
                da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);

                //string SQL = GetSqlCMDBConnect(project_code);
                //MySqlDataAdapter daConf = default(MySqlDataAdapter);
                //System.Data.DataTable dtConf = new System.Data.DataTable();
                //daConf = new MySqlDataAdapter(SQL, conn);
                //daConf.Fill(dtConf);

                //if (dtConf.Rows.Count > 0)
                if (!string.IsNullOrEmpty((string)Data.company_value))
                {
                    //dbName = dtConf.Rows[0]["DBName"].ToString();
                    dbName = (string)Data.company_value;
                }
                else
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Document Doc = new ICON.SAP.API.SAPB1.Document
                {
                    PostingDate = (DateTime)Data.RefDate,
                    DocDueDate = (DateTime)Data.DueDate,
                    DocumentDate = (DateTime)Data.RefDate,
                    TaxDate = (DateTime)Data.TaxDate,
                    DocType = SAPbobsCOM.BoObjectTypes.oJournalVouchers,
                    UDF_RefNo = (string)Data.Icon_ref,
                    JrnlMemo = (string)Data.Remarks,
                    StornoDate = ICON.Utilities.Convert.ConvertDateToString((DateTime)Data.StornoDate, "yyy-MM-dd"),
                };

                foreach (DataRow dr in dt.Rows)
                {
                    //if (string.IsNullOrEmpty(dr["pettycashi_vatt"].ToString()) || Convert.ToDecimal(dr["pettycashi_vatt"].ToString()) == 0)
                    //{

                    //    vatCode = "NIG";
                    //}
                    //else
                    //{
                    //    vatCode = "IG";


                    //}

                    if (!string.IsNullOrEmpty(dr["vatcode"].ToString()))
                    {
                        if (Convert.ToDouble(dr["Credit"]) > 0)
                        {
                            Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                            {
                                AccountCode = dr["Account"].ToString(),
                                ShortName = dr["shortname"].ToString(),
                                TaxCode = vatCode,
                                Credit = Convert.ToDouble(dr["Credit"]),
                                LineMemo = string.IsNullOrEmpty(dr["LineMoemo"].ToString()) ? "" : dr["LineMoemo"].ToString().Length > 50 ? dr["LineMoemo"].ToString().Substring(0, 50) : dr["LineMoemo"].ToString(),
                                ProjectCode = string.IsNullOrEmpty(dr["Project"].ToString()) ? "" : dr["Project"].ToString(),
                                OcrCode = string.IsNullOrEmpty(dr["OcrCode"].ToString()) ? "" : dr["OcrCode"].ToString(),
                                OcrCode2 = string.IsNullOrEmpty(dr["OcrCode2"].ToString()) ? "" : dr["OcrCode2"].ToString(),
                                OcrCode3 = string.IsNullOrEmpty(dr["OcrCode3"].ToString()) ? "" : dr["OcrCode3"].ToString(),
                                OcrCode4 = string.IsNullOrEmpty(dr["OcrCode4"].ToString()) ? "" : dr["OcrCode4"].ToString(),
                                //OcrCode5 = string.IsNullOrEmpty(dr["OcrCode5"].ToString()) ? "" : dr["OcrCode5"].ToString(),
                            });
                        }
                        else
                        {
                            Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                            {
                                AccountCode = dr["Account"].ToString(),
                                ShortName = dr["shortname"].ToString(),
                                TaxCode = vatCode,
                                Debit = Convert.ToDouble(dr["Debit"]),
                                LineMemo = string.IsNullOrEmpty(dr["LineMoemo"].ToString()) ? "" : dr["LineMoemo"].ToString().Length > 50 ? dr["LineMoemo"].ToString().Substring(0, 50) : dr["LineMoemo"].ToString(),
                                ProjectCode = string.IsNullOrEmpty(dr["Project"].ToString()) ? "" : dr["Project"].ToString(),
                                OcrCode = string.IsNullOrEmpty(dr["OcrCode"].ToString()) ? "" : dr["OcrCode"].ToString(),
                                OcrCode2 = string.IsNullOrEmpty(dr["OcrCode2"].ToString()) ? "" : dr["OcrCode2"].ToString(),
                                OcrCode3 = string.IsNullOrEmpty(dr["OcrCode3"].ToString()) ? "" : dr["OcrCode3"].ToString(),
                                OcrCode4 = string.IsNullOrEmpty(dr["OcrCode4"].ToString()) ? "" : dr["OcrCode4"].ToString(),
                                //OcrCode5 = string.IsNullOrEmpty(dr["OcrCode5"].ToString()) ? "" : dr["OcrCode5"].ToString(),
                            });
                        }

                    }
                    else
                    {
                        if (Convert.ToDouble(dr["Credit"]) > 0)
                        {
                            Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                            {
                                AccountCode = dr["Account"].ToString(),
                                ShortName = dr["shortname"].ToString(),
                                Credit = Convert.ToDouble(dr["Credit"]),
                                LineMemo = string.IsNullOrEmpty(dr["LineMoemo"].ToString()) ? "" : dr["LineMoemo"].ToString().Length > 50 ? dr["LineMoemo"].ToString().Substring(0, 50) : dr["LineMoemo"].ToString(),
                                ProjectCode = string.IsNullOrEmpty(dr["Project"].ToString()) ? "" : dr["Project"].ToString(),
                                OcrCode = string.IsNullOrEmpty(dr["OcrCode"].ToString()) ? "" : dr["OcrCode"].ToString(),
                                OcrCode2 = string.IsNullOrEmpty(dr["OcrCode2"].ToString()) ? "" : dr["OcrCode2"].ToString(),
                                OcrCode3 = string.IsNullOrEmpty(dr["OcrCode3"].ToString()) ? "" : dr["OcrCode3"].ToString(),
                                OcrCode4 = string.IsNullOrEmpty(dr["OcrCode4"].ToString()) ? "" : dr["OcrCode4"].ToString(),
                                //OcrCode5 = string.IsNullOrEmpty(dr["OcrCode5"].ToString()) ? "" : dr["OcrCode5"].ToString(),
                            });
                        }
                        else
                        {
                            Doc.Lines.Add(new ICON.SAP.API.SAPB1.DocumentLine
                            {
                                AccountCode = dr["Account"].ToString(),
                                ShortName = dr["shortname"].ToString(),
                                Debit = Convert.ToDouble(dr["Debit"]),
                                LineMemo = string.IsNullOrEmpty(dr["LineMoemo"].ToString()) ? "" : dr["LineMoemo"].ToString().Length > 50 ? dr["LineMoemo"].ToString().Substring(0, 50) : dr["LineMoemo"].ToString(),
                                ProjectCode = string.IsNullOrEmpty(dr["Project"].ToString()) ? "" : dr["Project"].ToString(),
                                OcrCode = string.IsNullOrEmpty(dr["OcrCode"].ToString()) ? "" : dr["OcrCode"].ToString(),
                                OcrCode2 = string.IsNullOrEmpty(dr["OcrCode2"].ToString()) ? "" : dr["OcrCode2"].ToString(),
                                OcrCode3 = string.IsNullOrEmpty(dr["OcrCode3"].ToString()) ? "" : dr["OcrCode3"].ToString(),
                                OcrCode4 = string.IsNullOrEmpty(dr["OcrCode4"].ToString()) ? "" : dr["OcrCode4"].ToString(),
                                //OcrCode5 = string.IsNullOrEmpty(dr["OcrCode5"].ToString()) ? "" : dr["OcrCode5"].ToString(),
                            });
                        }
                    }

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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Chart of Accounts #####
        private string CreateUpdateChartofAccounts(dynamic Data)
        {
            string docNo = (string)Data.act_code;
            string methodName = "CreateUpdateChartofAccounts";
            string refDescription = "acc_code";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateChartofAccounts_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OACT",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.ChartofAccounts ObjSave = new SAPB1.ChartofAccounts
                {
                    AcctCode = Data.act_code,
                    AcctName = Data.act_name,
                    FrgnName = Data.act_name_eng,
                    Postable = string.IsNullOrEmpty((string)Data.act_status) || (string)Data.act_status == "Y" ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO,
                    AccntntCod = Data.act_code,
                    AccountTyp = Data.act_header,
                    ActCurr = string.IsNullOrEmpty((string)Data.act_curr) ? "##" : (string)Data.act_curr,
                    FatherNum = (string)Data.father_num,
                    LocManTran = string.IsNullOrEmpty((string)Data.locmantran) || (string)Data.locmantran == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    Finanse = string.IsNullOrEmpty((string)Data.finanse) || (string)Data.finanse == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    CfwRlvnt = string.IsNullOrEmpty((string)Data.cfw_rlvnt) || (string)Data.cfw_rlvnt == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    PrjRelvnt = string.IsNullOrEmpty((string)Data.proj_relvnt) || (string)Data.proj_relvnt == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    Dim1Relvnt = string.IsNullOrEmpty((string)Data.dim1_relvnt) || (string)Data.dim1_relvnt == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    Dim2Relvnt = string.IsNullOrEmpty((string)Data.dim2_relvnt) || (string)Data.dim2_relvnt == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    Dim3Relvnt = string.IsNullOrEmpty((string)Data.dim3_relvnt) || (string)Data.dim3_relvnt == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    Dim4Relvnt = string.IsNullOrEmpty((string)Data.dim4_relvnt) || (string)Data.dim4_relvnt == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    Dim5Relvnt = string.IsNullOrEmpty((string)Data.dim5_relvnt) || (string)Data.dim5_relvnt == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    BlocManPos = string.IsNullOrEmpty((string)Data.blocmanpos) || (string)Data.blocmanpos == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    ValidFor = string.IsNullOrEmpty((string)Data.valid_for) || (string)Data.valid_for == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    FrozenFor = string.IsNullOrEmpty((string)Data.valid_for) || (string)Data.valid_for == "N" ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO,

                };

                try
                {
                    SAPB1.CreateChartofAccountMaster(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Cost Center #####
        private string CreateUpdateCostCenter(dynamic Data)
        {
            string docNo = (string)Data.cgroup_code;
            string methodName = "CreateUpdateCostCenter";
            string refDescription = "costcode";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateCostCenter_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OPRC",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.CostCenter ObjSave = new SAPB1.CostCenter
                {
                    PrcCode = (string)Data.cgroup_code,
                    PrcName = (string)Data.cgroup_name,
                    DimCode = Convert.ToInt32((string)Data.costcode_d),
                    GrpCode = (string)Data.grp_code,
                    ValidFrom = Data.valid_from,
                    ValidTo = (string)Data.valid_to,
                    Locked = (string)Data.locked == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,

                };

                try
                {
                    SAPB1.CreateUpdateCostCenterMaster(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Project #####
        private string CreateUpdateProject(dynamic Data)
        {
            string docNo = (string)Data.project_code;
            string methodName = "CMCreateUpdateProject";
            string refDescription = "pj";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateProject_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OPRJ",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Project Pro = new SAPB1.Project
                {
                    ProjectCode = (string)Data.project_code,
                    ProjectName = (string)Data.project_name,
                    //ValidFrom = (DateTime)Data.project_start,
                    //ValidTo = (string)Data.project_stop,
                    Active = (string)Data.project_status == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,

                };

                try
                {
                    SAPB1.CreateUpdateProjectMaster(Pro, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Withholding Tax #####
        private string CreateUpdateWithholdingTax(dynamic Data)
        {
            string docNo = (string)Data.WTax_Code;
            string methodName = "CMCreateUpdateWithholdingTax";
            string refDescription = "m_wt";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateWithholdingTax_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OWHT",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.WithholdingTax ObjSave = new SAPB1.WithholdingTax
                {
                    WTCode = (string)Data.WTax_Code,
                    WTName = (string)Data.name_wt,
                    Category = string.IsNullOrEmpty((string)Data.category) || (string)Data.category == "P" ? SAPbobsCOM.WithholdingTaxCodeCategoryEnum.wtcc_Payment : SAPbobsCOM.WithholdingTaxCodeCategoryEnum.wtcc_Invoice,
                    BaseType = string.IsNullOrEmpty((string)Data.Base_Type) || (string)Data.Base_Type == "N" ? SAPbobsCOM.WithholdingTaxCodeBaseTypeEnum.wtcbt_Net : SAPbobsCOM.WithholdingTaxCodeBaseTypeEnum.wtcbt_Gross,
                    PrctBsAmnt = string.IsNullOrEmpty((string)Data.Base_Amount) ? 0 : Convert.ToDouble((string)Data.Base_Amount),
                    OffClCode = (string)Data.offclcode,
                    Account = (string)Data.Account,
                    Inactive = (string)Data.Inactive == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,


                };

                ObjSave.Details.Add(new ICON.SAP.API.SAPB1.WHTDetail
                {
                    EffecDate = Data.effec_date,
                    Rate = string.IsNullOrEmpty((string)Data.rate) ? 0 : Convert.ToDouble((string)Data.rate),
                });

                try
                {
                    SAPB1.CreateUpdateWithholdingTax(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Vat Groups #####
        private string CreateUpdateVatGroups(dynamic Data)
        {
            string docNo = (string)Data.code_vat;
            string methodName = "CMCreateUpdateVatGroups";
            string refDescription = "vat";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateVatGroups_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OVTG",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.VatGroup ObjSave = new SAPB1.VatGroup
                {
                    Code = (string)Data.code_vat,
                    Name = (string)Data.vat_name,
                    Category = string.IsNullOrEmpty((string)Data.category) ? SAPbobsCOM.BoVatCategoryEnum.bovcOutputTax : (string)Data.category == "I" ? SAPbobsCOM.BoVatCategoryEnum.bovcInputTax : SAPbobsCOM.BoVatCategoryEnum.bovcOutputTax,
                    Account = (string)Data.account,
                    DeferrAcc = (string)Data.deferracc,
                    NonDedct = string.IsNullOrEmpty((string)Data.m_vat) ? 0 : Convert.ToDouble((string)Data.m_vat),
                    Inactive = (string)Data.status == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,

                };

                ObjSave.Details.Add(new ICON.SAP.API.SAPB1.VatGroupDetail
                {
                    EffecDate = Data.effec_date,
                    Rate = string.IsNullOrEmpty((string)Data.rate) ? 0 : Convert.ToDouble((string)Data.rate),
                });

                try
                {
                    SAPB1.CreateUpdateVatGroupMaster(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Payment Terms #####
        private string CreateUpdatePaymentTerms(dynamic Data)
        {
            string docNo = (string)Data.group_num;
            string methodName = "CreateUpdatePaymentTerms";
            string refDescription = "paymentterms";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdatePaymentTerms_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OCTG",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.PaymentTerms ObjSave = new SAPB1.PaymentTerms
                {
                    GroupNum = Convert.ToInt32((string)Data.group_num),
                    PymntGroup = (string)Data.pymnt_group,
                    BslineDate = (string)Data.bsline_date == "S" ? SAPbobsCOM.BoBaselineDate.bld_SystemDate : (string)Data.bsline_date == "P" ? SAPbobsCOM.BoBaselineDate.bld_PostingDate : SAPbobsCOM.BoBaselineDate.bld_TaxDate,
                    PayDuMonth = (string)Data.pay_du_month == "E" ? SAPbobsCOM.BoPayTermDueTypes.pdt_MonthEnd : (string)Data.bsline_date == "H" ? SAPbobsCOM.BoPayTermDueTypes.pdt_HalfMonth : (string)Data.bsline_date == "Y" ? SAPbobsCOM.BoPayTermDueTypes.pdt_MonthStart : SAPbobsCOM.BoPayTermDueTypes.pdt_None,
                    ExtraMonth = Convert.ToInt32((string)Data.extra_month),
                    ExtraDays = Convert.ToInt32((string)Data.extra_days),

                };

                try
                {
                    SAPB1.CreateUpdatePaymentTermsMaster(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Warehouse #####
        private string CreateUpdateWarehouse(dynamic Data)
        {
            string docNo = (string)Data.whcode;
            string methodName = "CMCreateUpdateWarehouse";
            string refDescription = "pj";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateWarehouse_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OWHS",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Warehouse Whs = new SAPB1.Warehouse
                {
                    WarehouseCode = (string)Data.whcode,
                    WarehouseName = (string)Data.whname,
                    DropShip = string.IsNullOrEmpty((string)Data.type_store) || (string)Data.type_store == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    Nettable = string.IsNullOrEmpty((string)Data.type_store) || (string)Data.type_store == "N" ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO,
                    Inactive = string.IsNullOrEmpty((string)Data.inactive) || (string)Data.inactive == "N" ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO,
                    Block = (string)Data.block,
                    ZipCode = (string)Data.zip_code,
                    City = (string)Data.city

                };

                try
                {
                    SAPB1.CreateUpdateWareHouseMaster(Whs, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Item Master #####
        private string CreateUpdateItemMaster(dynamic Data)
        {
            string docNo = (string)Data.materialcode;
            string methodName = "CMCreateUpdateItemMaster";
            string refDescription = "matunit";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateItemMaster_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OITM",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                string series = SAPB1.GetItemMasterSeries("4");
                ICON.SAP.API.SAPB1.ItemMaster ObjSave = new SAPB1.ItemMaster
                {
                    Series = series,
                    ItemCode = (string)Data.materialcode,
                    ItemName = (string)Data.materialname,
                    FrgnName = (string)Data.materialname_en,
                    ItemType = string.IsNullOrEmpty((string)Data.mat_type) || (string)Data.mat_type.ToUpper() == "I" ? SAPbobsCOM.ItemTypeEnum.itItems : (string)Data.mat_type.ToUpper() == "L" ? SAPbobsCOM.ItemTypeEnum.itLabor : (string)Data.mat_type.ToUpper() == "T" ? SAPbobsCOM.ItemTypeEnum.itTravel : SAPbobsCOM.ItemTypeEnum.itItems,
                    ItmsGrpCod = (string)Data.matgroup_code,
                    UserText = (string)Data.user_text,
                    WTLiable = string.IsNullOrEmpty((string)Data.wt_liable) || (string)Data.wt_liable.ToUpper() == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    PrchseItem = string.IsNullOrEmpty((string)Data.prchse_item) || (string)Data.prchse_item.ToUpper() == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    SellItem = string.IsNullOrEmpty((string)Data.sell_item) || (string)Data.sell_item.ToUpper() == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    InvntItem = string.IsNullOrEmpty((string)Data.invnt_item) || (string)Data.invnt_item.ToUpper() == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    ManSerNum = string.IsNullOrEmpty((string)Data.man_ser_num) || (string)Data.man_ser_num.ToUpper() == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    ManBtchNum = string.IsNullOrEmpty((string)Data.man_btch_num) || (string)Data.man_btch_num.ToUpper() == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    MngMethod = string.IsNullOrEmpty((string)Data.mng_method) || (string)Data.mng_method.ToUpper() == "A" ? SAPbobsCOM.BoManageMethod.bomm_OnEveryTransaction : SAPbobsCOM.BoManageMethod.bomm_OnReleaseOnly,
                    ValidFor = string.IsNullOrEmpty((string)Data.valid_for) || (string)Data.valid_for == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    FrozenFor = string.IsNullOrEmpty((string)Data.valid_for) || (string)Data.valid_for == "N" ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO,
                    CardCode = (string)Data.card_code,
                    SuppCatNum = (string)Data.supp_cat_num,
                    BuyUnitMsr = (string)Data.buy_unit_msr,
                    NumInBuy = string.IsNullOrEmpty((string)Data.num_in_buy) || (string)Data.num_in_buy == "0.00" ? "1" : (string)Data.num_in_buy,
                    PurPackMsr = (string)Data.pur_pack_msr,
                    PurPackUn = string.IsNullOrEmpty((string)Data.pur_packun) || (string)Data.pur_packun == "0.00" ? "1" : (string)Data.pur_packun,
                    VatGroupPu = string.IsNullOrEmpty((string)Data.vat_gourp_pu) ? "NIG" : (string)Data.vat_gourp_pu,
                    VatGourpSa = string.IsNullOrEmpty((string)Data.sales_tax) ? "NOG" : (string)Data.sales_tax,
                    SalUnitMsr = (string)Data.sales_uom,
                    NumInSale = string.IsNullOrEmpty((string)Data.num_sale) || (string)Data.num_sale == "0.00" ? "1" : (string)Data.num_sale,
                    SalPackMsr = (string)Data.sales_pack,
                    SalPackUn = string.IsNullOrEmpty((string)Data.qty_pack) || (string)Data.qty_pack == "0.00" ? "1" : (string)Data.qty_pack,
                    GLMethod = string.IsNullOrEmpty((string)Data.gl_method) || (string)Data.gl_method.ToUpper() == "C" ? SAPbobsCOM.BoGLMethods.glm_ItemClass : (string)Data.gl_method.ToUpper() == "W" ? SAPbobsCOM.BoGLMethods.glm_WH : (string)Data.mat_type.ToUpper() == "L" ? SAPbobsCOM.BoGLMethods.glm_ItemLevel : SAPbobsCOM.BoGLMethods.glm_ItemClass,
                    InvntryUom = (string)Data.invntry_uom,
                    IWeight1 = (string)Data.ic_weight,
                    ByWh = string.IsNullOrEmpty((string)Data.by_wh) || (string)Data.by_wh == "N" ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO,
                    EvalSystem = string.IsNullOrEmpty((string)Data.eval_system) || (string)Data.eval_system.ToUpper() == "M" ? SAPbobsCOM.BoInventorySystem.bis_MovingAverage : (string)Data.eval_system.ToUpper() == "S" ? SAPbobsCOM.BoInventorySystem.bis_Standard : (string)Data.mat_type.ToUpper() == "F" ? SAPbobsCOM.BoInventorySystem.bis_FIFO : (string)Data.mat_type.ToUpper() == "B" ? SAPbobsCOM.BoInventorySystem.bis_SNB : SAPbobsCOM.BoInventorySystem.bis_MovingAverage,


                    //Code = (string)Data.code_vat,
                    //Name = (string)Data.vat_name,
                    //Category = string.IsNullOrEmpty((string)Data.category) ? SAPbobsCOM.BoVatCategoryEnum.bovcOutputTax : (string)Data.category == "I" ? SAPbobsCOM.BoVatCategoryEnum.bovcInputTax : SAPbobsCOM.BoVatCategoryEnum.bovcOutputTax,
                    //Account = (string)Data.account,
                    //DeferrAcc = (string)Data.deferracc,
                    //NonDedct = string.IsNullOrEmpty((string)Data.m_vat) ? 0 : Convert.ToDouble((string)Data.m_vat),
                    //Inactive = (string)Data.status == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,

                };

                try
                {
                    SAPB1.CreateUpdateItemMaster(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Item Groups #####
        private string CreateUpdateItemGroups(dynamic Data)
        {
            string docNo = (string)Data.matgroup_code;
            string methodName = "CMCreateUpdateItemGroups";
            string refDescription = "mat_gr";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateItemGroups_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OITB",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.ItemGroup ObjSave = new SAPB1.ItemGroup
                {
                    ItmsGrpCod = Convert.ToInt32(Data.sap_code),
                    ItmsGrpNam = (string)Data.matgroup_name,
                    BalInvntAc = (string)Data.ic_acct,
                    SaleCostAc = (string)Data.sale_cost_acct,
                    TransferAc = (string)Data.transfer_acct,
                    PriceDifAc = (string)Data.price_dif_acct

                };

                try
                {
                    SAPB1.CreateUpdateItemGroupMaster(ObjSave, out SAPStatusCode, out SAPErrorMessage, out DocNum);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Business Partner #####
        private string CreateUpdateBusinessPartner(dynamic Data)
        {
            string docNo = (string)Data.vender_code;
            string methodName = "CreateUpdateBusinessPartner";
            string refDescription = "business";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateBusinessPartner_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OCRD",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.BusinessPartner ObjSave = new SAPB1.BusinessPartner
                {
                    //Locked = (string)Data.locked == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    //Series = (string)Data.series,
                    CardCode = (string)Data.vender_code,
                    CardName = (string)Data.vender_name,
                    CardFNme = (string)Data.cardfnme,
                    CardType = (string)Data.vender_type == "L" ? SAPbobsCOM.BoCardTypes.cLid : (string)Data.vender_type == "C" ? SAPbobsCOM.BoCardTypes.cCustomer : SAPbobsCOM.BoCardTypes.cSupplier,
                    CmpPrivate = (string)Data.cmpprivate == "I" ? SAPbobsCOM.BoCardCompanyTypes.cPrivate : SAPbobsCOM.BoCardCompanyTypes.cCompany,
                    GroupCode = (string)Data.groupcode,
                    Currency = (string)Data.currency,
                    LicTradNum = (string)Data.vender_tax,
                    Remarks = (string)Data.remarks,
                    Phone1 = (string)Data.vender_tel,
                    Phone2 = (string)Data.vender_tel2,
                    Fax = (string)Data.vender_fax,
                    E_Mail = (string)Data.e_mail,
                    IntrntSite = (string)Data.website,
                    AliasName = (string)Data.alias_name,
                    CntctPrsn = (string)Data.cntct_prsn,
                    Territory = (string)Data.territory,
                    GlblLocNum = (string)Data.glbl_loc_num,
                    GroupNum = (string)Data.glbl_loc_num == "0" ? "" : (string)Data.glbl_loc_num,
                    ValidFor = (string)Data.valid_for == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES,
                    FrozenFor = (string)Data.valid_for == "N" ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO,
                    Discount = (string)Data.payment_discount == "0" ? "" : (string)Data.payment_discount,
                    DebPayAcct = (string)Data.ar_payable,
                    VatStatus    = (string)Data.vat_status == "N" ? SAPbobsCOM.BoVatStatus.vExempted : (string)Data.vat_status == "C" ? SAPbobsCOM.BoVatStatus.vEC : SAPbobsCOM.BoVatStatus.vLiable,
                    ECVatGroup = (string)Data.tax_group,
                    WTLiable     = (string)Data.wt_liable == "Y" ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO,
                    TypWTReprt   = (string)Data.cmpprivate == "P" ? SAPbobsCOM.AssesseeTypeEnum.atOthers : SAPbobsCOM.AssesseeTypeEnum.atCompany,
                };

                try
                {
                    SAPB1.CreateUpdateBusinessPartner(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Business Contact Persons #####
        private string CreateUpdateBusinessContactPersons(dynamic Data)
        {
            string docNo = (string)Data.contact_code;
            string methodName = "CreateUpdateBusinessContactPersons";
            string refDescription = "contact";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateBusinessBank_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OCRB",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.ContactPersons ObjSave = new SAPB1.ContactPersons
                {
                    CntCtCode = (string)Data.contact_code,
                    CardCode = (string)Data.vender_code,
                    Name = string.IsNullOrEmpty((string)Data.bank_country) ? "TH" : (string)Data.bank_country,
                    FirstName = (string)Data.bank_code,
                    LastName = (string)Data.acc_code,
                    Position = Data.acc_name,
                    Tel1 = (string)Data.branch_code,
                    Tel2 = (string)Data.swift_code,
                    Cellolar = (string)Data.xxx,
                    Fax = (string)Data.xxx,
                    E_MaiL = (string)Data.xxx,
                    Notes1 = (string)Data.xxx,
                    Notes2 = (string)Data.xxx,
                    Active = (string)Data.xxx,


                };

                try
                {
                    SAPB1.CreateUpdateBusinessContactPersons(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Business Address #####
        private string CreateUpdateBusinessAddress(dynamic Data)
        {
            string docNo = (string)Data.vender_code;
            string methodName = "CreateUpdateBusinessAddress";
            string refDescription = "vender_a";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateBusinessAddress_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "CRD1",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.BusinessPartnerAddress ObjSave = new SAPB1.BusinessPartnerAddress
                {
                    CardCode = (string)Data.vender_code,
                    Address = (string)Data.vender_address,
                    AdresType = (string)Data.address_type,
                    Street = (string)Data.street,
                    Block = Data.block,
                    City = (string)Data.city,
                    County = (string)Data.county,
                    Country = (string)Data.country,
                    ZipCode = (string)Data.zip_code,


                };

                try
                {
                    SAPB1.CreateUpdateBusinessAddress(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Business Partner Groups #####
        private string CreateUpdateBusinessPartnerGroups(dynamic Data)
        {
            string docNo = (string)Data.group_code;
            string methodName = "CMCreateUpdateBusinessPartnerGroups";
            string refDescription = "business_b";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateItemGroups_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OITB",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.BusinessPartnerGroups ObjSave = new SAPB1.BusinessPartnerGroups
                {
                    GroupCode =(string)Data.sap_code,
                    GroupNum = (string)Data.group_num,
                    GroupType = (string)Data.group_type,

                };

                try
                {
                    SAPB1.CreateUpdateBusinessPartnerGroups(ObjSave, out SAPStatusCode, out SAPErrorMessage, out DocNum);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Business Bank #####
        private string CreateUpdateBusinessBank(dynamic Data)
        {
            string docNo = (string)Data.vender_code;
            string methodName = "CreateUpdateBusinessBank";
            string refDescription = "business_b";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateBusinessBank_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "OCRB",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);
            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.BusinessBank ObjSave = new SAPB1.BusinessBank
                {
                    CardCode = (string)Data.vender_code,
                    Country = string.IsNullOrEmpty((string)Data.bank_country) ? "TH" : (string)Data.bank_country,
                    BankCode = (string)Data.bank_code,
                    Account = (string)Data.acc_code,
                    AcctName = Data.acc_name,
                    Branch = (string)Data.branch_code,
                    IBAN = (string)Data.swift_code,

                };

                try
                {
                    SAPB1.CreateUpdateBusinessBank(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #region #### Create Update Banks ####
        private string CreateUpdateBanks(dynamic Data)
        {
            string docNo = (string)Data.bank_code;
            string methodName = "CreateUpdateBanks";
            string refDescription = "s_bank";
            string dbName = (string)Data.company_value;

            string TranID = string.Empty;
            string DocNum = string.Empty;
            string GLDocNum = string.Empty;
            ErrorMessage = string.Empty;
            HttpStatusCode ResponseCode = HttpStatusCode.OK;

            MySqlConnection conn = new MySqlConnection(mySQLConn);
            conn.Open();

            List<SAP_Interface_Log_Detail> LogDetail = new List<SAP_Interface_Log_Detail>();
            SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                    "CreateUpdateBanks_CM",
                    methodName,
                    docNo,
                    refDescription,
                    null,
                    "ODSC",
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data),
                    TranBy);

            try
            {

                if (string.IsNullOrEmpty(dbName))
                {
                    throw new Exception("company_value not found!!!!");
                }

                ICON.SAP.API.SAPB1 SAPB1 = oSAPB1(dbName);
                ICON.SAP.API.SAPB1.Banks ObjSave = new SAPB1.Banks
                {
                    CountryCod = string.IsNullOrEmpty((string)Data.bank_country) ? "TH" : (string)Data.bank_country,
                    BankCode = (string)Data.bank_code,
                    BankName = (string)Data.bank_name,                    
                };

                try
                {
                    SAPB1.CreateUpdateBanks(ObjSave, out SAPStatusCode, out SAPErrorMessage);
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

                conn.Dispose();
                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     docNo,
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

        #endregion
    }
}
