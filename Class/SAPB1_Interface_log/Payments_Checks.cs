/*
** Company Name     :  Trinity Solution Provider Co., Ltd. && ICON Framework Co.,Ltd.
** Contact          :  www.iconrem.com
** Product Name     :  Data Access framework(Class generator) (2012)
** Product by       :  Anupong Kwanpigul
** Modify by        :  Yuttapong Benjapornraksa
** Modify Date      :  2020-02-03 15:54:27
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
	[XmlRootAttribute("Payments_Checks", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class Payments_Checks
	{
		[XmlIgnore]
		[NonSerialized]
		public Payments_Checks_Command ExecCommand = null;
		public Payments_Checks()
		{
			ExecCommand = new Payments_Checks_Command(this);
		}
		public Payments_Checks(string ConnectionStr)
		{
			ExecCommand = new Payments_Checks_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new Payments_Checks_Command(ConnectionStr, this);
		}
		public Payments_Checks(string ConnectionStr, String L_Payments_ChecksID_Value)
		{
			ExecCommand = new Payments_Checks_Command(ConnectionStr, this);
			ExecCommand.Load(L_Payments_ChecksID_Value);
		}
        private String _L_Payments_ChecksID;
        public readonly bool _L_Payments_ChecksID_PKFlag = true;
        public readonly bool _L_Payments_ChecksID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_Payments_ChecksID = false;
		public String L_Payments_ChecksID
		{
			get
			{
				return _L_Payments_ChecksID;
			}
			set
			{
				if (_L_Payments_ChecksID != value)
                {
					_L_Payments_ChecksID = value;
					EditL_Payments_ChecksID = true;
				}
			}
		}

        private String _L_InterfaceLogID;
        public readonly bool _L_InterfaceLogID_PKFlag = false;
        public readonly bool _L_InterfaceLogID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_InterfaceLogID = false;
		public String L_InterfaceLogID
		{
			get
			{
				return _L_InterfaceLogID;
			}
			set
			{
				if (_L_InterfaceLogID != value)
                {
					_L_InterfaceLogID = value;
					EditL_InterfaceLogID = true;
				}
			}
		}

        private String _L_InterfaceLogDetailID;
        public readonly bool _L_InterfaceLogDetailID_PKFlag = false;
        public readonly bool _L_InterfaceLogDetailID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_InterfaceLogDetailID = false;
		public String L_InterfaceLogDetailID
		{
			get
			{
				return _L_InterfaceLogDetailID;
			}
			set
			{
				if (_L_InterfaceLogDetailID != value)
                {
					_L_InterfaceLogDetailID = value;
					EditL_InterfaceLogDetailID = true;
				}
			}
		}

        private String _L_PaymentsID;
        public readonly bool _L_PaymentsID_PKFlag = false;
        public readonly bool _L_PaymentsID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_PaymentsID = false;
		public String L_PaymentsID
		{
			get
			{
				return _L_PaymentsID;
			}
			set
			{
				if (_L_PaymentsID != value)
                {
					_L_PaymentsID = value;
					EditL_PaymentsID = true;
				}
			}
		}

        private Nullable<Int32> _L_LineNum;
        public readonly bool _L_LineNum_PKFlag = false;
        public readonly bool _L_LineNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_LineNum = false;
		public Nullable<Int32> L_LineNum
		{
			get
			{
				return _L_LineNum;
			}
			set
			{
				if (_L_LineNum != value)
                {
					_L_LineNum = value;
					EditL_LineNum = true;
				}
			}
		}

        private String _AccounttNum;
        public readonly bool _AccounttNum_PKFlag = false;
        public readonly bool _AccounttNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditAccounttNum = false;
		public String AccounttNum
		{
			get
			{
				return _AccounttNum;
			}
			set
			{
				if (_AccounttNum != value)
                {
					_AccounttNum = value;
					EditAccounttNum = true;
				}
			}
		}

        private String _BankCode;
        public readonly bool _BankCode_PKFlag = false;
        public readonly bool _BankCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditBankCode = false;
		public String BankCode
		{
			get
			{
				return _BankCode;
			}
			set
			{
				if (_BankCode != value)
                {
					_BankCode = value;
					EditBankCode = true;
				}
			}
		}

        private String _Branch;
        public readonly bool _Branch_PKFlag = false;
        public readonly bool _Branch_IDTFlag = false;
		[XmlIgnore]
		public bool EditBranch = false;
		public String Branch
		{
			get
			{
				return _Branch;
			}
			set
			{
				if (_Branch != value)
                {
					_Branch = value;
					EditBranch = true;
				}
			}
		}

        private String _CheckAccount;
        public readonly bool _CheckAccount_PKFlag = false;
        public readonly bool _CheckAccount_IDTFlag = false;
		[XmlIgnore]
		public bool EditCheckAccount = false;
		public String CheckAccount
		{
			get
			{
				return _CheckAccount;
			}
			set
			{
				if (_CheckAccount != value)
                {
					_CheckAccount = value;
					EditCheckAccount = true;
				}
			}
		}

        private Nullable<Int32> _CheckNumber;
        public readonly bool _CheckNumber_PKFlag = false;
        public readonly bool _CheckNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditCheckNumber = false;
		public Nullable<Int32> CheckNumber
		{
			get
			{
				return _CheckNumber;
			}
			set
			{
				if (_CheckNumber != value)
                {
					_CheckNumber = value;
					EditCheckNumber = true;
				}
			}
		}

        private Nullable<Decimal> _CheckSum;
        public readonly bool _CheckSum_PKFlag = false;
        public readonly bool _CheckSum_IDTFlag = false;
		[XmlIgnore]
		public bool EditCheckSum = false;
		public Nullable<Decimal> CheckSum
		{
			get
			{
				return _CheckSum;
			}
			set
			{
				if (_CheckSum != value)
                {
					_CheckSum = value;
					EditCheckSum = true;
				}
			}
		}

        private String _CountryCode;
        public readonly bool _CountryCode_PKFlag = false;
        public readonly bool _CountryCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditCountryCode = false;
		public String CountryCode
		{
			get
			{
				return _CountryCode;
			}
			set
			{
				if (_CountryCode != value)
                {
					_CountryCode = value;
					EditCountryCode = true;
				}
			}
		}

        private String _Details;
        public readonly bool _Details_PKFlag = false;
        public readonly bool _Details_IDTFlag = false;
		[XmlIgnore]
		public bool EditDetails = false;
		public String Details
		{
			get
			{
				return _Details;
			}
			set
			{
				if (_Details != value)
                {
					_Details = value;
					EditDetails = true;
				}
			}
		}

        private Nullable<DateTime> _DueDate;
        public readonly bool _DueDate_PKFlag = false;
        public readonly bool _DueDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditDueDate = false;
		public Nullable<DateTime> DueDate
		{
			get
			{
				return _DueDate;
			}
			set
			{
				if (_DueDate != value)
                {
					_DueDate = value;
					EditDueDate = true;
				}
			}
		}

        private Nullable<Int32> _EndorsableCheckNo;
        public readonly bool _EndorsableCheckNo_PKFlag = false;
        public readonly bool _EndorsableCheckNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditEndorsableCheckNo = false;
		public Nullable<Int32> EndorsableCheckNo
		{
			get
			{
				return _EndorsableCheckNo;
			}
			set
			{
				if (_EndorsableCheckNo != value)
                {
					_EndorsableCheckNo = value;
					EditEndorsableCheckNo = true;
				}
			}
		}

        private String _Endorse;
        public readonly bool _Endorse_PKFlag = false;
        public readonly bool _Endorse_IDTFlag = false;
		[XmlIgnore]
		public bool EditEndorse = false;
		public String Endorse
		{
			get
			{
				return _Endorse;
			}
			set
			{
				if (_Endorse != value)
                {
					_Endorse = value;
					EditEndorse = true;
				}
			}
		}

        private String _FiscalID;
        public readonly bool _FiscalID_PKFlag = false;
        public readonly bool _FiscalID_IDTFlag = false;
		[XmlIgnore]
		public bool EditFiscalID = false;
		public String FiscalID
		{
			get
			{
				return _FiscalID;
			}
			set
			{
				if (_FiscalID != value)
                {
					_FiscalID = value;
					EditFiscalID = true;
				}
			}
		}

        private String _ManualCheck;
        public readonly bool _ManualCheck_PKFlag = false;
        public readonly bool _ManualCheck_IDTFlag = false;
		[XmlIgnore]
		public bool EditManualCheck = false;
		public String ManualCheck
		{
			get
			{
				return _ManualCheck;
			}
			set
			{
				if (_ManualCheck != value)
                {
					_ManualCheck = value;
					EditManualCheck = true;
				}
			}
		}

        private String _OriginallyIssuedBy;
        public readonly bool _OriginallyIssuedBy_PKFlag = false;
        public readonly bool _OriginallyIssuedBy_IDTFlag = false;
		[XmlIgnore]
		public bool EditOriginallyIssuedBy = false;
		public String OriginallyIssuedBy
		{
			get
			{
				return _OriginallyIssuedBy;
			}
			set
			{
				if (_OriginallyIssuedBy != value)
                {
					_OriginallyIssuedBy = value;
					EditOriginallyIssuedBy = true;
				}
			}
		}

        private String _Trnsfrable;
        public readonly bool _Trnsfrable_PKFlag = false;
        public readonly bool _Trnsfrable_IDTFlag = false;
		[XmlIgnore]
		public bool EditTrnsfrable = false;
		public String Trnsfrable
		{
			get
			{
				return _Trnsfrable;
			}
			set
			{
				if (_Trnsfrable != value)
                {
					_Trnsfrable = value;
					EditTrnsfrable = true;
				}
			}
		}

	}
	public partial class Payments_Checks_Command
	{
		string TableName = "Payments_Checks";

		Payments_Checks _Payments_Checks = null;
		DBHelper _DBHelper = null;

		internal Payments_Checks_Command(Payments_Checks obj_Payments_Checks)
		{
			this._Payments_Checks = obj_Payments_Checks;
			this._DBHelper = new DBHelper();
		}

		internal Payments_Checks_Command(string ConnectionStr, Payments_Checks obj_Payments_Checks)
		{
			this._Payments_Checks = obj_Payments_Checks;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _Payments_Checks.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Payments_Checks.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_Payments_Checks, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_Payments_Checks, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_Payments_ChecksID_Value)
        {
            Load(_DBHelper, L_Payments_ChecksID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_Payments_ChecksID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Payments_ChecksID", L_Payments_ChecksID_Value, (DbType)Enum.Parse(typeof(DbType), L_Payments_ChecksID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Payments_Checks WHERE L_Payments_ChecksID=@L_Payments_ChecksID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_Payments_ChecksID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Payments_ChecksID", L_Payments_ChecksID_Value, (DbType)Enum.Parse(typeof(DbType), L_Payments_ChecksID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Payments_Checks WHERE L_Payments_ChecksID=@L_Payments_ChecksID", "Payments_Checks", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("Payments_Checks");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<Payments_Checks> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<Payments_Checks> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("Payments_Checks");
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
            List<Payments_Checks> Res = new List<Payments_Checks>();
            foreach (DataRow Dr in dt.Rows)
            {
                Payments_Checks Item = new Payments_Checks();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Checks.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_Checks.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_Payments_Checks);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Checks.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_Checks.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_Payments_Checks);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _Payments_Checks.GetType();
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

                foreach (PropertyInfo ProInfo in _Payments_Checks.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _Payments_Checks.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_Checks))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_Payments_Checks, null);
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
            Type MyType = _Payments_Checks.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Checks.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_Checks.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_Checks) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Payments_Checks, null);
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
            Type MyType = _Payments_Checks.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Checks.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Payments_Checks.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_Checks) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Payments_Checks, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_Payments_Checks, null)));
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
            string ssql = "DELETE Payments_Checks WHERE L_Payments_ChecksID=@L_Payments_ChecksID";
            Parameter.Add(new DBParameter("@L_Payments_ChecksID", _Payments_Checks.L_Payments_ChecksID));

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
