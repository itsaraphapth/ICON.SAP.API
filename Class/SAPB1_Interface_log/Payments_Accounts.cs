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
	[XmlRootAttribute("Payments_Accounts", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class Payments_Accounts
	{
		[XmlIgnore]
		[NonSerialized]
		public Payments_Accounts_Command ExecCommand = null;
		public Payments_Accounts()
		{
			ExecCommand = new Payments_Accounts_Command(this);
		}
		public Payments_Accounts(string ConnectionStr)
		{
			ExecCommand = new Payments_Accounts_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new Payments_Accounts_Command(ConnectionStr, this);
		}
		public Payments_Accounts(string ConnectionStr, String L_Payments_AccountsID_Value)
		{
			ExecCommand = new Payments_Accounts_Command(ConnectionStr, this);
			ExecCommand.Load(L_Payments_AccountsID_Value);
		}
        private String _L_Payments_AccountsID;
        public readonly bool _L_Payments_AccountsID_PKFlag = true;
        public readonly bool _L_Payments_AccountsID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_Payments_AccountsID = false;
		public String L_Payments_AccountsID
		{
			get
			{
				return _L_Payments_AccountsID;
			}
			set
			{
				if (_L_Payments_AccountsID != value)
                {
					_L_Payments_AccountsID = value;
					EditL_Payments_AccountsID = true;
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

        private String _AccountCode;
        public readonly bool _AccountCode_PKFlag = false;
        public readonly bool _AccountCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditAccountCode = false;
		public String AccountCode
		{
			get
			{
				return _AccountCode;
			}
			set
			{
				if (_AccountCode != value)
                {
					_AccountCode = value;
					EditAccountCode = true;
				}
			}
		}

        private String _AccountName;
        public readonly bool _AccountName_PKFlag = false;
        public readonly bool _AccountName_IDTFlag = false;
		[XmlIgnore]
		public bool EditAccountName = false;
		public String AccountName
		{
			get
			{
				return _AccountName;
			}
			set
			{
				if (_AccountName != value)
                {
					_AccountName = value;
					EditAccountName = true;
				}
			}
		}

        private String _Decription;
        public readonly bool _Decription_PKFlag = false;
        public readonly bool _Decription_IDTFlag = false;
		[XmlIgnore]
		public bool EditDecription = false;
		public String Decription
		{
			get
			{
				return _Decription;
			}
			set
			{
				if (_Decription != value)
                {
					_Decription = value;
					EditDecription = true;
				}
			}
		}

        private Nullable<Decimal> _GrossAmount;
        public readonly bool _GrossAmount_PKFlag = false;
        public readonly bool _GrossAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditGrossAmount = false;
		public Nullable<Decimal> GrossAmount
		{
			get
			{
				return _GrossAmount;
			}
			set
			{
				if (_GrossAmount != value)
                {
					_GrossAmount = value;
					EditGrossAmount = true;
				}
			}
		}

        private String _ProfitCenter;
        public readonly bool _ProfitCenter_PKFlag = false;
        public readonly bool _ProfitCenter_IDTFlag = false;
		[XmlIgnore]
		public bool EditProfitCenter = false;
		public String ProfitCenter
		{
			get
			{
				return _ProfitCenter;
			}
			set
			{
				if (_ProfitCenter != value)
                {
					_ProfitCenter = value;
					EditProfitCenter = true;
				}
			}
		}

        private String _ProfitCenter2;
        public readonly bool _ProfitCenter2_PKFlag = false;
        public readonly bool _ProfitCenter2_IDTFlag = false;
		[XmlIgnore]
		public bool EditProfitCenter2 = false;
		public String ProfitCenter2
		{
			get
			{
				return _ProfitCenter2;
			}
			set
			{
				if (_ProfitCenter2 != value)
                {
					_ProfitCenter2 = value;
					EditProfitCenter2 = true;
				}
			}
		}

        private String _ProfitCenter3;
        public readonly bool _ProfitCenter3_PKFlag = false;
        public readonly bool _ProfitCenter3_IDTFlag = false;
		[XmlIgnore]
		public bool EditProfitCenter3 = false;
		public String ProfitCenter3
		{
			get
			{
				return _ProfitCenter3;
			}
			set
			{
				if (_ProfitCenter3 != value)
                {
					_ProfitCenter3 = value;
					EditProfitCenter3 = true;
				}
			}
		}

        private String _ProfitCenter4;
        public readonly bool _ProfitCenter4_PKFlag = false;
        public readonly bool _ProfitCenter4_IDTFlag = false;
		[XmlIgnore]
		public bool EditProfitCenter4 = false;
		public String ProfitCenter4
		{
			get
			{
				return _ProfitCenter4;
			}
			set
			{
				if (_ProfitCenter4 != value)
                {
					_ProfitCenter4 = value;
					EditProfitCenter4 = true;
				}
			}
		}

        private String _ProfitCenter5;
        public readonly bool _ProfitCenter5_PKFlag = false;
        public readonly bool _ProfitCenter5_IDTFlag = false;
		[XmlIgnore]
		public bool EditProfitCenter5 = false;
		public String ProfitCenter5
		{
			get
			{
				return _ProfitCenter5;
			}
			set
			{
				if (_ProfitCenter5 != value)
                {
					_ProfitCenter5 = value;
					EditProfitCenter5 = true;
				}
			}
		}

        private String _ProjectCode;
        public readonly bool _ProjectCode_PKFlag = false;
        public readonly bool _ProjectCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditProjectCode = false;
		public String ProjectCode
		{
			get
			{
				return _ProjectCode;
			}
			set
			{
				if (_ProjectCode != value)
                {
					_ProjectCode = value;
					EditProjectCode = true;
				}
			}
		}

        private Nullable<Decimal> _SumPaid;
        public readonly bool _SumPaid_PKFlag = false;
        public readonly bool _SumPaid_IDTFlag = false;
		[XmlIgnore]
		public bool EditSumPaid = false;
		public Nullable<Decimal> SumPaid
		{
			get
			{
				return _SumPaid;
			}
			set
			{
				if (_SumPaid != value)
                {
					_SumPaid = value;
					EditSumPaid = true;
				}
			}
		}

        private Nullable<Decimal> _VatAmount;
        public readonly bool _VatAmount_PKFlag = false;
        public readonly bool _VatAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditVatAmount = false;
		public Nullable<Decimal> VatAmount
		{
			get
			{
				return _VatAmount;
			}
			set
			{
				if (_VatAmount != value)
                {
					_VatAmount = value;
					EditVatAmount = true;
				}
			}
		}

        private String _VatGroup;
        public readonly bool _VatGroup_PKFlag = false;
        public readonly bool _VatGroup_IDTFlag = false;
		[XmlIgnore]
		public bool EditVatGroup = false;
		public String VatGroup
		{
			get
			{
				return _VatGroup;
			}
			set
			{
				if (_VatGroup != value)
                {
					_VatGroup = value;
					EditVatGroup = true;
				}
			}
		}

	}
	public partial class Payments_Accounts_Command
	{
		string TableName = "Payments_Accounts";

		Payments_Accounts _Payments_Accounts = null;
		DBHelper _DBHelper = null;

		internal Payments_Accounts_Command(Payments_Accounts obj_Payments_Accounts)
		{
			this._Payments_Accounts = obj_Payments_Accounts;
			this._DBHelper = new DBHelper();
		}

		internal Payments_Accounts_Command(string ConnectionStr, Payments_Accounts obj_Payments_Accounts)
		{
			this._Payments_Accounts = obj_Payments_Accounts;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _Payments_Accounts.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Payments_Accounts.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_Payments_Accounts, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_Payments_Accounts, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_Payments_AccountsID_Value)
        {
            Load(_DBHelper, L_Payments_AccountsID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_Payments_AccountsID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Payments_AccountsID", L_Payments_AccountsID_Value, (DbType)Enum.Parse(typeof(DbType), L_Payments_AccountsID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Payments_Accounts WHERE L_Payments_AccountsID=@L_Payments_AccountsID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_Payments_AccountsID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Payments_AccountsID", L_Payments_AccountsID_Value, (DbType)Enum.Parse(typeof(DbType), L_Payments_AccountsID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Payments_Accounts WHERE L_Payments_AccountsID=@L_Payments_AccountsID", "Payments_Accounts", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("Payments_Accounts");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<Payments_Accounts> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<Payments_Accounts> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("Payments_Accounts");
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
            List<Payments_Accounts> Res = new List<Payments_Accounts>();
            foreach (DataRow Dr in dt.Rows)
            {
                Payments_Accounts Item = new Payments_Accounts();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Accounts.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_Accounts.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_Payments_Accounts);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Accounts.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_Accounts.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_Payments_Accounts);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _Payments_Accounts.GetType();
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

                foreach (PropertyInfo ProInfo in _Payments_Accounts.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _Payments_Accounts.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_Accounts))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_Payments_Accounts, null);
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
            Type MyType = _Payments_Accounts.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Accounts.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_Accounts.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_Accounts) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Payments_Accounts, null);
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
            Type MyType = _Payments_Accounts.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Accounts.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Payments_Accounts.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_Accounts) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Payments_Accounts, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_Payments_Accounts, null)));
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
            string ssql = "DELETE Payments_Accounts WHERE L_Payments_AccountsID=@L_Payments_AccountsID";
            Parameter.Add(new DBParameter("@L_Payments_AccountsID", _Payments_Accounts.L_Payments_AccountsID));

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
