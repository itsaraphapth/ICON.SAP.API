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
	[XmlRootAttribute("Payments_CreditCards", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class Payments_CreditCards
	{
		[XmlIgnore]
		[NonSerialized]
		public Payments_CreditCards_Command ExecCommand = null;
		public Payments_CreditCards()
		{
			ExecCommand = new Payments_CreditCards_Command(this);
		}
		public Payments_CreditCards(string ConnectionStr)
		{
			ExecCommand = new Payments_CreditCards_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new Payments_CreditCards_Command(ConnectionStr, this);
		}
		public Payments_CreditCards(string ConnectionStr, String L_Payments_CreditCardsID_Value)
		{
			ExecCommand = new Payments_CreditCards_Command(ConnectionStr, this);
			ExecCommand.Load(L_Payments_CreditCardsID_Value);
		}
        private String _L_Payments_CreditCardsID;
        public readonly bool _L_Payments_CreditCardsID_PKFlag = true;
        public readonly bool _L_Payments_CreditCardsID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_Payments_CreditCardsID = false;
		public String L_Payments_CreditCardsID
		{
			get
			{
				return _L_Payments_CreditCardsID;
			}
			set
			{
				if (_L_Payments_CreditCardsID != value)
                {
					_L_Payments_CreditCardsID = value;
					EditL_Payments_CreditCardsID = true;
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

        private Nullable<Decimal> _AdditionalPaymentSum;
        public readonly bool _AdditionalPaymentSum_PKFlag = false;
        public readonly bool _AdditionalPaymentSum_IDTFlag = false;
		[XmlIgnore]
		public bool EditAdditionalPaymentSum = false;
		public Nullable<Decimal> AdditionalPaymentSum
		{
			get
			{
				return _AdditionalPaymentSum;
			}
			set
			{
				if (_AdditionalPaymentSum != value)
                {
					_AdditionalPaymentSum = value;
					EditAdditionalPaymentSum = true;
				}
			}
		}

        private Nullable<DateTime> _CardValidUntil;
        public readonly bool _CardValidUntil_PKFlag = false;
        public readonly bool _CardValidUntil_IDTFlag = false;
		[XmlIgnore]
		public bool EditCardValidUntil = false;
		public Nullable<DateTime> CardValidUntil
		{
			get
			{
				return _CardValidUntil;
			}
			set
			{
				if (_CardValidUntil != value)
                {
					_CardValidUntil = value;
					EditCardValidUntil = true;
				}
			}
		}

        private String _ConfirmationNum;
        public readonly bool _ConfirmationNum_PKFlag = false;
        public readonly bool _ConfirmationNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditConfirmationNum = false;
		public String ConfirmationNum
		{
			get
			{
				return _ConfirmationNum;
			}
			set
			{
				if (_ConfirmationNum != value)
                {
					_ConfirmationNum = value;
					EditConfirmationNum = true;
				}
			}
		}

        private String _CreditAcct;
        public readonly bool _CreditAcct_PKFlag = false;
        public readonly bool _CreditAcct_IDTFlag = false;
		[XmlIgnore]
		public bool EditCreditAcct = false;
		public String CreditAcct
		{
			get
			{
				return _CreditAcct;
			}
			set
			{
				if (_CreditAcct != value)
                {
					_CreditAcct = value;
					EditCreditAcct = true;
				}
			}
		}

        private Nullable<Int32> _CreditCard;
        public readonly bool _CreditCard_PKFlag = false;
        public readonly bool _CreditCard_IDTFlag = false;
		[XmlIgnore]
		public bool EditCreditCard = false;
		public Nullable<Int32> CreditCard
		{
			get
			{
				return _CreditCard;
			}
			set
			{
				if (_CreditCard != value)
                {
					_CreditCard = value;
					EditCreditCard = true;
				}
			}
		}

        private String _CreditCardNumber;
        public readonly bool _CreditCardNumber_PKFlag = false;
        public readonly bool _CreditCardNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditCreditCardNumber = false;
		public String CreditCardNumber
		{
			get
			{
				return _CreditCardNumber;
			}
			set
			{
				if (_CreditCardNumber != value)
                {
					_CreditCardNumber = value;
					EditCreditCardNumber = true;
				}
			}
		}

        private Nullable<Decimal> _CreditSum;
        public readonly bool _CreditSum_PKFlag = false;
        public readonly bool _CreditSum_IDTFlag = false;
		[XmlIgnore]
		public bool EditCreditSum = false;
		public Nullable<Decimal> CreditSum
		{
			get
			{
				return _CreditSum;
			}
			set
			{
				if (_CreditSum != value)
                {
					_CreditSum = value;
					EditCreditSum = true;
				}
			}
		}

        private String _CreditType;
        public readonly bool _CreditType_PKFlag = false;
        public readonly bool _CreditType_IDTFlag = false;
		[XmlIgnore]
		public bool EditCreditType = false;
		public String CreditType
		{
			get
			{
				return _CreditType;
			}
			set
			{
				if (_CreditType != value)
                {
					_CreditType = value;
					EditCreditType = true;
				}
			}
		}

        private Nullable<DateTime> _FirstPaymentDue;
        public readonly bool _FirstPaymentDue_PKFlag = false;
        public readonly bool _FirstPaymentDue_IDTFlag = false;
		[XmlIgnore]
		public bool EditFirstPaymentDue = false;
		public Nullable<DateTime> FirstPaymentDue
		{
			get
			{
				return _FirstPaymentDue;
			}
			set
			{
				if (_FirstPaymentDue != value)
                {
					_FirstPaymentDue = value;
					EditFirstPaymentDue = true;
				}
			}
		}

        private Nullable<Decimal> _FirstPaymentSum;
        public readonly bool _FirstPaymentSum_PKFlag = false;
        public readonly bool _FirstPaymentSum_IDTFlag = false;
		[XmlIgnore]
		public bool EditFirstPaymentSum = false;
		public Nullable<Decimal> FirstPaymentSum
		{
			get
			{
				return _FirstPaymentSum;
			}
			set
			{
				if (_FirstPaymentSum != value)
                {
					_FirstPaymentSum = value;
					EditFirstPaymentSum = true;
				}
			}
		}

        private Nullable<Int32> _NumOfCreditPayments;
        public readonly bool _NumOfCreditPayments_PKFlag = false;
        public readonly bool _NumOfCreditPayments_IDTFlag = false;
		[XmlIgnore]
		public bool EditNumOfCreditPayments = false;
		public Nullable<Int32> NumOfCreditPayments
		{
			get
			{
				return _NumOfCreditPayments;
			}
			set
			{
				if (_NumOfCreditPayments != value)
                {
					_NumOfCreditPayments = value;
					EditNumOfCreditPayments = true;
				}
			}
		}

        private Nullable<Int32> _NumOfPayments;
        public readonly bool _NumOfPayments_PKFlag = false;
        public readonly bool _NumOfPayments_IDTFlag = false;
		[XmlIgnore]
		public bool EditNumOfPayments = false;
		public Nullable<Int32> NumOfPayments
		{
			get
			{
				return _NumOfPayments;
			}
			set
			{
				if (_NumOfPayments != value)
                {
					_NumOfPayments = value;
					EditNumOfPayments = true;
				}
			}
		}

        private String _OwnerIdNum;
        public readonly bool _OwnerIdNum_PKFlag = false;
        public readonly bool _OwnerIdNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditOwnerIdNum = false;
		public String OwnerIdNum
		{
			get
			{
				return _OwnerIdNum;
			}
			set
			{
				if (_OwnerIdNum != value)
                {
					_OwnerIdNum = value;
					EditOwnerIdNum = true;
				}
			}
		}

        private String _OwnerPhone;
        public readonly bool _OwnerPhone_PKFlag = false;
        public readonly bool _OwnerPhone_IDTFlag = false;
		[XmlIgnore]
		public bool EditOwnerPhone = false;
		public String OwnerPhone
		{
			get
			{
				return _OwnerPhone;
			}
			set
			{
				if (_OwnerPhone != value)
                {
					_OwnerPhone = value;
					EditOwnerPhone = true;
				}
			}
		}

        private Nullable<Int32> _PaymentMethodCode;
        public readonly bool _PaymentMethodCode_PKFlag = false;
        public readonly bool _PaymentMethodCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditPaymentMethodCode = false;
		public Nullable<Int32> PaymentMethodCode
		{
			get
			{
				return _PaymentMethodCode;
			}
			set
			{
				if (_PaymentMethodCode != value)
                {
					_PaymentMethodCode = value;
					EditPaymentMethodCode = true;
				}
			}
		}

        private String _SplitPayments;
        public readonly bool _SplitPayments_PKFlag = false;
        public readonly bool _SplitPayments_IDTFlag = false;
		[XmlIgnore]
		public bool EditSplitPayments = false;
		public String SplitPayments
		{
			get
			{
				return _SplitPayments;
			}
			set
			{
				if (_SplitPayments != value)
                {
					_SplitPayments = value;
					EditSplitPayments = true;
				}
			}
		}

        private String _VoucherNum;
        public readonly bool _VoucherNum_PKFlag = false;
        public readonly bool _VoucherNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditVoucherNum = false;
		public String VoucherNum
		{
			get
			{
				return _VoucherNum;
			}
			set
			{
				if (_VoucherNum != value)
                {
					_VoucherNum = value;
					EditVoucherNum = true;
				}
			}
		}

	}
	public partial class Payments_CreditCards_Command
	{
		string TableName = "Payments_CreditCards";

		Payments_CreditCards _Payments_CreditCards = null;
		DBHelper _DBHelper = null;

		internal Payments_CreditCards_Command(Payments_CreditCards obj_Payments_CreditCards)
		{
			this._Payments_CreditCards = obj_Payments_CreditCards;
			this._DBHelper = new DBHelper();
		}

		internal Payments_CreditCards_Command(string ConnectionStr, Payments_CreditCards obj_Payments_CreditCards)
		{
			this._Payments_CreditCards = obj_Payments_CreditCards;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _Payments_CreditCards.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Payments_CreditCards.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_Payments_CreditCards, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_Payments_CreditCards, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_Payments_CreditCardsID_Value)
        {
            Load(_DBHelper, L_Payments_CreditCardsID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_Payments_CreditCardsID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Payments_CreditCardsID", L_Payments_CreditCardsID_Value, (DbType)Enum.Parse(typeof(DbType), L_Payments_CreditCardsID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Payments_CreditCards WHERE L_Payments_CreditCardsID=@L_Payments_CreditCardsID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_Payments_CreditCardsID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Payments_CreditCardsID", L_Payments_CreditCardsID_Value, (DbType)Enum.Parse(typeof(DbType), L_Payments_CreditCardsID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Payments_CreditCards WHERE L_Payments_CreditCardsID=@L_Payments_CreditCardsID", "Payments_CreditCards", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("Payments_CreditCards");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<Payments_CreditCards> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<Payments_CreditCards> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("Payments_CreditCards");
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
            List<Payments_CreditCards> Res = new List<Payments_CreditCards>();
            foreach (DataRow Dr in dt.Rows)
            {
                Payments_CreditCards Item = new Payments_CreditCards();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_CreditCards.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_CreditCards.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_Payments_CreditCards);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_CreditCards.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_CreditCards.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_Payments_CreditCards);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _Payments_CreditCards.GetType();
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

                foreach (PropertyInfo ProInfo in _Payments_CreditCards.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _Payments_CreditCards.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_CreditCards))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_Payments_CreditCards, null);
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
            Type MyType = _Payments_CreditCards.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_CreditCards.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments_CreditCards.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_CreditCards) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Payments_CreditCards, null);
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
            Type MyType = _Payments_CreditCards.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments_CreditCards.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Payments_CreditCards.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments_CreditCards) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Payments_CreditCards, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_Payments_CreditCards, null)));
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
            string ssql = "DELETE Payments_CreditCards WHERE L_Payments_CreditCardsID=@L_Payments_CreditCardsID";
            Parameter.Add(new DBParameter("@L_Payments_CreditCardsID", _Payments_CreditCards.L_Payments_CreditCardsID));

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
