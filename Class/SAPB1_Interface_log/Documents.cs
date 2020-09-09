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
	[XmlRootAttribute("Documents", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class Documents
	{
		[XmlIgnore]
		[NonSerialized]
		public Documents_Command ExecCommand = null;
		public Documents()
		{
			ExecCommand = new Documents_Command(this);
		}
		public Documents(string ConnectionStr)
		{
			ExecCommand = new Documents_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new Documents_Command(ConnectionStr, this);
		}
		public Documents(string ConnectionStr, String L_DocumentsID_Value)
		{
			ExecCommand = new Documents_Command(ConnectionStr, this);
			ExecCommand.Load(L_DocumentsID_Value);
		}
        private String _L_DocumentsID;
        public readonly bool _L_DocumentsID_PKFlag = true;
        public readonly bool _L_DocumentsID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_DocumentsID = false;
		public String L_DocumentsID
		{
			get
			{
				return _L_DocumentsID;
			}
			set
			{
				if (_L_DocumentsID != value)
                {
					_L_DocumentsID = value;
					EditL_DocumentsID = true;
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

        private String _L_ABSEntry;
        public readonly bool _L_ABSEntry_PKFlag = false;
        public readonly bool _L_ABSEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_ABSEntry = false;
		public String L_ABSEntry
		{
			get
			{
				return _L_ABSEntry;
			}
			set
			{
				if (_L_ABSEntry != value)
                {
					_L_ABSEntry = value;
					EditL_ABSEntry = true;
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

        private String _Address2;
        public readonly bool _Address2_PKFlag = false;
        public readonly bool _Address2_IDTFlag = false;
		[XmlIgnore]
		public bool EditAddress2 = false;
		public String Address2
		{
			get
			{
				return _Address2;
			}
			set
			{
				if (_Address2 != value)
                {
					_Address2 = value;
					EditAddress2 = true;
				}
			}
		}

        private String _AgentCode;
        public readonly bool _AgentCode_PKFlag = false;
        public readonly bool _AgentCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditAgentCode = false;
		public String AgentCode
		{
			get
			{
				return _AgentCode;
			}
			set
			{
				if (_AgentCode != value)
                {
					_AgentCode = value;
					EditAgentCode = true;
				}
			}
		}

        private Nullable<Int32> _AnnualInvoiceDeclarationReference;
        public readonly bool _AnnualInvoiceDeclarationReference_PKFlag = false;
        public readonly bool _AnnualInvoiceDeclarationReference_IDTFlag = false;
		[XmlIgnore]
		public bool EditAnnualInvoiceDeclarationReference = false;
		public Nullable<Int32> AnnualInvoiceDeclarationReference
		{
			get
			{
				return _AnnualInvoiceDeclarationReference;
			}
			set
			{
				if (_AnnualInvoiceDeclarationReference != value)
                {
					_AnnualInvoiceDeclarationReference = value;
					EditAnnualInvoiceDeclarationReference = true;
				}
			}
		}

        private String _ApplyCurrentVATRatesForDownPaymentsToDraw;
        public readonly bool _ApplyCurrentVATRatesForDownPaymentsToDraw_PKFlag = false;
        public readonly bool _ApplyCurrentVATRatesForDownPaymentsToDraw_IDTFlag = false;
		[XmlIgnore]
		public bool EditApplyCurrentVATRatesForDownPaymentsToDraw = false;
		public String ApplyCurrentVATRatesForDownPaymentsToDraw
		{
			get
			{
				return _ApplyCurrentVATRatesForDownPaymentsToDraw;
			}
			set
			{
				if (_ApplyCurrentVATRatesForDownPaymentsToDraw != value)
                {
					_ApplyCurrentVATRatesForDownPaymentsToDraw = value;
					EditApplyCurrentVATRatesForDownPaymentsToDraw = true;
				}
			}
		}

        private String _ApplyTaxOnFirstInstallment;
        public readonly bool _ApplyTaxOnFirstInstallment_PKFlag = false;
        public readonly bool _ApplyTaxOnFirstInstallment_IDTFlag = false;
		[XmlIgnore]
		public bool EditApplyTaxOnFirstInstallment = false;
		public String ApplyTaxOnFirstInstallment
		{
			get
			{
				return _ApplyTaxOnFirstInstallment;
			}
			set
			{
				if (_ApplyTaxOnFirstInstallment != value)
                {
					_ApplyTaxOnFirstInstallment = value;
					EditApplyTaxOnFirstInstallment = true;
				}
			}
		}

        private String _ArchiveNonremovableSalesQuotation;
        public readonly bool _ArchiveNonremovableSalesQuotation_PKFlag = false;
        public readonly bool _ArchiveNonremovableSalesQuotation_IDTFlag = false;
		[XmlIgnore]
		public bool EditArchiveNonremovableSalesQuotation = false;
		public String ArchiveNonremovableSalesQuotation
		{
			get
			{
				return _ArchiveNonremovableSalesQuotation;
			}
			set
			{
				if (_ArchiveNonremovableSalesQuotation != value)
                {
					_ArchiveNonremovableSalesQuotation = value;
					EditArchiveNonremovableSalesQuotation = true;
				}
			}
		}

        private Nullable<DateTime> _AssetValueDate;
        public readonly bool _AssetValueDate_PKFlag = false;
        public readonly bool _AssetValueDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditAssetValueDate = false;
		public Nullable<DateTime> AssetValueDate
		{
			get
			{
				return _AssetValueDate;
			}
			set
			{
				if (_AssetValueDate != value)
                {
					_AssetValueDate = value;
					EditAssetValueDate = true;
				}
			}
		}

        private String _ATDocumentType;
        public readonly bool _ATDocumentType_PKFlag = false;
        public readonly bool _ATDocumentType_IDTFlag = false;
		[XmlIgnore]
		public bool EditATDocumentType = false;
		public String ATDocumentType
		{
			get
			{
				return _ATDocumentType;
			}
			set
			{
				if (_ATDocumentType != value)
                {
					_ATDocumentType = value;
					EditATDocumentType = true;
				}
			}
		}

        private Nullable<Int32> _AttachmentEntry;
        public readonly bool _AttachmentEntry_PKFlag = false;
        public readonly bool _AttachmentEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditAttachmentEntry = false;
		public Nullable<Int32> AttachmentEntry
		{
			get
			{
				return _AttachmentEntry;
			}
			set
			{
				if (_AttachmentEntry != value)
                {
					_AttachmentEntry = value;
					EditAttachmentEntry = true;
				}
			}
		}

        private String _AuthorizationCode;
        public readonly bool _AuthorizationCode_PKFlag = false;
        public readonly bool _AuthorizationCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditAuthorizationCode = false;
		public String AuthorizationCode
		{
			get
			{
				return _AuthorizationCode;
			}
			set
			{
				if (_AuthorizationCode != value)
                {
					_AuthorizationCode = value;
					EditAuthorizationCode = true;
				}
			}
		}

        private Nullable<Int32> _BlanketAgreementNumber;
        public readonly bool _BlanketAgreementNumber_PKFlag = false;
        public readonly bool _BlanketAgreementNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditBlanketAgreementNumber = false;
		public Nullable<Int32> BlanketAgreementNumber
		{
			get
			{
				return _BlanketAgreementNumber;
			}
			set
			{
				if (_BlanketAgreementNumber != value)
                {
					_BlanketAgreementNumber = value;
					EditBlanketAgreementNumber = true;
				}
			}
		}

        private String _BlockDunning;
        public readonly bool _BlockDunning_PKFlag = false;
        public readonly bool _BlockDunning_IDTFlag = false;
		[XmlIgnore]
		public bool EditBlockDunning = false;
		public String BlockDunning
		{
			get
			{
				return _BlockDunning;
			}
			set
			{
				if (_BlockDunning != value)
                {
					_BlockDunning = value;
					EditBlockDunning = true;
				}
			}
		}

        private String _Box1099;
        public readonly bool _Box1099_PKFlag = false;
        public readonly bool _Box1099_IDTFlag = false;
		[XmlIgnore]
		public bool EditBox1099 = false;
		public String Box1099
		{
			get
			{
				return _Box1099;
			}
			set
			{
				if (_Box1099 != value)
                {
					_Box1099 = value;
					EditBox1099 = true;
				}
			}
		}

        private String _BPChannelCode;
        public readonly bool _BPChannelCode_PKFlag = false;
        public readonly bool _BPChannelCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditBPChannelCode = false;
		public String BPChannelCode
		{
			get
			{
				return _BPChannelCode;
			}
			set
			{
				if (_BPChannelCode != value)
                {
					_BPChannelCode = value;
					EditBPChannelCode = true;
				}
			}
		}

        private Nullable<Int32> _BPChannelContact;
        public readonly bool _BPChannelContact_PKFlag = false;
        public readonly bool _BPChannelContact_IDTFlag = false;
		[XmlIgnore]
		public bool EditBPChannelContact = false;
		public Nullable<Int32> BPChannelContact
		{
			get
			{
				return _BPChannelContact;
			}
			set
			{
				if (_BPChannelContact != value)
                {
					_BPChannelContact = value;
					EditBPChannelContact = true;
				}
			}
		}

        private Nullable<Int32> _BPL_IDAssignedToInvoice;
        public readonly bool _BPL_IDAssignedToInvoice_PKFlag = false;
        public readonly bool _BPL_IDAssignedToInvoice_IDTFlag = false;
		[XmlIgnore]
		public bool EditBPL_IDAssignedToInvoice = false;
		public Nullable<Int32> BPL_IDAssignedToInvoice
		{
			get
			{
				return _BPL_IDAssignedToInvoice;
			}
			set
			{
				if (_BPL_IDAssignedToInvoice != value)
                {
					_BPL_IDAssignedToInvoice = value;
					EditBPL_IDAssignedToInvoice = true;
				}
			}
		}

        private Nullable<DateTime> _CancelDate;
        public readonly bool _CancelDate_PKFlag = false;
        public readonly bool _CancelDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditCancelDate = false;
		public Nullable<DateTime> CancelDate
		{
			get
			{
				return _CancelDate;
			}
			set
			{
				if (_CancelDate != value)
                {
					_CancelDate = value;
					EditCancelDate = true;
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

        private Nullable<Int32> _CashDiscountDateOffset;
        public readonly bool _CashDiscountDateOffset_PKFlag = false;
        public readonly bool _CashDiscountDateOffset_IDTFlag = false;
		[XmlIgnore]
		public bool EditCashDiscountDateOffset = false;
		public Nullable<Int32> CashDiscountDateOffset
		{
			get
			{
				return _CashDiscountDateOffset;
			}
			set
			{
				if (_CashDiscountDateOffset != value)
                {
					_CashDiscountDateOffset = value;
					EditCashDiscountDateOffset = true;
				}
			}
		}

        private String _CentralBankIndicator;
        public readonly bool _CentralBankIndicator_PKFlag = false;
        public readonly bool _CentralBankIndicator_IDTFlag = false;
		[XmlIgnore]
		public bool EditCentralBankIndicator = false;
		public String CentralBankIndicator
		{
			get
			{
				return _CentralBankIndicator;
			}
			set
			{
				if (_CentralBankIndicator != value)
                {
					_CentralBankIndicator = value;
					EditCentralBankIndicator = true;
				}
			}
		}

        private Nullable<DateTime> _ClosingDate;
        public readonly bool _ClosingDate_PKFlag = false;
        public readonly bool _ClosingDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditClosingDate = false;
		public Nullable<DateTime> ClosingDate
		{
			get
			{
				return _ClosingDate;
			}
			set
			{
				if (_ClosingDate != value)
                {
					_ClosingDate = value;
					EditClosingDate = true;
				}
			}
		}

        private String _ClosingOption;
        public readonly bool _ClosingOption_PKFlag = false;
        public readonly bool _ClosingOption_IDTFlag = false;
		[XmlIgnore]
		public bool EditClosingOption = false;
		public String ClosingOption
		{
			get
			{
				return _ClosingOption;
			}
			set
			{
				if (_ClosingOption != value)
                {
					_ClosingOption = value;
					EditClosingOption = true;
				}
			}
		}

        private String _ClosingRemarks;
        public readonly bool _ClosingRemarks_PKFlag = false;
        public readonly bool _ClosingRemarks_IDTFlag = false;
		[XmlIgnore]
		public bool EditClosingRemarks = false;
		public String ClosingRemarks
		{
			get
			{
				return _ClosingRemarks;
			}
			set
			{
				if (_ClosingRemarks != value)
                {
					_ClosingRemarks = value;
					EditClosingRemarks = true;
				}
			}
		}

        private String _Comments;
        public readonly bool _Comments_PKFlag = false;
        public readonly bool _Comments_IDTFlag = false;
		[XmlIgnore]
		public bool EditComments = false;
		public String Comments
		{
			get
			{
				return _Comments;
			}
			set
			{
				if (_Comments != value)
                {
					_Comments = value;
					EditComments = true;
				}
			}
		}

        private String _CommissionTrade;
        public readonly bool _CommissionTrade_PKFlag = false;
        public readonly bool _CommissionTrade_IDTFlag = false;
		[XmlIgnore]
		public bool EditCommissionTrade = false;
		public String CommissionTrade
		{
			get
			{
				return _CommissionTrade;
			}
			set
			{
				if (_CommissionTrade != value)
                {
					_CommissionTrade = value;
					EditCommissionTrade = true;
				}
			}
		}

        private String _CommissionTradeReturn;
        public readonly bool _CommissionTradeReturn_PKFlag = false;
        public readonly bool _CommissionTradeReturn_IDTFlag = false;
		[XmlIgnore]
		public bool EditCommissionTradeReturn = false;
		public String CommissionTradeReturn
		{
			get
			{
				return _CommissionTradeReturn;
			}
			set
			{
				if (_CommissionTradeReturn != value)
                {
					_CommissionTradeReturn = value;
					EditCommissionTradeReturn = true;
				}
			}
		}

        private String _Confirmed;
        public readonly bool _Confirmed_PKFlag = false;
        public readonly bool _Confirmed_IDTFlag = false;
		[XmlIgnore]
		public bool EditConfirmed = false;
		public String Confirmed
		{
			get
			{
				return _Confirmed;
			}
			set
			{
				if (_Confirmed != value)
                {
					_Confirmed = value;
					EditConfirmed = true;
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

        private String _CreateOnlineQuotation;
        public readonly bool _CreateOnlineQuotation_PKFlag = false;
        public readonly bool _CreateOnlineQuotation_IDTFlag = false;
		[XmlIgnore]
		public bool EditCreateOnlineQuotation = false;
		public String CreateOnlineQuotation
		{
			get
			{
				return _CreateOnlineQuotation;
			}
			set
			{
				if (_CreateOnlineQuotation != value)
                {
					_CreateOnlineQuotation = value;
					EditCreateOnlineQuotation = true;
				}
			}
		}

        private Nullable<DateTime> _DateOfReportingControlStatementVAT;
        public readonly bool _DateOfReportingControlStatementVAT_PKFlag = false;
        public readonly bool _DateOfReportingControlStatementVAT_IDTFlag = false;
		[XmlIgnore]
		public bool EditDateOfReportingControlStatementVAT = false;
		public Nullable<DateTime> DateOfReportingControlStatementVAT
		{
			get
			{
				return _DateOfReportingControlStatementVAT;
			}
			set
			{
				if (_DateOfReportingControlStatementVAT != value)
                {
					_DateOfReportingControlStatementVAT = value;
					EditDateOfReportingControlStatementVAT = true;
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

        private Nullable<DateTime> _DocDueDate;
        public readonly bool _DocDueDate_PKFlag = false;
        public readonly bool _DocDueDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocDueDate = false;
		public Nullable<DateTime> DocDueDate
		{
			get
			{
				return _DocDueDate;
			}
			set
			{
				if (_DocDueDate != value)
                {
					_DocDueDate = value;
					EditDocDueDate = true;
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

        private String _DocObjectCodeEx;
        public readonly bool _DocObjectCodeEx_PKFlag = false;
        public readonly bool _DocObjectCodeEx_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocObjectCodeEx = false;
		public String DocObjectCodeEx
		{
			get
			{
				return _DocObjectCodeEx;
			}
			set
			{
				if (_DocObjectCodeEx != value)
                {
					_DocObjectCodeEx = value;
					EditDocObjectCodeEx = true;
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

        private Nullable<DateTime> _DocTime;
        public readonly bool _DocTime_PKFlag = false;
        public readonly bool _DocTime_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocTime = false;
		public Nullable<DateTime> DocTime
		{
			get
			{
				return _DocTime;
			}
			set
			{
				if (_DocTime != value)
                {
					_DocTime = value;
					EditDocTime = true;
				}
			}
		}

        private Nullable<Decimal> _DocTotal;
        public readonly bool _DocTotal_PKFlag = false;
        public readonly bool _DocTotal_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocTotal = false;
		public Nullable<Decimal> DocTotal
		{
			get
			{
				return _DocTotal;
			}
			set
			{
				if (_DocTotal != value)
                {
					_DocTotal = value;
					EditDocTotal = true;
				}
			}
		}

        private Nullable<Decimal> _DocTotalFc;
        public readonly bool _DocTotalFc_PKFlag = false;
        public readonly bool _DocTotalFc_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocTotalFc = false;
		public Nullable<Decimal> DocTotalFc
		{
			get
			{
				return _DocTotalFc;
			}
			set
			{
				if (_DocTotalFc != value)
                {
					_DocTotalFc = value;
					EditDocTotalFc = true;
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

        private String _DocumentDelivery;
        public readonly bool _DocumentDelivery_PKFlag = false;
        public readonly bool _DocumentDelivery_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocumentDelivery = false;
		public String DocumentDelivery
		{
			get
			{
				return _DocumentDelivery;
			}
			set
			{
				if (_DocumentDelivery != value)
                {
					_DocumentDelivery = value;
					EditDocumentDelivery = true;
				}
			}
		}

        private Nullable<Int32> _DocumentsOwner;
        public readonly bool _DocumentsOwner_PKFlag = false;
        public readonly bool _DocumentsOwner_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocumentsOwner = false;
		public Nullable<Int32> DocumentsOwner
		{
			get
			{
				return _DocumentsOwner;
			}
			set
			{
				if (_DocumentsOwner != value)
                {
					_DocumentsOwner = value;
					EditDocumentsOwner = true;
				}
			}
		}

        private String _DocumentSubType;
        public readonly bool _DocumentSubType_PKFlag = false;
        public readonly bool _DocumentSubType_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocumentSubType = false;
		public String DocumentSubType
		{
			get
			{
				return _DocumentSubType;
			}
			set
			{
				if (_DocumentSubType != value)
                {
					_DocumentSubType = value;
					EditDocumentSubType = true;
				}
			}
		}

        private String _DocumentTaxID;
        public readonly bool _DocumentTaxID_PKFlag = false;
        public readonly bool _DocumentTaxID_IDTFlag = false;
		[XmlIgnore]
		public bool EditDocumentTaxID = false;
		public String DocumentTaxID
		{
			get
			{
				return _DocumentTaxID;
			}
			set
			{
				if (_DocumentTaxID != value)
                {
					_DocumentTaxID = value;
					EditDocumentTaxID = true;
				}
			}
		}

        private Nullable<Decimal> _DownPayment;
        public readonly bool _DownPayment_PKFlag = false;
        public readonly bool _DownPayment_IDTFlag = false;
		[XmlIgnore]
		public bool EditDownPayment = false;
		public Nullable<Decimal> DownPayment
		{
			get
			{
				return _DownPayment;
			}
			set
			{
				if (_DownPayment != value)
                {
					_DownPayment = value;
					EditDownPayment = true;
				}
			}
		}

        private Nullable<Decimal> _DownPaymentAmount;
        public readonly bool _DownPaymentAmount_PKFlag = false;
        public readonly bool _DownPaymentAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditDownPaymentAmount = false;
		public Nullable<Decimal> DownPaymentAmount
		{
			get
			{
				return _DownPaymentAmount;
			}
			set
			{
				if (_DownPaymentAmount != value)
                {
					_DownPaymentAmount = value;
					EditDownPaymentAmount = true;
				}
			}
		}

        private Nullable<Decimal> _DownPaymentAmountFC;
        public readonly bool _DownPaymentAmountFC_PKFlag = false;
        public readonly bool _DownPaymentAmountFC_IDTFlag = false;
		[XmlIgnore]
		public bool EditDownPaymentAmountFC = false;
		public Nullable<Decimal> DownPaymentAmountFC
		{
			get
			{
				return _DownPaymentAmountFC;
			}
			set
			{
				if (_DownPaymentAmountFC != value)
                {
					_DownPaymentAmountFC = value;
					EditDownPaymentAmountFC = true;
				}
			}
		}

        private Nullable<Decimal> _DownPaymentAmountSC;
        public readonly bool _DownPaymentAmountSC_PKFlag = false;
        public readonly bool _DownPaymentAmountSC_IDTFlag = false;
		[XmlIgnore]
		public bool EditDownPaymentAmountSC = false;
		public Nullable<Decimal> DownPaymentAmountSC
		{
			get
			{
				return _DownPaymentAmountSC;
			}
			set
			{
				if (_DownPaymentAmountSC != value)
                {
					_DownPaymentAmountSC = value;
					EditDownPaymentAmountSC = true;
				}
			}
		}

        private Nullable<Decimal> _DownPaymentPercentage;
        public readonly bool _DownPaymentPercentage_PKFlag = false;
        public readonly bool _DownPaymentPercentage_IDTFlag = false;
		[XmlIgnore]
		public bool EditDownPaymentPercentage = false;
		public Nullable<Decimal> DownPaymentPercentage
		{
			get
			{
				return _DownPaymentPercentage;
			}
			set
			{
				if (_DownPaymentPercentage != value)
                {
					_DownPaymentPercentage = value;
					EditDownPaymentPercentage = true;
				}
			}
		}

        private String _DownPaymentStatus;
        public readonly bool _DownPaymentStatus_PKFlag = false;
        public readonly bool _DownPaymentStatus_IDTFlag = false;
		[XmlIgnore]
		public bool EditDownPaymentStatus = false;
		public String DownPaymentStatus
		{
			get
			{
				return _DownPaymentStatus;
			}
			set
			{
				if (_DownPaymentStatus != value)
                {
					_DownPaymentStatus = value;
					EditDownPaymentStatus = true;
				}
			}
		}

        private String _DownPaymentTrasactionID;
        public readonly bool _DownPaymentTrasactionID_PKFlag = false;
        public readonly bool _DownPaymentTrasactionID_IDTFlag = false;
		[XmlIgnore]
		public bool EditDownPaymentTrasactionID = false;
		public String DownPaymentTrasactionID
		{
			get
			{
				return _DownPaymentTrasactionID;
			}
			set
			{
				if (_DownPaymentTrasactionID != value)
                {
					_DownPaymentTrasactionID = value;
					EditDownPaymentTrasactionID = true;
				}
			}
		}

        private String _DownPaymentType;
        public readonly bool _DownPaymentType_PKFlag = false;
        public readonly bool _DownPaymentType_IDTFlag = false;
		[XmlIgnore]
		public bool EditDownPaymentType = false;
		public String DownPaymentType
		{
			get
			{
				return _DownPaymentType;
			}
			set
			{
				if (_DownPaymentType != value)
                {
					_DownPaymentType = value;
					EditDownPaymentType = true;
				}
			}
		}

        private String _ECommerceGSTIN;
        public readonly bool _ECommerceGSTIN_PKFlag = false;
        public readonly bool _ECommerceGSTIN_IDTFlag = false;
		[XmlIgnore]
		public bool EditECommerceGSTIN = false;
		public String ECommerceGSTIN
		{
			get
			{
				return _ECommerceGSTIN;
			}
			set
			{
				if (_ECommerceGSTIN != value)
                {
					_ECommerceGSTIN = value;
					EditECommerceGSTIN = true;
				}
			}
		}

        private String _ECommerceOperator;
        public readonly bool _ECommerceOperator_PKFlag = false;
        public readonly bool _ECommerceOperator_IDTFlag = false;
		[XmlIgnore]
		public bool EditECommerceOperator = false;
		public String ECommerceOperator
		{
			get
			{
				return _ECommerceOperator;
			}
			set
			{
				if (_ECommerceOperator != value)
                {
					_ECommerceOperator = value;
					EditECommerceOperator = true;
				}
			}
		}

        private String _EDocErrorCode;
        public readonly bool _EDocErrorCode_PKFlag = false;
        public readonly bool _EDocErrorCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditEDocErrorCode = false;
		public String EDocErrorCode
		{
			get
			{
				return _EDocErrorCode;
			}
			set
			{
				if (_EDocErrorCode != value)
                {
					_EDocErrorCode = value;
					EditEDocErrorCode = true;
				}
			}
		}

        private String _EDocErrorMessage;
        public readonly bool _EDocErrorMessage_PKFlag = false;
        public readonly bool _EDocErrorMessage_IDTFlag = false;
		[XmlIgnore]
		public bool EditEDocErrorMessage = false;
		public String EDocErrorMessage
		{
			get
			{
				return _EDocErrorMessage;
			}
			set
			{
				if (_EDocErrorMessage != value)
                {
					_EDocErrorMessage = value;
					EditEDocErrorMessage = true;
				}
			}
		}

        private Nullable<Int32> _EDocExportFormat;
        public readonly bool _EDocExportFormat_PKFlag = false;
        public readonly bool _EDocExportFormat_IDTFlag = false;
		[XmlIgnore]
		public bool EditEDocExportFormat = false;
		public Nullable<Int32> EDocExportFormat
		{
			get
			{
				return _EDocExportFormat;
			}
			set
			{
				if (_EDocExportFormat != value)
                {
					_EDocExportFormat = value;
					EditEDocExportFormat = true;
				}
			}
		}

        private String _EDocGenerationType;
        public readonly bool _EDocGenerationType_PKFlag = false;
        public readonly bool _EDocGenerationType_IDTFlag = false;
		[XmlIgnore]
		public bool EditEDocGenerationType = false;
		public String EDocGenerationType
		{
			get
			{
				return _EDocGenerationType;
			}
			set
			{
				if (_EDocGenerationType != value)
                {
					_EDocGenerationType = value;
					EditEDocGenerationType = true;
				}
			}
		}

        private String _EDocNum;
        public readonly bool _EDocNum_PKFlag = false;
        public readonly bool _EDocNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditEDocNum = false;
		public String EDocNum
		{
			get
			{
				return _EDocNum;
			}
			set
			{
				if (_EDocNum != value)
                {
					_EDocNum = value;
					EditEDocNum = true;
				}
			}
		}

        private Nullable<Int32> _EDocSeries;
        public readonly bool _EDocSeries_PKFlag = false;
        public readonly bool _EDocSeries_IDTFlag = false;
		[XmlIgnore]
		public bool EditEDocSeries = false;
		public Nullable<Int32> EDocSeries
		{
			get
			{
				return _EDocSeries;
			}
			set
			{
				if (_EDocSeries != value)
                {
					_EDocSeries = value;
					EditEDocSeries = true;
				}
			}
		}

        private String _EDocStatus;
        public readonly bool _EDocStatus_PKFlag = false;
        public readonly bool _EDocStatus_IDTFlag = false;
		[XmlIgnore]
		public bool EditEDocStatus = false;
		public String EDocStatus
		{
			get
			{
				return _EDocStatus;
			}
			set
			{
				if (_EDocStatus != value)
                {
					_EDocStatus = value;
					EditEDocStatus = true;
				}
			}
		}

        private String _ElecCommStatus;
        public readonly bool _ElecCommStatus_PKFlag = false;
        public readonly bool _ElecCommStatus_IDTFlag = false;
		[XmlIgnore]
		public bool EditElecCommStatus = false;
		public String ElecCommStatus
		{
			get
			{
				return _ElecCommStatus;
			}
			set
			{
				if (_ElecCommStatus != value)
                {
					_ElecCommStatus = value;
					EditElecCommStatus = true;
				}
			}
		}

        private Nullable<DateTime> _EndDeliveryDate;
        public readonly bool _EndDeliveryDate_PKFlag = false;
        public readonly bool _EndDeliveryDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditEndDeliveryDate = false;
		public Nullable<DateTime> EndDeliveryDate
		{
			get
			{
				return _EndDeliveryDate;
			}
			set
			{
				if (_EndDeliveryDate != value)
                {
					_EndDeliveryDate = value;
					EditEndDeliveryDate = true;
				}
			}
		}

        private Nullable<DateTime> _EndDeliveryTime;
        public readonly bool _EndDeliveryTime_PKFlag = false;
        public readonly bool _EndDeliveryTime_IDTFlag = false;
		[XmlIgnore]
		public bool EditEndDeliveryTime = false;
		public Nullable<DateTime> EndDeliveryTime
		{
			get
			{
				return _EndDeliveryTime;
			}
			set
			{
				if (_EndDeliveryTime != value)
                {
					_EndDeliveryTime = value;
					EditEndDeliveryTime = true;
				}
			}
		}

        private String _ETaxNumber;
        public readonly bool _ETaxNumber_PKFlag = false;
        public readonly bool _ETaxNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditETaxNumber = false;
		public String ETaxNumber
		{
			get
			{
				return _ETaxNumber;
			}
			set
			{
				if (_ETaxNumber != value)
                {
					_ETaxNumber = value;
					EditETaxNumber = true;
				}
			}
		}

        private Nullable<Int32> _ETaxWebSite;
        public readonly bool _ETaxWebSite_PKFlag = false;
        public readonly bool _ETaxWebSite_IDTFlag = false;
		[XmlIgnore]
		public bool EditETaxWebSite = false;
		public Nullable<Int32> ETaxWebSite
		{
			get
			{
				return _ETaxWebSite;
			}
			set
			{
				if (_ETaxWebSite != value)
                {
					_ETaxWebSite = value;
					EditETaxWebSite = true;
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

        private Nullable<DateTime> _ExemptionValidityDateFrom;
        public readonly bool _ExemptionValidityDateFrom_PKFlag = false;
        public readonly bool _ExemptionValidityDateFrom_IDTFlag = false;
		[XmlIgnore]
		public bool EditExemptionValidityDateFrom = false;
		public Nullable<DateTime> ExemptionValidityDateFrom
		{
			get
			{
				return _ExemptionValidityDateFrom;
			}
			set
			{
				if (_ExemptionValidityDateFrom != value)
                {
					_ExemptionValidityDateFrom = value;
					EditExemptionValidityDateFrom = true;
				}
			}
		}

        private Nullable<DateTime> _ExemptionValidityDateTo;
        public readonly bool _ExemptionValidityDateTo_PKFlag = false;
        public readonly bool _ExemptionValidityDateTo_IDTFlag = false;
		[XmlIgnore]
		public bool EditExemptionValidityDateTo = false;
		public Nullable<DateTime> ExemptionValidityDateTo
		{
			get
			{
				return _ExemptionValidityDateTo;
			}
			set
			{
				if (_ExemptionValidityDateTo != value)
                {
					_ExemptionValidityDateTo = value;
					EditExemptionValidityDateTo = true;
				}
			}
		}

        private String _ExternalCorrectedDocNum;
        public readonly bool _ExternalCorrectedDocNum_PKFlag = false;
        public readonly bool _ExternalCorrectedDocNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditExternalCorrectedDocNum = false;
		public String ExternalCorrectedDocNum
		{
			get
			{
				return _ExternalCorrectedDocNum;
			}
			set
			{
				if (_ExternalCorrectedDocNum != value)
                {
					_ExternalCorrectedDocNum = value;
					EditExternalCorrectedDocNum = true;
				}
			}
		}

        private Nullable<Int32> _ExtraDays;
        public readonly bool _ExtraDays_PKFlag = false;
        public readonly bool _ExtraDays_IDTFlag = false;
		[XmlIgnore]
		public bool EditExtraDays = false;
		public Nullable<Int32> ExtraDays
		{
			get
			{
				return _ExtraDays;
			}
			set
			{
				if (_ExtraDays != value)
                {
					_ExtraDays = value;
					EditExtraDays = true;
				}
			}
		}

        private Nullable<Int32> _ExtraMonth;
        public readonly bool _ExtraMonth_PKFlag = false;
        public readonly bool _ExtraMonth_IDTFlag = false;
		[XmlIgnore]
		public bool EditExtraMonth = false;
		public Nullable<Int32> ExtraMonth
		{
			get
			{
				return _ExtraMonth;
			}
			set
			{
				if (_ExtraMonth != value)
                {
					_ExtraMonth = value;
					EditExtraMonth = true;
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

        private String _FiscalDocNum;
        public readonly bool _FiscalDocNum_PKFlag = false;
        public readonly bool _FiscalDocNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditFiscalDocNum = false;
		public String FiscalDocNum
		{
			get
			{
				return _FiscalDocNum;
			}
			set
			{
				if (_FiscalDocNum != value)
                {
					_FiscalDocNum = value;
					EditFiscalDocNum = true;
				}
			}
		}

        private Nullable<Int32> _FolioNumber;
        public readonly bool _FolioNumber_PKFlag = false;
        public readonly bool _FolioNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditFolioNumber = false;
		public Nullable<Int32> FolioNumber
		{
			get
			{
				return _FolioNumber;
			}
			set
			{
				if (_FolioNumber != value)
                {
					_FolioNumber = value;
					EditFolioNumber = true;
				}
			}
		}

        private Nullable<Int32> _FolioNumberFrom;
        public readonly bool _FolioNumberFrom_PKFlag = false;
        public readonly bool _FolioNumberFrom_IDTFlag = false;
		[XmlIgnore]
		public bool EditFolioNumberFrom = false;
		public Nullable<Int32> FolioNumberFrom
		{
			get
			{
				return _FolioNumberFrom;
			}
			set
			{
				if (_FolioNumberFrom != value)
                {
					_FolioNumberFrom = value;
					EditFolioNumberFrom = true;
				}
			}
		}

        private Nullable<Int32> _FolioNumberTo;
        public readonly bool _FolioNumberTo_PKFlag = false;
        public readonly bool _FolioNumberTo_IDTFlag = false;
		[XmlIgnore]
		public bool EditFolioNumberTo = false;
		public Nullable<Int32> FolioNumberTo
		{
			get
			{
				return _FolioNumberTo;
			}
			set
			{
				if (_FolioNumberTo != value)
                {
					_FolioNumberTo = value;
					EditFolioNumberTo = true;
				}
			}
		}

        private String _FolioPrefixString;
        public readonly bool _FolioPrefixString_PKFlag = false;
        public readonly bool _FolioPrefixString_IDTFlag = false;
		[XmlIgnore]
		public bool EditFolioPrefixString = false;
		public String FolioPrefixString
		{
			get
			{
				return _FolioPrefixString;
			}
			set
			{
				if (_FolioPrefixString != value)
                {
					_FolioPrefixString = value;
					EditFolioPrefixString = true;
				}
			}
		}

        private Nullable<Int32> _Form1099;
        public readonly bool _Form1099_PKFlag = false;
        public readonly bool _Form1099_IDTFlag = false;
		[XmlIgnore]
		public bool EditForm1099 = false;
		public Nullable<Int32> Form1099
		{
			get
			{
				return _Form1099;
			}
			set
			{
				if (_Form1099 != value)
                {
					_Form1099 = value;
					EditForm1099 = true;
				}
			}
		}

        private String _GroupHandWritten;
        public readonly bool _GroupHandWritten_PKFlag = false;
        public readonly bool _GroupHandWritten_IDTFlag = false;
		[XmlIgnore]
		public bool EditGroupHandWritten = false;
		public String GroupHandWritten
		{
			get
			{
				return _GroupHandWritten;
			}
			set
			{
				if (_GroupHandWritten != value)
                {
					_GroupHandWritten = value;
					EditGroupHandWritten = true;
				}
			}
		}

        private Nullable<Int32> _GroupNumber;
        public readonly bool _GroupNumber_PKFlag = false;
        public readonly bool _GroupNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditGroupNumber = false;
		public Nullable<Int32> GroupNumber
		{
			get
			{
				return _GroupNumber;
			}
			set
			{
				if (_GroupNumber != value)
                {
					_GroupNumber = value;
					EditGroupNumber = true;
				}
			}
		}

        private Nullable<Int32> _GroupSeries;
        public readonly bool _GroupSeries_PKFlag = false;
        public readonly bool _GroupSeries_IDTFlag = false;
		[XmlIgnore]
		public bool EditGroupSeries = false;
		public Nullable<Int32> GroupSeries
		{
			get
			{
				return _GroupSeries;
			}
			set
			{
				if (_GroupSeries != value)
                {
					_GroupSeries = value;
					EditGroupSeries = true;
				}
			}
		}

        private String _GSTTransactionType;
        public readonly bool _GSTTransactionType_PKFlag = false;
        public readonly bool _GSTTransactionType_IDTFlag = false;
		[XmlIgnore]
		public bool EditGSTTransactionType = false;
		public String GSTTransactionType
		{
			get
			{
				return _GSTTransactionType;
			}
			set
			{
				if (_GSTTransactionType != value)
                {
					_GSTTransactionType = value;
					EditGSTTransactionType = true;
				}
			}
		}

        private Nullable<Int32> _GTSChecker;
        public readonly bool _GTSChecker_PKFlag = false;
        public readonly bool _GTSChecker_IDTFlag = false;
		[XmlIgnore]
		public bool EditGTSChecker = false;
		public Nullable<Int32> GTSChecker
		{
			get
			{
				return _GTSChecker;
			}
			set
			{
				if (_GTSChecker != value)
                {
					_GTSChecker = value;
					EditGTSChecker = true;
				}
			}
		}

        private Nullable<Int32> _GTSPayee;
        public readonly bool _GTSPayee_PKFlag = false;
        public readonly bool _GTSPayee_IDTFlag = false;
		[XmlIgnore]
		public bool EditGTSPayee = false;
		public Nullable<Int32> GTSPayee
		{
			get
			{
				return _GTSPayee;
			}
			set
			{
				if (_GTSPayee != value)
                {
					_GTSPayee = value;
					EditGTSPayee = true;
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

        private Nullable<Int32> _ImportFileNum;
        public readonly bool _ImportFileNum_PKFlag = false;
        public readonly bool _ImportFileNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditImportFileNum = false;
		public Nullable<Int32> ImportFileNum
		{
			get
			{
				return _ImportFileNum;
			}
			set
			{
				if (_ImportFileNum != value)
                {
					_ImportFileNum = value;
					EditImportFileNum = true;
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

        private String _InsuranceOperation347;
        public readonly bool _InsuranceOperation347_PKFlag = false;
        public readonly bool _InsuranceOperation347_IDTFlag = false;
		[XmlIgnore]
		public bool EditInsuranceOperation347 = false;
		public String InsuranceOperation347
		{
			get
			{
				return _InsuranceOperation347;
			}
			set
			{
				if (_InsuranceOperation347 != value)
                {
					_InsuranceOperation347 = value;
					EditInsuranceOperation347 = true;
				}
			}
		}

        private String _InterimType;
        public readonly bool _InterimType_PKFlag = false;
        public readonly bool _InterimType_IDTFlag = false;
		[XmlIgnore]
		public bool EditInterimType = false;
		public String InterimType
		{
			get
			{
				return _InterimType;
			}
			set
			{
				if (_InterimType != value)
                {
					_InterimType = value;
					EditInterimType = true;
				}
			}
		}

        private Nullable<Int32> _InternalCorrectedDocNum;
        public readonly bool _InternalCorrectedDocNum_PKFlag = false;
        public readonly bool _InternalCorrectedDocNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditInternalCorrectedDocNum = false;
		public Nullable<Int32> InternalCorrectedDocNum
		{
			get
			{
				return _InternalCorrectedDocNum;
			}
			set
			{
				if (_InternalCorrectedDocNum != value)
                {
					_InternalCorrectedDocNum = value;
					EditInternalCorrectedDocNum = true;
				}
			}
		}

        private String _IsAlteration;
        public readonly bool _IsAlteration_PKFlag = false;
        public readonly bool _IsAlteration_IDTFlag = false;
		[XmlIgnore]
		public bool EditIsAlteration = false;
		public String IsAlteration
		{
			get
			{
				return _IsAlteration;
			}
			set
			{
				if (_IsAlteration != value)
                {
					_IsAlteration = value;
					EditIsAlteration = true;
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

        private Nullable<Int32> _IssuingReason;
        public readonly bool _IssuingReason_PKFlag = false;
        public readonly bool _IssuingReason_IDTFlag = false;
		[XmlIgnore]
		public bool EditIssuingReason = false;
		public Nullable<Int32> IssuingReason
		{
			get
			{
				return _IssuingReason;
			}
			set
			{
				if (_IssuingReason != value)
                {
					_IssuingReason = value;
					EditIssuingReason = true;
				}
			}
		}

        private String _JournalMemo;
        public readonly bool _JournalMemo_PKFlag = false;
        public readonly bool _JournalMemo_IDTFlag = false;
		[XmlIgnore]
		public bool EditJournalMemo = false;
		public String JournalMemo
		{
			get
			{
				return _JournalMemo;
			}
			set
			{
				if (_JournalMemo != value)
                {
					_JournalMemo = value;
					EditJournalMemo = true;
				}
			}
		}

        private Nullable<Int32> _LanguageCode;
        public readonly bool _LanguageCode_PKFlag = false;
        public readonly bool _LanguageCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditLanguageCode = false;
		public Nullable<Int32> LanguageCode
		{
			get
			{
				return _LanguageCode;
			}
			set
			{
				if (_LanguageCode != value)
                {
					_LanguageCode = value;
					EditLanguageCode = true;
				}
			}
		}

        private String _Letter;
        public readonly bool _Letter_PKFlag = false;
        public readonly bool _Letter_IDTFlag = false;
		[XmlIgnore]
		public bool EditLetter = false;
		public String Letter
		{
			get
			{
				return _Letter;
			}
			set
			{
				if (_Letter != value)
                {
					_Letter = value;
					EditLetter = true;
				}
			}
		}

        private String _ManualNumber;
        public readonly bool _ManualNumber_PKFlag = false;
        public readonly bool _ManualNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditManualNumber = false;
		public String ManualNumber
		{
			get
			{
				return _ManualNumber;
			}
			set
			{
				if (_ManualNumber != value)
                {
					_ManualNumber = value;
					EditManualNumber = true;
				}
			}
		}

        private String _MaximumCashDiscount;
        public readonly bool _MaximumCashDiscount_PKFlag = false;
        public readonly bool _MaximumCashDiscount_IDTFlag = false;
		[XmlIgnore]
		public bool EditMaximumCashDiscount = false;
		public String MaximumCashDiscount
		{
			get
			{
				return _MaximumCashDiscount;
			}
			set
			{
				if (_MaximumCashDiscount != value)
                {
					_MaximumCashDiscount = value;
					EditMaximumCashDiscount = true;
				}
			}
		}

        private String _NTSApproved;
        public readonly bool _NTSApproved_PKFlag = false;
        public readonly bool _NTSApproved_IDTFlag = false;
		[XmlIgnore]
		public bool EditNTSApproved = false;
		public String NTSApproved
		{
			get
			{
				return _NTSApproved;
			}
			set
			{
				if (_NTSApproved != value)
                {
					_NTSApproved = value;
					EditNTSApproved = true;
				}
			}
		}

        private String _NTSApprovedNumber;
        public readonly bool _NTSApprovedNumber_PKFlag = false;
        public readonly bool _NTSApprovedNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditNTSApprovedNumber = false;
		public String NTSApprovedNumber
		{
			get
			{
				return _NTSApprovedNumber;
			}
			set
			{
				if (_NTSApprovedNumber != value)
                {
					_NTSApprovedNumber = value;
					EditNTSApprovedNumber = true;
				}
			}
		}

        private String _NumAtCard;
        public readonly bool _NumAtCard_PKFlag = false;
        public readonly bool _NumAtCard_IDTFlag = false;
		[XmlIgnore]
		public bool EditNumAtCard = false;
		public String NumAtCard
		{
			get
			{
				return _NumAtCard;
			}
			set
			{
				if (_NumAtCard != value)
                {
					_NumAtCard = value;
					EditNumAtCard = true;
				}
			}
		}

        private Nullable<Int32> _NumberOfInstallments;
        public readonly bool _NumberOfInstallments_PKFlag = false;
        public readonly bool _NumberOfInstallments_IDTFlag = false;
		[XmlIgnore]
		public bool EditNumberOfInstallments = false;
		public Nullable<Int32> NumberOfInstallments
		{
			get
			{
				return _NumberOfInstallments;
			}
			set
			{
				if (_NumberOfInstallments != value)
                {
					_NumberOfInstallments = value;
					EditNumberOfInstallments = true;
				}
			}
		}

        private String _OpenForLandedCosts;
        public readonly bool _OpenForLandedCosts_PKFlag = false;
        public readonly bool _OpenForLandedCosts_IDTFlag = false;
		[XmlIgnore]
		public bool EditOpenForLandedCosts = false;
		public String OpenForLandedCosts
		{
			get
			{
				return _OpenForLandedCosts;
			}
			set
			{
				if (_OpenForLandedCosts != value)
                {
					_OpenForLandedCosts = value;
					EditOpenForLandedCosts = true;
				}
			}
		}

        private String _OpeningRemarks;
        public readonly bool _OpeningRemarks_PKFlag = false;
        public readonly bool _OpeningRemarks_IDTFlag = false;
		[XmlIgnore]
		public bool EditOpeningRemarks = false;
		public String OpeningRemarks
		{
			get
			{
				return _OpeningRemarks;
			}
			set
			{
				if (_OpeningRemarks != value)
                {
					_OpeningRemarks = value;
					EditOpeningRemarks = true;
				}
			}
		}

        private Nullable<DateTime> _OriginalCreditOrDebitDate;
        public readonly bool _OriginalCreditOrDebitDate_PKFlag = false;
        public readonly bool _OriginalCreditOrDebitDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditOriginalCreditOrDebitDate = false;
		public Nullable<DateTime> OriginalCreditOrDebitDate
		{
			get
			{
				return _OriginalCreditOrDebitDate;
			}
			set
			{
				if (_OriginalCreditOrDebitDate != value)
                {
					_OriginalCreditOrDebitDate = value;
					EditOriginalCreditOrDebitDate = true;
				}
			}
		}

        private String _OriginalCreditOrDebitNo;
        public readonly bool _OriginalCreditOrDebitNo_PKFlag = false;
        public readonly bool _OriginalCreditOrDebitNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditOriginalCreditOrDebitNo = false;
		public String OriginalCreditOrDebitNo
		{
			get
			{
				return _OriginalCreditOrDebitNo;
			}
			set
			{
				if (_OriginalCreditOrDebitNo != value)
                {
					_OriginalCreditOrDebitNo = value;
					EditOriginalCreditOrDebitNo = true;
				}
			}
		}

        private Nullable<DateTime> _OriginalRefDate;
        public readonly bool _OriginalRefDate_PKFlag = false;
        public readonly bool _OriginalRefDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditOriginalRefDate = false;
		public Nullable<DateTime> OriginalRefDate
		{
			get
			{
				return _OriginalRefDate;
			}
			set
			{
				if (_OriginalRefDate != value)
                {
					_OriginalRefDate = value;
					EditOriginalRefDate = true;
				}
			}
		}

        private String _OriginalRefNo;
        public readonly bool _OriginalRefNo_PKFlag = false;
        public readonly bool _OriginalRefNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditOriginalRefNo = false;
		public String OriginalRefNo
		{
			get
			{
				return _OriginalRefNo;
			}
			set
			{
				if (_OriginalRefNo != value)
                {
					_OriginalRefNo = value;
					EditOriginalRefNo = true;
				}
			}
		}

        private String _PartialSupply;
        public readonly bool _PartialSupply_PKFlag = false;
        public readonly bool _PartialSupply_IDTFlag = false;
		[XmlIgnore]
		public bool EditPartialSupply = false;
		public String PartialSupply
		{
			get
			{
				return _PartialSupply;
			}
			set
			{
				if (_PartialSupply != value)
                {
					_PartialSupply = value;
					EditPartialSupply = true;
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

        private Nullable<Int32> _PaymentBlockEntry;
        public readonly bool _PaymentBlockEntry_PKFlag = false;
        public readonly bool _PaymentBlockEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditPaymentBlockEntry = false;
		public Nullable<Int32> PaymentBlockEntry
		{
			get
			{
				return _PaymentBlockEntry;
			}
			set
			{
				if (_PaymentBlockEntry != value)
                {
					_PaymentBlockEntry = value;
					EditPaymentBlockEntry = true;
				}
			}
		}

        private Nullable<Int32> _PaymentGroupCode;
        public readonly bool _PaymentGroupCode_PKFlag = false;
        public readonly bool _PaymentGroupCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditPaymentGroupCode = false;
		public Nullable<Int32> PaymentGroupCode
		{
			get
			{
				return _PaymentGroupCode;
			}
			set
			{
				if (_PaymentGroupCode != value)
                {
					_PaymentGroupCode = value;
					EditPaymentGroupCode = true;
				}
			}
		}

        private String _PaymentMethod;
        public readonly bool _PaymentMethod_PKFlag = false;
        public readonly bool _PaymentMethod_IDTFlag = false;
		[XmlIgnore]
		public bool EditPaymentMethod = false;
		public String PaymentMethod
		{
			get
			{
				return _PaymentMethod;
			}
			set
			{
				if (_PaymentMethod != value)
                {
					_PaymentMethod = value;
					EditPaymentMethod = true;
				}
			}
		}

        private String _PaymentReference;
        public readonly bool _PaymentReference_PKFlag = false;
        public readonly bool _PaymentReference_IDTFlag = false;
		[XmlIgnore]
		public bool EditPaymentReference = false;
		public String PaymentReference
		{
			get
			{
				return _PaymentReference;
			}
			set
			{
				if (_PaymentReference != value)
                {
					_PaymentReference = value;
					EditPaymentReference = true;
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

        private String _Pick;
        public readonly bool _Pick_PKFlag = false;
        public readonly bool _Pick_IDTFlag = false;
		[XmlIgnore]
		public bool EditPick = false;
		public String Pick
		{
			get
			{
				return _Pick;
			}
			set
			{
				if (_Pick != value)
                {
					_Pick = value;
					EditPick = true;
				}
			}
		}

        private String _PickRemark;
        public readonly bool _PickRemark_PKFlag = false;
        public readonly bool _PickRemark_IDTFlag = false;
		[XmlIgnore]
		public bool EditPickRemark = false;
		public String PickRemark
		{
			get
			{
				return _PickRemark;
			}
			set
			{
				if (_PickRemark != value)
                {
					_PickRemark = value;
					EditPickRemark = true;
				}
			}
		}

        private String _PointOfIssueCode;
        public readonly bool _PointOfIssueCode_PKFlag = false;
        public readonly bool _PointOfIssueCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditPointOfIssueCode = false;
		public String PointOfIssueCode
		{
			get
			{
				return _PointOfIssueCode;
			}
			set
			{
				if (_PointOfIssueCode != value)
                {
					_PointOfIssueCode = value;
					EditPointOfIssueCode = true;
				}
			}
		}

        private Nullable<Int32> _POSCashierNumber;
        public readonly bool _POSCashierNumber_PKFlag = false;
        public readonly bool _POSCashierNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditPOSCashierNumber = false;
		public Nullable<Int32> POSCashierNumber
		{
			get
			{
				return _POSCashierNumber;
			}
			set
			{
				if (_POSCashierNumber != value)
                {
					_POSCashierNumber = value;
					EditPOSCashierNumber = true;
				}
			}
		}

        private Nullable<Int32> _POSDailySummaryNo;
        public readonly bool _POSDailySummaryNo_PKFlag = false;
        public readonly bool _POSDailySummaryNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditPOSDailySummaryNo = false;
		public Nullable<Int32> POSDailySummaryNo
		{
			get
			{
				return _POSDailySummaryNo;
			}
			set
			{
				if (_POSDailySummaryNo != value)
                {
					_POSDailySummaryNo = value;
					EditPOSDailySummaryNo = true;
				}
			}
		}

        private String _POSEquipmentNumber;
        public readonly bool _POSEquipmentNumber_PKFlag = false;
        public readonly bool _POSEquipmentNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditPOSEquipmentNumber = false;
		public String POSEquipmentNumber
		{
			get
			{
				return _POSEquipmentNumber;
			}
			set
			{
				if (_POSEquipmentNumber != value)
                {
					_POSEquipmentNumber = value;
					EditPOSEquipmentNumber = true;
				}
			}
		}

        private String _POSManufacturerSerialNumber;
        public readonly bool _POSManufacturerSerialNumber_PKFlag = false;
        public readonly bool _POSManufacturerSerialNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditPOSManufacturerSerialNumber = false;
		public String POSManufacturerSerialNumber
		{
			get
			{
				return _POSManufacturerSerialNumber;
			}
			set
			{
				if (_POSManufacturerSerialNumber != value)
                {
					_POSManufacturerSerialNumber = value;
					EditPOSManufacturerSerialNumber = true;
				}
			}
		}

        private Nullable<Int32> _POSReceiptNo;
        public readonly bool _POSReceiptNo_PKFlag = false;
        public readonly bool _POSReceiptNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditPOSReceiptNo = false;
		public Nullable<Int32> POSReceiptNo
		{
			get
			{
				return _POSReceiptNo;
			}
			set
			{
				if (_POSReceiptNo != value)
                {
					_POSReceiptNo = value;
					EditPOSReceiptNo = true;
				}
			}
		}

        private Nullable<Int32> _POS_CashRegister;
        public readonly bool _POS_CashRegister_PKFlag = false;
        public readonly bool _POS_CashRegister_IDTFlag = false;
		[XmlIgnore]
		public bool EditPOS_CashRegister = false;
		public Nullable<Int32> POS_CashRegister
		{
			get
			{
				return _POS_CashRegister;
			}
			set
			{
				if (_POS_CashRegister != value)
                {
					_POS_CashRegister = value;
					EditPOS_CashRegister = true;
				}
			}
		}

        private String _PriceMode;
        public readonly bool _PriceMode_PKFlag = false;
        public readonly bool _PriceMode_IDTFlag = false;
		[XmlIgnore]
		public bool EditPriceMode = false;
		public String PriceMode
		{
			get
			{
				return _PriceMode;
			}
			set
			{
				if (_PriceMode != value)
                {
					_PriceMode = value;
					EditPriceMode = true;
				}
			}
		}

        private String _Printed;
        public readonly bool _Printed_PKFlag = false;
        public readonly bool _Printed_IDTFlag = false;
		[XmlIgnore]
		public bool EditPrinted = false;
		public String Printed
		{
			get
			{
				return _Printed;
			}
			set
			{
				if (_Printed != value)
                {
					_Printed = value;
					EditPrinted = true;
				}
			}
		}

        private String _PrintSEPADirect;
        public readonly bool _PrintSEPADirect_PKFlag = false;
        public readonly bool _PrintSEPADirect_IDTFlag = false;
		[XmlIgnore]
		public bool EditPrintSEPADirect = false;
		public String PrintSEPADirect
		{
			get
			{
				return _PrintSEPADirect;
			}
			set
			{
				if (_PrintSEPADirect != value)
                {
					_PrintSEPADirect = value;
					EditPrintSEPADirect = true;
				}
			}
		}

        private String _Project;
        public readonly bool _Project_PKFlag = false;
        public readonly bool _Project_IDTFlag = false;
		[XmlIgnore]
		public bool EditProject = false;
		public String Project
		{
			get
			{
				return _Project;
			}
			set
			{
				if (_Project != value)
                {
					_Project = value;
					EditProject = true;
				}
			}
		}

        private Nullable<Int32> _Receiver;
        public readonly bool _Receiver_PKFlag = false;
        public readonly bool _Receiver_IDTFlag = false;
		[XmlIgnore]
		public bool EditReceiver = false;
		public Nullable<Int32> Receiver
		{
			get
			{
				return _Receiver;
			}
			set
			{
				if (_Receiver != value)
                {
					_Receiver = value;
					EditReceiver = true;
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

        private Nullable<Int32> _RelatedEntry;
        public readonly bool _RelatedEntry_PKFlag = false;
        public readonly bool _RelatedEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditRelatedEntry = false;
		public Nullable<Int32> RelatedEntry
		{
			get
			{
				return _RelatedEntry;
			}
			set
			{
				if (_RelatedEntry != value)
                {
					_RelatedEntry = value;
					EditRelatedEntry = true;
				}
			}
		}

        private Nullable<Int32> _RelatedType;
        public readonly bool _RelatedType_PKFlag = false;
        public readonly bool _RelatedType_IDTFlag = false;
		[XmlIgnore]
		public bool EditRelatedType = false;
		public Nullable<Int32> RelatedType
		{
			get
			{
				return _RelatedType;
			}
			set
			{
				if (_RelatedType != value)
                {
					_RelatedType = value;
					EditRelatedType = true;
				}
			}
		}

        private Nullable<Int32> _Releaser;
        public readonly bool _Releaser_PKFlag = false;
        public readonly bool _Releaser_IDTFlag = false;
		[XmlIgnore]
		public bool EditReleaser = false;
		public Nullable<Int32> Releaser
		{
			get
			{
				return _Releaser;
			}
			set
			{
				if (_Releaser != value)
                {
					_Releaser = value;
					EditReleaser = true;
				}
			}
		}

        private String _RelevantToGTS;
        public readonly bool _RelevantToGTS_PKFlag = false;
        public readonly bool _RelevantToGTS_IDTFlag = false;
		[XmlIgnore]
		public bool EditRelevantToGTS = false;
		public String RelevantToGTS
		{
			get
			{
				return _RelevantToGTS;
			}
			set
			{
				if (_RelevantToGTS != value)
                {
					_RelevantToGTS = value;
					EditRelevantToGTS = true;
				}
			}
		}

        private String _ReopenManuallyClosedOrCanceledDocument;
        public readonly bool _ReopenManuallyClosedOrCanceledDocument_PKFlag = false;
        public readonly bool _ReopenManuallyClosedOrCanceledDocument_IDTFlag = false;
		[XmlIgnore]
		public bool EditReopenManuallyClosedOrCanceledDocument = false;
		public String ReopenManuallyClosedOrCanceledDocument
		{
			get
			{
				return _ReopenManuallyClosedOrCanceledDocument;
			}
			set
			{
				if (_ReopenManuallyClosedOrCanceledDocument != value)
                {
					_ReopenManuallyClosedOrCanceledDocument = value;
					EditReopenManuallyClosedOrCanceledDocument = true;
				}
			}
		}

        private String _ReopenOriginalDocument;
        public readonly bool _ReopenOriginalDocument_PKFlag = false;
        public readonly bool _ReopenOriginalDocument_IDTFlag = false;
		[XmlIgnore]
		public bool EditReopenOriginalDocument = false;
		public String ReopenOriginalDocument
		{
			get
			{
				return _ReopenOriginalDocument;
			}
			set
			{
				if (_ReopenOriginalDocument != value)
                {
					_ReopenOriginalDocument = value;
					EditReopenOriginalDocument = true;
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

        private Nullable<Int32> _ReqType;
        public readonly bool _ReqType_PKFlag = false;
        public readonly bool _ReqType_IDTFlag = false;
		[XmlIgnore]
		public bool EditReqType = false;
		public Nullable<Int32> ReqType
		{
			get
			{
				return _ReqType;
			}
			set
			{
				if (_ReqType != value)
                {
					_ReqType = value;
					EditReqType = true;
				}
			}
		}

        private String _Requester;
        public readonly bool _Requester_PKFlag = false;
        public readonly bool _Requester_IDTFlag = false;
		[XmlIgnore]
		public bool EditRequester = false;
		public String Requester
		{
			get
			{
				return _Requester;
			}
			set
			{
				if (_Requester != value)
                {
					_Requester = value;
					EditRequester = true;
				}
			}
		}

        private Nullable<Int32> _RequesterBranch;
        public readonly bool _RequesterBranch_PKFlag = false;
        public readonly bool _RequesterBranch_IDTFlag = false;
		[XmlIgnore]
		public bool EditRequesterBranch = false;
		public Nullable<Int32> RequesterBranch
		{
			get
			{
				return _RequesterBranch;
			}
			set
			{
				if (_RequesterBranch != value)
                {
					_RequesterBranch = value;
					EditRequesterBranch = true;
				}
			}
		}

        private Nullable<Int32> _RequesterDepartment;
        public readonly bool _RequesterDepartment_PKFlag = false;
        public readonly bool _RequesterDepartment_IDTFlag = false;
		[XmlIgnore]
		public bool EditRequesterDepartment = false;
		public Nullable<Int32> RequesterDepartment
		{
			get
			{
				return _RequesterDepartment;
			}
			set
			{
				if (_RequesterDepartment != value)
                {
					_RequesterDepartment = value;
					EditRequesterDepartment = true;
				}
			}
		}

        private String _RequesterEmail;
        public readonly bool _RequesterEmail_PKFlag = false;
        public readonly bool _RequesterEmail_IDTFlag = false;
		[XmlIgnore]
		public bool EditRequesterEmail = false;
		public String RequesterEmail
		{
			get
			{
				return _RequesterEmail;
			}
			set
			{
				if (_RequesterEmail != value)
                {
					_RequesterEmail = value;
					EditRequesterEmail = true;
				}
			}
		}

        private String _RequesterName;
        public readonly bool _RequesterName_PKFlag = false;
        public readonly bool _RequesterName_IDTFlag = false;
		[XmlIgnore]
		public bool EditRequesterName = false;
		public String RequesterName
		{
			get
			{
				return _RequesterName;
			}
			set
			{
				if (_RequesterName != value)
                {
					_RequesterName = value;
					EditRequesterName = true;
				}
			}
		}

        private Nullable<DateTime> _RequriedDate;
        public readonly bool _RequriedDate_PKFlag = false;
        public readonly bool _RequriedDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditRequriedDate = false;
		public Nullable<DateTime> RequriedDate
		{
			get
			{
				return _RequriedDate;
			}
			set
			{
				if (_RequriedDate != value)
                {
					_RequriedDate = value;
					EditRequriedDate = true;
				}
			}
		}

        private String _ReserveInvoice;
        public readonly bool _ReserveInvoice_PKFlag = false;
        public readonly bool _ReserveInvoice_IDTFlag = false;
		[XmlIgnore]
		public bool EditReserveInvoice = false;
		public String ReserveInvoice
		{
			get
			{
				return _ReserveInvoice;
			}
			set
			{
				if (_ReserveInvoice != value)
                {
					_ReserveInvoice = value;
					EditReserveInvoice = true;
				}
			}
		}

        private String _ReuseDocumentNum;
        public readonly bool _ReuseDocumentNum_PKFlag = false;
        public readonly bool _ReuseDocumentNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditReuseDocumentNum = false;
		public String ReuseDocumentNum
		{
			get
			{
				return _ReuseDocumentNum;
			}
			set
			{
				if (_ReuseDocumentNum != value)
                {
					_ReuseDocumentNum = value;
					EditReuseDocumentNum = true;
				}
			}
		}

        private String _ReuseNotaFiscalNum;
        public readonly bool _ReuseNotaFiscalNum_PKFlag = false;
        public readonly bool _ReuseNotaFiscalNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditReuseNotaFiscalNum = false;
		public String ReuseNotaFiscalNum
		{
			get
			{
				return _ReuseNotaFiscalNum;
			}
			set
			{
				if (_ReuseNotaFiscalNum != value)
                {
					_ReuseNotaFiscalNum = value;
					EditReuseNotaFiscalNum = true;
				}
			}
		}

        private String _Revision;
        public readonly bool _Revision_PKFlag = false;
        public readonly bool _Revision_IDTFlag = false;
		[XmlIgnore]
		public bool EditRevision = false;
		public String Revision
		{
			get
			{
				return _Revision;
			}
			set
			{
				if (_Revision != value)
                {
					_Revision = value;
					EditRevision = true;
				}
			}
		}

        private String _RevisionPo;
        public readonly bool _RevisionPo_PKFlag = false;
        public readonly bool _RevisionPo_IDTFlag = false;
		[XmlIgnore]
		public bool EditRevisionPo = false;
		public String RevisionPo
		{
			get
			{
				return _RevisionPo;
			}
			set
			{
				if (_RevisionPo != value)
                {
					_RevisionPo = value;
					EditRevisionPo = true;
				}
			}
		}

        private String _Rounding;
        public readonly bool _Rounding_PKFlag = false;
        public readonly bool _Rounding_IDTFlag = false;
		[XmlIgnore]
		public bool EditRounding = false;
		public String Rounding
		{
			get
			{
				return _Rounding;
			}
			set
			{
				if (_Rounding != value)
                {
					_Rounding = value;
					EditRounding = true;
				}
			}
		}

        private Nullable<Decimal> _RoundingDiffAmount;
        public readonly bool _RoundingDiffAmount_PKFlag = false;
        public readonly bool _RoundingDiffAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditRoundingDiffAmount = false;
		public Nullable<Decimal> RoundingDiffAmount
		{
			get
			{
				return _RoundingDiffAmount;
			}
			set
			{
				if (_RoundingDiffAmount != value)
                {
					_RoundingDiffAmount = value;
					EditRoundingDiffAmount = true;
				}
			}
		}

        private Nullable<Int32> _SalesPersonCode;
        public readonly bool _SalesPersonCode_PKFlag = false;
        public readonly bool _SalesPersonCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditSalesPersonCode = false;
		public Nullable<Int32> SalesPersonCode
		{
			get
			{
				return _SalesPersonCode;
			}
			set
			{
				if (_SalesPersonCode != value)
                {
					_SalesPersonCode = value;
					EditSalesPersonCode = true;
				}
			}
		}

        private String _SendNotification;
        public readonly bool _SendNotification_PKFlag = false;
        public readonly bool _SendNotification_IDTFlag = false;
		[XmlIgnore]
		public bool EditSendNotification = false;
		public String SendNotification
		{
			get
			{
				return _SendNotification;
			}
			set
			{
				if (_SendNotification != value)
                {
					_SendNotification = value;
					EditSendNotification = true;
				}
			}
		}

        private Nullable<Int32> _SequenceCode;
        public readonly bool _SequenceCode_PKFlag = false;
        public readonly bool _SequenceCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditSequenceCode = false;
		public Nullable<Int32> SequenceCode
		{
			get
			{
				return _SequenceCode;
			}
			set
			{
				if (_SequenceCode != value)
                {
					_SequenceCode = value;
					EditSequenceCode = true;
				}
			}
		}

        private String _SequenceModel;
        public readonly bool _SequenceModel_PKFlag = false;
        public readonly bool _SequenceModel_IDTFlag = false;
		[XmlIgnore]
		public bool EditSequenceModel = false;
		public String SequenceModel
		{
			get
			{
				return _SequenceModel;
			}
			set
			{
				if (_SequenceModel != value)
                {
					_SequenceModel = value;
					EditSequenceModel = true;
				}
			}
		}

        private Nullable<Int32> _SequenceSerial;
        public readonly bool _SequenceSerial_PKFlag = false;
        public readonly bool _SequenceSerial_IDTFlag = false;
		[XmlIgnore]
		public bool EditSequenceSerial = false;
		public Nullable<Int32> SequenceSerial
		{
			get
			{
				return _SequenceSerial;
			}
			set
			{
				if (_SequenceSerial != value)
                {
					_SequenceSerial = value;
					EditSequenceSerial = true;
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

        private String _SeriesString;
        public readonly bool _SeriesString_PKFlag = false;
        public readonly bool _SeriesString_IDTFlag = false;
		[XmlIgnore]
		public bool EditSeriesString = false;
		public String SeriesString
		{
			get
			{
				return _SeriesString;
			}
			set
			{
				if (_SeriesString != value)
                {
					_SeriesString = value;
					EditSeriesString = true;
				}
			}
		}

        private Nullable<Decimal> _ServiceGrossProfitPercent;
        public readonly bool _ServiceGrossProfitPercent_PKFlag = false;
        public readonly bool _ServiceGrossProfitPercent_IDTFlag = false;
		[XmlIgnore]
		public bool EditServiceGrossProfitPercent = false;
		public Nullable<Decimal> ServiceGrossProfitPercent
		{
			get
			{
				return _ServiceGrossProfitPercent;
			}
			set
			{
				if (_ServiceGrossProfitPercent != value)
                {
					_ServiceGrossProfitPercent = value;
					EditServiceGrossProfitPercent = true;
				}
			}
		}

        private String _ShipFrom;
        public readonly bool _ShipFrom_PKFlag = false;
        public readonly bool _ShipFrom_IDTFlag = false;
		[XmlIgnore]
		public bool EditShipFrom = false;
		public String ShipFrom
		{
			get
			{
				return _ShipFrom;
			}
			set
			{
				if (_ShipFrom != value)
                {
					_ShipFrom = value;
					EditShipFrom = true;
				}
			}
		}

        private String _ShipToCode;
        public readonly bool _ShipToCode_PKFlag = false;
        public readonly bool _ShipToCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditShipToCode = false;
		public String ShipToCode
		{
			get
			{
				return _ShipToCode;
			}
			set
			{
				if (_ShipToCode != value)
                {
					_ShipToCode = value;
					EditShipToCode = true;
				}
			}
		}

        private String _ShowSCN;
        public readonly bool _ShowSCN_PKFlag = false;
        public readonly bool _ShowSCN_IDTFlag = false;
		[XmlIgnore]
		public bool EditShowSCN = false;
		public String ShowSCN
		{
			get
			{
				return _ShowSCN;
			}
			set
			{
				if (_ShowSCN != value)
                {
					_ShowSCN = value;
					EditShowSCN = true;
				}
			}
		}

        private Nullable<DateTime> _SpecifiedClosingDate;
        public readonly bool _SpecifiedClosingDate_PKFlag = false;
        public readonly bool _SpecifiedClosingDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditSpecifiedClosingDate = false;
		public Nullable<DateTime> SpecifiedClosingDate
		{
			get
			{
				return _SpecifiedClosingDate;
			}
			set
			{
				if (_SpecifiedClosingDate != value)
                {
					_SpecifiedClosingDate = value;
					EditSpecifiedClosingDate = true;
				}
			}
		}

        private Nullable<DateTime> _StartDeliveryDate;
        public readonly bool _StartDeliveryDate_PKFlag = false;
        public readonly bool _StartDeliveryDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditStartDeliveryDate = false;
		public Nullable<DateTime> StartDeliveryDate
		{
			get
			{
				return _StartDeliveryDate;
			}
			set
			{
				if (_StartDeliveryDate != value)
                {
					_StartDeliveryDate = value;
					EditStartDeliveryDate = true;
				}
			}
		}

        private Nullable<DateTime> _StartDeliveryTime;
        public readonly bool _StartDeliveryTime_PKFlag = false;
        public readonly bool _StartDeliveryTime_IDTFlag = false;
		[XmlIgnore]
		public bool EditStartDeliveryTime = false;
		public Nullable<DateTime> StartDeliveryTime
		{
			get
			{
				return _StartDeliveryTime;
			}
			set
			{
				if (_StartDeliveryTime != value)
                {
					_StartDeliveryTime = value;
					EditStartDeliveryTime = true;
				}
			}
		}

        private String _StartFrom;
        public readonly bool _StartFrom_PKFlag = false;
        public readonly bool _StartFrom_IDTFlag = false;
		[XmlIgnore]
		public bool EditStartFrom = false;
		public String StartFrom
		{
			get
			{
				return _StartFrom;
			}
			set
			{
				if (_StartFrom != value)
                {
					_StartFrom = value;
					EditStartFrom = true;
				}
			}
		}

        private String _SubSeriesString;
        public readonly bool _SubSeriesString_PKFlag = false;
        public readonly bool _SubSeriesString_IDTFlag = false;
		[XmlIgnore]
		public bool EditSubSeriesString = false;
		public String SubSeriesString
		{
			get
			{
				return _SubSeriesString;
			}
			set
			{
				if (_SubSeriesString != value)
                {
					_SubSeriesString = value;
					EditSubSeriesString = true;
				}
			}
		}

        private String _SummeryType;
        public readonly bool _SummeryType_PKFlag = false;
        public readonly bool _SummeryType_IDTFlag = false;
		[XmlIgnore]
		public bool EditSummeryType = false;
		public String SummeryType
		{
			get
			{
				return _SummeryType;
			}
			set
			{
				if (_SummeryType != value)
                {
					_SummeryType = value;
					EditSummeryType = true;
				}
			}
		}

        private String _Supplier;
        public readonly bool _Supplier_PKFlag = false;
        public readonly bool _Supplier_IDTFlag = false;
		[XmlIgnore]
		public bool EditSupplier = false;
		public String Supplier
		{
			get
			{
				return _Supplier;
			}
			set
			{
				if (_Supplier != value)
                {
					_Supplier = value;
					EditSupplier = true;
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

        private String _TaxExemptionLetterNum;
        public readonly bool _TaxExemptionLetterNum_PKFlag = false;
        public readonly bool _TaxExemptionLetterNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxExemptionLetterNum = false;
		public String TaxExemptionLetterNum
		{
			get
			{
				return _TaxExemptionLetterNum;
			}
			set
			{
				if (_TaxExemptionLetterNum != value)
                {
					_TaxExemptionLetterNum = value;
					EditTaxExemptionLetterNum = true;
				}
			}
		}

        private Nullable<DateTime> _TaxInvoiceDate;
        public readonly bool _TaxInvoiceDate_PKFlag = false;
        public readonly bool _TaxInvoiceDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxInvoiceDate = false;
		public Nullable<DateTime> TaxInvoiceDate
		{
			get
			{
				return _TaxInvoiceDate;
			}
			set
			{
				if (_TaxInvoiceDate != value)
                {
					_TaxInvoiceDate = value;
					EditTaxInvoiceDate = true;
				}
			}
		}

        private String _TaxInvoiceNo;
        public readonly bool _TaxInvoiceNo_PKFlag = false;
        public readonly bool _TaxInvoiceNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxInvoiceNo = false;
		public String TaxInvoiceNo
		{
			get
			{
				return _TaxInvoiceNo;
			}
			set
			{
				if (_TaxInvoiceNo != value)
                {
					_TaxInvoiceNo = value;
					EditTaxInvoiceNo = true;
				}
			}
		}

        private String _TrackingNumber;
        public readonly bool _TrackingNumber_PKFlag = false;
        public readonly bool _TrackingNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditTrackingNumber = false;
		public String TrackingNumber
		{
			get
			{
				return _TrackingNumber;
			}
			set
			{
				if (_TrackingNumber != value)
                {
					_TrackingNumber = value;
					EditTrackingNumber = true;
				}
			}
		}

        private Nullable<Int32> _TransportationCode;
        public readonly bool _TransportationCode_PKFlag = false;
        public readonly bool _TransportationCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditTransportationCode = false;
		public Nullable<Int32> TransportationCode
		{
			get
			{
				return _TransportationCode;
			}
			set
			{
				if (_TransportationCode != value)
                {
					_TransportationCode = value;
					EditTransportationCode = true;
				}
			}
		}

        private String _UseBillToAddrToDetermineTax;
        public readonly bool _UseBillToAddrToDetermineTax_PKFlag = false;
        public readonly bool _UseBillToAddrToDetermineTax_IDTFlag = false;
		[XmlIgnore]
		public bool EditUseBillToAddrToDetermineTax = false;
		public String UseBillToAddrToDetermineTax
		{
			get
			{
				return _UseBillToAddrToDetermineTax;
			}
			set
			{
				if (_UseBillToAddrToDetermineTax != value)
                {
					_UseBillToAddrToDetermineTax = value;
					EditUseBillToAddrToDetermineTax = true;
				}
			}
		}

        private String _UseCorrectionVATGroup;
        public readonly bool _UseCorrectionVATGroup_PKFlag = false;
        public readonly bool _UseCorrectionVATGroup_IDTFlag = false;
		[XmlIgnore]
		public bool EditUseCorrectionVATGroup = false;
		public String UseCorrectionVATGroup
		{
			get
			{
				return _UseCorrectionVATGroup;
			}
			set
			{
				if (_UseCorrectionVATGroup != value)
                {
					_UseCorrectionVATGroup = value;
					EditUseCorrectionVATGroup = true;
				}
			}
		}

        private String _UseShpdGoodsAct;
        public readonly bool _UseShpdGoodsAct_PKFlag = false;
        public readonly bool _UseShpdGoodsAct_IDTFlag = false;
		[XmlIgnore]
		public bool EditUseShpdGoodsAct = false;
		public String UseShpdGoodsAct
		{
			get
			{
				return _UseShpdGoodsAct;
			}
			set
			{
				if (_UseShpdGoodsAct != value)
                {
					_UseShpdGoodsAct = value;
					EditUseShpdGoodsAct = true;
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

        private Nullable<Decimal> _VatPercent;
        public readonly bool _VatPercent_PKFlag = false;
        public readonly bool _VatPercent_IDTFlag = false;
		[XmlIgnore]
		public bool EditVatPercent = false;
		public Nullable<Decimal> VatPercent
		{
			get
			{
				return _VatPercent;
			}
			set
			{
				if (_VatPercent != value)
                {
					_VatPercent = value;
					EditVatPercent = true;
				}
			}
		}

        private String _VehiclePlate;
        public readonly bool _VehiclePlate_PKFlag = false;
        public readonly bool _VehiclePlate_IDTFlag = false;
		[XmlIgnore]
		public bool EditVehiclePlate = false;
		public String VehiclePlate
		{
			get
			{
				return _VehiclePlate;
			}
			set
			{
				if (_VehiclePlate != value)
                {
					_VehiclePlate = value;
					EditVehiclePlate = true;
				}
			}
		}

        private String _WareHouseUpdateType;
        public readonly bool _WareHouseUpdateType_PKFlag = false;
        public readonly bool _WareHouseUpdateType_IDTFlag = false;
		[XmlIgnore]
		public bool EditWareHouseUpdateType = false;
		public String WareHouseUpdateType
		{
			get
			{
				return _WareHouseUpdateType;
			}
			set
			{
				if (_WareHouseUpdateType != value)
                {
					_WareHouseUpdateType = value;
					EditWareHouseUpdateType = true;
				}
			}
		}

	}
	public partial class Documents_Command
	{
		string TableName = "Documents";

		Documents _Documents = null;
		DBHelper _DBHelper = null;

		internal Documents_Command(Documents obj_Documents)
		{
			this._Documents = obj_Documents;
			this._DBHelper = new DBHelper();
		}

		internal Documents_Command(string ConnectionStr, Documents obj_Documents)
		{
			this._Documents = obj_Documents;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _Documents.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Documents.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_Documents, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_Documents, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_DocumentsID_Value)
        {
            Load(_DBHelper, L_DocumentsID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_DocumentsID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_DocumentsID", L_DocumentsID_Value, (DbType)Enum.Parse(typeof(DbType), L_DocumentsID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Documents WHERE L_DocumentsID=@L_DocumentsID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_DocumentsID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_DocumentsID", L_DocumentsID_Value, (DbType)Enum.Parse(typeof(DbType), L_DocumentsID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Documents WHERE L_DocumentsID=@L_DocumentsID", "Documents", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("Documents");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<Documents> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<Documents> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("Documents");
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
            List<Documents> Res = new List<Documents>();
            foreach (DataRow Dr in dt.Rows)
            {
                Documents Item = new Documents();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Documents.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Documents.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_Documents);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Documents.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Documents.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_Documents);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _Documents.GetType();
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

                foreach (PropertyInfo ProInfo in _Documents.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _Documents.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_Documents))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_Documents, null);
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
            Type MyType = _Documents.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Documents.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Documents.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_Documents) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Documents, null);
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
            Type MyType = _Documents.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Documents.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Documents.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_Documents) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Documents, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_Documents, null)));
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
            string ssql = "DELETE Documents WHERE L_DocumentsID=@L_DocumentsID";
            Parameter.Add(new DBParameter("@L_DocumentsID", _Documents.L_DocumentsID));

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
