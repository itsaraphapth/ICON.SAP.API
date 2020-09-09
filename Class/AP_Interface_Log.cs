/*
** Company Name     :  Trinity Solution Provider Co., Ltd.
** Contact          :  www.iconframework.co.th
** Product Name     :  Data Access framework(Class generator) (2012)
** Modify by        :  Anupong kwanpigul
** Modify Date      : 2019-05-16 09:43:33
 ** Version          : 1.0.0.10
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection;
using ICON.Framework.Provider;
using ICON.Framework.Provider.QueryBuilder;
using ICON.Framework.Provider.QueryBuilder.Enums;

namespace ICON.Interface
{
	[Serializable]
	[XmlRootAttribute("AP_Interface_Log", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class AP_Interface_Log
	{
		[XmlIgnore]
		[NonSerialized]
		public AP_Interface_Log_Command ExecCommand = null;
		public AP_Interface_Log()
		{
			ExecCommand = new AP_Interface_Log_Command(this);
		}
		public AP_Interface_Log(string ConnectionStr)
		{
			ExecCommand = new AP_Interface_Log_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new AP_Interface_Log_Command(ConnectionStr, this);
		}
		public AP_Interface_Log(string ConnectionStr, Int32 TranID_Value)
		{
			ExecCommand = new AP_Interface_Log_Command(ConnectionStr, this);
			ExecCommand.Load(TranID_Value);
		}
        private Int32 _TranID ;
		[XmlIgnore]
		public bool EditTranID = false;
		public Int32 TranID
		{
			get
			{
				return _TranID;
			}
			set
			{
				
				if(_TranID != value){
					_TranID = value;
					EditTranID = true;
				}
			}
		}

        private String _Module ;
        private int _Module_Limit = 50;
		[XmlIgnore]
		public bool EditModule = false;
		public String Module
		{
			get
			{
				return _Module;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Module_Limit)
					throw new Exception("String length longer then prescribed on Module property.");
				if(_Module != value){
					_Module = value;
					EditModule = true;
				}
			}
		}

        private String _MethodName ;
        private int _MethodName_Limit = 50;
		[XmlIgnore]
		public bool EditMethodName = false;
		public String MethodName
		{
			get
			{
				return _MethodName;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _MethodName_Limit)
					throw new Exception("String length longer then prescribed on MethodName property.");
				if(_MethodName != value){
					_MethodName = value;
					EditMethodName = true;
				}
			}
		}

        private String _APIRequestData ;
		[XmlIgnore]
		public bool EditAPIRequestData = false;
		public String APIRequestData
		{
			get
			{
				return _APIRequestData;
			}
			set
			{
				
				if(_APIRequestData != value){
					_APIRequestData = value;
					EditAPIRequestData = true;
				}
			}
		}

        private String _APIResponseData ;
		[XmlIgnore]
		public bool EditAPIResponseData = false;
		public String APIResponseData
		{
			get
			{
				return _APIResponseData;
			}
			set
			{
				
				if(_APIResponseData != value){
					_APIResponseData = value;
					EditAPIResponseData = true;
				}
			}
		}

        private Nullable<Int32> _APIResponseCode ;
		[XmlIgnore]
		public bool EditAPIResponseCode = false;
		public Nullable<Int32> APIResponseCode
		{
			get
			{
				return _APIResponseCode;
			}
			set
			{
				
				if(_APIResponseCode != value){
					_APIResponseCode = value;
					EditAPIResponseCode = true;
				}
			}
		}

        private String _APIErrorMessage ;
		[XmlIgnore]
		public bool EditAPIErrorMessage = false;
		public String APIErrorMessage
		{
			get
			{
				return _APIErrorMessage;
			}
			set
			{
				
				if(_APIErrorMessage != value){
					_APIErrorMessage = value;
					EditAPIErrorMessage = true;
				}
			}
		}

        private Nullable<DateTime> _CreateDate ;
		[XmlIgnore]
		public bool EditCreateDate = false;
		public Nullable<DateTime> CreateDate
		{
			get
			{
				return _CreateDate;
			}
			set
			{
				
				if(_CreateDate != value){
					_CreateDate = value;
					EditCreateDate = true;
				}
			}
		}

        private String _CreateBy ;
        private int _CreateBy_Limit = 50;
		[XmlIgnore]
		public bool EditCreateBy = false;
		public String CreateBy
		{
			get
			{
				return _CreateBy;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _CreateBy_Limit)
					throw new Exception("String length longer then prescribed on CreateBy property.");
				if(_CreateBy != value){
					_CreateBy = value;
					EditCreateBy = true;
				}
			}
		}

        private Nullable<DateTime> _UpdateDate ;
		[XmlIgnore]
		public bool EditUpdateDate = false;
		public Nullable<DateTime> UpdateDate
		{
			get
			{
				return _UpdateDate;
			}
			set
			{
				
				if(_UpdateDate != value){
					_UpdateDate = value;
					EditUpdateDate = true;
				}
			}
		}

        private String _UpdateBy ;
        private int _UpdateBy_Limit = 50;
		[XmlIgnore]
		public bool EditUpdateBy = false;
		public String UpdateBy
		{
			get
			{
				return _UpdateBy;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _UpdateBy_Limit)
					throw new Exception("String length longer then prescribed on UpdateBy property.");
				if(_UpdateBy != value){
					_UpdateBy = value;
					EditUpdateBy = true;
				}
			}
		}

	}
	public partial class AP_Interface_Log_Command
	{
		string TableName = "AP_Interface_Log";

		AP_Interface_Log _AP_Interface_Log = null;
		DBHelper _DBHelper = null;

		internal AP_Interface_Log_Command(AP_Interface_Log obj_AP_Interface_Log)
		{
			this._AP_Interface_Log = obj_AP_Interface_Log;
			this._DBHelper = new DBHelper();
		}

		internal AP_Interface_Log_Command(string ConnectionStr,AP_Interface_Log obj_AP_Interface_Log)
		{
			this._AP_Interface_Log = obj_AP_Interface_Log;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _AP_Interface_Log.GetType().GetProperties())
			{
				_AP_Interface_Log.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Interface_Log, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_AP_Interface_Log,dr[ProInfo.Name],null);
					    //_AP_Interface_Log.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Interface_Log, true);
                    }
				}
			}
		}

        public void Load(Int32 TranID_Value)
        {
            Load(_DBHelper, TranID_Value);
        }

		public void Load(DBHelper _DBHelper ,Int32 TranID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@TranID", TranID_Value, (DbType)Enum.Parse(typeof(DbType), TranID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Interface_Log WHERE TranID=@TranID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper ,IDbTransaction Trans ,Int32 TranID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@TranID", TranID_Value, (DbType)Enum.Parse(typeof(DbType), TranID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Interface_Log WHERE TranID=@TranID", "AP_Interface_Log", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("AP_Interface_Log");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<AP_Interface_Log> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<AP_Interface_Log> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("AP_Interface_Log");
            if (Where != null)
            {
                foreach (WhereClause Item in Where)
                {
                    Sqb.AddWhere(Item);
                }
            }
            if (OrderBy != null)
            {
                foreach (OrderByClause Item in OrderBy)
                {
                    Sqb.AddOrderBy(Item);
                }
            }

            DataTable dt = LoadByQueryBuilder(Sqb);
            List<AP_Interface_Log> Res = new List<AP_Interface_Log>();
            foreach(DataRow Dr in dt.Rows)
            {
                AP_Interface_Log Item = new AP_Interface_Log();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO AP_Interface_Log (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _AP_Interface_Log.GetType().GetProperties())
			{
				if ((bool)_AP_Interface_Log.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Interface_Log)  && ProInfo.Name != "TranID")
				{
                    object Value = ProInfo.GetValue(_AP_Interface_Log, null);
                    if (Value == null) Value = DBNull.Value;
					if (extColumn != "") extColumn += ",";
					extColumn += ProInfo.Name ;

					if (extParameter != "") extParameter += ",";
					extParameter += "@" + ProInfo.Name ;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
			}
            if (extColumn == "") return "";
			ssql += extColumn + ") VALUES(" + extParameter + ")";
            
            string GetIdentityComm = string.Empty;
            if (_DBHelper.Provider == "System.Data.SqlClient")
                GetIdentityComm = "SELECT @@IDENTITY";
            else if (_DBHelper.Provider == "MySql.Data.MySqlClient")
                GetIdentityComm = "SELECT LAST_INSERT_ID()";
            return ssql + ";" + GetIdentityComm;
                    
        }

        public void Insert(DBHelper DBHelp, IDbTransaction DbTransaction)
		{
            DBParameterCollection Parameter = null;
            string SqlInsert = GetInsertCommand(out Parameter);
            if (SqlInsert == "") return;
            _AP_Interface_Log.TranID = int.Parse(DBHelp.ExecuteScalar(SqlInsert, Parameter, DbTransaction).ToString());

		}

        public void Insert()
        {
            DBParameterCollection Parameter = null;
            string SqlInsert = GetInsertCommand(out Parameter);
            if (SqlInsert == "") return;
            _AP_Interface_Log.TranID = int.Parse(_DBHelper.ExecuteScalar(SqlInsert, Parameter).ToString());

        }
            
        private string GetUpdateCommand(out DBParameterCollection Parameter)
        {
            Parameter = new DBParameterCollection();
			string ssql = "UPDATE AP_Interface_Log SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _AP_Interface_Log.GetType().GetProperties())
			{
				if ((bool)_AP_Interface_Log.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Interface_Log)  && ProInfo.Name != "TranID")
				{
                    object Value = ProInfo.GetValue(_AP_Interface_Log, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "TranID")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_AP_Interface_Log, null)));
				}
			}
			ssql += extSql;
			ssql += " WHERE TranID=@TranID";
            if (extSql == "") return "";
            return ssql;
        }

        public void Update(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            DBParameterCollection Parameter = null;
            string SqlUpdate = GetUpdateCommand(out Parameter);
            if (SqlUpdate == "") return;
            DBHelp.ExecuteNonQuery(SqlUpdate, Parameter, DbTransaction);
        }

		public void Update()
		{
            DBParameterCollection Parameter = null;
            string SqlUpdate = GetUpdateCommand(out Parameter);
            if (SqlUpdate == "") return;
            _DBHelper.ExecuteNonQuery(SqlUpdate, Parameter);
		}
            
        private string GetDeleteCommand(out DBParameterCollection Parameter)
        {
            Parameter = new DBParameterCollection();
            string ssql = "DELETE AP_Interface_Log WHERE TranID=@TranID";
            Parameter.Add(new DBParameter("@TranID", _AP_Interface_Log.TranID));

            return ssql;
        }

		public void Delete()
		{
			DBParameterCollection Parameter = new DBParameterCollection();
            string SqlDelete = GetDeleteCommand(out Parameter);
            _DBHelper.ExecuteNonQuery(SqlDelete, Parameter);
		}

        public void Delete(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            DBParameterCollection Parameter = new DBParameterCollection();
            string SqlDelete = GetDeleteCommand(out Parameter);
            DBHelp.ExecuteNonQuery(SqlDelete, Parameter, DbTransaction);
        }
	}
}
