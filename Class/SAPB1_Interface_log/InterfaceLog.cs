/*
** Company Name     :  Trinity Solution Provider Co., Ltd. && ICON Framework Co.,Ltd.
** Contact          :  www.iconrem.com
** Product Name     :  Data Access framework(Class generator) (2012)
** Product by       :  Anupong Kwanpigul
** Modify by        :  Yuttapong Benjapornraksa
** Modify Date      :  2020-02-03 22:35:55
** Version          :  1.0.0.10
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
	[XmlRootAttribute("InterfaceLog", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class InterfaceLog
	{
		[XmlIgnore]
		[NonSerialized]
		public InterfaceLog_Command ExecCommand = null;
		public InterfaceLog()
		{
			ExecCommand = new InterfaceLog_Command(this);
		}
		public InterfaceLog(string ConnectionStr)
		{
			ExecCommand = new InterfaceLog_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new InterfaceLog_Command(ConnectionStr, this);
		}
		public InterfaceLog(string ConnectionStr, String InterfaceLogID_Value)
		{
			ExecCommand = new InterfaceLog_Command(ConnectionStr, this);
			ExecCommand.Load(InterfaceLogID_Value);
		}
        private String _InterfaceLogID;
        public readonly bool _InterfaceLogID_PKFlag = true;
        public readonly bool _InterfaceLogID_IDTFlag = false;
		[XmlIgnore]
		public bool EditInterfaceLogID = false;
		public String InterfaceLogID
		{
			get
			{
				return _InterfaceLogID;
			}
			set
			{
				if (_InterfaceLogID != value)
                {
					_InterfaceLogID = value;
					EditInterfaceLogID = true;
				}
			}
		}

        private String _Module;
        public readonly bool _Module_PKFlag = false;
        public readonly bool _Module_IDTFlag = false;
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
				if (_Module != value)
                {
					_Module = value;
					EditModule = true;
				}
			}
		}

        private String _MethodName;
        public readonly bool _MethodName_PKFlag = false;
        public readonly bool _MethodName_IDTFlag = false;
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
				if (_MethodName != value)
                {
					_MethodName = value;
					EditMethodName = true;
				}
			}
		}

        private String _REMCompanyID;
        public readonly bool _REMCompanyID_PKFlag = false;
        public readonly bool _REMCompanyID_IDTFlag = false;
		[XmlIgnore]
		public bool EditREMCompanyID = false;
		public String REMCompanyID
		{
			get
			{
				return _REMCompanyID;
			}
			set
			{
				if (_REMCompanyID != value)
                {
					_REMCompanyID = value;
					EditREMCompanyID = true;
				}
			}
		}

        private String _REMRefID;
        public readonly bool _REMRefID_PKFlag = false;
        public readonly bool _REMRefID_IDTFlag = false;
		[XmlIgnore]
		public bool EditREMRefID = false;
		public String REMRefID
		{
			get
			{
				return _REMRefID;
			}
			set
			{
				if (_REMRefID != value)
                {
					_REMRefID = value;
					EditREMRefID = true;
				}
			}
		}

        private String _REMRefDescription;
        public readonly bool _REMRefDescription_PKFlag = false;
        public readonly bool _REMRefDescription_IDTFlag = false;
		[XmlIgnore]
		public bool EditREMRefDescription = false;
		public String REMRefDescription
		{
			get
			{
				return _REMRefDescription;
			}
			set
			{
				if (_REMRefDescription != value)
                {
					_REMRefDescription = value;
					EditREMRefDescription = true;
				}
			}
		}

        private String _SAPRefID;
        public readonly bool _SAPRefID_PKFlag = false;
        public readonly bool _SAPRefID_IDTFlag = false;
		[XmlIgnore]
		public bool EditSAPRefID = false;
		public String SAPRefID
		{
			get
			{
				return _SAPRefID;
			}
			set
			{
				if (_SAPRefID != value)
                {
					_SAPRefID = value;
					EditSAPRefID = true;
				}
			}
		}

        private String _SAPRefDescription;
        public readonly bool _SAPRefDescription_PKFlag = false;
        public readonly bool _SAPRefDescription_IDTFlag = false;
		[XmlIgnore]
		public bool EditSAPRefDescription = false;
		public String SAPRefDescription
		{
			get
			{
				return _SAPRefDescription;
			}
			set
			{
				if (_SAPRefDescription != value)
                {
					_SAPRefDescription = value;
					EditSAPRefDescription = true;
				}
			}
		}

        private String _SAPRefNo;
        public readonly bool _SAPRefNo_PKFlag = false;
        public readonly bool _SAPRefNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditSAPRefNo = false;
		public String SAPRefNo
		{
			get
			{
				return _SAPRefNo;
			}
			set
			{
				if (_SAPRefNo != value)
                {
					_SAPRefNo = value;
					EditSAPRefNo = true;
				}
			}
		}

        private String _SAPRefGLNo;
        public readonly bool _SAPRefGLNo_PKFlag = false;
        public readonly bool _SAPRefGLNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditSAPRefGLNo = false;
		public String SAPRefGLNo
		{
			get
			{
				return _SAPRefGLNo;
			}
			set
			{
				if (_SAPRefGLNo != value)
                {
					_SAPRefGLNo = value;
					EditSAPRefGLNo = true;
				}
			}
		}

        private String _APIRequestData;
        public readonly bool _APIRequestData_PKFlag = false;
        public readonly bool _APIRequestData_IDTFlag = false;
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
				if (_APIRequestData != value)
                {
					_APIRequestData = value;
					EditAPIRequestData = true;
				}
			}
		}

        private Nullable<Int32> _APIResponseCode;
        public readonly bool _APIResponseCode_PKFlag = false;
        public readonly bool _APIResponseCode_IDTFlag = false;
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
				if (_APIResponseCode != value)
                {
					_APIResponseCode = value;
					EditAPIResponseCode = true;
				}
			}
		}

        private String _APIErrorMessage;
        public readonly bool _APIErrorMessage_PKFlag = false;
        public readonly bool _APIErrorMessage_IDTFlag = false;
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
				if (_APIErrorMessage != value)
                {
					_APIErrorMessage = value;
					EditAPIErrorMessage = true;
				}
			}
		}

        private String _SAPStatusCode;
        public readonly bool _SAPStatusCode_PKFlag = false;
        public readonly bool _SAPStatusCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditSAPStatusCode = false;
		public String SAPStatusCode
		{
			get
			{
				return _SAPStatusCode;
			}
			set
			{
				if (_SAPStatusCode != value)
                {
					_SAPStatusCode = value;
					EditSAPStatusCode = true;
				}
			}
		}

        private String _SAPErrorMessage;
        public readonly bool _SAPErrorMessage_PKFlag = false;
        public readonly bool _SAPErrorMessage_IDTFlag = false;
		[XmlIgnore]
		public bool EditSAPErrorMessage = false;
		public String SAPErrorMessage
		{
			get
			{
				return _SAPErrorMessage;
			}
			set
			{
				if (_SAPErrorMessage != value)
                {
					_SAPErrorMessage = value;
					EditSAPErrorMessage = true;
				}
			}
		}

        private Nullable<DateTime> _CreateDate;
        public readonly bool _CreateDate_PKFlag = false;
        public readonly bool _CreateDate_IDTFlag = false;
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
				if (_CreateDate != value)
                {
					_CreateDate = value;
					EditCreateDate = true;
				}
			}
		}

        private String _CreateBy;
        public readonly bool _CreateBy_PKFlag = false;
        public readonly bool _CreateBy_IDTFlag = false;
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
				if (_CreateBy != value)
                {
					_CreateBy = value;
					EditCreateBy = true;
				}
			}
		}

        private Nullable<DateTime> _UpdateDate;
        public readonly bool _UpdateDate_PKFlag = false;
        public readonly bool _UpdateDate_IDTFlag = false;
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
				if (_UpdateDate != value)
                {
					_UpdateDate = value;
					EditUpdateDate = true;
				}
			}
		}

        private String _UpdateBy;
        public readonly bool _UpdateBy_PKFlag = false;
        public readonly bool _UpdateBy_IDTFlag = false;
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
				if (_UpdateBy != value)
                {
					_UpdateBy = value;
					EditUpdateBy = true;
				}
			}
		}

        private Nullable<Int32> _RetryCount;
        public readonly bool _RetryCount_PKFlag = false;
        public readonly bool _RetryCount_IDTFlag = false;
		[XmlIgnore]
		public bool EditRetryCount = false;
		public Nullable<Int32> RetryCount
		{
			get
			{
				return _RetryCount;
			}
			set
			{
				if (_RetryCount != value)
                {
					_RetryCount = value;
					EditRetryCount = true;
				}
			}
		}

        private String _APIResponseData;
        public readonly bool _APIResponseData_PKFlag = false;
        public readonly bool _APIResponseData_IDTFlag = false;
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
				if (_APIResponseData != value)
                {
					_APIResponseData = value;
					EditAPIResponseData = true;
				}
			}
		}

        private String _RefInterfaceLogID;
        public readonly bool _RefInterfaceLogID_PKFlag = false;
        public readonly bool _RefInterfaceLogID_IDTFlag = false;
		[XmlIgnore]
		public bool EditRefInterfaceLogID = false;
		public String RefInterfaceLogID
		{
			get
			{
				return _RefInterfaceLogID;
			}
			set
			{
				if (_RefInterfaceLogID != value)
                {
					_RefInterfaceLogID = value;
					EditRefInterfaceLogID = true;
				}
			}
		}

	}
	public partial class InterfaceLog_Command
	{
		string TableName = "InterfaceLog";

		InterfaceLog _InterfaceLog = null;
		DBHelper _DBHelper = null;

		internal InterfaceLog_Command(InterfaceLog obj_InterfaceLog)
		{
			this._InterfaceLog = obj_InterfaceLog;
			this._DBHelper = new DBHelper();
		}

		internal InterfaceLog_Command(string ConnectionStr, InterfaceLog obj_InterfaceLog)
		{
			this._InterfaceLog = obj_InterfaceLog;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _InterfaceLog.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _InterfaceLog.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_InterfaceLog, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_InterfaceLog, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String InterfaceLogID_Value)
        {
            Load(_DBHelper, InterfaceLogID_Value);
        }

		public void Load(DBHelper _DBHelper, String InterfaceLogID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@InterfaceLogID", InterfaceLogID_Value, (DbType)Enum.Parse(typeof(DbType), InterfaceLogID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM InterfaceLog WHERE InterfaceLogID=@InterfaceLogID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String InterfaceLogID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@InterfaceLogID", InterfaceLogID_Value, (DbType)Enum.Parse(typeof(DbType), InterfaceLogID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM InterfaceLog WHERE InterfaceLogID=@InterfaceLogID", "InterfaceLog", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("InterfaceLog");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<InterfaceLog> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<InterfaceLog> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("InterfaceLog");
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
            List<InterfaceLog> Res = new List<InterfaceLog>();
            foreach (DataRow Dr in dt.Rows)
            {
                InterfaceLog Item = new InterfaceLog();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _InterfaceLog.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _InterfaceLog.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_InterfaceLog);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _InterfaceLog.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _InterfaceLog.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_InterfaceLog);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _InterfaceLog.GetType();
                Type BaseType = MyType.BaseType;
                Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

                System.Text.StringBuilder OverLimitMsg = new System.Text.StringBuilder();
                System.Text.StringBuilder SQL = new System.Text.StringBuilder();
                SQL.AppendLine("SELECT");
                SQL.AppendLine("	c.name				AS ColumnName");
                SQL.AppendLine("	, t.name			AS TypeName");
                SQL.AppendLine("	, c.max_length		AS MaxLength");
                SQL.AppendLine("FROM sys.columns c");
                SQL.AppendLine("INNER JOIN sys.objects tb ON c.object_id = tb.object_id");
                SQL.AppendLine("INNER JOIN sys.types t ON c.user_type_id = t.user_type_id");
                SQL.AppendLine("WHERE tb.name = '" + TableType.Name + "'");
                System.Data.DataTable DT = DBHelp.ExecuteDataTable(SQL.ToString(), DbTransaction);

                foreach (PropertyInfo ProInfo in _InterfaceLog.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _InterfaceLog.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_InterfaceLog))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_InterfaceLog, null);
                            string ValueStr = Value == null ? "" : Value.ToString();
                            if (ValueStr.Length > MaxLength)
                            {
                                OverLimitMsg.AppendFormat("String length longer then prescribed on {1} property.({2}>{3}){0}", Environment.NewLine, ProInfo.Name, ValueStr.Length, MaxLength);
                            }
                        }
                    }
                }

                return OverLimitMsg.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
            
        private string GetInsertCommand(DBHelper DBHelp, out DBParameterCollection Parameter)
        {
            List<string> IdentityInsertKey_List = this.GetIdentityInsertKey();
            Type MyType = _InterfaceLog.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _InterfaceLog.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _InterfaceLog.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_InterfaceLog) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_InterfaceLog, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
            }
            string SQL = "INSERT INTO " + TableType.Name + " (" + string.Join(",", Field_List.Select(x => x)) + ")";
            SQL += " VALUES(" + string.Join(",", Field_List.Select(x => "@" + x)) + ")";
            if (Field_List.Count == 0) return "";
            return SQL;
        }

        public void Insert(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                DBParameterCollection Parameter = null;
                string SqlInsert = GetInsertCommand(DBHelp, out Parameter);
                if (SqlInsert == "") return;
                DBHelp.ExecuteNonQuery(SqlInsert, Parameter, DbTransaction);
            }
            catch (Exception ex)
            {
                string LimitErrorMsg = GetOverLimitErrorMsg(DBHelp, DbTransaction);
                if (LimitErrorMsg != "") throw new Exception(ex.Message + Environment.NewLine + LimitErrorMsg);
                else throw new Exception(ex.Message);
            }
        }

        public void Insert()
        {
            try
            {
                DBParameterCollection Parameter = null;
                string SqlInsert = GetInsertCommand(_DBHelper, out Parameter);
                if (SqlInsert == "") return;
                _DBHelper.ExecuteNonQuery(SqlInsert, Parameter);
            }
            catch (Exception ex)
            {
                string LimitErrorMsg = GetOverLimitErrorMsg(_DBHelper, null);
                if (LimitErrorMsg != "") throw new Exception(ex.Message + Environment.NewLine + LimitErrorMsg);
                else throw new Exception(ex.Message);
            }
        }
            
        private string GetUpdateCommand(out DBParameterCollection Parameter)
        {
            List<string> PrimaryKey_List = this.GetPrimaryKey();
            Type MyType = _InterfaceLog.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _InterfaceLog.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _InterfaceLog.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_InterfaceLog) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_InterfaceLog, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_InterfaceLog, null)));
				}
            }
            string SQL = "UPDATE " + TableType.Name + " SET " + string.Join(",", Field_List.Select(x => x + "=@" + x));
            SQL += " WHERE " + string.Join(" AND ", PrimaryKey_List.Select(x => x + "=@" + x));
            if (Field_List.Count == 0) return "";
            return SQL;
        }

        public void Update(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                DBParameterCollection Parameter = null;
                string SqlUpdate = GetUpdateCommand(out Parameter);
                if (SqlUpdate == "") return;
                DBHelp.ExecuteNonQuery(SqlUpdate, Parameter, DbTransaction);
            }
            catch (Exception ex)
            {
                string LimitErrorMsg = GetOverLimitErrorMsg(_DBHelper, null);
                if (LimitErrorMsg != "") throw new Exception(ex.Message + Environment.NewLine + LimitErrorMsg);
                else throw new Exception(ex.Message);
            }
        }

        public void Update()
        {
            try
            {
                DBParameterCollection Parameter = null;
                string SqlUpdate = GetUpdateCommand(out Parameter);
                if (SqlUpdate == "") return;
                _DBHelper.ExecuteNonQuery(SqlUpdate, Parameter);
            }
            catch (Exception ex)
            {
                string LimitErrorMsg = GetOverLimitErrorMsg(_DBHelper, null);
                if (LimitErrorMsg != "") throw new Exception(ex.Message + Environment.NewLine + LimitErrorMsg);
                else throw new Exception(ex.Message);
            }
        }
            
        private string GetDeleteCommand(out DBParameterCollection Parameter)
        {
            Parameter = new DBParameterCollection();
            string ssql = "DELETE InterfaceLog WHERE InterfaceLogID=@InterfaceLogID";
            Parameter.Add(new DBParameter("@InterfaceLogID", _InterfaceLog.InterfaceLogID));

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
