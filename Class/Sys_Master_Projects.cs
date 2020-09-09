/*
** Company Name     :  Trinity Solution Provider Co., Ltd.
** Contact          :  www.iconframework.co.th
** Product Name     :  Data Access framework(Class generator) (2012)
** Modify by        :  Anupong kwanpigul
** Modify Date      : 2019-05-15 14:20:43
 ** Version          : 1.0.0.10
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
	[XmlRootAttribute("Sys_Master_Projects", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class Sys_Master_Projects
	{
		[XmlIgnore]
		[NonSerialized]
		public Sys_Master_Projects_Command ExecCommand = null;
		public Sys_Master_Projects()
		{
			ExecCommand = new Sys_Master_Projects_Command(this);
		}
		public Sys_Master_Projects(string ConnectionStr)
		{
			ExecCommand = new Sys_Master_Projects_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new Sys_Master_Projects_Command(ConnectionStr, this);
		}
		public Sys_Master_Projects(string ConnectionStr, String ProjectID_Value)
		{
			ExecCommand = new Sys_Master_Projects_Command(ConnectionStr, this);
			ExecCommand.Load(ProjectID_Value);
		}
        private String _ProjectID ;
        private int _ProjectID_Limit = 15;
		[XmlIgnore]
		public bool EditProjectID = false;
		public String ProjectID
		{
			get
			{
				return _ProjectID;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ProjectID_Limit)
					throw new Exception("String length longer then prescribed on ProjectID property.");
				if(_ProjectID != value){
					_ProjectID = value;
					EditProjectID = true;
				}
			}
		}

        private String _ProjectName ;
		[XmlIgnore]
		public bool EditProjectName = false;
		public String ProjectName
		{
			get
			{
				return _ProjectName;
			}
			set
			{
				
				if(_ProjectName != value){
					_ProjectName = value;
					EditProjectName = true;
				}
			}
		}

        private String _ProjectNameEng ;
		[XmlIgnore]
		public bool EditProjectNameEng = false;
		public String ProjectNameEng
		{
			get
			{
				return _ProjectNameEng;
			}
			set
			{
				
				if(_ProjectNameEng != value){
					_ProjectNameEng = value;
					EditProjectNameEng = true;
				}
			}
		}

        private String _ProjectNameTitleDeed ;
		[XmlIgnore]
		public bool EditProjectNameTitleDeed = false;
		public String ProjectNameTitleDeed
		{
			get
			{
				return _ProjectNameTitleDeed;
			}
			set
			{
				
				if(_ProjectNameTitleDeed != value){
					_ProjectNameTitleDeed = value;
					EditProjectNameTitleDeed = true;
				}
			}
		}

        private String _ProjectType ;
        private int _ProjectType_Limit = 1;
		[XmlIgnore]
		public bool EditProjectType = false;
		public String ProjectType
		{
			get
			{
				return _ProjectType;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ProjectType_Limit)
					throw new Exception("String length longer then prescribed on ProjectType property.");
				if(_ProjectType != value){
					_ProjectType = value;
					EditProjectType = true;
				}
			}
		}

        private Nullable<Int32> _RealEstateType ;
		[XmlIgnore]
		public bool EditRealEstateType = false;
		public Nullable<Int32> RealEstateType
		{
			get
			{
				return _RealEstateType;
			}
			set
			{
				
				if(_RealEstateType != value){
					_RealEstateType = value;
					EditRealEstateType = true;
				}
			}
		}

        private String _BUID ;
        private int _BUID_Limit = 50;
		[XmlIgnore]
		public bool EditBUID = false;
		public String BUID
		{
			get
			{
				return _BUID;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _BUID_Limit)
					throw new Exception("String length longer then prescribed on BUID property.");
				if(_BUID != value){
					_BUID = value;
					EditBUID = true;
				}
			}
		}

        private Nullable<Int32> _SubBUID ;
		[XmlIgnore]
		public bool EditSubBUID = false;
		public Nullable<Int32> SubBUID
		{
			get
			{
				return _SubBUID;
			}
			set
			{
				
				if(_SubBUID != value){
					_SubBUID = value;
					EditSubBUID = true;
				}
			}
		}

        private String _BrandID ;
        private int _BrandID_Limit = 15;
		[XmlIgnore]
		public bool EditBrandID = false;
		public String BrandID
		{
			get
			{
				return _BrandID;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _BrandID_Limit)
					throw new Exception("String length longer then prescribed on BrandID property.");
				if(_BrandID != value){
					_BrandID = value;
					EditBrandID = true;
				}
			}
		}

        private String _CompanyID ;
        private int _CompanyID_Limit = 15;
		[XmlIgnore]
		public bool EditCompanyID = false;
		public String CompanyID
		{
			get
			{
				return _CompanyID;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _CompanyID_Limit)
					throw new Exception("String length longer then prescribed on CompanyID property.");
				if(_CompanyID != value){
					_CompanyID = value;
					EditCompanyID = true;
				}
			}
		}

        private Nullable<Int32> _TotalUnit ;
		[XmlIgnore]
		public bool EditTotalUnit = false;
		public Nullable<Int32> TotalUnit
		{
			get
			{
				return _TotalUnit;
			}
			set
			{
				
				if(_TotalUnit != value){
					_TotalUnit = value;
					EditTotalUnit = true;
				}
			}
		}

        private Nullable<Int32> _TotalTitleDeed ;
		[XmlIgnore]
		public bool EditTotalTitleDeed = false;
		public Nullable<Int32> TotalTitleDeed
		{
			get
			{
				return _TotalTitleDeed;
			}
			set
			{
				
				if(_TotalTitleDeed != value){
					_TotalTitleDeed = value;
					EditTotalTitleDeed = true;
				}
			}
		}

        private String _ProjectStatus ;
        private int _ProjectStatus_Limit = 15;
		[XmlIgnore]
		public bool EditProjectStatus = false;
		public String ProjectStatus
		{
			get
			{
				return _ProjectStatus;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ProjectStatus_Limit)
					throw new Exception("String length longer then prescribed on ProjectStatus property.");
				if(_ProjectStatus != value){
					_ProjectStatus = value;
					EditProjectStatus = true;
				}
			}
		}

        private Nullable<DateTime> _ProjectOpen ;
		[XmlIgnore]
		public bool EditProjectOpen = false;
		public Nullable<DateTime> ProjectOpen
		{
			get
			{
				return _ProjectOpen;
			}
			set
			{
				
				if(_ProjectOpen != value){
					_ProjectOpen = value;
					EditProjectOpen = true;
				}
			}
		}

        private Nullable<DateTime> _ProjectClose ;
		[XmlIgnore]
		public bool EditProjectClose = false;
		public Nullable<DateTime> ProjectClose
		{
			get
			{
				return _ProjectClose;
			}
			set
			{
				
				if(_ProjectClose != value){
					_ProjectClose = value;
					EditProjectClose = true;
				}
			}
		}

        private String _ProjectOwner ;
        private int _ProjectOwner_Limit = 50;
		[XmlIgnore]
		public bool EditProjectOwner = false;
		public String ProjectOwner
		{
			get
			{
				return _ProjectOwner;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ProjectOwner_Limit)
					throw new Exception("String length longer then prescribed on ProjectOwner property.");
				if(_ProjectOwner != value){
					_ProjectOwner = value;
					EditProjectOwner = true;
				}
			}
		}

        private String _ProjectTel ;
        private int _ProjectTel_Limit = 20;
		[XmlIgnore]
		public bool EditProjectTel = false;
		public String ProjectTel
		{
			get
			{
				return _ProjectTel;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ProjectTel_Limit)
					throw new Exception("String length longer then prescribed on ProjectTel property.");
				if(_ProjectTel != value){
					_ProjectTel = value;
					EditProjectTel = true;
				}
			}
		}

        private String _ProjectFax ;
        private int _ProjectFax_Limit = 20;
		[XmlIgnore]
		public bool EditProjectFax = false;
		public String ProjectFax
		{
			get
			{
				return _ProjectFax;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ProjectFax_Limit)
					throw new Exception("String length longer then prescribed on ProjectFax property.");
				if(_ProjectFax != value){
					_ProjectFax = value;
					EditProjectFax = true;
				}
			}
		}

        private String _ProjectEmail ;
        private int _ProjectEmail_Limit = 100;
		[XmlIgnore]
		public bool EditProjectEmail = false;
		public String ProjectEmail
		{
			get
			{
				return _ProjectEmail;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ProjectEmail_Limit)
					throw new Exception("String length longer then prescribed on ProjectEmail property.");
				if(_ProjectEmail != value){
					_ProjectEmail = value;
					EditProjectEmail = true;
				}
			}
		}

        private String _ProjectWebsite ;
        private int _ProjectWebsite_Limit = 100;
		[XmlIgnore]
		public bool EditProjectWebsite = false;
		public String ProjectWebsite
		{
			get
			{
				return _ProjectWebsite;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ProjectWebsite_Limit)
					throw new Exception("String length longer then prescribed on ProjectWebsite property.");
				if(_ProjectWebsite != value){
					_ProjectWebsite = value;
					EditProjectWebsite = true;
				}
			}
		}

        private Nullable<DateTime> _BuildCompleteDate ;
		[XmlIgnore]
		public bool EditBuildCompleteDate = false;
		public Nullable<DateTime> BuildCompleteDate
		{
			get
			{
				return _BuildCompleteDate;
			}
			set
			{
				
				if(_BuildCompleteDate != value){
					_BuildCompleteDate = value;
					EditBuildCompleteDate = true;
				}
			}
		}

        private Nullable<Decimal> _ProjectValues ;
		[XmlIgnore]
		public bool EditProjectValues = false;
		public Nullable<Decimal> ProjectValues
		{
			get
			{
				return _ProjectValues;
			}
			set
			{
				
				if(_ProjectValues != value){
					_ProjectValues = value;
					EditProjectValues = true;
				}
			}
		}

        private Nullable<Int32> _AreaRai ;
		[XmlIgnore]
		public bool EditAreaRai = false;
		public Nullable<Int32> AreaRai
		{
			get
			{
				return _AreaRai;
			}
			set
			{
				
				if(_AreaRai != value){
					_AreaRai = value;
					EditAreaRai = true;
				}
			}
		}

        private Nullable<Int32> _Areangan ;
		[XmlIgnore]
		public bool EditAreangan = false;
		public Nullable<Int32> Areangan
		{
			get
			{
				return _Areangan;
			}
			set
			{
				
				if(_Areangan != value){
					_Areangan = value;
					EditAreangan = true;
				}
			}
		}

        private Nullable<Decimal> _AreaSquareWah ;
		[XmlIgnore]
		public bool EditAreaSquareWah = false;
		public Nullable<Decimal> AreaSquareWah
		{
			get
			{
				return _AreaSquareWah;
			}
			set
			{
				
				if(_AreaSquareWah != value){
					_AreaSquareWah = value;
					EditAreaSquareWah = true;
				}
			}
		}

        private String _Remark ;
		[XmlIgnore]
		public bool EditRemark = false;
		public String Remark
		{
			get
			{
				return _Remark;
			}
			set
			{
				
				if(_Remark != value){
					_Remark = value;
					EditRemark = true;
				}
			}
		}

        private Nullable<Int32> _JuristicID ;
		[XmlIgnore]
		public bool EditJuristicID = false;
		public Nullable<Int32> JuristicID
		{
			get
			{
				return _JuristicID;
			}
			set
			{
				
				if(_JuristicID != value){
					_JuristicID = value;
					EditJuristicID = true;
				}
			}
		}

        private String _JuristicName ;
        private int _JuristicName_Limit = 50;
		[XmlIgnore]
		public bool EditJuristicName = false;
		public String JuristicName
		{
			get
			{
				return _JuristicName;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _JuristicName_Limit)
					throw new Exception("String length longer then prescribed on JuristicName property.");
				if(_JuristicName != value){
					_JuristicName = value;
					EditJuristicName = true;
				}
			}
		}

        private String _JuristicNameEng ;
        private int _JuristicNameEng_Limit = 50;
		[XmlIgnore]
		public bool EditJuristicNameEng = false;
		public String JuristicNameEng
		{
			get
			{
				return _JuristicNameEng;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _JuristicNameEng_Limit)
					throw new Exception("String length longer then prescribed on JuristicNameEng property.");
				if(_JuristicNameEng != value){
					_JuristicNameEng = value;
					EditJuristicNameEng = true;
				}
			}
		}

        private Nullable<DateTime> _JuristicDate ;
		[XmlIgnore]
		public bool EditJuristicDate = false;
		public Nullable<DateTime> JuristicDate
		{
			get
			{
				return _JuristicDate;
			}
			set
			{
				
				if(_JuristicDate != value){
					_JuristicDate = value;
					EditJuristicDate = true;
				}
			}
		}

        private String _ImgPath ;
        private int _ImgPath_Limit = 50;
		[XmlIgnore]
		public bool EditImgPath = false;
		public String ImgPath
		{
			get
			{
				return _ImgPath;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ImgPath_Limit)
					throw new Exception("String length longer then prescribed on ImgPath property.");
				if(_ImgPath != value){
					_ImgPath = value;
					EditImgPath = true;
				}
			}
		}

        private Nullable<Double> _BudgetAlertPerc ;
		[XmlIgnore]
		public bool EditBudgetAlertPerc = false;
		public Nullable<Double> BudgetAlertPerc
		{
			get
			{
				return _BudgetAlertPerc;
			}
			set
			{
				
				if(_BudgetAlertPerc != value){
					_BudgetAlertPerc = value;
					EditBudgetAlertPerc = true;
				}
			}
		}

        private Nullable<Decimal> _BudgetAlertAmt ;
		[XmlIgnore]
		public bool EditBudgetAlertAmt = false;
		public Nullable<Decimal> BudgetAlertAmt
		{
			get
			{
				return _BudgetAlertAmt;
			}
			set
			{
				
				if(_BudgetAlertAmt != value){
					_BudgetAlertAmt = value;
					EditBudgetAlertAmt = true;
				}
			}
		}

        private Nullable<Boolean> _isRenovate ;
		[XmlIgnore]
		public bool EditisRenovate = false;
		public Nullable<Boolean> isRenovate
		{
			get
			{
				return _isRenovate;
			}
			set
			{
				
				if(_isRenovate != value){
					_isRenovate = value;
					EditisRenovate = true;
				}
			}
		}

        private Nullable<Boolean> _isDelete ;
		[XmlIgnore]
		public bool EditisDelete = false;
		public Nullable<Boolean> isDelete
		{
			get
			{
				return _isDelete;
			}
			set
			{
				
				if(_isDelete != value){
					_isDelete = value;
					EditisDelete = true;
				}
			}
		}

        private Nullable<DateTime> _CreateDate ;
		[XmlIgnore]
		public bool EditCreateDate = false;
		public Nullable<DateTime> CreateDate
		{
			get
			{
				return _CreateDate;
			}
			set
			{
				
				if(_CreateDate != value){
					_CreateDate = value;
					EditCreateDate = true;
				}
			}
		}

        private String _CreateBy ;
        private int _CreateBy_Limit = 50;
		[XmlIgnore]
		public bool EditCreateBy = false;
		public String CreateBy
		{
			get
			{
				return _CreateBy;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _CreateBy_Limit)
					throw new Exception("String length longer then prescribed on CreateBy property.");
				if(_CreateBy != value){
					_CreateBy = value;
					EditCreateBy = true;
				}
			}
		}

        private Nullable<DateTime> _ModifyDate ;
		[XmlIgnore]
		public bool EditModifyDate = false;
		public Nullable<DateTime> ModifyDate
		{
			get
			{
				return _ModifyDate;
			}
			set
			{
				
				if(_ModifyDate != value){
					_ModifyDate = value;
					EditModifyDate = true;
				}
			}
		}

        private String _ModifyBy ;
        private int _ModifyBy_Limit = 50;
		[XmlIgnore]
		public bool EditModifyBy = false;
		public String ModifyBy
		{
			get
			{
				return _ModifyBy;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ModifyBy_Limit)
					throw new Exception("String length longer then prescribed on ModifyBy property.");
				if(_ModifyBy != value){
					_ModifyBy = value;
					EditModifyBy = true;
				}
			}
		}

        private String _BOQID ;
        private int _BOQID_Limit = 50;
		[XmlIgnore]
		public bool EditBOQID = false;
		public String BOQID
		{
			get
			{
				return _BOQID;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _BOQID_Limit)
					throw new Exception("String length longer then prescribed on BOQID property.");
				if(_BOQID != value){
					_BOQID = value;
					EditBOQID = true;
				}
			}
		}

        private String _MoFinanceNameTH ;
        private int _MoFinanceNameTH_Limit = 255;
		[XmlIgnore]
		public bool EditMoFinanceNameTH = false;
		public String MoFinanceNameTH
		{
			get
			{
				return _MoFinanceNameTH;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _MoFinanceNameTH_Limit)
					throw new Exception("String length longer then prescribed on MoFinanceNameTH property.");
				if(_MoFinanceNameTH != value){
					_MoFinanceNameTH = value;
					EditMoFinanceNameTH = true;
				}
			}
		}

        private String _MoFinanceNameEN ;
        private int _MoFinanceNameEN_Limit = 255;
		[XmlIgnore]
		public bool EditMoFinanceNameEN = false;
		public String MoFinanceNameEN
		{
			get
			{
				return _MoFinanceNameEN;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _MoFinanceNameEN_Limit)
					throw new Exception("String length longer then prescribed on MoFinanceNameEN property.");
				if(_MoFinanceNameEN != value){
					_MoFinanceNameEN = value;
					EditMoFinanceNameEN = true;
				}
			}
		}

        private String _Port ;
        private int _Port_Limit = 255;
		[XmlIgnore]
		public bool EditPort = false;
		public String Port
		{
			get
			{
				return _Port;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Port_Limit)
					throw new Exception("String length longer then prescribed on Port property.");
				if(_Port != value){
					_Port = value;
					EditPort = true;
				}
			}
		}

        private String _AbProjectName ;
        private int _AbProjectName_Limit = 255;
		[XmlIgnore]
		public bool EditAbProjectName = false;
		public String AbProjectName
		{
			get
			{
				return _AbProjectName;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _AbProjectName_Limit)
					throw new Exception("String length longer then prescribed on AbProjectName property.");
				if(_AbProjectName != value){
					_AbProjectName = value;
					EditAbProjectName = true;
				}
			}
		}

        private String _ProjectImagePath ;
        private int _ProjectImagePath_Limit = 510;
		[XmlIgnore]
		public bool EditProjectImagePath = false;
		public String ProjectImagePath
		{
			get
			{
				return _ProjectImagePath;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ProjectImagePath_Limit)
					throw new Exception("String length longer then prescribed on ProjectImagePath property.");
				if(_ProjectImagePath != value){
					_ProjectImagePath = value;
					EditProjectImagePath = true;
				}
			}
		}

        private String _SAPWBSCode ;
        private int _SAPWBSCode_Limit = 20;
		[XmlIgnore]
		public bool EditSAPWBSCode = false;
		public String SAPWBSCode
		{
			get
			{
				return _SAPWBSCode;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPWBSCode_Limit)
					throw new Exception("String length longer then prescribed on SAPWBSCode property.");
				if(_SAPWBSCode != value){
					_SAPWBSCode = value;
					EditSAPWBSCode = true;
				}
			}
		}

        private String _ACCWBSCode ;
        private int _ACCWBSCode_Limit = 20;
		[XmlIgnore]
		public bool EditACCWBSCode = false;
		public String ACCWBSCode
		{
			get
			{
				return _ACCWBSCode;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ACCWBSCode_Limit)
					throw new Exception("String length longer then prescribed on ACCWBSCode property.");
				if(_ACCWBSCode != value){
					_ACCWBSCode = value;
					EditACCWBSCode = true;
				}
			}
		}

        private String _COMWBSCode ;
        private int _COMWBSCode_Limit = 20;
		[XmlIgnore]
		public bool EditCOMWBSCode = false;
		public String COMWBSCode
		{
			get
			{
				return _COMWBSCode;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _COMWBSCode_Limit)
					throw new Exception("String length longer then prescribed on COMWBSCode property.");
				if(_COMWBSCode != value){
					_COMWBSCode = value;
					EditCOMWBSCode = true;
				}
			}
		}

        private String _PlantCode ;
        private int _PlantCode_Limit = 20;
		[XmlIgnore]
		public bool EditPlantCode = false;
		public String PlantCode
		{
			get
			{
				return _PlantCode;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _PlantCode_Limit)
					throw new Exception("String length longer then prescribed on PlantCode property.");
				if(_PlantCode != value){
					_PlantCode = value;
					EditPlantCode = true;
				}
			}
		}

        private String _SAPProfitCenter ;
        private int _SAPProfitCenter_Limit = 50;
		[XmlIgnore]
		public bool EditSAPProfitCenter = false;
		public String SAPProfitCenter
		{
			get
			{
				return _SAPProfitCenter;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPProfitCenter_Limit)
					throw new Exception("String length longer then prescribed on SAPProfitCenter property.");
				if(_SAPProfitCenter != value){
					_SAPProfitCenter = value;
					EditSAPProfitCenter = true;
				}
			}
		}

        private String _SAPProfixCenter ;
        private int _SAPProfixCenter_Limit = 50;
		[XmlIgnore]
		public bool EditSAPProfixCenter = false;
		public String SAPProfixCenter
		{
			get
			{
				return _SAPProfixCenter;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPProfixCenter_Limit)
					throw new Exception("String length longer then prescribed on SAPProfixCenter property.");
				if(_SAPProfixCenter != value){
					_SAPProfixCenter = value;
					EditSAPProfixCenter = true;
				}
			}
		}

        private String _SAPPostCenter ;
        private int _SAPPostCenter_Limit = 50;
		[XmlIgnore]
		public bool EditSAPPostCenter = false;
		public String SAPPostCenter
		{
			get
			{
				return _SAPPostCenter;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPPostCenter_Limit)
					throw new Exception("String length longer then prescribed on SAPPostCenter property.");
				if(_SAPPostCenter != value){
					_SAPPostCenter = value;
					EditSAPPostCenter = true;
				}
			}
		}

        private String _SAPCostCenter ;
        private int _SAPCostCenter_Limit = 50;
		[XmlIgnore]
		public bool EditSAPCostCenter = false;
		public String SAPCostCenter
		{
			get
			{
				return _SAPCostCenter;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPCostCenter_Limit)
					throw new Exception("String length longer then prescribed on SAPCostCenter property.");
				if(_SAPCostCenter != value){
					_SAPCostCenter = value;
					EditSAPCostCenter = true;
				}
			}
		}

        private Nullable<Boolean> _AllowSendSAP ;
		[XmlIgnore]
		public bool EditAllowSendSAP = false;
		public Nullable<Boolean> AllowSendSAP
		{
			get
			{
				return _AllowSendSAP;
			}
			set
			{
				
				if(_AllowSendSAP != value){
					_AllowSendSAP = value;
					EditAllowSendSAP = true;
				}
			}
		}

        private String _SAPCostCenter2 ;
        private int _SAPCostCenter2_Limit = 50;
		[XmlIgnore]
		public bool EditSAPCostCenter2 = false;
		public String SAPCostCenter2
		{
			get
			{
				return _SAPCostCenter2;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPCostCenter2_Limit)
					throw new Exception("String length longer then prescribed on SAPCostCenter2 property.");
				if(_SAPCostCenter2 != value){
					_SAPCostCenter2 = value;
					EditSAPCostCenter2 = true;
				}
			}
		}

        private String _SAPBandCode ;
        private int _SAPBandCode_Limit = 50;
		[XmlIgnore]
		public bool EditSAPBandCode = false;
		public String SAPBandCode
		{
			get
			{
				return _SAPBandCode;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPBandCode_Limit)
					throw new Exception("String length longer then prescribed on SAPBandCode property.");
				if(_SAPBandCode != value){
					_SAPBandCode = value;
					EditSAPBandCode = true;
				}
			}
		}

        private String _SAPPlantCode ;
        private int _SAPPlantCode_Limit = 50;
		[XmlIgnore]
		public bool EditSAPPlantCode = false;
		public String SAPPlantCode
		{
			get
			{
				return _SAPPlantCode;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPPlantCode_Limit)
					throw new Exception("String length longer then prescribed on SAPPlantCode property.");
				if(_SAPPlantCode != value){
					_SAPPlantCode = value;
					EditSAPPlantCode = true;
				}
			}
		}

        private String _SAPPlantCode2 ;
        private int _SAPPlantCode2_Limit = 50;
		[XmlIgnore]
		public bool EditSAPPlantCode2 = false;
		public String SAPPlantCode2
		{
			get
			{
				return _SAPPlantCode2;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPPlantCode2_Limit)
					throw new Exception("String length longer then prescribed on SAPPlantCode2 property.");
				if(_SAPPlantCode2 != value){
					_SAPPlantCode2 = value;
					EditSAPPlantCode2 = true;
				}
			}
		}

        private String _SAPWBSCode47 ;
        private int _SAPWBSCode47_Limit = 50;
		[XmlIgnore]
		public bool EditSAPWBSCode47 = false;
		public String SAPWBSCode47
		{
			get
			{
				return _SAPWBSCode47;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPWBSCode47_Limit)
					throw new Exception("String length longer then prescribed on SAPWBSCode47 property.");
				if(_SAPWBSCode47 != value){
					_SAPWBSCode47 = value;
					EditSAPWBSCode47 = true;
				}
			}
		}

        private String _SAPCostCenter47 ;
        private int _SAPCostCenter47_Limit = 50;
		[XmlIgnore]
		public bool EditSAPCostCenter47 = false;
		public String SAPCostCenter47
		{
			get
			{
				return _SAPCostCenter47;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPCostCenter47_Limit)
					throw new Exception("String length longer then prescribed on SAPCostCenter47 property.");
				if(_SAPCostCenter47 != value){
					_SAPCostCenter47 = value;
					EditSAPCostCenter47 = true;
				}
			}
		}

        private String _SAPCostCenter472 ;
        private int _SAPCostCenter472_Limit = 50;
		[XmlIgnore]
		public bool EditSAPCostCenter472 = false;
		public String SAPCostCenter472
		{
			get
			{
				return _SAPCostCenter472;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SAPCostCenter472_Limit)
					throw new Exception("String length longer then prescribed on SAPCostCenter472 property.");
				if(_SAPCostCenter472 != value){
					_SAPCostCenter472 = value;
					EditSAPCostCenter472 = true;
				}
			}
		}

        private String _AccountStaffName ;
		[XmlIgnore]
		public bool EditAccountStaffName = false;
		public String AccountStaffName
		{
			get
			{
				return _AccountStaffName;
			}
			set
			{
				
				if(_AccountStaffName != value){
					_AccountStaffName = value;
					EditAccountStaffName = true;
				}
			}
		}

        private Nullable<Decimal> _ProjectValues2 ;
		[XmlIgnore]
		public bool EditProjectValues2 = false;
		public Nullable<Decimal> ProjectValues2
		{
			get
			{
				return _ProjectValues2;
			}
			set
			{
				
				if(_ProjectValues2 != value){
					_ProjectValues2 = value;
					EditProjectValues2 = true;
				}
			}
		}

        private String _ContractType ;
        private int _ContractType_Limit = 20;
		[XmlIgnore]
		public bool EditContractType = false;
		public String ContractType
		{
			get
			{
				return _ContractType;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ContractType_Limit)
					throw new Exception("String length longer then prescribed on ContractType property.");
				if(_ContractType != value){
					_ContractType = value;
					EditContractType = true;
				}
			}
		}

        private String _AccountProject ;
        private int _AccountProject_Limit = 50;
		[XmlIgnore]
		public bool EditAccountProject = false;
		public String AccountProject
		{
			get
			{
				return _AccountProject;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _AccountProject_Limit)
					throw new Exception("String length longer then prescribed on AccountProject property.");
				if(_AccountProject != value){
					_AccountProject = value;
					EditAccountProject = true;
				}
			}
		}

        private String _Base64Image ;
		[XmlIgnore]
		public bool EditBase64Image = false;
		public String Base64Image
		{
			get
			{
				return _Base64Image;
			}
			set
			{
				
				if(_Base64Image != value){
					_Base64Image = value;
					EditBase64Image = true;
				}
			}
		}

        private Nullable<Boolean> _isReserve ;
		[XmlIgnore]
		public bool EditisReserve = false;
		public Nullable<Boolean> isReserve
		{
			get
			{
				return _isReserve;
			}
			set
			{
				
				if(_isReserve != value){
					_isReserve = value;
					EditisReserve = true;
				}
			}
		}

        private String _AllocateLand ;
        private int _AllocateLand_Limit = 50;
		[XmlIgnore]
		public bool EditAllocateLand = false;
		public String AllocateLand
		{
			get
			{
				return _AllocateLand;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _AllocateLand_Limit)
					throw new Exception("String length longer then prescribed on AllocateLand property.");
				if(_AllocateLand != value){
					_AllocateLand = value;
					EditAllocateLand = true;
				}
			}
		}

        private String _JuristicStatus ;
        private int _JuristicStatus_Limit = 5;
		[XmlIgnore]
		public bool EditJuristicStatus = false;
		public String JuristicStatus
		{
			get
			{
				return _JuristicStatus;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _JuristicStatus_Limit)
					throw new Exception("String length longer then prescribed on JuristicStatus property.");
				if(_JuristicStatus != value){
					_JuristicStatus = value;
					EditJuristicStatus = true;
				}
			}
		}

	}
	public partial class Sys_Master_Projects_Command
	{
		string TableName = "Sys_Master_Projects";

		Sys_Master_Projects _Sys_Master_Projects = null;
		DBHelper _DBHelper = null;

		internal Sys_Master_Projects_Command(Sys_Master_Projects obj_Sys_Master_Projects)
		{
			this._Sys_Master_Projects = obj_Sys_Master_Projects;
			this._DBHelper = new DBHelper();
		}

		internal Sys_Master_Projects_Command(string ConnectionStr,Sys_Master_Projects obj_Sys_Master_Projects)
		{
			this._Sys_Master_Projects = obj_Sys_Master_Projects;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _Sys_Master_Projects.GetType().GetProperties())
			{
				_Sys_Master_Projects.GetType().GetField("Edit" + ProInfo.Name).SetValue(_Sys_Master_Projects, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_Sys_Master_Projects,dr[ProInfo.Name],null);
					    //_Sys_Master_Projects.GetType().GetField("Edit" + ProInfo.Name).SetValue(_Sys_Master_Projects, true);
                    }
				}
			}
		}

        public void Load(String ProjectID_Value)
        {
            Load(_DBHelper, ProjectID_Value);
        }

		public void Load(DBHelper _DBHelper ,String ProjectID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@ProjectID", ProjectID_Value, (DbType)Enum.Parse(typeof(DbType), ProjectID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Sys_Master_Projects WHERE ProjectID=@ProjectID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper ,IDbTransaction Trans ,String ProjectID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@ProjectID", ProjectID_Value, (DbType)Enum.Parse(typeof(DbType), ProjectID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Sys_Master_Projects WHERE ProjectID=@ProjectID", "Sys_Master_Projects", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("Sys_Master_Projects");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<Sys_Master_Projects> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<Sys_Master_Projects> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("Sys_Master_Projects");
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
            List<Sys_Master_Projects> Res = new List<Sys_Master_Projects>();
            foreach(DataRow Dr in dt.Rows)
            {
                Sys_Master_Projects Item = new Sys_Master_Projects();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO Sys_Master_Projects (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _Sys_Master_Projects.GetType().GetProperties())
			{
				if ((bool)_Sys_Master_Projects.GetType().GetField("Edit" + ProInfo.Name).GetValue(_Sys_Master_Projects) )
				{
                    object Value = ProInfo.GetValue(_Sys_Master_Projects, null);
                    if (Value == null) Value = DBNull.Value;
					if (extColumn != "") extColumn += ",";
					extColumn += ProInfo.Name ;

					if (extParameter != "") extParameter += ",";
					extParameter += "@" + ProInfo.Name ;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
			}
            if (extColumn == "") return "";
			ssql += extColumn + ") VALUES(" + extParameter + ")";
            return ssql;

        }

        public void Insert(DBHelper DBHelp, IDbTransaction DbTransaction)
		{
            DBParameterCollection Parameter = null;
            string SqlInsert = GetInsertCommand(out Parameter);
            if (SqlInsert == "") return;
            DBHelp.ExecuteNonQuery(SqlInsert, Parameter, DbTransaction);

		}

        public void Insert()
        {
            DBParameterCollection Parameter = null;
            string SqlInsert = GetInsertCommand(out Parameter);
            if (SqlInsert == "") return;
            _DBHelper.ExecuteNonQuery(SqlInsert, Parameter);

        }
            
        private string GetUpdateCommand(out DBParameterCollection Parameter)
        {
            Parameter = new DBParameterCollection();
			string ssql = "UPDATE Sys_Master_Projects SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _Sys_Master_Projects.GetType().GetProperties())
			{
				if ((bool)_Sys_Master_Projects.GetType().GetField("Edit" + ProInfo.Name).GetValue(_Sys_Master_Projects)  && ProInfo.Name != "ProjectID")
				{
                    object Value = ProInfo.GetValue(_Sys_Master_Projects, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "ProjectID")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_Sys_Master_Projects, null)));
				}
			}
			ssql += extSql;
			ssql += " WHERE ProjectID=@ProjectID";
            if (extSql == "") return "";
            return ssql;
        }

        public void Update(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            DBParameterCollection Parameter = null;
            string SqlUpdate = GetUpdateCommand(out Parameter);
            if (SqlUpdate == "") return;
            DBHelp.ExecuteNonQuery(SqlUpdate, Parameter, DbTransaction);
        }

		public void Update()
		{
            DBParameterCollection Parameter = null;
            string SqlUpdate = GetUpdateCommand(out Parameter);
            if (SqlUpdate == "") return;
            _DBHelper.ExecuteNonQuery(SqlUpdate, Parameter);
		}
            
        private string GetDeleteCommand(out DBParameterCollection Parameter)
        {
            Parameter = new DBParameterCollection();
            string ssql = "DELETE Sys_Master_Projects WHERE ProjectID=@ProjectID";
            Parameter.Add(new DBParameter("@ProjectID", _Sys_Master_Projects.ProjectID));

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
