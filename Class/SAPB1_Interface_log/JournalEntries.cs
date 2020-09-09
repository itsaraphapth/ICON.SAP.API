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
	[XmlRootAttribute("JournalEntries", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class JournalEntries
	{
		[XmlIgnore]
		[NonSerialized]
		public JournalEntries_Command ExecCommand = null;
		public JournalEntries()
		{
			ExecCommand = new JournalEntries_Command(this);
		}
		public JournalEntries(string ConnectionStr)
		{
			ExecCommand = new JournalEntries_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new JournalEntries_Command(ConnectionStr, this);
		}
		public JournalEntries(string ConnectionStr, String L_JournalEntriesID_Value)
		{
			ExecCommand = new JournalEntries_Command(ConnectionStr, this);
			ExecCommand.Load(L_JournalEntriesID_Value);
		}
        private String _L_JournalEntriesID;
        public readonly bool _L_JournalEntriesID_PKFlag = true;
        public readonly bool _L_JournalEntriesID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_JournalEntriesID = false;
		public String L_JournalEntriesID
		{
			get
			{
				return _L_JournalEntriesID;
			}
			set
			{
				if (_L_JournalEntriesID != value)
                {
					_L_JournalEntriesID = value;
					EditL_JournalEntriesID = true;
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

        private String _L_DocType;
        public readonly bool _L_DocType_PKFlag = false;
        public readonly bool _L_DocType_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_DocType = false;
		public String L_DocType
		{
			get
			{
				return _L_DocType;
			}
			set
			{
				if (_L_DocType != value)
                {
					_L_DocType = value;
					EditL_DocType = true;
				}
			}
		}

        private String _AutomaticWT;
        public readonly bool _AutomaticWT_PKFlag = false;
        public readonly bool _AutomaticWT_IDTFlag = false;
		[XmlIgnore]
		public bool EditAutomaticWT = false;
		public String AutomaticWT
		{
			get
			{
				return _AutomaticWT;
			}
			set
			{
				if (_AutomaticWT != value)
                {
					_AutomaticWT = value;
					EditAutomaticWT = true;
				}
			}
		}

        private String _AutoVAT;
        public readonly bool _AutoVAT_PKFlag = false;
        public readonly bool _AutoVAT_IDTFlag = false;
		[XmlIgnore]
		public bool EditAutoVAT = false;
		public String AutoVAT
		{
			get
			{
				return _AutoVAT;
			}
			set
			{
				if (_AutoVAT != value)
                {
					_AutoVAT = value;
					EditAutoVAT = true;
				}
			}
		}

        private String _BlockDunningLetter;
        public readonly bool _BlockDunningLetter_PKFlag = false;
        public readonly bool _BlockDunningLetter_IDTFlag = false;
		[XmlIgnore]
		public bool EditBlockDunningLetter = false;
		public String BlockDunningLetter
		{
			get
			{
				return _BlockDunningLetter;
			}
			set
			{
				if (_BlockDunningLetter != value)
                {
					_BlockDunningLetter = value;
					EditBlockDunningLetter = true;
				}
			}
		}

        private String _Corisptivi;
        public readonly bool _Corisptivi_PKFlag = false;
        public readonly bool _Corisptivi_IDTFlag = false;
		[XmlIgnore]
		public bool EditCorisptivi = false;
		public String Corisptivi
		{
			get
			{
				return _Corisptivi;
			}
			set
			{
				if (_Corisptivi != value)
                {
					_Corisptivi = value;
					EditCorisptivi = true;
				}
			}
		}

        private String _DeferredTax;
        public readonly bool _DeferredTax_PKFlag = false;
        public readonly bool _DeferredTax_IDTFlag = false;
		[XmlIgnore]
		public bool EditDeferredTax = false;
		public String DeferredTax
		{
			get
			{
				return _DeferredTax;
			}
			set
			{
				if (_DeferredTax != value)
                {
					_DeferredTax = value;
					EditDeferredTax = true;
				}
			}
		}

        private String _DocumentType;
        public readonly bool _DocumentType_PKFlag = false;
        public readonly bool _DocumentType_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocumentType = false;
		public String DocumentType
		{
			get
			{
				return _DocumentType;
			}
			set
			{
				if (_DocumentType != value)
                {
					_DocumentType = value;
					EditDocumentType = true;
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

        private String _ECDPostingType;
        public readonly bool _ECDPostingType_PKFlag = false;
        public readonly bool _ECDPostingType_IDTFlag = false;
		[XmlIgnore]
		public bool EditECDPostingType = false;
		public String ECDPostingType
		{
			get
			{
				return _ECDPostingType;
			}
			set
			{
				if (_ECDPostingType != value)
                {
					_ECDPostingType = value;
					EditECDPostingType = true;
				}
			}
		}

        private String _ExcludeFromTaxReportControlStatementVAT;
        public readonly bool _ExcludeFromTaxReportControlStatementVAT_PKFlag = false;
        public readonly bool _ExcludeFromTaxReportControlStatementVAT_IDTFlag = false;
		[XmlIgnore]
		public bool EditExcludeFromTaxReportControlStatementVAT = false;
		public String ExcludeFromTaxReportControlStatementVAT
		{
			get
			{
				return _ExcludeFromTaxReportControlStatementVAT;
			}
			set
			{
				if (_ExcludeFromTaxReportControlStatementVAT != value)
                {
					_ExcludeFromTaxReportControlStatementVAT = value;
					EditExcludeFromTaxReportControlStatementVAT = true;
				}
			}
		}

        private Nullable<Int32> _ExposedTransNumber;
        public readonly bool _ExposedTransNumber_PKFlag = false;
        public readonly bool _ExposedTransNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditExposedTransNumber = false;
		public Nullable<Int32> ExposedTransNumber
		{
			get
			{
				return _ExposedTransNumber;
			}
			set
			{
				if (_ExposedTransNumber != value)
                {
					_ExposedTransNumber = value;
					EditExposedTransNumber = true;
				}
			}
		}

        private String _Indicator;
        public readonly bool _Indicator_PKFlag = false;
        public readonly bool _Indicator_IDTFlag = false;
		[XmlIgnore]
		public bool EditIndicator = false;
		public String Indicator
		{
			get
			{
				return _Indicator;
			}
			set
			{
				if (_Indicator != value)
                {
					_Indicator = value;
					EditIndicator = true;
				}
			}
		}

        private String _IsCostCenterTransfer;
        public readonly bool _IsCostCenterTransfer_PKFlag = false;
        public readonly bool _IsCostCenterTransfer_IDTFlag = false;
		[XmlIgnore]
		public bool EditIsCostCenterTransfer = false;
		public String IsCostCenterTransfer
		{
			get
			{
				return _IsCostCenterTransfer;
			}
			set
			{
				if (_IsCostCenterTransfer != value)
                {
					_IsCostCenterTransfer = value;
					EditIsCostCenterTransfer = true;
				}
			}
		}

        private Nullable<Int32> _LocationCode;
        public readonly bool _LocationCode_PKFlag = false;
        public readonly bool _LocationCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditLocationCode = false;
		public Nullable<Int32> LocationCode
		{
			get
			{
				return _LocationCode;
			}
			set
			{
				if (_LocationCode != value)
                {
					_LocationCode = value;
					EditLocationCode = true;
				}
			}
		}

        private String _Memo;
        public readonly bool _Memo_PKFlag = false;
        public readonly bool _Memo_IDTFlag = false;
		[XmlIgnore]
		public bool EditMemo = false;
		public String Memo
		{
			get
			{
				return _Memo;
			}
			set
			{
				if (_Memo != value)
                {
					_Memo = value;
					EditMemo = true;
				}
			}
		}

        private String _OperationCode;
        public readonly bool _OperationCode_PKFlag = false;
        public readonly bool _OperationCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditOperationCode = false;
		public String OperationCode
		{
			get
			{
				return _OperationCode;
			}
			set
			{
				if (_OperationCode != value)
                {
					_OperationCode = value;
					EditOperationCode = true;
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

        private String _Reference;
        public readonly bool _Reference_PKFlag = false;
        public readonly bool _Reference_IDTFlag = false;
		[XmlIgnore]
		public bool EditReference = false;
		public String Reference
		{
			get
			{
				return _Reference;
			}
			set
			{
				if (_Reference != value)
                {
					_Reference = value;
					EditReference = true;
				}
			}
		}

        private String _Reference2;
        public readonly bool _Reference2_PKFlag = false;
        public readonly bool _Reference2_IDTFlag = false;
		[XmlIgnore]
		public bool EditReference2 = false;
		public String Reference2
		{
			get
			{
				return _Reference2;
			}
			set
			{
				if (_Reference2 != value)
                {
					_Reference2 = value;
					EditReference2 = true;
				}
			}
		}

        private String _Reference3;
        public readonly bool _Reference3_PKFlag = false;
        public readonly bool _Reference3_IDTFlag = false;
		[XmlIgnore]
		public bool EditReference3 = false;
		public String Reference3
		{
			get
			{
				return _Reference3;
			}
			set
			{
				if (_Reference3 != value)
                {
					_Reference3 = value;
					EditReference3 = true;
				}
			}
		}

        private Nullable<DateTime> _ReferenceDate;
        public readonly bool _ReferenceDate_PKFlag = false;
        public readonly bool _ReferenceDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditReferenceDate = false;
		public Nullable<DateTime> ReferenceDate
		{
			get
			{
				return _ReferenceDate;
			}
			set
			{
				if (_ReferenceDate != value)
                {
					_ReferenceDate = value;
					EditReferenceDate = true;
				}
			}
		}

        private String _Report347;
        public readonly bool _Report347_PKFlag = false;
        public readonly bool _Report347_IDTFlag = false;
		[XmlIgnore]
		public bool EditReport347 = false;
		public String Report347
		{
			get
			{
				return _Report347;
			}
			set
			{
				if (_Report347 != value)
                {
					_Report347 = value;
					EditReport347 = true;
				}
			}
		}

        private String _ReportEU;
        public readonly bool _ReportEU_PKFlag = false;
        public readonly bool _ReportEU_IDTFlag = false;
		[XmlIgnore]
		public bool EditReportEU = false;
		public String ReportEU
		{
			get
			{
				return _ReportEU;
			}
			set
			{
				if (_ReportEU != value)
                {
					_ReportEU = value;
					EditReportEU = true;
				}
			}
		}

        private String _ReportingSectionControlStatementVAT;
        public readonly bool _ReportingSectionControlStatementVAT_PKFlag = false;
        public readonly bool _ReportingSectionControlStatementVAT_IDTFlag = false;
		[XmlIgnore]
		public bool EditReportingSectionControlStatementVAT = false;
		public String ReportingSectionControlStatementVAT
		{
			get
			{
				return _ReportingSectionControlStatementVAT;
			}
			set
			{
				if (_ReportingSectionControlStatementVAT != value)
                {
					_ReportingSectionControlStatementVAT = value;
					EditReportingSectionControlStatementVAT = true;
				}
			}
		}

        private String _ResidenceNumberType;
        public readonly bool _ResidenceNumberType_PKFlag = false;
        public readonly bool _ResidenceNumberType_IDTFlag = false;
		[XmlIgnore]
		public bool EditResidenceNumberType = false;
		public String ResidenceNumberType
		{
			get
			{
				return _ResidenceNumberType;
			}
			set
			{
				if (_ResidenceNumberType != value)
                {
					_ResidenceNumberType = value;
					EditResidenceNumberType = true;
				}
			}
		}

        private Nullable<Int32> _Series;
        public readonly bool _Series_PKFlag = false;
        public readonly bool _Series_IDTFlag = false;
		[XmlIgnore]
		public bool EditSeries = false;
		public Nullable<Int32> Series
		{
			get
			{
				return _Series;
			}
			set
			{
				if (_Series != value)
                {
					_Series = value;
					EditSeries = true;
				}
			}
		}

        private String _StampTax;
        public readonly bool _StampTax_PKFlag = false;
        public readonly bool _StampTax_IDTFlag = false;
		[XmlIgnore]
		public bool EditStampTax = false;
		public String StampTax
		{
			get
			{
				return _StampTax;
			}
			set
			{
				if (_StampTax != value)
                {
					_StampTax = value;
					EditStampTax = true;
				}
			}
		}

        private Nullable<DateTime> _StornoDate;
        public readonly bool _StornoDate_PKFlag = false;
        public readonly bool _StornoDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditStornoDate = false;
		public Nullable<DateTime> StornoDate
		{
			get
			{
				return _StornoDate;
			}
			set
			{
				if (_StornoDate != value)
                {
					_StornoDate = value;
					EditStornoDate = true;
				}
			}
		}

        private Nullable<DateTime> _TaxDate;
        public readonly bool _TaxDate_PKFlag = false;
        public readonly bool _TaxDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxDate = false;
		public Nullable<DateTime> TaxDate
		{
			get
			{
				return _TaxDate;
			}
			set
			{
				if (_TaxDate != value)
                {
					_TaxDate = value;
					EditTaxDate = true;
				}
			}
		}

        private String _TransactionCode;
        public readonly bool _TransactionCode_PKFlag = false;
        public readonly bool _TransactionCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditTransactionCode = false;
		public String TransactionCode
		{
			get
			{
				return _TransactionCode;
			}
			set
			{
				if (_TransactionCode != value)
                {
					_TransactionCode = value;
					EditTransactionCode = true;
				}
			}
		}

        private String _UseAutoStorno;
        public readonly bool _UseAutoStorno_PKFlag = false;
        public readonly bool _UseAutoStorno_IDTFlag = false;
		[XmlIgnore]
		public bool EditUseAutoStorno = false;
		public String UseAutoStorno
		{
			get
			{
				return _UseAutoStorno;
			}
			set
			{
				if (_UseAutoStorno != value)
                {
					_UseAutoStorno = value;
					EditUseAutoStorno = true;
				}
			}
		}

        private Nullable<DateTime> _VatDate;
        public readonly bool _VatDate_PKFlag = false;
        public readonly bool _VatDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditVatDate = false;
		public Nullable<DateTime> VatDate
		{
			get
			{
				return _VatDate;
			}
			set
			{
				if (_VatDate != value)
                {
					_VatDate = value;
					EditVatDate = true;
				}
			}
		}

	}
	public partial class JournalEntries_Command
	{
		string TableName = "JournalEntries";

		JournalEntries _JournalEntries = null;
		DBHelper _DBHelper = null;

		internal JournalEntries_Command(JournalEntries obj_JournalEntries)
		{
			this._JournalEntries = obj_JournalEntries;
			this._DBHelper = new DBHelper();
		}

		internal JournalEntries_Command(string ConnectionStr, JournalEntries obj_JournalEntries)
		{
			this._JournalEntries = obj_JournalEntries;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _JournalEntries.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _JournalEntries.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_JournalEntries, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_JournalEntries, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_JournalEntriesID_Value)
        {
            Load(_DBHelper, L_JournalEntriesID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_JournalEntriesID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_JournalEntriesID", L_JournalEntriesID_Value, (DbType)Enum.Parse(typeof(DbType), L_JournalEntriesID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM JournalEntries WHERE L_JournalEntriesID=@L_JournalEntriesID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_JournalEntriesID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_JournalEntriesID", L_JournalEntriesID_Value, (DbType)Enum.Parse(typeof(DbType), L_JournalEntriesID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM JournalEntries WHERE L_JournalEntriesID=@L_JournalEntriesID", "JournalEntries", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("JournalEntries");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<JournalEntries> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<JournalEntries> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("JournalEntries");
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
            List<JournalEntries> Res = new List<JournalEntries>();
            foreach (DataRow Dr in dt.Rows)
            {
                JournalEntries Item = new JournalEntries();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _JournalEntries.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _JournalEntries.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_JournalEntries);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _JournalEntries.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _JournalEntries.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_JournalEntries);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _JournalEntries.GetType();
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

                foreach (PropertyInfo ProInfo in _JournalEntries.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _JournalEntries.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_JournalEntries))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_JournalEntries, null);
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
            Type MyType = _JournalEntries.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _JournalEntries.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _JournalEntries.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_JournalEntries) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_JournalEntries, null);
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
            Type MyType = _JournalEntries.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _JournalEntries.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _JournalEntries.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_JournalEntries) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_JournalEntries, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_JournalEntries, null)));
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
            string ssql = "DELETE JournalEntries WHERE L_JournalEntriesID=@L_JournalEntriesID";
            Parameter.Add(new DBParameter("@L_JournalEntriesID", _JournalEntries.L_JournalEntriesID));

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
