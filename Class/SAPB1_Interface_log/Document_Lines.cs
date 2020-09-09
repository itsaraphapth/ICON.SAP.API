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
	[XmlRootAttribute("Document_Lines", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class Document_Lines
	{
		[XmlIgnore]
		[NonSerialized]
		public Document_Lines_Command ExecCommand = null;
		public Document_Lines()
		{
			ExecCommand = new Document_Lines_Command(this);
		}
		public Document_Lines(string ConnectionStr)
		{
			ExecCommand = new Document_Lines_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new Document_Lines_Command(ConnectionStr, this);
		}
		public Document_Lines(string ConnectionStr, String L_Document_LinesID_Value)
		{
			ExecCommand = new Document_Lines_Command(ConnectionStr, this);
			ExecCommand.Load(L_Document_LinesID_Value);
		}
        private String _L_Document_LinesID;
        public readonly bool _L_Document_LinesID_PKFlag = true;
        public readonly bool _L_Document_LinesID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_Document_LinesID = false;
		public String L_Document_LinesID
		{
			get
			{
				return _L_Document_LinesID;
			}
			set
			{
				if (_L_Document_LinesID != value)
                {
					_L_Document_LinesID = value;
					EditL_Document_LinesID = true;
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

        private String _L_DocumentsID;
        public readonly bool _L_DocumentsID_PKFlag = false;
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

        private Nullable<Int32> _ActualBaseEntry;
        public readonly bool _ActualBaseEntry_PKFlag = false;
        public readonly bool _ActualBaseEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditActualBaseEntry = false;
		public Nullable<Int32> ActualBaseEntry
		{
			get
			{
				return _ActualBaseEntry;
			}
			set
			{
				if (_ActualBaseEntry != value)
                {
					_ActualBaseEntry = value;
					EditActualBaseEntry = true;
				}
			}
		}

        private Nullable<Int32> _ActualBaseLine;
        public readonly bool _ActualBaseLine_PKFlag = false;
        public readonly bool _ActualBaseLine_IDTFlag = false;
		[XmlIgnore]
		public bool EditActualBaseLine = false;
		public Nullable<Int32> ActualBaseLine
		{
			get
			{
				return _ActualBaseLine;
			}
			set
			{
				if (_ActualBaseLine != value)
                {
					_ActualBaseLine = value;
					EditActualBaseLine = true;
				}
			}
		}

        private Nullable<DateTime> _ActualDeliveryDate;
        public readonly bool _ActualDeliveryDate_PKFlag = false;
        public readonly bool _ActualDeliveryDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditActualDeliveryDate = false;
		public Nullable<DateTime> ActualDeliveryDate
		{
			get
			{
				return _ActualDeliveryDate;
			}
			set
			{
				if (_ActualDeliveryDate != value)
                {
					_ActualDeliveryDate = value;
					EditActualDeliveryDate = true;
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

        private Nullable<Int32> _AgreementNo;
        public readonly bool _AgreementNo_PKFlag = false;
        public readonly bool _AgreementNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditAgreementNo = false;
		public Nullable<Int32> AgreementNo
		{
			get
			{
				return _AgreementNo;
			}
			set
			{
				if (_AgreementNo != value)
                {
					_AgreementNo = value;
					EditAgreementNo = true;
				}
			}
		}

        private Nullable<Int32> _AgreementRowNumber;
        public readonly bool _AgreementRowNumber_PKFlag = false;
        public readonly bool _AgreementRowNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditAgreementRowNumber = false;
		public Nullable<Int32> AgreementRowNumber
		{
			get
			{
				return _AgreementRowNumber;
			}
			set
			{
				if (_AgreementRowNumber != value)
                {
					_AgreementRowNumber = value;
					EditAgreementRowNumber = true;
				}
			}
		}

        private String _BackOrder;
        public readonly bool _BackOrder_PKFlag = false;
        public readonly bool _BackOrder_IDTFlag = false;
		[XmlIgnore]
		public bool EditBackOrder = false;
		public String BackOrder
		{
			get
			{
				return _BackOrder;
			}
			set
			{
				if (_BackOrder != value)
                {
					_BackOrder = value;
					EditBackOrder = true;
				}
			}
		}

        private String _BarCode;
        public readonly bool _BarCode_PKFlag = false;
        public readonly bool _BarCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditBarCode = false;
		public String BarCode
		{
			get
			{
				return _BarCode;
			}
			set
			{
				if (_BarCode != value)
                {
					_BarCode = value;
					EditBarCode = true;
				}
			}
		}

        private Nullable<Int32> _BaseEntry;
        public readonly bool _BaseEntry_PKFlag = false;
        public readonly bool _BaseEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditBaseEntry = false;
		public Nullable<Int32> BaseEntry
		{
			get
			{
				return _BaseEntry;
			}
			set
			{
				if (_BaseEntry != value)
                {
					_BaseEntry = value;
					EditBaseEntry = true;
				}
			}
		}

        private Nullable<Int32> _BaseLine;
        public readonly bool _BaseLine_PKFlag = false;
        public readonly bool _BaseLine_IDTFlag = false;
		[XmlIgnore]
		public bool EditBaseLine = false;
		public Nullable<Int32> BaseLine
		{
			get
			{
				return _BaseLine;
			}
			set
			{
				if (_BaseLine != value)
                {
					_BaseLine = value;
					EditBaseLine = true;
				}
			}
		}

        private Nullable<Int32> _BaseType;
        public readonly bool _BaseType_PKFlag = false;
        public readonly bool _BaseType_IDTFlag = false;
		[XmlIgnore]
		public bool EditBaseType = false;
		public Nullable<Int32> BaseType
		{
			get
			{
				return _BaseType;
			}
			set
			{
				if (_BaseType != value)
                {
					_BaseType = value;
					EditBaseType = true;
				}
			}
		}

        private String _CFOPCode;
        public readonly bool _CFOPCode_PKFlag = false;
        public readonly bool _CFOPCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditCFOPCode = false;
		public String CFOPCode
		{
			get
			{
				return _CFOPCode;
			}
			set
			{
				if (_CFOPCode != value)
                {
					_CFOPCode = value;
					EditCFOPCode = true;
				}
			}
		}

        private String _ChangeAssemlyBoMWarehouse;
        public readonly bool _ChangeAssemlyBoMWarehouse_PKFlag = false;
        public readonly bool _ChangeAssemlyBoMWarehouse_IDTFlag = false;
		[XmlIgnore]
		public bool EditChangeAssemlyBoMWarehouse = false;
		public String ChangeAssemlyBoMWarehouse
		{
			get
			{
				return _ChangeAssemlyBoMWarehouse;
			}
			set
			{
				if (_ChangeAssemlyBoMWarehouse != value)
                {
					_ChangeAssemlyBoMWarehouse = value;
					EditChangeAssemlyBoMWarehouse = true;
				}
			}
		}

        private String _ChangeInventoryQuantityIndependently;
        public readonly bool _ChangeInventoryQuantityIndependently_PKFlag = false;
        public readonly bool _ChangeInventoryQuantityIndependently_IDTFlag = false;
		[XmlIgnore]
		public bool EditChangeInventoryQuantityIndependently = false;
		public String ChangeInventoryQuantityIndependently
		{
			get
			{
				return _ChangeInventoryQuantityIndependently;
			}
			set
			{
				if (_ChangeInventoryQuantityIndependently != value)
                {
					_ChangeInventoryQuantityIndependently = value;
					EditChangeInventoryQuantityIndependently = true;
				}
			}
		}

        private String _COGSAccountCode;
        public readonly bool _COGSAccountCode_PKFlag = false;
        public readonly bool _COGSAccountCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditCOGSAccountCode = false;
		public String COGSAccountCode
		{
			get
			{
				return _COGSAccountCode;
			}
			set
			{
				if (_COGSAccountCode != value)
                {
					_COGSAccountCode = value;
					EditCOGSAccountCode = true;
				}
			}
		}

        private String _COGSCostingCode;
        public readonly bool _COGSCostingCode_PKFlag = false;
        public readonly bool _COGSCostingCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditCOGSCostingCode = false;
		public String COGSCostingCode
		{
			get
			{
				return _COGSCostingCode;
			}
			set
			{
				if (_COGSCostingCode != value)
                {
					_COGSCostingCode = value;
					EditCOGSCostingCode = true;
				}
			}
		}

        private String _COGSCostingCode2;
        public readonly bool _COGSCostingCode2_PKFlag = false;
        public readonly bool _COGSCostingCode2_IDTFlag = false;
		[XmlIgnore]
		public bool EditCOGSCostingCode2 = false;
		public String COGSCostingCode2
		{
			get
			{
				return _COGSCostingCode2;
			}
			set
			{
				if (_COGSCostingCode2 != value)
                {
					_COGSCostingCode2 = value;
					EditCOGSCostingCode2 = true;
				}
			}
		}

        private String _COGSCostingCode3;
        public readonly bool _COGSCostingCode3_PKFlag = false;
        public readonly bool _COGSCostingCode3_IDTFlag = false;
		[XmlIgnore]
		public bool EditCOGSCostingCode3 = false;
		public String COGSCostingCode3
		{
			get
			{
				return _COGSCostingCode3;
			}
			set
			{
				if (_COGSCostingCode3 != value)
                {
					_COGSCostingCode3 = value;
					EditCOGSCostingCode3 = true;
				}
			}
		}

        private String _COGSCostingCode4;
        public readonly bool _COGSCostingCode4_PKFlag = false;
        public readonly bool _COGSCostingCode4_IDTFlag = false;
		[XmlIgnore]
		public bool EditCOGSCostingCode4 = false;
		public String COGSCostingCode4
		{
			get
			{
				return _COGSCostingCode4;
			}
			set
			{
				if (_COGSCostingCode4 != value)
                {
					_COGSCostingCode4 = value;
					EditCOGSCostingCode4 = true;
				}
			}
		}

        private String _COGSCostingCode5;
        public readonly bool _COGSCostingCode5_PKFlag = false;
        public readonly bool _COGSCostingCode5_IDTFlag = false;
		[XmlIgnore]
		public bool EditCOGSCostingCode5 = false;
		public String COGSCostingCode5
		{
			get
			{
				return _COGSCostingCode5;
			}
			set
			{
				if (_COGSCostingCode5 != value)
                {
					_COGSCostingCode5 = value;
					EditCOGSCostingCode5 = true;
				}
			}
		}

        private Nullable<Decimal> _CommisionPercent;
        public readonly bool _CommisionPercent_PKFlag = false;
        public readonly bool _CommisionPercent_IDTFlag = false;
		[XmlIgnore]
		public bool EditCommisionPercent = false;
		public Nullable<Decimal> CommisionPercent
		{
			get
			{
				return _CommisionPercent;
			}
			set
			{
				if (_CommisionPercent != value)
                {
					_CommisionPercent = value;
					EditCommisionPercent = true;
				}
			}
		}

        private String _ConsiderQuantity;
        public readonly bool _ConsiderQuantity_PKFlag = false;
        public readonly bool _ConsiderQuantity_IDTFlag = false;
		[XmlIgnore]
		public bool EditConsiderQuantity = false;
		public String ConsiderQuantity
		{
			get
			{
				return _ConsiderQuantity;
			}
			set
			{
				if (_ConsiderQuantity != value)
                {
					_ConsiderQuantity = value;
					EditConsiderQuantity = true;
				}
			}
		}

        private String _ConsumerSalesForecast;
        public readonly bool _ConsumerSalesForecast_PKFlag = false;
        public readonly bool _ConsumerSalesForecast_IDTFlag = false;
		[XmlIgnore]
		public bool EditConsumerSalesForecast = false;
		public String ConsumerSalesForecast
		{
			get
			{
				return _ConsumerSalesForecast;
			}
			set
			{
				if (_ConsumerSalesForecast != value)
                {
					_ConsumerSalesForecast = value;
					EditConsumerSalesForecast = true;
				}
			}
		}

        private String _CorrectionInvoiceItem;
        public readonly bool _CorrectionInvoiceItem_PKFlag = false;
        public readonly bool _CorrectionInvoiceItem_IDTFlag = false;
		[XmlIgnore]
		public bool EditCorrectionInvoiceItem = false;
		public String CorrectionInvoiceItem
		{
			get
			{
				return _CorrectionInvoiceItem;
			}
			set
			{
				if (_CorrectionInvoiceItem != value)
                {
					_CorrectionInvoiceItem = value;
					EditCorrectionInvoiceItem = true;
				}
			}
		}

        private Nullable<Decimal> _CorrInvAmountToDiffAcct;
        public readonly bool _CorrInvAmountToDiffAcct_PKFlag = false;
        public readonly bool _CorrInvAmountToDiffAcct_IDTFlag = false;
		[XmlIgnore]
		public bool EditCorrInvAmountToDiffAcct = false;
		public Nullable<Decimal> CorrInvAmountToDiffAcct
		{
			get
			{
				return _CorrInvAmountToDiffAcct;
			}
			set
			{
				if (_CorrInvAmountToDiffAcct != value)
                {
					_CorrInvAmountToDiffAcct = value;
					EditCorrInvAmountToDiffAcct = true;
				}
			}
		}

        private Nullable<Decimal> _CorrInvAmountToStock;
        public readonly bool _CorrInvAmountToStock_PKFlag = false;
        public readonly bool _CorrInvAmountToStock_IDTFlag = false;
		[XmlIgnore]
		public bool EditCorrInvAmountToStock = false;
		public Nullable<Decimal> CorrInvAmountToStock
		{
			get
			{
				return _CorrInvAmountToStock;
			}
			set
			{
				if (_CorrInvAmountToStock != value)
                {
					_CorrInvAmountToStock = value;
					EditCorrInvAmountToStock = true;
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

        private String _CountryOrg;
        public readonly bool _CountryOrg_PKFlag = false;
        public readonly bool _CountryOrg_IDTFlag = false;
		[XmlIgnore]
		public bool EditCountryOrg = false;
		public String CountryOrg
		{
			get
			{
				return _CountryOrg;
			}
			set
			{
				if (_CountryOrg != value)
                {
					_CountryOrg = value;
					EditCountryOrg = true;
				}
			}
		}

        private String _CreditOriginCode;
        public readonly bool _CreditOriginCode_PKFlag = false;
        public readonly bool _CreditOriginCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditCreditOriginCode = false;
		public String CreditOriginCode
		{
			get
			{
				return _CreditOriginCode;
			}
			set
			{
				if (_CreditOriginCode != value)
                {
					_CreditOriginCode = value;
					EditCreditOriginCode = true;
				}
			}
		}

        private String _CSTCode;
        public readonly bool _CSTCode_PKFlag = false;
        public readonly bool _CSTCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditCSTCode = false;
		public String CSTCode
		{
			get
			{
				return _CSTCode;
			}
			set
			{
				if (_CSTCode != value)
                {
					_CSTCode = value;
					EditCSTCode = true;
				}
			}
		}

        private String _CSTforCOFINS;
        public readonly bool _CSTforCOFINS_PKFlag = false;
        public readonly bool _CSTforCOFINS_IDTFlag = false;
		[XmlIgnore]
		public bool EditCSTforCOFINS = false;
		public String CSTforCOFINS
		{
			get
			{
				return _CSTforCOFINS;
			}
			set
			{
				if (_CSTforCOFINS != value)
                {
					_CSTforCOFINS = value;
					EditCSTforCOFINS = true;
				}
			}
		}

        private String _CSTforIPI;
        public readonly bool _CSTforIPI_PKFlag = false;
        public readonly bool _CSTforIPI_IDTFlag = false;
		[XmlIgnore]
		public bool EditCSTforIPI = false;
		public String CSTforIPI
		{
			get
			{
				return _CSTforIPI;
			}
			set
			{
				if (_CSTforIPI != value)
                {
					_CSTforIPI = value;
					EditCSTforIPI = true;
				}
			}
		}

        private String _CSTforPIS;
        public readonly bool _CSTforPIS_PKFlag = false;
        public readonly bool _CSTforPIS_IDTFlag = false;
		[XmlIgnore]
		public bool EditCSTforPIS = false;
		public String CSTforPIS
		{
			get
			{
				return _CSTforPIS;
			}
			set
			{
				if (_CSTforPIS != value)
                {
					_CSTforPIS = value;
					EditCSTforPIS = true;
				}
			}
		}

        private String _Currency;
        public readonly bool _Currency_PKFlag = false;
        public readonly bool _Currency_IDTFlag = false;
		[XmlIgnore]
		public bool EditCurrency = false;
		public String Currency
		{
			get
			{
				return _Currency;
			}
			set
			{
				if (_Currency != value)
                {
					_Currency = value;
					EditCurrency = true;
				}
			}
		}

        private Nullable<Decimal> _DefectAndBreakup;
        public readonly bool _DefectAndBreakup_PKFlag = false;
        public readonly bool _DefectAndBreakup_IDTFlag = false;
		[XmlIgnore]
		public bool EditDefectAndBreakup = false;
		public Nullable<Decimal> DefectAndBreakup
		{
			get
			{
				return _DefectAndBreakup;
			}
			set
			{
				if (_DefectAndBreakup != value)
                {
					_DefectAndBreakup = value;
					EditDefectAndBreakup = true;
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

        private String _DistributeExpense;
        public readonly bool _DistributeExpense_PKFlag = false;
        public readonly bool _DistributeExpense_IDTFlag = false;
		[XmlIgnore]
		public bool EditDistributeExpense = false;
		public String DistributeExpense
		{
			get
			{
				return _DistributeExpense;
			}
			set
			{
				if (_DistributeExpense != value)
                {
					_DistributeExpense = value;
					EditDistributeExpense = true;
				}
			}
		}

        private String _EnableReturnCost;
        public readonly bool _EnableReturnCost_PKFlag = false;
        public readonly bool _EnableReturnCost_IDTFlag = false;
		[XmlIgnore]
		public bool EditEnableReturnCost = false;
		public String EnableReturnCost
		{
			get
			{
				return _EnableReturnCost;
			}
			set
			{
				if (_EnableReturnCost != value)
                {
					_EnableReturnCost = value;
					EditEnableReturnCost = true;
				}
			}
		}

        private Nullable<Decimal> _ExciseAmount;
        public readonly bool _ExciseAmount_PKFlag = false;
        public readonly bool _ExciseAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditExciseAmount = false;
		public Nullable<Decimal> ExciseAmount
		{
			get
			{
				return _ExciseAmount;
			}
			set
			{
				if (_ExciseAmount != value)
                {
					_ExciseAmount = value;
					EditExciseAmount = true;
				}
			}
		}

        private String _ExLineNo;
        public readonly bool _ExLineNo_PKFlag = false;
        public readonly bool _ExLineNo_IDTFlag = false;
		[XmlIgnore]
		public bool EditExLineNo = false;
		public String ExLineNo
		{
			get
			{
				return _ExLineNo;
			}
			set
			{
				if (_ExLineNo != value)
                {
					_ExLineNo = value;
					EditExLineNo = true;
				}
			}
		}

        private String _ExpenseOperationType;
        public readonly bool _ExpenseOperationType_PKFlag = false;
        public readonly bool _ExpenseOperationType_IDTFlag = false;
		[XmlIgnore]
		public bool EditExpenseOperationType = false;
		public String ExpenseOperationType
		{
			get
			{
				return _ExpenseOperationType;
			}
			set
			{
				if (_ExpenseOperationType != value)
                {
					_ExpenseOperationType = value;
					EditExpenseOperationType = true;
				}
			}
		}

        private String _ExpenseType;
        public readonly bool _ExpenseType_PKFlag = false;
        public readonly bool _ExpenseType_IDTFlag = false;
		[XmlIgnore]
		public bool EditExpenseType = false;
		public String ExpenseType
		{
			get
			{
				return _ExpenseType;
			}
			set
			{
				if (_ExpenseType != value)
                {
					_ExpenseType = value;
					EditExpenseType = true;
				}
			}
		}

        private Nullable<Decimal> _Factor1;
        public readonly bool _Factor1_PKFlag = false;
        public readonly bool _Factor1_IDTFlag = false;
		[XmlIgnore]
		public bool EditFactor1 = false;
		public Nullable<Decimal> Factor1
		{
			get
			{
				return _Factor1;
			}
			set
			{
				if (_Factor1 != value)
                {
					_Factor1 = value;
					EditFactor1 = true;
				}
			}
		}

        private Nullable<Decimal> _Factor2;
        public readonly bool _Factor2_PKFlag = false;
        public readonly bool _Factor2_IDTFlag = false;
		[XmlIgnore]
		public bool EditFactor2 = false;
		public Nullable<Decimal> Factor2
		{
			get
			{
				return _Factor2;
			}
			set
			{
				if (_Factor2 != value)
                {
					_Factor2 = value;
					EditFactor2 = true;
				}
			}
		}

        private Nullable<Decimal> _Factor3;
        public readonly bool _Factor3_PKFlag = false;
        public readonly bool _Factor3_IDTFlag = false;
		[XmlIgnore]
		public bool EditFactor3 = false;
		public Nullable<Decimal> Factor3
		{
			get
			{
				return _Factor3;
			}
			set
			{
				if (_Factor3 != value)
                {
					_Factor3 = value;
					EditFactor3 = true;
				}
			}
		}

        private Nullable<Decimal> _Factor4;
        public readonly bool _Factor4_PKFlag = false;
        public readonly bool _Factor4_IDTFlag = false;
		[XmlIgnore]
		public bool EditFactor4 = false;
		public Nullable<Decimal> Factor4
		{
			get
			{
				return _Factor4;
			}
			set
			{
				if (_Factor4 != value)
                {
					_Factor4 = value;
					EditFactor4 = true;
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

        private String _FreeOfChargeBP;
        public readonly bool _FreeOfChargeBP_PKFlag = false;
        public readonly bool _FreeOfChargeBP_IDTFlag = false;
		[XmlIgnore]
		public bool EditFreeOfChargeBP = false;
		public String FreeOfChargeBP
		{
			get
			{
				return _FreeOfChargeBP;
			}
			set
			{
				if (_FreeOfChargeBP != value)
                {
					_FreeOfChargeBP = value;
					EditFreeOfChargeBP = true;
				}
			}
		}

        private String _FreeText;
        public readonly bool _FreeText_PKFlag = false;
        public readonly bool _FreeText_IDTFlag = false;
		[XmlIgnore]
		public bool EditFreeText = false;
		public String FreeText
		{
			get
			{
				return _FreeText;
			}
			set
			{
				if (_FreeText != value)
                {
					_FreeText = value;
					EditFreeText = true;
				}
			}
		}

        private Nullable<Int32> _GrossBase;
        public readonly bool _GrossBase_PKFlag = false;
        public readonly bool _GrossBase_IDTFlag = false;
		[XmlIgnore]
		public bool EditGrossBase = false;
		public Nullable<Int32> GrossBase
		{
			get
			{
				return _GrossBase;
			}
			set
			{
				if (_GrossBase != value)
                {
					_GrossBase = value;
					EditGrossBase = true;
				}
			}
		}

        private Nullable<Decimal> _GrossBuyPrice;
        public readonly bool _GrossBuyPrice_PKFlag = false;
        public readonly bool _GrossBuyPrice_IDTFlag = false;
		[XmlIgnore]
		public bool EditGrossBuyPrice = false;
		public Nullable<Decimal> GrossBuyPrice
		{
			get
			{
				return _GrossBuyPrice;
			}
			set
			{
				if (_GrossBuyPrice != value)
                {
					_GrossBuyPrice = value;
					EditGrossBuyPrice = true;
				}
			}
		}

        private Nullable<Decimal> _GrossPrice;
        public readonly bool _GrossPrice_PKFlag = false;
        public readonly bool _GrossPrice_IDTFlag = false;
		[XmlIgnore]
		public bool EditGrossPrice = false;
		public Nullable<Decimal> GrossPrice
		{
			get
			{
				return _GrossPrice;
			}
			set
			{
				if (_GrossPrice != value)
                {
					_GrossPrice = value;
					EditGrossPrice = true;
				}
			}
		}

        private Nullable<Decimal> _GrossProfitTotalBasePrice;
        public readonly bool _GrossProfitTotalBasePrice_PKFlag = false;
        public readonly bool _GrossProfitTotalBasePrice_IDTFlag = false;
		[XmlIgnore]
		public bool EditGrossProfitTotalBasePrice = false;
		public Nullable<Decimal> GrossProfitTotalBasePrice
		{
			get
			{
				return _GrossProfitTotalBasePrice;
			}
			set
			{
				if (_GrossProfitTotalBasePrice != value)
                {
					_GrossProfitTotalBasePrice = value;
					EditGrossProfitTotalBasePrice = true;
				}
			}
		}

        private Nullable<Decimal> _GrossTotal;
        public readonly bool _GrossTotal_PKFlag = false;
        public readonly bool _GrossTotal_IDTFlag = false;
		[XmlIgnore]
		public bool EditGrossTotal = false;
		public Nullable<Decimal> GrossTotal
		{
			get
			{
				return _GrossTotal;
			}
			set
			{
				if (_GrossTotal != value)
                {
					_GrossTotal = value;
					EditGrossTotal = true;
				}
			}
		}

        private Nullable<Decimal> _GrossTotalFC;
        public readonly bool _GrossTotalFC_PKFlag = false;
        public readonly bool _GrossTotalFC_IDTFlag = false;
		[XmlIgnore]
		public bool EditGrossTotalFC = false;
		public Nullable<Decimal> GrossTotalFC
		{
			get
			{
				return _GrossTotalFC;
			}
			set
			{
				if (_GrossTotalFC != value)
                {
					_GrossTotalFC = value;
					EditGrossTotalFC = true;
				}
			}
		}

        private Nullable<Decimal> _Height1;
        public readonly bool _Height1_PKFlag = false;
        public readonly bool _Height1_IDTFlag = false;
		[XmlIgnore]
		public bool EditHeight1 = false;
		public Nullable<Decimal> Height1
		{
			get
			{
				return _Height1;
			}
			set
			{
				if (_Height1 != value)
                {
					_Height1 = value;
					EditHeight1 = true;
				}
			}
		}

        private Nullable<Decimal> _Height2;
        public readonly bool _Height2_PKFlag = false;
        public readonly bool _Height2_IDTFlag = false;
		[XmlIgnore]
		public bool EditHeight2 = false;
		public Nullable<Decimal> Height2
		{
			get
			{
				return _Height2;
			}
			set
			{
				if (_Height2 != value)
                {
					_Height2 = value;
					EditHeight2 = true;
				}
			}
		}

        private Nullable<Int32> _Height2Unit;
        public readonly bool _Height2Unit_PKFlag = false;
        public readonly bool _Height2Unit_IDTFlag = false;
		[XmlIgnore]
		public bool EditHeight2Unit = false;
		public Nullable<Int32> Height2Unit
		{
			get
			{
				return _Height2Unit;
			}
			set
			{
				if (_Height2Unit != value)
                {
					_Height2Unit = value;
					EditHeight2Unit = true;
				}
			}
		}

        private Nullable<Int32> _Hight1Unit;
        public readonly bool _Hight1Unit_PKFlag = false;
        public readonly bool _Hight1Unit_IDTFlag = false;
		[XmlIgnore]
		public bool EditHight1Unit = false;
		public Nullable<Int32> Hight1Unit
		{
			get
			{
				return _Hight1Unit;
			}
			set
			{
				if (_Hight1Unit != value)
                {
					_Hight1Unit = value;
					EditHight1Unit = true;
				}
			}
		}

        private Nullable<Int32> _HSNEntry;
        public readonly bool _HSNEntry_PKFlag = false;
        public readonly bool _HSNEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditHSNEntry = false;
		public Nullable<Int32> HSNEntry
		{
			get
			{
				return _HSNEntry;
			}
			set
			{
				if (_HSNEntry != value)
                {
					_HSNEntry = value;
					EditHSNEntry = true;
				}
			}
		}

        private Nullable<Int32> _Incoterms;
        public readonly bool _Incoterms_PKFlag = false;
        public readonly bool _Incoterms_IDTFlag = false;
		[XmlIgnore]
		public bool EditIncoterms = false;
		public Nullable<Int32> Incoterms
		{
			get
			{
				return _Incoterms;
			}
			set
			{
				if (_Incoterms != value)
                {
					_Incoterms = value;
					EditIncoterms = true;
				}
			}
		}

        private Nullable<Decimal> _InventoryQuantity;
        public readonly bool _InventoryQuantity_PKFlag = false;
        public readonly bool _InventoryQuantity_IDTFlag = false;
		[XmlIgnore]
		public bool EditInventoryQuantity = false;
		public Nullable<Decimal> InventoryQuantity
		{
			get
			{
				return _InventoryQuantity;
			}
			set
			{
				if (_InventoryQuantity != value)
                {
					_InventoryQuantity = value;
					EditInventoryQuantity = true;
				}
			}
		}

        private String _ItemCode;
        public readonly bool _ItemCode_PKFlag = false;
        public readonly bool _ItemCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditItemCode = false;
		public String ItemCode
		{
			get
			{
				return _ItemCode;
			}
			set
			{
				if (_ItemCode != value)
                {
					_ItemCode = value;
					EditItemCode = true;
				}
			}
		}

        private String _ItemDescription;
        public readonly bool _ItemDescription_PKFlag = false;
        public readonly bool _ItemDescription_IDTFlag = false;
		[XmlIgnore]
		public bool EditItemDescription = false;
		public String ItemDescription
		{
			get
			{
				return _ItemDescription;
			}
			set
			{
				if (_ItemDescription != value)
                {
					_ItemDescription = value;
					EditItemDescription = true;
				}
			}
		}

        private String _ItemDetails;
        public readonly bool _ItemDetails_PKFlag = false;
        public readonly bool _ItemDetails_IDTFlag = false;
		[XmlIgnore]
		public bool EditItemDetails = false;
		public String ItemDetails
		{
			get
			{
				return _ItemDetails;
			}
			set
			{
				if (_ItemDetails != value)
                {
					_ItemDetails = value;
					EditItemDetails = true;
				}
			}
		}

        private Nullable<Decimal> _Lengh1;
        public readonly bool _Lengh1_PKFlag = false;
        public readonly bool _Lengh1_IDTFlag = false;
		[XmlIgnore]
		public bool EditLengh1 = false;
		public Nullable<Decimal> Lengh1
		{
			get
			{
				return _Lengh1;
			}
			set
			{
				if (_Lengh1 != value)
                {
					_Lengh1 = value;
					EditLengh1 = true;
				}
			}
		}

        private Nullable<Int32> _Lengh1Unit;
        public readonly bool _Lengh1Unit_PKFlag = false;
        public readonly bool _Lengh1Unit_IDTFlag = false;
		[XmlIgnore]
		public bool EditLengh1Unit = false;
		public Nullable<Int32> Lengh1Unit
		{
			get
			{
				return _Lengh1Unit;
			}
			set
			{
				if (_Lengh1Unit != value)
                {
					_Lengh1Unit = value;
					EditLengh1Unit = true;
				}
			}
		}

        private Nullable<Decimal> _Lengh2;
        public readonly bool _Lengh2_PKFlag = false;
        public readonly bool _Lengh2_IDTFlag = false;
		[XmlIgnore]
		public bool EditLengh2 = false;
		public Nullable<Decimal> Lengh2
		{
			get
			{
				return _Lengh2;
			}
			set
			{
				if (_Lengh2 != value)
                {
					_Lengh2 = value;
					EditLengh2 = true;
				}
			}
		}

        private Nullable<Int32> _Lengh2Unit;
        public readonly bool _Lengh2Unit_PKFlag = false;
        public readonly bool _Lengh2Unit_IDTFlag = false;
		[XmlIgnore]
		public bool EditLengh2Unit = false;
		public Nullable<Int32> Lengh2Unit
		{
			get
			{
				return _Lengh2Unit;
			}
			set
			{
				if (_Lengh2Unit != value)
                {
					_Lengh2Unit = value;
					EditLengh2Unit = true;
				}
			}
		}

        private String _LineStatus;
        public readonly bool _LineStatus_PKFlag = false;
        public readonly bool _LineStatus_IDTFlag = false;
		[XmlIgnore]
		public bool EditLineStatus = false;
		public String LineStatus
		{
			get
			{
				return _LineStatus;
			}
			set
			{
				if (_LineStatus != value)
                {
					_LineStatus = value;
					EditLineStatus = true;
				}
			}
		}

        private Nullable<Decimal> _LineTotal;
        public readonly bool _LineTotal_PKFlag = false;
        public readonly bool _LineTotal_IDTFlag = false;
		[XmlIgnore]
		public bool EditLineTotal = false;
		public Nullable<Decimal> LineTotal
		{
			get
			{
				return _LineTotal;
			}
			set
			{
				if (_LineTotal != value)
                {
					_LineTotal = value;
					EditLineTotal = true;
				}
			}
		}

        private String _LineType;
        public readonly bool _LineType_PKFlag = false;
        public readonly bool _LineType_IDTFlag = false;
		[XmlIgnore]
		public bool EditLineType = false;
		public String LineType
		{
			get
			{
				return _LineType;
			}
			set
			{
				if (_LineType != value)
                {
					_LineType = value;
					EditLineType = true;
				}
			}
		}

        private String _LineVendor;
        public readonly bool _LineVendor_PKFlag = false;
        public readonly bool _LineVendor_IDTFlag = false;
		[XmlIgnore]
		public bool EditLineVendor = false;
		public String LineVendor
		{
			get
			{
				return _LineVendor;
			}
			set
			{
				if (_LineVendor != value)
                {
					_LineVendor = value;
					EditLineVendor = true;
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

        private String _MeasureUnit;
        public readonly bool _MeasureUnit_PKFlag = false;
        public readonly bool _MeasureUnit_IDTFlag = false;
		[XmlIgnore]
		public bool EditMeasureUnit = false;
		public String MeasureUnit
		{
			get
			{
				return _MeasureUnit;
			}
			set
			{
				if (_MeasureUnit != value)
                {
					_MeasureUnit = value;
					EditMeasureUnit = true;
				}
			}
		}

        private Nullable<Int32> _NCMCode;
        public readonly bool _NCMCode_PKFlag = false;
        public readonly bool _NCMCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditNCMCode = false;
		public Nullable<Int32> NCMCode
		{
			get
			{
				return _NCMCode;
			}
			set
			{
				if (_NCMCode != value)
                {
					_NCMCode = value;
					EditNCMCode = true;
				}
			}
		}

        private Nullable<Decimal> _NetTaxAmount;
        public readonly bool _NetTaxAmount_PKFlag = false;
        public readonly bool _NetTaxAmount_IDTFlag = false;
		[XmlIgnore]
		public bool EditNetTaxAmount = false;
		public Nullable<Decimal> NetTaxAmount
		{
			get
			{
				return _NetTaxAmount;
			}
			set
			{
				if (_NetTaxAmount != value)
                {
					_NetTaxAmount = value;
					EditNetTaxAmount = true;
				}
			}
		}

        private Nullable<Decimal> _NetTaxAmountFC;
        public readonly bool _NetTaxAmountFC_PKFlag = false;
        public readonly bool _NetTaxAmountFC_IDTFlag = false;
		[XmlIgnore]
		public bool EditNetTaxAmountFC = false;
		public Nullable<Decimal> NetTaxAmountFC
		{
			get
			{
				return _NetTaxAmountFC;
			}
			set
			{
				if (_NetTaxAmountFC != value)
                {
					_NetTaxAmountFC = value;
					EditNetTaxAmountFC = true;
				}
			}
		}

        private Nullable<Decimal> _PackageQuantity;
        public readonly bool _PackageQuantity_PKFlag = false;
        public readonly bool _PackageQuantity_IDTFlag = false;
		[XmlIgnore]
		public bool EditPackageQuantity = false;
		public Nullable<Decimal> PackageQuantity
		{
			get
			{
				return _PackageQuantity;
			}
			set
			{
				if (_PackageQuantity != value)
                {
					_PackageQuantity = value;
					EditPackageQuantity = true;
				}
			}
		}

        private Nullable<Int32> _ParentLineNum;
        public readonly bool _ParentLineNum_PKFlag = false;
        public readonly bool _ParentLineNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditParentLineNum = false;
		public Nullable<Int32> ParentLineNum
		{
			get
			{
				return _ParentLineNum;
			}
			set
			{
				if (_ParentLineNum != value)
                {
					_ParentLineNum = value;
					EditParentLineNum = true;
				}
			}
		}

        private String _PartialRetirement;
        public readonly bool _PartialRetirement_PKFlag = false;
        public readonly bool _PartialRetirement_IDTFlag = false;
		[XmlIgnore]
		public bool EditPartialRetirement = false;
		public String PartialRetirement
		{
			get
			{
				return _PartialRetirement;
			}
			set
			{
				if (_PartialRetirement != value)
                {
					_PartialRetirement = value;
					EditPartialRetirement = true;
				}
			}
		}

        private Nullable<Decimal> _Price;
        public readonly bool _Price_PKFlag = false;
        public readonly bool _Price_IDTFlag = false;
		[XmlIgnore]
		public bool EditPrice = false;
		public Nullable<Decimal> Price
		{
			get
			{
				return _Price;
			}
			set
			{
				if (_Price != value)
                {
					_Price = value;
					EditPrice = true;
				}
			}
		}

        private Nullable<Decimal> _PriceAfterVAT;
        public readonly bool _PriceAfterVAT_PKFlag = false;
        public readonly bool _PriceAfterVAT_IDTFlag = false;
		[XmlIgnore]
		public bool EditPriceAfterVAT = false;
		public Nullable<Decimal> PriceAfterVAT
		{
			get
			{
				return _PriceAfterVAT;
			}
			set
			{
				if (_PriceAfterVAT != value)
                {
					_PriceAfterVAT = value;
					EditPriceAfterVAT = true;
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

        private Nullable<Decimal> _Quantity;
        public readonly bool _Quantity_PKFlag = false;
        public readonly bool _Quantity_IDTFlag = false;
		[XmlIgnore]
		public bool EditQuantity = false;
		public Nullable<Decimal> Quantity
		{
			get
			{
				return _Quantity;
			}
			set
			{
				if (_Quantity != value)
                {
					_Quantity = value;
					EditQuantity = true;
				}
			}
		}

        private Nullable<Decimal> _Rate;
        public readonly bool _Rate_PKFlag = false;
        public readonly bool _Rate_IDTFlag = false;
		[XmlIgnore]
		public bool EditRate = false;
		public Nullable<Decimal> Rate
		{
			get
			{
				return _Rate;
			}
			set
			{
				if (_Rate != value)
                {
					_Rate = value;
					EditRate = true;
				}
			}
		}

        private String _ReceiptNumber;
        public readonly bool _ReceiptNumber_PKFlag = false;
        public readonly bool _ReceiptNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditReceiptNumber = false;
		public String ReceiptNumber
		{
			get
			{
				return _ReceiptNumber;
			}
			set
			{
				if (_ReceiptNumber != value)
                {
					_ReceiptNumber = value;
					EditReceiptNumber = true;
				}
			}
		}

        private Nullable<DateTime> _RequiredDate;
        public readonly bool _RequiredDate_PKFlag = false;
        public readonly bool _RequiredDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditRequiredDate = false;
		public Nullable<DateTime> RequiredDate
		{
			get
			{
				return _RequiredDate;
			}
			set
			{
				if (_RequiredDate != value)
                {
					_RequiredDate = value;
					EditRequiredDate = true;
				}
			}
		}

        private Nullable<Decimal> _RequiredQuantity;
        public readonly bool _RequiredQuantity_PKFlag = false;
        public readonly bool _RequiredQuantity_IDTFlag = false;
		[XmlIgnore]
		public bool EditRequiredQuantity = false;
		public Nullable<Decimal> RequiredQuantity
		{
			get
			{
				return _RequiredQuantity;
			}
			set
			{
				if (_RequiredQuantity != value)
                {
					_RequiredQuantity = value;
					EditRequiredQuantity = true;
				}
			}
		}

        private Nullable<Decimal> _RetirementAPC;
        public readonly bool _RetirementAPC_PKFlag = false;
        public readonly bool _RetirementAPC_IDTFlag = false;
		[XmlIgnore]
		public bool EditRetirementAPC = false;
		public Nullable<Decimal> RetirementAPC
		{
			get
			{
				return _RetirementAPC;
			}
			set
			{
				if (_RetirementAPC != value)
                {
					_RetirementAPC = value;
					EditRetirementAPC = true;
				}
			}
		}

        private Nullable<Decimal> _RetirementQuantity;
        public readonly bool _RetirementQuantity_PKFlag = false;
        public readonly bool _RetirementQuantity_IDTFlag = false;
		[XmlIgnore]
		public bool EditRetirementQuantity = false;
		public Nullable<Decimal> RetirementQuantity
		{
			get
			{
				return _RetirementQuantity;
			}
			set
			{
				if (_RetirementQuantity != value)
                {
					_RetirementQuantity = value;
					EditRetirementQuantity = true;
				}
			}
		}

        private Nullable<Int32> _ReturnAction;
        public readonly bool _ReturnAction_PKFlag = false;
        public readonly bool _ReturnAction_IDTFlag = false;
		[XmlIgnore]
		public bool EditReturnAction = false;
		public Nullable<Int32> ReturnAction
		{
			get
			{
				return _ReturnAction;
			}
			set
			{
				if (_ReturnAction != value)
                {
					_ReturnAction = value;
					EditReturnAction = true;
				}
			}
		}

        private Nullable<Decimal> _ReturnCost;
        public readonly bool _ReturnCost_PKFlag = false;
        public readonly bool _ReturnCost_IDTFlag = false;
		[XmlIgnore]
		public bool EditReturnCost = false;
		public Nullable<Decimal> ReturnCost
		{
			get
			{
				return _ReturnCost;
			}
			set
			{
				if (_ReturnCost != value)
                {
					_ReturnCost = value;
					EditReturnCost = true;
				}
			}
		}

        private Nullable<Int32> _ReturnReason;
        public readonly bool _ReturnReason_PKFlag = false;
        public readonly bool _ReturnReason_IDTFlag = false;
		[XmlIgnore]
		public bool EditReturnReason = false;
		public Nullable<Int32> ReturnReason
		{
			get
			{
				return _ReturnReason;
			}
			set
			{
				if (_ReturnReason != value)
                {
					_ReturnReason = value;
					EditReturnReason = true;
				}
			}
		}

        private Nullable<Decimal> _RowTotalFC;
        public readonly bool _RowTotalFC_PKFlag = false;
        public readonly bool _RowTotalFC_IDTFlag = false;
		[XmlIgnore]
		public bool EditRowTotalFC = false;
		public Nullable<Decimal> RowTotalFC
		{
			get
			{
				return _RowTotalFC;
			}
			set
			{
				if (_RowTotalFC != value)
                {
					_RowTotalFC = value;
					EditRowTotalFC = true;
				}
			}
		}

        private Nullable<Int32> _SACEntry;
        public readonly bool _SACEntry_PKFlag = false;
        public readonly bool _SACEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditSACEntry = false;
		public Nullable<Int32> SACEntry
		{
			get
			{
				return _SACEntry;
			}
			set
			{
				if (_SACEntry != value)
                {
					_SACEntry = value;
					EditSACEntry = true;
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

        private String _SerialNum;
        public readonly bool _SerialNum_PKFlag = false;
        public readonly bool _SerialNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditSerialNum = false;
		public String SerialNum
		{
			get
			{
				return _SerialNum;
			}
			set
			{
				if (_SerialNum != value)
                {
					_SerialNum = value;
					EditSerialNum = true;
				}
			}
		}

        private Nullable<DateTime> _ShipDate;
        public readonly bool _ShipDate_PKFlag = false;
        public readonly bool _ShipDate_IDTFlag = false;
		[XmlIgnore]
		public bool EditShipDate = false;
		public Nullable<DateTime> ShipDate
		{
			get
			{
				return _ShipDate;
			}
			set
			{
				if (_ShipDate != value)
                {
					_ShipDate = value;
					EditShipDate = true;
				}
			}
		}

        private String _ShipFromCode;
        public readonly bool _ShipFromCode_PKFlag = false;
        public readonly bool _ShipFromCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditShipFromCode = false;
		public String ShipFromCode
		{
			get
			{
				return _ShipFromCode;
			}
			set
			{
				if (_ShipFromCode != value)
                {
					_ShipFromCode = value;
					EditShipFromCode = true;
				}
			}
		}

        private String _ShipFromDescription;
        public readonly bool _ShipFromDescription_PKFlag = false;
        public readonly bool _ShipFromDescription_IDTFlag = false;
		[XmlIgnore]
		public bool EditShipFromDescription = false;
		public String ShipFromDescription
		{
			get
			{
				return _ShipFromDescription;
			}
			set
			{
				if (_ShipFromDescription != value)
                {
					_ShipFromDescription = value;
					EditShipFromDescription = true;
				}
			}
		}

        private Nullable<Int32> _ShippingMethod;
        public readonly bool _ShippingMethod_PKFlag = false;
        public readonly bool _ShippingMethod_IDTFlag = false;
		[XmlIgnore]
		public bool EditShippingMethod = false;
		public Nullable<Int32> ShippingMethod
		{
			get
			{
				return _ShippingMethod;
			}
			set
			{
				if (_ShippingMethod != value)
                {
					_ShippingMethod = value;
					EditShippingMethod = true;
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

        private String _ShipToDescription;
        public readonly bool _ShipToDescription_PKFlag = false;
        public readonly bool _ShipToDescription_IDTFlag = false;
		[XmlIgnore]
		public bool EditShipToDescription = false;
		public String ShipToDescription
		{
			get
			{
				return _ShipToDescription;
			}
			set
			{
				if (_ShipToDescription != value)
                {
					_ShipToDescription = value;
					EditShipToDescription = true;
				}
			}
		}

        private Nullable<Decimal> _Shortages;
        public readonly bool _Shortages_PKFlag = false;
        public readonly bool _Shortages_IDTFlag = false;
		[XmlIgnore]
		public bool EditShortages = false;
		public Nullable<Decimal> Shortages
		{
			get
			{
				return _Shortages;
			}
			set
			{
				if (_Shortages != value)
                {
					_Shortages = value;
					EditShortages = true;
				}
			}
		}

        private String _SupplierCatNum;
        public readonly bool _SupplierCatNum_PKFlag = false;
        public readonly bool _SupplierCatNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditSupplierCatNum = false;
		public String SupplierCatNum
		{
			get
			{
				return _SupplierCatNum;
			}
			set
			{
				if (_SupplierCatNum != value)
                {
					_SupplierCatNum = value;
					EditSupplierCatNum = true;
				}
			}
		}

        private Nullable<Decimal> _Surpluses;
        public readonly bool _Surpluses_PKFlag = false;
        public readonly bool _Surpluses_IDTFlag = false;
		[XmlIgnore]
		public bool EditSurpluses = false;
		public Nullable<Decimal> Surpluses
		{
			get
			{
				return _Surpluses;
			}
			set
			{
				if (_Surpluses != value)
                {
					_Surpluses = value;
					EditSurpluses = true;
				}
			}
		}

        private String _SWW;
        public readonly bool _SWW_PKFlag = false;
        public readonly bool _SWW_IDTFlag = false;
		[XmlIgnore]
		public bool EditSWW = false;
		public String SWW
		{
			get
			{
				return _SWW;
			}
			set
			{
				if (_SWW != value)
                {
					_SWW = value;
					EditSWW = true;
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

        private String _TaxLiable;
        public readonly bool _TaxLiable_PKFlag = false;
        public readonly bool _TaxLiable_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxLiable = false;
		public String TaxLiable
		{
			get
			{
				return _TaxLiable;
			}
			set
			{
				if (_TaxLiable != value)
                {
					_TaxLiable = value;
					EditTaxLiable = true;
				}
			}
		}

        private String _TaxOnly;
        public readonly bool _TaxOnly_PKFlag = false;
        public readonly bool _TaxOnly_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxOnly = false;
		public String TaxOnly
		{
			get
			{
				return _TaxOnly;
			}
			set
			{
				if (_TaxOnly != value)
                {
					_TaxOnly = value;
					EditTaxOnly = true;
				}
			}
		}

        private Nullable<Decimal> _TaxPercentagePerRow;
        public readonly bool _TaxPercentagePerRow_PKFlag = false;
        public readonly bool _TaxPercentagePerRow_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxPercentagePerRow = false;
		public Nullable<Decimal> TaxPercentagePerRow
		{
			get
			{
				return _TaxPercentagePerRow;
			}
			set
			{
				if (_TaxPercentagePerRow != value)
                {
					_TaxPercentagePerRow = value;
					EditTaxPercentagePerRow = true;
				}
			}
		}

        private Nullable<Decimal> _TaxTotal;
        public readonly bool _TaxTotal_PKFlag = false;
        public readonly bool _TaxTotal_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxTotal = false;
		public Nullable<Decimal> TaxTotal
		{
			get
			{
				return _TaxTotal;
			}
			set
			{
				if (_TaxTotal != value)
                {
					_TaxTotal = value;
					EditTaxTotal = true;
				}
			}
		}

        private String _TaxType;
        public readonly bool _TaxType_PKFlag = false;
        public readonly bool _TaxType_IDTFlag = false;
		[XmlIgnore]
		public bool EditTaxType = false;
		public String TaxType
		{
			get
			{
				return _TaxType;
			}
			set
			{
				if (_TaxType != value)
                {
					_TaxType = value;
					EditTaxType = true;
				}
			}
		}

        private String _ThirdParty;
        public readonly bool _ThirdParty_PKFlag = false;
        public readonly bool _ThirdParty_IDTFlag = false;
		[XmlIgnore]
		public bool EditThirdParty = false;
		public String ThirdParty
		{
			get
			{
				return _ThirdParty;
			}
			set
			{
				if (_ThirdParty != value)
                {
					_ThirdParty = value;
					EditThirdParty = true;
				}
			}
		}

        private String _TransactionType;
        public readonly bool _TransactionType_PKFlag = false;
        public readonly bool _TransactionType_IDTFlag = false;
		[XmlIgnore]
		public bool EditTransactionType = false;
		public String TransactionType
		{
			get
			{
				return _TransactionType;
			}
			set
			{
				if (_TransactionType != value)
                {
					_TransactionType = value;
					EditTransactionType = true;
				}
			}
		}

        private Nullable<Int32> _TransportMode;
        public readonly bool _TransportMode_PKFlag = false;
        public readonly bool _TransportMode_IDTFlag = false;
		[XmlIgnore]
		public bool EditTransportMode = false;
		public Nullable<Int32> TransportMode
		{
			get
			{
				return _TransportMode;
			}
			set
			{
				if (_TransportMode != value)
                {
					_TransportMode = value;
					EditTransportMode = true;
				}
			}
		}

        private Nullable<Decimal> _UnitPrice;
        public readonly bool _UnitPrice_PKFlag = false;
        public readonly bool _UnitPrice_IDTFlag = false;
		[XmlIgnore]
		public bool EditUnitPrice = false;
		public Nullable<Decimal> UnitPrice
		{
			get
			{
				return _UnitPrice;
			}
			set
			{
				if (_UnitPrice != value)
                {
					_UnitPrice = value;
					EditUnitPrice = true;
				}
			}
		}

        private Nullable<Decimal> _UnitsOfMeasurment;
        public readonly bool _UnitsOfMeasurment_PKFlag = false;
        public readonly bool _UnitsOfMeasurment_IDTFlag = false;
		[XmlIgnore]
		public bool EditUnitsOfMeasurment = false;
		public Nullable<Decimal> UnitsOfMeasurment
		{
			get
			{
				return _UnitsOfMeasurment;
			}
			set
			{
				if (_UnitsOfMeasurment != value)
                {
					_UnitsOfMeasurment = value;
					EditUnitsOfMeasurment = true;
				}
			}
		}

        private Nullable<Int32> _UoMEntry;
        public readonly bool _UoMEntry_PKFlag = false;
        public readonly bool _UoMEntry_IDTFlag = false;
		[XmlIgnore]
		public bool EditUoMEntry = false;
		public Nullable<Int32> UoMEntry
		{
			get
			{
				return _UoMEntry;
			}
			set
			{
				if (_UoMEntry != value)
                {
					_UoMEntry = value;
					EditUoMEntry = true;
				}
			}
		}

        private String _Usage;
        public readonly bool _Usage_PKFlag = false;
        public readonly bool _Usage_IDTFlag = false;
		[XmlIgnore]
		public bool EditUsage = false;
		public String Usage
		{
			get
			{
				return _Usage;
			}
			set
			{
				if (_Usage != value)
                {
					_Usage = value;
					EditUsage = true;
				}
			}
		}

        private String _UseBaseUnits;
        public readonly bool _UseBaseUnits_PKFlag = false;
        public readonly bool _UseBaseUnits_IDTFlag = false;
		[XmlIgnore]
		public bool EditUseBaseUnits = false;
		public String UseBaseUnits
		{
			get
			{
				return _UseBaseUnits;
			}
			set
			{
				if (_UseBaseUnits != value)
                {
					_UseBaseUnits = value;
					EditUseBaseUnits = true;
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

        private String _VendorNum;
        public readonly bool _VendorNum_PKFlag = false;
        public readonly bool _VendorNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditVendorNum = false;
		public String VendorNum
		{
			get
			{
				return _VendorNum;
			}
			set
			{
				if (_VendorNum != value)
                {
					_VendorNum = value;
					EditVendorNum = true;
				}
			}
		}

        private Nullable<Decimal> _Volume;
        public readonly bool _Volume_PKFlag = false;
        public readonly bool _Volume_IDTFlag = false;
		[XmlIgnore]
		public bool EditVolume = false;
		public Nullable<Decimal> Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (_Volume != value)
                {
					_Volume = value;
					EditVolume = true;
				}
			}
		}

        private Nullable<Int32> _VolumeUnit;
        public readonly bool _VolumeUnit_PKFlag = false;
        public readonly bool _VolumeUnit_IDTFlag = false;
		[XmlIgnore]
		public bool EditVolumeUnit = false;
		public Nullable<Int32> VolumeUnit
		{
			get
			{
				return _VolumeUnit;
			}
			set
			{
				if (_VolumeUnit != value)
                {
					_VolumeUnit = value;
					EditVolumeUnit = true;
				}
			}
		}

        private String _WarehouseCode;
        public readonly bool _WarehouseCode_PKFlag = false;
        public readonly bool _WarehouseCode_IDTFlag = false;
		[XmlIgnore]
		public bool EditWarehouseCode = false;
		public String WarehouseCode
		{
			get
			{
				return _WarehouseCode;
			}
			set
			{
				if (_WarehouseCode != value)
                {
					_WarehouseCode = value;
					EditWarehouseCode = true;
				}
			}
		}

        private Nullable<Decimal> _Weight1;
        public readonly bool _Weight1_PKFlag = false;
        public readonly bool _Weight1_IDTFlag = false;
		[XmlIgnore]
		public bool EditWeight1 = false;
		public Nullable<Decimal> Weight1
		{
			get
			{
				return _Weight1;
			}
			set
			{
				if (_Weight1 != value)
                {
					_Weight1 = value;
					EditWeight1 = true;
				}
			}
		}

        private Nullable<Int32> _Weight1Unit;
        public readonly bool _Weight1Unit_PKFlag = false;
        public readonly bool _Weight1Unit_IDTFlag = false;
		[XmlIgnore]
		public bool EditWeight1Unit = false;
		public Nullable<Int32> Weight1Unit
		{
			get
			{
				return _Weight1Unit;
			}
			set
			{
				if (_Weight1Unit != value)
                {
					_Weight1Unit = value;
					EditWeight1Unit = true;
				}
			}
		}

        private Nullable<Decimal> _Weight2;
        public readonly bool _Weight2_PKFlag = false;
        public readonly bool _Weight2_IDTFlag = false;
		[XmlIgnore]
		public bool EditWeight2 = false;
		public Nullable<Decimal> Weight2
		{
			get
			{
				return _Weight2;
			}
			set
			{
				if (_Weight2 != value)
                {
					_Weight2 = value;
					EditWeight2 = true;
				}
			}
		}

        private Nullable<Int32> _Weight2Unit;
        public readonly bool _Weight2Unit_PKFlag = false;
        public readonly bool _Weight2Unit_IDTFlag = false;
		[XmlIgnore]
		public bool EditWeight2Unit = false;
		public Nullable<Int32> Weight2Unit
		{
			get
			{
				return _Weight2Unit;
			}
			set
			{
				if (_Weight2Unit != value)
                {
					_Weight2Unit = value;
					EditWeight2Unit = true;
				}
			}
		}

        private Nullable<Decimal> _Width1;
        public readonly bool _Width1_PKFlag = false;
        public readonly bool _Width1_IDTFlag = false;
		[XmlIgnore]
		public bool EditWidth1 = false;
		public Nullable<Decimal> Width1
		{
			get
			{
				return _Width1;
			}
			set
			{
				if (_Width1 != value)
                {
					_Width1 = value;
					EditWidth1 = true;
				}
			}
		}

        private Nullable<Int32> _Width1Unit;
        public readonly bool _Width1Unit_PKFlag = false;
        public readonly bool _Width1Unit_IDTFlag = false;
		[XmlIgnore]
		public bool EditWidth1Unit = false;
		public Nullable<Int32> Width1Unit
		{
			get
			{
				return _Width1Unit;
			}
			set
			{
				if (_Width1Unit != value)
                {
					_Width1Unit = value;
					EditWidth1Unit = true;
				}
			}
		}

        private Nullable<Decimal> _Width2;
        public readonly bool _Width2_PKFlag = false;
        public readonly bool _Width2_IDTFlag = false;
		[XmlIgnore]
		public bool EditWidth2 = false;
		public Nullable<Decimal> Width2
		{
			get
			{
				return _Width2;
			}
			set
			{
				if (_Width2 != value)
                {
					_Width2 = value;
					EditWidth2 = true;
				}
			}
		}

        private Nullable<Int32> _Width2Unit;
        public readonly bool _Width2Unit_PKFlag = false;
        public readonly bool _Width2Unit_IDTFlag = false;
		[XmlIgnore]
		public bool EditWidth2Unit = false;
		public Nullable<Int32> Width2Unit
		{
			get
			{
				return _Width2Unit;
			}
			set
			{
				if (_Width2Unit != value)
                {
					_Width2Unit = value;
					EditWidth2Unit = true;
				}
			}
		}

        private String _WithoutInventoryMovement;
        public readonly bool _WithoutInventoryMovement_PKFlag = false;
        public readonly bool _WithoutInventoryMovement_IDTFlag = false;
		[XmlIgnore]
		public bool EditWithoutInventoryMovement = false;
		public String WithoutInventoryMovement
		{
			get
			{
				return _WithoutInventoryMovement;
			}
			set
			{
				if (_WithoutInventoryMovement != value)
                {
					_WithoutInventoryMovement = value;
					EditWithoutInventoryMovement = true;
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

	}
	public partial class Document_Lines_Command
	{
		string TableName = "Document_Lines";

		Document_Lines _Document_Lines = null;
		DBHelper _DBHelper = null;

		internal Document_Lines_Command(Document_Lines obj_Document_Lines)
		{
			this._Document_Lines = obj_Document_Lines;
			this._DBHelper = new DBHelper();
		}

		internal Document_Lines_Command(string ConnectionStr, Document_Lines obj_Document_Lines)
		{
			this._Document_Lines = obj_Document_Lines;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _Document_Lines.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Document_Lines.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_Document_Lines, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_Document_Lines, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_Document_LinesID_Value)
        {
            Load(_DBHelper, L_Document_LinesID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_Document_LinesID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Document_LinesID", L_Document_LinesID_Value, (DbType)Enum.Parse(typeof(DbType), L_Document_LinesID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Document_Lines WHERE L_Document_LinesID=@L_Document_LinesID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_Document_LinesID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Document_LinesID", L_Document_LinesID_Value, (DbType)Enum.Parse(typeof(DbType), L_Document_LinesID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Document_Lines WHERE L_Document_LinesID=@L_Document_LinesID", "Document_Lines", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("Document_Lines");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<Document_Lines> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<Document_Lines> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("Document_Lines");
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
            List<Document_Lines> Res = new List<Document_Lines>();
            foreach (DataRow Dr in dt.Rows)
            {
                Document_Lines Item = new Document_Lines();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Document_Lines.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Document_Lines.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_Document_Lines);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Document_Lines.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Document_Lines.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_Document_Lines);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _Document_Lines.GetType();
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

                foreach (PropertyInfo ProInfo in _Document_Lines.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _Document_Lines.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_Document_Lines))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_Document_Lines, null);
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
            Type MyType = _Document_Lines.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Document_Lines.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Document_Lines.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_Document_Lines) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Document_Lines, null);
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
            Type MyType = _Document_Lines.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Document_Lines.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Document_Lines.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_Document_Lines) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Document_Lines, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_Document_Lines, null)));
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
            string ssql = "DELETE Document_Lines WHERE L_Document_LinesID=@L_Document_LinesID";
            Parameter.Add(new DBParameter("@L_Document_LinesID", _Document_Lines.L_Document_LinesID));

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
