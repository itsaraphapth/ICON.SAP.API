using ICON.Interface;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICON.SAP.API
{
    public class SAPB1
    {
        #region --- AR ---
        public static SAPbobsCOM.Company oCompany;
        public static int lRetCode;
        public string DBServer;
        public SAPbobsCOM.BoDataServerTypes DBServerType;
        public string CompanyDB;
        public string UserName;
        public string Password;
        public string LicenseServer;
        public string SLDAddress;
        public SAPbobsCOM.BoSuppLangs Language;
        public SAPTABLE SAPTABLE;

        public SAPB1(
            string DBServer,
            SAPbobsCOM.BoDataServerTypes DBServerType,
            string CompanyDB,
            string UserName,
            string Password,
            string LicenseServer,
            string SLDAddress,
            SAPbobsCOM.BoSuppLangs Language
        )
        {
            this.DBServer = DBServer;
            this.DBServerType = DBServerType;
            this.CompanyDB = CompanyDB;
            this.UserName = UserName;
            this.Password = Password;
            this.LicenseServer = LicenseServer;
            this.SLDAddress = SLDAddress;
            this.Language = Language;
        }

        public int ConnectCompanyDB()
        {
            if (oCompany == null)
            {
                oCompany = new SAPbobsCOM.Company();
            }

            oCompany.Server = DBServer;
            oCompany.DbServerType = DBServerType;
            oCompany.CompanyDB = CompanyDB;
            oCompany.UserName = UserName;
            oCompany.Password = Password;
            if (String.IsNullOrEmpty(SLDAddress))
                oCompany.LicenseServer = LicenseServer;
            else
                oCompany.SLDServer = SLDAddress;

            oCompany.language = Language;

            lRetCode = oCompany.Connect();

            if (lRetCode != 0)
            {
                throw new Exception("Connect fail " + oCompany.GetLastErrorDescription());
            }

            return lRetCode;
        }

        public void DisConnectCompanyDB()
        {
            try
            {
                if (oCompany != null)
                {
                    oCompany.Disconnect();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Connect fail " + ex.Message);
            }
        }

        #region --- Invoice ---
        public void CreateDocument(Document Doc, out string DocEntry, out string DocNum, out string GLDocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            SAPbobsCOM.Documents oDoc = null;
            this.ConnectCompanyDB();
            oCompany.StartTransaction();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string DocTransId = string.Empty;

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));

                oDoc = oCompany.GetBusinessObject(Doc.DocType);
                oDoc.CardCode = Doc.CardCode;
                oDoc.DocDate = Doc.PostingDate;
                oDoc.DocDueDate = Doc.DocDueDate;
                oDoc.TaxDate = Doc.DocumentDate;
                oDoc.UserFields.Fields.Item("U_ICON_RefNo").Value = Doc.UDF_RefNo;
                oDoc.UserFields.Fields.Item("U_ICON_CustName").Value = Doc.UDF_CustName;
                oDoc.UserFields.Fields.Item("U_ICON_UnitRef").Value = Doc.UDF_UnitRef;
                oDoc.UserFields.Fields.Item("U_ICON_Project").Value = Doc.UDF_Project;
                oDoc.UserFields.Fields.Item("U_ICON_ProjectName").Value = Doc.UDF_ProjectName;
                oDoc.UserFields.Fields.Item("U_ICON_CID").Value = Doc.UDF_TaxID;
                oDoc.UserFields.Fields.Item("U_ICON_Add").Value = Doc.UDF_Address;
                oDoc.UserFields.Fields.Item("U_VATPeriod").Value = Doc.DocumentDate.ToString("yyyy-MM");

                if (!String.IsNullOrEmpty(Doc.UDF_InvNo))
                {
                    oDoc.UserFields.Fields.Item("U_ICON_InvNo").Value = Doc.UDF_InvNo;
                }

                oDoc.DeferredTax = Doc.IsDeferVAT ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO;


                oDoc.Comments = Doc.Remark;

                bool firstLine = true;

                // [TIPS] - Caching Variable
                SAPbobsCOM.Document_Lines oLines = oDoc.Lines;
                // Details (Document Lines)
                foreach (DocumentLine docLine in Doc.Lines)
                {
                    if (firstLine)
                        firstLine = false;
                    else
                        oLines.Add();


                    oLines.ItemCode = docLine.ItemCode;
                    oLines.ItemDescription = docLine.ItemDescription;
                    oLines.Quantity = docLine.Qty;
                    if (!String.IsNullOrEmpty(docLine.UnitPrice))
                    {
                        oLines.UnitPrice = Convert.ToDouble(docLine.UnitPrice);
                    }
                    oLines.PriceAfterVAT = docLine.UnitAfterPrice;
                    oLines.ProjectCode = Doc.UDF_Project;

                    if (!String.IsNullOrEmpty(docLine.TaxCode))
                    {
                        oLines.VatGroup = docLine.TaxCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.BaseEntry))
                    {
                        oLines.BaseType = docLine.BaseType;
                        oLines.BaseEntry = Convert.ToInt32(docLine.BaseEntry);
                    }
                    if (!String.IsNullOrEmpty(docLine.LineNumber))
                    {
                        oLines.BaseLine = Convert.ToInt32(docLine.LineNumber);
                    }

                }

                lRetCode = oDoc.Add();

                if (lRetCode != 0)
                {
                    throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                }

                oCompany.GetNewObjectCode(out DocEntry);
                DocNum = GetDocNum(DocEntry);
                DocTransId = GetDocTransId(DocEntry);
                GLDocNum = GetGLDocNum(DocTransId);

                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = DocEntry;
                Log_Detail.SAPRefNo = DocNum;
                Log_Detail.SAPRefGLNo = GLDocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public void CancelDocument(Document Doc, out string TranID, out string DocNum, out string GLDocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            SAPbobsCOM.Documents oDoc = null;
            try
            {
                this.ConnectCompanyDB();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("connected to a company"))
                {
                    this.DisConnectCompanyDB();
                    this.ConnectCompanyDB();
                }

            }
            oCompany.StartTransaction();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string DocTransId = string.Empty;

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));

                oDoc = oCompany.GetBusinessObject(Doc.DocType);
                oDoc.GetByKey(Doc.DocEntry);
                oDoc.Cancel();

                SAPbobsCOM.Documents cancelDoc = oDoc.CreateCancellationDocument();
                cancelDoc.DocDate = Doc.DocumentDate;

                lRetCode = cancelDoc.Add();

                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);
                    throw new Exception($"SAP Error: Code {InternalErrorCode} {InternalErrorMessage}");
                }

                oCompany.GetNewObjectCode(out TranID);
                DocNum = GetDocNum(TranID);
                DocTransId = GetDocTransId(TranID);
                GLDocNum = GetGLDocNum(DocTransId);

                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = TranID;
                Log_Detail.SAPRefNo = DocNum;
                Log_Detail.SAPRefGLNo = GLDocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message + " " + ex.StackTrace;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }
        #endregion

        #region --- JournalEntry ---
        public void CreateJournalEntry(ICON.SAP.API.SAPB1.Document Doc, out string TranID, out string DocNum, out string GLDocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            SAPbobsCOM.JournalEntries oJE = null;
            this.ConnectCompanyDB();
            oCompany.StartTransaction();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                // Create Journal Entry
                oJE = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(Doc.DocType);
                oJE.ReferenceDate = Doc.PostingDate;
                oJE.DueDate = Doc.DocDueDate;
                oJE.TaxDate = Doc.DocumentDate;
                oJE.Memo = Doc.Remark;
                oJE.Reference = Doc.Ref1;
                oJE.Reference2 = Doc.Ref2;
                oJE.Reference3 = Doc.Ref3;
                oJE.UserFields.Fields.Item("U_ICON_Ref").Value = Doc.UDF_Ref;

                bool firstLine = true;

                foreach (var line in Doc.Lines)
                {
                    SAPbobsCOM.JournalEntries_Lines oLines = oJE.Lines;

                    if (firstLine)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        oLines.Add();
                    }

                    oLines.Reference1 = line.ref1;
                    oLines.Reference2 = line.ref2;
                    oLines.ProjectCode = line.ProjectCode;
                    oLines.LineMemo = line.LineMemo;
                    oLines.ReferenceDate1 = Doc.PostingDate;
                    oLines.ShortName = line.AccountCode;
                    oLines.AccountCode = line.AccountCode;
                    oLines.Debit = line.Debit;
                    oLines.Credit = line.Credit;
                    oLines.AdditionalReference = line.RefID;
                }

                lRetCode = oJE.Add();

                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);
                    throw new Exception($"SAP Error: Code {InternalErrorCode} {InternalErrorMessage}");
                }

                oCompany.GetNewObjectCode(out TranID);
                DocNum = GetGLDocNum(TranID);
                GLDocNum = DocNum;

                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = TranID;
                Log_Detail.SAPRefNo = DocNum;
                Log_Detail.SAPRefGLNo = GLDocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }
        #endregion

        #region --- Payment ---
        public void CreatePayment(List<Dictionary<string, object>> PaymentDetails, Payments payment, Payments payment_Other
            , out string paymentEntry, out string DocNum, out string GLDocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string TransId = string.Empty;
            SAPbobsCOM.Payments oPayment = null;
            SAPbobsCOM.Payments oPayment_Over = null;
            SAPbobsCOM.Documents oInvoice = null;
            this.ConnectCompanyDB();
            oCompany.StartTransaction();

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                SAPTABLE = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oIncomingPayments));

                if (payment_Other.SumPaid > 0)
                {
                    try
                    {
                        oPayment_Over = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                        oPayment_Over.DocType = SAPbobsCOM.BoRcptTypes.rAccount; //******
                        oPayment_Over.UserFields.Fields.Item("U_ICON_RecpRefNo").Value = payment_Other.UDF_RecpRefNo;
                        oPayment_Over.UserFields.Fields.Item("U_ICON_CustName").Value = payment_Other.UDF_CustName;
                        oPayment_Over.UserFields.Fields.Item("U_ICON_UnitRef").Value = payment_Other.UDF_UnitRef;
                        oPayment_Over.UserFields.Fields.Item("U_ICON_Project").Value = payment_Other.UDF_Project;
                        oPayment_Over.UserFields.Fields.Item("U_ICON_ProjectName").Value = payment_Other.UDF_ProjectName;

                        oPayment_Over.AccountPayments.AccountCode = payment_Other.AccountCode;
                        oPayment_Over.AccountPayments.SumPaid = payment_Other.SumPaid;

                        //oPayment_Over.CardCode = payment_Other.CardCode; ห้ามใส่
                        oPayment_Over.DocDate = payment_Other.ReceiptDate;
                        oPayment_Over.DueDate = payment_Other.ReceiptDate;
                        oPayment_Over.TaxDate = payment_Other.ReceiptDate;
                        oPayment_Over.ProjectCode = payment_Other.UDF_Project;

                        // CA
                        if (payment_Other.CashAmount > 0)
                        {
                            oPayment_Over.CashSum = payment_Other.CashAmount;
                            oPayment_Over.CashAccount = payment_Other.CashAccount;
                        }

                        // CR
                        if (payment_Other.Credit.Count > 0)
                        {
                            bool firstLine = true;

                            foreach (CreditCard CR_Item in payment_Other.Credit)
                            {
                                if (firstLine)
                                    firstLine = false;
                                else
                                    oPayment_Over.CreditCards.Add();

                                oPayment_Over.CreditCards.AdditionalPaymentSum = 0;
                                oPayment_Over.CreditCards.CreditSum = CR_Item.Amount;
                                oPayment_Over.CreditCards.CreditCard = 1;
                                oPayment_Over.CreditCards.VoucherNum = CR_Item.PayDetailID;
                                //oPayment_Over.CreditCards.PaymentMethodCode = 1;
                                oPayment_Over.CreditCards.CreditCardNumber = CR_Item.CardNumber;
                                oPayment_Over.CreditCards.CreditType = CR_Item.CreditType;
                                oPayment_Over.CreditCards.CreditAcct = CR_Item.Account;
                                oPayment_Over.CreditCards.CardValidUntil = CR_Item.ExpireDate;
                                oPayment_Over.CreditCards.UserFields.Fields.Item("U_WHTType").Value = CR_Item.WHTType;
                                oPayment_Over.CreditCards.UserFields.Fields.Item("U_WHT_Percent").Value = CR_Item.WHTPercent;
                                oPayment_Over.CreditCards.UserFields.Fields.Item("U_WHTBaseAmount").Value = CR_Item.WHTAmount;
                            }
                        }

                        // TR
                        if (!string.IsNullOrEmpty(payment_Other.TransferAccount))
                        {
                            oPayment_Over.TransferSum = payment_Other.TransferAmount;
                            oPayment_Over.TransferAccount = payment_Other.TransferAccount;
                            oPayment_Over.TransferDate = payment_Other.TransferDate;
                        }

                        // CQ
                        if (payment_Other.Cheque.Count > 0)
                        {
                            bool firstLine = true;

                            foreach (Cheque CQ_Item in payment_Other.Cheque)
                            {
                                if (firstLine)
                                    firstLine = false;
                                else
                                    oPayment_Over.Checks.Add();

                                oPayment_Over.Checks.CheckSum = CQ_Item.Amount;
                                oPayment_Over.Checks.CheckNumber = CQ_Item.Number;
                                oPayment_Over.Checks.AccounttNum = CQ_Item.Account;
                                oPayment_Over.Checks.DueDate = CQ_Item.Date;
                            }
                        }

                        lRetCode = oPayment_Over.Add();

                        if (lRetCode != 0)
                        {
                            throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                        }

                        oCompany.GetNewObjectCode(out paymentEntry);
                        DocNum = GetDocNum(paymentEntry);
                        TransId = GetDocTransId(paymentEntry);
                        GLDocNum = GetGLDocNum(TransId);

                        SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                        Log_Detail.SAPRefID = paymentEntry;
                        Log_Detail.SAPRefNo = DocNum;
                        Log_Detail.SAPRefGLNo = GLDocNum;
                        LogDetail.Add(Log_Detail);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Payment_Over : " + ex.Message);
                    }
                }

                try
                {
                    oPayment = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                    oPayment.DocType = SAPbobsCOM.BoRcptTypes.rCustomer;
                    oPayment.UserFields.Fields.Item("U_ICON_RecpRefNo").Value = payment.UDF_RecpRefNo;
                    oPayment.UserFields.Fields.Item("U_ICON_CustName").Value = payment.UDF_CustName;

                    bool firstLineINV = true;

                    List<int> INV_DocEntry = new List<int>();
                    foreach (Dictionary<string, object> item in PaymentDetails)
                    {
                        int docEntry = 0;
                        try
                        {
                            docEntry = Convert.ToInt32(item["DocEntry"]);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("DocEntry : " + ex.Message);
                        }

                        if (!INV_DocEntry.Contains(docEntry))
                        {
                            INV_DocEntry.Add(docEntry);
                        }
                    }

                    foreach (int docEntry in INV_DocEntry)
                    {
                        List<Dictionary<string, object>> INV_PaymentDetails = PaymentDetails.FindAll(x => Convert.ToInt32(x["DocEntry"]) == docEntry);
                        oInvoice = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                        oInvoice.GetByKey(docEntry);

                        if (firstLineINV)
                        {
                            firstLineINV = false;

                            oPayment.CardCode = oInvoice.CardCode;
                            oPayment.DocDate = payment.ReceiptDate;
                            oPayment.DueDate = payment.ReceiptDate;
                            oPayment.TaxDate = payment.ReceiptDate;
                            oPayment.ProjectCode = oInvoice.UserFields.Fields.Item("U_ICON_Project").Value;
                            oPayment.UserFields.Fields.Item("U_ICON_UnitRef").Value = oInvoice.UserFields.Fields.Item("U_ICON_UnitRef").Value;
                            oPayment.UserFields.Fields.Item("U_ICON_Project").Value = oInvoice.UserFields.Fields.Item("U_ICON_Project").Value;
                            oPayment.UserFields.Fields.Item("U_ICON_ProjectName").Value = oInvoice.UserFields.Fields.Item("U_ICON_ProjectName").Value;
                        }
                        else
                        {
                            oPayment.Invoices.Add();
                        }

                        oPayment.Invoices.InvoiceType = SAPbobsCOM.BoRcptInvTypes.it_Invoice;
                        oPayment.Invoices.DocEntry = docEntry;
                        oPayment.Invoices.SumApplied = INV_PaymentDetails.Sum(x => Convert.ToDouble(x["Amount"]) + Convert.ToDouble(x["WHTAmount"]));
                    }

                    // CA
                    if (payment.CashAmount > 0)
                    {
                        oPayment.CashSum = payment.CashAmount;
                        oPayment.CashAccount = payment.CashAccount;
                    }

                    // CR
                    if (payment.Credit.Count > 0)
                    {
                        bool firstLine = true;

                        foreach (CreditCard CR_Item in payment.Credit)
                        {
                            if (firstLine)
                                firstLine = false;
                            else
                                oPayment.CreditCards.Add();

                            oPayment.CreditCards.AdditionalPaymentSum = 0;
                            oPayment.CreditCards.CreditSum = CR_Item.Amount;
                            oPayment.CreditCards.CreditCard = 1;
                            oPayment.CreditCards.VoucherNum = CR_Item.PayDetailID;
                            //oPayment.CreditCards.PaymentMethodCode = 1;
                            oPayment.CreditCards.CreditCardNumber = CR_Item.CardNumber;
                            oPayment.CreditCards.CreditType = CR_Item.CreditType;
                            oPayment.CreditCards.CreditAcct = CR_Item.Account;
                            oPayment.CreditCards.CardValidUntil = CR_Item.ExpireDate;
                            oPayment.CreditCards.UserFields.Fields.Item("U_WHTType").Value = CR_Item.WHTType;
                            oPayment.CreditCards.UserFields.Fields.Item("U_WHT_Percent").Value = CR_Item.WHTPercent;
                            oPayment.CreditCards.UserFields.Fields.Item("U_WHTBaseAmount").Value = CR_Item.WHTAmount;
                        }
                    }

                    // TR
                    if (!string.IsNullOrEmpty(payment.TransferAccount))
                    {
                        oPayment.TransferSum = payment.TransferAmount;
                        oPayment.TransferAccount = payment.TransferAccount;
                        oPayment.TransferDate = payment.TransferDate;
                    }

                    // CQ
                    if (payment.Cheque.Count > 0)
                    {
                        bool firstLine = true;

                        foreach (Cheque CQ_Item in payment.Cheque)
                        {
                            if (firstLine)
                                firstLine = false;
                            else
                                oPayment.Checks.Add();

                            oPayment.Checks.CheckSum = CQ_Item.Amount;
                            oPayment.Checks.CheckNumber = CQ_Item.Number;
                            oPayment.Checks.AccounttNum = CQ_Item.Account;
                            oPayment.Checks.DueDate = CQ_Item.Date;
                        }
                    }

                    lRetCode = oPayment.Add();

                    if (lRetCode != 0)
                    {
                        throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                    }

                    oCompany.GetNewObjectCode(out paymentEntry);
                    DocNum = GetDocNum(paymentEntry);
                    TransId = GetDocTransId(paymentEntry);
                    GLDocNum = GetGLDocNum(TransId);

                    SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                    Log_Detail.SAPRefID = paymentEntry;
                    Log_Detail.SAPRefNo = DocNum;
                    Log_Detail.SAPRefGLNo = GLDocNum;
                    LogDetail.Add(Log_Detail);
                }
                catch (Exception ex)
                {
                    throw new Exception("Payment : " + ex.Message);
                }

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }
        public void CreatePaymentBeforeInvoice(List<Dictionary<string, object>> PaymentDetails, Payments payment, Payments payment_Other
            , out string paymentEntry, out string DocNum, out string GLDocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string TransId = string.Empty;
            SAPbobsCOM.Payments oPayment_Before = null;
            SAPbobsCOM.Payments oPayment_Over = null;
            SAPbobsCOM.Payments oPayment = null;
            SAPbobsCOM.Documents oInvoice = null;
            this.ConnectCompanyDB();
            oCompany.StartTransaction();

            try
            {
                SAPTABLE = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oIncomingPayments));
                LogDetail = new List<SAP_Interface_Log_Detail>();
                try
                {
                    oPayment_Before = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                    oPayment_Before.DocType = SAPbobsCOM.BoRcptTypes.rAccount; //******
                    oPayment_Before.UserFields.Fields.Item("U_ICON_RecpRefNo").Value = payment.UDF_RecpRefNo;
                    oPayment_Before.UserFields.Fields.Item("U_ICON_CustName").Value = payment.UDF_CustName;
                    oPayment_Before.UserFields.Fields.Item("U_ICON_UnitRef").Value = payment.UDF_UnitRef;
                    oPayment_Before.UserFields.Fields.Item("U_ICON_Project").Value = payment.UDF_Project;
                    oPayment_Before.UserFields.Fields.Item("U_ICON_ProjectName").Value = payment.UDF_ProjectName;

                    oPayment_Before.AccountPayments.AccountCode = payment.AccountCode;
                    oPayment_Before.AccountPayments.SumPaid = payment.SumPaid;

                    //oPayment_Before.CardCode = payment.CardCode; ห้ามใส่
                    oPayment_Before.DocDate = payment.ReceiptDate;
                    oPayment_Before.DueDate = payment.ReceiptDate;
                    oPayment_Before.TaxDate = payment.ReceiptDate;
                    oPayment_Before.ProjectCode = payment.UDF_Project;

                    // CA
                    if (payment.CashAmount > 0)
                    {
                        oPayment_Before.CashSum = payment.CashAmount;
                        oPayment_Before.CashAccount = payment.CashAccount;
                    }

                    // CR
                    if (payment.Credit.Count > 0)
                    {
                        bool firstLine = true;

                        foreach (CreditCard CR_Item in payment.Credit)
                        {
                            if (firstLine)
                                firstLine = false;
                            else
                                oPayment_Before.CreditCards.Add();

                            oPayment_Before.CreditCards.AdditionalPaymentSum = 0;
                            oPayment_Before.CreditCards.CreditSum = CR_Item.Amount;
                            oPayment_Before.CreditCards.CreditCard = 1;
                            oPayment_Before.CreditCards.VoucherNum = CR_Item.PayDetailID;
                            //oPayment_Before.CreditCards.PaymentMethodCode = 1;
                            oPayment_Before.CreditCards.CreditCardNumber = CR_Item.CardNumber;
                            oPayment_Before.CreditCards.CreditType = CR_Item.CreditType;
                            oPayment_Before.CreditCards.CreditAcct = CR_Item.Account;
                            oPayment_Before.CreditCards.CardValidUntil = CR_Item.ExpireDate;
                            oPayment_Before.CreditCards.UserFields.Fields.Item("U_WHTType").Value = CR_Item.WHTType;
                            oPayment_Before.CreditCards.UserFields.Fields.Item("U_WHT_Percent").Value = CR_Item.WHTPercent;
                            oPayment_Before.CreditCards.UserFields.Fields.Item("U_WHTBaseAmount").Value = CR_Item.WHTAmount;
                        }
                    }

                    // TR
                    if (!string.IsNullOrEmpty(payment.TransferAccount))
                    {
                        oPayment_Before.TransferSum = payment.TransferAmount;
                        oPayment_Before.TransferAccount = payment.TransferAccount;
                        oPayment_Before.TransferDate = payment.TransferDate;
                    }

                    // CQ
                    if (payment.Cheque.Count > 0)
                    {
                        bool firstLine = true;

                        foreach (Cheque CQ_Item in payment.Cheque)
                        {
                            if (firstLine)
                                firstLine = false;
                            else
                                oPayment_Before.Checks.Add();

                            oPayment_Before.Checks.CheckSum = CQ_Item.Amount;
                            oPayment_Before.Checks.CheckNumber = CQ_Item.Number;
                            oPayment_Before.Checks.AccounttNum = CQ_Item.Account;
                            oPayment_Before.Checks.DueDate = CQ_Item.Date;
                        }
                    }

                    lRetCode = oPayment_Before.Add();

                    if (lRetCode != 0)
                    {
                        throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                    }

                    oCompany.GetNewObjectCode(out paymentEntry);
                    DocNum = GetDocNum(paymentEntry);
                    TransId = GetDocTransId(paymentEntry);
                    GLDocNum = GetGLDocNum(TransId);

                    SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                    Log_Detail.SAPRefID = paymentEntry;
                    Log_Detail.SAPRefNo = DocNum;
                    Log_Detail.SAPRefGLNo = GLDocNum;
                    LogDetail.Add(Log_Detail);
                }
                catch (Exception ex)
                {
                    throw new Exception("Payment_Before : " + ex.Message);
                }

                if (payment_Other.SumPaid > 0)
                {
                    try
                    {
                        oPayment_Over = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                        oPayment_Over.DocType = SAPbobsCOM.BoRcptTypes.rAccount; //******
                        oPayment_Over.UserFields.Fields.Item("U_ICON_RecpRefNo").Value = payment_Other.UDF_RecpRefNo;
                        oPayment_Over.UserFields.Fields.Item("U_ICON_CustName").Value = payment_Other.UDF_CustName;
                        oPayment_Over.UserFields.Fields.Item("U_ICON_UnitRef").Value = payment_Other.UDF_UnitRef;
                        oPayment_Over.UserFields.Fields.Item("U_ICON_Project").Value = payment_Other.UDF_Project;
                        oPayment_Over.UserFields.Fields.Item("U_ICON_ProjectName").Value = payment_Other.UDF_ProjectName;

                        oPayment_Over.AccountPayments.AccountCode = payment_Other.AccountCode;
                        oPayment_Over.AccountPayments.SumPaid = payment_Other.SumPaid;

                        //oPayment_Over.CardCode = payment_Other.CardCode; ห้ามใส่
                        oPayment_Over.DocDate = payment_Other.ReceiptDate;
                        oPayment_Over.DueDate = payment_Other.ReceiptDate;
                        oPayment_Over.TaxDate = payment_Other.ReceiptDate;
                        oPayment_Over.ProjectCode = payment_Other.UDF_Project;

                        // CA
                        if (payment_Other.CashAmount > 0)
                        {
                            oPayment_Over.CashSum = payment_Other.CashAmount;
                            oPayment_Over.CashAccount = payment_Other.CashAccount;
                        }

                        // CR
                        if (payment_Other.Credit.Count > 0)
                        {
                            bool firstLine = true;

                            foreach (CreditCard CR_Item in payment_Other.Credit)
                            {
                                if (firstLine)
                                    firstLine = false;
                                else
                                    oPayment_Over.CreditCards.Add();

                                oPayment_Over.CreditCards.AdditionalPaymentSum = 0;
                                oPayment_Over.CreditCards.CreditSum = CR_Item.Amount;
                                oPayment_Over.CreditCards.CreditCard = 1;
                                oPayment_Over.CreditCards.VoucherNum = CR_Item.PayDetailID;
                                //oPayment_Over.CreditCards.PaymentMethodCode = 1;
                                oPayment_Over.CreditCards.CreditCardNumber = CR_Item.CardNumber;
                                oPayment_Over.CreditCards.CreditType = CR_Item.CreditType;
                                oPayment_Over.CreditCards.CreditAcct = CR_Item.Account;
                                oPayment_Over.CreditCards.CardValidUntil = CR_Item.ExpireDate;
                                oPayment_Over.CreditCards.UserFields.Fields.Item("U_WHTType").Value = CR_Item.WHTType;
                                oPayment_Over.CreditCards.UserFields.Fields.Item("U_WHT_Percent").Value = CR_Item.WHTPercent;
                                oPayment_Over.CreditCards.UserFields.Fields.Item("U_WHTBaseAmount").Value = CR_Item.WHTAmount;
                            }
                        }

                        // TR
                        if (!string.IsNullOrEmpty(payment_Other.TransferAccount))
                        {
                            oPayment_Over.TransferSum = payment_Other.TransferAmount;
                            oPayment_Over.TransferAccount = payment_Other.TransferAccount;
                            oPayment_Over.TransferDate = payment_Other.TransferDate;
                        }

                        // CQ
                        if (payment_Other.Cheque.Count > 0)
                        {
                            bool firstLine = true;

                            foreach (Cheque CQ_Item in payment_Other.Cheque)
                            {
                                if (firstLine)
                                    firstLine = false;
                                else
                                    oPayment_Over.Checks.Add();

                                oPayment_Over.Checks.CheckSum = CQ_Item.Amount;
                                oPayment_Over.Checks.CheckNumber = CQ_Item.Number;
                                oPayment_Over.Checks.AccounttNum = CQ_Item.Account;
                                oPayment_Over.Checks.DueDate = CQ_Item.Date;
                            }
                        }

                        lRetCode = oPayment_Over.Add();

                        if (lRetCode != 0)
                        {
                            throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                        }

                        oCompany.GetNewObjectCode(out paymentEntry);
                        DocNum = GetDocNum(paymentEntry);
                        TransId = GetDocTransId(paymentEntry);
                        GLDocNum = GetGLDocNum(TransId);

                        SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                        Log_Detail.SAPRefID = paymentEntry;
                        Log_Detail.SAPRefNo = DocNum;
                        Log_Detail.SAPRefGLNo = GLDocNum;
                        LogDetail.Add(Log_Detail);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Payment_Over : " + ex.Message);
                    }
                }

                try
                {
                    oPayment = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                    oPayment.DocType = SAPbobsCOM.BoRcptTypes.rCustomer;
                    oPayment.UserFields.Fields.Item("U_ICON_RecpRefNo").Value = payment.UDF_RecpRefNo;
                    oPayment.UserFields.Fields.Item("U_ICON_CustName").Value = payment.UDF_CustName;

                    bool firstLineINV = true;

                    List<int> INV_DocEntry = new List<int>();
                    foreach (Dictionary<string, object> item in PaymentDetails)
                    {
                        int docEntry = 0;
                        try
                        {
                            docEntry = Convert.ToInt32(item["DocEntry"]);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("DocEntry : " + ex.Message);
                        }

                        if (!INV_DocEntry.Contains(docEntry))
                        {
                            INV_DocEntry.Add(docEntry);
                        }
                    }

                    foreach (int docEntry in INV_DocEntry)
                    {
                        List<Dictionary<string, object>> INV_PaymentDetails = PaymentDetails.FindAll(x => Convert.ToInt32(x["DocEntry"]) == docEntry);
                        oInvoice = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                        oInvoice.GetByKey(docEntry);

                        if (firstLineINV)
                        {
                            firstLineINV = false;

                            oPayment.CardCode = oInvoice.CardCode;
                            oPayment.DocDate = payment.TransecDate;
                            oPayment.DueDate = payment.TransecDate;
                            oPayment.TaxDate = payment.TransecDate;
                            oPayment.ProjectCode = oInvoice.UserFields.Fields.Item("U_ICON_Project").Value;
                            oPayment.UserFields.Fields.Item("U_ICON_UnitRef").Value = oInvoice.UserFields.Fields.Item("U_ICON_UnitRef").Value;
                            oPayment.UserFields.Fields.Item("U_ICON_Project").Value = oInvoice.UserFields.Fields.Item("U_ICON_Project").Value;
                            oPayment.UserFields.Fields.Item("U_ICON_ProjectName").Value = oInvoice.UserFields.Fields.Item("U_ICON_ProjectName").Value;
                        }
                        else
                        {
                            oPayment.Invoices.Add();
                        }

                        oPayment.Invoices.InvoiceType = SAPbobsCOM.BoRcptInvTypes.it_Invoice;
                        oPayment.Invoices.DocEntry = docEntry;
                        oPayment.Invoices.SumApplied = INV_PaymentDetails.Sum(x => Convert.ToDouble(x["Amount"]) + Convert.ToDouble(x["WHTAmount"]));
                    }


                    oPayment.CreditCards.AdditionalPaymentSum = 0;
                    oPayment.CreditCards.CreditSum = payment.SumPaid;
                    oPayment.CreditCards.CreditCard = 1;
                    oPayment.CreditCards.VoucherNum = "1";
                    //oPayment.CreditCards.PaymentMethodCode = 1;
                    oPayment.CreditCards.CreditCardNumber = "1234";
                    oPayment.CreditCards.CreditType = SAPbobsCOM.BoRcptCredTypes.cr_Regular;
                    oPayment.CreditCards.CreditAcct = payment.AccountCode;
                    oPayment.CreditCards.CardValidUntil = DateTime.MaxValue;
                    //oPayment.CreditCards.UserFields.Fields.Item("U_WHTType").Value = CR_Item.WHTType;
                    //oPayment.CreditCards.UserFields.Fields.Item("U_WHT_Percent").Value = CR_Item.WHTPercent;
                    //oPayment.CreditCards.UserFields.Fields.Item("U_WHTBaseAmount").Value = CR_Item.WHTAmount;

                    lRetCode = oPayment.Add();

                    if (lRetCode != 0)
                    {
                        throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                    }

                    oCompany.GetNewObjectCode(out paymentEntry);
                    DocNum = GetDocNum(paymentEntry);
                    TransId = GetDocTransId(paymentEntry);
                    GLDocNum = GetGLDocNum(TransId);

                    SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                    Log_Detail.SAPRefID = paymentEntry;
                    Log_Detail.SAPRefNo = DocNum;
                    Log_Detail.SAPRefGLNo = GLDocNum;
                    LogDetail.Add(Log_Detail);
                }
                catch (Exception ex)
                {
                    throw new Exception("Payment : " + ex.Message);
                }

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }
        public void CancelPayment(int docEntry, out string paymentEntry, out string DocNum, out string GLDocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string TransId = string.Empty;
            SAPbobsCOM.Payments oPayment = null;
            //SAPbobsCOM.Documents oInvoice = null;
            this.ConnectCompanyDB();
            oCompany.StartTransaction();

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                SAPTABLE = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oIncomingPayments));

                oPayment = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                oPayment.GetByKey(docEntry);
                lRetCode = oPayment.Cancel();

                if (lRetCode != 0)
                {
                    throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                }

                oCompany.GetNewObjectCode(out paymentEntry);
                DocNum = GetDocNum(paymentEntry);
                TransId = GetDocTransId(paymentEntry);
                GLDocNum = GetGLDocNum(TransId);

                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = paymentEntry;
                Log_Detail.SAPRefNo = DocNum;
                Log_Detail.SAPRefGLNo = GLDocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }
        #endregion

        #region --- AP Class ---
        public class Document
        {
            // Header
            public int DocEntry { get; set; }
            public string CardCode { get; set; }
            public string CardName { get; set; }
            public string CardNumber { get; set; }
            public SAPbobsCOM.BoObjectTypes DocType { get; set; }
            public DateTime PostingDate { get; set; }
            public DateTime DocDueDate { get; set; }
            public DateTime DocumentDate { get; set; }
            public DateTime UDF_VatPeriodDate { get; set; }
            public DateTime TaxDate { get; set; }
            public string UDF_RefNo { get; set; }
            public string UDF_CustName { get; set; }
            public string UDF_UnitRef { get; set; }
            public string UDF_Project { get; set; }
            public string UDF_ProjectName { get; set; }
            public string UDF_TaxID { get; set; }
            public string UDF_Address { get; set; }
            public string UDF_VatBranch { get; set; }
            public string UDF_TAX_PECL { get; set; }
            public string ICON_DeliveryNo { get; set; }
            public string U_ICON_CMPO { get; set; }
            public string U_ICON_CMWO { get; set; }
            public string U_ICON_CMAtth { get; set; }
            public string Remark { get; set; }
            public SAPbobsCOM.BoObjectTypes DocObjectCode { get; set; }
            public string Ref1 { get; set; }
            public string Ref2 { get; set; }
            public string Ref3 { get; set; }
            public string UDF_Ref { get; set; }
            public string UDF_InvNo { get; set; }
            // Details (Line Items)
            public IList<DocumentLine> Lines;
            public bool IsDeferVAT { get; set; }
            public Document()
            {
                this.Lines = new List<DocumentLine>();
                this.Remark = string.Empty;
            }
            public string NumAtCard { get; set; }
            public int BatchNum { get; set; }
            public int TransId { get; set; }
            public string Address { get; set; }
            public string VatPercent { get; set; }
            public string VatSum { get; set; }
            public string DiscPrcnt { get; set; }
            public string DiscSum { get; set; }
            public string DiscSumFC { get; set; }
            public string DocCur { get; set; }
            public string DocRate { get; set; }
            public string DocTotal { get; set; }
            public string JrnlMemo { get; set; }
            public string GroupNum { get; set; }
            public string SlpCode { get; set; }
            public string FatherCard { get; set; }
            public string Address2 { get; set; }
            public string LicTradNum { get; set; }
            public string OwnerCode { get; set; }
            public string HeaderID { get; set; }
            public string U_ICON_IssueReason { get; set; }
            public string IsAutoReverse { get; set; }
            public string StornoDate { get; set; }
            public string Project { get; set; }
            public string Series { get; set; }
            public string U_ICON_PT { get; set; }
            public string U_ICON_BF { get; set; }
            public string Rounding { get; set; }
            public string RoundDif { get; set; }
            public string PayToCode { get; set; }
            public string FreeText { get; set; }

        }
        public class DocumentLine
        {
            public string BaseEntry { get; set; }
            public int BaseType { get; set; }
            public string LineNumber { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public double Qty { get; set; }
            public string TaxCode { get; set; }
            public string TaxGroup { get; set; }
            public string TaxPercentagePerRow { get; set; }
            public string UnitPrice { get; set; }
            public double UnitAfterPrice { get; set; }
            public string AccountCode { get; set; }
            public string ShortName { get; set; }
            public double Debit { get; set; }
            public double Credit { get; set; }
            public string RefID { get; set; }
            public string FreeTxt { get; set; }
            public string WhsCode { get; set; }
            public string CostCode { get; set; }
            public string ProjectCode { get; set; }
            public bool IsDeferVAT { get; set; }
            public string VatSum { get; set; }
            public string LineMemo { get; set; }
            public string OcrCode { get; set; }
            public string OcrCode2 { get; set; }
            public string OcrCode3 { get; set; }
            public string OcrCode4 { get; set; }
            public string OcrCode5 { get; set; }
            public string UOMCode { get; set; }
            public string SlpCode { get; set; }
            public string TotalFrgn { get; set; }
            public string LineTotal { get; set; }
            public string PriceAfVAT { get; set; }
            public string ContraAccount { get; set; }
            public string TAX_BASE { get; set; }
            public string TAX_NO { get; set; }
            public string TAX_REFNO { get; set; }
            public string TAX_PECL { get; set; }
            public string TAX_TYPE { get; set; }
            public string TAX_BOOKNO { get; set; }
            public string TAX_CARDNAME { get; set; }
            public string TAX_ADDRESS { get; set; }
            public string TAX_TAXID { get; set; }
            public DateTime TAX_DATE { get; set; }
            public string TAX_CODE { get; set; }
            public string TAX_CODENAME { get; set; }
            public string TAX_RATE { get; set; }
            public string TAX_DEDUCT { get; set; }
            public string TAX_OTHER { get; set; }
            public string DiscountPerRow { get; set; }
            public string U_TAX_CardName { get; set; }
            public string ref1 { get; set; }
            public string ref2 { get; set; }
            public string U_ICON_IssueReason { get; set; }
            public string U_ICON_PT { get; set; }
            public string U_ICON_BF { get; set; }
            public string U_ICON_PRNumber { get; set; }
            public string Reference1 { get; set; }
            public string Reference2 { get; set; }
            public string Reference3 { get; set; }
            public string U_VATBRANCH { get; set; }
            public string U_BPBRANCH { get; set; }
        }
        public class Payments
        {
            public double SumPaid { get; set; }
            public string UDF_RecpRefNo { get; set; }
            public string UDF_CustName { get; set; }
            public string UDF_UnitRef { get; set; }
            public string UDF_Project { get; set; }
            public string UDF_ProjectName { get; set; }
            public DateTime ReceiptDate { get; set; }
            public DateTime TransecDate { get; set; }
            public string AccountCode { get; set; }
            public string CardCode { get; set; }

            // CA
            public double CashAmount { get; set; }
            public string CashAccount { get; set; }

            // TR, QR
            public double TransferAmount { get; set; }
            public string TransferAccount { get; set; }
            public DateTime TransferDate { get; set; }

            // CC, CQ
            public List<Cheque> Cheque { get; set; }

            // CR
            public List<CreditCard> Credit { get; set; }

            public Payments()
            {
                this.SumPaid = 0;
                this.Credit = new List<CreditCard>();
                this.Cheque = new List<Cheque>();
            }
        }
        public class CreditCard
        {
            //public string CardName { get; set; }
            public double Amount { get; set; }
            public string CardNumber { get; set; }
            public SAPbobsCOM.BoRcptCredTypes CreditType { get; set; }
            public string Account { get; set; }
            public DateTime ExpireDate { get; set; }
            public string PayDetailID { get; set; }
            public string WHTType { get; set; }
            public double WHTAmount { get; set; }
            public double WHTPercent { get; set; }
        }
        public class Cheque
        {
            public double Amount { get; set; }
            public int Number { get; set; }
            public string Account { get; set; }
            public DateTime Date { get; set; }
        }

        public class ChartofAccounts
        {
            public string AcctCode { get; set; }
            public string AcctName { get; set; }
            public string FrgnName { get; set; }
            public SAPbobsCOM.BoYesNoEnum Postable { get; set; }
            public string AccntntCod { get; set; }
            public string AccountTyp { get; set; }
            public string ActCurr { get; set; }
            public string FatherNum { get; set; }
            public SAPbobsCOM.BoYesNoEnum LocManTran { get; set; }
            public SAPbobsCOM.BoYesNoEnum Finanse { get; set; }
            public SAPbobsCOM.BoYesNoEnum CfwRlvnt { get; set; }
            public SAPbobsCOM.BoYesNoEnum PrjRelvnt { get; set; }
            public SAPbobsCOM.BoYesNoEnum Dim1Relvnt { get; set; }
            public SAPbobsCOM.BoYesNoEnum Dim2Relvnt { get; set; }
            public SAPbobsCOM.BoYesNoEnum Dim3Relvnt { get; set; }
            public SAPbobsCOM.BoYesNoEnum Dim4Relvnt { get; set; }
            public SAPbobsCOM.BoYesNoEnum Dim5Relvnt { get; set; }
            public SAPbobsCOM.BoYesNoEnum BlocManPos { get; set; }
            public SAPbobsCOM.BoYesNoEnum ValidFor { get; set; }
            public SAPbobsCOM.BoYesNoEnum FrozenFor { get; set; }

        }

        public class Project
        {
            public string ProjectCode { get; set; }
            public string ProjectName { get; set; }
            public DateTime ValidFrom { get; set; }
            public DateTime ValidTo { get; set; }
            public SAPbobsCOM.BoYesNoEnum Active { get; set; }
        }

        public class Warehouse
        {
            public string WarehouseCode { get; set; }
            public string WarehouseName { get; set; }
            public string Block { get; set; }
            public string City { get; set; }
            public string ZipCode { get; set; }
            public SAPbobsCOM.BoYesNoEnum DropShip { get; set; }
            public SAPbobsCOM.BoYesNoEnum Nettable { get; set; }
            public SAPbobsCOM.BoYesNoEnum Inactive { get; set; }
            public string BalInvntAc { get; set; }
            public string SaleCostAc { get; set; }
            public string TransferAc { get; set; }
            public string PriceDifAc { get; set; }
            public string ExpensesAC { get; set; }
            public string APCMAct { get; set; }
            public string RevenuesAc { get; set; }
            public string ARCMAct { get; set; }
            public string IncresGlAc { get; set; }
            public string DecresGlAc { get; set; }
        }

        public class CostCenter
        {
            public string PrcCode { get; set; }
            public string PrcName { get; set; }
            public string GrpCode { get; set; }
            public int DimCode { get; set; }
            public DateTime ValidFrom { get; set; }
            public DateTime ValidTo { get; set; }
            public SAPbobsCOM.BoYesNoEnum Locked { get; set; }

        }

        public class VatGroup
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public SAPbobsCOM.BoVatCategoryEnum Category { get; set; }
            public string Account { get; set; }
            public string DeferrAcc { get; set; }
            public double NonDedct { get; set; }
            public SAPbobsCOM.BoYesNoEnum Inactive { get; set; }

            public DateTime EffecDate { get; set; }
            public Decimal Rate { get; set; }

            // Details (Line Items)
            public IList<VatGroupDetail> Details;

            public VatGroup()
            {
                this.Details = new List<VatGroupDetail>();
            }
        }

        public class VatGroupDetail
        {
            public string EffecDate { get; set; }
            public double Rate { get; set; }
        }

        public class WithholdingTax
        {
            public string WTCode { get; set; }
            public string WTName { get; set; }
            public SAPbobsCOM.WithholdingTaxCodeCategoryEnum Category { get; set; }
            public SAPbobsCOM.WithholdingTaxCodeBaseTypeEnum BaseType { get; set; }
            public double PrctBsAmnt { get; set; }
            public string OffClCode { get; set; }
            public string Account { get; set; }
            public SAPbobsCOM.BoYesNoEnum Inactive { get; set; }

            // Details (Line Items)
            public IList<WHTDetail> Details;

            public WithholdingTax()
            {
                this.Details = new List<WHTDetail>();
            }

        }

        public class WHTDetail
        {
            public string EffecDate { get; set; }
            public double Rate { get; set; }

        }

        public class PaymentTerms
        {
            public int GroupNum { get; set; }
            public string PymntGroup { get; set; }
            public SAPbobsCOM.BoBaselineDate BslineDate { get; set; }
            public SAPbobsCOM.BoPayTermDueTypes PayDuMonth { get; set; }
            public int ExtraMonth { get; set; }
            public int ExtraDays { get; set; }

        }

        public class ItemMaster
        {
            public string Series { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string FrgnName { get; set; }
            public SAPbobsCOM.ItemTypeEnum ItemType { get; set; }
            public string ItmsGrpCod { get; set; }
            public string UserText { get; set; }
            //Tab General
            public SAPbobsCOM.BoYesNoEnum WTLiable { get; set; }
            public SAPbobsCOM.BoYesNoEnum PrchseItem { get; set; }
            public SAPbobsCOM.BoYesNoEnum SellItem { get; set; }
            public SAPbobsCOM.BoYesNoEnum InvntItem { get; set; }
            public SAPbobsCOM.BoYesNoEnum ManSerNum { get; set; }
            public SAPbobsCOM.BoYesNoEnum ManBtchNum { get; set; }
            public SAPbobsCOM.BoManageMethod MngMethod { get; set; }
            public SAPbobsCOM.BoYesNoEnum ValidFor { get; set; }
            public SAPbobsCOM.BoYesNoEnum FrozenFor { get; set; }
            //Tab Purchasing
            public string CardCode { get; set; }
            public string SuppCatNum { get; set; }
            public string BuyUnitMsr { get; set; }
            public string NumInBuy { get; set; }
            public string PurPackMsr { get; set; }
            public string PurPackUn { get; set; }
            public string VatGroupPu { get; set; }
            //Tab Sales
            public string VatGourpSa { get; set; }
            public string SalUnitMsr { get; set; }
            public string NumInSale { get; set; }
            public string SalPackMsr { get; set; }
            public string SalPackUn { get; set; }
            //Tab Inventory
            public SAPbobsCOM.BoGLMethods GLMethod { get; set; }
            public string InvntryUom { get; set; }
            public string IWeight1 { get; set; }
            public SAPbobsCOM.BoYesNoEnum ByWh { get; set; }
            public SAPbobsCOM.BoInventorySystem EvalSystem { get; set; }
            public string BalInvntAc { get; set; }
            public string ExpensesAc { get; set; }
            public string DecreasAc { get; set; }
            public string IncreasAc { get; set; }
            public string APCMAct { get; set; }

        }

        public class ItemToWarehouse
        {
            public string ItemCode    { get; set; }
            public string WhsCode     { get; set; }
            public SAPbobsCOM.BoYesNoEnum Locked      { get; set; }
            public string BalInvntAc  { get; set; }
            public string SaleCostAc  { get; set; }
            public string TransferAc  { get; set; }
            public string PriceDifAc  { get; set; }
            public string ExpensesAC  { get; set; }
            public string APCMAct     { get; set; }
            public string RevenuesAc  { get; set; }
            public string ARCMAct     { get; set; }
            public string IncresGlAc  { get; set; }
            public string DecresGlAc { get; set; }

        }
        public class ItemGroup
        {
            public int ItmsGrpCod { get; set; }
            public string ItmsGrpNam { get; set; }
            public string BalInvntAc { get; set; }
            public string SaleCostAc { get; set; }
            public string TransferAc { get; set; }
            public string PriceDifAc { get; set; }
            public string ExpensesAC { get; set; }
            public string APCMAct { get; set; }
            public string RevenuesAc { get; set; }
            public string ARCMAct { get; set; }
            public string IncresGlAc { get; set; }
            public string DecresGlAc { get; set; }

        }
        public class BusinessPartner
        {
            public string Series { get; set; }
            public string CardCode { get; set; }
            public string CardName { get; set; }
            public string CardFNme { get; set; }
            public SAPbobsCOM.BoCardTypes CardType { get; set; }
            public SAPbobsCOM.BoCardCompanyTypes CmpPrivate { get; set; }
            public string GroupCode { get; set; }
            public string Currency { get; set; }
            public string LicTradNum { get; set; }
            public string Remarks { get; set; }
            public string Phone1 { get; set; }
            public string Phone2 { get; set; }
            public string Fax { get; set; }
            public string E_Mail { get; set; }
            public string IntrntSite { get; set; }
            public string AliasName { get; set; }
            public string CntctPrsn { get; set; }
            public string Territory { get; set; }
            public string GlblLocNum { get; set; }
            public SAPbobsCOM.BoYesNoEnum ValidFor { get; set; }
            public SAPbobsCOM.BoYesNoEnum FrozenFor { get; set; }
            //Payment Terms 
            public string GroupNum { get; set; }
            public string Discount { get; set; }
            //Accounting (ข้อมูลบัญชีและภาษี) , Withholding Tax Allowed for BP (ภาษีหัก/ถูกหัก ณ ที่จ่าย) 
            public string DebPayAcct { get; set; }
            public SAPbobsCOM.BoVatStatus VatStatus { get; set; }
            public string ECVatGroup { get; set; }
            public SAPbobsCOM.BoYesNoEnum WTLiable { get; set; }
            public SAPbobsCOM.AssesseeTypeEnum TypWTReprt { get; set; }
            public BusinessBank BPBankAccounts { get; set; }

        }

        public class ContactPersons
        {
            public string CntCtCode { get; set; }
            public string CardCode { get; set; }
            public string Name { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Position { get; set; }
            public string Tel1 { get; set; }
            public string Tel2 { get; set; }
            public string Cellolar { get; set; }
            public string Fax { get; set; }
            public string E_MaiL { get; set; }
            public string Notes1 { get; set; }
            public string Notes2 { get; set; }
            public string Active { get; set; }
            public string Default { get; set; }
            public string CurrentLine { get; set; }

        }

        public class BusinessPartnerAddress
        {
            public string Address { get; set; }
            public string AdresType { get; set; }
            public string CardCode { get; set; }
            public string Street { get; set; }
            public string Block { get; set; }
            public string City { get; set; }
            public string County { get; set; }
            public string Country { get; set; }
            public string ZipCode { get; set; }
            public string Default { get; set; }
            public string CurrentLine { get; set; }
            public string AddressesName { get; set; }
        }
        public class BusinessPartnerGroups
        {
            public string GroupCode { get; set; }
            public string GroupNum { get; set; }
            public string GroupType { get; set; }

        }
        public class BusinessBank
        {
            public string CardCode { get; set; }
            public string Country { get; set; }
            public string BankCode { get; set; }
            public string Account { get; set; }
            public string AcctName { get; set; }
            public string Branch { get; set; }
            public string IBAN { get; set; }

        }

        public class BusinessParnerWithHoldingTax
        {
            public string CardCode { get; set; }
            public string WTCode { get; set; }
            public string Default { get; set; }
        }

        public class VatBranch
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string U_CMPNAME { get; set; }
            public string U_PLCNAME { get; set; }
            public string U_ZIPCODE { get; set; }
            public string U_TEL { get; set; }
            public string U_FAX { get; set; }
            public string U_TAXID { get; set; }
            public string U_BRNCHADD { get; set; }

        }

        public class Banks
        {
            public string CountryCod { get; set; }
            public string BankCode { get; set; }
            public string BankName { get; set; }

        }

        public class HouseBankAccount
        {
            public string BankCode { get; set; }
            public string Country { get; set; }
            public string Branch { get; set; }
            public string Account { get; set; }
            public string AcctName { get; set; }
            public string GLAccount { get; set; }

        }

        public class StockTransfer
        {
            public string DocNum { get; set; }
            public string Series { get; set; }
            public DateTime DocDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime TaxDate { get; set; }
            public string Filler { get; set; }
            public string ToWhsCode { get; set; }
            public string Comments { get; set; }
            public string JrnlMemo { get; set; }


            // Details (Line Items)
            public IList<StockTransfer_Detail> Details;

            public StockTransfer()
            {
                this.Details = new List<StockTransfer_Detail>();
            }

        }

        public class StockTransfer_Detail
        {
            public string ItemCode { get; set; }
            public string FromWhsCod { get; set; }
            public string WhsCode { get; set; }
            public string Quantity { get; set; }
            public string UnitMsr { get; set; }
            public string Project { get; set; }
            public string OcrCode { get; set; }
            public string OcrCode2 { get; set; }
            public string OcrCode3 { get; set; }
            public string OcrCode4 { get; set; }
            public string OcrCode5 { get; set; }

        }

        #endregion

        #endregion

        #region --- Sync Data SAP Master ---
        public List<Dictionary<string, object>> GetMasterItem(out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();

            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery(@"
SELECT 
    i.ItemCode,
    ItemName,
    isnull(FrgnName,'') FrgnName,
    ItemType,
    isnull(validFrom,'') validFrom,
    validTo,
    BuyUnitMsr,
    isnull(CodeBars,'') CodeBars,
    isnull(LeadTime,'') LeadTime,
    case 
        when ItemType = 'F' then 'a'
        when InvntItem = 'N' and AssetItem = 'N' and PrchseItem = 'Y' then 's' else 'm' end Mat_Type,
    isnull(DfltExpn,'') DfltExpn,
    i.InvntItem
FROM OITM i
left join ogar acc on i.itemcode = acc.itemcode
--where i.itemcode = '0001-ALM-00027'
");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetMasterItemAP(out int InternalErrorCode, out string InternalErrorMessage, string CompanyID = "")
        {
            this.ConnectCompanyDB();

            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery($@"
DECLARE @CompanyId NVARCHAR(50) = '{CompanyID}'

SELECT it.ItemCode Code
 ,@CompanyId CompanyId
 ,ItemName Name
 ,ItemName Description
 ,CASE 
  WHEN InvntItem = 'Y'
   THEN 'Stock'
  ELSE CASE ItemType
    WHEN 'F'
     THEN 'Fix Asset'
    ELSE 'Expense'
    END
  END Type
 ,getdate() StartDate
 ,getdate() ToDate
 ,'' UnitCode
 ,0 UnitPrice
 ,NULL PriceType
 ,'' BarCode
 ,NULL LeadTime
 ,'Active' STATUS
 ,NULL CreateById
 ,'SAP APi' CreateBy
 ,GETDATE() CreateDate
 ,NULL ModifyById
 ,'SAP APi' ModifyBy
 ,GETDATE() ModifyDate
FROM OITM it
--WHERE NOT EXISTS (
--  SELECT 1
--  FROM A_NEXT_AP.dbo.AP_Master_Item
--  WHERE Code = itemcode collate THAI_CI_AS
--   AND CompanyId = @CompanyId
--  )
where 1 = 1
AND PrchseItem = 'Y'
");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetMasterSupplier(out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();

            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery(@"
SELECT 
    CardCode,
	isnull(CardName,'') CardName,
	CardFName,
	LicTradNum,
	Currency,
	isnull(Phone1,'') Phone1,
	isnull(Phone2,'') Phone2,
	isnull(Cellular,'') Cellular,
	isnull(Fax,'') Fax,
	isnull(E_Mail,'') E_Mail,
	isnull(IntrntSite,'') IntrntSite,
	isnull(Notes,'') Notes,
	T2.ExtraDays as PaymentTerms,
    isnull([Address],'') + ' ' + isnull([block],'') + ' ' + isnull([city],'') + ' ' + isnull([ZipCode],'') Address 
FROM 
    OCRD T1
    LEFT JOIN OCTG T2 ON T1.GroupNum = T2.GroupNum
WHERE 
    CardType = 'S'");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetmasterSupplier_Address(out int InternalErrorCode, out string InternalErrorMessage, string CardCode = "")
        {
            this.ConnectCompanyDB();

            InternalErrorCode = 0;
            InternalErrorMessage = string.Empty;
            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery($@"
SELECT 
	o.CardCode,
	CASE AdresType
		WHEN 'B'
			THEN 'BillTo'
		WHEN 'S'
			THEN 'ShipTo'
		ELSE AdresType
		END [AddressType]
	,isnull(Address3, '') + ' ' + isnull(Street, '') + ' ' + isnull(it.Block, '') + ' ' + isnull(it.City, '') Address
	,isnull(it.ZipCode, '') [PostCode]
	,'SAP Api' CreateBy
	,getdate() CreateDate
	,'SAP Api' ModifyBy
	,GETDATE() ModifyDate
	,it.Address BranchCode
	,Address2 BranchName
	,CASE 
		WHEN AdresType = 'B'
			AND BillToDef = it.Address
			THEN 1
		ELSE CASE 
				WHEN AdresType = 'S'
					AND ShipToDef = it.address
					THEN 1
				ELSE 0
				END
		END IsDefault
FROM CRD1 it
INNER JOIN OCRD o ON it.CardCode = o.CardCode
where o.cardcode = '{CardCode}'");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetmasterSupplier_Contact(out int InternalErrorCode, out string InternalErrorMessage, string CardCode = "")
        {
            this.ConnectCompanyDB();

            InternalErrorCode = 0;
            InternalErrorMessage = string.Empty;
            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery($@"
SELECT p.CardCode --[SupplierCode]
	,isnull(p.title, '') + isnull(p.FirstName, '') + ' ' + isnull(p.LastName, '') [ContactName]
	,CASE 
		WHEN d.CntctPrsn = p.Name
			THEN 1
		ELSE 0
		END [IsDefault]
	--,s.Id [SupplierId]
	,p.[Tel1]
	,p.[Tel2]
	,p.Cellolar [Mobile]
	,p.E_MailL [Email]
	,p.[Fax]
FROM OCPR p
INNER JOIN OCRD d ON p.CardCode = d.CardCode
--INNER JOIN A_NEXT_AP.dbo.AP_Master_Supplier s ON p.CardCode collate THAI_CI_AS = s.Code
	--AND s.CompanyId = @CompanyId
WHERE Active = 'Y'
and p.cardcode = '{CardCode}'");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetMasterCostCode(out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();

            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery(@"
SELECT 
    T2.[PrcCode], 
    T2.[PrcName], 
    T1.[DimDesc],
    T1.[DimCode] 
FROM [dbo].[ODIM]  T1 
INNER JOIN OPRC T2 ON T1.[DimCode] = T2.[DimCode] 
WHERE T1.[DimActive]  = 'Y'");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetMasterAccountCode(out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();

            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery(@"
SELECT 
    AcctCode Code,
    AcctName Name,
    Postable
FROM OACT");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetPettyCashBalance(string ProjectCode, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();

            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery($@"
SELECT T0.[U_PT_Balance]
 ,T0.[U_Project]
 ,CardCode
 ,CardName
 ,DebPayAcct
FROM OCRD T0
WHERE T0.[CardType] = 'C'
AND T0.[U_PT_Balance] IS NOT NULL
AND [U_PT_Balance] >= 0.00
AND U_Project = '{ProjectCode}'");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetMasterPettyCashBalance(out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();

            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery($@"
SELECT 
    CardCode
    ,CardName
    ,[U_Project]
    ,[U_PT_Balance]
    ,DebPayAcct
FROM OCRD
WHERE [CardType] = 'C'
AND [U_PT_Balance] >= 0.00
AND [U_Project] IS NOT NULL");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetStockBalance(string ItemCode, string WhsCode, out int InternalErrorCode, out string InternalErrorMessage)
        {
            ICON.Interface.SAP_Interface_Log Log = ICON.Interface.Transaction.CreateSAPLog(
                   "GetStockBalance",
                   "getstockbalancetest",
                   "",
                   "",
                   "ConnectingCompanyDB",
                   "SAP_StockBalance",
                   WhsCode,
                   "SAPApi");

            this.ConnectCompanyDB();

            ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     "",
                    "",
                    "",
                    "",
                    200,
                    "",
                    "",
                    "ConnectedCompanyDB",
                    new List<SAP_Interface_Log_Detail>());

            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                string sql = $@"
SELECT 
       [ItemCode]
      ,[ItemName]
      ,[WhsCode]
      ,[OnHand]
  FROM [SAP_StockBalance]
where 1 = 1";

                if (!string.IsNullOrEmpty(ItemCode))
                {
                    sql += $" and itemcode = '{ItemCode}'";
                }

                if (!string.IsNullOrEmpty(WhsCode))
                {
                    sql += $" and whscode = '{WhsCode}'";
                }

                oRecordSet.DoQuery(sql);

                ICON.Interface.Transaction.UpdateSAPLog(
                    Log.TranID,
                     "",
                    "",
                    "",
                    "",
                    200,
                    "",
                    "",
                    "oRecordSet.DoQuery(sql)",
                    new List<SAP_Interface_Log_Detail>());

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetStockBalanceAll(out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();

            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery($@"
SELECT 
       [ItemCode]
      ,[ItemName]
      ,[WhsCode]
      ,[OnHand]
  FROM [SAP_StockBalance]
order by ItemCode,WhsCode");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public List<Dictionary<string, object>> GetIssueReason(out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();

            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery($@"
SELECT *  FROM [dbo].[@ISSUE_REASON]");

                List<Dictionary<string, object>> APItems = new List<Dictionary<string, object>>();
                if (oRecordSet.RecordCount > 0)
                {
                    Dictionary<string, object> APItem;

                    while (!oRecordSet.EoF)
                    {
                        APItem = new Dictionary<string, object>();

                        for (int i = 0; i < oRecordSet.Fields.Count; i++)
                        {
                            APItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                        }

                        APItems.Add(APItem);

                        oRecordSet.MoveNext();
                    }
                }

                return APItems;
            }
            catch (Exception ex)
            {
                oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }

        public void CreateUpdateProjectMaster(Project Pro, out int InternalErrorCode, out string InternalErrorMessage)
        {
            SAPbobsCOM.CompanyService oCmpSrv = null;
            SAPbobsCOM.ProjectsService projectService = null;
            SAPbobsCOM.Project project = null;
            SAPbobsCOM.ProjectParams projectParams = null;

            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                oCmpSrv = oCompany.GetCompanyService();
                projectService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.ProjectsService);


                //Get a project
                projectParams = projectService.GetDataInterface(SAPbobsCOM.ProjectsServiceDataInterfaces.psProjectParams);
                projectParams.Code = Pro.ProjectCode;

                try
                {
                    project = projectService.GetProject(projectParams);

                    project.Code = Pro.ProjectCode;
                    project.Name = Pro.ProjectName;
                    project.ValidFrom = Pro.ValidFrom;
                    if (Pro.ValidTo != DateTime.MinValue) project.ValidTo = Convert.ToDateTime(Pro.ValidTo);
                    project.Active = Pro.Active;
                    projectService.UpdateProject(project);
                }
                catch (Exception)
                {
                    project = projectService.GetDataInterface(SAPbobsCOM.ProjectsServiceDataInterfaces.psProject);
                    project.Code = Pro.ProjectCode;
                    project.Name = Pro.ProjectName;
                    project.ValidFrom = Pro.ValidFrom;
                    if (Pro.ValidTo != DateTime.MinValue) project.ValidTo = Convert.ToDateTime(Pro.ValidTo);
                    project.Active = Pro.Active;
                    projectService.AddProject(project);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateWareHouseMaster(Warehouse Whs, out int InternalErrorCode, out string InternalErrorMessage)
        {
            SAPbobsCOM.Warehouses warehouse = null;
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                warehouse = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oWarehouses);

                if (warehouse.GetByKey(Whs.WarehouseCode))
                {
                    warehouse.WarehouseCode = Whs.WarehouseCode;
                    warehouse.WarehouseName = Whs.WarehouseName;
                    if (!string.IsNullOrEmpty(Whs.Block)) warehouse.Block = Whs.Block;
                    if (!string.IsNullOrEmpty(Whs.City)) warehouse.City = Whs.City;
                    if (!string.IsNullOrEmpty(Whs.ZipCode)) warehouse.ZipCode = Whs.ZipCode;
                    warehouse.DropShip = Whs.DropShip;
                    warehouse.Nettable = Whs.Nettable;
                    warehouse.Inactive = Whs.Inactive;
                    warehouse.StockAccount = Whs.BalInvntAc;
                    warehouse.CostOfGoodsSold = Whs.SaleCostAc;
                    warehouse.TransfersAcc = Whs.TransferAc;
                    warehouse.PriceDifferencesAccount = Whs.PriceDifAc;
                    warehouse.ExpenseAccount = Whs.ExpensesAC;
                    warehouse.PurchaseCreditAcc = Whs.APCMAct;
                    warehouse.RevenuesAccount = Whs.RevenuesAc;
                    warehouse.SalesCreditAcc = Whs.ARCMAct;
                    warehouse.IncreaseGLAccount = Whs.IncresGlAc;
                    warehouse.DecreaseGLAccount = Whs.DecresGlAc;
                    lRetCode = warehouse.Update();
                }
                else
                {
                    warehouse.WarehouseCode = Whs.WarehouseCode;
                    warehouse.WarehouseName = Whs.WarehouseName;
                    if (!string.IsNullOrEmpty(Whs.Block)) warehouse.Block = Whs.Block;
                    if (!string.IsNullOrEmpty(Whs.City)) warehouse.City = Whs.City;
                    if (!string.IsNullOrEmpty(Whs.ZipCode)) warehouse.ZipCode = Whs.ZipCode;
                    warehouse.DropShip = Whs.DropShip;
                    warehouse.Nettable = Whs.Nettable;
                    warehouse.Inactive = Whs.Inactive;
                    warehouse.StockAccount = Whs.BalInvntAc;
                    warehouse.CostOfGoodsSold = Whs.SaleCostAc;
                    warehouse.TransfersAcc = Whs.TransferAc;
                    warehouse.PriceDifferencesAccount = Whs.PriceDifAc;
                    warehouse.ExpenseAccount = Whs.ExpensesAC;
                    warehouse.PurchaseCreditAcc = Whs.APCMAct;
                    warehouse.RevenuesAccount = Whs.RevenuesAc;
                    warehouse.SalesCreditAcc = Whs.ARCMAct;
                    warehouse.IncreaseGLAccount = Whs.IncresGlAc;
                    warehouse.DecreaseGLAccount = Whs.DecresGlAc;
                    lRetCode = warehouse.Add();
                }



                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateChartofAccountMaster(ChartofAccounts objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.ChartOfAccounts oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);

                if (oData.GetByKey(objSave.AcctCode))
                {
                    oData.Name = objSave.AcctName;
                    oData.ForeignName = objSave.FrgnName;
                    //oData.ActiveAccount = objSave.Postable;
                    if (!string.IsNullOrEmpty(objSave.AccntntCod)) oData.ExternalCode = objSave.AccntntCod;
                    if (!string.IsNullOrEmpty(objSave.AccountTyp)) oData.AccountType = objSave.AccountTyp == "I" ? SAPbobsCOM.BoAccountTypes.at_Revenues : objSave.AccountTyp == "E" ? SAPbobsCOM.BoAccountTypes.at_Expenses : objSave.AccountTyp == "N" ? SAPbobsCOM.BoAccountTypes.at_Other : SAPbobsCOM.BoAccountTypes.at_Other;
                    oData.AcctCurrency = objSave.ActCurr;
                    oData.FatherAccountKey = objSave.FatherNum;
                    oData.LockManualTransaction = objSave.LocManTran;
                    oData.CashAccount = objSave.Finanse;
                    oData.CashFlowRelevant = objSave.CfwRlvnt;
                    oData.ProjectRelevant = objSave.PrjRelvnt;
                    oData.DistributionRuleRelevant = objSave.Dim1Relvnt;
                    oData.DistributionRule2Relevant = objSave.Dim2Relvnt;
                    oData.DistributionRule3Relevant = objSave.Dim3Relvnt;
                    oData.DistributionRule4Relevant = objSave.Dim4Relvnt;
                    oData.DistributionRule5Relevant = objSave.Dim5Relvnt;
                    oData.BlockManualPosting = objSave.BlocManPos;
                    oData.ValidFor = objSave.ValidFor;
                    oData.FrozenFor = objSave.FrozenFor;
                    lRetCode = oData.Update();
                }
                else
                {
                    oData.Code = objSave.AcctCode;
                    oData.Name = objSave.AcctName;
                    oData.ForeignName = objSave.FrgnName;
                    oData.ActiveAccount = objSave.Postable;
                    if (!string.IsNullOrEmpty(objSave.AccntntCod)) oData.ExternalCode = objSave.AccntntCod;
                    if (!string.IsNullOrEmpty(objSave.AccountTyp)) oData.AccountType = objSave.AccountTyp == "I" ? SAPbobsCOM.BoAccountTypes.at_Revenues : objSave.AccountTyp == "E" ? SAPbobsCOM.BoAccountTypes.at_Expenses : objSave.AccountTyp == "N" ? SAPbobsCOM.BoAccountTypes.at_Other : SAPbobsCOM.BoAccountTypes.at_Other;
                    oData.AcctCurrency = objSave.ActCurr;
                    oData.FatherAccountKey = objSave.FatherNum;
                    oData.LockManualTransaction = objSave.LocManTran;
                    oData.CashAccount = objSave.Finanse;
                    oData.CashFlowRelevant = objSave.CfwRlvnt;
                    oData.ProjectRelevant = objSave.PrjRelvnt;
                    oData.DistributionRuleRelevant = objSave.Dim1Relvnt;
                    oData.DistributionRule2Relevant = objSave.Dim2Relvnt;
                    oData.DistributionRule3Relevant = objSave.Dim3Relvnt;
                    oData.DistributionRule4Relevant = objSave.Dim4Relvnt;
                    oData.DistributionRule5Relevant = objSave.Dim5Relvnt;
                    oData.BlockManualPosting = objSave.BlocManPos;
                    oData.ValidFor = objSave.ValidFor;
                    oData.FrozenFor = objSave.FrozenFor;
                    lRetCode = oData.Add();
                }

                string errorMsg = string.Empty;

                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }


            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateCostCenterMaster(CostCenter CostCenter, out int InternalErrorCode, out string InternalErrorMessage)
        {
            SAPbobsCOM.CompanyService oCmpSrv = null;
            SAPbobsCOM.ProfitCentersService oService = null;

            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                oCmpSrv = oCompany.GetCompanyService();
                oService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.ProfitCentersService);

                SAPbobsCOM.ProfitCenter oData = null;
                SAPbobsCOM.ProfitCenterParams oParams;

                //Get a Data
                oParams = oService.GetDataInterface(SAPbobsCOM.ProfitCentersServiceDataInterfaces.pcsProfitCenterParams);
                oParams.CenterCode = CostCenter.PrcCode;

                try
                {
                    oData = oService.GetProfitCenter(oParams);

                    oData.CenterCode = CostCenter.PrcCode;
                    oData.CenterName = CostCenter.PrcName;
                    oData.GroupCode = CostCenter.GrpCode;
                    oData.InWhichDimension = CostCenter.DimCode;
                    oData.Effectivefrom = CostCenter.ValidFrom;
                    if (CostCenter.ValidTo != DateTime.MinValue) oData.EffectiveTo = Convert.ToDateTime(CostCenter.ValidTo);
                    oData.Active = CostCenter.Locked;
                    oService.UpdateProfitCenter(oData);
                }
                catch (Exception)
                {
                    oData = oService.GetDataInterface(SAPbobsCOM.ProfitCentersServiceDataInterfaces.pcsProfitCenter);
                    oData.CenterCode = CostCenter.PrcCode;
                    oData.CenterName = CostCenter.PrcName;
                    oData.GroupCode = CostCenter.GrpCode;
                    oData.InWhichDimension = CostCenter.DimCode;
                    oData.Effectivefrom = CostCenter.ValidFrom;
                    if (CostCenter.ValidTo != DateTime.MinValue) oData.EffectiveTo = Convert.ToDateTime(CostCenter.ValidTo);
                    oData.Active = CostCenter.Locked;
                    oService.AddProfitCenter(oData);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateVatGroupMaster(VatGroup objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.VatGroups oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVatGroups);

                if (oData.GetByKey(objSave.Code))
                {
                    oData.Code = objSave.Code;
                    oData.Name = objSave.Name;
                    oData.Category = objSave.Category;
                    if (!string.IsNullOrEmpty(objSave.Account)) oData.TaxAccount = objSave.Account;
                    if (!string.IsNullOrEmpty(objSave.DeferrAcc)) oData.DeferredTaxAcc = objSave.DeferrAcc;
                    oData.NonDeduct = objSave.NonDedct;
                    oData.Inactive = objSave.Inactive;

                    bool firstLine = true;
                    // [TIPS] - Caching Variable
                    SAPbobsCOM.VatGroups_Lines oDetails = oData.VatGroups_Lines;
                    foreach (VatGroupDetail VatDetail in objSave.Details)
                    {
                        if (firstLine)
                        {
                            firstLine = false;
                        }
                        else
                        {
                            oDetails.Add();
                        }

                        if (!string.IsNullOrEmpty(VatDetail.EffecDate))
                        {
                            oDetails.Effectivefrom = Convert.ToDateTime(VatDetail.EffecDate);
                        }
                        if (VatDetail.Rate > 0)
                        {
                            oDetails.Rate = VatDetail.Rate;
                        }
                    }
                    lRetCode = oData.Update();
                }
                else
                {
                    oData.Code = objSave.Code;
                    oData.Name = objSave.Name;
                    oData.Category = objSave.Category;
                    if (!string.IsNullOrEmpty(objSave.Account)) oData.TaxAccount = objSave.Account;
                    if (!string.IsNullOrEmpty(objSave.DeferrAcc)) oData.DeferredTaxAcc = objSave.DeferrAcc;
                    oData.NonDeduct = objSave.NonDedct;
                    oData.Inactive = objSave.Inactive;
                    bool firstLine = true;
                    // [TIPS] - Caching Variable
                    SAPbobsCOM.VatGroups_Lines oDetails = oData.VatGroups_Lines;
                    foreach (VatGroupDetail VatDetail in objSave.Details)
                    {
                        if (firstLine)
                        {
                            firstLine = false;
                        }
                        else
                        {
                            oDetails.Add();
                        }

                        if (!string.IsNullOrEmpty(VatDetail.EffecDate))
                        {
                            oDetails.Effectivefrom = Convert.ToDateTime(VatDetail.EffecDate);
                        }
                        if (VatDetail.Rate > 0)
                        {
                            oDetails.Rate = VatDetail.Rate;
                        }
                    }
                    lRetCode = oData.Add();
                }



                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateWithholdingTax(WithholdingTax objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.WithholdingTaxCodes oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oWithholdingTaxCodes);

                if (oData.GetByKey(objSave.WTCode))
                {
                    oData.WTCode = objSave.WTCode;
                    oData.WTName = objSave.WTName;
                    oData.Category = objSave.Category;
                    oData.BaseType = objSave.BaseType;
                    oData.BaseAmount = objSave.PrctBsAmnt;
                    if (!string.IsNullOrEmpty(objSave.OffClCode)) oData.OfficialCode = objSave.OffClCode;
                    if (!string.IsNullOrEmpty(objSave.Account)) oData.Account = objSave.Account;
                    oData.Inactive = objSave.Inactive;

                    bool firstLine = true;
                    // [TIPS] - Caching Variable
                    SAPbobsCOM.WithholdingTaxCodes_Lines oDetails = oData.Lines;
                    foreach (WHTDetail VatDetail in objSave.Details)
                    {
                        if (firstLine)
                        {
                            firstLine = false;
                        }
                        else
                        {
                            oDetails.Add();
                        }

                        if (!string.IsNullOrEmpty(VatDetail.EffecDate))
                        {
                            oDetails.Effectivefrom = Convert.ToDateTime(VatDetail.EffecDate);
                        }
                        if (VatDetail.Rate > 0)
                        {
                            oDetails.Rate = VatDetail.Rate;
                        }
                    }
                    lRetCode = oData.Update();
                }
                else
                {
                    oData.WTCode = objSave.WTCode;
                    oData.WTName = objSave.WTName;
                    oData.Category = objSave.Category;
                    oData.BaseType = objSave.BaseType;
                    oData.BaseAmount = objSave.PrctBsAmnt;
                    if (!string.IsNullOrEmpty(objSave.OffClCode)) oData.OfficialCode = objSave.OffClCode;
                    if (!string.IsNullOrEmpty(objSave.Account)) oData.Account = objSave.Account;
                    oData.Inactive = objSave.Inactive;

                    bool firstLine = true;
                    // [TIPS] - Caching Variable
                    SAPbobsCOM.WithholdingTaxCodes_Lines oDetails = oData.Lines;
                    foreach (WHTDetail VatDetail in objSave.Details)
                    {
                        if (firstLine)
                        {
                            firstLine = false;
                        }
                        else
                        {
                            oDetails.Add();
                        }

                        if (!string.IsNullOrEmpty(VatDetail.EffecDate))
                        {
                            oDetails.Effectivefrom = Convert.ToDateTime(VatDetail.EffecDate);
                        }
                        if (VatDetail.Rate > 0)
                        {
                            oDetails.Rate = VatDetail.Rate;
                        }
                    }

                    lRetCode = oData.Add();
                }



                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }


        public void CreateUpdatePaymentTermsMaster(PaymentTerms objSave, out int InternalErrorCode, out string InternalErrorMessage, out string GroupNum)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string SAP_CODE = string.Empty;
            try
            {
                SAPbobsCOM.PaymentTermsTypes oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPaymentTermsTypes);
                SAPTABLE PaymentTerms = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oPaymentTermsTypes));
                if (objSave.GroupNum == 0)
                {
                    SAP_CODE = GetDataColumn("GroupNum", PaymentTerms, "PymntGroup", objSave.PymntGroup);
                }
                else
                {
                    SAP_CODE = objSave.GroupNum.ToString();
                }

                if (oData.GetByKey(objSave.GroupNum))
                {
                    oData.PaymentTermsGroupName = objSave.PymntGroup;
                    oData.BaselineDate = objSave.BslineDate;
                    oData.StartFrom = objSave.PayDuMonth;
                    oData.NumberOfAdditionalMonths = objSave.ExtraMonth;
                    oData.NumberOfAdditionalDays = objSave.ExtraDays;
                    lRetCode = oData.Update();
                }
                else
                {
                    oData.PaymentTermsGroupName = objSave.PymntGroup;
                    oData.BaselineDate = objSave.BslineDate;
                    oData.StartFrom = objSave.PayDuMonth;
                    oData.NumberOfAdditionalMonths = objSave.ExtraMonth;
                    oData.NumberOfAdditionalDays = objSave.ExtraDays;
                    lRetCode = oData.Add();
                }



                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }
                else
                {
                    GroupNum = GetDataColumn("GroupNum", PaymentTerms, "PymntGroup", oData.PaymentTermsGroupName);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateItemMaster(ItemMaster objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.Items oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);

                if (oData.GetByKey(objSave.ItemCode))
                {
                    oData.ItemCode = objSave.ItemCode;
                    oData.ItemName = objSave.ItemName;
                    if (!string.IsNullOrEmpty(objSave.FrgnName)) oData.ForeignName = objSave.FrgnName;
                    oData.ItemType = objSave.ItemType;
                    if (!string.IsNullOrEmpty(objSave.ItmsGrpCod)) oData.ItemsGroupCode = Convert.ToInt32(objSave.ItmsGrpCod);
                    if (!string.IsNullOrEmpty(objSave.UserText)) oData.User_Text = objSave.UserText;
                    oData.WTLiable = objSave.WTLiable;
                    oData.PurchaseItem = objSave.PrchseItem;
                    oData.SalesItem = objSave.SellItem;
                    oData.InventoryItem = objSave.InvntItem;
                    oData.ManageSerialNumbers = objSave.ManSerNum;
                    oData.ManageBatchNumbers = objSave.ManBtchNum;
                    oData.SRIAndBatchManageMethod = objSave.MngMethod;
                    oData.Valid = objSave.ValidFor;
                    oData.Frozen = objSave.FrozenFor;
                    if (!string.IsNullOrEmpty(objSave.CardCode)) oData.Mainsupplier = objSave.CardCode;
                    if (!string.IsNullOrEmpty(objSave.SuppCatNum)) oData.SupplierCatalogNo = objSave.SuppCatNum;
                    if (!string.IsNullOrEmpty(objSave.BuyUnitMsr)) oData.PurchaseUnit = objSave.BuyUnitMsr;
                    oData.PurchaseItemsPerUnit = Convert.ToDouble(objSave.NumInBuy);
                    if (!string.IsNullOrEmpty(objSave.PurPackMsr)) oData.PurchasePackagingUnit = objSave.PurPackMsr;
                    oData.PurchaseQtyPerPackUnit = Convert.ToDouble(objSave.PurPackUn);
                    if (!string.IsNullOrEmpty(objSave.VatGroupPu)) oData.PurchaseVATGroup = objSave.VatGroupPu; //NIG
                    if (!string.IsNullOrEmpty(objSave.VatGourpSa)) oData.SalesVATGroup = objSave.VatGourpSa; //NOG
                    if (!string.IsNullOrEmpty(objSave.SalUnitMsr)) oData.SalesUnit = objSave.SalUnitMsr;
                    oData.SalesItemsPerUnit = Convert.ToDouble(objSave.NumInSale);
                    if (!string.IsNullOrEmpty(objSave.SalPackMsr)) oData.SalesPackagingUnit = objSave.SalPackMsr;
                    oData.SalesQtyPerPackUnit = Convert.ToDouble(objSave.SalPackUn);
                    oData.GLMethod = objSave.GLMethod;
                    if (!string.IsNullOrEmpty(objSave.InvntryUom)) oData.InventoryUOM = objSave.InvntryUom;
                    if (!string.IsNullOrEmpty(objSave.IWeight1)) oData.InventoryWeight = Convert.ToDouble(objSave.IWeight1);
                    oData.ManageStockByWarehouse = objSave.ByWh;
                    oData.CostAccountingMethod = objSave.EvalSystem;
                    //oData.InventoryAccount = objSave.BalInvntAc;
                    //oData.ExpensesAc = objSave.ExpensesAc;
                    //oData.DecreasAc = objSave.DecreasAc;
                    //oData.IncreasAc = objSave.IncreasAc;
                    //oData.APCMAct = objSave.APCMAct;
                    

                    lRetCode = oData.Update();
                }
                else
                {
                    oData.Series = Convert.ToInt32(objSave.Series);
                    oData.ItemCode = objSave.ItemCode;
                    oData.ItemName = objSave.ItemName;
                    if (!string.IsNullOrEmpty(objSave.FrgnName)) oData.ForeignName = objSave.FrgnName;
                    oData.ItemType = objSave.ItemType;
                    if (!string.IsNullOrEmpty(objSave.ItmsGrpCod)) oData.ItemsGroupCode = Convert.ToInt32(objSave.ItmsGrpCod);
                    oData.User_Text = objSave.UserText;
                    oData.WTLiable = objSave.WTLiable;
                    oData.PurchaseItem = objSave.PrchseItem;
                    oData.SalesItem = objSave.SellItem;
                    oData.InventoryItem = objSave.InvntItem;
                    oData.ManageSerialNumbers = objSave.ManSerNum;
                    oData.ManageBatchNumbers = objSave.ManBtchNum;
                    oData.SRIAndBatchManageMethod = objSave.MngMethod;
                    oData.Valid = objSave.ValidFor;
                    oData.Frozen = objSave.FrozenFor;
                    if (!string.IsNullOrEmpty(objSave.CardCode)) oData.Mainsupplier = objSave.CardCode;
                    if (!string.IsNullOrEmpty(objSave.SuppCatNum)) oData.SupplierCatalogNo = objSave.SuppCatNum;
                    if (!string.IsNullOrEmpty(objSave.BuyUnitMsr)) oData.PurchaseUnit = objSave.BuyUnitMsr;
                    oData.PurchaseItemsPerUnit = Convert.ToDouble(objSave.NumInBuy);
                    if (!string.IsNullOrEmpty(objSave.PurPackMsr)) oData.PurchasePackagingUnit = objSave.PurPackMsr;
                    oData.PurchaseQtyPerPackUnit = Convert.ToDouble(objSave.PurPackUn);
                    oData.PurchaseVATGroup = objSave.VatGroupPu; //NIG
                    oData.SalesVATGroup = objSave.VatGourpSa; //NOG
                    if (!string.IsNullOrEmpty(objSave.SalUnitMsr)) oData.SalesUnit = objSave.SalUnitMsr;
                    oData.SalesItemsPerUnit = Convert.ToDouble(objSave.NumInSale);
                    if (!string.IsNullOrEmpty(objSave.SalPackMsr)) oData.SalesPackagingUnit = objSave.SalPackMsr;
                    oData.SalesQtyPerPackUnit = Convert.ToDouble(objSave.SalPackUn);
                    oData.GLMethod = objSave.GLMethod;
                    if (!string.IsNullOrEmpty(objSave.InvntryUom)) oData.InventoryUOM = objSave.InvntryUom;
                    if (!string.IsNullOrEmpty(objSave.IWeight1)) oData.InventoryWeight = Convert.ToDouble(objSave.IWeight1);
                    oData.ManageStockByWarehouse = objSave.ByWh;
                    oData.CostAccountingMethod = objSave.EvalSystem;
                    //oData.InventoryAccount = objSave.BalInvntAc;
                    //oData.ExpensesAc = objSave.ExpensesAc;
                    //oData.DecreasAc = objSave.DecreasAc;
                    //oData.IncreasAc = objSave.IncreasAc;
                    //oData.APCMAct = objSave.APCMAct;
                    lRetCode = oData.Add();
                }



                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateItemToWarehouse(ItemToWarehouse objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            bool IsUpdate = false;

            try
            {
                SAPbobsCOM.Items oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);


                if (oData.GetByKey(objSave.ItemCode))
                {
                    SAPbobsCOM.IItemWarehouseInfo oDataS = oData.WhsInfo;

                    SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecordSet.DoQuery($@"

select
     WhsCode
from OITW
where ItemCode = '{objSave.ItemCode}'
");

                    List<Dictionary<string, object>> bpAItems = new List<Dictionary<string, object>>();
                    if (oRecordSet.RecordCount > 0)
                    {
                        Dictionary<string, object> bpAItem;

                        while (!oRecordSet.EoF)
                        {
                            bpAItem = new Dictionary<string, object>();

                            for (int i = 0; i < oRecordSet.Fields.Count; i++)
                            {
                                bpAItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                            }

                            bpAItems.Add(bpAItem);

                            oRecordSet.MoveNext();
                        }
                    }
                    int lineaw = 0;
                    foreach (Dictionary<string, object> IMW in bpAItems)
                    {
                        if (IMW["WhsCode"].ToString() == objSave.WhsCode)
                        {
                            oDataS.SetCurrentLine(Convert.ToInt32(lineaw));
                            IsUpdate = true;
                            oDataS.WarehouseCode = objSave.WhsCode;
                            oDataS.Locked = objSave.Locked;
                            oDataS.InventoryAccount = objSave.BalInvntAc;
                            oDataS.CostAccount = objSave.SaleCostAc;
                            oDataS.TransferAccount = objSave.TransferAc;
                            oDataS.PriceDifferenceAcc = objSave.PriceDifAc;
                            oDataS.ExpensesAccount = objSave.ExpensesAC;
                            oDataS.PurchaseCreditAcc = objSave.APCMAct;
                            oDataS.RevenuesAccount = objSave.RevenuesAc;
                            oDataS.SalesCreditAcc = objSave.ARCMAct;
                            oDataS.GLIncreaseAcct = objSave.IncresGlAc;
                            oDataS.GLDecreaseAcct = objSave.DecresGlAc;
                        }

                        lineaw++;
                    }

                    if (!IsUpdate)
                    {
                        oDataS.Add();
                        oDataS.WarehouseCode = objSave.WhsCode;
                        oDataS.Locked = objSave.Locked;
                        oDataS.InventoryAccount = objSave.BalInvntAc;
                        oDataS.CostAccount = objSave.SaleCostAc;
                        oDataS.TransferAccount = objSave.TransferAc;
                        oDataS.PriceDifferenceAcc = objSave.PriceDifAc;
                        oDataS.ExpensesAccount = objSave.ExpensesAC;
                        oDataS.PurchaseCreditAcc = objSave.APCMAct;
                        oDataS.RevenuesAccount = objSave.RevenuesAc;
                        oDataS.SalesCreditAcc = objSave.ARCMAct;
                        oDataS.GLIncreaseAcct = objSave.IncresGlAc;
                        oDataS.GLDecreaseAcct = objSave.DecresGlAc;
                    }

                    lRetCode = oData.Update();

                }
                else
                {
                    throw new Exception("Unable to find " + objSave.ItemCode + " in the database");
                }

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateItemGroupMaster(ItemGroup objSave, out int InternalErrorCode, out string InternalErrorMessage, out string ItemGroupCode)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string SAP_CODE = string.Empty;
            try
            {
                SAPbobsCOM.ItemGroups oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItemGroups);
                SAPTABLE ItemGroups = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oItemGroups));

                if (objSave.ItmsGrpCod == 0)
                {
                    SAP_CODE = GetDataColumn("ItmsGrpCod", ItemGroups, "ItmsGrpNam", oData.GroupName);
                }
                else
                {
                    SAP_CODE = objSave.ItmsGrpCod.ToString();
                }

                if (oData.GetByKey(objSave.ItmsGrpCod))
                {
                    oData.GroupName = objSave.ItmsGrpNam.Length > 20 ? objSave.ItmsGrpNam.Substring(0, 20) : objSave.ItmsGrpNam;
                    oData.UserFields.Fields.Item("U_ICON_GRP_NAME").Value = objSave.ItmsGrpNam;
                    oData.InventoryAccount = objSave.BalInvntAc;
                    oData.CostAccount = objSave.SaleCostAc;
                    oData.TransfersAccount = objSave.TransferAc;
                    oData.PriceDifferencesAccount = objSave.PriceDifAc;
                    oData.ExpensesAccount = objSave.ExpensesAC;
                    oData.PurchaseCreditAcc = objSave.APCMAct;
                    oData.RevenuesAccount = objSave.RevenuesAc;
                    oData.SalesCreditAcc = objSave.ARCMAct;
                    oData.IncreaseGLAccount = objSave.IncresGlAc;
                    oData.DecreaseGLAccount = objSave.DecresGlAc;
                    lRetCode = oData.Update();
                }
                else
                {
                    oData.GroupName = objSave.ItmsGrpNam.Length > 20 ? objSave.ItmsGrpNam.Substring(0, 20) : objSave.ItmsGrpNam;
                    oData.UserFields.Fields.Item("U_ICON_GRP_NAME").Value = objSave.ItmsGrpNam;
                    oData.InventoryAccount = objSave.BalInvntAc;
                    oData.CostAccount = objSave.SaleCostAc;
                    oData.TransfersAccount = objSave.TransferAc;
                    oData.PriceDifferencesAccount = objSave.PriceDifAc;
                    oData.ExpensesAccount = objSave.ExpensesAC;
                    oData.PurchaseCreditAcc = objSave.APCMAct;
                    oData.RevenuesAccount = objSave.RevenuesAc;
                    oData.SalesCreditAcc = objSave.ARCMAct;
                    oData.IncreaseGLAccount = objSave.IncresGlAc;
                    oData.DecreaseGLAccount = objSave.DecresGlAc;
                    lRetCode = oData.Add();
                }

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }
                else
                {
                    ItemGroupCode = GetDataColumn("ItmsGrpCod", ItemGroups, "ItmsGrpNam", oData.GroupName);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateBusinessPartner(BusinessPartner objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.BusinessPartners oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                if (oData.GetByKey(objSave.CardCode))
                {
                    oData.CardCode = objSave.CardCode;
                    oData.CardName = objSave.CardName;
                    if (!string.IsNullOrEmpty(objSave.CardFNme)) oData.CardForeignName = objSave.CardFNme;
                    oData.CardType = objSave.CardType;
                    oData.CompanyPrivate = objSave.CmpPrivate;
                    if (!string.IsNullOrEmpty(objSave.GroupCode)) oData.GroupCode = Convert.ToInt32(objSave.GroupCode);
                    if (!string.IsNullOrEmpty(objSave.Currency)) oData.Currency = objSave.Currency;
                    if (!string.IsNullOrEmpty(objSave.LicTradNum)) oData.FederalTaxID = objSave.LicTradNum;
                    if (!string.IsNullOrEmpty(objSave.Remarks)) oData.FreeText    = objSave.Remarks;
                    if (!string.IsNullOrEmpty(objSave.Phone1)) oData.Phone1 = objSave.Phone1;
                    if (!string.IsNullOrEmpty(objSave.Phone2)) oData.Phone2 = objSave.Phone2;
                    if (!string.IsNullOrEmpty(objSave.Fax)) oData.Fax = objSave.Fax;
                    if (!string.IsNullOrEmpty(objSave.E_Mail)) oData.EmailAddress = objSave.E_Mail;
                    if (!string.IsNullOrEmpty(objSave.IntrntSite)) oData.Website = objSave.IntrntSite;
                    if (!string.IsNullOrEmpty(objSave.AliasName)) oData.AliasName = objSave.AliasName;
                    if (!string.IsNullOrEmpty(objSave.CntctPrsn)) oData.ContactPerson = objSave.CntctPrsn;
                    if (!string.IsNullOrEmpty(objSave.Territory)) oData.Territory = Convert.ToInt32(objSave.Territory);
                    if (!string.IsNullOrEmpty(objSave.GlblLocNum)) oData.GlobalLocationNumber = objSave.GlblLocNum;
                    oData.Valid = objSave.ValidFor;
                    oData.Frozen = objSave.FrozenFor;
                    if (!string.IsNullOrEmpty(objSave.GroupNum)) oData.PayTermsGrpCode = Convert.ToInt32(objSave.GroupNum);
                    if (!string.IsNullOrEmpty(objSave.Discount)) oData.DiscountPercent = Convert.ToDouble(objSave.Discount);
                    if (!string.IsNullOrEmpty(objSave.DebPayAcct)) oData.DebitorAccount = objSave.DebPayAcct;
                    oData.VatLiable = objSave.VatStatus;
                    if (!string.IsNullOrEmpty(objSave.ECVatGroup)) oData.VatGroup = objSave.ECVatGroup;
                    oData.SubjectToWithholdingTax = objSave.WTLiable;
                    oData.TypeReport = objSave.TypWTReprt;
                    oData.Update();
                }
                else
                {
                    oData.CardCode = objSave.CardCode;
                    oData.CardName = objSave.CardName;
                    if (!string.IsNullOrEmpty(objSave.CardFNme)) oData.CardForeignName = objSave.CardFNme;
                    oData.CardType = objSave.CardType;
                    oData.CompanyPrivate = objSave.CmpPrivate;
                    if (!string.IsNullOrEmpty(objSave.GroupCode)) oData.GroupCode = Convert.ToInt32(objSave.GroupCode);
                    if (!string.IsNullOrEmpty(objSave.Currency)) oData.Currency = objSave.Currency;
                    if (!string.IsNullOrEmpty(objSave.LicTradNum)) oData.FederalTaxID = objSave.LicTradNum;
                    if (!string.IsNullOrEmpty(objSave.Remarks)) oData.FreeText = objSave.Remarks;
                    if (!string.IsNullOrEmpty(objSave.Phone1)) oData.Phone1 = objSave.Phone1;
                    if (!string.IsNullOrEmpty(objSave.Phone2)) oData.Phone2 = objSave.Phone2;
                    if (!string.IsNullOrEmpty(objSave.Fax)) oData.Fax = objSave.Fax;
                    if (!string.IsNullOrEmpty(objSave.E_Mail)) oData.EmailAddress = objSave.E_Mail;
                    if (!string.IsNullOrEmpty(objSave.IntrntSite)) oData.Website = objSave.IntrntSite;
                    if (!string.IsNullOrEmpty(objSave.AliasName)) oData.AliasName = objSave.AliasName;
                    if (!string.IsNullOrEmpty(objSave.CntctPrsn)) oData.ContactPerson = objSave.CntctPrsn;
                    if (!string.IsNullOrEmpty(objSave.Territory)) oData.Territory = Convert.ToInt32(objSave.Territory);
                    if (!string.IsNullOrEmpty(objSave.GlblLocNum)) oData.GlobalLocationNumber = objSave.GlblLocNum;
                    oData.Valid = objSave.ValidFor;
                    oData.Frozen = objSave.FrozenFor;
                    if (!string.IsNullOrEmpty(objSave.GroupNum)) oData.PayTermsGrpCode = Convert.ToInt32(objSave.GroupNum);
                    if (!string.IsNullOrEmpty(objSave.Discount)) oData.DiscountPercent = Convert.ToDouble(objSave.Discount);
                    if (!string.IsNullOrEmpty(objSave.DebPayAcct)) oData.DebitorAccount = objSave.DebPayAcct;
                    oData.VatLiable = objSave.VatStatus;
                    if (!string.IsNullOrEmpty(objSave.ECVatGroup)) oData.VatGroup = objSave.ECVatGroup;
                    oData.SubjectToWithholdingTax = objSave.WTLiable;
                    oData.TypeReport = objSave.TypWTReprt;
                    oData.Add();
                }

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateBusinessContactPersons(ContactPersons objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            bool IsUpdate = false;

            try
            {
                SAPbobsCOM.BusinessPartners oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);


                if (oData.GetByKey(objSave.CardCode))
                {
                    SAPbobsCOM.ContactEmployees oDataC = oData.ContactEmployees;

                    SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecordSet.DoQuery($@"

select
     Name
from OCPR
where cardcode = '{objSave.CardCode}'
");

                    List<Dictionary<string, object>> bpAItems = new List<Dictionary<string, object>>();
                    if (oRecordSet.RecordCount > 0)
                    {
                        Dictionary<string, object> bpAItem;

                        while (!oRecordSet.EoF)
                        {
                            bpAItem = new Dictionary<string, object>();

                            for (int i = 0; i < oRecordSet.Fields.Count; i++)
                            {
                                bpAItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                            }

                            bpAItems.Add(bpAItem);

                            oRecordSet.MoveNext();
                        }
                    }

                    foreach (Dictionary<string, object> bpA in bpAItems)
                    {
                        if (bpA["Name"].ToString() == objSave.Name)
                        {
                            oDataC.SetCurrentLine(Convert.ToInt32(objSave.CurrentLine));
                            IsUpdate = true;
                            oDataC.FirstName = objSave.FirstName;
                            oDataC.LastName = objSave.LastName;
                            oDataC.Position = objSave.Position;
                            oDataC.Phone1 = objSave.Tel1;
                            oDataC.Phone2 = objSave.Tel2;
                            oDataC.MobilePhone = objSave.Cellolar;
                            oDataC.Fax = objSave.Fax;
                            oDataC.E_Mail = objSave.E_MaiL;
                            oDataC.Remarks1 = objSave.Notes1;
                            oDataC.Remarks2 = objSave.Notes2;
                            oDataC.Active = string.IsNullOrEmpty((string)objSave.Active) || (string)objSave.Active == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES;
                        }
                    }    

                    if (!IsUpdate)
                    {
                        oDataC.Add();
                        oDataC.SetCurrentLine(Convert.ToInt32(objSave.CurrentLine));
                        oDataC.Name = objSave.Name;
                        oDataC.FirstName = objSave.FirstName;
                        oDataC.LastName = objSave.LastName;
                        oDataC.Position = objSave.Position;
                        oDataC.Phone1 = objSave.Tel1;
                        oDataC.Phone2 = objSave.Tel2;
                        oDataC.MobilePhone = objSave.Cellolar;
                        oDataC.Fax = objSave.Fax;
                        oDataC.E_Mail = objSave.E_MaiL;
                        oDataC.Remarks1 = objSave.Notes1;
                        oDataC.Remarks2 = objSave.Notes2;
                        oDataC.Active = string.IsNullOrEmpty((string)objSave.Active) || (string)objSave.Active == "N" ? SAPbobsCOM.BoYesNoEnum.tNO : SAPbobsCOM.BoYesNoEnum.tYES;
                    }

                    lRetCode = oData.Update();

                }
                else
                {
                    throw new Exception("Unable to find " + objSave.CardCode + " in the database");
                }

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateBusinessAddress(BusinessPartnerAddress objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            int Cnt = 0;
            bool IsUpdate = false;
            try
            {
                SAPbobsCOM.BusinessPartners oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);


                if (oData.GetByKey(objSave.CardCode))
                {
                    SAPbobsCOM.BPAddresses oDataA = oData.Addresses;

                    SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecordSet.DoQuery($@"

select
     Address
    ,LineNum
    ,AdresType
from CRD1
where cardcode = '{objSave.CardCode}'
");

                    List<Dictionary<string, object>> bpAItems = new List<Dictionary<string, object>>();
                    if (oRecordSet.RecordCount > 0)
                    {
                        Dictionary<string, object> bpAItem;

                        while (!oRecordSet.EoF)
                        {
                            bpAItem = new Dictionary<string, object>();

                            for (int i = 0; i < oRecordSet.Fields.Count; i++)
                            {
                                bpAItem.Add(oRecordSet.Fields.Item(i).Name, oRecordSet.Fields.Item(i).Value);
                            }

                            bpAItems.Add(bpAItem);

                            oRecordSet.MoveNext();
                        }
                    }

                    foreach(Dictionary<string, object> bpA in bpAItems)
                    {
                        if(bpA["Address"].ToString() == objSave.Address && bpA["AdresType"].ToString().ToLower() == objSave.AdresType.ToLower())
                        {
                            oDataA.SetCurrentLine(Convert.ToInt32(objSave.CurrentLine));
                            IsUpdate = true;
                            oDataA.AddressName = objSave.Address;
                            oDataA.AddressName2 = objSave.AddressesName;
                            oDataA.AddressType = objSave.AdresType.ToLower() == "b" ? SAPbobsCOM.BoAddressType.bo_BillTo : SAPbobsCOM.BoAddressType.bo_ShipTo;
                            oDataA.Street = objSave.Street;
                            oDataA.Block = objSave.Block;
                            oDataA.City = objSave.City;
                            oDataA.County = objSave.County;
                            oDataA.Country = objSave.Country;
                            oDataA.ZipCode = objSave.ZipCode;
                        }
                    }

                    if (!IsUpdate)
                    {
                       
                        oDataA.Add();
                        oDataA.SetCurrentLine(Convert.ToInt32(objSave.CurrentLine));
                        oDataA.AddressName = objSave.Address;
                        oDataA.AddressName2 = objSave.AddressesName;
                        oDataA.AddressType = objSave.AdresType.ToLower() == "b" ? SAPbobsCOM.BoAddressType.bo_BillTo : SAPbobsCOM.BoAddressType.bo_ShipTo;
                        oDataA.Street = objSave.Street;
                        oDataA.Block = objSave.Block;
                        oDataA.City = objSave.City;
                        oDataA.County = objSave.County;
                        oDataA.Country = objSave.Country;
                        oDataA.ZipCode = objSave.ZipCode;
                        
                    }

                    if(objSave.AdresType.ToLower() == "b")
                    {
                        if (objSave.Default == "Y") { oData.BilltoDefault = objSave.Address; }
                    }
                    else
                    {
                        if (objSave.Default == "Y") { oData.ShipToDefault = objSave.Address; }
                    }
                    
                    lRetCode = oData.Update();

                }
                else
                {
                    throw new Exception("Unable to find " + objSave.CardCode + " in the database");
                }

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateBusinessPartnerGroups(BusinessPartnerGroups objSave, out int InternalErrorCode, out string InternalErrorMessage, out string GroupCode)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string SAP_CODE = string.Empty;
            try
            {
                SAPbobsCOM.BusinessPartnerGroups oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartnerGroups);
                SAPTABLE BPGroups = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oBusinessPartnerGroups));

                if (string.IsNullOrEmpty(objSave.GroupCode) || Convert.ToInt32(objSave.GroupCode) == 0)
                {
                    SAP_CODE = GetDataColumn("GroupCode", BPGroups, "GroupName", oData.Name);
                }
                else
                {
                    SAP_CODE = objSave.GroupCode;
                }


                if (oData.GetByKey(Convert.ToInt32(SAP_CODE)))
                {
                    oData.Name = objSave.GroupNum.Length > 20 ? objSave.GroupNum.Substring(0, 20) : objSave.GroupNum;
                    if (!string.IsNullOrEmpty(objSave.GroupType)) oData.Type = objSave.GroupType.ToLower() == "c" ? SAPbobsCOM.BoBusinessPartnerGroupTypes.bbpgt_CustomerGroup : SAPbobsCOM.BoBusinessPartnerGroupTypes.bbpgt_VendorGroup;
                    lRetCode = oData.Update();
                }
                else
                {
                    oData.Name = objSave.GroupNum.Length > 20 ? objSave.GroupNum.Substring(0, 20) : objSave.GroupNum;
                    if (!string.IsNullOrEmpty(objSave.GroupType)) oData.Type = objSave.GroupType.ToLower() == "c" ? SAPbobsCOM.BoBusinessPartnerGroupTypes.bbpgt_CustomerGroup : SAPbobsCOM.BoBusinessPartnerGroupTypes.bbpgt_VendorGroup;
                    lRetCode = oData.Add();
                }


                GroupCode = GetDataColumn("GroupCode", BPGroups, "GroupName", oData.Name);

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateBusinessBank(BusinessBank objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.BusinessPartners oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                oData.GetByKey(objSave.CardCode);
                oData.BPBankAccounts.Add();
                oData.BPBankAccounts.AccountNo = objSave.Account;
                oData.BPBankAccounts.AccountName = objSave.AcctName;
                oData.BPBankAccounts.Country = objSave.Country;
                oData.BPBankAccounts.BankCode = objSave.BankCode;
               
                if (!string.IsNullOrEmpty(objSave.Branch)) oData.BPBankAccounts.Branch = objSave.Branch;
                if (!string.IsNullOrEmpty(objSave.IBAN)) oData.BPBankAccounts.IBAN = objSave.IBAN;
                lRetCode = oData.Update();

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateBusinessPartnerWithHoldingTax(BusinessParnerWithHoldingTax objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.BusinessPartners oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                oData.GetByKey(objSave.CardCode);
                oData.BPWithholdingTax.Add();
                oData.BPWithholdingTax.WTCode = objSave.WTCode;
                if (objSave.Default == "Y") oData.WTCode = objSave.WTCode;
                lRetCode = oData.Update();

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateVatBranchMaster(BusinessPartner objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                SAPbobsCOM.BusinessPartners oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                //if (oData.GetByKey(objSave.CardCode))
                //{
                //    oData.BPBankAccounts.Country = objSave.ItmsGrpNam;
                //    oData.BPBankAccounts.BankCode = objSave.;
                //    oData.CostAccount = objSave.SaleCostAc;
                //    oData.TransfersAccount = objSave.TransferAc;
                //    oData.PriceDifferencesAccount = objSave.PriceDifAc;
                //    lRetCode = oData.Update();
                //}
                //else
                //{
                //    oData.GroupName = objSave.ItmsGrpNam;
                //    oData.InventoryAccount = objSave.BalInvntAc;
                //    oData.CostAccount = objSave.SaleCostAc;
                //    oData.TransfersAccount = objSave.TransferAc;
                //    oData.PriceDifferencesAccount = objSave.PriceDifAc;
                //    lRetCode = oData.Add();
                //}



                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateBanks(Banks objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string SAP_CODE = string.Empty;
            try
            {
                SAPbobsCOM.Banks oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBanks);
                SAPTABLE Banks = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oBanks));
                SAP_CODE = GetDataColumn("AbsEntry", Banks, "BankCode", objSave.BankCode);

                if (oData.GetByKey(Convert.ToInt32(SAP_CODE)))
                {
                    oData.CountryCode = (objSave.CountryCod.Length > 8 ? objSave.CountryCod.Substring(0, 8) : objSave.CountryCod);
                    oData.BankName = (objSave.BankName.Length > 250 ? objSave.BankName.Substring(0, 250) : objSave.BankName);
                    oData.Update();
                }
                else
                {
                    oData.CountryCode = (objSave.CountryCod.Length > 8 ? objSave.CountryCod.Substring(0, 8) : objSave.CountryCod);
                    oData.BankCode = (objSave.BankCode.Length > 30 ? objSave.BankCode.Substring(0, 30) : objSave.BankCode);
                    oData.BankName = (objSave.BankName.Length > 250 ? objSave.BankName.Substring(0, 250) : objSave.BankName);
                    oData.Add();
                }

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateUpdateHouseBankAccount(HouseBankAccount objSave, out int InternalErrorCode, out string InternalErrorMessage)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string SAP_CODE = string.Empty;
            try
            {
                SAPbobsCOM.HouseBankAccounts oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oHouseBankAccounts);
                SAPTABLE Banks = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oHouseBankAccounts));
                SAP_CODE = GetDataColumn2Condition("AbsEntry", Banks, "BankCode", objSave.BankCode, "Account", objSave.Account);

                if (oData.GetByKey(Convert.ToInt32(SAP_CODE)))
                {
                    oData.Branch = (objSave.Branch.Length > 30 ? objSave.Branch.Substring(0, 30) : objSave.Branch);
                    oData.AccNo = (objSave.Account.Length > 50 ? objSave.Account.Substring(0, 50) : objSave.Account);
                    oData.AccountName = (objSave.AcctName.Length > 250 ? objSave.AcctName.Substring(0, 250) : objSave.AcctName);
                    oData.GLAccount = (objSave.GLAccount.Length > 8 ? objSave.GLAccount.Substring(0, 8) : objSave.GLAccount);
                    oData.Update();
                }
                else
                {
                    oData.Branch = (objSave.Branch.Length > 30 ? objSave.Branch.Substring(0, 30) : objSave.Branch);
                    oData.AccNo = (objSave.Account.Length > 50 ? objSave.Account.Substring(0, 50) : objSave.Account);
                    oData.AccountName = (objSave.AcctName.Length > 250 ? objSave.AcctName.Substring(0, 250) : objSave.AcctName);
                    oData.GLAccount = (objSave.GLAccount.Length > 8 ? objSave.GLAccount.Substring(0, 8) : objSave.GLAccount);
                    oData.Add();
                }

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }
        #endregion

        #region #### Interface AP ####

        #region #### Goods Receipt PO ####
        public void CreateDocumentAP(Document Doc, out string DocEntry, out string DocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            SAPbobsCOM.Documents oDoc = null;
            try
            {
                this.ConnectCompanyDB();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("connected to a company"))
                {
                    this.DisConnectCompanyDB();
                    this.ConnectCompanyDB();
                }

            }
            oCompany.StartTransaction();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));
                // Header (Document)
                oDoc = oCompany.GetBusinessObject(Doc.DocType);
                oDoc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                if (Doc.DocType != SAPbobsCOM.BoObjectTypes.oPurchaseRequest)
                {
                    oDoc.CardCode = Doc.CardCode;
                }
                oDoc.DocDate = Doc.PostingDate;
                oDoc.DocDueDate = Doc.DocDueDate;
                oDoc.TaxDate = Doc.DocumentDate;
                oDoc.UserFields.Fields.Item("U_ICON_RefNo").Value = Doc.UDF_RefNo;
                oDoc.UserFields.Fields.Item("U_VATPeriod").Value = Doc.UDF_VatPeriodDate.ToString("yyyy-MM");
                if (!string.IsNullOrEmpty(Doc.U_ICON_IssueReason)) oDoc.UserFields.Fields.Item("U_ICON_IssueReason").Value = Doc.U_ICON_IssueReason;
                if (!string.IsNullOrEmpty(Doc.ICON_DeliveryNo)) oDoc.UserFields.Fields.Item("U_ICON_DeliveryNo").Value = Doc.ICON_DeliveryNo;
                if (!string.IsNullOrEmpty(Doc.U_ICON_CMPO)) oDoc.UserFields.Fields.Item("U_ICON_CMPO").Value = Doc.U_ICON_CMPO;
                if (!string.IsNullOrEmpty(Doc.U_ICON_CMWO)) oDoc.UserFields.Fields.Item("U_ICON_CMWO").Value = Doc.U_ICON_CMWO;
                if (!string.IsNullOrEmpty(Doc.UDF_Project)) oDoc.UserFields.Fields.Item("U_ICON_Project").Value = Doc.UDF_Project;
                if (!string.IsNullOrEmpty(Doc.U_ICON_CMAtth)) oDoc.UserFields.Fields.Item("U_ICON_CMAtth").Value = Doc.U_ICON_CMAtth;

                if (!string.IsNullOrEmpty(Doc.DocTotal) && Convert.ToDecimal(Doc.DocTotal) > 0)
                {
                    oDoc.DocTotal = Convert.ToDouble(Doc.DocTotal);
                }
                if (!string.IsNullOrEmpty(Doc.Remark))
                {
                    oDoc.Comments = Doc.Remark;
                }
                if (!String.IsNullOrEmpty(Doc.NumAtCard))
                {
                    oDoc.NumAtCard = Doc.NumAtCard;
                }
                if (!string.IsNullOrEmpty(Doc.DiscPrcnt)) oDoc.DiscountPercent = Convert.ToDouble(Doc.DiscPrcnt);
                if (!string.IsNullOrEmpty(Doc.Rounding)) oDoc.Rounding = Doc.Rounding.ToString().ToUpper() == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                if (!string.IsNullOrEmpty(Doc.RoundDif)) oDoc.RoundingDiffAmount = Convert.ToDouble(Doc.RoundDif);
                if (!string.IsNullOrEmpty(Doc.PayToCode)) oDoc.PayToCode = Doc.PayToCode;
                if (!string.IsNullOrEmpty(Doc.Address)) oDoc.Address = Doc.Address;
                if (!string.IsNullOrEmpty(Doc.Address2)) oDoc.Address2 = Doc.Address2;
                if (!string.IsNullOrEmpty(Doc.GroupNum)) oDoc.GroupNumber = Convert.ToInt32(Doc.GroupNum);
                //if (Doc.DocType == SAPbobsCOM.BoObjectTypes.oPurchaseRequest)
                //{
                //    oDoc.RequriedDate = Doc.RequireDate;
                //}

                //if (Doc.DocType == SAPbobsCOM.BoObjectTypes.oDownPayments)
                //{
                //    oDoc.DownPaymentType = SAPbobsCOM.DownPaymentTypeEnum.dptInvoice;
                //    oDoc.DownPaymentAmount = Doc.DownPaymentAmt;
                //}

                bool firstLine = true;

                // [TIPS] - Caching Variable
                SAPbobsCOM.Document_Lines oLines = oDoc.Lines;
                // Details (Document Lines)
                foreach (DocumentLine docLine in Doc.Lines)
                {
                    if (firstLine)
                        firstLine = false;
                    else
                        oLines.Add();

                    //oLines.ItemCode = GetItemCode(docLine.ItemName);
                    oLines.ItemCode = docLine.ItemCode;
                    if (!string.IsNullOrEmpty(docLine.ItemDescription))
                    {
                        //oLines.ItemDescription = docLine.ItemDescription;
                        //oDoc.SpecialLines.LineText = docLine.ItemDescription;
                        oLines.ItemDetails = docLine.ItemDescription;
                    }
                    oLines.Quantity = docLine.Qty;
                    if (!String.IsNullOrEmpty(docLine.UnitPrice))
                    {
                        oLines.UnitPrice = Convert.ToDouble(docLine.UnitPrice);
                    }

                    if (!String.IsNullOrEmpty(docLine.TaxCode))
                    {
                        oLines.VatGroup = docLine.TaxCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.BaseEntry))
                    {
                        oLines.BaseType = docLine.BaseType;
                        oLines.BaseEntry = Convert.ToInt32(docLine.BaseEntry);
                    }
                    if (!String.IsNullOrEmpty(docLine.LineNumber))
                    {
                        oLines.BaseLine = Convert.ToInt32(docLine.LineNumber);
                    }
                    if (!String.IsNullOrEmpty(docLine.FreeTxt))
                    {
                        oLines.FreeText = docLine.FreeTxt;
                    }
                    if (!String.IsNullOrEmpty(docLine.WhsCode))
                    {
                        oLines.WarehouseCode = docLine.WhsCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.VatSum))
                    {
                        oLines.TaxTotal = Convert.ToDouble(docLine.VatSum);
                    }
                    if (!String.IsNullOrEmpty(docLine.ProjectCode))
                    {
                        oLines.ProjectCode = docLine.ProjectCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode))
                    {
                        oLines.CostingCode = docLine.OcrCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode2))
                    {
                        oLines.CostingCode2 = docLine.OcrCode2;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode3))
                    {
                        oLines.CostingCode3 = docLine.OcrCode3;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode4))
                    {
                        oLines.CostingCode4 = docLine.OcrCode4;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode5))
                    {
                        oLines.CostingCode5 = docLine.OcrCode5;
                    }
                    if (!String.IsNullOrEmpty(docLine.AccountCode))
                    {
                        oLines.AccountCode = docLine.AccountCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.DiscountPerRow))
                    {
                        oLines.DiscountPercent = Convert.ToDouble(docLine.DiscountPerRow);
                    }
                    if (!String.IsNullOrEmpty(docLine.LineTotal))
                    {
                        oLines.LineTotal = Convert.ToDouble(docLine.LineTotal);
                    }
                    oLines.UnitsOfMeasurment = 1;
                    //oLines.DeferredTax = docLine.IsDeferVAT ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO;
                    if (!String.IsNullOrEmpty(docLine.U_ICON_IssueReason))
                    {
                        oLines.UserFields.Fields.Item("U_ICON_IssueReason").Value = docLine.U_ICON_IssueReason;
                    }
                    if (!String.IsNullOrEmpty(docLine.U_ICON_PT))
                    {
                        oLines.UserFields.Fields.Item("U_ICON_PT").Value = docLine.U_ICON_PT;
                    }
                    if (!String.IsNullOrEmpty(docLine.U_ICON_BF))
                    {
                        oLines.UserFields.Fields.Item("U_ICON_BF").Value = docLine.U_ICON_BF;
                    }
                    if (!String.IsNullOrEmpty(docLine.U_ICON_PRNumber))
                    {
                        oLines.UserFields.Fields.Item("U_ICON_PRNumber").Value = docLine.U_ICON_BF;
                    }
                }


                #region Settle Down Payment
                //if (Doc.DownPaymentEntry > 0)
                //{
                //    oDoc.DownPaymentsToDraw.DocEntry = Doc.DownPaymentEntry;
                //    oDoc.DownPaymentsToDraw.AmountToDraw = Doc.DownPaymentAmt;
                //}
                #endregion

                lRetCode = oDoc.Add();
                oCompany.GetNewObjectCode(out DocEntry);


                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);
                    throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                }

                DocNum = GetDocNum(DocEntry);

                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = DocEntry;
                Log_Detail.SAPRefNo = DocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }

                // Payment
                //string errMsg = "";
                //if (DIUtil.lRetCode == 0)
                //{
                //    if (Doc.TransferAmt > 0 || Doc.CashAmt > 0)
                //    {
                //        CreatePayment(Convert.ToInt32(docEntry), Doc, out errMsg);
                //    }
                //}
            }
            catch (Exception ex)
            {
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void CreateDocumentPMAP(Document Doc, out string DocEntry, out string DocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            SAPbobsCOM.Documents oDoc = null;

            try
            {
                this.ConnectCompanyDB();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("connected to a company"))
                {
                    this.DisConnectCompanyDB();
                    this.ConnectCompanyDB();
                }

            }
            oCompany.StartTransaction();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));
                // Header (Document)
                oDoc = oCompany.GetBusinessObject(Doc.DocType);
                oDoc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;

                oDoc.DocDate = Doc.PostingDate;
                oDoc.DocDueDate = Doc.DocDueDate;
                oDoc.TaxDate = Doc.DocumentDate;

                if (Doc.DocType != SAPbobsCOM.BoObjectTypes.oPurchaseRequest)
                {
                    oDoc.CardCode = Doc.CardCode;
                }
                if (!String.IsNullOrEmpty(Doc.CardName))
                {
                    oDoc.CardName = Doc.CardName;
                }
                if (!String.IsNullOrEmpty(Doc.Address))
                {
                    oDoc.Address = Doc.Address;
                }
                if (!String.IsNullOrEmpty(Doc.NumAtCard))
                {
                    oDoc.NumAtCard = Doc.NumAtCard;
                }
                if (!String.IsNullOrEmpty(Doc.VatPercent))
                {
                    oDoc.VatPercent = Convert.ToDouble(Doc.VatPercent);
                }
                if (!String.IsNullOrEmpty(Doc.DiscPrcnt))
                {
                    oDoc.DiscountPercent = Convert.ToInt32(Doc.DiscPrcnt);
                }
                if (!String.IsNullOrEmpty(Doc.DocCur))
                {
                    oDoc.DocCurrency = Doc.DocCur;
                }
                if (!String.IsNullOrEmpty(Doc.DocRate))
                {
                    oDoc.DocRate = Convert.ToDouble(Doc.DocRate);
                }
                if (!String.IsNullOrEmpty(Doc.DocTotal))
                {
                    oDoc.DocTotal = Convert.ToDouble(Doc.DocTotal);
                }
                if (!String.IsNullOrEmpty(Doc.Remark))
                {
                    oDoc.Comments = Doc.Remark;
                }
                if (!String.IsNullOrEmpty(Doc.JrnlMemo))
                {
                    oDoc.JournalMemo = Doc.JrnlMemo;
                }
                if (!String.IsNullOrEmpty(Doc.GroupNum))
                {
                    oDoc.GroupNumber = Convert.ToInt32(Doc.GroupNum);
                }
                if (!String.IsNullOrEmpty(Doc.SlpCode))
                {
                    oDoc.SalesPersonCode = Convert.ToInt32(Doc.SlpCode);
                }
                if (!String.IsNullOrEmpty(Doc.Address2))
                {
                    oDoc.Address2 = Doc.Address2;
                }
                if (!String.IsNullOrEmpty(Doc.LicTradNum))
                {
                    oDoc.FederalTaxID = Doc.LicTradNum;
                }
                if (Doc.IsDeferVAT)
                {
                    oDoc.DeferredTax = SAPbobsCOM.BoYesNoEnum.tYES;
                }
                else
                {
                    oDoc.DeferredTax = SAPbobsCOM.BoYesNoEnum.tNO;
                }
                if (!String.IsNullOrEmpty(Doc.OwnerCode))
                {
                    oDoc.DocumentsOwner = Convert.ToInt32(Doc.OwnerCode);
                }
                oDoc.UserFields.Fields.Item("U_ICON_RefNo").Value = Doc.UDF_RefNo;
                //oDoc.UserFields.Fields.Item("U_VATBranch").Value = Doc.UDF_VATBranch;
                oDoc.UserFields.Fields.Item("U_VATPeriod").Value = Doc.UDF_TAX_PECL;
                //if (Doc.DocType == SAPbobsCOM.BoObjectTypes.oPurchaseRequest)
                //{
                //    oDoc.RequriedDate = Doc.RequireDate;
                //}

                //if (Doc.DocType == SAPbobsCOM.BoObjectTypes.oDownPayments)
                //{
                //    oDoc.DownPaymentType = SAPbobsCOM.DownPaymentTypeEnum.dptInvoice;
                //    oDoc.DownPaymentAmount = Doc.DownPaymentAmt;
                //}

                //if (Doc.DocTotal > 0)
                //{
                //    oDoc.DocTotal = Doc.DocTotal;
                //}

                bool firstLine = true;

                // [TIPS] - Caching Variable
                SAPbobsCOM.Document_Lines oLines = oDoc.Lines;
                // Details (Document Lines)
                foreach (DocumentLine docLine in Doc.Lines)
                {
                    if (firstLine)
                        firstLine = false;
                    else
                        oLines.Add();

                    //oLines.ItemCode = GetItemCode(docLine.ItemName);
                    oLines.ItemCode = docLine.ItemCode;
                    if (!string.IsNullOrEmpty(docLine.ItemDescription))
                    {
                        oLines.ItemDescription = docLine.ItemDescription;
                    }
                    oLines.Quantity = docLine.Qty;
                    if (!String.IsNullOrEmpty(docLine.UnitPrice))
                    {
                        oLines.UnitPrice = Convert.ToDouble(docLine.UnitPrice);
                    }

                    if (!String.IsNullOrEmpty(docLine.PriceAfVAT))
                    {
                        oLines.PriceAfterVAT = Convert.ToDouble(docLine.PriceAfVAT);
                    }
                    if (!String.IsNullOrEmpty(docLine.TaxCode))
                    {
                        oLines.VatGroup = docLine.TaxCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.LineTotal))
                    {
                        oLines.LineTotal = Convert.ToDouble(docLine.LineTotal);
                    }
                    if (!String.IsNullOrEmpty(docLine.TotalFrgn))
                    {
                        oLines.RowTotalFC = Convert.ToDouble(docLine.TotalFrgn);
                    }
                    if (!String.IsNullOrEmpty(docLine.BaseEntry))
                    {
                        oLines.BaseType = docLine.BaseType;
                        oLines.BaseEntry = Convert.ToInt32(docLine.BaseEntry);
                        oLines.BaseLine = Convert.ToInt32(docLine.LineNumber);
                    }
                    if (!String.IsNullOrEmpty(docLine.FreeTxt))
                    {
                        oLines.FreeText = docLine.FreeTxt;
                    }
                    if (!String.IsNullOrEmpty(docLine.WhsCode))
                    {
                        oLines.WarehouseCode = docLine.WhsCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.AccountCode))
                    {
                        oLines.AccountCode = docLine.AccountCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.VatSum))
                    {
                        oLines.TaxTotal = Convert.ToDouble(docLine.VatSum);
                    }

                    if (!String.IsNullOrEmpty(docLine.TaxPercentagePerRow))
                    {
                        oLines.TaxPercentagePerRow = Convert.ToDouble(docLine.TaxPercentagePerRow);
                    }

                    if (!String.IsNullOrEmpty(docLine.ProjectCode))
                    {
                        oLines.ProjectCode = docLine.ProjectCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode))
                    {
                        oLines.CostingCode = docLine.OcrCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode2))
                    {
                        oLines.CostingCode2 = docLine.OcrCode2;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode3))
                    {
                        oLines.CostingCode3 = docLine.OcrCode3;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode4))
                    {
                        oLines.CostingCode4 = docLine.OcrCode4;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode5))
                    {
                        oLines.CostingCode5 = docLine.OcrCode5;
                    }
                    if (!String.IsNullOrEmpty(docLine.DiscountPerRow))
                    {
                        oLines.DiscountPercent = Convert.ToDouble(docLine.DiscountPerRow);
                    }
                    //oLines.DeferredTax = docLine.IsDeferVAT ? SAPbobsCOM.BoYesNoEnum.tYES : SAPbobsCOM.BoYesNoEnum.tNO;
                }


                #region Settle Down Payment
                //if (Doc.DownPaymentEntry > 0)
                //{
                //    oDoc.DownPaymentsToDraw.DocEntry = Doc.DownPaymentEntry;
                //    oDoc.DownPaymentsToDraw.AmountToDraw = Doc.DownPaymentAmt;
                //}
                #endregion

                lRetCode = oDoc.Add();
                oCompany.GetNewObjectCode(out DocEntry);


                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);
                    throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                }

                DocNum = GetDocNum(DocEntry);

                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = DocEntry;
                Log_Detail.SAPRefNo = DocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }

                // Payment
                //string errMsg = "";
                //if (DIUtil.lRetCode == 0)
                //{
                //    if (Doc.TransferAmt > 0 || Doc.CashAmt > 0)
                //    {
                //        CreatePayment(Convert.ToInt32(docEntry), Doc, out errMsg);
                //    }
                //}
            }
            catch (Exception ex)
            {
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        #endregion

        #region #### Journal Voucher ####
        public void CreateJournalVoucher(Document Doc, out string BatchNum, out string DocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            try
            {
                this.ConnectCompanyDB();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("connected to a company"))
                {
                    this.DisConnectCompanyDB();
                    this.ConnectCompanyDB();
                }

            }
            oCompany.StartTransaction();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                //Create Journal Voucher
                SAPbobsCOM.JournalVouchers oJV = (SAPbobsCOM.JournalVouchers)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers);
                oJV.JournalEntries.ReferenceDate = Doc.PostingDate;
                oJV.JournalEntries.DueDate = Doc.DocDueDate;
                oJV.JournalEntries.TaxDate = Doc.TaxDate;

                if (!string.IsNullOrEmpty(Doc.Series))
                {
                    oJV.JournalEntries.Series = Convert.ToInt32(Doc.Series);
                }
                if (!string.IsNullOrEmpty(Doc.StornoDate))
                {
                    oJV.JournalEntries.StornoDate = Convert.ToDateTime(Doc.StornoDate);
                }

                if (!string.IsNullOrEmpty(Doc.IsAutoReverse) && Doc.IsAutoReverse == "Y")
                {
                    oJV.JournalEntries.UseAutoStorno = SAPbobsCOM.BoYesNoEnum.tYES;
                }

                oJV.JournalEntries.UserFields.Fields.Item("U_ICON_Ref").Value = Doc.UDF_RefNo;
                if (!string.IsNullOrEmpty(Doc.U_ICON_PT)) oJV.JournalEntries.UserFields.Fields.Item("U_ICON_PT").Value = Doc.U_ICON_PT;
                if (!string.IsNullOrEmpty(Doc.U_ICON_BF)) oJV.JournalEntries.UserFields.Fields.Item("U_ICON_BF").Value = Doc.U_ICON_BF;

                if (!string.IsNullOrEmpty(Doc.Remark))
                {
                    oJV.JournalEntries.Memo = Doc.Remark.Length > 50 ? Doc.Remark.Substring(0, 50) : Doc.Remark;
                    oJV.JournalEntries.UserFields.Fields.Item("U_ICON_Remark").Value = Doc.Remark;
                }

                if (!string.IsNullOrEmpty(Doc.Project))
                {
                    oJV.JournalEntries.ProjectCode = Doc.Project;
                }

                if (!string.IsNullOrEmpty(Doc.JrnlMemo))
                {
                    oJV.JournalEntries.Memo = Doc.JrnlMemo;
                }

                if (!string.IsNullOrEmpty(Doc.Ref1))
                {
                    oJV.JournalEntries.Reference = Doc.Ref1;
                }

                if (!string.IsNullOrEmpty(Doc.Ref2))
                {
                    oJV.JournalEntries.Reference2 = Doc.Ref2;
                }

                if (!string.IsNullOrEmpty(Doc.Ref3))
                {
                    oJV.JournalEntries.Reference3 = Doc.Ref3;
                }

                bool firstLine = true;

                foreach (DocumentLine Line in Doc.Lines)
                {
                    SAPbobsCOM.JournalEntries_Lines oLine = oJV.JournalEntries.Lines;

                    if (firstLine)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        oLine.Add();
                    }

                    if (!String.IsNullOrEmpty(Line.ShortName))
                    {
                        oLine.ShortName = Line.ShortName;
                    }
                    //else
                    //{
                    //    oLine.ShortName = Line.AccountCode;
                    //}
                    if (!String.IsNullOrEmpty(Line.ShortName))
                    {
                        oLine.ContraAccount = Line.AccountCode;
                    }
                    else
                    {
                        oLine.AccountCode = Line.AccountCode;
                    }

                    oLine.Debit = Line.Debit;
                    oLine.Credit = Line.Credit;

                    if (!String.IsNullOrEmpty(Line.TaxGroup))
                    {
                        oLine.TaxGroup = Line.TaxGroup;
                    }
                    if (!String.IsNullOrEmpty(Line.LineMemo))
                    {
                        oLine.LineMemo = Line.LineMemo;
                    }
                    if (!String.IsNullOrEmpty(Line.Reference1))
                    {
                        oLine.Reference1 = Line.Reference1;
                    }
                    if (!String.IsNullOrEmpty(Line.Reference2))
                    {
                        oLine.Reference2 = Line.Reference2;
                    }
                    if (!String.IsNullOrEmpty(Line.ProjectCode))
                    {
                        oLine.ProjectCode = Line.ProjectCode;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode))
                    {
                        oLine.CostingCode = Line.OcrCode;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode2))
                    {
                        oLine.CostingCode2 = Line.OcrCode2;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode3))
                    {
                        oLine.CostingCode3 = Line.OcrCode3;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode4))
                    {
                        oLine.CostingCode4 = Line.OcrCode4;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode5))
                    {
                        oLine.CostingCode5 = Line.OcrCode5;
                    }

                    if (!String.IsNullOrEmpty(Line.TAX_BASE))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_BASE").Value = Line.TAX_BASE;

                    }
                    if (!String.IsNullOrEmpty(Line.TAX_NO))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_NO").Value = Line.TAX_NO;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_REFNO))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_REFNO").Value = Line.TAX_REFNO;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_PECL))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_PECL").Value = Line.TAX_PECL;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_TYPE))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_TYPE").Value = Line.TAX_TYPE;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_BOOKNO))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_BOOKNO").Value = Line.TAX_BOOKNO;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_CARDNAME))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_CARDNAME").Value = Line.TAX_CARDNAME;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_ADDRESS))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_ADDRESS").Value = Line.TAX_ADDRESS;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_TAXID))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_TAXID").Value = Line.TAX_TAXID;
                    }
                    if (Line.TAX_DATE != DateTime.MinValue)
                    {
                        oLine.UserFields.Fields.Item("U_TAX_DATE").Value = Line.TAX_DATE;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_CODE))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_CODE").Value = Line.TAX_CODE;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_CODENAME))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_CODENAME").Value = Line.TAX_CODENAME;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_RATE))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_RATE").Value = Line.TAX_RATE;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_DEDUCT))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_DEDUCT").Value = Line.TAX_DEDUCT;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_OTHER))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_OTHER").Value = Line.TAX_OTHER;
                    }
                    if (!String.IsNullOrEmpty(Line.U_ICON_PT))
                    {
                        oLine.UserFields.Fields.Item("U_ICON_PT").Value = Line.U_ICON_PT;
                    }
                    if (!String.IsNullOrEmpty(Line.U_ICON_BF))
                    {
                        oLine.UserFields.Fields.Item("U_ICON_BF").Value = Line.U_ICON_BF;
                    }
                    if (!String.IsNullOrEmpty(Line.U_BPBRANCH))
                    {
                        oLine.UserFields.Fields.Item("U_BPBRANCH").Value = Line.U_BPBRANCH;
                    }
                    //if(!String.IsNullOrEmpty(Line.U_))
                    //{
                    //    oLine.UserFields.Fields.Item("")
                    //}
                }

                lRetCode = oJV.Add();

                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);
                    throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                }

                BatchNum = oCompany.GetNewObjectKey();
                SAPTABLE JournalVoucher = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oJournalVouchers));
                //DocNum = GetDataColumn("BatchNum", JournalVoucher, "BatchNum", BatchNum);
                DocNum = BatchNum.Split('\t')[0];
                //GLDocNum = DocNum;

                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = BatchNum;
                Log_Detail.SAPRefNo = DocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }

        public void RemoveJournalVoucher(Document Doc, out string TranID, out string DocNum, out string GLDocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            SAPbobsCOM.Documents oDoc = null;
            this.ConnectCompanyDB();
            oCompany.StartTransaction();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string DocTransId = string.Empty;

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));

                oDoc = oCompany.GetBusinessObject(Doc.DocType);
                oDoc.GetByKey(Doc.BatchNum);

                lRetCode = oDoc.Remove();

                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);
                    throw new Exception($"SAP Error: Code {InternalErrorCode} {InternalErrorMessage}");
                }

                oCompany.GetNewObjectCode(out TranID);
                DocNum = GetDocNum(TranID);
                DocTransId = GetDocTransId(TranID);
                GLDocNum = GetGLDocNum(DocTransId);


                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = TranID;
                Log_Detail.SAPRefNo = DocNum;
                Log_Detail.SAPRefGLNo = GLDocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }
        }
        #endregion

        #region #### Journal Entry ####
        public void CreateJournalEntry(Document Doc, out string BatchNum, out string DocNum, out int InternalErrorCode, out string InternalErrorMessage
            , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            try
            {
                this.ConnectCompanyDB();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("connected to a company"))
                {
                    this.DisConnectCompanyDB();
                    this.ConnectCompanyDB();
                }

            }
            oCompany.StartTransaction();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                //Create Journal Voucher
                SAPbobsCOM.JournalEntries oJE = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                oJE.ReferenceDate = Doc.PostingDate;
                oJE.DueDate = Doc.DocDueDate;
                oJE.TaxDate = Doc.TaxDate;

                if (!string.IsNullOrEmpty(Doc.Series))
                {
                    oJE.Series = Convert.ToInt32(Doc.Series);
                }
                if (!string.IsNullOrEmpty(Doc.StornoDate))
                {
                    oJE.StornoDate = Convert.ToDateTime(Doc.StornoDate);
                }

                if (!string.IsNullOrEmpty(Doc.IsAutoReverse) && Doc.IsAutoReverse == "Y")
                {
                    oJE.UseAutoStorno = SAPbobsCOM.BoYesNoEnum.tYES;
                }

                oJE.UserFields.Fields.Item("U_ICON_Ref").Value = Doc.UDF_RefNo;
                if (!string.IsNullOrEmpty(Doc.U_ICON_PT)) oJE.UserFields.Fields.Item("U_ICON_PT").Value = Doc.U_ICON_PT;
                if (!string.IsNullOrEmpty(Doc.U_ICON_BF)) oJE.UserFields.Fields.Item("U_ICON_BF").Value = Doc.U_ICON_BF;

                if (!string.IsNullOrEmpty(Doc.Remark))
                {
                    oJE.Memo = Doc.Remark.Length > 50 ? Doc.Remark.Substring(0, 50) : Doc.Remark;
                    oJE.UserFields.Fields.Item("U_ICON_Remark").Value = Doc.Remark;
                }

                if (!string.IsNullOrEmpty(Doc.Project))
                {
                    oJE.ProjectCode = Doc.Project;
                }

                if (!string.IsNullOrEmpty(Doc.JrnlMemo))
                {
                    oJE.Memo = Doc.JrnlMemo;
                }

                if (!string.IsNullOrEmpty(Doc.Ref1))
                {
                    oJE.Reference = Doc.Ref1;
                }

                bool firstLine = true;

                foreach (DocumentLine Line in Doc.Lines)
                {
                    SAPbobsCOM.JournalEntries_Lines oLine = oJE.Lines;

                    if (firstLine)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        oLine.Add();
                    }

                    if (!String.IsNullOrEmpty(Line.ShortName))
                    {
                        oLine.ShortName = Line.ShortName;
                    }
                    //else
                    //{
                    //    oLine.ShortName = Line.AccountCode;
                    //}
                    if (!String.IsNullOrEmpty(Line.ShortName))
                    {
                        oLine.ContraAccount = Line.AccountCode;
                    }
                    else
                    {
                        oLine.AccountCode = Line.AccountCode;
                    }

                    oLine.Debit = Line.Debit;
                    oLine.Credit = Line.Credit;

                    if (!String.IsNullOrEmpty(Line.LineMemo))
                    {
                        oLine.LineMemo = Line.LineMemo;
                    }
                    if (!String.IsNullOrEmpty(Line.ProjectCode))
                    {
                        oLine.ProjectCode = Line.ProjectCode;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode))
                    {
                        oLine.CostingCode = Line.OcrCode;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode2))
                    {
                        oLine.CostingCode2 = Line.OcrCode2;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode3))
                    {
                        oLine.CostingCode3 = Line.OcrCode3;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode4))
                    {
                        oLine.CostingCode4 = Line.OcrCode4;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode5))
                    {
                        oLine.CostingCode5 = Line.OcrCode5;
                    }

                    //if (!String.IsNullOrEmpty(Line.Reference1))
                    //{
                    //    oLine.Reference1 = Line.Reference1;
                    //}
                    //if (!String.IsNullOrEmpty(Line.Reference2))
                    //{
                    //    oLine.Reference2 = Line.Reference2;
                    //}
                    //if (!String.IsNullOrEmpty(Line.Reference3))
                    //{
                    //    oLine.li = Line.Reference3;
                    //}

                    if (!String.IsNullOrEmpty(Line.TAX_BASE))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_BASE").Value = Line.TAX_BASE;

                    }
                    if (!String.IsNullOrEmpty(Line.TAX_NO))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_NO").Value = Line.TAX_NO;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_REFNO))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_REFNO").Value = Line.TAX_REFNO;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_PECL))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_PECL").Value = Line.TAX_PECL;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_TYPE))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_TYPE").Value = Line.TAX_TYPE;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_BOOKNO))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_BOOKNO").Value = Line.TAX_BOOKNO;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_CARDNAME))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_CARDNAME").Value = Line.TAX_CARDNAME;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_ADDRESS))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_ADDRESS").Value = Line.TAX_ADDRESS;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_TAXID))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_TAXID").Value = Line.TAX_TAXID;
                    }
                    if (Line.TAX_DATE != DateTime.MinValue)
                    {
                        oLine.UserFields.Fields.Item("U_TAX_DATE").Value = Line.TAX_DATE;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_CODE))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_CODE").Value = Line.TAX_CODE;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_CODENAME))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_CODENAME").Value = Line.TAX_CODENAME;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_RATE))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_RATE").Value = Line.TAX_RATE;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_DEDUCT))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_DEDUCT").Value = Line.TAX_DEDUCT;
                    }
                    if (!String.IsNullOrEmpty(Line.TAX_OTHER))
                    {
                        oLine.UserFields.Fields.Item("U_TAX_OTHER").Value = Line.TAX_OTHER;
                    }
                    if (!String.IsNullOrEmpty(Line.U_ICON_PT))
                    {
                        oLine.UserFields.Fields.Item("U_ICON_PT").Value = Line.U_ICON_PT;
                    }
                    if (!String.IsNullOrEmpty(Line.U_ICON_BF))
                    {
                        oLine.UserFields.Fields.Item("U_ICON_BF").Value = Line.U_ICON_BF;
                    }
                }

                lRetCode = oJE.Add();

                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out InternalErrorCode, out InternalErrorMessage);
                    throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                }

                BatchNum = oCompany.GetNewObjectKey();
                SAPTABLE JournalVoucher = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oJournalVouchers));
                //DocNum = GetDataColumn("BatchNum", JournalVoucher, "BatchNum", BatchNum);
                DocNum = BatchNum.Split('\t')[0];
                //GLDocNum = DocNum;

                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = BatchNum;
                Log_Detail.SAPRefNo = DocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }
        #endregion
        #endregion

        #region #### Interface Inventory ####
        public void CreateGoodsIssue(Document Doc, out string DocEntry, out string DocNum, out int InterfaceErrorCoe, out string InternalErrorMessage
             , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            SAPbobsCOM.Documents oDoc = null;
            try
            {
                this.ConnectCompanyDB();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("connected to a company"))
                {
                    this.DisConnectCompanyDB();
                    this.ConnectCompanyDB();
                }

            }
            oCompany.StartTransaction();
            InternalErrorMessage = string.Empty;
            InterfaceErrorCoe = 0;

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));
                //Header (Document)
                oDoc = oCompany.GetBusinessObject(Doc.DocType);
                oDoc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                oDoc.DocDate = Doc.PostingDate;
                oDoc.TaxDate = Doc.DocumentDate;

                oDoc.UserFields.Fields.Item("U_ICON_RefNo").Value = Doc.UDF_RefNo;
                oDoc.UserFields.Fields.Item("U_ICON_CustName").Value = Doc.UDF_CustName;
                oDoc.UserFields.Fields.Item("U_ICON_IssueReason").Value = Doc.U_ICON_IssueReason;
                //oDoc.Comments = Doc.Remark;

                //Detail (Document Lines)
                bool firstLine = true;
                SAPbobsCOM.Document_Lines oLine = oDoc.Lines;

                foreach (DocumentLine Line in Doc.Lines)
                {
                    if (firstLine)
                        firstLine = false;
                    else
                        oLine.Add();

                    oLine.ItemCode = Line.ItemCode;
                    oLine.Quantity = Line.Qty;

                    if (!String.IsNullOrEmpty(Line.WhsCode))
                    {
                        oLine.WarehouseCode = Line.WhsCode;
                    }
                    if (!String.IsNullOrEmpty(Line.CostCode))
                    {
                        oLine.CostingCode = Line.CostCode;
                    }
                    if (!String.IsNullOrEmpty(Line.ProjectCode))
                    {
                        oLine.ProjectCode = Line.ProjectCode;
                    }

                    if (!String.IsNullOrEmpty(Line.OcrCode))
                    {
                        oLine.CostingCode = Line.OcrCode;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode2))
                    {
                        oLine.CostingCode2 = Line.OcrCode2;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode3))
                    {
                        oLine.CostingCode3 = Line.OcrCode3;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode4))
                    {
                        oLine.CostingCode4 = Line.OcrCode4;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode5))
                    {
                        oLine.CostingCode5 = Line.OcrCode5;
                    }
                    if (!String.IsNullOrEmpty(Line.AccountCode))
                    {
                        oLine.AccountCode = Line.AccountCode;
                    }
                }

                lRetCode = oDoc.Add();
                oCompany.GetNewObjectCode(out DocEntry);

                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out InterfaceErrorCoe, out InternalErrorMessage);
                    throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                }

                DocNum = GetDocNum(DocEntry);

                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = DocEntry;
                Log_Detail.SAPRefNo = DocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (lRetCode == 0)
                {
                    InterfaceErrorCoe = -1;
                    InternalErrorMessage = ex.Message;
                }
                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }

        }
        public void CreateGoodsIssueCM(Document Doc, out string DocEntry, out string DocNum, out int InterfaceErrorCoe, out string InternalErrorMessage
             , out List<SAP_Interface_Log_Detail> LogDetail)
        {
            SAPbobsCOM.Documents oDoc = null;
            try
            {
                this.ConnectCompanyDB();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("connected to a company"))
                {
                    this.DisConnectCompanyDB();
                    this.ConnectCompanyDB();
                }

            }
            oCompany.StartTransaction();
            InternalErrorMessage = string.Empty;
            InterfaceErrorCoe = 0;

            try
            {
                LogDetail = new List<SAP_Interface_Log_Detail>();
                SAPTABLE = new SAPTABLE(Convert.ToInt32(Doc.DocType));
                //Header (Document)
                //save draft
                //oDoc = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);
                //oDoc.DocObjectCode = Doc.DocType;

                oDoc = oCompany.GetBusinessObject(Doc.DocType);
                oDoc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                oDoc.DocDate = Doc.PostingDate;
                oDoc.TaxDate = Doc.DocumentDate;

                oDoc.UserFields.Fields.Item("U_ICON_RefNo").Value = Doc.UDF_RefNo;
                oDoc.UserFields.Fields.Item("U_ICON_CustName").Value = Doc.UDF_CustName;
                oDoc.UserFields.Fields.Item("U_ICON_IssueReason").Value = Doc.U_ICON_IssueReason;
                if (!string.IsNullOrEmpty(Doc.JrnlMemo)) oDoc.JournalMemo = Doc.JrnlMemo;
                //oDoc.Comments = Doc.Remark;

                //Detail (Document Lines)
                bool firstLine = true;
                SAPbobsCOM.Document_Lines oLine = oDoc.Lines;

                foreach (DocumentLine Line in Doc.Lines)
                {
                    if (firstLine)
                        firstLine = false;
                    else
                        oLine.Add();

                    oLine.ItemCode = Line.ItemCode;
                    oLine.Quantity = Line.Qty;

                    if (!String.IsNullOrEmpty(Line.WhsCode))
                    {
                        oLine.WarehouseCode = Line.WhsCode;
                    }
                    if (!String.IsNullOrEmpty(Line.CostCode))
                    {
                        oLine.CostingCode = Line.CostCode;
                    }
                    if (!String.IsNullOrEmpty(Line.ProjectCode))
                    {
                        oLine.ProjectCode = Line.ProjectCode;
                    }

                    if (!String.IsNullOrEmpty(Line.OcrCode))
                    {
                        oLine.CostingCode = Line.OcrCode;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode2))
                    {
                        oLine.CostingCode2 = Line.OcrCode2;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode3))
                    {
                        oLine.CostingCode3 = Line.OcrCode3;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode4))
                    {
                        oLine.CostingCode4 = Line.OcrCode4;
                    }
                    if (!String.IsNullOrEmpty(Line.OcrCode5))
                    {
                        oLine.CostingCode5 = Line.OcrCode5;
                    }
                    if (!String.IsNullOrEmpty(Line.AccountCode))
                    {
                        oLine.AccountCode = Line.AccountCode;
                    }
                }

                lRetCode = oDoc.Add();
                oCompany.GetNewObjectCode(out DocEntry);

                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out InterfaceErrorCoe, out InternalErrorMessage);
                    throw new Exception($"SAP Error: Code {oCompany.GetLastErrorCode()} {oCompany.GetLastErrorDescription()}");
                }

                DocNum = GetDocNum(DocEntry);

                SAP_Interface_Log_Detail Log_Detail = new SAP_Interface_Log_Detail();
                Log_Detail.SAPRefID = DocEntry;
                Log_Detail.SAPRefNo = DocNum;
                LogDetail.Add(Log_Detail);

                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                if (lRetCode == 0)
                {
                    InterfaceErrorCoe = -1;
                    InternalErrorMessage = ex.Message;
                }
                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }

        }
        public void CreateStockTransfer(StockTransfer objSave, out int InternalErrorCode, out string InternalErrorMessage, out string TransferCode)
        {
            this.ConnectCompanyDB();
            InternalErrorMessage = string.Empty;
            InternalErrorCode = 0;
            string SAP_CODE = string.Empty;
            try
            {
                SAPbobsCOM.StockTransfer oData = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer);
                SAPTABLE = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oStockTransfer));
                oData.DocObjectCode = SAPbobsCOM.BoObjectTypes.oStockTransfer;
                oData.DocDate = objSave.DocDate;
                oData.DueDate = objSave.DueDate;
                oData.TaxDate = objSave.TaxDate;
                oData.Series = Convert.ToInt32(objSave.Series);

                bool firstLine = true;

                // [TIPS] - Caching Variable
                SAPbobsCOM.StockTransfer_Lines oLines = oData.Lines;
                // Details (Document Lines)
                foreach (StockTransfer_Detail docLine in objSave.Details)
                {
                    if (firstLine)
                        firstLine = false;
                    else
                        oLines.Add();

                    oLines.ItemCode = docLine.ItemCode;
                    oLines.Quantity = Convert.ToDouble(docLine.Quantity);
                    //if (!String.IsNullOrEmpty(docLine.UnitMsr))
                    //{
                    //    oLines.MeasureUnit = docLine.UnitMsr;
                    //}
                    if (!String.IsNullOrEmpty(docLine.FromWhsCod))
                    {
                        oLines.FromWarehouseCode = docLine.FromWhsCod;
                    }
                    if (!String.IsNullOrEmpty(docLine.WhsCode))
                    {
                        oLines.WarehouseCode = docLine.WhsCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.Project))
                    {
                        oLines.ProjectCode = docLine.Project;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode))
                    {
                        oLines.DistributionRule = docLine.OcrCode;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode2))
                    {
                        oLines.DistributionRule2 = docLine.OcrCode2;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode3))
                    {
                        oLines.DistributionRule3 = docLine.OcrCode3;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode4))
                    {
                        oLines.DistributionRule4 = docLine.OcrCode4;
                    }
                    if (!String.IsNullOrEmpty(docLine.OcrCode5))
                    {
                        oLines.DistributionRule5 = docLine.OcrCode5;
                    }

                }

                lRetCode = oData.Add();

                oCompany.GetNewObjectCode(out TransferCode);
                TransferCode = GetDocNum(TransferCode);

                string errorMsg = string.Empty;
                if (lRetCode != 0)
                {
                    errorMsg = oCompany.GetLastErrorDescription();
                    InternalErrorMessage = errorMsg;
                    throw new Exception(errorMsg);
                }

            }
            catch (Exception ex)
            {
                if (InternalErrorCode == 0)
                {
                    InternalErrorCode = -1;
                    InternalErrorMessage = ex.Message;
                }

                throw new Exception(ex.Message);
            }
            finally { this.DisConnectCompanyDB(); }
        }
        #endregion

        public Boolean GetIsLock(DateTime DocDate)
        {
            Boolean IsLock = false;
            string DocDateStr = DocDate.ToString("yyyy-MM-dd");

            System.Text.StringBuilder SQL_CheckLockPeriod = new System.Text.StringBuilder();
            SQL_CheckLockPeriod.AppendLine("SELECT");
            SQL_CheckLockPeriod.AppendLine("	COUNT(1) Cnt");
            SQL_CheckLockPeriod.AppendLine("FROM");
            SQL_CheckLockPeriod.AppendLine("	OFPR");
            SQL_CheckLockPeriod.AppendLine("WHERE");
            SQL_CheckLockPeriod.AppendLine("	1=1");
            SQL_CheckLockPeriod.AppendLine("	AND PeriodStat = 'L'");
            SQL_CheckLockPeriod.AppendLine($"	AND '{DocDateStr}' BETWEEN F_RefDate AND T_RefDate");

            int CountLock = 0;
            SAPbobsCOM.Recordset oRecordSet = SAPB1.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordSet.DoQuery(SQL_CheckLockPeriod.ToString());
            if (oRecordSet.RecordCount > 0)
            {
                CountLock = oRecordSet.Fields.Item("Cnt").Value;
            }

            IsLock = (CountLock > 0);

            return IsLock;
        }
        public string GetDocNum(string DocEntry)
        {
            int docNum = 0;

            SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordSet.DoQuery($"SELECT DocNum FROM {SAPTABLE.Table} WHERE DocEntry = {DocEntry}");
            if (oRecordSet.RecordCount > 0)
            {
                docNum = oRecordSet.Fields.Item("DocNum").Value;
            }

            return docNum.ToString();
        }
        public string GetDocTransId(string DocEntry)
        {
            int DocTransId = 0;

            SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordSet.DoQuery($"SELECT TransId FROM {SAPTABLE.Table} WHERE DocEntry = {DocEntry}");
            if (oRecordSet.RecordCount > 0)
            {
                DocTransId = oRecordSet.Fields.Item("TransId").Value;
            }

            return DocTransId.ToString();
        }
        public string GetGLDocNum(string DocTransId)
        {
            int GLDocNum = 0;
            SAPTABLE JournalEntry = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oJournalEntries));

            SAPbobsCOM.Recordset oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordSet.DoQuery($"SELECT Number FROM {JournalEntry.Table} WHERE TransId = {DocTransId}");
            if (oRecordSet.RecordCount > 0)
            {
                GLDocNum = oRecordSet.Fields.Item("Number").Value;
            }

            return GLDocNum.ToString();
        }
        public string GetDataColumn(string ColumnName, SAPTABLE SABTable, string ColumnWhere, string RefID)
        {
            try
            {
                this.ConnectCompanyDB();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("connected to a company"))
                {
                    this.DisConnectCompanyDB();
                    this.ConnectCompanyDB();
                }

            }

            try
            {
                int Data = 0;
                SAPbobsCOM.Recordset oRecordset = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordset.DoQuery($"select {ColumnName} from {SABTable.Table} WHERE {ColumnWhere} = N'{RefID}'");
                if (oRecordset.RecordCount > 0)
                {
                    Data = oRecordset.Fields.Item($"{ColumnName}").Value;
                }

                return Data.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }



        }
        public string GetDataColumn2Condition(string ColumnName, SAPTABLE SABTable, string ColumnWhere, string RefID, string ColumnAnd, string RefID2)
        {
            try
            {
                this.ConnectCompanyDB();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("connected to a company"))
                {
                    this.DisConnectCompanyDB();
                    this.ConnectCompanyDB();
                }

            }

            try
            {
                int Data = 0;
                SAPbobsCOM.Recordset oRecordset = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordset.DoQuery($"select {ColumnName} from {SABTable.Table} WHERE {ColumnWhere} = N'{RefID}' AND {ColumnAnd} = N'{RefID2}'");
                if (oRecordset.RecordCount > 0)
                {
                    Data = oRecordset.Fields.Item($"{ColumnName}").Value;
                }

                return Data.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }



        }
        public string GetDocEntry(SAPTABLE SABTable, string RefID)
        {
            this.ConnectCompanyDB();

            try
            {
                int Data = 0;
                SAPbobsCOM.Recordset oRecordset = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordset.DoQuery($"select DocEntry from {SABTable.Table} WHERE DocNum = '{RefID}' ");
                if (oRecordset.RecordCount > 0)
                {
                    Data = oRecordset.Fields.Item("DocEntry").Value;
                }

                return Data.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }



        }
        public string GetSeries(string ObjCode, string Indicator, string SeriesType = "RV")
        {
            this.ConnectCompanyDB();

            try
            {
                int Data = 0;

                string sql = "";

                if (string.IsNullOrWhiteSpace(SeriesType))
                {
                    sql = $"select Series from nnm1 WHERE objectcode = '{ObjCode}' and indicator = '{Indicator}'";
                }
                else
                {
                    sql = $"select Series from nnm1 WHERE objectcode = '{ObjCode}' and indicator = '{Indicator}' and BeginStr = '{SeriesType}'";
                }


                SAPbobsCOM.Recordset oRecordset = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordset.DoQuery(sql);
                if (oRecordset.RecordCount > 0)
                {
                    Data = oRecordset.Fields.Item("Series").Value;
                }

                return Data.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }



        }

        public string GetItemMasterSeries(string ObjCode)
        {
            this.ConnectCompanyDB();

            try
            {
                int Data = 0;
                SAPbobsCOM.Recordset oRecordset = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordset.DoQuery($"select Series from nnm1 WHERE objectcode = '{ObjCode}' and SeriesName = 'Manual'");
                if (oRecordset.RecordCount > 0)
                {
                    Data = oRecordset.Fields.Item("Series").Value;
                }

                return Data.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                this.DisConnectCompanyDB();
            }



        }
    }

    public class SAPTABLE
    {
        public SAPTABLE(int DocType)
        {
            this.TransType = 0; this.Table = "None"; this.Description = "ERROR"; this.PK = "None";

            if (DocType == 1) { this.TransType = 1; this.Table = "OACT"; this.Description = "G/L Accounts"; this.PK = "AcctCode"; }
            if (DocType == 2) { this.TransType = 2; this.Table = "OCRD"; this.Description = "Business Partner"; this.PK = "CardCode"; }
            if (DocType == 3) { this.TransType = 3; this.Table = "ODSC"; this.Description = "Bank Codes"; this.PK = "AbsEntry"; }
            if (DocType == 4) { this.TransType = 4; this.Table = "OITM"; this.Description = "Items"; this.PK = "ItemCode"; }
            if (DocType == 5) { this.TransType = 5; this.Table = "OVTG"; this.Description = "Tax Definition"; this.PK = "Code"; }
            if (DocType == 6) { this.TransType = 6; this.Table = "OPLN"; this.Description = "Price Lists"; this.PK = "ListNum"; }
            if (DocType == 7) { this.TransType = 7; this.Table = "OSPP"; this.Description = "Special Prices"; this.PK = "CardCode, ItemCode"; }
            if (DocType == 8) { this.TransType = 8; this.Table = "OITG"; this.Description = "Item Properties"; this.PK = "ItmsTypCod"; }
            if (DocType == 9) { this.TransType = 9; this.Table = "ORTM"; this.Description = "Rate Differences"; this.PK = "LineNum, IsSysCurr"; }
            if (DocType == 10) { this.TransType = 10; this.Table = "OCRG"; this.Description = "Card Groups"; this.PK = "GroupCode"; }
            if (DocType == 11) { this.TransType = 11; this.Table = "OCPR"; this.Description = "Contact Persons"; this.PK = "CntctCode"; }
            if (DocType == 12) { this.TransType = 12; this.Table = "OUSR"; this.Description = "Users"; this.PK = "USERID"; }
            if (DocType == 13) { this.TransType = 13; this.Table = "OINV"; this.Description = "A/R Invoice"; this.PK = "DocEntry"; }
            if (DocType == 14) { this.TransType = 14; this.Table = "ORIN"; this.Description = "A/R Credit Memo"; this.PK = "DocEntry"; }
            if (DocType == 15) { this.TransType = 15; this.Table = "ODLN"; this.Description = "Delivery"; this.PK = "DocEntry"; }
            if (DocType == 16) { this.TransType = 16; this.Table = "ORDN"; this.Description = "Returns"; this.PK = "DocEntry"; }
            if (DocType == 17) { this.TransType = 17; this.Table = "ORDR"; this.Description = "Sales Order"; this.PK = "DocEntry"; }
            if (DocType == 18) { this.TransType = 18; this.Table = "OPCH"; this.Description = "A/P Invoice"; this.PK = "DocEntry"; }
            if (DocType == 19) { this.TransType = 19; this.Table = "ORPC"; this.Description = "A/P Credit Memo"; this.PK = "DocEntry"; }
            if (DocType == 20) { this.TransType = 20; this.Table = "OPDN"; this.Description = "Goods Receipt PO"; this.PK = "DocEntry"; }
            if (DocType == 21) { this.TransType = 21; this.Table = "ORPD"; this.Description = "Goods Return"; this.PK = "DocEntry"; }
            if (DocType == 22) { this.TransType = 22; this.Table = "OPOR"; this.Description = "Purchase Order"; this.PK = "DocEntry"; }
            if (DocType == 23) { this.TransType = 23; this.Table = "OQUT"; this.Description = "Sales Quotation"; this.PK = "DocEntry"; }
            if (DocType == 24) { this.TransType = 24; this.Table = "ORCT"; this.Description = "Incoming Payment"; this.PK = "DocEntry"; }
            if (DocType == 25) { this.TransType = 25; this.Table = "ODPS"; this.Description = "Deposit"; this.PK = "DeposId"; }
            if (DocType == 26) { this.TransType = 26; this.Table = "OMTH"; this.Description = "Reconciliation History"; this.PK = "MthAcctCod, IsInternal, MatchNum"; }
            if (DocType == 27) { this.TransType = 27; this.Table = "OCHH"; this.Description = "Check Register"; this.PK = "CheckKey"; }
            if (DocType == 28) { this.TransType = 28; this.Table = "OBTF"; this.Description = "Journal Voucher Entry"; this.PK = "BatchNum, TransId"; }
            if (DocType == 29) { this.TransType = 29; this.Table = "OBTD"; this.Description = "Journal Vouchers List"; this.PK = "BatchNum"; }
            if (DocType == 30) { this.TransType = 30; this.Table = "OJDT"; this.Description = "Journal Entry"; this.PK = "TransId"; }
            if (DocType == 31) { this.TransType = 31; this.Table = "OITW"; this.Description = "Items – Warehouse"; this.PK = "ItemCode, WhsCode"; }
            if (DocType == 32) { this.TransType = 32; this.Table = "OADP"; this.Description = "Print Preferences"; this.PK = "PrintId"; }
            if (DocType == 33) { this.TransType = 33; this.Table = "OCLG"; this.Description = "Activities"; this.PK = "ClgCode"; }
            if (DocType == 34) { this.TransType = 34; this.Table = "ORCR"; this.Description = "Recurring Postings"; this.PK = "RcurCode, Instance"; }
            if (DocType == 35) { this.TransType = 35; this.Table = "ONNM"; this.Description = "Document Numbering"; this.PK = "ObjectCode, DocSubType"; }
            if (DocType == 36) { this.TransType = 36; this.Table = "OCRC"; this.Description = "Credit Cards"; this.PK = "CreditCard"; }
            if (DocType == 37) { this.TransType = 37; this.Table = "OCRN"; this.Description = "Currency Codes"; this.PK = "CurrCode"; }
            if (DocType == 38) { this.TransType = 38; this.Table = "OIDX"; this.Description = "CPI Codes"; this.PK = "IdexCode"; }
            if (DocType == 39) { this.TransType = 39; this.Table = "OADM"; this.Description = "Administration"; this.PK = "Code"; }
            if (DocType == 40) { this.TransType = 40; this.Table = "OCTG"; this.Description = "Payment Terms"; this.PK = "GroupNum"; }
            if (DocType == 41) { this.TransType = 41; this.Table = "OPRF"; this.Description = "Preferences"; this.PK = "FormNumber, UserSign"; }
            if (DocType == 42) { this.TransType = 42; this.Table = "OBNK"; this.Description = "External Bank Statement Received"; this.PK = "AcctCode, Sequence"; }
            if (DocType == 43) { this.TransType = 43; this.Table = "OMRC"; this.Description = "Manufacturers"; this.PK = "FirmCode"; }
            if (DocType == 44) { this.TransType = 44; this.Table = "OCQG"; this.Description = "Card Properties"; this.PK = "GroupCode"; }
            if (DocType == 45) { this.TransType = 45; this.Table = "OTRC"; this.Description = "Journal Entry Codes"; this.PK = "TrnsCode"; }
            if (DocType == 46) { this.TransType = 46; this.Table = "OVPM"; this.Description = "Outgoing Payments"; this.PK = "DocEntry"; }
            if (DocType == 47) { this.TransType = 47; this.Table = "OSRL"; this.Description = "Serial Numbers"; this.PK = "ItemCode, SerialNum"; }
            if (DocType == 48) { this.TransType = 48; this.Table = "OALC"; this.Description = "Loading Expenses"; this.PK = "AlcCode"; }
            if (DocType == 49) { this.TransType = 49; this.Table = "OSHP"; this.Description = "Delivery Types"; this.PK = "TrnspCode"; }
            if (DocType == 50) { this.TransType = 50; this.Table = "OLGT"; this.Description = "Length Units"; this.PK = "UnitCode"; }
            if (DocType == 51) { this.TransType = 51; this.Table = "OWGT"; this.Description = "Weight Units"; this.PK = "UnitCode"; }
            if (DocType == 52) { this.TransType = 52; this.Table = "OITB"; this.Description = "Item Groups"; this.PK = "ItmsGrpCod"; }
            if (DocType == 53) { this.TransType = 53; this.Table = "OSLP"; this.Description = "Sales Employee"; this.PK = "SlpCode"; }
            if (DocType == 54) { this.TransType = 54; this.Table = "OFLT"; this.Description = "Report – Selection Criteria"; this.PK = "FormNum, UserSign, FilterName"; }
            if (DocType == 55) { this.TransType = 55; this.Table = "OTRT"; this.Description = "Posting Templates"; this.PK = "TrtCode"; }
            if (DocType == 56) { this.TransType = 56; this.Table = "OARG"; this.Description = "Customs Groups"; this.PK = "CstGrpCode"; }
            if (DocType == 57) { this.TransType = 57; this.Table = "OCHO"; this.Description = "Checks for Payment"; this.PK = "CheckKey"; }
            if (DocType == 58) { this.TransType = 58; this.Table = "OINM"; this.Description = "Whse Journal"; this.PK = "TransNum, Instance"; }
            if (DocType == 59) { this.TransType = 59; this.Table = "OIGN"; this.Description = "Goods Receipt"; this.PK = "DocEntry"; }
            if (DocType == 60) { this.TransType = 60; this.Table = "OIGE"; this.Description = "Goods Issue"; this.PK = "DocEntry"; }
            if (DocType == 61) { this.TransType = 61; this.Table = "OPRC"; this.Description = "Cost Center"; this.PK = "PrcCode"; }
            if (DocType == 62) { this.TransType = 62; this.Table = "OOCR"; this.Description = "Cost Rate"; this.PK = "OcrCode"; }
            if (DocType == 63) { this.TransType = 63; this.Table = "OPRJ"; this.Description = "Project Codes"; this.PK = "PrjCode"; }
            if (DocType == 64) { this.TransType = 64; this.Table = "OWHS"; this.Description = "Warehouses"; this.PK = "WhsCode"; }
            if (DocType == 65) { this.TransType = 65; this.Table = "OCOG"; this.Description = "Commission Groups"; this.PK = "GroupCode"; }
            if (DocType == 66) { this.TransType = 66; this.Table = "OITT"; this.Description = "Product Tree"; this.PK = "Code"; }
            if (DocType == 67) { this.TransType = 67; this.Table = "OWTR"; this.Description = "Inventory Transfer"; this.PK = "DocEntry"; }
            if (DocType == 68) { this.TransType = 68; this.Table = "OWKO"; this.Description = "Production Instructions"; this.PK = "OrderNum"; }
            if (DocType == 69) { this.TransType = 69; this.Table = "OIPF"; this.Description = "Landed Costs"; this.PK = "DocEntry"; }
            if (DocType == 70) { this.TransType = 70; this.Table = "OCRP"; this.Description = "Payment Methods"; this.PK = "CrTypeCode"; }
            if (DocType == 71) { this.TransType = 71; this.Table = "OCDT"; this.Description = "Credit Card Payment"; this.PK = "Code"; }
            if (DocType == 72) { this.TransType = 72; this.Table = "OCRH"; this.Description = "Credit Card Management"; this.PK = "AbsId, Instance"; }
            if (DocType == 73) { this.TransType = 73; this.Table = "OSCN"; this.Description = "Customer/Vendor Cat. No."; this.PK = "ItemCode, CardCode, Substitute"; }
            if (DocType == 74) { this.TransType = 74; this.Table = "OCRV"; this.Description = "Credit Payments"; this.PK = "AbsId, PayId, Instance"; }
            if (DocType == 75) { this.TransType = 75; this.Table = "ORTT"; this.Description = "CPI and FC Rates"; this.PK = "RateDate, Currency"; }
            if (DocType == 76) { this.TransType = 76; this.Table = "ODPT"; this.Description = "Postdated Deposit"; this.PK = "DeposId"; }
            if (DocType == 77) { this.TransType = 77; this.Table = "OBGT"; this.Description = "Budget"; this.PK = "AbsId"; }
            if (DocType == 78) { this.TransType = 78; this.Table = "OBGD"; this.Description = "Budget Cost Assess. Mthd"; this.PK = "BgdCode"; }
            if (DocType == 79) { this.TransType = 79; this.Table = "ORCN"; this.Description = "Retail Chains"; this.PK = "ChainCode"; }
            if (DocType == 80) { this.TransType = 80; this.Table = "OALT"; this.Description = "Alerts Template"; this.PK = "Code"; }
            if (DocType == 81) { this.TransType = 81; this.Table = "OALR"; this.Description = "Alerts"; this.PK = "Code"; }
            if (DocType == 82) { this.TransType = 82; this.Table = "OAIB"; this.Description = "Received Alerts"; this.PK = "AlertCode, UserSign"; }
            if (DocType == 83) { this.TransType = 83; this.Table = "OAOB"; this.Description = "Message Sent"; this.PK = "AlertCode, UserSign"; }
            if (DocType == 84) { this.TransType = 84; this.Table = "OCLS"; this.Description = "Activity Subjects"; this.PK = "Code"; }
            if (DocType == 85) { this.TransType = 85; this.Table = "OSPG"; this.Description = "Special Prices for Groups"; this.PK = "CardCode, ObjType, ObjKey"; }
            if (DocType == 86) { this.TransType = 86; this.Table = "SPRG"; this.Description = "Application Start"; this.PK = "LineNum, UserCode"; }
            if (DocType == 87) { this.TransType = 87; this.Table = "OMLS"; this.Description = "Distribution List"; this.PK = "Code"; }
            if (DocType == 88) { this.TransType = 88; this.Table = "OENT"; this.Description = "Shipping Types"; this.PK = "DocEntry"; }
            if (DocType == 89) { this.TransType = 89; this.Table = "OSAL"; this.Description = "Outgoing"; this.PK = "DocEntry"; }
            if (DocType == 90) { this.TransType = 90; this.Table = "OTRA"; this.Description = "Transition"; this.PK = "DocEntry"; }
            if (DocType == 91) { this.TransType = 91; this.Table = "OBGS"; this.Description = "Budget Scenario"; this.PK = "AbsId"; }
            if (DocType == 92) { this.TransType = 92; this.Table = "OIRT"; this.Description = "Interest Prices"; this.PK = "Numerator"; }
            if (DocType == 93) { this.TransType = 93; this.Table = "OUDG"; this.Description = "User Defaults"; this.PK = "Code"; }
            if (DocType == 94) { this.TransType = 94; this.Table = "OSRI"; this.Description = "Serial Numbers for Items"; this.PK = "ItemCode, SysSerial"; }
            if (DocType == 95) { this.TransType = 95; this.Table = "OFRT"; this.Description = "Financial Report Templates"; this.PK = "AbsId"; }
            if (DocType == 96) { this.TransType = 96; this.Table = "OFRC"; this.Description = "Financial Report Categories"; this.PK = "TemplateId, CatId"; }
            if (DocType == 97) { this.TransType = 97; this.Table = "OOPR"; this.Description = "Opportunity"; this.PK = "OpprId"; }
            if (DocType == 98) { this.TransType = 98; this.Table = "OOIN"; this.Description = "Interest"; this.PK = "Num"; }
            if (DocType == 99) { this.TransType = 99; this.Table = "OOIR"; this.Description = "Interest Level"; this.PK = "Num"; }
            if (DocType == 100) { this.TransType = 100; this.Table = "OOSR"; this.Description = "Information Source"; this.PK = "Num"; }
            if (DocType == 101) { this.TransType = 101; this.Table = "OOST"; this.Description = "Opportunity Stage"; this.PK = "Num"; }
            if (DocType == 102) { this.TransType = 102; this.Table = "OOFR"; this.Description = "Defect Cause"; this.PK = "Num"; }
            if (DocType == 103) { this.TransType = 103; this.Table = "OCLT"; this.Description = "Activity Types"; this.PK = "Code"; }
            if (DocType == 104) { this.TransType = 104; this.Table = "OCLO"; this.Description = "Meetings Location"; this.PK = "Code"; }
            if (DocType == 105) { this.TransType = 105; this.Table = "OISR"; this.Description = "Service Calls"; this.PK = "RequestNum"; }
            if (DocType == 106) { this.TransType = 106; this.Table = "OIBT"; this.Description = "Batch No. for Item"; this.PK = "ItemCode, BatchNum, WhsCode"; }
            if (DocType == 107) { this.TransType = 107; this.Table = "OALI"; this.Description = "Alternative Items 2"; this.PK = "OrigItem, AltItem"; }
            if (DocType == 108) { this.TransType = 108; this.Table = "OPRT"; this.Description = "Partners"; this.PK = "PrtId"; }
            if (DocType == 109) { this.TransType = 109; this.Table = "OCMT"; this.Description = "Competitors"; this.PK = "CompetId"; }
            if (DocType == 110) { this.TransType = 110; this.Table = "OUVV"; this.Description = "User Validations"; this.PK = "IndexID, LineNum"; }
            if (DocType == 111) { this.TransType = 111; this.Table = "OFPR"; this.Description = "Posting Period"; this.PK = "AbsEntry"; }
            if (DocType == 112) { this.TransType = 112; this.Table = "ODRF"; this.Description = "Drafts"; this.PK = "DocEntry"; }
            if (DocType == 113) { this.TransType = 113; this.Table = "OSRD"; this.Description = "Batches and Serial Numbers"; this.PK = "ItemCode, DocType, DocEntry, DocLineNum"; }
            if (DocType == 114) { this.TransType = 114; this.Table = "OUDC"; this.Description = "User Display Cat."; this.PK = "CodeID"; }
            if (DocType == 115) { this.TransType = 115; this.Table = "OPVL"; this.Description = "Lender – Pelecard"; this.PK = "Code"; }
            if (DocType == 116) { this.TransType = 116; this.Table = "ODDT"; this.Description = "Withholding Tax Deduction Hierarchy"; this.PK = "Numerator"; }
            if (DocType == 117) { this.TransType = 117; this.Table = "ODDG"; this.Description = "Withholding Tax Deduction Groups"; this.PK = "Numerator"; }
            if (DocType == 118) { this.TransType = 118; this.Table = "OUBR"; this.Description = "Branches"; this.PK = "Code"; }
            if (DocType == 119) { this.TransType = 119; this.Table = "OUDP"; this.Description = "Departments"; this.PK = "Code"; }
            if (DocType == 120) { this.TransType = 120; this.Table = "OWST"; this.Description = "Confirmation Level"; this.PK = "WstCode"; }
            if (DocType == 121) { this.TransType = 121; this.Table = "OWTM"; this.Description = "Approval Templates"; this.PK = "WtmCode"; }
            if (DocType == 122) { this.TransType = 122; this.Table = "OWDD"; this.Description = "Docs. for Confirmation"; this.PK = "WddCode"; }
            if (DocType == 123) { this.TransType = 123; this.Table = "OCHD"; this.Description = "Checks for Payment Drafts"; this.PK = "CheckKey"; }
            if (DocType == 124) { this.TransType = 124; this.Table = "CINF"; this.Description = "Company Info"; this.PK = "Version"; }
            if (DocType == 125) { this.TransType = 125; this.Table = "OEXD"; this.Description = "Freight Setup"; this.PK = "ExpnsCode"; }
            if (DocType == 126) { this.TransType = 126; this.Table = "OSTA"; this.Description = "Sales Tax Authorities"; this.PK = "Code, Type"; }
            if (DocType == 127) { this.TransType = 127; this.Table = "OSTT"; this.Description = "Sales Tax Authorities Type"; this.PK = "AbsId"; }
            if (DocType == 128) { this.TransType = 128; this.Table = "OSTC"; this.Description = "Sales Tax Codes"; this.PK = "Code"; }
            if (DocType == 129) { this.TransType = 129; this.Table = "OCRY"; this.Description = "Countries"; this.PK = "Code"; }
            if (DocType == 130) { this.TransType = 130; this.Table = "OCST"; this.Description = "States"; this.PK = "Country, Code"; }
            if (DocType == 131) { this.TransType = 131; this.Table = "OADF"; this.Description = "Address Formats"; this.PK = "Code"; }
            if (DocType == 132) { this.TransType = 132; this.Table = "OCIN"; this.Description = "A/R Correction Invoice"; this.PK = "DocEntry"; }
            if (DocType == 133) { this.TransType = 133; this.Table = "OCDC"; this.Description = "Cash Discount"; this.PK = "Code"; }
            if (DocType == 134) { this.TransType = 134; this.Table = "OQCN"; this.Description = "Query Catagories"; this.PK = "CategoryId"; }
            if (DocType == 135) { this.TransType = 135; this.Table = "OIND"; this.Description = "Triangular Deal"; this.PK = "Code"; }
            if (DocType == 136) { this.TransType = 136; this.Table = "ODMW"; this.Description = "Data Migration"; this.PK = "Code"; }
            if (DocType == 137) { this.TransType = 137; this.Table = "OCSTN"; this.Description = "Workstation ID"; this.PK = "Code"; }
            if (DocType == 138) { this.TransType = 138; this.Table = "OIDC"; this.Description = "Indicator"; this.PK = "Code"; }
            if (DocType == 139) { this.TransType = 139; this.Table = "OGSP"; this.Description = "Goods Shipment"; this.PK = "Code"; }
            if (DocType == 140) { this.TransType = 140; this.Table = "OPDF"; this.Description = "Payment Draft"; this.PK = "DocEntry"; }
            if (DocType == 141) { this.TransType = 141; this.Table = "OQWZ"; this.Description = "Query Wizard"; this.PK = "Code"; }
            if (DocType == 142) { this.TransType = 142; this.Table = "OASG"; this.Description = "Account Segmentation"; this.PK = "AbsId"; }
            if (DocType == 143) { this.TransType = 143; this.Table = "OASC"; this.Description = "Account Segmentation Categories"; this.PK = "SegmentId, Code"; }
            if (DocType == 144) { this.TransType = 144; this.Table = "OLCT"; this.Description = "Location"; this.PK = "Code"; }
            if (DocType == 145) { this.TransType = 145; this.Table = "OTNN"; this.Description = "1099 Forms"; this.PK = "FormCode"; }
            if (DocType == 146) { this.TransType = 146; this.Table = "OCYC"; this.Description = "Cycle"; this.PK = "Code"; }
            if (DocType == 147) { this.TransType = 147; this.Table = "OPYM"; this.Description = "Payment Methods for Payment Wizard"; this.PK = "PayMethCod"; }
            if (DocType == 148) { this.TransType = 148; this.Table = "OTOB"; this.Description = "1099 Opening Balance"; this.PK = "VendCode, Form1099, Box1099"; }
            if (DocType == 149) { this.TransType = 149; this.Table = "ORIT"; this.Description = "Dunning Interest Rate"; this.PK = "Code"; }
            if (DocType == 150) { this.TransType = 150; this.Table = "OBPP"; this.Description = "BP Priorities"; this.PK = "PrioCode"; }
            if (DocType == 151) { this.TransType = 151; this.Table = "ODUN"; this.Description = "Dunning Letters"; this.PK = "LineNum"; }
            if (DocType == 152) { this.TransType = 152; this.Table = "CUFD"; this.Description = "User Fields – Description"; this.PK = "TableID, FieldID"; }
            if (DocType == 153) { this.TransType = 153; this.Table = "OUTB"; this.Description = "User Tables"; this.PK = "TableName"; }
            if (DocType == 154) { this.TransType = 154; this.Table = "OCUMI"; this.Description = "My Menu Items"; this.PK = "UserSign , Id_"; }
            if (DocType == 155) { this.TransType = 155; this.Table = "OPYD"; this.Description = "Payment Run"; this.PK = "Code"; }
            if (DocType == 156) { this.TransType = 156; this.Table = "OPKL"; this.Description = "Pick List"; this.PK = "AbsEntry"; }
            if (DocType == 157) { this.TransType = 157; this.Table = "OPWZ"; this.Description = "Payment Wizard"; this.PK = "IdNumber"; }
            if (DocType == 158) { this.TransType = 158; this.Table = "OPEX"; this.Description = "Payment Results Table"; this.PK = "AbsEntry"; }
            if (DocType == 159) { this.TransType = 159; this.Table = "OPYB"; this.Description = "Payment Block"; this.PK = "AbsEntry"; }
            if (DocType == 160) { this.TransType = 160; this.Table = "OUQR"; this.Description = "Queries"; this.PK = "IntrnalKey, Qcategory"; }
            if (DocType == 161) { this.TransType = 161; this.Table = "OCBI"; this.Description = "Central Bank Ind."; this.PK = "Indicator"; }
            if (DocType == 162) { this.TransType = 162; this.Table = "OMRV"; this.Description = "Inventory Revaluation"; this.PK = "DocEntry"; }
            if (DocType == 163) { this.TransType = 163; this.Table = "OCPI"; this.Description = "A/P Correction Invoice"; this.PK = "DocEntry"; }
            if (DocType == 164) { this.TransType = 164; this.Table = "OCPV"; this.Description = "A/P Correction Invoice Reversal"; this.PK = "DocEntry"; }
            if (DocType == 165) { this.TransType = 165; this.Table = "OCSI"; this.Description = "A/R Correction Invoice"; this.PK = "DocEntry"; }
            if (DocType == 166) { this.TransType = 166; this.Table = "OCSV"; this.Description = "A/R Correction Invoice Reversal"; this.PK = "DocEntry"; }
            if (DocType == 167) { this.TransType = 167; this.Table = "OSCS"; this.Description = "Service Call Statuses"; this.PK = "statusID"; }
            if (DocType == 168) { this.TransType = 168; this.Table = "OSCT"; this.Description = "Service Call Types"; this.PK = "callTypeID"; }
            if (DocType == 169) { this.TransType = 169; this.Table = "OSCP"; this.Description = "Service Call Problem Types"; this.PK = "prblmTypID"; }
            if (DocType == 170) { this.TransType = 170; this.Table = "OCTT"; this.Description = "Contract Template"; this.PK = "TmpltName"; }
            if (DocType == 171) { this.TransType = 171; this.Table = "OHEM"; this.Description = "Employees"; this.PK = "empID"; }
            if (DocType == 172) { this.TransType = 172; this.Table = "OHTY"; this.Description = "Employee Types"; this.PK = "typeID"; }
            if (DocType == 173) { this.TransType = 173; this.Table = "OHST"; this.Description = "Employee Status"; this.PK = "statusID"; }
            if (DocType == 174) { this.TransType = 174; this.Table = "OHTR"; this.Description = "Termination Reason"; this.PK = "reasonID"; }
            if (DocType == 175) { this.TransType = 175; this.Table = "OHED"; this.Description = "Education Types"; this.PK = "edType"; }
            if (DocType == 176) { this.TransType = 176; this.Table = "OINS"; this.Description = "Customer Equipment Card"; this.PK = "insID"; }
            if (DocType == 177) { this.TransType = 177; this.Table = "OAGP"; this.Description = "Agent Name"; this.PK = "AgentCode"; }
            if (DocType == 178) { this.TransType = 178; this.Table = "OWHT"; this.Description = "Withholding Tax"; this.PK = "WTCode"; }
            if (DocType == 180) { this.TransType = 180; this.Table = "OVTR"; this.Description = "Tax Report"; this.PK = "AbsEntry"; }
            if (DocType == 181) { this.TransType = 181; this.Table = "OBOE"; this.Description = "Bill of Exchange for Payment"; this.PK = "BoeKey"; }
            if (DocType == 182) { this.TransType = 182; this.Table = "OBOT"; this.Description = "Bill Of Exchang Transaction"; this.PK = "AbsEntry"; }
            if (DocType == 183) { this.TransType = 183; this.Table = "OFRM"; this.Description = "File Format"; this.PK = "AbsEntry"; }
            if (DocType == 184) { this.TransType = 184; this.Table = "OPID"; this.Description = "Period Indicator"; this.PK = "Indicator"; }
            if (DocType == 185) { this.TransType = 185; this.Table = "ODOR"; this.Description = "Doubtful Debts"; this.PK = "AbsEntry"; }
            if (DocType == 186) { this.TransType = 186; this.Table = "OHLD"; this.Description = "Holiday Table"; this.PK = "HldCode"; }
            if (DocType == 187) { this.TransType = 187; this.Table = "OCRB"; this.Description = "BP – Bank Account"; this.PK = "Country, BankCode, Account, CardCode"; }
            if (DocType == 188) { this.TransType = 188; this.Table = "OSST"; this.Description = "Service Call Solution Statuses"; this.PK = "Number"; }
            if (DocType == 189) { this.TransType = 189; this.Table = "OSLT"; this.Description = "Service Call Solutions"; this.PK = "SltCode"; }
            if (DocType == 190) { this.TransType = 190; this.Table = "OCTR"; this.Description = "Service Contracts"; this.PK = "ContractID"; }
            if (DocType == 191) { this.TransType = 191; this.Table = "OSCL"; this.Description = "Service Calls"; this.PK = "callID"; }
            if (DocType == 192) { this.TransType = 192; this.Table = "OSCO"; this.Description = "Service Call Origins"; this.PK = "originID"; }
            if (DocType == 193) { this.TransType = 193; this.Table = "OUKD"; this.Description = "User Key Description"; this.PK = "TableName, KeyId"; }
            if (DocType == 194) { this.TransType = 194; this.Table = "OQUE"; this.Description = "Queue"; this.PK = "queueID"; }
            if (DocType == 195) { this.TransType = 195; this.Table = "OIWZ"; this.Description = "Inflation Wizard"; this.PK = "AbsEntry"; }
            if (DocType == 196) { this.TransType = 196; this.Table = "ODUT"; this.Description = "Dunning Terms"; this.PK = "TermCode"; }
            if (DocType == 197) { this.TransType = 197; this.Table = "ODWZ"; this.Description = "Dunning Wizard"; this.PK = "WizardId"; }
            if (DocType == 198) { this.TransType = 198; this.Table = "OFCT"; this.Description = "Sales Forecast"; this.PK = "AbsID"; }
            if (DocType == 199) { this.TransType = 199; this.Table = "OMSN"; this.Description = "MRP Scenarios"; this.PK = "AbsEntry"; }
            if (DocType == 200) { this.TransType = 200; this.Table = "OTER"; this.Description = "Territories"; this.PK = "territryID"; }
            if (DocType == 201) { this.TransType = 201; this.Table = "OOND"; this.Description = "Industries"; this.PK = "IndCode"; }
            if (DocType == 202) { this.TransType = 202; this.Table = "OWOR"; this.Description = "Production Order"; this.PK = "DocEntry"; }
            if (DocType == 203) { this.TransType = 203; this.Table = "ODPI"; this.Description = "A/R Down Payment"; this.PK = "DocEntry"; }
            if (DocType == 204) { this.TransType = 204; this.Table = "ODPO"; this.Description = "A/P Down Payment"; this.PK = "DocEntry"; }
            if (DocType == 205) { this.TransType = 205; this.Table = "OPKG"; this.Description = "Package Types"; this.PK = "PkgCode"; }
            if (DocType == 206) { this.TransType = 206; this.Table = "OUDO"; this.Description = "User-Defined Object"; this.PK = "Code"; }
            if (DocType == 207) { this.TransType = 207; this.Table = "ODOW"; this.Description = "Data Ownership – Objects"; this.PK = "Object, SubObject"; }
            if (DocType == 208) { this.TransType = 208; this.Table = "ODOX"; this.Description = "Data Ownership – Exceptions"; this.PK = "QueryId, Object, SubObject"; }
            if (DocType == 210) { this.TransType = 210; this.Table = "OHPS"; this.Description = "Employee Position"; this.PK = "posID"; }
            if (DocType == 211) { this.TransType = 211; this.Table = "OHTM"; this.Description = "Employee Teams"; this.PK = "teamID"; }
            if (DocType == 212) { this.TransType = 212; this.Table = "OORL"; this.Description = "Relationships"; this.PK = "OrlCode"; }
            if (DocType == 213) { this.TransType = 213; this.Table = "ORCM"; this.Description = "Recommendation Data"; this.PK = "DocEntry"; }
            if (DocType == 214) { this.TransType = 214; this.Table = "OUPT"; this.Description = "User Autorization Tree"; this.PK = "AbsId"; }
            if (DocType == 215) { this.TransType = 215; this.Table = "OPDT"; this.Description = "Predefined Text"; this.PK = "AbsEntry"; }
            if (DocType == 216) { this.TransType = 216; this.Table = "OBOX"; this.Description = "Box Definition"; this.PK = "BoxCode, ReportType, BosCode"; }
            if (DocType == 217) { this.TransType = 217; this.Table = "OCLA"; this.Description = "Activity Status"; this.PK = "statusID"; }
            if (DocType == 218) { this.TransType = 218; this.Table = "OCHF"; this.Description = "312"; this.PK = "ObjName"; }
            if (DocType == 219) { this.TransType = 219; this.Table = "OCSHS"; this.Description = "User-Defined Values"; this.PK = "IndexID"; }
            if (DocType == 220) { this.TransType = 220; this.Table = "OACP"; this.Description = "Periods Category"; this.PK = "AbsEntry"; }
            if (DocType == 221) { this.TransType = 221; this.Table = "OATC"; this.Description = "Attachments"; this.PK = "AbsEntry"; }
            if (DocType == 222) { this.TransType = 222; this.Table = "OGFL"; this.Description = "Grid Filter"; this.PK = "FormID, GridID, UserCode"; }
            if (DocType == 223) { this.TransType = 223; this.Table = "OLNG"; this.Description = "User Language Table"; this.PK = "Code"; }
            if (DocType == 224) { this.TransType = 224; this.Table = "OMLT"; this.Description = "Multi-Language Translation"; this.PK = "TranEntry"; }
            if (DocType == 225) { this.TransType = 225; this.Table = "OAPA3"; this.Description = ""; this.PK = ""; }
            if (DocType == 226) { this.TransType = 226; this.Table = "OAPA4"; this.Description = ""; this.PK = ""; }
            if (DocType == 227) { this.TransType = 227; this.Table = "OAPA5"; this.Description = ""; this.PK = ""; }
            if (DocType == 229) { this.TransType = 229; this.Table = "SDIS"; this.Description = "Dynamic Interface (Strings)"; this.PK = "FormId, ItemId, ColumnId, Language"; }
            if (DocType == 230) { this.TransType = 230; this.Table = "OSVR"; this.Description = "Saved Reconciliations"; this.PK = "acctCode"; }
            if (DocType == 231) { this.TransType = 231; this.Table = "DSC1"; this.Description = "House Bank Accounts"; this.PK = "AbsEntry"; }
            if (DocType == 232) { this.TransType = 232; this.Table = "RDOC"; this.Description = "Document"; this.PK = "DocCode"; }
            if (DocType == 233) { this.TransType = 233; this.Table = "ODGP"; this.Description = "Document Generation Parameter Sets"; this.PK = "AbsEntry"; }
            if (DocType == 234) { this.TransType = 234; this.Table = "OMHD"; this.Description = "#740"; this.PK = "AlertCode"; }
            if (DocType == 238) { this.TransType = 238; this.Table = "OACG"; this.Description = "Account Category"; this.PK = "AbsId"; }
            if (DocType == 239) { this.TransType = 239; this.Table = "OBCA"; this.Description = "Bank Charges Allocation Codes"; this.PK = "Code"; }
            if (DocType == 241) { this.TransType = 241; this.Table = "OCFT"; this.Description = "Cash Flow Transactions – Rows"; this.PK = "CFTId"; }
            if (DocType == 242) { this.TransType = 242; this.Table = "OCFW"; this.Description = "Cash Flow Line Item"; this.PK = "CFWId"; }
            if (DocType == 247) { this.TransType = 247; this.Table = "OBPL"; this.Description = "Business Place"; this.PK = "BPLId"; }
            if (DocType == 250) { this.TransType = 250; this.Table = "OJPE"; this.Description = "Local Era Calendar"; this.PK = "Code"; }
            if (DocType == 251) { this.TransType = 251; this.Table = "ODIM"; this.Description = "Cost Accounting Dimension"; this.PK = "DimCode"; }
            if (DocType == 254) { this.TransType = 254; this.Table = "OSCD"; this.Description = "Service Code Table"; this.PK = "AbsEntry"; }
            if (DocType == 255) { this.TransType = 255; this.Table = "OSGP"; this.Description = "Service Group for Brazil"; this.PK = "AbsEntry"; }
            if (DocType == 256) { this.TransType = 256; this.Table = "OMGP"; this.Description = "Material Group"; this.PK = "AbsEntry"; }
            if (DocType == 257) { this.TransType = 257; this.Table = "ONCM"; this.Description = "NCM Code"; this.PK = "AbsEntry"; }
            if (DocType == 258) { this.TransType = 258; this.Table = "OCFP"; this.Description = "CFOP for Nota Fiscal"; this.PK = "ID"; }
            if (DocType == 259) { this.TransType = 259; this.Table = "OTSC"; this.Description = "CST Code for Nota Fiscal"; this.PK = "ID"; }
            if (DocType == 260) { this.TransType = 260; this.Table = "OUSG"; this.Description = "Usage of Nota Fiscal"; this.PK = "ID"; }
            if (DocType == 261) { this.TransType = 261; this.Table = "OCDP"; this.Description = "Closing Date Procedure"; this.PK = "ClsDateNum"; }
            if (DocType == 263) { this.TransType = 263; this.Table = "ONFN"; this.Description = "Nota Fiscal Numbering"; this.PK = "ObjectCode, DocSubType"; }
            if (DocType == 264) { this.TransType = 264; this.Table = "ONFT"; this.Description = "Nota Fiscal Tax Category (Brazil)"; this.PK = "AbsId"; }
            if (DocType == 265) { this.TransType = 265; this.Table = "OCNT"; this.Description = "Counties"; this.PK = "AbsId"; }
            if (DocType == 266) { this.TransType = 266; this.Table = "OTCD"; this.Description = "Tax Code Determination"; this.PK = "AbsId"; }
            if (DocType == 267) { this.TransType = 267; this.Table = "ODTY"; this.Description = "BoE Document Type"; this.PK = "AbsEntry"; }
            if (DocType == 268) { this.TransType = 268; this.Table = "OPTF"; this.Description = "BoE Portfolio"; this.PK = "AbsEntry"; }
            if (DocType == 269) { this.TransType = 269; this.Table = "OIST"; this.Description = "BoE Instruction"; this.PK = "AbsEntry"; }
            if (DocType == 271) { this.TransType = 271; this.Table = "OTPS"; this.Description = "Tax Parameter"; this.PK = "AbsId"; }
            if (DocType == 275) { this.TransType = 275; this.Table = "OTFC"; this.Description = "Tax Type Combination"; this.PK = "AbsId"; }
            if (DocType == 276) { this.TransType = 276; this.Table = "OFML"; this.Description = "Tax Formula Master Table"; this.PK = "AbsId"; }
            if (DocType == 278) { this.TransType = 278; this.Table = "OCNA"; this.Description = "CNAE Code"; this.PK = "AbsId"; }
            if (DocType == 280) { this.TransType = 280; this.Table = "OTSI"; this.Description = "Sales Tax Invoice"; this.PK = "DocEntry"; }
            if (DocType == 281) { this.TransType = 281; this.Table = "OTPI"; this.Description = "Purchase Tax Invoice"; this.PK = "DocEntry"; }
            if (DocType == 283) { this.TransType = 283; this.Table = "OCCD"; this.Description = "Cargo Customs Declaration Numbers"; this.PK = "CCDNum"; }
            if (DocType == 290) { this.TransType = 290; this.Table = "ORSC"; this.Description = "Resources"; this.PK = "ResCode"; }
            if (DocType == 291) { this.TransType = 291; this.Table = "ORSG"; this.Description = "Resource Properties"; this.PK = "ResTypCod"; }
            if (DocType == 292) { this.TransType = 292; this.Table = "ORSB"; this.Description = "ResGrpCod"; this.PK = "ResGrpCod"; }
            if (DocType == 321) { this.TransType = 321; this.Table = "OITR"; this.Description = "Internal Reconciliation"; this.PK = "ReconNum"; }
            if (DocType == 541) { this.TransType = 541; this.Table = "OPOS"; this.Description = "POS Master Data"; this.PK = "EquipNo"; }
            if (DocType == 1179) { this.TransType = 1179; this.Table = "ODRF"; this.Description = "Stock Transfer Draft"; this.PK = "DocEntry"; }
            if (DocType == 10000044) { this.TransType = 10000044; this.Table = "OBTN"; this.Description = "Batch Numbers Master Data"; this.PK = "AbsEntry"; }
            if (DocType == 10000045) { this.TransType = 10000045; this.Table = "OSRN"; this.Description = "Serial Numbers Master Data"; this.PK = "AbsEntry"; }
            if (DocType == 10000062) { this.TransType = 10000062; this.Table = "OIVK"; this.Description = "IVL Vs OINM Keys"; this.PK = "TransSeq"; }
            if (DocType == 10000071) { this.TransType = 10000071; this.Table = "OIQR"; this.Description = "Inventory Posting"; this.PK = "DocEntry"; }
            if (DocType == 10000073) { this.TransType = 10000073; this.Table = "OFYM"; this.Description = "Financial Year Master"; this.PK = "AbsId"; }
            if (DocType == 10000074) { this.TransType = 10000074; this.Table = "OSEC"; this.Description = "Sections"; this.PK = "AbsId"; }
            if (DocType == 10000075) { this.TransType = 10000075; this.Table = "OCSN"; this.Description = "Certificate Series"; this.PK = "AbsId"; }
            if (DocType == 10000077) { this.TransType = 10000077; this.Table = "ONOA"; this.Description = "Nature of Assessee"; this.PK = "AbsId"; }
            if (DocType == 10000105) { this.TransType = 10000105; this.Table = "OMSG"; this.Description = "Messaging Service Settings"; this.PK = "USERID"; }
            if (DocType == 10000196) { this.TransType = 10000196; this.Table = "RTYP"; this.Description = "Document Type List"; this.PK = "CODE"; }
            if (DocType == 10000197) { this.TransType = 10000197; this.Table = "OUGP"; this.Description = "UoM Group"; this.PK = "UgpEntry"; }
            if (DocType == 10000199) { this.TransType = 10000199; this.Table = "OUOM"; this.Description = "UoM Master Data"; this.PK = "UomEntry"; }
            if (DocType == 10000203) { this.TransType = 10000203; this.Table = "OBFC"; this.Description = "Bin Field Configuration"; this.PK = "AbsEntry"; }
            if (DocType == 10000204) { this.TransType = 10000204; this.Table = "OBAT"; this.Description = "Bin Location Attribute"; this.PK = "AbsEntry"; }
            if (DocType == 10000205) { this.TransType = 10000205; this.Table = "OBSL"; this.Description = "Warehouse Sublevel"; this.PK = "AbsEntry"; }
            if (DocType == 10000206) { this.TransType = 10000206; this.Table = "OBIN"; this.Description = "Bin Location"; this.PK = "AbsEntry"; }
            if (DocType == 140000041) { this.TransType = 140000041; this.Table = "ODNF"; this.Description = "DNF Code"; this.PK = "AbsEntry"; }
            if (DocType == 231000000) { this.TransType = 231000000; this.Table = "OUGR"; this.Description = "Authorization Group"; this.PK = "GroupId"; }
            if (DocType == 234000004) { this.TransType = 234000004; this.Table = "OEGP"; this.Description = "E-Mail Group"; this.PK = "EmlGrpCode"; }
            if (DocType == 243000001) { this.TransType = 243000001; this.Table = "OGPC"; this.Description = "Government Payment Code"; this.PK = "AbsId"; }
            if (DocType == 310000001) { this.TransType = 310000001; this.Table = "OIQI"; this.Description = "Inventory Opening Balance"; this.PK = "DocEntry"; }
            if (DocType == 310000008) { this.TransType = 310000008; this.Table = "OBTW"; this.Description = "Batch Attributes in Location"; this.PK = "AbsEntry"; }
            if (DocType == 410000005) { this.TransType = 410000005; this.Table = "OLLF"; this.Description = "Legal List Format"; this.PK = "AbsEntry"; }
            if (DocType == 480000001) { this.TransType = 480000001; this.Table = "OHET"; this.Description = "Object: HR Employee Transfer"; this.PK = "TransferID"; }
            if (DocType == 540000005) { this.TransType = 540000005; this.Table = "OTCX"; this.Description = "Tax Code Determination"; this.PK = "DocEntry"; }
            if (DocType == 540000006) { this.TransType = 540000006; this.Table = "OPQT"; this.Description = "Purchase Quotation"; this.PK = "DocEntry"; }
            if (DocType == 540000040) { this.TransType = 540000040; this.Table = "ORCP"; this.Description = "Recurring Transaction Template"; this.PK = "AbsEntry"; }
            if (DocType == 540000042) { this.TransType = 540000042; this.Table = "OCCT"; this.Description = "Cost Center Type"; this.PK = "CctCode"; }
            if (DocType == 540000048) { this.TransType = 540000048; this.Table = "OACR"; this.Description = "Accrual Type"; this.PK = "Code"; }
            if (DocType == 540000056) { this.TransType = 540000056; this.Table = "ONFM"; this.Description = "Nota Fiscal Model"; this.PK = "AbsEntry"; }
            if (DocType == 540000067) { this.TransType = 540000067; this.Table = "OBFI"; this.Description = "Brazil Fuel Indexer"; this.PK = "ID"; }
            if (DocType == 540000068) { this.TransType = 540000068; this.Table = "OBBI"; this.Description = "Brazil Beverage Indexer"; this.PK = "ID"; }
            if (DocType == 1210000000) { this.TransType = 1210000000; this.Table = "OCPT"; this.Description = "Cockpit Main Table"; this.PK = "AbsEntry"; }
            if (DocType == 1250000001) { this.TransType = 1250000001; this.Table = "OWTQ"; this.Description = "Inventory Transfer Request"; this.PK = "DocEntry"; }
            if (DocType == 1250000025) { this.TransType = 1250000025; this.Table = "OOAT"; this.Description = "Blanket Agreement"; this.PK = "AbsID"; }
            if (DocType == 1320000000) { this.TransType = 1320000000; this.Table = "OKPI"; this.Description = "Key Performance Indicator Package"; this.PK = "AbsEntry"; }
            if (DocType == 1320000002) { this.TransType = 1320000002; this.Table = "OTGG"; this.Description = "Target Group"; this.PK = "TargetCode"; }
            if (DocType == 1320000012) { this.TransType = 1320000012; this.Table = "OCPN"; this.Description = "Campaign"; this.PK = "CpnNo"; }
            if (DocType == 1320000028) { this.TransType = 1320000028; this.Table = "OROC"; this.Description = "Retorno Operation Codes"; this.PK = "AbsEntry"; }
            if (DocType == 1320000039) { this.TransType = 1320000039; this.Table = "OPSC"; this.Description = "Product Source Code"; this.PK = "Code"; }
            if (DocType == 1470000000) { this.TransType = 1470000000; this.Table = "ODTP"; this.Description = "Fixed Assets Depreciation Types"; this.PK = "Code"; }
            if (DocType == 1470000002) { this.TransType = 1470000002; this.Table = "OADT"; this.Description = "Fixed Assets Account Determination"; this.PK = "Code"; }
            if (DocType == 1470000003) { this.TransType = 1470000003; this.Table = "ODPA"; this.Description = "Fixed Asset Depreciation Areas"; this.PK = "Code"; }
            if (DocType == 1470000004) { this.TransType = 1470000004; this.Table = "ODPP"; this.Description = "Depreciation Type Pools"; this.PK = "Code"; }
            if (DocType == 1470000032) { this.TransType = 1470000032; this.Table = "OACS"; this.Description = "Asset Classes"; this.PK = "Code"; }
            if (DocType == 1470000046) { this.TransType = 1470000046; this.Table = "OAGS"; this.Description = "Asset Groups"; this.PK = "Code"; }
            if (DocType == 1470000048) { this.TransType = 1470000048; this.Table = "ODMC"; this.Description = "G/L Account Determination Criteria – Inventory"; this.PK = "DmcId"; }
            if (DocType == 1470000049) { this.TransType = 1470000049; this.Table = "OACQ"; this.Description = "Capitalization"; this.PK = "DocEntry"; }
            if (DocType == 1470000057) { this.TransType = 1470000057; this.Table = "OGAR"; this.Description = "G/L Account Advanced Rules"; this.PK = "AbsEntry"; }
            if (DocType == 1470000060) { this.TransType = 1470000060; this.Table = "OACD"; this.Description = "Credit Memo"; this.PK = "DocEntry"; }
            if (DocType == 1470000062) { this.TransType = 1470000062; this.Table = "OBCD"; this.Description = "Bar Code Master Data"; this.PK = "BcdEntry"; }
            if (DocType == 1470000065) { this.TransType = 1470000065; this.Table = "OINC"; this.Description = "Inventory Counting"; this.PK = "DocEntry"; }
            if (DocType == 1470000077) { this.TransType = 1470000077; this.Table = "OEDG"; this.Description = "Discount Groups"; this.PK = "AbsEntry"; }
            if (DocType == 1470000092) { this.TransType = 1470000092; this.Table = "OCCS"; this.Description = "Cycle Count Determination"; this.PK = "WhsCode"; }
            if (DocType == 1470000113) { this.TransType = 1470000113; this.Table = "OPRQ"; this.Description = "Purchase Request"; this.PK = "DocEntry"; }
            if (DocType == 1620000000) { this.TransType = 1620000000; this.Table = "OWLS"; this.Description = "Workflow – Task Details"; this.PK = "TaskID"; }
        }
        public int TransType { get; set; }
        public string Table { get; set; }
        public string Description { get; set; }
        public string PK { get; set; }
    }
}
