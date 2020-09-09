/*
** Company Name     :  Trinity Solution Provider Co., Ltd. && ICON Framework Co.,Ltd.
** Contact          :  www.iconrem.com
** Product Name     :  Data Access framework(Class generator) (2012)
** Product by       :  Anupong Kwanpigul
** Modify by        :  Yuttapong Benjapornraksa
** Modify Date      :  2020-02-10 01:38:07
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
	[XmlRootAttribute("Payments_Invoices", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class Payments_Invoices
	{
		[XmlIgnore]
		[NonSerialized]
		public Payments_Invoices_Command ExecCommand = null;
		public Payments_Invoices()
		{
			ExecCommand = new Payments_Invoices_Command(this);
		}
		public Payments_Invoices(string ConnectionStr)
		{
			ExecCommand = new Payments_Invoices_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new Payments_Invoices_Command(ConnectionStr, this);
		}
		public Payments_Invoices(string ConnectionStr, String L_Payments_InvoicesID_Value)
		{
			ExecCommand = new Payments_Invoices_Command(ConnectionStr, this);
			ExecCommand.Load(L_Payments_InvoicesID_Value);
		}
        private String _L_Payments_InvoicesID;
        public readonly bool _L_Payments_InvoicesID_PKFlag = true;
        public readonly bool _L_Payments_InvoicesID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_Payments_InvoicesID = false;
		public String L_Payments_InvoicesID
		{
			get
			{
				return _L_Payments_InvoicesID;
			}
			set
			{
				if (_L_Payments_InvoicesID != value)
                {
					_L_Payments_InvoicesID = value;
					EditL_Payments_InvoicesID = true;
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

        private Nullable<Decimal> _AppliedFC;
        public readonly bool _AppliedFC_PKFlag = false;
        public readonly bool _AppliedFC_IDTFlag = false;
		[XmlIgnore]
		public bool EditAppliedFC = false;
		public Nullable<Decimal> AppliedFC
		{
			get
			{
				return _AppliedFC;
			}
			set
			{
				if (_AppliedFC != value)
                {
					_AppliedFC = value;
					EditAppliedFC = true;
				}
			}
		}

        private Nullable<Decimal> _DiscountPercent;
        public readonly bool _DiscountPercent_PKFlag = false;
        public readonly bool _DiscountPercent_IDTFlag = false;
		[XmlIgnore]
		public bool EditDiscountPercent = false;
		public Nullable<Decimal> DiscountPercent
		{
			get
			{
				return _DiscountPercent;
			}
			set
			{
				if (_DiscountPercent != value)
                {
					_DiscountPercent = value;
					EditDiscountPercent = true;
				}
			}
		}

        private String _DistributionRule;
        public readonly bool _DistributionRule_PKFlag = false;
        public readonly bool _DistributionRule_IDTFlag = false;
		[XmlIgnore]
		public bool EditDistributionRule = false;
		public String DistributionRule
		{
			get
			{
				return _DistributionRule;
			}
			set
			{
				if (_DistributionRule != value)
                {
					_DistributionRule = value;
					EditDistributionRule = true;
				}
			}
		}

        private String _DistributionRule2;
        public readonly bool _DistributionRule2_PKFlag = false;
        public readonly bool _DistributionRule2_IDTFlag = false;
		[XmlIgnore]
		public bool EditDistributionRule2 = false;
		public String DistributionRule2
		{
			get
			{
				return _DistributionRule2;
			}
			set
			{
				if (_DistributionRule2 != value)
                {
					_DistributionRule2 = value;
					EditDistributionRule2 = true;
				}
			}
		}

        private String _DistributionRule3;
        public readonly bool _DistributionRule3_PKFlag = false;
        public readonly bool _DistributionRule3_IDTFlag = false;
		[XmlIgnore]
		public bool EditDistributionRule3 = false;
		public String DistributionRule3
		{
			get
			{
				return _DistributionRule3;
			}
			set
			{
				if (_DistributionRule3 != value)
                {
					_DistributionRule3 = value;
					EditDistributionRule3 = true;
				}
			}
		}

        private String _DistributionRule4;
        public readonly bool _DistributionRule4_PKFlag = false;
        public readonly bool _DistributionRule4_IDTFlag = false;
		[XmlIgnore]
		public bool EditDistributionRule4 = false;
		public String DistributionRule4
		{
			get
			{
				return _DistributionRule4;
			}
			set
			{
				if (_DistributionRule4 != value)
                {
					_DistributionRule4 = value;
					EditDistributionRule4 = true;
				}
			}
		}

        private String _DistributionRule5;
        public readonly bool _DistributionRule5_PKFlag = false;
        public readonly bool _DistributionRule5_IDTFlag = false;
		[XmlIgnore]
		public bool EditDistributionRule5 = false;
		public String DistributionRule5
		{
			get
			{
				return _DistributionRule5;
			}
			set
			{
				if (_DistributionRule5 != value)
                {
					_DistributionRule5 = value;
					EditDistributionRule5 = true;
				}
			}
		}

        private Nullable<Int32> _DocEntry;
        public readonly bool _DocEntry_PKFlag = false;
        public readonly bool _DocEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocEntry = false;
		public Nullable<Int32> DocEntry
		{
			get
			{
				return _DocEntry;
			}
			set
			{
				if (_DocEntry != value)
                {
					_DocEntry = value;
					EditDocEntry = true;
				}
			}
		}

        private Nullable<Int32> _DocLine;
        public readonly bool _DocLine_PKFlag = false;
        public readonly bool _DocLine_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocLine = false;
		public Nullable<Int32> DocLine
		{
			get
			{
				return _DocLine;
			}
			set
			{
				if (_DocLine != value)
                {
					_DocLine = value;
					EditDocLine = true;
				}
			}
		}

        private Nullable<Int32> _InstallmentId;
        public readonly bool _InstallmentId_PKFlag = false;
        public readonly bool _InstallmentId_IDTFlag = false;
		[XmlIgnore]
		public bool EditInstallmentId = false;
		public Nullable<Int32> InstallmentId
		{
			get
			{
				return _InstallmentId;
			}
			set
			{
				if (_InstallmentId != value)
                {
					_InstallmentId = value;
					EditInstallmentId = true;
				}
			}
		}

        private String _InvoiceType;
        public readonly bool _InvoiceType_PKFlag = false;
        public readonly bool _InvoiceType_IDTFlag = false;
		[XmlIgnore]
		public bool EditInvoiceType = false;
		public String InvoiceType
		{
			get
			{
				return _InvoiceType;
			}
			set
			{
				if (_InvoiceType != value)
                {
					_InvoiceType = value;
					EditInvoiceType = true;
				}
			}
		}

        private Nullable<Decimal> _SumApplied;
        public readonly bool _SumApplied_PKFlag = false;
        public readonly bool _SumApplied_IDTFlag = false;
		[XmlIgnore]
		public bool EditSumApplied = false;
		public Nullable<Decimal> SumApplied
		{
			get
			{
				return _SumApplied;
			}
			set
			{
				if (_SumApplied != value)
                {
					_SumApplied = value;
					EditSumApplied = true;
				}
			}
		}

        private Nullable<Decimal> _TotalDiscount;
        public readonly bool _TotalDiscount_PKFlag = false;
        public readonly bool _TotalDiscount_IDTFlag = false;
		[XmlIgnore]
		public bool EditTotalDiscount = false;
		public Nullable<Decimal> TotalDiscount
		{
			get
			{
				return _TotalDiscount;
			}
			set
			{
				if (_TotalDiscount != value)
                {
					_TotalDiscount = value;
					EditTotalDiscount = true;
				}
			}
		}

        private Nullable<Decimal> _TotalDiscountFC;
        public readonly bool _TotalDiscountFC_PKFlag = false;
        public readonly bool _TotalDiscountFC_IDTFlag = false;
		[XmlIgnore]
		public bool EditTotalDiscountFC = false;
		public Nullable<Decimal> TotalDiscountFC
		{
			get
			{
				return _TotalDiscountFC;
			}
			set
			{
				if (_TotalDiscountFC != value)
                {
					_TotalDiscountFC = value;
					EditTotalDiscountFC = true;
				}
			}
		}

	}
	public partial class Payments_Invoices_Command
	{
		string TableName = "Payments_Invoices";

		Payments_Invoices _Payments_Invoices = null;
		DBHelper _DBHelper = null;

		internal Payments_Invoices_Command(Payments_Invoices obj_Payments_Invoices)
		{
			this._Payments_Invoices = obj_Payments_Invoices;
			this._DBHelper = new DBHelper();
		}

		internal Payments_Invoices_Command(string ConnectionStr, Payments_Invoices obj_Payments_Invoices)
		{
			this._Payments_Invoices = obj_Payments_Invoices;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _Payments_Invoices.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Payments_Invoices.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_Payments_Invoices, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_Payments_Invoices, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_Payments_InvoicesID_Value)
        {
            Load(_DBHelper, L_Payments_InvoicesID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_Payments_InvoicesID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Payments_InvoicesID", L_Payments_InvoicesID_Value, (DbType)Enum.Parse(typeof(DbType), L_Payments_InvoicesID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Payments_Invoices WHERE L_Payments_InvoicesID=@L_Payments_InvoicesID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_Payments_InvoicesID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Payments_InvoicesID", L_Payments_InvoicesID_Value, (DbType)Enum.Parse(typeof(DbType), L_Payments_InvoicesID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Payments_Invoices WHERE L_Payments_InvoicesID=@L_Payments_InvoicesID", "Payments_Invoices", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("Payments_Invoices");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<Payments_Invoices> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<Payments_Invoices> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("Payments_Invoices");
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
            List<Payments_Invoices> Res = new List<Payments_Invoices>();
            foreach (DataRow Dr in dt.Rows)
            {
                Payments_Invoices Item = new Payments_Invoices();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Invoices.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_Invoices.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_Payments_Invoices);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Invoices.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_Invoices.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_Payments_Invoices);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _Payments_Invoices.GetType();
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

                foreach (PropertyInfo ProInfo in _Payments_Invoices.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _Payments_Invoices.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_Invoices))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_Payments_Invoices, null);
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
            Type MyType = _Payments_Invoices.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Invoices.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_Invoices.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_Invoices) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Payments_Invoices, null);
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
            Type MyType = _Payments_Invoices.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_Invoices.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Payments_Invoices.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_Invoices) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Payments_Invoices, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_Payments_Invoices, null)));
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
            string ssql = "DELETE Payments_Invoices WHERE L_Payments_InvoicesID=@L_Payments_InvoicesID";
            Parameter.Add(new DBParameter("@L_Payments_InvoicesID", _Payments_Invoices.L_Payments_InvoicesID));

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
