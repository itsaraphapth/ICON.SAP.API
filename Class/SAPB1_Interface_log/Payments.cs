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
	[XmlRootAttribute("Payments", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class Payments
	{
		[XmlIgnore]
		[NonSerialized]
		public Payments_Command ExecCommand = null;
		public Payments()
		{
			ExecCommand = new Payments_Command(this);
		}
		public Payments(string ConnectionStr)
		{
			ExecCommand = new Payments_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new Payments_Command(ConnectionStr, this);
		}
		public Payments(string ConnectionStr, String L_PaymentsID_Value)
		{
			ExecCommand = new Payments_Command(ConnectionStr, this);
			ExecCommand.Load(L_PaymentsID_Value);
		}
        private String _L_PaymentsID;
        public readonly bool _L_PaymentsID_PKFlag = true;
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

        private String _L_RctEntry;
        public readonly bool _L_RctEntry_PKFlag = false;
        public readonly bool _L_RctEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_RctEntry = false;
		public String L_RctEntry
		{
			get
			{
				return _L_RctEntry;
			}
			set
			{
				if (_L_RctEntry != value)
                {
					_L_RctEntry = value;
					EditL_RctEntry = true;
				}
			}
		}

        private String _Address;
        public readonly bool _Address_PKFlag = false;
        public readonly bool _Address_IDTFlag = false;
		[XmlIgnore]
		public bool EditAddress = false;
		public String Address
		{
			get
			{
				return _Address;
			}
			set
			{
				if (_Address != value)
                {
					_Address = value;
					EditAddress = true;
				}
			}
		}

        private String _ApplyVAT;
        public readonly bool _ApplyVAT_PKFlag = false;
        public readonly bool _ApplyVAT_IDTFlag = false;
		[XmlIgnore]
		public bool EditApplyVAT = false;
		public String ApplyVAT
		{
			get
			{
				return _ApplyVAT;
			}
			set
			{
				if (_ApplyVAT != value)
                {
					_ApplyVAT = value;
					EditApplyVAT = true;
				}
			}
		}

        private String _BankAccount;
        public readonly bool _BankAccount_PKFlag = false;
        public readonly bool _BankAccount_IDTFlag = false;
		[XmlIgnore]
		public bool EditBankAccount = false;
		public String BankAccount
		{
			get
			{
				return _BankAccount;
			}
			set
			{
				if (_BankAccount != value)
                {
					_BankAccount = value;
					EditBankAccount = true;
				}
			}
		}

        private Nullable<Decimal> _BankChargeAmount;
        public readonly bool _BankChargeAmount_PKFlag = false;
        public readonly bool _BankChargeAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditBankChargeAmount = false;
		public Nullable<Decimal> BankChargeAmount
		{
			get
			{
				return _BankChargeAmount;
			}
			set
			{
				if (_BankChargeAmount != value)
                {
					_BankChargeAmount = value;
					EditBankChargeAmount = true;
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

        private String _BillOfExchangeAgent;
        public readonly bool _BillOfExchangeAgent_PKFlag = false;
        public readonly bool _BillOfExchangeAgent_IDTFlag = false;
		[XmlIgnore]
		public bool EditBillOfExchangeAgent = false;
		public String BillOfExchangeAgent
		{
			get
			{
				return _BillOfExchangeAgent;
			}
			set
			{
				if (_BillOfExchangeAgent != value)
                {
					_BillOfExchangeAgent = value;
					EditBillOfExchangeAgent = true;
				}
			}
		}

        private Nullable<Decimal> _BillOfExchangeAmount;
        public readonly bool _BillOfExchangeAmount_PKFlag = false;
        public readonly bool _BillOfExchangeAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditBillOfExchangeAmount = false;
		public Nullable<Decimal> BillOfExchangeAmount
		{
			get
			{
				return _BillOfExchangeAmount;
			}
			set
			{
				if (_BillOfExchangeAmount != value)
                {
					_BillOfExchangeAmount = value;
					EditBillOfExchangeAmount = true;
				}
			}
		}

        private String _BillofExchangeStatus;
        public readonly bool _BillofExchangeStatus_PKFlag = false;
        public readonly bool _BillofExchangeStatus_IDTFlag = false;
		[XmlIgnore]
		public bool EditBillofExchangeStatus = false;
		public String BillofExchangeStatus
		{
			get
			{
				return _BillofExchangeStatus;
			}
			set
			{
				if (_BillofExchangeStatus != value)
                {
					_BillofExchangeStatus = value;
					EditBillofExchangeStatus = true;
				}
			}
		}

        private Nullable<Int32> _BlanketAgreement;
        public readonly bool _BlanketAgreement_PKFlag = false;
        public readonly bool _BlanketAgreement_IDTFlag = false;
		[XmlIgnore]
		public bool EditBlanketAgreement = false;
		public Nullable<Int32> BlanketAgreement
		{
			get
			{
				return _BlanketAgreement;
			}
			set
			{
				if (_BlanketAgreement != value)
                {
					_BlanketAgreement = value;
					EditBlanketAgreement = true;
				}
			}
		}

        private String _BoeAccount;
        public readonly bool _BoeAccount_PKFlag = false;
        public readonly bool _BoeAccount_IDTFlag = false;
		[XmlIgnore]
		public bool EditBoeAccount = false;
		public String BoeAccount
		{
			get
			{
				return _BoeAccount;
			}
			set
			{
				if (_BoeAccount != value)
                {
					_BoeAccount = value;
					EditBoeAccount = true;
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

        private String _CardCode;
        public readonly bool _CardCode_PKFlag = false;
        public readonly bool _CardCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditCardCode = false;
		public String CardCode
		{
			get
			{
				return _CardCode;
			}
			set
			{
				if (_CardCode != value)
                {
					_CardCode = value;
					EditCardCode = true;
				}
			}
		}

        private String _CardName;
        public readonly bool _CardName_PKFlag = false;
        public readonly bool _CardName_IDTFlag = false;
		[XmlIgnore]
		public bool EditCardName = false;
		public String CardName
		{
			get
			{
				return _CardName;
			}
			set
			{
				if (_CardName != value)
                {
					_CardName = value;
					EditCardName = true;
				}
			}
		}

        private String _CashAccount;
        public readonly bool _CashAccount_PKFlag = false;
        public readonly bool _CashAccount_IDTFlag = false;
		[XmlIgnore]
		public bool EditCashAccount = false;
		public String CashAccount
		{
			get
			{
				return _CashAccount;
			}
			set
			{
				if (_CashAccount != value)
                {
					_CashAccount = value;
					EditCashAccount = true;
				}
			}
		}

        private Nullable<Decimal> _CashSum;
        public readonly bool _CashSum_PKFlag = false;
        public readonly bool _CashSum_IDTFlag = false;
		[XmlIgnore]
		public bool EditCashSum = false;
		public Nullable<Decimal> CashSum
		{
			get
			{
				return _CashSum;
			}
			set
			{
				if (_CashSum != value)
                {
					_CashSum = value;
					EditCashSum = true;
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

        private Nullable<Int32> _ContactPersonCode;
        public readonly bool _ContactPersonCode_PKFlag = false;
        public readonly bool _ContactPersonCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditContactPersonCode = false;
		public Nullable<Int32> ContactPersonCode
		{
			get
			{
				return _ContactPersonCode;
			}
			set
			{
				if (_ContactPersonCode != value)
                {
					_ContactPersonCode = value;
					EditContactPersonCode = true;
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

        private String _CounterReference;
        public readonly bool _CounterReference_PKFlag = false;
        public readonly bool _CounterReference_IDTFlag = false;
		[XmlIgnore]
		public bool EditCounterReference = false;
		public String CounterReference
		{
			get
			{
				return _CounterReference;
			}
			set
			{
				if (_CounterReference != value)
                {
					_CounterReference = value;
					EditCounterReference = true;
				}
			}
		}

        private Nullable<Decimal> _DeductionPercent;
        public readonly bool _DeductionPercent_PKFlag = false;
        public readonly bool _DeductionPercent_IDTFlag = false;
		[XmlIgnore]
		public bool EditDeductionPercent = false;
		public Nullable<Decimal> DeductionPercent
		{
			get
			{
				return _DeductionPercent;
			}
			set
			{
				if (_DeductionPercent != value)
                {
					_DeductionPercent = value;
					EditDeductionPercent = true;
				}
			}
		}

        private Nullable<Decimal> _DeductionSum;
        public readonly bool _DeductionSum_PKFlag = false;
        public readonly bool _DeductionSum_IDTFlag = false;
		[XmlIgnore]
		public bool EditDeductionSum = false;
		public Nullable<Decimal> DeductionSum
		{
			get
			{
				return _DeductionSum;
			}
			set
			{
				if (_DeductionSum != value)
                {
					_DeductionSum = value;
					EditDeductionSum = true;
				}
			}
		}

        private String _DocCurrency;
        public readonly bool _DocCurrency_PKFlag = false;
        public readonly bool _DocCurrency_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocCurrency = false;
		public String DocCurrency
		{
			get
			{
				return _DocCurrency;
			}
			set
			{
				if (_DocCurrency != value)
                {
					_DocCurrency = value;
					EditDocCurrency = true;
				}
			}
		}

        private Nullable<DateTime> _DocDate;
        public readonly bool _DocDate_PKFlag = false;
        public readonly bool _DocDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocDate = false;
		public Nullable<DateTime> DocDate
		{
			get
			{
				return _DocDate;
			}
			set
			{
				if (_DocDate != value)
                {
					_DocDate = value;
					EditDocDate = true;
				}
			}
		}

        private Nullable<Int32> _DocNum;
        public readonly bool _DocNum_PKFlag = false;
        public readonly bool _DocNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocNum = false;
		public Nullable<Int32> DocNum
		{
			get
			{
				return _DocNum;
			}
			set
			{
				if (_DocNum != value)
                {
					_DocNum = value;
					EditDocNum = true;
				}
			}
		}

        private String _DocObjectCode;
        public readonly bool _DocObjectCode_PKFlag = false;
        public readonly bool _DocObjectCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocObjectCode = false;
		public String DocObjectCode
		{
			get
			{
				return _DocObjectCode;
			}
			set
			{
				if (_DocObjectCode != value)
                {
					_DocObjectCode = value;
					EditDocObjectCode = true;
				}
			}
		}

        private Nullable<Decimal> _DocRate;
        public readonly bool _DocRate_PKFlag = false;
        public readonly bool _DocRate_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocRate = false;
		public Nullable<Decimal> DocRate
		{
			get
			{
				return _DocRate;
			}
			set
			{
				if (_DocRate != value)
                {
					_DocRate = value;
					EditDocRate = true;
				}
			}
		}

        private String _DocType;
        public readonly bool _DocType_PKFlag = false;
        public readonly bool _DocType_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocType = false;
		public String DocType
		{
			get
			{
				return _DocType;
			}
			set
			{
				if (_DocType != value)
                {
					_DocType = value;
					EditDocType = true;
				}
			}
		}

        private String _DocTypte;
        public readonly bool _DocTypte_PKFlag = false;
        public readonly bool _DocTypte_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocTypte = false;
		public String DocTypte
		{
			get
			{
				return _DocTypte;
			}
			set
			{
				if (_DocTypte != value)
                {
					_DocTypte = value;
					EditDocTypte = true;
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

        private String _HandWritten;
        public readonly bool _HandWritten_PKFlag = false;
        public readonly bool _HandWritten_IDTFlag = false;
		[XmlIgnore]
		public bool EditHandWritten = false;
		public String HandWritten
		{
			get
			{
				return _HandWritten;
			}
			set
			{
				if (_HandWritten != value)
                {
					_HandWritten = value;
					EditHandWritten = true;
				}
			}
		}

        private String _IsPayToBank;
        public readonly bool _IsPayToBank_PKFlag = false;
        public readonly bool _IsPayToBank_IDTFlag = false;
		[XmlIgnore]
		public bool EditIsPayToBank = false;
		public String IsPayToBank
		{
			get
			{
				return _IsPayToBank;
			}
			set
			{
				if (_IsPayToBank != value)
                {
					_IsPayToBank = value;
					EditIsPayToBank = true;
				}
			}
		}

        private String _JournalRemarks;
        public readonly bool _JournalRemarks_PKFlag = false;
        public readonly bool _JournalRemarks_IDTFlag = false;
		[XmlIgnore]
		public bool EditJournalRemarks = false;
		public String JournalRemarks
		{
			get
			{
				return _JournalRemarks;
			}
			set
			{
				if (_JournalRemarks != value)
                {
					_JournalRemarks = value;
					EditJournalRemarks = true;
				}
			}
		}

        private String _LocalCurrency;
        public readonly bool _LocalCurrency_PKFlag = false;
        public readonly bool _LocalCurrency_IDTFlag = false;
		[XmlIgnore]
		public bool EditLocalCurrency = false;
		public String LocalCurrency
		{
			get
			{
				return _LocalCurrency;
			}
			set
			{
				if (_LocalCurrency != value)
                {
					_LocalCurrency = value;
					EditLocalCurrency = true;
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

        private String _PaymentByWTCertif;
        public readonly bool _PaymentByWTCertif_PKFlag = false;
        public readonly bool _PaymentByWTCertif_IDTFlag = false;
		[XmlIgnore]
		public bool EditPaymentByWTCertif = false;
		public String PaymentByWTCertif
		{
			get
			{
				return _PaymentByWTCertif;
			}
			set
			{
				if (_PaymentByWTCertif != value)
                {
					_PaymentByWTCertif = value;
					EditPaymentByWTCertif = true;
				}
			}
		}

        private String _PaymentPriority;
        public readonly bool _PaymentPriority_PKFlag = false;
        public readonly bool _PaymentPriority_IDTFlag = false;
		[XmlIgnore]
		public bool EditPaymentPriority = false;
		public String PaymentPriority
		{
			get
			{
				return _PaymentPriority;
			}
			set
			{
				if (_PaymentPriority != value)
                {
					_PaymentPriority = value;
					EditPaymentPriority = true;
				}
			}
		}

        private String _PaymentType;
        public readonly bool _PaymentType_PKFlag = false;
        public readonly bool _PaymentType_IDTFlag = false;
		[XmlIgnore]
		public bool EditPaymentType = false;
		public String PaymentType
		{
			get
			{
				return _PaymentType;
			}
			set
			{
				if (_PaymentType != value)
                {
					_PaymentType = value;
					EditPaymentType = true;
				}
			}
		}

        private String _PayToBankAccountNo;
        public readonly bool _PayToBankAccountNo_PKFlag = false;
        public readonly bool _PayToBankAccountNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditPayToBankAccountNo = false;
		public String PayToBankAccountNo
		{
			get
			{
				return _PayToBankAccountNo;
			}
			set
			{
				if (_PayToBankAccountNo != value)
                {
					_PayToBankAccountNo = value;
					EditPayToBankAccountNo = true;
				}
			}
		}

        private String _PayToBankBranch;
        public readonly bool _PayToBankBranch_PKFlag = false;
        public readonly bool _PayToBankBranch_IDTFlag = false;
		[XmlIgnore]
		public bool EditPayToBankBranch = false;
		public String PayToBankBranch
		{
			get
			{
				return _PayToBankBranch;
			}
			set
			{
				if (_PayToBankBranch != value)
                {
					_PayToBankBranch = value;
					EditPayToBankBranch = true;
				}
			}
		}

        private String _PayToBankCode;
        public readonly bool _PayToBankCode_PKFlag = false;
        public readonly bool _PayToBankCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditPayToBankCode = false;
		public String PayToBankCode
		{
			get
			{
				return _PayToBankCode;
			}
			set
			{
				if (_PayToBankCode != value)
                {
					_PayToBankCode = value;
					EditPayToBankCode = true;
				}
			}
		}

        private String _PayToBankCountry;
        public readonly bool _PayToBankCountry_PKFlag = false;
        public readonly bool _PayToBankCountry_IDTFlag = false;
		[XmlIgnore]
		public bool EditPayToBankCountry = false;
		public String PayToBankCountry
		{
			get
			{
				return _PayToBankCountry;
			}
			set
			{
				if (_PayToBankCountry != value)
                {
					_PayToBankCountry = value;
					EditPayToBankCountry = true;
				}
			}
		}

        private String _PayToCode;
        public readonly bool _PayToCode_PKFlag = false;
        public readonly bool _PayToCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditPayToCode = false;
		public String PayToCode
		{
			get
			{
				return _PayToCode;
			}
			set
			{
				if (_PayToCode != value)
                {
					_PayToCode = value;
					EditPayToCode = true;
				}
			}
		}

        private String _Proforma;
        public readonly bool _Proforma_PKFlag = false;
        public readonly bool _Proforma_IDTFlag = false;
		[XmlIgnore]
		public bool EditProforma = false;
		public String Proforma
		{
			get
			{
				return _Proforma;
			}
			set
			{
				if (_Proforma != value)
                {
					_Proforma = value;
					EditProforma = true;
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

        private String _Remarks;
        public readonly bool _Remarks_PKFlag = false;
        public readonly bool _Remarks_IDTFlag = false;
		[XmlIgnore]
		public bool EditRemarks = false;
		public String Remarks
		{
			get
			{
				return _Remarks;
			}
			set
			{
				if (_Remarks != value)
                {
					_Remarks = value;
					EditRemarks = true;
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

        private String _TransferAccount;
        public readonly bool _TransferAccount_PKFlag = false;
        public readonly bool _TransferAccount_IDTFlag = false;
		[XmlIgnore]
		public bool EditTransferAccount = false;
		public String TransferAccount
		{
			get
			{
				return _TransferAccount;
			}
			set
			{
				if (_TransferAccount != value)
                {
					_TransferAccount = value;
					EditTransferAccount = true;
				}
			}
		}

        private Nullable<DateTime> _TransferDate;
        public readonly bool _TransferDate_PKFlag = false;
        public readonly bool _TransferDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditTransferDate = false;
		public Nullable<DateTime> TransferDate
		{
			get
			{
				return _TransferDate;
			}
			set
			{
				if (_TransferDate != value)
                {
					_TransferDate = value;
					EditTransferDate = true;
				}
			}
		}

        private Nullable<Decimal> _TransferRealAmount;
        public readonly bool _TransferRealAmount_PKFlag = false;
        public readonly bool _TransferRealAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditTransferRealAmount = false;
		public Nullable<Decimal> TransferRealAmount
		{
			get
			{
				return _TransferRealAmount;
			}
			set
			{
				if (_TransferRealAmount != value)
                {
					_TransferRealAmount = value;
					EditTransferRealAmount = true;
				}
			}
		}

        private String _TransferReference;
        public readonly bool _TransferReference_PKFlag = false;
        public readonly bool _TransferReference_IDTFlag = false;
		[XmlIgnore]
		public bool EditTransferReference = false;
		public String TransferReference
		{
			get
			{
				return _TransferReference;
			}
			set
			{
				if (_TransferReference != value)
                {
					_TransferReference = value;
					EditTransferReference = true;
				}
			}
		}

        private Nullable<Decimal> _TransferSum;
        public readonly bool _TransferSum_PKFlag = false;
        public readonly bool _TransferSum_IDTFlag = false;
		[XmlIgnore]
		public bool EditTransferSum = false;
		public Nullable<Decimal> TransferSum
		{
			get
			{
				return _TransferSum;
			}
			set
			{
				if (_TransferSum != value)
                {
					_TransferSum = value;
					EditTransferSum = true;
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

        private Nullable<Decimal> _WTAmount;
        public readonly bool _WTAmount_PKFlag = false;
        public readonly bool _WTAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditWTAmount = false;
		public Nullable<Decimal> WTAmount
		{
			get
			{
				return _WTAmount;
			}
			set
			{
				if (_WTAmount != value)
                {
					_WTAmount = value;
					EditWTAmount = true;
				}
			}
		}

        private Nullable<Decimal> _WtBaseSum;
        public readonly bool _WtBaseSum_PKFlag = false;
        public readonly bool _WtBaseSum_IDTFlag = false;
		[XmlIgnore]
		public bool EditWtBaseSum = false;
		public Nullable<Decimal> WtBaseSum
		{
			get
			{
				return _WtBaseSum;
			}
			set
			{
				if (_WtBaseSum != value)
                {
					_WtBaseSum = value;
					EditWtBaseSum = true;
				}
			}
		}

        private String _WTCode;
        public readonly bool _WTCode_PKFlag = false;
        public readonly bool _WTCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditWTCode = false;
		public String WTCode
		{
			get
			{
				return _WTCode;
			}
			set
			{
				if (_WTCode != value)
                {
					_WTCode = value;
					EditWTCode = true;
				}
			}
		}

	}
	public partial class Payments_Command
	{
		string TableName = "Payments";

		Payments _Payments = null;
		DBHelper _DBHelper = null;

		internal Payments_Command(Payments obj_Payments)
		{
			this._Payments = obj_Payments;
			this._DBHelper = new DBHelper();
		}

		internal Payments_Command(string ConnectionStr, Payments obj_Payments)
		{
			this._Payments = obj_Payments;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _Payments.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Payments.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_Payments, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_Payments, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_PaymentsID_Value)
        {
            Load(_DBHelper, L_PaymentsID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_PaymentsID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_PaymentsID", L_PaymentsID_Value, (DbType)Enum.Parse(typeof(DbType), L_PaymentsID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Payments WHERE L_PaymentsID=@L_PaymentsID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_PaymentsID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_PaymentsID", L_PaymentsID_Value, (DbType)Enum.Parse(typeof(DbType), L_PaymentsID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Payments WHERE L_PaymentsID=@L_PaymentsID", "Payments", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("Payments");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<Payments> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<Payments> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("Payments");
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
            List<Payments> Res = new List<Payments>();
            foreach (DataRow Dr in dt.Rows)
            {
                Payments Item = new Payments();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_Payments);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_Payments);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _Payments.GetType();
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

                foreach (PropertyInfo ProInfo in _Payments.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _Payments.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_Payments, null);
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
            Type MyType = _Payments.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Payments.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Payments, null);
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
            Type MyType = _Payments.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Payments.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Payments.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_Payments) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Payments, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_Payments, null)));
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
            string ssql = "DELETE Payments WHERE L_PaymentsID=@L_PaymentsID";
            Parameter.Add(new DBParameter("@L_PaymentsID", _Payments.L_PaymentsID));

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
