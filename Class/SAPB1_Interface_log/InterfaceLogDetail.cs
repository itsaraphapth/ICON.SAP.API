/*
** Company Name     :  Trinity Solution Provider Co., Ltd. && ICON Framework Co.,Ltd.
** Contact          :  www.iconrem.com
** Product Name     :  Data Access framework(Class generator) (2012)
** Product by       :  Anupong Kwanpigul
** Modify by        :  Yuttapong Benjapornraksa
** Modify Date      :  2020-02-04 21:57:45
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
	[XmlRootAttribute("InterfaceLogDetail", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class InterfaceLogDetail
	{
		[XmlIgnore]
		[NonSerialized]
		public InterfaceLogDetail_Command ExecCommand = null;
		public InterfaceLogDetail()
		{
			ExecCommand = new InterfaceLogDetail_Command(this);
		}
		public InterfaceLogDetail(string ConnectionStr)
		{
			ExecCommand = new InterfaceLogDetail_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new InterfaceLogDetail_Command(ConnectionStr, this);
		}
		public InterfaceLogDetail(string ConnectionStr, String InterfaceLogDetailID_Value)
		{
			ExecCommand = new InterfaceLogDetail_Command(ConnectionStr, this);
			ExecCommand.Load(InterfaceLogDetailID_Value);
		}
        private String _InterfaceLogDetailID;
        public readonly bool _InterfaceLogDetailID_PKFlag = true;
        public readonly bool _InterfaceLogDetailID_IDTFlag = false;
		[XmlIgnore]
		public bool EditInterfaceLogDetailID = false;
		public String InterfaceLogDetailID
		{
			get
			{
				return _InterfaceLogDetailID;
			}
			set
			{
				if (_InterfaceLogDetailID != value)
                {
					_InterfaceLogDetailID = value;
					EditInterfaceLogDetailID = true;
				}
			}
		}

        private String _InterfaceLogID;
        public readonly bool _InterfaceLogID_PKFlag = false;
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

        private String _B1MethodName;
        public readonly bool _B1MethodName_PKFlag = false;
        public readonly bool _B1MethodName_IDTFlag = false;
		[XmlIgnore]
		public bool EditB1MethodName = false;
		public String B1MethodName
		{
			get
			{
				return _B1MethodName;
			}
			set
			{
				if (_B1MethodName != value)
                {
					_B1MethodName = value;
					EditB1MethodName = true;
				}
			}
		}

        private String _B1EventName;
        public readonly bool _B1EventName_PKFlag = false;
        public readonly bool _B1EventName_IDTFlag = false;
		[XmlIgnore]
		public bool EditB1EventName = false;
		public String B1EventName
		{
			get
			{
				return _B1EventName;
			}
			set
			{
				if (_B1EventName != value)
                {
					_B1EventName = value;
					EditB1EventName = true;
				}
			}
		}

        private String _REMMethodName;
        public readonly bool _REMMethodName_PKFlag = false;
        public readonly bool _REMMethodName_IDTFlag = false;
		[XmlIgnore]
		public bool EditREMMethodName = false;
		public String REMMethodName
		{
			get
			{
				return _REMMethodName;
			}
			set
			{
				if (_REMMethodName != value)
                {
					_REMMethodName = value;
					EditREMMethodName = true;
				}
			}
		}

        private String _REMEventName;
        public readonly bool _REMEventName_PKFlag = false;
        public readonly bool _REMEventName_IDTFlag = false;
		[XmlIgnore]
		public bool EditREMEventName = false;
		public String REMEventName
		{
			get
			{
				return _REMEventName;
			}
			set
			{
				if (_REMEventName != value)
                {
					_REMEventName = value;
					EditREMEventName = true;
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

        private Nullable<Int32> _REMResponseCode;
        public readonly bool _REMResponseCode_PKFlag = false;
        public readonly bool _REMResponseCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditREMResponseCode = false;
		public Nullable<Int32> REMResponseCode
		{
			get
			{
				return _REMResponseCode;
			}
			set
			{
				if (_REMResponseCode != value)
                {
					_REMResponseCode = value;
					EditREMResponseCode = true;
				}
			}
		}

        private String _REMErrorMessage;
        public readonly bool _REMErrorMessage_PKFlag = false;
        public readonly bool _REMErrorMessage_IDTFlag = false;
		[XmlIgnore]
		public bool EditREMErrorMessage = false;
		public String REMErrorMessage
		{
			get
			{
				return _REMErrorMessage;
			}
			set
			{
				if (_REMErrorMessage != value)
                {
					_REMErrorMessage = value;
					EditREMErrorMessage = true;
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

        private Nullable<DateTime> _ModifyDate;
        public readonly bool _ModifyDate_PKFlag = false;
        public readonly bool _ModifyDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditModifyDate = false;
		public Nullable<DateTime> ModifyDate
		{
			get
			{
				return _ModifyDate;
			}
			set
			{
				if (_ModifyDate != value)
                {
					_ModifyDate = value;
					EditModifyDate = true;
				}
			}
		}

        private String _ModifyBy;
        public readonly bool _ModifyBy_PKFlag = false;
        public readonly bool _ModifyBy_IDTFlag = false;
		[XmlIgnore]
		public bool EditModifyBy = false;
		public String ModifyBy
		{
			get
			{
				return _ModifyBy;
			}
			set
			{
				if (_ModifyBy != value)
                {
					_ModifyBy = value;
					EditModifyBy = true;
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

        private String _RefInterfaceLogDetailID;
        public readonly bool _RefInterfaceLogDetailID_PKFlag = false;
        public readonly bool _RefInterfaceLogDetailID_IDTFlag = false;
		[XmlIgnore]
		public bool EditRefInterfaceLogDetailID = false;
		public String RefInterfaceLogDetailID
		{
			get
			{
				return _RefInterfaceLogDetailID;
			}
			set
			{
				if (_RefInterfaceLogDetailID != value)
                {
					_RefInterfaceLogDetailID = value;
					EditRefInterfaceLogDetailID = true;
				}
			}
		}

	}
	public partial class InterfaceLogDetail_Command
	{
		string TableName = "InterfaceLogDetail";

		InterfaceLogDetail _InterfaceLogDetail = null;
		DBHelper _DBHelper = null;

		internal InterfaceLogDetail_Command(InterfaceLogDetail obj_InterfaceLogDetail)
		{
			this._InterfaceLogDetail = obj_InterfaceLogDetail;
			this._DBHelper = new DBHelper();
		}

		internal InterfaceLogDetail_Command(string ConnectionStr, InterfaceLogDetail obj_InterfaceLogDetail)
		{
			this._InterfaceLogDetail = obj_InterfaceLogDetail;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _InterfaceLogDetail.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _InterfaceLogDetail.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_InterfaceLogDetail, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_InterfaceLogDetail, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String InterfaceLogDetailID_Value)
        {
            Load(_DBHelper, InterfaceLogDetailID_Value);
        }

		public void Load(DBHelper _DBHelper, String InterfaceLogDetailID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@InterfaceLogDetailID", InterfaceLogDetailID_Value, (DbType)Enum.Parse(typeof(DbType), InterfaceLogDetailID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM InterfaceLogDetail WHERE InterfaceLogDetailID=@InterfaceLogDetailID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String InterfaceLogDetailID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@InterfaceLogDetailID", InterfaceLogDetailID_Value, (DbType)Enum.Parse(typeof(DbType), InterfaceLogDetailID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM InterfaceLogDetail WHERE InterfaceLogDetailID=@InterfaceLogDetailID", "InterfaceLogDetail", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("InterfaceLogDetail");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<InterfaceLogDetail> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<InterfaceLogDetail> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("InterfaceLogDetail");
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
            List<InterfaceLogDetail> Res = new List<InterfaceLogDetail>();
            foreach (DataRow Dr in dt.Rows)
            {
                InterfaceLogDetail Item = new InterfaceLogDetail();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _InterfaceLogDetail.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _InterfaceLogDetail.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_InterfaceLogDetail);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _InterfaceLogDetail.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _InterfaceLogDetail.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_InterfaceLogDetail);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _InterfaceLogDetail.GetType();
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

                foreach (PropertyInfo ProInfo in _InterfaceLogDetail.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _InterfaceLogDetail.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_InterfaceLogDetail))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_InterfaceLogDetail, null);
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
            Type MyType = _InterfaceLogDetail.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _InterfaceLogDetail.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _InterfaceLogDetail.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_InterfaceLogDetail) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_InterfaceLogDetail, null);
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
            Type MyType = _InterfaceLogDetail.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _InterfaceLogDetail.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _InterfaceLogDetail.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_InterfaceLogDetail) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_InterfaceLogDetail, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_InterfaceLogDetail, null)));
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
            string ssql = "DELETE InterfaceLogDetail WHERE InterfaceLogDetailID=@InterfaceLogDetailID";
            Parameter.Add(new DBParameter("@InterfaceLogDetailID", _InterfaceLogDetail.InterfaceLogDetailID));

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
