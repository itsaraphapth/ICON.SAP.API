using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICON.Configuration
{
    public static class Database
    {
        public static string REM_ConnectionString
        {
            get
            {
                string server = System.Configuration.ConfigurationManager.AppSettings["rem_host"].ToString();
                string database = System.Configuration.ConfigurationManager.AppSettings["rem_dbname"].ToString();
                string user = System.Configuration.ConfigurationManager.AppSettings["rem_user"].ToString();
                string password = System.Configuration.ConfigurationManager.AppSettings["rem_password"].ToString();
                return string.Format("Persist Security Info=False;Initial Catalog={1};Data Source={0};User ID={2};Password={3}", server, database, user, password);
            }
        }

        public static string AP_ConnectionString
        {
            get
            {
                string server = System.Configuration.ConfigurationManager.AppSettings["ap_host"].ToString();
                string database = System.Configuration.ConfigurationManager.AppSettings["ap_dbname"].ToString();
                string user = System.Configuration.ConfigurationManager.AppSettings["ap_user"].ToString();
                string password = System.Configuration.ConfigurationManager.AppSettings["ap_password"].ToString();
                return string.Format("Persist Security Info=False;Initial Catalog={1};Data Source={0};User ID={2};Password={3}", server, database, user, password);
            }
        }

        public static string CM_ConnectionString
        {
            get
            {
                string server = System.Configuration.ConfigurationManager.AppSettings["cm_host"].ToString();
                string database = System.Configuration.ConfigurationManager.AppSettings["cm_dbname"].ToString();
                string user = System.Configuration.ConfigurationManager.AppSettings["cm_user"].ToString();
                string password = System.Configuration.ConfigurationManager.AppSettings["cm_password"].ToString();
                string port = "3306";
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["cm_port"])) port = System.Configuration.ConfigurationManager.AppSettings["cm_port"].ToString();
                return string.Format("server={0}; port={1}; User Id={2}; Password={3}; Database={4};Allow Zero Datetime=True; CharSet=UTF8;", server, port, user, password, database);
            }
        }
        public static string Log_ConnectionString
        {
            get
            {
                string server = System.Configuration.ConfigurationManager.AppSettings["log_host"].ToString();
                string database = System.Configuration.ConfigurationManager.AppSettings["log_dbname"].ToString();
                string user = System.Configuration.ConfigurationManager.AppSettings["log_user"].ToString();
                string password = System.Configuration.ConfigurationManager.AppSettings["log_password"].ToString();
                return string.Format("Persist Security Info=False;Initial Catalog={1};Data Source={0};User ID={2};Password={3}", server, database, user, password);
            }
        }

        public static SAPServer SAP_Server
        {
            get
            {
                string dbserver = System.Configuration.ConfigurationManager.AppSettings["sap_dbserver"].ToString();
                string dbservertype = System.Configuration.ConfigurationManager.AppSettings["sap_dbservertype"].ToString();
                string companydb = System.Configuration.ConfigurationManager.AppSettings["sap_companydb"].ToString();
                string username = System.Configuration.ConfigurationManager.AppSettings["sap_username"].ToString();
                string password = System.Configuration.ConfigurationManager.AppSettings["sap_password"].ToString();
                string license_server = System.Configuration.ConfigurationManager.AppSettings["sap_license_server"].ToString();
                string sld_address = System.Configuration.ConfigurationManager.AppSettings["sap_sld_address"].ToString();
                string language = System.Configuration.ConfigurationManager.AppSettings["sap_language"].ToString();

                SAPbobsCOM.BoDataServerTypes DBServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
                SAPbobsCOM.BoSuppLangs Lang = SAPbobsCOM.BoSuppLangs.ln_English;
                switch (dbservertype)
                {
                    case "MSSQL":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL;
                        break;
                    case "DB_2":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_DB_2;
                        break;
                    case "SYBASE":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_SYBASE;
                        break;
                    case "MSSQL2005":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005;
                        break;
                    case "MAXDB":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_MAXDB;
                        break;
                    case "MSSQL2008":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                        break;
                    case "MSSQL2012":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                        break;
                    case "MSSQL2014":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                        break;
                    case "HANADB":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB;
                        break;
                    case "MSSQL2016":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
                        break;
                    case "MSSQL2017":
                        DBServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017;
                        break;
                }
                switch (language.ToLower())
                {
                    case "en":
                        Lang = SAPbobsCOM.BoSuppLangs.ln_English;
                        break;
                }

                SAPServer Server = new SAPServer
                {
                    DBServer = dbserver,
                    DBServerType  = DBServerType,
                    CompanyDB = companydb,
                    UserName = username,
                    Password = password,
                    LicenseServer = license_server,
                    SLDAddress = sld_address,
                    Language = Lang
                };

                return Server;
            }
        }

        public static ICON.Provider.Connection ConnectionPhase1
        {
            get
            {
                return new ICON.Provider.Connection(Database.REM_ConnectionString);
            }
        }

        public static ICON.Framework.Provider.DBHelper ConnectionPhase2
        {
            get
            {
                return new ICON.Framework.Provider.DBHelper(Database.REM_ConnectionString, null);
            }
        }
    }

    public static class Util
    {
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
    public class SAPServer
    {
        public string DBServer { get; set; }
        public SAPbobsCOM.BoDataServerTypes DBServerType { get; set; }
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LicenseServer { get; set; }
        public string SLDAddress { get; set; }
        public SAPbobsCOM.BoSuppLangs Language { get; set; }
    }
}