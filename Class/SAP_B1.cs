using ICON.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICON.SAP.API
{
    public class SAP_B1
    {
        protected ICON.SAP.API.SAPB1 _SAPB1 = null;
        protected SAP_B1Header _B1Header = null;
        protected SAPbobsCOM.Company oCompany = ICON.SAP.API.SAPB1.oCompany;
        public SAP_TABLE SAP_TABLE;
        public SAP_B1()
        {
        }
        public Dictionary<string, object> ProcessSAP_B1(SAP_B1Header B1Header)
        {
            try
            {
                this._B1Header = B1Header;

                switch (this._B1Header.Method.ToLower() + "|" + this._B1Header.Event.ToLower())
                {
                    case "error|reset":
                        {
                            this.Process_Error_Reset();
                            break;
                        }
                    case "transaction|init":
                        {
                            this.Process_Transaction_Init();
                            break;
                        }
                    case "transaction|begintransaction":
                        {
                            this.Process_Transaction_BeginTransaction();
                            break;
                        }
                    case "transaction|committransaction":
                        {
                            this.Process_Transaction_CommitTransaction();
                            break;
                        }
                    case "transaction|rollbacktransaction":
                        {
                            this.Process_Transaction_RollbackTransaction();
                            break;
                        }
                    case "transaction|dispose":
                        {
                            this.Process_Transaction_Dispose();
                            break;
                        }
                    case "journalentries|create":
                        {
                            this.Process_Error_Reset();
                            this.Process_JournalEntries_Create();
                            break;
                        }
                    case "journalvouchers|create":
                        {
                            this.Process_Error_Reset();
                            this.Process_JournalVouchers_Create();
                            break;
                        }
                    case "documents|create":
                        {
                            this.Process_Error_Reset();
                            this.Process_Documents_Create();
                            break;
                        }
                    case "documents|cancel":
                        {
                            this.Process_Error_Reset();
                            this.Process_Documents_Cancel();
                            break;
                        }
                    case "documents|remove":
                        {
                            this.Process_Error_Reset();
                            this.Process_Documents_Remove();
                            break;
                        }
                    case "payments|create":
                        {
                            this.Process_Error_Reset();
                            this.Process_Payments_Create();
                            break;
                        }
                    case "payments|cancel":
                        {
                            this.Process_Error_Reset();
                            this.Process_Payments_Cancel();
                            break;
                        }
                }

                Dictionary<string, object> ReturnDic = new Dictionary<string, object>();
                return ReturnDic;
            }
            catch (Exception ex)
            {
                throw new Exception("Error ICON.SAP.API.SAP_B1.ProcessSAP_B1(METHOD: " + this._B1Header.Method + "/EVENT: " + this._B1Header.Event + ") : " + ex.Message);
            }
        }

        #region Private Zone
        private void Process_Error_Reset()
        {
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            InterfaceLogDetail LogDetail = this._B1Header.B1Entity.LogDetail;
            try
            {
                if (Log != null)
                {
                    Log.EditSAPStatusCode = true;
                    Log.EditSAPErrorMessage = true;
                    Log.SAPStatusCode = null;
                    Log.SAPErrorMessage = null;
                }
                if (LogDetail != null)
                {
                    LogDetail.EditREMResponseCode = true;
                    LogDetail.EditREMErrorMessage = true;
                    LogDetail.EditSAPStatusCode = true;
                    LogDetail.EditSAPErrorMessage = true;
                    LogDetail.REMResponseCode = null;
                    LogDetail.REMErrorMessage = null;
                    LogDetail.SAPStatusCode = null;
                    LogDetail.SAPErrorMessage = null;
                }
            }
            catch (Exception ex)
            {
                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_Error_Reset : " + Msg);
            }
        }
        private void Process_Transaction_Init()
        {
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            try
            {
                ICON.Configuration.SAPServer SAPServer = ICON.Configuration.Database.SAP_Server;
                string CompanyDB = string.IsNullOrEmpty(this._B1Header.B1Entity.LogDetail.CompanyDB) ? SAPServer.CompanyDB : this._B1Header.B1Entity.LogDetail.CompanyDB;
                this._SAPB1 = new ICON.SAP.API.SAPB1(
                    SAPServer.DBServer,
                    SAPServer.DBServerType,
                    CompanyDB,
                    SAPServer.UserName,
                    SAPServer.Password,
                    SAPServer.LicenseServer,
                    SAPServer.SLDAddress,
                    SAPServer.Language
                    );
                this._SAPB1.ConnectCompanyDB();
                this.oCompany = ICON.SAP.API.SAPB1.oCompany;
            }
            catch (Exception ex)
            {
                string Msg = "";
                try
                {
                    if (ex.Message.Contains("connected to a company"))
                    {
                        Msg += ex.Message + "/ ";
                        this._SAPB1.DisConnectCompanyDB();
                        this._SAPB1.ConnectCompanyDB();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex2)
                {
                    Msg += ex2.Message;
                    throw new Exception("/ Error Process_Transaction_Init : " + Msg);
                }
            }
        }
        private void Process_Transaction_BeginTransaction()
        {
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            try
            {
                this.oCompany.StartTransaction();
            }
            catch (Exception ex)
            {
                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_Transaction_BeginTransaction : " + Msg);
            }
        }
        private void Process_Transaction_CommitTransaction()
        {
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            try
            {
                if (this.oCompany.InTransaction)
                {
                    this.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_Transaction_CommitTransaction : " + Msg);
            }
        }
        private void Process_Transaction_RollbackTransaction()
        {
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            try
            {
                if (this.oCompany.InTransaction)
                {
                    this.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }
            }
            catch (Exception ex)
            {
                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_Transaction_RollbackTransaction : " + Msg);
            }
        }
        private void Process_Transaction_Dispose()
        {
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            try
            {
                if (this.oCompany.InTransaction)
                {
                    this.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }
                this._SAPB1.DisConnectCompanyDB();
            }
            catch (Exception ex)
            {
                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_Transaction_Dispose : " + Msg);
            }
        }
        private void Process_JournalEntries_Create()
        {
            int lRetCode = 0;
            int SAPErrorCode = 0;
            string SAPErrorMessage = "";
            string BatchNum = null;
            string TranID = null;
            string DocNum = null;
            string GLTranID = null;
            string GLDocNum = null;
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            InterfaceLogDetail LogDetail = this._B1Header.B1Entity.LogDetail;
            try
            {
                List<JournalEntries> JE_List = this._B1Header.B1Entity.JE_List;
                List<JournalEntries_Lines> JELine_List = this._B1Header.B1Entity.JELine_List;
                List<Fields> JE_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.JournalEntries));
                List<Fields> JELine_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.JournalEntries_Lines));
                foreach (JournalEntries JE in JE_List)
                {
                    SAPbobsCOM.BoObjectTypes L_DocType;
                    if (!Enum.TryParse<SAPbobsCOM.BoObjectTypes>(JE.L_DocType, true, out L_DocType)) throw new Exception("Invalid DocType");
                    this.SAP_TABLE = new SAP_TABLE(Convert.ToInt32(L_DocType));
                    SAPbobsCOM.JournalEntries oJE = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(L_DocType);

                    #region Set JournalEntries
                    SAPbobsCOM.BoYesNoEnum AutomaticWT;
                    SAPbobsCOM.BoYesNoEnum AutoVAT;
                    SAPbobsCOM.BoYesNoEnum BlockDunningLetter;
                    SAPbobsCOM.BoYesNoEnum Corisptivi;
                    SAPbobsCOM.BoYesNoEnum DeferredTax;
                    SAPbobsCOM.ECDPostingTypeEnum ECDPostingType;
                    SAPbobsCOM.BoYesNoEnum ExcludeFromTaxReportControlStatementVAT;
                    SAPbobsCOM.BoYesNoEnum IsCostCenterTransfer;
                    SAPbobsCOM.OperationCodeTypeEnum OperationCode;
                    SAPbobsCOM.BoYesNoEnum Report347;
                    SAPbobsCOM.BoYesNoEnum ReportEU;
                    SAPbobsCOM.ResidenceNumberTypeEnum ResidenceNumberType;
                    SAPbobsCOM.BoYesNoEnum StampTax;
                    SAPbobsCOM.BoYesNoEnum UseAutoStorno;
                    if (!string.IsNullOrEmpty(JE.AutomaticWT) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.AutomaticWT, true, out AutomaticWT)) oJE.AutomaticWT = AutomaticWT;
                    if (!string.IsNullOrEmpty(JE.AutoVAT) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.AutoVAT, true, out AutoVAT)) oJE.AutoVAT = AutoVAT;
                    if (!string.IsNullOrEmpty(JE.BlockDunningLetter) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.BlockDunningLetter, true, out BlockDunningLetter)) oJE.BlockDunningLetter = BlockDunningLetter;
                    if (!string.IsNullOrEmpty(JE.Corisptivi) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.Corisptivi, true, out Corisptivi)) oJE.Corisptivi = Corisptivi;
                    if (!string.IsNullOrEmpty(JE.DeferredTax) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.DeferredTax, true, out DeferredTax)) oJE.DeferredTax = DeferredTax;
                    if (!string.IsNullOrEmpty(JE.DocumentType)) oJE.DocumentType = JE.DocumentType;
                    if (JE.DueDate.HasValue) oJE.DueDate = (DateTime)JE.DueDate;
                    if (!string.IsNullOrEmpty(JE.ECDPostingType) && Enum.TryParse<SAPbobsCOM.ECDPostingTypeEnum>(JE.ECDPostingType, true, out ECDPostingType)) oJE.ECDPostingType = ECDPostingType;
                    if (!string.IsNullOrEmpty(JE.ExcludeFromTaxReportControlStatementVAT) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.ExcludeFromTaxReportControlStatementVAT, true, out ExcludeFromTaxReportControlStatementVAT)) oJE.ExcludeFromTaxReportControlStatementVAT = ExcludeFromTaxReportControlStatementVAT;
                    if (JE.ExposedTransNumber.HasValue) oJE.ExposedTransNumber = (int)JE.ExposedTransNumber;
                    if (!string.IsNullOrEmpty(JE.Indicator)) oJE.Indicator = JE.Indicator;
                    if (!string.IsNullOrEmpty(JE.IsCostCenterTransfer) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.IsCostCenterTransfer, true, out IsCostCenterTransfer)) oJE.IsCostCenterTransfer = IsCostCenterTransfer;
                    if (JE.LocationCode.HasValue) oJE.LocationCode = (int)JE.LocationCode;
                    if (!string.IsNullOrEmpty(JE.Memo)) oJE.Memo = JE.Memo;
                    if (!string.IsNullOrEmpty(JE.OperationCode) && Enum.TryParse<SAPbobsCOM.OperationCodeTypeEnum>(JE.OperationCode, true, out OperationCode)) oJE.OperationCode = OperationCode;
                    if (!string.IsNullOrEmpty(JE.ProjectCode)) oJE.ProjectCode = JE.ProjectCode;
                    if (!string.IsNullOrEmpty(JE.Reference)) oJE.Reference = JE.Reference;
                    if (!string.IsNullOrEmpty(JE.Reference2)) oJE.Reference2 = JE.Reference2;
                    if (!string.IsNullOrEmpty(JE.Reference3)) oJE.Reference3 = JE.Reference3;
                    if (JE.ReferenceDate.HasValue) oJE.ReferenceDate = (DateTime)JE.ReferenceDate;
                    if (!string.IsNullOrEmpty(JE.Report347) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.Report347, true, out Report347)) oJE.Report347 = Report347;
                    if (!string.IsNullOrEmpty(JE.ReportEU) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.ReportEU, true, out ReportEU)) oJE.ReportEU = ReportEU;
                    if (!string.IsNullOrEmpty(JE.ReportingSectionControlStatementVAT)) oJE.ReportingSectionControlStatementVAT = JE.ReportingSectionControlStatementVAT;
                    if (!string.IsNullOrEmpty(JE.ResidenceNumberType) && Enum.TryParse<SAPbobsCOM.ResidenceNumberTypeEnum>(JE.ResidenceNumberType, true, out ResidenceNumberType)) oJE.ResidenceNumberType = ResidenceNumberType;
                    if (JE.Series.HasValue) oJE.Series = (int)JE.Series;
                    if (!string.IsNullOrEmpty(JE.StampTax) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.StampTax, true, out StampTax)) oJE.StampTax = StampTax;
                    if (JE.StornoDate.HasValue) oJE.StornoDate = (DateTime)JE.StornoDate;
                    if (JE.TaxDate.HasValue) oJE.TaxDate = (DateTime)JE.TaxDate;
                    if (!string.IsNullOrEmpty(JE.TransactionCode)) oJE.TransactionCode = JE.TransactionCode;
                    if (!string.IsNullOrEmpty(JE.UseAutoStorno) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.UseAutoStorno, true, out UseAutoStorno)) oJE.UseAutoStorno = UseAutoStorno;
                    if (JE.VatDate.HasValue) oJE.VatDate = (DateTime)JE.VatDate;
                    #endregion

                    #region JournalEntries User Defined Fields
                    foreach (Fields JE_UField in JE_UField_List.FindAll(x => x.L_InterfaceLogDetailID == JE.L_InterfaceLogDetailID))
                    {
                        if (!string.IsNullOrEmpty(JE_UField.Value)) oJE.UserFields.Fields.Item(JE_UField.L_Name).Value = JE_UField.Value;
                        if (!string.IsNullOrEmpty(JE_UField.ValidValue)) oJE.UserFields.Fields.Item(JE_UField.L_Name).ValidValue = JE_UField.ValidValue;
                    }
                    #endregion

                    foreach (JournalEntries_Lines JELine in JELine_List.FindAll(x => x.L_InterfaceLogDetailID == JE.L_InterfaceLogDetailID && x.L_JournalEntriesID == JE.L_JournalEntriesID))
                    {
                        SAPbobsCOM.JournalEntries_Lines oLines = oJE.Lines;
                        if (JELine.L_LineNum != 0) oLines.Add();

                        #region Set JournalEntries_Lines
                        SAPbobsCOM.BoYesNoEnum PaymentBlock;
                        SAPbobsCOM.BoTaxPostAccEnum TaxPostAccount;
                        SAPbobsCOM.BoYesNoEnum VatLine;
                        SAPbobsCOM.BoYesNoEnum WTLiable;
                        SAPbobsCOM.BoYesNoEnum WTRow;
                        if (!string.IsNullOrEmpty(JELine.AccountCode)) oLines.AccountCode = JELine.AccountCode;
                        if (!string.IsNullOrEmpty(JELine.AdditionalReference)) oLines.AdditionalReference = JELine.AdditionalReference;
                        if (JELine.BaseSum.HasValue) oLines.BaseSum = (double)JELine.BaseSum;
                        if (JELine.BlockReason.HasValue) oLines.BlockReason = (int)JELine.BlockReason;
                        if (JELine.BPLID.HasValue) oLines.BPLID = (int)JELine.BPLID;
                        if (!string.IsNullOrEmpty(JELine.ContraAccount)) oLines.ContraAccount = JELine.ContraAccount;
                        if (!string.IsNullOrEmpty(JELine.ControlAccount)) oLines.ControlAccount = JELine.ControlAccount;
                        if (!string.IsNullOrEmpty(JELine.CostingCode)) oLines.CostingCode = JELine.CostingCode;
                        if (!string.IsNullOrEmpty(JELine.CostingCode2)) oLines.CostingCode2 = JELine.CostingCode2;
                        if (!string.IsNullOrEmpty(JELine.CostingCode3)) oLines.CostingCode3 = JELine.CostingCode3;
                        if (!string.IsNullOrEmpty(JELine.CostingCode4)) oLines.CostingCode4 = JELine.CostingCode4;
                        if (!string.IsNullOrEmpty(JELine.CostingCode5)) oLines.CostingCode5 = JELine.CostingCode5;
                        if (JELine.Credit.HasValue) oLines.Credit = (double)JELine.Credit;
                        if (JELine.CreditSys.HasValue) oLines.CreditSys = (double)JELine.CreditSys;
                        if (JELine.Debit.HasValue) oLines.Debit = (double)JELine.Debit;
                        if (JELine.DebitSys.HasValue) oLines.DebitSys = (double)JELine.DebitSys;
                        if (JELine.DueDate.HasValue) oLines.DueDate = (DateTime)JELine.DueDate;
                        if (JELine.ExposedTransNumber.HasValue) oLines.ExposedTransNumber = (int)JELine.ExposedTransNumber;
                        if (JELine.FCCredit.HasValue) oLines.FCCredit = (double)JELine.FCCredit;
                        if (!string.IsNullOrEmpty(JELine.FCCurrency)) oLines.FCCurrency = JELine.FCCurrency;
                        if (JELine.FCDebit.HasValue) oLines.FCDebit = (double)JELine.FCDebit;
                        if (!string.IsNullOrEmpty(JELine.FederalTaxID)) oLines.FederalTaxID = JELine.FederalTaxID;
                        if (JELine.GrossValue.HasValue) oLines.GrossValue = (double)JELine.GrossValue;
                        if (!string.IsNullOrEmpty(JELine.LineMemo)) oLines.LineMemo = JELine.LineMemo;
                        if (JELine.LocationCode.HasValue) oLines.LocationCode = (int)JELine.LocationCode;
                        if (!string.IsNullOrEmpty(JELine.PaymentBlock) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JELine.PaymentBlock, true, out PaymentBlock)) oLines.PaymentBlock = PaymentBlock;
                        if (!string.IsNullOrEmpty(JELine.ProjectCode)) oLines.ProjectCode = JELine.ProjectCode;
                        if (!string.IsNullOrEmpty(JELine.Reference1)) oLines.Reference1 = JELine.Reference1;
                        if (!string.IsNullOrEmpty(JELine.Reference2)) oLines.Reference2 = JELine.Reference2;
                        if (JELine.ReferenceDate1.HasValue) oLines.ReferenceDate1 = (DateTime)JELine.ReferenceDate1;
                        if (JELine.ReferenceDate2.HasValue) oLines.ReferenceDate2 = (DateTime)JELine.ReferenceDate2;
                        if (!string.IsNullOrEmpty(JELine.ShortName)) oLines.ShortName = JELine.ShortName;
                        if (JELine.SystemBaseAmount.HasValue) oLines.SystemBaseAmount = (double)JELine.SystemBaseAmount;
                        if (JELine.SystemVatAmount.HasValue) oLines.SystemVatAmount = (double)JELine.SystemVatAmount;
                        if (!string.IsNullOrEmpty(JELine.TaxCode)) oLines.TaxCode = JELine.TaxCode;
                        if (JELine.TaxDate.HasValue) oLines.TaxDate = (DateTime)JELine.TaxDate;
                        if (!string.IsNullOrEmpty(JELine.TaxGroup)) oLines.TaxGroup = JELine.TaxGroup;
                        if (!string.IsNullOrEmpty(JELine.TaxPostAccount) && Enum.TryParse<SAPbobsCOM.BoTaxPostAccEnum>(JELine.TaxPostAccount, true, out TaxPostAccount)) oLines.TaxPostAccount = TaxPostAccount;
                        if (JELine.VatAmount.HasValue) oLines.VatAmount = (double)JELine.VatAmount;
                        if (JELine.VatDate.HasValue) oLines.VatDate = (DateTime)JELine.VatDate;
                        if (!string.IsNullOrEmpty(JELine.VatLine) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JELine.VatLine, true, out VatLine)) oLines.VatLine = VatLine;
                        if (!string.IsNullOrEmpty(JELine.WTLiable) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JELine.WTLiable, true, out WTLiable)) oLines.WTLiable = WTLiable;
                        if (!string.IsNullOrEmpty(JELine.WTRow) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JELine.WTRow, true, out WTRow)) oLines.WTRow = WTRow;
                        #endregion

                        #region JournalEntries_Lines User Defined Fields
                        foreach (Fields JELine_UField in JELine_UField_List.FindAll(x => x.L_InterfaceLogDetailID == JE.L_InterfaceLogDetailID))
                        {
                            if (!string.IsNullOrEmpty(JELine_UField.Value)) oJE.UserFields.Fields.Item(JELine_UField.L_Name).Value = JELine_UField.Value;
                            if (!string.IsNullOrEmpty(JELine_UField.ValidValue)) oJE.UserFields.Fields.Item(JELine_UField.L_Name).Value = JELine_UField.ValidValue;
                        }
                        #endregion
                    }

                    lRetCode = oJE.Add();

                    if (lRetCode != 0)
                    {
                        this.oCompany.GetLastError(out SAPErrorCode, out SAPErrorMessage);

                        LogDetail.SAPStatusCode = SAPErrorCode.ToString();
                        LogDetail.SAPErrorMessage = SAPErrorMessage;
                        Log.SAPStatusCode = LogDetail.SAPStatusCode;
                        Log.SAPErrorMessage = LogDetail.SAPErrorMessage;
                        throw new Exception($"SAP Error: Code {SAPErrorCode} {SAPErrorMessage}");
                    }

                    this.oCompany.GetNewObjectCode(out TranID);
                    DocNum = this.GetGLDocNum(TranID);
                    GLDocNum = DocNum;
                    if (string.IsNullOrEmpty(Log.SAPRefID))
                    {
                        Log.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                        Log.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                        Log.SAPRefNo = DocNum;
                        Log.SAPRefGLNo = GLDocNum;
                    }
                    LogDetail.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                    LogDetail.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                    LogDetail.SAPRefNo = DocNum;
                    LogDetail.SAPRefGLNo = GLDocNum;
                    LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.OK;
                    LogDetail.SAPStatusCode = lRetCode.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SAPRefID = null;
                Log.SAPRefNo = null;
                Log.SAPRefGLNo = null;
                LogDetail.SAPRefID = null;
                LogDetail.SAPRefNo = null;
                LogDetail.SAPRefGLNo = null;
                if (string.IsNullOrEmpty(LogDetail.SAPStatusCode)) LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.InternalServerError;
                if (string.IsNullOrEmpty(LogDetail.SAPErrorMessage)) LogDetail.REMErrorMessage = ex.Message;

                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_JournalEntries_Create : " + Msg);
            }
        }
        private void Process_JournalVouchers_Create()
        {
            int lRetCode = 0;
            int SAPErrorCode = 0;
            string SAPErrorMessage = "";
            string BatchNum = null;
            string TranID = null;
            string DocNum = null;
            string GLTranID = null;
            string GLDocNum = null;
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            InterfaceLogDetail LogDetail = this._B1Header.B1Entity.LogDetail;
            try
            {
                List<JournalEntries> JE_List = this._B1Header.B1Entity.JE_List;
                List<JournalEntries_Lines> JELine_List = this._B1Header.B1Entity.JELine_List;
                List<Fields> JE_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.JournalEntries));
                List<Fields> JELine_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.JournalEntries_Lines));
                foreach (JournalEntries JE in JE_List)
                {
                    SAPbobsCOM.BoObjectTypes L_DocType;
                    if (!Enum.TryParse<SAPbobsCOM.BoObjectTypes>(JE.L_DocType, true, out L_DocType)) throw new Exception("Invalid DocType");
                    this.SAP_TABLE = new SAP_TABLE(Convert.ToInt32(L_DocType));
                    SAPbobsCOM.JournalVouchers oJV = (SAPbobsCOM.JournalVouchers)oCompany.GetBusinessObject(L_DocType);
                    SAPbobsCOM.JournalEntries oJE = oJV.JournalEntries;

                    #region Set JournalEntries
                    SAPbobsCOM.BoYesNoEnum AutomaticWT;
                    SAPbobsCOM.BoYesNoEnum AutoVAT;
                    SAPbobsCOM.BoYesNoEnum BlockDunningLetter;
                    SAPbobsCOM.BoYesNoEnum Corisptivi;
                    SAPbobsCOM.BoYesNoEnum DeferredTax;
                    SAPbobsCOM.ECDPostingTypeEnum ECDPostingType;
                    SAPbobsCOM.BoYesNoEnum ExcludeFromTaxReportControlStatementVAT;
                    SAPbobsCOM.BoYesNoEnum IsCostCenterTransfer;
                    SAPbobsCOM.OperationCodeTypeEnum OperationCode;
                    SAPbobsCOM.BoYesNoEnum Report347;
                    SAPbobsCOM.BoYesNoEnum ReportEU;
                    SAPbobsCOM.ResidenceNumberTypeEnum ResidenceNumberType;
                    SAPbobsCOM.BoYesNoEnum StampTax;
                    SAPbobsCOM.BoYesNoEnum UseAutoStorno;
                    if (!string.IsNullOrEmpty(JE.AutomaticWT) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.AutomaticWT, true, out AutomaticWT)) oJE.AutomaticWT = AutomaticWT;
                    if (!string.IsNullOrEmpty(JE.AutoVAT) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.AutoVAT, true, out AutoVAT)) oJE.AutoVAT = AutoVAT;
                    if (!string.IsNullOrEmpty(JE.BlockDunningLetter) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.BlockDunningLetter, true, out BlockDunningLetter)) oJE.BlockDunningLetter = BlockDunningLetter;
                    if (!string.IsNullOrEmpty(JE.Corisptivi) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.Corisptivi, true, out Corisptivi)) oJE.Corisptivi = Corisptivi;
                    if (!string.IsNullOrEmpty(JE.DeferredTax) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.DeferredTax, true, out DeferredTax)) oJE.DeferredTax = DeferredTax;
                    if (!string.IsNullOrEmpty(JE.DocumentType)) oJE.DocumentType = JE.DocumentType;
                    if (JE.DueDate.HasValue) oJE.DueDate = (DateTime)JE.DueDate;
                    if (!string.IsNullOrEmpty(JE.ECDPostingType) && Enum.TryParse<SAPbobsCOM.ECDPostingTypeEnum>(JE.ECDPostingType, true, out ECDPostingType)) oJE.ECDPostingType = ECDPostingType;
                    if (!string.IsNullOrEmpty(JE.ExcludeFromTaxReportControlStatementVAT) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.ExcludeFromTaxReportControlStatementVAT, true, out ExcludeFromTaxReportControlStatementVAT)) oJE.ExcludeFromTaxReportControlStatementVAT = ExcludeFromTaxReportControlStatementVAT;
                    if (JE.ExposedTransNumber.HasValue) oJE.ExposedTransNumber = (int)JE.ExposedTransNumber;
                    if (!string.IsNullOrEmpty(JE.Indicator)) oJE.Indicator = JE.Indicator;
                    if (!string.IsNullOrEmpty(JE.IsCostCenterTransfer) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.IsCostCenterTransfer, true, out IsCostCenterTransfer)) oJE.IsCostCenterTransfer = IsCostCenterTransfer;
                    if (JE.LocationCode.HasValue) oJE.LocationCode = (int)JE.LocationCode;
                    if (!string.IsNullOrEmpty(JE.Memo)) oJE.Memo = JE.Memo;
                    if (!string.IsNullOrEmpty(JE.OperationCode) && Enum.TryParse<SAPbobsCOM.OperationCodeTypeEnum>(JE.OperationCode, true, out OperationCode)) oJE.OperationCode = OperationCode;
                    if (!string.IsNullOrEmpty(JE.ProjectCode)) oJE.ProjectCode = JE.ProjectCode;
                    if (!string.IsNullOrEmpty(JE.Reference)) oJE.Reference = JE.Reference;
                    if (!string.IsNullOrEmpty(JE.Reference2)) oJE.Reference2 = JE.Reference2;
                    if (!string.IsNullOrEmpty(JE.Reference3)) oJE.Reference3 = JE.Reference3;
                    if (JE.ReferenceDate.HasValue) oJE.ReferenceDate = (DateTime)JE.ReferenceDate;
                    if (!string.IsNullOrEmpty(JE.Report347) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.Report347, true, out Report347)) oJE.Report347 = Report347;
                    if (!string.IsNullOrEmpty(JE.ReportEU) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.ReportEU, true, out ReportEU)) oJE.ReportEU = ReportEU;
                    if (!string.IsNullOrEmpty(JE.ReportingSectionControlStatementVAT)) oJE.ReportingSectionControlStatementVAT = JE.ReportingSectionControlStatementVAT;
                    if (!string.IsNullOrEmpty(JE.ResidenceNumberType) && Enum.TryParse<SAPbobsCOM.ResidenceNumberTypeEnum>(JE.ResidenceNumberType, true, out ResidenceNumberType)) oJE.ResidenceNumberType = ResidenceNumberType;
                    if (JE.Series.HasValue) oJE.Series = (int)JE.Series;
                    if (!string.IsNullOrEmpty(JE.StampTax) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.StampTax, true, out StampTax)) oJE.StampTax = StampTax;
                    if (JE.StornoDate.HasValue) oJE.StornoDate = (DateTime)JE.StornoDate;
                    if (JE.TaxDate.HasValue) oJE.TaxDate = (DateTime)JE.TaxDate;
                    if (!string.IsNullOrEmpty(JE.TransactionCode)) oJE.TransactionCode = JE.TransactionCode;
                    if (!string.IsNullOrEmpty(JE.UseAutoStorno) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JE.UseAutoStorno, true, out UseAutoStorno)) oJE.UseAutoStorno = UseAutoStorno;
                    if (JE.VatDate.HasValue) oJE.VatDate = (DateTime)JE.VatDate;
                    #endregion

                    #region JournalEntries User Defined Fields
                    foreach (Fields JE_UField in JE_UField_List.FindAll(x => x.L_InterfaceLogDetailID == JE.L_InterfaceLogDetailID))
                    {
                        if (!string.IsNullOrEmpty(JE_UField.Value)) oJE.UserFields.Fields.Item(JE_UField.L_Name).Value = JE_UField.Value;
                        if (!string.IsNullOrEmpty(JE_UField.ValidValue)) oJE.UserFields.Fields.Item(JE_UField.L_Name).ValidValue = JE_UField.ValidValue;
                    }
                    #endregion

                    foreach (JournalEntries_Lines JELine in JELine_List.FindAll(x => x.L_InterfaceLogDetailID == JE.L_InterfaceLogDetailID && x.L_JournalEntriesID == JE.L_JournalEntriesID))
                    {
                        SAPbobsCOM.JournalEntries_Lines oLines = oJE.Lines;
                        if (JELine.L_LineNum != 0) oLines.Add();

                        #region Set JournalEntries_Lines
                        SAPbobsCOM.BoYesNoEnum PaymentBlock;
                        SAPbobsCOM.BoTaxPostAccEnum TaxPostAccount;
                        SAPbobsCOM.BoYesNoEnum VatLine;
                        SAPbobsCOM.BoYesNoEnum WTLiable;
                        SAPbobsCOM.BoYesNoEnum WTRow;
                        if (!string.IsNullOrEmpty(JELine.AccountCode)) oLines.AccountCode = JELine.AccountCode;
                        if (!string.IsNullOrEmpty(JELine.AdditionalReference)) oLines.AdditionalReference = JELine.AdditionalReference;
                        if (JELine.BaseSum.HasValue) oLines.BaseSum = (double)JELine.BaseSum;
                        if (JELine.BlockReason.HasValue) oLines.BlockReason = (int)JELine.BlockReason;
                        if (JELine.BPLID.HasValue) oLines.BPLID = (int)JELine.BPLID;
                        if (!string.IsNullOrEmpty(JELine.ContraAccount)) oLines.ContraAccount = JELine.ContraAccount;
                        if (!string.IsNullOrEmpty(JELine.ControlAccount)) oLines.ControlAccount = JELine.ControlAccount;
                        if (!string.IsNullOrEmpty(JELine.CostingCode)) oLines.CostingCode = JELine.CostingCode;
                        if (!string.IsNullOrEmpty(JELine.CostingCode2)) oLines.CostingCode2 = JELine.CostingCode2;
                        if (!string.IsNullOrEmpty(JELine.CostingCode3)) oLines.CostingCode3 = JELine.CostingCode3;
                        if (!string.IsNullOrEmpty(JELine.CostingCode4)) oLines.CostingCode4 = JELine.CostingCode4;
                        if (!string.IsNullOrEmpty(JELine.CostingCode5)) oLines.CostingCode5 = JELine.CostingCode5;
                        if (JELine.Credit.HasValue) oLines.Credit = (double)JELine.Credit;
                        if (JELine.CreditSys.HasValue) oLines.CreditSys = (double)JELine.CreditSys;
                        if (JELine.Debit.HasValue) oLines.Debit = (double)JELine.Debit;
                        if (JELine.DebitSys.HasValue) oLines.DebitSys = (double)JELine.DebitSys;
                        if (JELine.DueDate.HasValue) oLines.DueDate = (DateTime)JELine.DueDate;
                        if (JELine.ExposedTransNumber.HasValue) oLines.ExposedTransNumber = (int)JELine.ExposedTransNumber;
                        if (JELine.FCCredit.HasValue) oLines.FCCredit = (double)JELine.FCCredit;
                        if (!string.IsNullOrEmpty(JELine.FCCurrency)) oLines.FCCurrency = JELine.FCCurrency;
                        if (JELine.FCDebit.HasValue) oLines.FCDebit = (double)JELine.FCDebit;
                        if (!string.IsNullOrEmpty(JELine.FederalTaxID)) oLines.FederalTaxID = JELine.FederalTaxID;
                        if (JELine.GrossValue.HasValue) oLines.GrossValue = (double)JELine.GrossValue;
                        if (!string.IsNullOrEmpty(JELine.LineMemo)) oLines.LineMemo = JELine.LineMemo;
                        if (JELine.LocationCode.HasValue) oLines.LocationCode = (int)JELine.LocationCode;
                        if (!string.IsNullOrEmpty(JELine.PaymentBlock) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JELine.PaymentBlock, true, out PaymentBlock)) oLines.PaymentBlock = PaymentBlock;
                        if (!string.IsNullOrEmpty(JELine.ProjectCode)) oLines.ProjectCode = JELine.ProjectCode;
                        if (!string.IsNullOrEmpty(JELine.Reference1)) oLines.Reference1 = JELine.Reference1;
                        if (!string.IsNullOrEmpty(JELine.Reference2)) oLines.Reference2 = JELine.Reference2;
                        if (JELine.ReferenceDate1.HasValue) oLines.ReferenceDate1 = (DateTime)JELine.ReferenceDate1;
                        if (JELine.ReferenceDate2.HasValue) oLines.ReferenceDate2 = (DateTime)JELine.ReferenceDate2;
                        if (!string.IsNullOrEmpty(JELine.ShortName)) oLines.ShortName = JELine.ShortName;
                        if (JELine.SystemBaseAmount.HasValue) oLines.SystemBaseAmount = (double)JELine.SystemBaseAmount;
                        if (JELine.SystemVatAmount.HasValue) oLines.SystemVatAmount = (double)JELine.SystemVatAmount;
                        if (!string.IsNullOrEmpty(JELine.TaxCode)) oLines.TaxCode = JELine.TaxCode;
                        if (JELine.TaxDate.HasValue) oLines.TaxDate = (DateTime)JELine.TaxDate;
                        if (!string.IsNullOrEmpty(JELine.TaxGroup)) oLines.TaxGroup = JELine.TaxGroup;
                        if (!string.IsNullOrEmpty(JELine.TaxPostAccount) && Enum.TryParse<SAPbobsCOM.BoTaxPostAccEnum>(JELine.TaxPostAccount, true, out TaxPostAccount)) oLines.TaxPostAccount = TaxPostAccount;
                        if (JELine.VatAmount.HasValue) oLines.VatAmount = (double)JELine.VatAmount;
                        if (JELine.VatDate.HasValue) oLines.VatDate = (DateTime)JELine.VatDate;
                        if (!string.IsNullOrEmpty(JELine.VatLine) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JELine.VatLine, true, out VatLine)) oLines.VatLine = VatLine;
                        if (!string.IsNullOrEmpty(JELine.WTLiable) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JELine.WTLiable, true, out WTLiable)) oLines.WTLiable = WTLiable;
                        if (!string.IsNullOrEmpty(JELine.WTRow) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(JELine.WTRow, true, out WTRow)) oLines.WTRow = WTRow;
                        #endregion

                        #region JournalEntries_Lines User Defined Fields
                        foreach (Fields JELine_UField in JELine_UField_List.FindAll(x => x.L_InterfaceLogDetailID == JE.L_InterfaceLogDetailID))
                        {
                            if (!string.IsNullOrEmpty(JELine_UField.Value)) oJE.UserFields.Fields.Item(JELine_UField.L_Name).Value = JELine_UField.Value;
                            if (!string.IsNullOrEmpty(JELine_UField.ValidValue)) oJE.UserFields.Fields.Item(JELine_UField.L_Name).Value = JELine_UField.ValidValue;
                        }
                        #endregion
                    }

                    lRetCode = oJV.Add();

                    if (lRetCode != 0)
                    {
                        this.oCompany.GetLastError(out SAPErrorCode, out SAPErrorMessage);

                        LogDetail.SAPStatusCode = SAPErrorCode.ToString();
                        LogDetail.SAPErrorMessage = SAPErrorMessage;
                        Log.SAPStatusCode = LogDetail.SAPStatusCode;
                        Log.SAPErrorMessage = LogDetail.SAPErrorMessage;
                        throw new Exception($"SAP Error: Code {SAPErrorCode} {SAPErrorMessage}");
                    }

                    BatchNum = this.oCompany.GetNewObjectKey();
                    //SAPTABLE JournalVoucher = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oJournalVouchers));
                    //DocNum = GetDataColumn("BatchNum", JournalVoucher, "BatchNum", BatchNum);
                    DocNum = BatchNum.Split('\t')[0];
                    //GLDocNum = DocNum;
                    if (string.IsNullOrEmpty(Log.SAPRefID))
                    {
                        Log.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                        Log.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                        Log.SAPRefNo = DocNum;
                        Log.SAPRefGLNo = GLDocNum;
                    }
                    LogDetail.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                    LogDetail.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                    LogDetail.SAPRefNo = DocNum;
                    LogDetail.SAPRefGLNo = GLDocNum;
                    LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.OK;
                    LogDetail.SAPStatusCode = lRetCode.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SAPRefID = null;
                Log.SAPRefNo = null;
                Log.SAPRefGLNo = null;
                LogDetail.SAPRefID = null;
                LogDetail.SAPRefNo = null;
                LogDetail.SAPRefGLNo = null;
                if (string.IsNullOrEmpty(LogDetail.SAPStatusCode)) LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.InternalServerError;
                if (string.IsNullOrEmpty(LogDetail.SAPErrorMessage)) LogDetail.REMErrorMessage = ex.Message;

                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_JournalVouchers_Create : " + Msg);
            }
        }
        private void Process_Documents_Create()
        {
            int lRetCode = 0;
            int SAPErrorCode = 0;
            string SAPErrorMessage = "";
            string BatchNum = null;
            string TranID = null;
            string DocNum = null;
            string GLTranID = null;
            string GLDocNum = null;
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            InterfaceLogDetail LogDetail = this._B1Header.B1Entity.LogDetail;
            try
            {
                List<Documents> Doc_List = this._B1Header.B1Entity.Doc_List;
                List<Document_Lines> DocLine_List = this._B1Header.B1Entity.DocLine_List;
                List<Document_SpecialLines> DocSpLine_List = this._B1Header.B1Entity.DocSpLine_List;
                List<Fields> Doc_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.Documents));
                List<Fields> DocLine_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.Document_Lines));
                foreach (Documents Doc in Doc_List)
                {
                    SAPbobsCOM.BoObjectTypes L_DocType;
                    if (!Enum.TryParse<SAPbobsCOM.BoObjectTypes>(Doc.L_DocType, true, out L_DocType)) throw new Exception("Invalid DocType");
                    this.SAP_TABLE = new SAP_TABLE(Convert.ToInt32(L_DocType));
                    SAPbobsCOM.Documents oDoc = oCompany.GetBusinessObject(L_DocType);

                    #region Set Documents
                    SAPbobsCOM.BoYesNoEnum ApplyCurrentVATRatesForDownPaymentsToDraw;
                    SAPbobsCOM.BoYesNoEnum ApplyTaxOnFirstInstallment;
                    SAPbobsCOM.BoYesNoEnum ArchiveNonremovableSalesQuotation;
                    SAPbobsCOM.BoYesNoEnum BlockDunning;
                    SAPbobsCOM.ClosingOptionEnum ClosingOption;
                    SAPbobsCOM.CommissionTradeTypeEnum CommissionTrade;
                    SAPbobsCOM.BoYesNoEnum CommissionTradeReturn;
                    SAPbobsCOM.BoYesNoEnum Confirmed;
                    SAPbobsCOM.BoYesNoEnum CreateOnlineQuotation;
                    SAPbobsCOM.BoYesNoEnum DeferredTax;
                    SAPbobsCOM.BoObjectTypes DocObjectCode;
                    SAPbobsCOM.BoDocumentTypes DocType;
                    SAPbobsCOM.DocumentDeliveryTypeEnum DocumentDelivery;
                    SAPbobsCOM.BoDocumentSubType DocumentSubType;
                    SAPbobsCOM.BoSoStatus DownPaymentStatus;
                    SAPbobsCOM.DownPaymentTypeEnum DownPaymentType;
                    SAPbobsCOM.EDocGenerationTypeEnum EDocGenerationType;
                    SAPbobsCOM.EDocStatusEnum EDocStatus;
                    SAPbobsCOM.ElecCommStatusEnum ElecCommStatus;
                    SAPbobsCOM.BoYesNoEnum ExcludeFromTaxReportControlStatementVAT;
                    SAPbobsCOM.BoYesNoEnum GroupHandWritten;
                    SAPbobsCOM.GSTTransactionTypeEnum GSTTransactionType;
                    SAPbobsCOM.BoYesNoEnum HandWritten;
                    SAPbobsCOM.BoYesNoEnum InsuranceOperation347;
                    SAPbobsCOM.BoInterimDocTypes InterimType;
                    SAPbobsCOM.BoYesNoEnum IsAlteration;
                    SAPbobsCOM.BoYesNoEnum IsPayToBank;
                    SAPbobsCOM.FolioLetterEnum Letter;
                    SAPbobsCOM.BoYesNoEnum MaximumCashDiscount;
                    SAPbobsCOM.BoYesNoEnum NTSApproved;
                    SAPbobsCOM.BoYesNoEnum OpenForLandedCosts;
                    SAPbobsCOM.BoYesNoEnum PartialSupply;
                    SAPbobsCOM.BoYesNoEnum PaymentBlock;
                    SAPbobsCOM.BoYesNoEnum Pick;
                    SAPbobsCOM.PriceModeDocumentEnum PriceMode;
                    SAPbobsCOM.PrintStatusEnum Printed;
                    SAPbobsCOM.BoYesNoEnum PrintSEPADirect;
                    SAPbobsCOM.BoYesNoEnum RelevantToGTS;
                    SAPbobsCOM.BoYesNoEnum ReopenManuallyClosedOrCanceledDocument;
                    SAPbobsCOM.BoYesNoEnum ReopenOriginalDocument;
                    SAPbobsCOM.BoYesNoEnum ReserveInvoice;
                    SAPbobsCOM.BoYesNoEnum ReuseDocumentNum;
                    SAPbobsCOM.BoYesNoEnum ReuseNotaFiscalNum;
                    SAPbobsCOM.BoYesNoEnum Revision;
                    SAPbobsCOM.BoYesNoEnum RevisionPo;
                    SAPbobsCOM.BoYesNoEnum Rounding;
                    SAPbobsCOM.BoYesNoEnum SendNotification;
                    SAPbobsCOM.BoYesNoEnum ShowSCN;
                    SAPbobsCOM.BoPayTermDueTypes StartFrom;
                    SAPbobsCOM.BoDocSummaryTypes SummeryType;
                    SAPbobsCOM.BoYesNoEnum UseBillToAddrToDetermineTax;
                    SAPbobsCOM.BoYesNoEnum UseCorrectionVATGroup;
                    SAPbobsCOM.BoYesNoEnum UseShpdGoodsAct;
                    SAPbobsCOM.BoDocWhsUpdateTypes WareHouseUpdateType;

                    if (!string.IsNullOrEmpty(Doc.Address)) oDoc.Address = Doc.Address;
                    if (!string.IsNullOrEmpty(Doc.Address2)) oDoc.Address2 = Doc.Address2;
                    if (!string.IsNullOrEmpty(Doc.AgentCode)) oDoc.AgentCode = Doc.AgentCode;
                    if (Doc.AnnualInvoiceDeclarationReference.HasValue) oDoc.AnnualInvoiceDeclarationReference = (int)Doc.AnnualInvoiceDeclarationReference;
                    if (!string.IsNullOrEmpty(Doc.ApplyCurrentVATRatesForDownPaymentsToDraw) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.ApplyCurrentVATRatesForDownPaymentsToDraw, true, out ApplyCurrentVATRatesForDownPaymentsToDraw)) oDoc.ApplyCurrentVATRatesForDownPaymentsToDraw = ApplyCurrentVATRatesForDownPaymentsToDraw;
                    if (!string.IsNullOrEmpty(Doc.ApplyTaxOnFirstInstallment) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.ApplyTaxOnFirstInstallment, true, out ApplyTaxOnFirstInstallment)) oDoc.ApplyTaxOnFirstInstallment = ApplyTaxOnFirstInstallment;
                    if (!string.IsNullOrEmpty(Doc.ArchiveNonremovableSalesQuotation) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.ArchiveNonremovableSalesQuotation, true, out ArchiveNonremovableSalesQuotation)) oDoc.ArchiveNonremovableSalesQuotation = ArchiveNonremovableSalesQuotation;
                    if (Doc.AssetValueDate.HasValue) oDoc.AssetValueDate = (DateTime)Doc.AssetValueDate;
                    if (!string.IsNullOrEmpty(Doc.ATDocumentType)) oDoc.ATDocumentType = Doc.ATDocumentType;
                    if (Doc.AttachmentEntry.HasValue) oDoc.AttachmentEntry = (int)Doc.AttachmentEntry;
                    if (!string.IsNullOrEmpty(Doc.AuthorizationCode)) oDoc.AuthorizationCode = Doc.AuthorizationCode;
                    if (Doc.BlanketAgreementNumber.HasValue) oDoc.BlanketAgreementNumber = (int)Doc.BlanketAgreementNumber;
                    if (!string.IsNullOrEmpty(Doc.BlockDunning) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.BlockDunning, true, out BlockDunning)) oDoc.BlockDunning = BlockDunning;
                    if (!string.IsNullOrEmpty(Doc.Box1099)) oDoc.Box1099 = Doc.Box1099;
                    if (!string.IsNullOrEmpty(Doc.BPChannelCode)) oDoc.BPChannelCode = Doc.BPChannelCode;
                    if (Doc.BPChannelContact.HasValue) oDoc.BPChannelContact = (int)Doc.BPChannelContact;
                    if (Doc.BPL_IDAssignedToInvoice.HasValue) oDoc.BPL_IDAssignedToInvoice = (int)Doc.BPL_IDAssignedToInvoice;
                    if (Doc.CancelDate.HasValue) oDoc.CancelDate = (DateTime)Doc.CancelDate;
                    if (!string.IsNullOrEmpty(Doc.CardCode)) oDoc.CardCode = Doc.CardCode;
                    if (!string.IsNullOrEmpty(Doc.CardName)) oDoc.CardName = Doc.CardName;
                    if (Doc.CashDiscountDateOffset.HasValue) oDoc.CashDiscountDateOffset = (int)Doc.CashDiscountDateOffset;
                    if (!string.IsNullOrEmpty(Doc.CentralBankIndicator)) oDoc.CentralBankIndicator = Doc.CentralBankIndicator;
                    if (Doc.ClosingDate.HasValue) oDoc.ClosingDate = (DateTime)Doc.ClosingDate;
                    if (!string.IsNullOrEmpty(Doc.ClosingOption) && Enum.TryParse<SAPbobsCOM.ClosingOptionEnum>(Doc.ClosingOption, true, out ClosingOption)) oDoc.ClosingOption = ClosingOption;
                    if (!string.IsNullOrEmpty(Doc.ClosingRemarks)) oDoc.ClosingRemarks = Doc.ClosingRemarks;
                    if (!string.IsNullOrEmpty(Doc.Comments)) oDoc.Comments = Doc.Comments;
                    if (!string.IsNullOrEmpty(Doc.CommissionTrade) && Enum.TryParse<SAPbobsCOM.CommissionTradeTypeEnum>(Doc.CommissionTrade, true, out CommissionTrade)) oDoc.CommissionTrade = CommissionTrade;
                    if (!string.IsNullOrEmpty(Doc.CommissionTradeReturn) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.CommissionTradeReturn, true, out CommissionTradeReturn)) oDoc.CommissionTradeReturn = CommissionTradeReturn;
                    if (!string.IsNullOrEmpty(Doc.Confirmed) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.Confirmed, true, out Confirmed)) oDoc.Confirmed = Confirmed;
                    if (Doc.ContactPersonCode.HasValue) oDoc.ContactPersonCode = (int)Doc.ContactPersonCode;
                    if (!string.IsNullOrEmpty(Doc.ControlAccount)) oDoc.ControlAccount = Doc.ControlAccount;
                    if (!string.IsNullOrEmpty(Doc.CreateOnlineQuotation) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.CreateOnlineQuotation, true, out CreateOnlineQuotation)) oDoc.CreateOnlineQuotation = CreateOnlineQuotation;
                    if (Doc.DateOfReportingControlStatementVAT.HasValue) oDoc.DateOfReportingControlStatementVAT = (DateTime)Doc.DateOfReportingControlStatementVAT;
                    if (!string.IsNullOrEmpty(Doc.DeferredTax) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.DeferredTax, true, out DeferredTax)) oDoc.DeferredTax = DeferredTax;
                    if (Doc.DiscountPercent.HasValue) oDoc.DiscountPercent = (double)Doc.DiscountPercent;
                    if (!string.IsNullOrEmpty(Doc.DocCurrency)) oDoc.DocCurrency = Doc.DocCurrency;
                    if (Doc.DocDate.HasValue) oDoc.DocDate = (DateTime)Doc.DocDate;
                    if (Doc.DocDueDate.HasValue) oDoc.DocDueDate = (DateTime)Doc.DocDueDate;
                    if (Doc.DocNum.HasValue) oDoc.DocNum = (int)Doc.DocNum;
                    if (!string.IsNullOrEmpty(Doc.DocObjectCode) && Enum.TryParse<SAPbobsCOM.BoObjectTypes>(Doc.DocObjectCode, true, out DocObjectCode)) oDoc.DocObjectCode = DocObjectCode;
                    if (!string.IsNullOrEmpty(Doc.DocObjectCodeEx)) oDoc.DocObjectCodeEx = Doc.DocObjectCodeEx;
                    if (Doc.DocRate.HasValue) oDoc.DocRate = (double)Doc.DocRate;
                    if (Doc.DocTime.HasValue) oDoc.DocTime = (DateTime)Doc.DocTime;
                    if (Doc.DocTotal.HasValue) oDoc.DocTotal = (double)Doc.DocTotal;
                    if (Doc.DocTotalFc.HasValue) oDoc.DocTotalFc = (double)Doc.DocTotalFc;
                    if (!string.IsNullOrEmpty(Doc.DocType) && Enum.TryParse<SAPbobsCOM.BoDocumentTypes>(Doc.DocType, true, out DocType)) oDoc.DocType = DocType;
                    if (!string.IsNullOrEmpty(Doc.DocumentDelivery) && Enum.TryParse<SAPbobsCOM.DocumentDeliveryTypeEnum>(Doc.DocumentDelivery, true, out DocumentDelivery)) oDoc.DocumentDelivery = DocumentDelivery;
                    if (Doc.DocumentsOwner.HasValue) oDoc.DocumentsOwner = (int)Doc.DocumentsOwner;
                    if (!string.IsNullOrEmpty(Doc.DocumentSubType) && Enum.TryParse<SAPbobsCOM.BoDocumentSubType>(Doc.DocumentSubType, true, out DocumentSubType)) oDoc.DocumentSubType = DocumentSubType;
                    if (!string.IsNullOrEmpty(Doc.DocumentTaxID)) oDoc.DocumentTaxID = Doc.DocumentTaxID;
                    if (Doc.DownPayment.HasValue) oDoc.DownPayment = (double)Doc.DownPayment;
                    if (Doc.DownPaymentAmount.HasValue) oDoc.DownPaymentAmount = (double)Doc.DownPaymentAmount;
                    if (Doc.DownPaymentAmountFC.HasValue) oDoc.DownPaymentAmountFC = (double)Doc.DownPaymentAmountFC;
                    if (Doc.DownPaymentAmountSC.HasValue) oDoc.DownPaymentAmountSC = (double)Doc.DownPaymentAmountSC;
                    if (Doc.DownPaymentPercentage.HasValue) oDoc.DownPaymentPercentage = (double)Doc.DownPaymentPercentage;
                    if (!string.IsNullOrEmpty(Doc.DownPaymentStatus) && Enum.TryParse<SAPbobsCOM.BoSoStatus>(Doc.DownPaymentStatus, true, out DownPaymentStatus)) oDoc.DownPaymentStatus = DownPaymentStatus;
                    if (!string.IsNullOrEmpty(Doc.DownPaymentTrasactionID)) oDoc.DownPaymentTrasactionID = Doc.DownPaymentTrasactionID;
                    if (!string.IsNullOrEmpty(Doc.DownPaymentType) && Enum.TryParse<SAPbobsCOM.DownPaymentTypeEnum>(Doc.DownPaymentType, true, out DownPaymentType)) oDoc.DownPaymentType = DownPaymentType;
                    if (!string.IsNullOrEmpty(Doc.ECommerceGSTIN)) oDoc.ECommerceGSTIN = Doc.ECommerceGSTIN;
                    if (!string.IsNullOrEmpty(Doc.ECommerceOperator)) oDoc.ECommerceOperator = Doc.ECommerceOperator;
                    if (!string.IsNullOrEmpty(Doc.EDocErrorCode)) oDoc.EDocErrorCode = Doc.EDocErrorCode;
                    if (!string.IsNullOrEmpty(Doc.EDocErrorMessage)) oDoc.EDocErrorMessage = Doc.EDocErrorMessage;
                    if (Doc.EDocExportFormat.HasValue) oDoc.EDocExportFormat = (int)Doc.EDocExportFormat;
                    if (!string.IsNullOrEmpty(Doc.EDocGenerationType) && Enum.TryParse<SAPbobsCOM.EDocGenerationTypeEnum>(Doc.EDocGenerationType, true, out EDocGenerationType)) oDoc.EDocGenerationType = EDocGenerationType;
                    if (!string.IsNullOrEmpty(Doc.EDocNum)) oDoc.EDocNum = Doc.EDocNum;
                    if (Doc.EDocSeries.HasValue) oDoc.EDocSeries = (int)Doc.EDocSeries;
                    if (!string.IsNullOrEmpty(Doc.EDocStatus) && Enum.TryParse<SAPbobsCOM.EDocStatusEnum>(Doc.EDocStatus, true, out EDocStatus)) oDoc.EDocStatus = EDocStatus;
                    if (!string.IsNullOrEmpty(Doc.ElecCommStatus) && Enum.TryParse<SAPbobsCOM.ElecCommStatusEnum>(Doc.ElecCommStatus, true, out ElecCommStatus)) oDoc.ElecCommStatus = ElecCommStatus;
                    if (Doc.EndDeliveryDate.HasValue) oDoc.EndDeliveryDate = (DateTime)Doc.EndDeliveryDate;
                    if (Doc.EndDeliveryTime.HasValue) oDoc.EndDeliveryTime = (DateTime)Doc.EndDeliveryTime;
                    if (!string.IsNullOrEmpty(Doc.ETaxNumber)) oDoc.ETaxNumber = Doc.ETaxNumber;
                    if (Doc.ETaxWebSite.HasValue) oDoc.ETaxWebSite = (int)Doc.ETaxWebSite;
                    if (!string.IsNullOrEmpty(Doc.ExcludeFromTaxReportControlStatementVAT) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.ExcludeFromTaxReportControlStatementVAT, true, out ExcludeFromTaxReportControlStatementVAT)) oDoc.ExcludeFromTaxReportControlStatementVAT = ExcludeFromTaxReportControlStatementVAT;
                    if (Doc.ExemptionValidityDateFrom.HasValue) oDoc.ExemptionValidityDateFrom = (DateTime)Doc.ExemptionValidityDateFrom;
                    if (Doc.ExemptionValidityDateTo.HasValue) oDoc.ExemptionValidityDateTo = (DateTime)Doc.ExemptionValidityDateTo;
                    if (!string.IsNullOrEmpty(Doc.ExternalCorrectedDocNum)) oDoc.ExternalCorrectedDocNum = Doc.ExternalCorrectedDocNum;
                    if (Doc.ExtraDays.HasValue) oDoc.ExtraDays = (int)Doc.ExtraDays;
                    if (Doc.ExtraMonth.HasValue) oDoc.ExtraMonth = (int)Doc.ExtraMonth;
                    if (!string.IsNullOrEmpty(Doc.FederalTaxID)) oDoc.FederalTaxID = Doc.FederalTaxID;
                    if (!string.IsNullOrEmpty(Doc.FiscalDocNum)) oDoc.FiscalDocNum = Doc.FiscalDocNum;
                    if (Doc.FolioNumber.HasValue) oDoc.FolioNumber = (int)Doc.FolioNumber;
                    if (Doc.FolioNumberFrom.HasValue) oDoc.FolioNumberFrom = (int)Doc.FolioNumberFrom;
                    if (Doc.FolioNumberTo.HasValue) oDoc.FolioNumberTo = (int)Doc.FolioNumberTo;
                    if (!string.IsNullOrEmpty(Doc.FolioPrefixString)) oDoc.FolioPrefixString = Doc.FolioPrefixString;
                    if (Doc.Form1099.HasValue) oDoc.Form1099 = (int)Doc.Form1099;
                    if (!string.IsNullOrEmpty(Doc.GroupHandWritten) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.GroupHandWritten, true, out GroupHandWritten)) oDoc.GroupHandWritten = GroupHandWritten;
                    if (Doc.GroupNumber.HasValue) oDoc.GroupNumber = (int)Doc.GroupNumber;
                    if (Doc.GroupSeries.HasValue) oDoc.GroupSeries = (int)Doc.GroupSeries;
                    if (!string.IsNullOrEmpty(Doc.GSTTransactionType) && Enum.TryParse<SAPbobsCOM.GSTTransactionTypeEnum>(Doc.GSTTransactionType, true, out GSTTransactionType)) oDoc.GSTTransactionType = GSTTransactionType;
                    if (Doc.GTSChecker.HasValue) oDoc.GTSChecker = (int)Doc.GTSChecker;
                    if (Doc.GTSPayee.HasValue) oDoc.GTSPayee = (int)Doc.GTSPayee;
                    if (!string.IsNullOrEmpty(Doc.HandWritten) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.HandWritten, true, out HandWritten)) oDoc.HandWritten = HandWritten;
                    if (Doc.ImportFileNum.HasValue) oDoc.ImportFileNum = (int)Doc.ImportFileNum;
                    if (!string.IsNullOrEmpty(Doc.Indicator)) oDoc.Indicator = Doc.Indicator;
                    if (!string.IsNullOrEmpty(Doc.InsuranceOperation347) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.InsuranceOperation347, true, out InsuranceOperation347)) oDoc.InsuranceOperation347 = InsuranceOperation347;
                    if (!string.IsNullOrEmpty(Doc.InterimType) && Enum.TryParse<SAPbobsCOM.BoInterimDocTypes>(Doc.InterimType, true, out InterimType)) oDoc.InterimType = InterimType;
                    if (Doc.InternalCorrectedDocNum.HasValue) oDoc.InternalCorrectedDocNum = (int)Doc.InternalCorrectedDocNum;
                    if (!string.IsNullOrEmpty(Doc.IsAlteration) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.IsAlteration, true, out IsAlteration)) oDoc.IsAlteration = IsAlteration;
                    if (!string.IsNullOrEmpty(Doc.IsPayToBank) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.IsPayToBank, true, out IsPayToBank)) oDoc.IsPayToBank = IsPayToBank;
                    if (Doc.IssuingReason.HasValue) oDoc.IssuingReason = (int)Doc.IssuingReason;
                    if (!string.IsNullOrEmpty(Doc.JournalMemo)) oDoc.JournalMemo = Doc.JournalMemo;
                    if (Doc.LanguageCode.HasValue) oDoc.LanguageCode = (int)Doc.LanguageCode;
                    if (!string.IsNullOrEmpty(Doc.Letter) && Enum.TryParse<SAPbobsCOM.FolioLetterEnum>(Doc.Letter, true, out Letter)) oDoc.Letter = Letter;
                    if (!string.IsNullOrEmpty(Doc.ManualNumber)) oDoc.ManualNumber = Doc.ManualNumber;
                    if (!string.IsNullOrEmpty(Doc.MaximumCashDiscount) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.MaximumCashDiscount, true, out MaximumCashDiscount)) oDoc.MaximumCashDiscount = MaximumCashDiscount;
                    if (!string.IsNullOrEmpty(Doc.NTSApproved) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.NTSApproved, true, out NTSApproved)) oDoc.NTSApproved = NTSApproved;
                    if (!string.IsNullOrEmpty(Doc.NTSApprovedNumber)) oDoc.NTSApprovedNumber = Doc.NTSApprovedNumber;
                    if (!string.IsNullOrEmpty(Doc.NumAtCard)) oDoc.NumAtCard = Doc.NumAtCard;
                    if (Doc.NumberOfInstallments.HasValue) oDoc.NumberOfInstallments = (int)Doc.NumberOfInstallments;
                    if (!string.IsNullOrEmpty(Doc.OpenForLandedCosts) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.OpenForLandedCosts, true, out OpenForLandedCosts)) oDoc.OpenForLandedCosts = OpenForLandedCosts;
                    if (!string.IsNullOrEmpty(Doc.OpeningRemarks)) oDoc.OpeningRemarks = Doc.OpeningRemarks;
                    if (Doc.OriginalCreditOrDebitDate.HasValue) oDoc.OriginalCreditOrDebitDate = (DateTime)Doc.OriginalCreditOrDebitDate;
                    if (!string.IsNullOrEmpty(Doc.OriginalCreditOrDebitNo)) oDoc.OriginalCreditOrDebitNo = Doc.OriginalCreditOrDebitNo;
                    if (Doc.OriginalRefDate.HasValue) oDoc.OriginalRefDate = (DateTime)Doc.OriginalRefDate;
                    if (!string.IsNullOrEmpty(Doc.OriginalRefNo)) oDoc.OriginalRefNo = Doc.OriginalRefNo;
                    if (!string.IsNullOrEmpty(Doc.PartialSupply) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.PartialSupply, true, out PartialSupply)) oDoc.PartialSupply = PartialSupply;
                    if (!string.IsNullOrEmpty(Doc.PaymentBlock) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.PaymentBlock, true, out PaymentBlock)) oDoc.PaymentBlock = PaymentBlock;
                    if (Doc.PaymentBlockEntry.HasValue) oDoc.PaymentBlockEntry = (int)Doc.PaymentBlockEntry;
                    if (Doc.PaymentGroupCode.HasValue) oDoc.PaymentGroupCode = (int)Doc.PaymentGroupCode;
                    if (!string.IsNullOrEmpty(Doc.PaymentMethod)) oDoc.PaymentMethod = Doc.PaymentMethod;
                    if (!string.IsNullOrEmpty(Doc.PaymentReference)) oDoc.PaymentReference = Doc.PaymentReference;
                    if (!string.IsNullOrEmpty(Doc.PayToBankAccountNo)) oDoc.PayToBankAccountNo = Doc.PayToBankAccountNo;
                    if (!string.IsNullOrEmpty(Doc.PayToBankBranch)) oDoc.PayToBankBranch = Doc.PayToBankBranch;
                    if (!string.IsNullOrEmpty(Doc.PayToBankCode)) oDoc.PayToBankCode = Doc.PayToBankCode;
                    if (!string.IsNullOrEmpty(Doc.PayToBankCountry)) oDoc.PayToBankCountry = Doc.PayToBankCountry;
                    if (!string.IsNullOrEmpty(Doc.PayToCode)) oDoc.PayToCode = Doc.PayToCode;
                    if (!string.IsNullOrEmpty(Doc.Pick) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.Pick, true, out Pick)) oDoc.Pick = Pick;
                    if (!string.IsNullOrEmpty(Doc.PickRemark)) oDoc.PickRemark = Doc.PickRemark;
                    if (!string.IsNullOrEmpty(Doc.PointOfIssueCode)) oDoc.PointOfIssueCode = Doc.PointOfIssueCode;
                    if (Doc.POSCashierNumber.HasValue) oDoc.POSCashierNumber = (int)Doc.POSCashierNumber;
                    if (Doc.POSDailySummaryNo.HasValue) oDoc.POSDailySummaryNo = (int)Doc.POSDailySummaryNo;
                    if (!string.IsNullOrEmpty(Doc.POSEquipmentNumber)) oDoc.POSEquipmentNumber = Doc.POSEquipmentNumber;
                    if (!string.IsNullOrEmpty(Doc.POSManufacturerSerialNumber)) oDoc.POSManufacturerSerialNumber = Doc.POSManufacturerSerialNumber;
                    if (Doc.POSReceiptNo.HasValue) oDoc.POSReceiptNo = (int)Doc.POSReceiptNo;
                    if (Doc.POS_CashRegister.HasValue) oDoc.POS_CashRegister = (int)Doc.POS_CashRegister;
                    if (!string.IsNullOrEmpty(Doc.PriceMode) && Enum.TryParse<SAPbobsCOM.PriceModeDocumentEnum>(Doc.PriceMode, true, out PriceMode)) oDoc.PriceMode = PriceMode;
                    if (!string.IsNullOrEmpty(Doc.Printed) && Enum.TryParse<SAPbobsCOM.PrintStatusEnum>(Doc.Printed, true, out Printed)) oDoc.Printed = Printed;
                    if (!string.IsNullOrEmpty(Doc.PrintSEPADirect) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.PrintSEPADirect, true, out PrintSEPADirect)) oDoc.PrintSEPADirect = PrintSEPADirect;
                    if (!string.IsNullOrEmpty(Doc.Project)) oDoc.Project = Doc.Project;
                    if (Doc.Receiver.HasValue) oDoc.Receiver = (int)Doc.Receiver;
                    if (!string.IsNullOrEmpty(Doc.Reference1)) oDoc.Reference1 = Doc.Reference1;
                    if (!string.IsNullOrEmpty(Doc.Reference2)) oDoc.Reference2 = Doc.Reference2;
                    if (Doc.RelatedEntry.HasValue) oDoc.RelatedEntry = (int)Doc.RelatedEntry;
                    if (Doc.RelatedType.HasValue) oDoc.RelatedType = (int)Doc.RelatedType;
                    if (Doc.Releaser.HasValue) oDoc.Releaser = (int)Doc.Releaser;
                    if (!string.IsNullOrEmpty(Doc.RelevantToGTS) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.RelevantToGTS, true, out RelevantToGTS)) oDoc.RelevantToGTS = RelevantToGTS;
                    if (!string.IsNullOrEmpty(Doc.ReopenManuallyClosedOrCanceledDocument) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.ReopenManuallyClosedOrCanceledDocument, true, out ReopenManuallyClosedOrCanceledDocument)) oDoc.ReopenManuallyClosedOrCanceledDocument = ReopenManuallyClosedOrCanceledDocument;
                    if (!string.IsNullOrEmpty(Doc.ReopenOriginalDocument) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.ReopenOriginalDocument, true, out ReopenOriginalDocument)) oDoc.ReopenOriginalDocument = ReopenOriginalDocument;
                    if (!string.IsNullOrEmpty(Doc.ReportingSectionControlStatementVAT)) oDoc.ReportingSectionControlStatementVAT = Doc.ReportingSectionControlStatementVAT;
                    if (Doc.ReqType.HasValue) oDoc.ReqType = (int)Doc.ReqType;
                    if (!string.IsNullOrEmpty(Doc.Requester)) oDoc.Requester = Doc.Requester;
                    if (Doc.RequesterBranch.HasValue) oDoc.RequesterBranch = (int)Doc.RequesterBranch;
                    if (Doc.RequesterDepartment.HasValue) oDoc.RequesterDepartment = (int)Doc.RequesterDepartment;
                    if (!string.IsNullOrEmpty(Doc.RequesterEmail)) oDoc.RequesterEmail = Doc.RequesterEmail;
                    if (!string.IsNullOrEmpty(Doc.RequesterName)) oDoc.RequesterName = Doc.RequesterName;
                    if (Doc.RequriedDate.HasValue) oDoc.RequriedDate = (DateTime)Doc.RequriedDate;
                    if (!string.IsNullOrEmpty(Doc.ReserveInvoice) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.ReserveInvoice, true, out ReserveInvoice)) oDoc.ReserveInvoice = ReserveInvoice;
                    if (!string.IsNullOrEmpty(Doc.ReuseDocumentNum) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.ReuseDocumentNum, true, out ReuseDocumentNum)) oDoc.ReuseDocumentNum = ReuseDocumentNum;
                    if (!string.IsNullOrEmpty(Doc.ReuseNotaFiscalNum) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.ReuseNotaFiscalNum, true, out ReuseNotaFiscalNum)) oDoc.ReuseNotaFiscalNum = ReuseNotaFiscalNum;
                    if (!string.IsNullOrEmpty(Doc.Revision) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.Revision, true, out Revision)) oDoc.Revision = Revision;
                    if (!string.IsNullOrEmpty(Doc.RevisionPo) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.RevisionPo, true, out RevisionPo)) oDoc.RevisionPo = RevisionPo;
                    if (!string.IsNullOrEmpty(Doc.Rounding) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.Rounding, true, out Rounding)) oDoc.Rounding = Rounding;
                    if (Doc.RoundingDiffAmount.HasValue) oDoc.RoundingDiffAmount = (double)Doc.RoundingDiffAmount;
                    if (Doc.SalesPersonCode.HasValue) oDoc.SalesPersonCode = (int)Doc.SalesPersonCode;
                    if (!string.IsNullOrEmpty(Doc.SendNotification) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.SendNotification, true, out SendNotification)) oDoc.SendNotification = SendNotification;
                    if (Doc.SequenceCode.HasValue) oDoc.SequenceCode = (int)Doc.SequenceCode;
                    if (!string.IsNullOrEmpty(Doc.SequenceModel)) oDoc.SequenceModel = Doc.SequenceModel;
                    if (Doc.SequenceSerial.HasValue) oDoc.SequenceSerial = (int)Doc.SequenceSerial;
                    if (Doc.Series.HasValue) oDoc.Series = (int)Doc.Series;
                    if (!string.IsNullOrEmpty(Doc.SeriesString)) oDoc.SeriesString = Doc.SeriesString;
                    if (Doc.ServiceGrossProfitPercent.HasValue) oDoc.ServiceGrossProfitPercent = (double)Doc.ServiceGrossProfitPercent;
                    if (!string.IsNullOrEmpty(Doc.ShipFrom)) oDoc.ShipFrom = Doc.ShipFrom;
                    if (!string.IsNullOrEmpty(Doc.ShipToCode)) oDoc.ShipToCode = Doc.ShipToCode;
                    if (!string.IsNullOrEmpty(Doc.ShowSCN) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.ShowSCN, true, out ShowSCN)) oDoc.ShowSCN = ShowSCN;
                    if (Doc.SpecifiedClosingDate.HasValue) oDoc.SpecifiedClosingDate = (DateTime)Doc.SpecifiedClosingDate;
                    if (Doc.StartDeliveryDate.HasValue) oDoc.StartDeliveryDate = (DateTime)Doc.StartDeliveryDate;
                    if (Doc.StartDeliveryTime.HasValue) oDoc.StartDeliveryTime = (DateTime)Doc.StartDeliveryTime;
                    if (!string.IsNullOrEmpty(Doc.StartFrom) && Enum.TryParse<SAPbobsCOM.BoPayTermDueTypes>(Doc.StartFrom, true, out StartFrom)) oDoc.StartFrom = StartFrom;
                    if (!string.IsNullOrEmpty(Doc.SubSeriesString)) oDoc.SubSeriesString = Doc.SubSeriesString;
                    if (!string.IsNullOrEmpty(Doc.SummeryType) && Enum.TryParse<SAPbobsCOM.BoDocSummaryTypes>(Doc.SummeryType, true, out SummeryType)) oDoc.SummeryType = SummeryType;
                    if (!string.IsNullOrEmpty(Doc.Supplier)) oDoc.Supplier = Doc.Supplier;
                    if (Doc.TaxDate.HasValue) oDoc.TaxDate = (DateTime)Doc.TaxDate;
                    if (!string.IsNullOrEmpty(Doc.TaxExemptionLetterNum)) oDoc.TaxExemptionLetterNum = Doc.TaxExemptionLetterNum;
                    if (Doc.TaxInvoiceDate.HasValue) oDoc.TaxInvoiceDate = (DateTime)Doc.TaxInvoiceDate;
                    if (!string.IsNullOrEmpty(Doc.TaxInvoiceNo)) oDoc.TaxInvoiceNo = Doc.TaxInvoiceNo;
                    if (!string.IsNullOrEmpty(Doc.TrackingNumber)) oDoc.TrackingNumber = Doc.TrackingNumber;
                    if (Doc.TransportationCode.HasValue) oDoc.TransportationCode = (int)Doc.TransportationCode;
                    if (!string.IsNullOrEmpty(Doc.UseBillToAddrToDetermineTax) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.UseBillToAddrToDetermineTax, true, out UseBillToAddrToDetermineTax)) oDoc.UseBillToAddrToDetermineTax = UseBillToAddrToDetermineTax;
                    if (!string.IsNullOrEmpty(Doc.UseCorrectionVATGroup) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.UseCorrectionVATGroup, true, out UseCorrectionVATGroup)) oDoc.UseCorrectionVATGroup = UseCorrectionVATGroup;
                    if (!string.IsNullOrEmpty(Doc.UseShpdGoodsAct) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Doc.UseShpdGoodsAct, true, out UseShpdGoodsAct)) oDoc.UseShpdGoodsAct = UseShpdGoodsAct;
                    if (Doc.VatDate.HasValue) oDoc.VatDate = (DateTime)Doc.VatDate;
                    if (Doc.VatPercent.HasValue) oDoc.VatPercent = (double)Doc.VatPercent;
                    if (!string.IsNullOrEmpty(Doc.VehiclePlate)) oDoc.VehiclePlate = Doc.VehiclePlate;
                    if (!string.IsNullOrEmpty(Doc.WareHouseUpdateType) && Enum.TryParse<SAPbobsCOM.BoDocWhsUpdateTypes>(Doc.WareHouseUpdateType, true, out WareHouseUpdateType)) oDoc.WareHouseUpdateType = WareHouseUpdateType;
                    #endregion

                    #region Documents User Defined Fields
                    foreach (Fields Doc_UField in Doc_UField_List.FindAll(x => x.L_FromRefID == Doc.L_DocumentsID))
                    {
                        if (!string.IsNullOrEmpty(Doc_UField.Value)) oDoc.UserFields.Fields.Item(Doc_UField.L_Name).Value = Doc_UField.Value;
                        if (!string.IsNullOrEmpty(Doc_UField.ValidValue)) oDoc.UserFields.Fields.Item(Doc_UField.L_Name).ValidValue = Doc_UField.ValidValue;
                    }
                    #endregion

                    foreach (Document_Lines DocLine in DocLine_List.FindAll(x => x.L_InterfaceLogDetailID == Doc.L_InterfaceLogDetailID && x.L_DocumentsID == Doc.L_DocumentsID))
                    {
                        SAPbobsCOM.Document_Lines oDocLine = oDoc.Lines;
                        if (DocLine.L_LineNum != 0) oDocLine.Add();

                        #region Set Document_Lines
                        SAPbobsCOM.BoYesNoEnum BackOrder;
                        SAPbobsCOM.BoYesNoEnum ChangeInventoryQuantityIndependently;
                        SAPbobsCOM.BoYesNoEnum ConsiderQuantity;
                        SAPbobsCOM.BoYesNoEnum ConsumerSalesForecast;
                        SAPbobsCOM.BoCorInvItemStatus CorrectionInvoiceItem;
                        SAPbobsCOM.BoYesNoEnum Deferred_Tax;
                        SAPbobsCOM.BoYesNoEnum DistributeExpense;
                        SAPbobsCOM.BoYesNoEnum EnableReturnCost;
                        SAPbobsCOM.BoExpenseOperationTypeEnum ExpenseOperationType;
                        SAPbobsCOM.BoYesNoEnum FreeOfChargeBP;
                        SAPbobsCOM.BoStatus LineStatus;
                        SAPbobsCOM.BoDocLineType LineType;
                        SAPbobsCOM.BoYesNoEnum PartialRetirement;
                        SAPbobsCOM.BoYesNoEnum TaxLiable;
                        SAPbobsCOM.BoYesNoEnum TaxOnly;
                        SAPbobsCOM.BoTaxTypes TaxType;
                        SAPbobsCOM.BoYesNoEnum ThirdParty;
                        SAPbobsCOM.BoTransactionTypeEnum TransactionType;
                        SAPbobsCOM.BoYesNoEnum UseBaseUnits;
                        SAPbobsCOM.BoYesNoEnum WithoutInventoryMovement;
                        SAPbobsCOM.BoYesNoEnum WTLiable;

                        if (!string.IsNullOrEmpty(DocLine.AccountCode)) oDocLine.AccountCode = DocLine.AccountCode;
                        if (DocLine.ActualBaseEntry.HasValue) oDocLine.ActualBaseEntry = (int)DocLine.ActualBaseEntry;
                        if (DocLine.ActualBaseLine.HasValue) oDocLine.ActualBaseLine = (int)DocLine.ActualBaseLine;
                        if (DocLine.ActualDeliveryDate.HasValue) oDocLine.ActualDeliveryDate = (DateTime)DocLine.ActualDeliveryDate;
                        if (!string.IsNullOrEmpty(DocLine.Address)) oDocLine.Address = DocLine.Address;
                        if (DocLine.AgreementNo.HasValue) oDocLine.AgreementNo = (int)DocLine.AgreementNo;
                        if (DocLine.AgreementRowNumber.HasValue) oDocLine.AgreementRowNumber = (int)DocLine.AgreementRowNumber;
                        if (!string.IsNullOrEmpty(DocLine.BackOrder) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.BackOrder, true, out BackOrder)) oDocLine.BackOrder = BackOrder;
                        if (!string.IsNullOrEmpty(DocLine.BarCode)) oDocLine.BarCode = DocLine.BarCode;
                        if (DocLine.BaseEntry.HasValue) oDocLine.BaseEntry = (int)DocLine.BaseEntry;
                        if (DocLine.BaseLine.HasValue) oDocLine.BaseLine = (int)DocLine.BaseLine;
                        if (DocLine.BaseType.HasValue) oDocLine.BaseType = (int)DocLine.BaseType;
                        if (!string.IsNullOrEmpty(DocLine.CFOPCode)) oDocLine.CFOPCode = DocLine.CFOPCode;
                        if (!string.IsNullOrEmpty(DocLine.ChangeAssemlyBoMWarehouse)) oDocLine.ChangeAssemlyBoMWarehouse = DocLine.ChangeAssemlyBoMWarehouse;
                        if (!string.IsNullOrEmpty(DocLine.ChangeInventoryQuantityIndependently) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.ChangeInventoryQuantityIndependently, true, out ChangeInventoryQuantityIndependently)) oDocLine.ChangeInventoryQuantityIndependently = ChangeInventoryQuantityIndependently;
                        if (!string.IsNullOrEmpty(DocLine.COGSAccountCode)) oDocLine.COGSAccountCode = DocLine.COGSAccountCode;
                        if (!string.IsNullOrEmpty(DocLine.COGSCostingCode)) oDocLine.COGSCostingCode = DocLine.COGSCostingCode;
                        if (!string.IsNullOrEmpty(DocLine.COGSCostingCode2)) oDocLine.COGSCostingCode2 = DocLine.COGSCostingCode2;
                        if (!string.IsNullOrEmpty(DocLine.COGSCostingCode3)) oDocLine.COGSCostingCode3 = DocLine.COGSCostingCode3;
                        if (!string.IsNullOrEmpty(DocLine.COGSCostingCode4)) oDocLine.COGSCostingCode4 = DocLine.COGSCostingCode4;
                        if (!string.IsNullOrEmpty(DocLine.COGSCostingCode5)) oDocLine.COGSCostingCode5 = DocLine.COGSCostingCode5;
                        if (DocLine.CommisionPercent.HasValue) oDocLine.CommisionPercent = (double)DocLine.CommisionPercent;
                        if (!string.IsNullOrEmpty(DocLine.ConsiderQuantity) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.ConsiderQuantity, true, out ConsiderQuantity)) oDocLine.ConsiderQuantity = ConsiderQuantity;
                        if (!string.IsNullOrEmpty(DocLine.ConsumerSalesForecast) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.ConsumerSalesForecast, true, out ConsumerSalesForecast)) oDocLine.ConsumerSalesForecast = ConsumerSalesForecast;
                        if (!string.IsNullOrEmpty(DocLine.CorrectionInvoiceItem) && Enum.TryParse<SAPbobsCOM.BoCorInvItemStatus>(DocLine.CorrectionInvoiceItem, true, out CorrectionInvoiceItem)) oDocLine.CorrectionInvoiceItem = CorrectionInvoiceItem;
                        if (DocLine.CorrInvAmountToDiffAcct.HasValue) oDocLine.CorrInvAmountToDiffAcct = (double)DocLine.CorrInvAmountToDiffAcct;
                        if (DocLine.CorrInvAmountToStock.HasValue) oDocLine.CorrInvAmountToStock = (double)DocLine.CorrInvAmountToStock;
                        if (!string.IsNullOrEmpty(DocLine.CostingCode)) oDocLine.CostingCode = DocLine.CostingCode;
                        if (!string.IsNullOrEmpty(DocLine.CostingCode2)) oDocLine.CostingCode2 = DocLine.CostingCode2;
                        if (!string.IsNullOrEmpty(DocLine.CostingCode3)) oDocLine.CostingCode3 = DocLine.CostingCode3;
                        if (!string.IsNullOrEmpty(DocLine.CostingCode4)) oDocLine.CostingCode4 = DocLine.CostingCode4;
                        if (!string.IsNullOrEmpty(DocLine.CostingCode5)) oDocLine.CostingCode5 = DocLine.CostingCode5;
                        if (!string.IsNullOrEmpty(DocLine.CountryOrg)) oDocLine.CountryOrg = DocLine.CountryOrg;
                        if (!string.IsNullOrEmpty(DocLine.CreditOriginCode)) oDocLine.CreditOriginCode = DocLine.CreditOriginCode;
                        if (!string.IsNullOrEmpty(DocLine.CSTCode)) oDocLine.CSTCode = DocLine.CSTCode;
                        if (!string.IsNullOrEmpty(DocLine.CSTforCOFINS)) oDocLine.CSTforCOFINS = DocLine.CSTforCOFINS;
                        if (!string.IsNullOrEmpty(DocLine.CSTforIPI)) oDocLine.CSTforIPI = DocLine.CSTforIPI;
                        if (!string.IsNullOrEmpty(DocLine.CSTforPIS)) oDocLine.CSTforPIS = DocLine.CSTforPIS;
                        if (!string.IsNullOrEmpty(DocLine.Currency)) oDocLine.Currency = DocLine.Currency;
                        if (DocLine.DefectAndBreakup.HasValue) oDocLine.DefectAndBreakup = (double)DocLine.DefectAndBreakup;
                        if (!string.IsNullOrEmpty(DocLine.DeferredTax) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.DeferredTax, true, out Deferred_Tax)) oDocLine.DeferredTax = Deferred_Tax;
                        if (DocLine.DiscountPercent.HasValue) oDocLine.DiscountPercent = (double)DocLine.DiscountPercent;
                        if (!string.IsNullOrEmpty(DocLine.DistributeExpense) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.DistributeExpense, true, out DistributeExpense)) oDocLine.DistributeExpense = DistributeExpense;
                        if (!string.IsNullOrEmpty(DocLine.EnableReturnCost) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.EnableReturnCost, true, out EnableReturnCost)) oDocLine.EnableReturnCost = EnableReturnCost;
                        if (DocLine.ExciseAmount.HasValue) oDocLine.ExciseAmount = (double)DocLine.ExciseAmount;
                        if (!string.IsNullOrEmpty(DocLine.ExLineNo)) oDocLine.ExLineNo = DocLine.ExLineNo;
                        if (!string.IsNullOrEmpty(DocLine.ExpenseOperationType) && Enum.TryParse<SAPbobsCOM.BoExpenseOperationTypeEnum>(DocLine.ExpenseOperationType, true, out ExpenseOperationType)) oDocLine.ExpenseOperationType = ExpenseOperationType;
                        if (!string.IsNullOrEmpty(DocLine.ExpenseType)) oDocLine.ExpenseType = DocLine.ExpenseType;
                        if (DocLine.Factor1.HasValue) oDocLine.Factor1 = (double)DocLine.Factor1;
                        if (DocLine.Factor2.HasValue) oDocLine.Factor2 = (double)DocLine.Factor2;
                        if (DocLine.Factor3.HasValue) oDocLine.Factor3 = (double)DocLine.Factor3;
                        if (DocLine.Factor4.HasValue) oDocLine.Factor4 = (double)DocLine.Factor4;
                        if (!string.IsNullOrEmpty(DocLine.FederalTaxID)) oDocLine.FederalTaxID = DocLine.FederalTaxID;
                        if (!string.IsNullOrEmpty(DocLine.FreeOfChargeBP) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.FreeOfChargeBP, true, out FreeOfChargeBP)) oDocLine.FreeOfChargeBP = FreeOfChargeBP;
                        if (!string.IsNullOrEmpty(DocLine.FreeText)) oDocLine.FreeText = DocLine.FreeText;
                        if (DocLine.GrossBase.HasValue) oDocLine.GrossBase = (int)DocLine.GrossBase;
                        if (DocLine.GrossBuyPrice.HasValue) oDocLine.GrossBuyPrice = (double)DocLine.GrossBuyPrice;
                        if (DocLine.GrossPrice.HasValue) oDocLine.GrossPrice = (double)DocLine.GrossPrice;
                        if (DocLine.GrossProfitTotalBasePrice.HasValue) oDocLine.GrossProfitTotalBasePrice = (double)DocLine.GrossProfitTotalBasePrice;
                        if (DocLine.GrossTotal.HasValue) oDocLine.GrossTotal = (double)DocLine.GrossTotal;
                        if (DocLine.GrossTotalFC.HasValue) oDocLine.GrossTotalFC = (double)DocLine.GrossTotalFC;
                        if (DocLine.Height1.HasValue) oDocLine.Height1 = (double)DocLine.Height1;
                        if (DocLine.Height2.HasValue) oDocLine.Height2 = (double)DocLine.Height2;
                        if (DocLine.Height2Unit.HasValue) oDocLine.Height2Unit = (int)DocLine.Height2Unit;
                        if (DocLine.Hight1Unit.HasValue) oDocLine.Hight1Unit = (int)DocLine.Hight1Unit;
                        if (DocLine.HSNEntry.HasValue) oDocLine.HSNEntry = (int)DocLine.HSNEntry;
                        if (DocLine.Incoterms.HasValue) oDocLine.Incoterms = (int)DocLine.Incoterms;
                        if (DocLine.InventoryQuantity.HasValue) oDocLine.InventoryQuantity = (double)DocLine.InventoryQuantity;
                        if (!string.IsNullOrEmpty(DocLine.ItemCode)) oDocLine.ItemCode = DocLine.ItemCode;
                        if (!string.IsNullOrEmpty(DocLine.ItemDescription)) oDocLine.ItemDescription = DocLine.ItemDescription;
                        if (!string.IsNullOrEmpty(DocLine.ItemDetails)) oDocLine.ItemDetails = DocLine.ItemDetails;
                        if (DocLine.Lengh1.HasValue) oDocLine.Lengh1 = (double)DocLine.Lengh1;
                        if (DocLine.Lengh1Unit.HasValue) oDocLine.Lengh1Unit = (int)DocLine.Lengh1Unit;
                        if (DocLine.Lengh2.HasValue) oDocLine.Lengh2 = (double)DocLine.Lengh2;
                        if (DocLine.Lengh2Unit.HasValue) oDocLine.Lengh2Unit = (int)DocLine.Lengh2Unit;
                        if (!string.IsNullOrEmpty(DocLine.LineStatus) && Enum.TryParse<SAPbobsCOM.BoStatus>(DocLine.LineStatus, true, out LineStatus)) oDocLine.LineStatus = LineStatus;
                        if (DocLine.LineTotal.HasValue) oDocLine.LineTotal = (double)DocLine.LineTotal;
                        if (!string.IsNullOrEmpty(DocLine.LineType) && Enum.TryParse<SAPbobsCOM.BoDocLineType>(DocLine.LineType, true, out LineType)) oDocLine.LineType = LineType;
                        if (!string.IsNullOrEmpty(DocLine.LineVendor)) oDocLine.LineVendor = DocLine.LineVendor;
                        if (DocLine.LocationCode.HasValue) oDocLine.LocationCode = (int)DocLine.LocationCode;
                        if (!string.IsNullOrEmpty(DocLine.MeasureUnit)) oDocLine.MeasureUnit = DocLine.MeasureUnit;
                        if (DocLine.NCMCode.HasValue) oDocLine.NCMCode = (int)DocLine.NCMCode;
                        if (DocLine.NetTaxAmount.HasValue) oDocLine.NetTaxAmount = (double)DocLine.NetTaxAmount;
                        if (DocLine.NetTaxAmountFC.HasValue) oDocLine.NetTaxAmountFC = (double)DocLine.NetTaxAmountFC;
                        if (DocLine.PackageQuantity.HasValue) oDocLine.PackageQuantity = (double)DocLine.PackageQuantity;
                        if (DocLine.ParentLineNum.HasValue) oDocLine.ParentLineNum = (int)DocLine.ParentLineNum;
                        if (!string.IsNullOrEmpty(DocLine.PartialRetirement) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.PartialRetirement, true, out PartialRetirement)) oDocLine.PartialRetirement = PartialRetirement;
                        if (DocLine.Price.HasValue) oDocLine.Price = (double)DocLine.Price;
                        if (DocLine.PriceAfterVAT.HasValue) oDocLine.PriceAfterVAT = (double)DocLine.PriceAfterVAT;
                        if (!string.IsNullOrEmpty(DocLine.ProjectCode)) oDocLine.ProjectCode = DocLine.ProjectCode;
                        if (DocLine.Quantity.HasValue) oDocLine.Quantity = (double)DocLine.Quantity;
                        if (DocLine.Rate.HasValue) oDocLine.Rate = (double)DocLine.Rate;
                        if (!string.IsNullOrEmpty(DocLine.ReceiptNumber)) oDocLine.ReceiptNumber = DocLine.ReceiptNumber;
                        if (DocLine.RequiredDate.HasValue) oDocLine.RequiredDate = (DateTime)DocLine.RequiredDate;
                        if (DocLine.RequiredQuantity.HasValue) oDocLine.RequiredQuantity = (double)DocLine.RequiredQuantity;
                        if (DocLine.RetirementAPC.HasValue) oDocLine.RetirementAPC = (double)DocLine.RetirementAPC;
                        if (DocLine.RetirementQuantity.HasValue) oDocLine.RetirementQuantity = (double)DocLine.RetirementQuantity;
                        if (DocLine.ReturnAction.HasValue) oDocLine.ReturnAction = (int)DocLine.ReturnAction;
                        if (DocLine.ReturnCost.HasValue) oDocLine.ReturnCost = (double)DocLine.ReturnCost;
                        if (DocLine.ReturnReason.HasValue) oDocLine.ReturnReason = (int)DocLine.ReturnReason;
                        if (DocLine.RowTotalFC.HasValue) oDocLine.RowTotalFC = (double)DocLine.RowTotalFC;
                        if (DocLine.SACEntry.HasValue) oDocLine.SACEntry = (int)DocLine.SACEntry;
                        if (DocLine.SalesPersonCode.HasValue) oDocLine.SalesPersonCode = (int)DocLine.SalesPersonCode;
                        if (!string.IsNullOrEmpty(DocLine.SerialNum)) oDocLine.SerialNum = DocLine.SerialNum;
                        if (DocLine.ShipDate.HasValue) oDocLine.ShipDate = (DateTime)DocLine.ShipDate;
                        if (!string.IsNullOrEmpty(DocLine.ShipFromCode)) oDocLine.ShipFromCode = DocLine.ShipFromCode;
                        if (!string.IsNullOrEmpty(DocLine.ShipFromDescription)) oDocLine.ShipFromDescription = DocLine.ShipFromDescription;
                        if (DocLine.ShippingMethod.HasValue) oDocLine.ShippingMethod = (int)DocLine.ShippingMethod;
                        if (!string.IsNullOrEmpty(DocLine.ShipToCode)) oDocLine.ShipToCode = DocLine.ShipToCode;
                        if (!string.IsNullOrEmpty(DocLine.ShipToDescription)) oDocLine.ShipToDescription = DocLine.ShipToDescription;
                        if (DocLine.Shortages.HasValue) oDocLine.Shortages = (double)DocLine.Shortages;
                        if (!string.IsNullOrEmpty(DocLine.SupplierCatNum)) oDocLine.SupplierCatNum = DocLine.SupplierCatNum;
                        if (DocLine.Surpluses.HasValue) oDocLine.Surpluses = (double)DocLine.Surpluses;
                        if (!string.IsNullOrEmpty(DocLine.SWW)) oDocLine.SWW = DocLine.SWW;
                        if (!string.IsNullOrEmpty(DocLine.TaxCode)) oDocLine.TaxCode = DocLine.TaxCode;
                        if (!string.IsNullOrEmpty(DocLine.TaxLiable) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.TaxLiable, true, out TaxLiable)) oDocLine.TaxLiable = TaxLiable;
                        if (!string.IsNullOrEmpty(DocLine.TaxOnly) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.TaxOnly, true, out TaxOnly)) oDocLine.TaxOnly = TaxOnly;
                        if (DocLine.TaxPercentagePerRow.HasValue) oDocLine.TaxPercentagePerRow = (double)DocLine.TaxPercentagePerRow;
                        if (DocLine.TaxTotal.HasValue) oDocLine.TaxTotal = (double)DocLine.TaxTotal;
                        if (!string.IsNullOrEmpty(DocLine.TaxType) && Enum.TryParse<SAPbobsCOM.BoTaxTypes>(DocLine.TaxType, true, out TaxType)) oDocLine.TaxType = TaxType;
                        if (!string.IsNullOrEmpty(DocLine.ThirdParty) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.ThirdParty, true, out ThirdParty)) oDocLine.ThirdParty = ThirdParty;
                        if (!string.IsNullOrEmpty(DocLine.TransactionType) && Enum.TryParse<SAPbobsCOM.BoTransactionTypeEnum>(DocLine.TransactionType, true, out TransactionType)) oDocLine.TransactionType = TransactionType;
                        if (DocLine.TransportMode.HasValue) oDocLine.TransportMode = (int)DocLine.TransportMode;
                        if (DocLine.UnitPrice.HasValue) oDocLine.UnitPrice = (double)DocLine.UnitPrice;
                        if (DocLine.UnitsOfMeasurment.HasValue) oDocLine.UnitsOfMeasurment = (double)DocLine.UnitsOfMeasurment;
                        if (DocLine.UoMEntry.HasValue) oDocLine.UoMEntry = (int)DocLine.UoMEntry;
                        if (!string.IsNullOrEmpty(DocLine.Usage)) oDocLine.Usage = DocLine.Usage;
                        if (!string.IsNullOrEmpty(DocLine.UseBaseUnits) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.UseBaseUnits, true, out UseBaseUnits)) oDocLine.UseBaseUnits = UseBaseUnits;
                        if (!string.IsNullOrEmpty(DocLine.VatGroup)) oDocLine.VatGroup = DocLine.VatGroup;
                        if (!string.IsNullOrEmpty(DocLine.VendorNum)) oDocLine.VendorNum = DocLine.VendorNum;
                        if (DocLine.Volume.HasValue) oDocLine.Volume = (double)DocLine.Volume;
                        if (DocLine.VolumeUnit.HasValue) oDocLine.VolumeUnit = (int)DocLine.VolumeUnit;
                        if (!string.IsNullOrEmpty(DocLine.WarehouseCode)) oDocLine.WarehouseCode = DocLine.WarehouseCode;
                        if (DocLine.Weight1.HasValue) oDocLine.Weight1 = (double)DocLine.Weight1;
                        if (DocLine.Weight1Unit.HasValue) oDocLine.Weight1Unit = (int)DocLine.Weight1Unit;
                        if (DocLine.Weight2.HasValue) oDocLine.Weight2 = (double)DocLine.Weight2;
                        if (DocLine.Weight2Unit.HasValue) oDocLine.Weight2Unit = (int)DocLine.Weight2Unit;
                        if (DocLine.Width1.HasValue) oDocLine.Width1 = (double)DocLine.Width1;
                        if (DocLine.Width1Unit.HasValue) oDocLine.Width1Unit = (int)DocLine.Width1Unit;
                        if (DocLine.Width2.HasValue) oDocLine.Width2 = (double)DocLine.Width2;
                        if (DocLine.Width2Unit.HasValue) oDocLine.Width2Unit = (int)DocLine.Width2Unit;
                        if (!string.IsNullOrEmpty(DocLine.WithoutInventoryMovement) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.WithoutInventoryMovement, true, out WithoutInventoryMovement)) oDocLine.WithoutInventoryMovement = WithoutInventoryMovement;
                        if (!string.IsNullOrEmpty(DocLine.WTLiable) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(DocLine.WTLiable, true, out WTLiable)) oDocLine.WTLiable = WTLiable;
                        #endregion

                        #region Document_Lines User Defined Fields
                        foreach (Fields DocLine_UField in DocLine_UField_List.FindAll(x => x.L_FromRefID == DocLine.L_Document_LinesID))
                        {
                            if (!string.IsNullOrEmpty(DocLine_UField.Value)) oDocLine.UserFields.Fields.Item(DocLine_UField.L_Name).Value = DocLine_UField.Value;
                            if (!string.IsNullOrEmpty(DocLine_UField.ValidValue)) oDocLine.UserFields.Fields.Item(DocLine_UField.L_Name).Value = DocLine_UField.ValidValue;
                        }
                        #endregion
                    }

                    foreach (Document_SpecialLines DocSpLine in DocSpLine_List.FindAll(x => x.L_InterfaceLogDetailID == Doc.L_InterfaceLogDetailID && x.L_DocumentsID == Doc.L_DocumentsID))
                    {
                        SAPbobsCOM.Document_SpecialLines oDocSpLine = oDoc.SpecialLines;
                        if (DocSpLine.L_LineNum != 0) oDocSpLine.Add();

                        #region Set Document_SpecialLines
                        SAPbobsCOM.BoDocSpecialLineType LineType;

                        if (DocSpLine.AfterLineNumber.HasValue) oDocSpLine.AfterLineNumber = (int)DocSpLine.AfterLineNumber;
                        if (!string.IsNullOrEmpty(DocSpLine.LineText)) oDocSpLine.LineText = DocSpLine.LineText;
                        if (!string.IsNullOrEmpty(DocSpLine.LineType) && Enum.TryParse<SAPbobsCOM.BoDocSpecialLineType>(DocSpLine.LineType, true, out LineType)) oDocSpLine.LineType = LineType;
                        #endregion
                    }

                    lRetCode = oDoc.Add();

                    if (lRetCode != 0)
                    {
                        this.oCompany.GetLastError(out SAPErrorCode, out SAPErrorMessage);

                        LogDetail.SAPStatusCode = SAPErrorCode.ToString();
                        LogDetail.SAPErrorMessage = SAPErrorMessage;
                        Log.SAPStatusCode = LogDetail.SAPStatusCode;
                        Log.SAPErrorMessage = LogDetail.SAPErrorMessage;
                        throw new Exception($"SAP Error: Code {SAPErrorCode} {SAPErrorMessage}");
                    }

                    this.oCompany.GetNewObjectCode(out TranID);
                    DocNum = this.GetDocNum(TranID);
                    GLTranID = this.GetDocTransId(TranID);
                    GLDocNum = this.GetGLDocNum(GLTranID);
                    if (string.IsNullOrEmpty(Log.SAPRefID))
                    {
                        Log.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                        Log.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                        Log.SAPRefNo = DocNum;
                        Log.SAPRefGLNo = GLDocNum;
                    }
                    LogDetail.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                    LogDetail.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                    LogDetail.SAPRefNo = DocNum;
                    LogDetail.SAPRefGLNo = GLDocNum;
                    LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.OK;
                    LogDetail.SAPStatusCode = lRetCode.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SAPRefID = null;
                Log.SAPRefNo = null;
                Log.SAPRefGLNo = null;
                LogDetail.SAPRefID = null;
                LogDetail.SAPRefNo = null;
                LogDetail.SAPRefGLNo = null;
                if (string.IsNullOrEmpty(LogDetail.SAPStatusCode)) LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.InternalServerError;
                if (string.IsNullOrEmpty(LogDetail.SAPErrorMessage)) LogDetail.REMErrorMessage = ex.Message;
                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_Documents_Create : " + Msg);
            }
        }
        private void Process_Documents_Cancel()
        {
            int lRetCode = 0;
            int SAPErrorCode = 0;
            string SAPErrorMessage = "";
            string BatchNum = null;
            string TranID = null;
            string DocNum = null;
            string GLTranID = null;
            string GLDocNum = null;
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            InterfaceLogDetail LogDetail = this._B1Header.B1Entity.LogDetail;
            try
            {
                List<Documents> Doc_List = this._B1Header.B1Entity.Doc_List;
                foreach (Documents Doc in Doc_List)
                {
                    SAPbobsCOM.BoObjectTypes L_DocType;
                    if (!Enum.TryParse<SAPbobsCOM.BoObjectTypes>(Doc.L_DocType, true, out L_DocType)) throw new Exception("Invalid DocType");
                    this.SAP_TABLE = new SAP_TABLE(Convert.ToInt32(L_DocType));
                    SAPbobsCOM.Documents oDoc = oCompany.GetBusinessObject(L_DocType);
                    oDoc.GetByKey(Convert.ToInt32(Doc.L_ABSEntry));
                    oDoc.Cancel();

                    SAPbobsCOM.Documents cancelDoc = oDoc.CreateCancellationDocument();
                    if (Doc.DocDate.HasValue) oDoc.DocDate = (DateTime)Doc.DocDate;
                    lRetCode = cancelDoc.Add();

                    if (lRetCode != 0)
                    {
                        this.oCompany.GetLastError(out SAPErrorCode, out SAPErrorMessage);

                        LogDetail.SAPStatusCode = SAPErrorCode.ToString();
                        LogDetail.SAPErrorMessage = SAPErrorMessage;
                        Log.SAPStatusCode = LogDetail.SAPStatusCode;
                        Log.SAPErrorMessage = LogDetail.SAPErrorMessage;
                        throw new Exception($"SAP Error: Code {SAPErrorCode} {SAPErrorMessage}");
                    }

                    this.oCompany.GetNewObjectCode(out TranID);
                    DocNum = this.GetDocNum(TranID);
                    GLTranID = this.GetDocTransId(TranID);
                    GLDocNum = this.GetGLDocNum(GLTranID);
                    if (string.IsNullOrEmpty(Log.SAPRefID))
                    {
                        Log.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                        Log.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                        Log.SAPRefNo = DocNum;
                        Log.SAPRefGLNo = GLDocNum;
                    }
                    LogDetail.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                    LogDetail.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                    LogDetail.SAPRefNo = DocNum;
                    LogDetail.SAPRefGLNo = GLDocNum;
                    LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.OK;
                    LogDetail.SAPStatusCode = lRetCode.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SAPRefID = null;
                Log.SAPRefNo = null;
                Log.SAPRefGLNo = null;
                LogDetail.SAPRefID = null;
                LogDetail.SAPRefNo = null;
                LogDetail.SAPRefGLNo = null;
                if (string.IsNullOrEmpty(LogDetail.SAPStatusCode)) LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.InternalServerError;
                if (string.IsNullOrEmpty(LogDetail.SAPErrorMessage)) LogDetail.REMErrorMessage = ex.Message;

                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_Documents_Cancel : " + Msg);
            }
        }
        private void Process_Documents_Remove()
        {
            int lRetCode = 0;
            int SAPErrorCode = 0;
            string SAPErrorMessage = "";
            string BatchNum = null;
            string TranID = null;
            string DocNum = null;
            string GLTranID = null;
            string GLDocNum = null;
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            InterfaceLogDetail LogDetail = this._B1Header.B1Entity.LogDetail;
            try
            {
                List<Documents> Doc_List = this._B1Header.B1Entity.Doc_List;
                foreach (Documents Doc in Doc_List)
                {
                    SAPbobsCOM.BoObjectTypes L_DocType;
                    if (!Enum.TryParse<SAPbobsCOM.BoObjectTypes>(Doc.L_DocType, true, out L_DocType)) throw new Exception("Invalid DocType");
                    this.SAP_TABLE = new SAP_TABLE(Convert.ToInt32(L_DocType));
                    SAPbobsCOM.Documents oDoc = oCompany.GetBusinessObject(L_DocType);
                    oDoc.GetByKey(Convert.ToInt32(Doc.L_ABSEntry));

                    lRetCode = oDoc.Remove();

                    if (lRetCode != 0)
                    {
                        this.oCompany.GetLastError(out SAPErrorCode, out SAPErrorMessage);

                        LogDetail.SAPStatusCode = SAPErrorCode.ToString();
                        LogDetail.SAPErrorMessage = SAPErrorMessage;
                        Log.SAPStatusCode = LogDetail.SAPStatusCode;
                        Log.SAPErrorMessage = LogDetail.SAPErrorMessage;
                        throw new Exception($"SAP Error: Code {SAPErrorCode} {SAPErrorMessage}");
                    }

                    this.oCompany.GetNewObjectCode(out TranID);
                    DocNum = this.GetDocNum(TranID);
                    GLTranID = this.GetDocTransId(TranID);
                    GLDocNum = this.GetGLDocNum(GLTranID);
                    if (string.IsNullOrEmpty(Log.SAPRefID))
                    {
                        Log.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                        Log.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                        Log.SAPRefNo = DocNum;
                        Log.SAPRefGLNo = GLDocNum;
                    }
                    LogDetail.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                    LogDetail.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                    LogDetail.SAPRefNo = DocNum;
                    LogDetail.SAPRefGLNo = GLDocNum;
                    LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.OK;
                    LogDetail.SAPStatusCode = lRetCode.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SAPRefID = null;
                Log.SAPRefNo = null;
                Log.SAPRefGLNo = null;
                LogDetail.SAPRefID = null;
                LogDetail.SAPRefNo = null;
                LogDetail.SAPRefGLNo = null;
                if (string.IsNullOrEmpty(LogDetail.SAPStatusCode)) LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.InternalServerError;
                if (string.IsNullOrEmpty(LogDetail.SAPErrorMessage)) LogDetail.REMErrorMessage = ex.Message;

                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_Documents_Remove : " + Msg);
            }
        }
        private void Process_Payments_Create()
        {
            int lRetCode = 0;
            int SAPErrorCode = 0;
            string SAPErrorMessage = "";
            string BatchNum = null;
            string TranID = null;
            string DocNum = null;
            string GLTranID = null;
            string GLDocNum = null;
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            InterfaceLogDetail LogDetail = this._B1Header.B1Entity.LogDetail;
            try
            {
                List<Payments> Pay_List = this._B1Header.B1Entity.Pay_List;
                List<Payments_Accounts> PayACC_List = this._B1Header.B1Entity.PayACC_List;
                List<Payments_Invoices> PayINV_List = this._B1Header.B1Entity.PayINV_List;
                List<Payments_CreditCards> PayCR_List = this._B1Header.B1Entity.PayCR_List;
                List<Payments_Checks> PayCQ_List = this._B1Header.B1Entity.PayCQ_List;
                List<Fields> Pay_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.Payments));
                List<Fields> PayACC_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.Payments_Accounts));
                List<Fields> PayINV_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.Payments_Invoices));
                List<Fields> PayCR_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.Payments_CreditCards));
                List<Fields> PayCQ_UField_List = this._B1Header.B1Entity.UField_List.FindAll(x => x.L_FromTable == nameof(SAPbobsCOM.Payments_Checks));
                foreach (Payments Pay in Pay_List)
                {
                    SAPbobsCOM.BoObjectTypes L_DocType;
                    if (!Enum.TryParse<SAPbobsCOM.BoObjectTypes>(Pay.L_DocType, true, out L_DocType)) throw new Exception("Invalid DocType");
                    this.SAP_TABLE = new SAP_TABLE(Convert.ToInt32(L_DocType));
                    SAPbobsCOM.Payments oPay = oCompany.GetBusinessObject(L_DocType);

                    #region Set Payments
                    SAPbobsCOM.BoYesNoEnum ApplyVAT;
                    SAPbobsCOM.BoBoeStatus BillofExchangeStatus;
                    SAPbobsCOM.BoPaymentsObjectType DocObjectCode;
                    SAPbobsCOM.BoRcptTypes DocType;
                    SAPbobsCOM.BoRcptTypes DocTypte;
                    SAPbobsCOM.BoYesNoEnum HandWritten;
                    SAPbobsCOM.BoYesNoEnum IsPayToBank;
                    SAPbobsCOM.BoYesNoEnum LocalCurrency;
                    SAPbobsCOM.BoYesNoEnum PaymentByWTCertif;
                    SAPbobsCOM.BoPaymentPriorities PaymentPriority;
                    SAPbobsCOM.BoORCTPaymentTypeEnum PaymentType;
                    SAPbobsCOM.BoYesNoEnum Proforma;

                    if (!string.IsNullOrEmpty(Pay.Address)) oPay.Address = Pay.Address;
                    if (!string.IsNullOrEmpty(Pay.ApplyVAT) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Pay.ApplyVAT, true, out ApplyVAT)) oPay.ApplyVAT = ApplyVAT;
                    if (!string.IsNullOrEmpty(Pay.BankAccount)) oPay.BankAccount = Pay.BankAccount;
                    if (Pay.BankChargeAmount.HasValue) oPay.BankChargeAmount = (double)Pay.BankChargeAmount;
                    if (!string.IsNullOrEmpty(Pay.BankCode)) oPay.BankCode = Pay.BankCode;
                    if (!string.IsNullOrEmpty(Pay.BillOfExchangeAgent)) oPay.BillOfExchangeAgent = Pay.BillOfExchangeAgent;
                    if (Pay.BillOfExchangeAmount.HasValue) oPay.BillOfExchangeAmount = (double)Pay.BillOfExchangeAmount;
                    if (!string.IsNullOrEmpty(Pay.BillofExchangeStatus) && Enum.TryParse<SAPbobsCOM.BoBoeStatus>(Pay.BillofExchangeStatus, true, out BillofExchangeStatus)) oPay.BillofExchangeStatus = BillofExchangeStatus;
                    if (Pay.BlanketAgreement.HasValue) oPay.BlanketAgreement = (int)Pay.BlanketAgreement;
                    if (!string.IsNullOrEmpty(Pay.BoeAccount)) oPay.BoeAccount = Pay.BoeAccount;
                    if (Pay.BPLID.HasValue) oPay.BPLID = (int)Pay.BPLID;
                    if (!string.IsNullOrEmpty(Pay.CardCode)) oPay.CardCode = Pay.CardCode;
                    if (!string.IsNullOrEmpty(Pay.CardName)) oPay.CardName = Pay.CardName;
                    if (!string.IsNullOrEmpty(Pay.CashAccount)) oPay.CashAccount = Pay.CashAccount;
                    if (Pay.CashSum.HasValue) oPay.CashSum = (double)Pay.CashSum;
                    if (!string.IsNullOrEmpty(Pay.CheckAccount)) oPay.CheckAccount = Pay.CheckAccount;
                    if (Pay.ContactPersonCode.HasValue) oPay.ContactPersonCode = (int)Pay.ContactPersonCode;
                    if (!string.IsNullOrEmpty(Pay.ControlAccount)) oPay.ControlAccount = Pay.ControlAccount;
                    if (!string.IsNullOrEmpty(Pay.CounterReference)) oPay.CounterReference = Pay.CounterReference;
                    if (Pay.DeductionPercent.HasValue) oPay.DeductionPercent = (double)Pay.DeductionPercent;
                    if (Pay.DeductionSum.HasValue) oPay.DeductionSum = (double)Pay.DeductionSum;
                    if (!string.IsNullOrEmpty(Pay.DocCurrency)) oPay.DocCurrency = Pay.DocCurrency;
                    if (Pay.DocDate.HasValue) oPay.DocDate = (DateTime)Pay.DocDate;
                    if (Pay.DocNum.HasValue) oPay.DocNum = (int)Pay.DocNum;
                    if (!string.IsNullOrEmpty(Pay.DocObjectCode) && Enum.TryParse<SAPbobsCOM.BoPaymentsObjectType>(Pay.DocObjectCode, true, out DocObjectCode)) oPay.DocObjectCode = DocObjectCode;
                    if (Pay.DocRate.HasValue) oPay.DocRate = (double)Pay.DocRate;
                    if (!string.IsNullOrEmpty(Pay.DocType) && Enum.TryParse<SAPbobsCOM.BoRcptTypes>(Pay.DocType, true, out DocType)) oPay.DocType = DocType;
                    if (!string.IsNullOrEmpty(Pay.DocTypte) && Enum.TryParse<SAPbobsCOM.BoRcptTypes>(Pay.DocTypte, true, out DocTypte)) oPay.DocTypte = DocTypte;
                    if (Pay.DueDate.HasValue) oPay.DueDate = (DateTime)Pay.DueDate;
                    if (!string.IsNullOrEmpty(Pay.HandWritten) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Pay.HandWritten, true, out HandWritten)) oPay.HandWritten = HandWritten;
                    if (!string.IsNullOrEmpty(Pay.IsPayToBank) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Pay.IsPayToBank, true, out IsPayToBank)) oPay.IsPayToBank = IsPayToBank;
                    if (!string.IsNullOrEmpty(Pay.JournalRemarks)) oPay.JournalRemarks = Pay.JournalRemarks;
                    if (!string.IsNullOrEmpty(Pay.LocalCurrency) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Pay.LocalCurrency, true, out LocalCurrency)) oPay.LocalCurrency = LocalCurrency;
                    if (Pay.LocationCode.HasValue) oPay.LocationCode = (int)Pay.LocationCode;
                    if (!string.IsNullOrEmpty(Pay.PaymentByWTCertif) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Pay.PaymentByWTCertif, true, out PaymentByWTCertif)) oPay.PaymentByWTCertif = PaymentByWTCertif;
                    if (!string.IsNullOrEmpty(Pay.PaymentPriority) && Enum.TryParse<SAPbobsCOM.BoPaymentPriorities>(Pay.PaymentPriority, true, out PaymentPriority)) oPay.PaymentPriority = PaymentPriority;
                    if (!string.IsNullOrEmpty(Pay.PaymentType) && Enum.TryParse<SAPbobsCOM.BoORCTPaymentTypeEnum>(Pay.PaymentType, true, out PaymentType)) oPay.PaymentType = PaymentType;
                    if (!string.IsNullOrEmpty(Pay.PayToBankAccountNo)) oPay.PayToBankAccountNo = Pay.PayToBankAccountNo;
                    if (!string.IsNullOrEmpty(Pay.PayToBankBranch)) oPay.PayToBankBranch = Pay.PayToBankBranch;
                    if (!string.IsNullOrEmpty(Pay.PayToBankCode)) oPay.PayToBankCode = Pay.PayToBankCode;
                    if (!string.IsNullOrEmpty(Pay.PayToBankCountry)) oPay.PayToBankCountry = Pay.PayToBankCountry;
                    if (!string.IsNullOrEmpty(Pay.PayToCode)) oPay.PayToCode = Pay.PayToCode;
                    if (!string.IsNullOrEmpty(Pay.Proforma) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(Pay.Proforma, true, out Proforma)) oPay.Proforma = Proforma;
                    if (!string.IsNullOrEmpty(Pay.ProjectCode)) oPay.ProjectCode = Pay.ProjectCode;
                    if (!string.IsNullOrEmpty(Pay.Reference1)) oPay.Reference1 = Pay.Reference1;
                    if (!string.IsNullOrEmpty(Pay.Reference2)) oPay.Reference2 = Pay.Reference2;
                    if (!string.IsNullOrEmpty(Pay.Remarks)) oPay.Remarks = Pay.Remarks;
                    if (Pay.Series.HasValue) oPay.Series = (int)Pay.Series;
                    if (Pay.TaxDate.HasValue) oPay.TaxDate = (DateTime)Pay.TaxDate;
                    if (!string.IsNullOrEmpty(Pay.TaxGroup)) oPay.TaxGroup = Pay.TaxGroup;
                    if (!string.IsNullOrEmpty(Pay.TransactionCode)) oPay.TransactionCode = Pay.TransactionCode;
                    if (!string.IsNullOrEmpty(Pay.TransferAccount)) oPay.TransferAccount = Pay.TransferAccount;
                    if (Pay.TransferDate.HasValue) oPay.TransferDate = (DateTime)Pay.TransferDate;
                    if (Pay.TransferRealAmount.HasValue) oPay.TransferRealAmount = (double)Pay.TransferRealAmount;
                    if (!string.IsNullOrEmpty(Pay.TransferReference)) oPay.TransferReference = Pay.TransferReference;
                    if (Pay.TransferSum.HasValue) oPay.TransferSum = (double)Pay.TransferSum;
                    if (Pay.VatDate.HasValue) oPay.VatDate = (DateTime)Pay.VatDate;
                    if (Pay.WTAmount.HasValue) oPay.WTAmount = (double)Pay.WTAmount;
                    if (Pay.WtBaseSum.HasValue) oPay.WtBaseSum = (double)Pay.WtBaseSum;
                    if (!string.IsNullOrEmpty(Pay.WTCode)) oPay.WTCode = Pay.WTCode;
                    #endregion

                    #region Documents User Defined Fields
                    foreach (Fields Pay_UField in Pay_UField_List.FindAll(x => x.L_FromRefID == Pay.L_PaymentsID))
                    {
                        if (!string.IsNullOrEmpty(Pay_UField.Value)) oPay.UserFields.Fields.Item(Pay_UField.L_Name).Value = Pay_UField.Value;
                        if (!string.IsNullOrEmpty(Pay_UField.ValidValue)) oPay.UserFields.Fields.Item(Pay_UField.L_Name).ValidValue = Pay_UField.ValidValue;
                    }
                    #endregion

                    foreach (Payments_Accounts PayACC in PayACC_List.FindAll(x => x.L_InterfaceLogDetailID == Pay.L_InterfaceLogDetailID && x.L_PaymentsID == Pay.L_PaymentsID))
                    {
                        SAPbobsCOM.Payments_Accounts oPayACC = oPay.AccountPayments;
                        if (PayACC.L_LineNum != 0) oPayACC.Add();

                        #region Set Payments_Accounts
                        if (!string.IsNullOrEmpty(PayACC.AccountCode)) oPayACC.AccountCode = PayACC.AccountCode;
                        if (!string.IsNullOrEmpty(PayACC.AccountName)) oPayACC.AccountName = PayACC.AccountName;
                        if (!string.IsNullOrEmpty(PayACC.Decription)) oPayACC.Decription = PayACC.Decription;
                        if (PayACC.GrossAmount.HasValue) oPayACC.GrossAmount = (double)PayACC.GrossAmount;
                        if (!string.IsNullOrEmpty(PayACC.ProfitCenter)) oPayACC.ProfitCenter = PayACC.ProfitCenter;
                        if (!string.IsNullOrEmpty(PayACC.ProfitCenter2)) oPayACC.ProfitCenter2 = PayACC.ProfitCenter2;
                        if (!string.IsNullOrEmpty(PayACC.ProfitCenter3)) oPayACC.ProfitCenter3 = PayACC.ProfitCenter3;
                        if (!string.IsNullOrEmpty(PayACC.ProfitCenter4)) oPayACC.ProfitCenter4 = PayACC.ProfitCenter4;
                        if (!string.IsNullOrEmpty(PayACC.ProfitCenter5)) oPayACC.ProfitCenter5 = PayACC.ProfitCenter5;
                        if (!string.IsNullOrEmpty(PayACC.ProjectCode)) oPayACC.ProjectCode = PayACC.ProjectCode;
                        if (PayACC.SumPaid.HasValue) oPayACC.SumPaid = (double)PayACC.SumPaid;
                        if (PayACC.VatAmount.HasValue) oPayACC.VatAmount = (double)PayACC.VatAmount;
                        if (!string.IsNullOrEmpty(PayACC.VatGroup)) oPayACC.VatGroup = PayACC.VatGroup;
                        #endregion

                        #region Payments_Accounts User Defined Fields
                        foreach (Fields PayACC_UField in PayACC_UField_List.FindAll(x => x.L_FromRefID == PayACC.L_Payments_AccountsID))
                        {
                            if (!string.IsNullOrEmpty(PayACC_UField.Value)) oPayACC.UserFields.Fields.Item(PayACC_UField.L_Name).Value = PayACC_UField.Value;
                            if (!string.IsNullOrEmpty(PayACC_UField.ValidValue)) oPayACC.UserFields.Fields.Item(PayACC_UField.L_Name).Value = PayACC_UField.ValidValue;
                        }
                        #endregion
                    }

                    foreach (Payments_Invoices PayINV in PayINV_List.FindAll(x => x.L_InterfaceLogDetailID == Pay.L_InterfaceLogDetailID && x.L_PaymentsID == Pay.L_PaymentsID))
                    {
                        SAPbobsCOM.Payments_Invoices oPayINV = oPay.Invoices;
                        if (PayINV.L_LineNum != 0) oPayINV.Add();

                        #region Set Payments_Invoices
                        SAPbobsCOM.BoRcptInvTypes InvoiceType;

                        if (PayINV.AppliedFC.HasValue) oPayINV.AppliedFC = (double)PayINV.AppliedFC;
                        if (PayINV.DiscountPercent.HasValue) oPayINV.DiscountPercent = (double)PayINV.DiscountPercent;
                        if (!string.IsNullOrEmpty(PayINV.DistributionRule)) oPayINV.DistributionRule = PayINV.DistributionRule;
                        if (!string.IsNullOrEmpty(PayINV.DistributionRule2)) oPayINV.DistributionRule2 = PayINV.DistributionRule2;
                        if (!string.IsNullOrEmpty(PayINV.DistributionRule3)) oPayINV.DistributionRule3 = PayINV.DistributionRule3;
                        if (!string.IsNullOrEmpty(PayINV.DistributionRule4)) oPayINV.DistributionRule4 = PayINV.DistributionRule4;
                        if (!string.IsNullOrEmpty(PayINV.DistributionRule5)) oPayINV.DistributionRule5 = PayINV.DistributionRule5;
                        if (PayINV.DocEntry.HasValue) oPayINV.DocEntry = (int)PayINV.DocEntry;
                        if (PayINV.DocLine.HasValue) oPayINV.DocLine = (int)PayINV.DocLine;
                        if (PayINV.InstallmentId.HasValue) oPayINV.InstallmentId = (int)PayINV.InstallmentId;
                        if (!string.IsNullOrEmpty(PayINV.InvoiceType) && Enum.TryParse<SAPbobsCOM.BoRcptInvTypes>(PayINV.InvoiceType, true, out InvoiceType)) oPayINV.InvoiceType = InvoiceType;
                        if (PayINV.SumApplied.HasValue) oPayINV.SumApplied = (double)PayINV.SumApplied;
                        if (PayINV.TotalDiscount.HasValue) oPayINV.TotalDiscount = (double)PayINV.TotalDiscount;
                        if (PayINV.TotalDiscountFC.HasValue) oPayINV.TotalDiscountFC = (double)PayINV.TotalDiscountFC;
                        #endregion

                        #region Payments_Accounts User Defined Fields
                        foreach (Fields PayINV_UField in PayINV_UField_List.FindAll(x => x.L_FromRefID == PayINV.L_Payments_InvoicesID))
                        {
                            if (!string.IsNullOrEmpty(PayINV_UField.Value)) oPayINV.UserFields.Fields.Item(PayINV_UField.L_Name).Value = PayINV_UField.Value;
                            if (!string.IsNullOrEmpty(PayINV_UField.ValidValue)) oPayINV.UserFields.Fields.Item(PayINV_UField.L_Name).Value = PayINV_UField.ValidValue;
                        }
                        #endregion
                    }

                    foreach (Payments_CreditCards PayCR in PayCR_List.FindAll(x => x.L_InterfaceLogDetailID == Pay.L_InterfaceLogDetailID && x.L_PaymentsID == Pay.L_PaymentsID))
                    {
                        SAPbobsCOM.Payments_CreditCards oPayCR = oPay.CreditCards;
                        if (PayCR.L_LineNum != 0) oPayCR.Add();

                        #region Set Payments_CreditCards
                        SAPbobsCOM.BoRcptCredTypes CreditType;
                        SAPbobsCOM.BoYesNoEnum SplitPayments;

                        if (PayCR.AdditionalPaymentSum.HasValue) oPayCR.AdditionalPaymentSum = (double)PayCR.AdditionalPaymentSum;
                        if (PayCR.CardValidUntil.HasValue) oPayCR.CardValidUntil = (DateTime)PayCR.CardValidUntil;
                        if (!string.IsNullOrEmpty(PayCR.ConfirmationNum)) oPayCR.ConfirmationNum = PayCR.ConfirmationNum;
                        if (!string.IsNullOrEmpty(PayCR.CreditAcct)) oPayCR.CreditAcct = PayCR.CreditAcct;
                        if (PayCR.CreditCard.HasValue) oPayCR.CreditCard = (int)PayCR.CreditCard;
                        if (!string.IsNullOrEmpty(PayCR.CreditCardNumber)) oPayCR.CreditCardNumber = PayCR.CreditCardNumber;
                        if (PayCR.CreditSum.HasValue) oPayCR.CreditSum = (double)PayCR.CreditSum;
                        if (!string.IsNullOrEmpty(PayCR.CreditType) && Enum.TryParse<SAPbobsCOM.BoRcptCredTypes>(PayCR.CreditType, true, out CreditType)) oPayCR.CreditType = CreditType;
                        if (PayCR.FirstPaymentDue.HasValue) oPayCR.FirstPaymentDue = (DateTime)PayCR.FirstPaymentDue;
                        if (PayCR.FirstPaymentSum.HasValue) oPayCR.FirstPaymentSum = (double)PayCR.FirstPaymentSum;
                        if (PayCR.NumOfCreditPayments.HasValue) oPayCR.NumOfCreditPayments = (int)PayCR.NumOfCreditPayments;
                        if (PayCR.NumOfPayments.HasValue) oPayCR.NumOfPayments = (int)PayCR.NumOfPayments;
                        if (!string.IsNullOrEmpty(PayCR.OwnerIdNum)) oPayCR.OwnerIdNum = PayCR.OwnerIdNum;
                        if (!string.IsNullOrEmpty(PayCR.OwnerPhone)) oPayCR.OwnerPhone = PayCR.OwnerPhone;
                        if (PayCR.PaymentMethodCode.HasValue) oPayCR.PaymentMethodCode = (int)PayCR.PaymentMethodCode;
                        if (!string.IsNullOrEmpty(PayCR.SplitPayments) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(PayCR.SplitPayments, true, out SplitPayments)) oPayCR.SplitPayments = SplitPayments;
                        if (!string.IsNullOrEmpty(PayCR.VoucherNum)) oPayCR.VoucherNum = PayCR.VoucherNum;
                        #endregion

                        #region Payments_CreditCards User Defined Fields
                        foreach (Fields PayCR_UField in PayCR_UField_List.FindAll(x => x.L_FromRefID == PayCR.L_Payments_CreditCardsID))
                        {
                            if (!string.IsNullOrEmpty(PayCR_UField.Value)) oPayCR.UserFields.Fields.Item(PayCR_UField.L_Name).Value = PayCR_UField.Value;
                            if (!string.IsNullOrEmpty(PayCR_UField.ValidValue)) oPayCR.UserFields.Fields.Item(PayCR_UField.L_Name).Value = PayCR_UField.ValidValue;
                        }
                        #endregion
                    }

                    foreach (Payments_Checks PayCQ in PayCQ_List.FindAll(x => x.L_InterfaceLogDetailID == Pay.L_InterfaceLogDetailID && x.L_PaymentsID == Pay.L_PaymentsID))
                    {
                        SAPbobsCOM.Payments_Checks oPayCQ = oPay.Checks;
                        if (PayCQ.L_LineNum != 0) oPayCQ.Add();

                        #region Set Payments_Checks
                        SAPbobsCOM.BoYesNoEnum Endorse;
                        SAPbobsCOM.BoYesNoEnum ManualCheck;
                        SAPbobsCOM.BoYesNoEnum Trnsfrable;

                        if (!string.IsNullOrEmpty(PayCQ.AccounttNum)) oPayCQ.AccounttNum = PayCQ.AccounttNum;
                        if (!string.IsNullOrEmpty(PayCQ.BankCode)) oPayCQ.BankCode = PayCQ.BankCode;
                        if (!string.IsNullOrEmpty(PayCQ.Branch)) oPayCQ.Branch = PayCQ.Branch;
                        if (!string.IsNullOrEmpty(PayCQ.CheckAccount)) oPayCQ.CheckAccount = PayCQ.CheckAccount;
                        if (PayCQ.CheckNumber.HasValue) oPayCQ.CheckNumber = (int)PayCQ.CheckNumber;
                        if (PayCQ.CheckSum.HasValue) oPayCQ.CheckSum = (double)PayCQ.CheckSum;
                        if (!string.IsNullOrEmpty(PayCQ.CountryCode)) oPayCQ.CountryCode = PayCQ.CountryCode;
                        if (!string.IsNullOrEmpty(PayCQ.Details)) oPayCQ.Details = PayCQ.Details;
                        if (PayCQ.DueDate.HasValue) oPayCQ.DueDate = (DateTime)PayCQ.DueDate;
                        if (PayCQ.EndorsableCheckNo.HasValue) oPayCQ.EndorsableCheckNo = (int)PayCQ.EndorsableCheckNo;
                        if (!string.IsNullOrEmpty(PayCQ.Endorse) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(PayCQ.Endorse, true, out Endorse)) oPayCQ.Endorse = Endorse;
                        if (!string.IsNullOrEmpty(PayCQ.FiscalID)) oPayCQ.FiscalID = PayCQ.FiscalID;
                        if (!string.IsNullOrEmpty(PayCQ.ManualCheck) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(PayCQ.ManualCheck, true, out ManualCheck)) oPayCQ.ManualCheck = ManualCheck;
                        if (!string.IsNullOrEmpty(PayCQ.OriginallyIssuedBy)) oPayCQ.OriginallyIssuedBy = PayCQ.OriginallyIssuedBy;
                        if (!string.IsNullOrEmpty(PayCQ.Trnsfrable) && Enum.TryParse<SAPbobsCOM.BoYesNoEnum>(PayCQ.Trnsfrable, true, out Trnsfrable)) oPayCQ.Trnsfrable = Trnsfrable;
                        #endregion

                        #region Payments_Checks User Defined Fields
                        foreach (Fields PayCQ_UField in PayCQ_UField_List.FindAll(x => x.L_FromRefID == PayCQ.L_Payments_ChecksID))
                        {
                            if (!string.IsNullOrEmpty(PayCQ_UField.Value)) oPayCQ.UserFields.Fields.Item(PayCQ_UField.L_Name).Value = PayCQ_UField.Value;
                            if (!string.IsNullOrEmpty(PayCQ_UField.ValidValue)) oPayCQ.UserFields.Fields.Item(PayCQ_UField.L_Name).Value = PayCQ_UField.ValidValue;
                        }
                        #endregion
                    }

                    lRetCode = oPay.Add();

                    if (lRetCode != 0)
                    {
                        this.oCompany.GetLastError(out SAPErrorCode, out SAPErrorMessage);

                        LogDetail.SAPStatusCode = SAPErrorCode.ToString();
                        LogDetail.SAPErrorMessage = SAPErrorMessage;
                        Log.SAPStatusCode = LogDetail.SAPStatusCode;
                        Log.SAPErrorMessage = LogDetail.SAPErrorMessage;
                        throw new Exception($"SAP Error: Code {SAPErrorCode} {SAPErrorMessage}");
                    }

                    this.oCompany.GetNewObjectCode(out TranID);
                    DocNum = this.GetDocNum(TranID);
                    GLTranID = this.GetDocTransId(TranID);
                    GLDocNum = this.GetGLDocNum(GLTranID);
                    if (string.IsNullOrEmpty(Log.SAPRefID))
                    {
                        Log.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                        Log.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                        Log.SAPRefNo = DocNum;
                        Log.SAPRefGLNo = GLDocNum;
                    }
                    LogDetail.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                    LogDetail.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                    LogDetail.SAPRefNo = DocNum;
                    LogDetail.SAPRefGLNo = GLDocNum;
                    LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.OK;
                    LogDetail.SAPStatusCode = lRetCode.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SAPRefID = null;
                Log.SAPRefNo = null;
                Log.SAPRefGLNo = null;
                LogDetail.SAPRefID = null;
                LogDetail.SAPRefNo = null;
                LogDetail.SAPRefGLNo = null;
                if (string.IsNullOrEmpty(LogDetail.SAPStatusCode)) LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.InternalServerError;
                if (string.IsNullOrEmpty(LogDetail.SAPErrorMessage)) LogDetail.REMErrorMessage = ex.Message;
                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_Payments_Create : " + Msg);
            }
        }
        private void Process_Payments_Cancel()
        {
            int lRetCode = 0;
            int SAPErrorCode = 0;
            string SAPErrorMessage = "";
            string BatchNum = null;
            string TranID = null;
            string DocNum = null;
            string GLTranID = null;
            string GLDocNum = null;
            InterfaceLog_ext Log = this._B1Header.B1Entity.Log;
            InterfaceLogDetail LogDetail = this._B1Header.B1Entity.LogDetail;
            try
            {
                List<Payments> Pay_List = this._B1Header.B1Entity.Pay_List;
                foreach (Payments Pay in Pay_List)
                {
                    SAPbobsCOM.BoObjectTypes L_DocType;
                    if (!Enum.TryParse<SAPbobsCOM.BoObjectTypes>(Pay.L_DocType, true, out L_DocType)) throw new Exception("Invalid DocType");
                    this.SAP_TABLE = new SAP_TABLE(Convert.ToInt32(L_DocType));
                    SAPbobsCOM.Payments oPay = oCompany.GetBusinessObject(L_DocType);
                    oPay.GetByKey(Convert.ToInt32(Pay.L_RctEntry));

                    lRetCode = oPay.Cancel();

                    if (lRetCode != 0)
                    {
                        this.oCompany.GetLastError(out SAPErrorCode, out SAPErrorMessage);

                        LogDetail.SAPStatusCode = SAPErrorCode.ToString();
                        LogDetail.SAPErrorMessage = SAPErrorMessage;
                        Log.SAPStatusCode = LogDetail.SAPStatusCode;
                        Log.SAPErrorMessage = LogDetail.SAPErrorMessage;
                        throw new Exception($"SAP Error: Code {SAPErrorCode} {SAPErrorMessage}");
                    }

                    this.oCompany.GetNewObjectCode(out TranID);
                    DocNum = this.GetDocNum(TranID);
                    GLTranID = this.GetDocTransId(TranID);
                    GLDocNum = this.GetGLDocNum(GLTranID);
                    if (string.IsNullOrEmpty(Log.SAPRefID))
                    {
                        Log.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                        Log.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                        Log.SAPRefNo = DocNum;
                        Log.SAPRefGLNo = GLDocNum;
                    }
                    LogDetail.SAPRefID = string.IsNullOrEmpty(BatchNum) ? TranID : BatchNum;
                    LogDetail.SAPRefDescription = this.SAP_TABLE.Table + "." + this.SAP_TABLE.PK;
                    LogDetail.SAPRefNo = DocNum;
                    LogDetail.SAPRefGLNo = GLDocNum;
                    LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.OK;
                    LogDetail.SAPStatusCode = lRetCode.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SAPRefID = null;
                Log.SAPRefNo = null;
                Log.SAPRefGLNo = null;
                LogDetail.SAPRefID = null;
                LogDetail.SAPRefNo = null;
                LogDetail.SAPRefGLNo = null;
                if (string.IsNullOrEmpty(LogDetail.SAPStatusCode)) LogDetail.REMResponseCode = (int)System.Net.HttpStatusCode.InternalServerError;
                if (string.IsNullOrEmpty(LogDetail.SAPErrorMessage)) LogDetail.REMErrorMessage = ex.Message;

                string Msg = "";
                Msg += ex.Message;
                throw new Exception("/ Error Process_Payments_Cancel : " + Msg);
            }
        }
        #endregion

        #region Sub Private Zone
        private Boolean GetIsLock(DateTime DocDate)
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
            SAPbobsCOM.Recordset oRecordSet = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordSet.DoQuery(SQL_CheckLockPeriod.ToString());
            if (oRecordSet.RecordCount > 0)
            {
                CountLock = oRecordSet.Fields.Item("Cnt").Value;
            }

            IsLock = (CountLock > 0);

            return IsLock;
        }
        private string GetDocNum(string DocEntry)
        {
            int docNum = 0;

            SAPbobsCOM.Recordset oRecordSet = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordSet.DoQuery($"SELECT DocNum FROM {this.SAP_TABLE.Table} WHERE DocEntry = {DocEntry}");
            if (oRecordSet.RecordCount > 0)
            {
                docNum = oRecordSet.Fields.Item("DocNum").Value;
            }

            return docNum.ToString();
        }
        private string GetDocTransId(string DocEntry)
        {
            int DocTransId = 0;

            SAPbobsCOM.Recordset oRecordSet = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordSet.DoQuery($"SELECT TransId FROM {this.SAP_TABLE.Table} WHERE DocEntry = {DocEntry}");
            if (oRecordSet.RecordCount > 0)
            {
                DocTransId = oRecordSet.Fields.Item("TransId").Value;
            }

            return DocTransId.ToString();
        }
        private string GetGLDocNum(string DocTransId)
        {
            int GLDocNum = 0;
            SAPTABLE JournalEntry = new SAPTABLE(Convert.ToInt32(SAPbobsCOM.BoObjectTypes.oJournalEntries));

            SAPbobsCOM.Recordset oRecordSet = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordSet.DoQuery($"SELECT Number FROM {JournalEntry.Table} WHERE TransId = {DocTransId}");
            if (oRecordSet.RecordCount > 0)
            {
                GLDocNum = oRecordSet.Fields.Item("Number").Value;
            }

            return GLDocNum.ToString();
        }
        private string GetDataColumn(string ColumnName, SAPTABLE SABTable, string ColumnWhere, string RefID)
        {
            int Data = 0;
            SAPbobsCOM.Recordset oRecordset = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordset.DoQuery($"select {ColumnName} from {SABTable.Table} WHERE {ColumnWhere} = '{RefID}' ");
            if (oRecordset.RecordCount > 0)
            {
                Data = oRecordset.Fields.Item($"{ColumnName}").Value;
            }

            return Data.ToString();
        }
        private string GetDocEntry(SAPTABLE SABTable, string RefID)
        {
            int Data = 0;
            SAPbobsCOM.Recordset oRecordset = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordset.DoQuery($"select DocEntry from {SABTable.Table} WHERE DocNum = '{RefID}' ");
            if (oRecordset.RecordCount > 0)
            {
                Data = oRecordset.Fields.Item("DocEntry").Value;
            }

            return Data.ToString();
        }
        private string GetSeries(string ObjCode, string Indicator)
        {

            int Data = 0;
            SAPbobsCOM.Recordset oRecordset = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordset.DoQuery($"select Series from nnm1 WHERE objectcode = '{ObjCode}' and indicator = '{Indicator}' and BeginStr = 'RV'");
            if (oRecordset.RecordCount > 0)
            {
                Data = oRecordset.Fields.Item("Series").Value;
            }

            return Data.ToString();
        }
        private string GetItemMasterSeries(string ObjCode)
        {
            int Data = 0;
            SAPbobsCOM.Recordset oRecordset = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordset.DoQuery($"select Series from nnm1 WHERE objectcode = '{ObjCode}' and SeriesName = 'Manual'");
            if (oRecordset.RecordCount > 0)
            {
                Data = oRecordset.Fields.Item("Series").Value;
            }

            return Data.ToString();
        }
        #endregion
    }

    public class SAP_TABLE
    {
        public SAP_TABLE(int DocType)
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

    public class SAP_B1Header
    {
        public SAP_B1Header()
        {
            this.Event = "";
            this.Method = "";
        }
        public void SetByB1Entity(SAP_B1Entity B1Entity)
        {
            this.B1Entity = B1Entity;
            this.Event = B1Entity.LogDetail.B1EventName;
            this.Method = B1Entity.LogDetail.B1MethodName;
        }
        public string Event { get; set; }
        public string Method { get; set; }
        public SAP_B1Entity B1Entity { get; set; }
    }
    public class SAP_B1Entity
    {
        public SAP_B1Entity(InterfaceLog_ext Log, InterfaceLogDetail_ext LogDetail)
        {
            this.Log = Log;
            this.LogDetail = LogDetail;
            this.UField_List = new List<Fields>();
            this.JE_List = new List<JournalEntries>();
            this.JELine_List = new List<JournalEntries_Lines>();
            this.Doc_List = new List<Documents>();
            this.DocLine_List = new List<Document_Lines>();
            this.DocSpLine_List = new List<Document_SpecialLines>();
            this.Pay_List = new List<Payments>();
            this.PayACC_List = new List<Payments_Accounts>();
            this.PayINV_List = new List<Payments_Invoices>();
            this.PayCR_List = new List<Payments_CreditCards>();
            this.PayCQ_List = new List<Payments_Checks>();
        }
        public InterfaceLog_ext Log { get; set; }
        public InterfaceLogDetail_ext LogDetail { get; set; }
        public List<Fields> UField_List { get; set; }
        public List<JournalEntries> JE_List { get; set; }
        public List<JournalEntries_Lines> JELine_List { get; set; }
        public List<Documents> Doc_List { get; set; }
        public List<Document_Lines> DocLine_List { get; set; }
        public List<Document_SpecialLines> DocSpLine_List { get; set; }
        public List<Payments> Pay_List { get; set; }
        public List<Payments_Accounts> PayACC_List { get; set; }
        public List<Payments_Invoices> PayINV_List { get; set; }
        public List<Payments_CreditCards> PayCR_List { get; set; }
        public List<Payments_Checks> PayCQ_List { get; set; }
    }
}
