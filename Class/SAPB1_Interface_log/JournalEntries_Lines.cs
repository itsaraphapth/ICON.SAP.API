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
	[XmlRootAttribute("JournalEntries_Lines", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class JournalEntries_Lines
	{
		[XmlIgnore]
		[NonSerialized]
		public JournalEntries_Lines_Command ExecCommand = null;
		public JournalEntries_Lines()
		{
			ExecCommand = new JournalEntries_Lines_Command(this);
		}
		public JournalEntries_Lines(string ConnectionStr)
		{
			ExecCommand = new JournalEntries_Lines_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new JournalEntries_Lines_Command(ConnectionStr, this);
		}
		public JournalEntries_Lines(string ConnectionStr, String L_JournalEntries_LinesID_Value)
		{
			ExecCommand = new JournalEntries_Lines_Command(ConnectionStr, this);
			ExecCommand.Load(L_JournalEntries_LinesID_Value);
		}
        private String _L_JournalEntries_LinesID;
        public readonly bool _L_JournalEntries_LinesID_PKFlag = true;
        public readonly bool _L_JournalEntries_LinesID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_JournalEntries_LinesID = false;
		public String L_JournalEntries_LinesID
		{
			get
			{
				return _L_JournalEntries_LinesID;
			}
			set
			{
				if (_L_JournalEntries_LinesID != value)
                {
					_L_JournalEntries_LinesID = value;
					EditL_JournalEntries_LinesID = true;
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

        private String _L_JournalEntriesID;
        public readonly bool _L_JournalEntriesID_PKFlag = false;
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

        private String _AdditionalReference;
        public readonly bool _AdditionalReference_PKFlag = false;
        public readonly bool _AdditionalReference_IDTFlag = false;
		[XmlIgnore]
		public bool EditAdditionalReference = false;
		public String AdditionalReference
		{
			get
			{
				return _AdditionalReference;
			}
			set
			{
				if (_AdditionalReference != value)
                {
					_AdditionalReference = value;
					EditAdditionalReference = true;
				}
			}
		}

        private Nullable<Decimal> _BaseSum;
        public readonly bool _BaseSum_PKFlag = false;
        public readonly bool _BaseSum_IDTFlag = false;
		[XmlIgnore]
		public bool EditBaseSum = false;
		public Nullable<Decimal> BaseSum
		{
			get
			{
				return _BaseSum;
			}
			set
			{
				if (_BaseSum != value)
                {
					_BaseSum = value;
					EditBaseSum = true;
				}
			}
		}

        private Nullable<Int32> _BlockReason;
        public readonly bool _BlockReason_PKFlag = false;
        public readonly bool _BlockReason_IDTFlag = false;
		[XmlIgnore]
		public bool EditBlockReason = false;
		public Nullable<Int32> BlockReason
		{
			get
			{
				return _BlockReason;
			}
			set
			{
				if (_BlockReason != value)
                {
					_BlockReason = value;
					EditBlockReason = true;
				}
			}
		}

        private Nullable<Int32> _BPLID;
        public readonly bool _BPLID_PKFlag = false;
        public readonly bool _BPLID_IDTFlag = false;
		[XmlIgnore]
		public bool EditBPLID = false;
		public Nullable<Int32> BPLID
		{
			get
			{
				return _BPLID;
			}
			set
			{
				if (_BPLID != value)
                {
					_BPLID = value;
					EditBPLID = true;
				}
			}
		}

        private String _ContraAccount;
        public readonly bool _ContraAccount_PKFlag = false;
        public readonly bool _ContraAccount_IDTFlag = false;
		[XmlIgnore]
		public bool EditContraAccount = false;
		public String ContraAccount
		{
			get
			{
				return _ContraAccount;
			}
			set
			{
				if (_ContraAccount != value)
                {
					_ContraAccount = value;
					EditContraAccount = true;
				}
			}
		}

        private String _ControlAccount;
        public readonly bool _ControlAccount_PKFlag = false;
        public readonly bool _ControlAccount_IDTFlag = false;
		[XmlIgnore]
		public bool EditControlAccount = false;
		public String ControlAccount
		{
			get
			{
				return _ControlAccount;
			}
			set
			{
				if (_ControlAccount != value)
                {
					_ControlAccount = value;
					EditControlAccount = true;
				}
			}
		}

        private String _CostingCode;
        public readonly bool _CostingCode_PKFlag = false;
        public readonly bool _CostingCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditCostingCode = false;
		public String CostingCode
		{
			get
			{
				return _CostingCode;
			}
			set
			{
				if (_CostingCode != value)
                {
					_CostingCode = value;
					EditCostingCode = true;
				}
			}
		}

        private String _CostingCode2;
        public readonly bool _CostingCode2_PKFlag = false;
        public readonly bool _CostingCode2_IDTFlag = false;
		[XmlIgnore]
		public bool EditCostingCode2 = false;
		public String CostingCode2
		{
			get
			{
				return _CostingCode2;
			}
			set
			{
				if (_CostingCode2 != value)
                {
					_CostingCode2 = value;
					EditCostingCode2 = true;
				}
			}
		}

        private String _CostingCode3;
        public readonly bool _CostingCode3_PKFlag = false;
        public readonly bool _CostingCode3_IDTFlag = false;
		[XmlIgnore]
		public bool EditCostingCode3 = false;
		public String CostingCode3
		{
			get
			{
				return _CostingCode3;
			}
			set
			{
				if (_CostingCode3 != value)
                {
					_CostingCode3 = value;
					EditCostingCode3 = true;
				}
			}
		}

        private String _CostingCode4;
        public readonly bool _CostingCode4_PKFlag = false;
        public readonly bool _CostingCode4_IDTFlag = false;
		[XmlIgnore]
		public bool EditCostingCode4 = false;
		public String CostingCode4
		{
			get
			{
				return _CostingCode4;
			}
			set
			{
				if (_CostingCode4 != value)
                {
					_CostingCode4 = value;
					EditCostingCode4 = true;
				}
			}
		}

        private String _CostingCode5;
        public readonly bool _CostingCode5_PKFlag = false;
        public readonly bool _CostingCode5_IDTFlag = false;
		[XmlIgnore]
		public bool EditCostingCode5 = false;
		public String CostingCode5
		{
			get
			{
				return _CostingCode5;
			}
			set
			{
				if (_CostingCode5 != value)
                {
					_CostingCode5 = value;
					EditCostingCode5 = true;
				}
			}
		}

        private Nullable<Decimal> _Credit;
        public readonly bool _Credit_PKFlag = false;
        public readonly bool _Credit_IDTFlag = false;
		[XmlIgnore]
		public bool EditCredit = false;
		public Nullable<Decimal> Credit
		{
			get
			{
				return _Credit;
			}
			set
			{
				if (_Credit != value)
                {
					_Credit = value;
					EditCredit = true;
				}
			}
		}

        private Nullable<Decimal> _CreditSys;
        public readonly bool _CreditSys_PKFlag = false;
        public readonly bool _CreditSys_IDTFlag = false;
		[XmlIgnore]
		public bool EditCreditSys = false;
		public Nullable<Decimal> CreditSys
		{
			get
			{
				return _CreditSys;
			}
			set
			{
				if (_CreditSys != value)
                {
					_CreditSys = value;
					EditCreditSys = true;
				}
			}
		}

        private Nullable<Decimal> _Debit;
        public readonly bool _Debit_PKFlag = false;
        public readonly bool _Debit_IDTFlag = false;
		[XmlIgnore]
		public bool EditDebit = false;
		public Nullable<Decimal> Debit
		{
			get
			{
				return _Debit;
			}
			set
			{
				if (_Debit != value)
                {
					_Debit = value;
					EditDebit = true;
				}
			}
		}

        private Nullable<Decimal> _DebitSys;
        public readonly bool _DebitSys_PKFlag = false;
        public readonly bool _DebitSys_IDTFlag = false;
		[XmlIgnore]
		public bool EditDebitSys = false;
		public Nullable<Decimal> DebitSys
		{
			get
			{
				return _DebitSys;
			}
			set
			{
				if (_DebitSys != value)
                {
					_DebitSys = value;
					EditDebitSys = true;
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

        private Nullable<Decimal> _FCCredit;
        public readonly bool _FCCredit_PKFlag = false;
        public readonly bool _FCCredit_IDTFlag = false;
		[XmlIgnore]
		public bool EditFCCredit = false;
		public Nullable<Decimal> FCCredit
		{
			get
			{
				return _FCCredit;
			}
			set
			{
				if (_FCCredit != value)
                {
					_FCCredit = value;
					EditFCCredit = true;
				}
			}
		}

        private String _FCCurrency;
        public readonly bool _FCCurrency_PKFlag = false;
        public readonly bool _FCCurrency_IDTFlag = false;
		[XmlIgnore]
		public bool EditFCCurrency = false;
		public String FCCurrency
		{
			get
			{
				return _FCCurrency;
			}
			set
			{
				if (_FCCurrency != value)
                {
					_FCCurrency = value;
					EditFCCurrency = true;
				}
			}
		}

        private Nullable<Decimal> _FCDebit;
        public readonly bool _FCDebit_PKFlag = false;
        public readonly bool _FCDebit_IDTFlag = false;
		[XmlIgnore]
		public bool EditFCDebit = false;
		public Nullable<Decimal> FCDebit
		{
			get
			{
				return _FCDebit;
			}
			set
			{
				if (_FCDebit != value)
                {
					_FCDebit = value;
					EditFCDebit = true;
				}
			}
		}

        private String _FederalTaxID;
        public readonly bool _FederalTaxID_PKFlag = false;
        public readonly bool _FederalTaxID_IDTFlag = false;
		[XmlIgnore]
		public bool EditFederalTaxID = false;
		public String FederalTaxID
		{
			get
			{
				return _FederalTaxID;
			}
			set
			{
				if (_FederalTaxID != value)
                {
					_FederalTaxID = value;
					EditFederalTaxID = true;
				}
			}
		}

        private Nullable<Decimal> _GrossValue;
        public readonly bool _GrossValue_PKFlag = false;
        public readonly bool _GrossValue_IDTFlag = false;
		[XmlIgnore]
		public bool EditGrossValue = false;
		public Nullable<Decimal> GrossValue
		{
			get
			{
				return _GrossValue;
			}
			set
			{
				if (_GrossValue != value)
                {
					_GrossValue = value;
					EditGrossValue = true;
				}
			}
		}

        private String _LineMemo;
        public readonly bool _LineMemo_PKFlag = false;
        public readonly bool _LineMemo_IDTFlag = false;
		[XmlIgnore]
		public bool EditLineMemo = false;
		public String LineMemo
		{
			get
			{
				return _LineMemo;
			}
			set
			{
				if (_LineMemo != value)
                {
					_LineMemo = value;
					EditLineMemo = true;
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

        private String _PaymentBlock;
        public readonly bool _PaymentBlock_PKFlag = false;
        public readonly bool _PaymentBlock_IDTFlag = false;
		[XmlIgnore]
		public bool EditPaymentBlock = false;
		public String PaymentBlock
		{
			get
			{
				return _PaymentBlock;
			}
			set
			{
				if (_PaymentBlock != value)
                {
					_PaymentBlock = value;
					EditPaymentBlock = true;
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

        private String _Reference1;
        public readonly bool _Reference1_PKFlag = false;
        public readonly bool _Reference1_IDTFlag = false;
		[XmlIgnore]
		public bool EditReference1 = false;
		public String Reference1
		{
			get
			{
				return _Reference1;
			}
			set
			{
				if (_Reference1 != value)
                {
					_Reference1 = value;
					EditReference1 = true;
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

        private Nullable<DateTime> _ReferenceDate1;
        public readonly bool _ReferenceDate1_PKFlag = false;
        public readonly bool _ReferenceDate1_IDTFlag = false;
		[XmlIgnore]
		public bool EditReferenceDate1 = false;
		public Nullable<DateTime> ReferenceDate1
		{
			get
			{
				return _ReferenceDate1;
			}
			set
			{
				if (_ReferenceDate1 != value)
                {
					_ReferenceDate1 = value;
					EditReferenceDate1 = true;
				}
			}
		}

        private Nullable<DateTime> _ReferenceDate2;
        public readonly bool _ReferenceDate2_PKFlag = false;
        public readonly bool _ReferenceDate2_IDTFlag = false;
		[XmlIgnore]
		public bool EditReferenceDate2 = false;
		public Nullable<DateTime> ReferenceDate2
		{
			get
			{
				return _ReferenceDate2;
			}
			set
			{
				if (_ReferenceDate2 != value)
                {
					_ReferenceDate2 = value;
					EditReferenceDate2 = true;
				}
			}
		}

        private String _ShortName;
        public readonly bool _ShortName_PKFlag = false;
        public readonly bool _ShortName_IDTFlag = false;
		[XmlIgnore]
		public bool EditShortName = false;
		public String ShortName
		{
			get
			{
				return _ShortName;
			}
			set
			{
				if (_ShortName != value)
                {
					_ShortName = value;
					EditShortName = true;
				}
			}
		}

        private Nullable<Decimal> _SystemBaseAmount;
        public readonly bool _SystemBaseAmount_PKFlag = false;
        public readonly bool _SystemBaseAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditSystemBaseAmount = false;
		public Nullable<Decimal> SystemBaseAmount
		{
			get
			{
				return _SystemBaseAmount;
			}
			set
			{
				if (_SystemBaseAmount != value)
                {
					_SystemBaseAmount = value;
					EditSystemBaseAmount = true;
				}
			}
		}

        private Nullable<Decimal> _SystemVatAmount;
        public readonly bool _SystemVatAmount_PKFlag = false;
        public readonly bool _SystemVatAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditSystemVatAmount = false;
		public Nullable<Decimal> SystemVatAmount
		{
			get
			{
				return _SystemVatAmount;
			}
			set
			{
				if (_SystemVatAmount != value)
                {
					_SystemVatAmount = value;
					EditSystemVatAmount = true;
				}
			}
		}

        private String _TaxCode;
        public readonly bool _TaxCode_PKFlag = false;
        public readonly bool _TaxCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxCode = false;
		public String TaxCode
		{
			get
			{
				return _TaxCode;
			}
			set
			{
				if (_TaxCode != value)
                {
					_TaxCode = value;
					EditTaxCode = true;
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

        private String _TaxGroup;
        public readonly bool _TaxGroup_PKFlag = false;
        public readonly bool _TaxGroup_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxGroup = false;
		public String TaxGroup
		{
			get
			{
				return _TaxGroup;
			}
			set
			{
				if (_TaxGroup != value)
                {
					_TaxGroup = value;
					EditTaxGroup = true;
				}
			}
		}

        private String _TaxPostAccount;
        public readonly bool _TaxPostAccount_PKFlag = false;
        public readonly bool _TaxPostAccount_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxPostAccount = false;
		public String TaxPostAccount
		{
			get
			{
				return _TaxPostAccount;
			}
			set
			{
				if (_TaxPostAccount != value)
                {
					_TaxPostAccount = value;
					EditTaxPostAccount = true;
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

        private String _VatLine;
        public readonly bool _VatLine_PKFlag = false;
        public readonly bool _VatLine_IDTFlag = false;
		[XmlIgnore]
		public bool EditVatLine = false;
		public String VatLine
		{
			get
			{
				return _VatLine;
			}
			set
			{
				if (_VatLine != value)
                {
					_VatLine = value;
					EditVatLine = true;
				}
			}
		}

        private String _WTLiable;
        public readonly bool _WTLiable_PKFlag = false;
        public readonly bool _WTLiable_IDTFlag = false;
		[XmlIgnore]
		public bool EditWTLiable = false;
		public String WTLiable
		{
			get
			{
				return _WTLiable;
			}
			set
			{
				if (_WTLiable != value)
                {
					_WTLiable = value;
					EditWTLiable = true;
				}
			}
		}

        private String _WTRow;
        public readonly bool _WTRow_PKFlag = false;
        public readonly bool _WTRow_IDTFlag = false;
		[XmlIgnore]
		public bool EditWTRow = false;
		public String WTRow
		{
			get
			{
				return _WTRow;
			}
			set
			{
				if (_WTRow != value)
                {
					_WTRow = value;
					EditWTRow = true;
				}
			}
		}

	}
	public partial class JournalEntries_Lines_Command
	{
		string TableName = "JournalEntries_Lines";

		JournalEntries_Lines _JournalEntries_Lines = null;
		DBHelper _DBHelper = null;

		internal JournalEntries_Lines_Command(JournalEntries_Lines obj_JournalEntries_Lines)
		{
			this._JournalEntries_Lines = obj_JournalEntries_Lines;
			this._DBHelper = new DBHelper();
		}

		internal JournalEntries_Lines_Command(string ConnectionStr, JournalEntries_Lines obj_JournalEntries_Lines)
		{
			this._JournalEntries_Lines = obj_JournalEntries_Lines;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _JournalEntries_Lines.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _JournalEntries_Lines.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_JournalEntries_Lines, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_JournalEntries_Lines, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_JournalEntries_LinesID_Value)
        {
            Load(_DBHelper, L_JournalEntries_LinesID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_JournalEntries_LinesID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_JournalEntries_LinesID", L_JournalEntries_LinesID_Value, (DbType)Enum.Parse(typeof(DbType), L_JournalEntries_LinesID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM JournalEntries_Lines WHERE L_JournalEntries_LinesID=@L_JournalEntries_LinesID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_JournalEntries_LinesID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_JournalEntries_LinesID", L_JournalEntries_LinesID_Value, (DbType)Enum.Parse(typeof(DbType), L_JournalEntries_LinesID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM JournalEntries_Lines WHERE L_JournalEntries_LinesID=@L_JournalEntries_LinesID", "JournalEntries_Lines", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("JournalEntries_Lines");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<JournalEntries_Lines> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<JournalEntries_Lines> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("JournalEntries_Lines");
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
            List<JournalEntries_Lines> Res = new List<JournalEntries_Lines>();
            foreach (DataRow Dr in dt.Rows)
            {
                JournalEntries_Lines Item = new JournalEntries_Lines();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _JournalEntries_Lines.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _JournalEntries_Lines.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_JournalEntries_Lines);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _JournalEntries_Lines.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _JournalEntries_Lines.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_JournalEntries_Lines);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _JournalEntries_Lines.GetType();
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

                foreach (PropertyInfo ProInfo in _JournalEntries_Lines.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _JournalEntries_Lines.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_JournalEntries_Lines))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_JournalEntries_Lines, null);
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
            Type MyType = _JournalEntries_Lines.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _JournalEntries_Lines.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _JournalEntries_Lines.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_JournalEntries_Lines) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_JournalEntries_Lines, null);
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
            Type MyType = _JournalEntries_Lines.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _JournalEntries_Lines.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _JournalEntries_Lines.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_JournalEntries_Lines) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_JournalEntries_Lines, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_JournalEntries_Lines, null)));
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
            string ssql = "DELETE JournalEntries_Lines WHERE L_JournalEntries_LinesID=@L_JournalEntries_LinesID";
            Parameter.Add(new DBParameter("@L_JournalEntries_LinesID", _JournalEntries_Lines.L_JournalEntries_LinesID));

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
