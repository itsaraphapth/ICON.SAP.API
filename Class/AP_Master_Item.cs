/*
** Company Name     :  Trinity Solution Provider Co., Ltd.
** Contact          :  www.iconframework.co.th
** Product Name     :  Data Access framework(Class generator) (2012)
** Modify by        :  Anupong kwanpigul
** Modify Date      : 2019-08-13 17:29:38
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
	[XmlRootAttribute("AP_Master_Item", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class AP_Master_Item
	{
		[XmlIgnore]
		[NonSerialized]
		public AP_Master_Item_Command ExecCommand = null;
		public AP_Master_Item()
		{
			ExecCommand = new AP_Master_Item_Command(this);
		}
		public AP_Master_Item(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Item_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Item_Command(ConnectionStr, this);
		}
		public AP_Master_Item(string ConnectionStr, String Code_Value,String CompanyId_Value)
		{
			ExecCommand = new AP_Master_Item_Command(ConnectionStr, this);
			ExecCommand.Load(Code_Value,CompanyId_Value);
		}
        private Int32 _Id ;
		[XmlIgnore]
		public bool EditId = false;
		public Int32 Id
		{
			get
			{
				return _Id;
			}
			set
			{
				
				if(_Id != value){
					_Id = value;
					EditId = true;
				}
			}
		}

        private String _Code ;
        private int _Code_Limit = 50;
		[XmlIgnore]
		public bool EditCode = false;
		public String Code
		{
			get
			{
				return _Code;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Code_Limit)
					throw new Exception("String length longer then prescribed on Code property.");
				if(_Code != value){
					_Code = value;
					EditCode = true;
				}
			}
		}

        private String _CompanyId ;
        private int _CompanyId_Limit = 50;
		[XmlIgnore]
		public bool EditCompanyId = false;
		public String CompanyId
		{
			get
			{
				return _CompanyId;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _CompanyId_Limit)
					throw new Exception("String length longer then prescribed on CompanyId property.");
				if(_CompanyId != value){
					_CompanyId = value;
					EditCompanyId = true;
				}
			}
		}

        private String _Name ;
        private int _Name_Limit = 200;
		[XmlIgnore]
		public bool EditName = false;
		public String Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Name_Limit)
					throw new Exception("String length longer then prescribed on Name property.");
				if(_Name != value){
					_Name = value;
					EditName = true;
				}
			}
		}

        private String _Description ;
		[XmlIgnore]
		public bool EditDescription = false;
		public String Description
		{
			get
			{
				return _Description;
			}
			set
			{
				
				if(_Description != value){
					_Description = value;
					EditDescription = true;
				}
			}
		}

        private String _Type ;
        private int _Type_Limit = 50;
		[XmlIgnore]
		public bool EditType = false;
		public String Type
		{
			get
			{
				return _Type;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Type_Limit)
					throw new Exception("String length longer then prescribed on Type property.");
				if(_Type != value){
					_Type = value;
					EditType = true;
				}
			}
		}

        private Nullable<DateTime> _StartDate ;
		[XmlIgnore]
		public bool EditStartDate = false;
		public Nullable<DateTime> StartDate
		{
			get
			{
				return _StartDate;
			}
			set
			{
				
				if(_StartDate != value){
					_StartDate = value;
					EditStartDate = true;
				}
			}
		}

        private Nullable<DateTime> _ToDate ;
		[XmlIgnore]
		public bool EditToDate = false;
		public Nullable<DateTime> ToDate
		{
			get
			{
				return _ToDate;
			}
			set
			{
				
				if(_ToDate != value){
					_ToDate = value;
					EditToDate = true;
				}
			}
		}

        private String _UnitCode ;
        private int _UnitCode_Limit = 20;
		[XmlIgnore]
		public bool EditUnitCode = false;
		public String UnitCode
		{
			get
			{
				return _UnitCode;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _UnitCode_Limit)
					throw new Exception("String length longer then prescribed on UnitCode property.");
				if(_UnitCode != value){
					_UnitCode = value;
					EditUnitCode = true;
				}
			}
		}

        private Nullable<Decimal> _UnitPrice ;
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
				
				if(_UnitPrice != value){
					_UnitPrice = value;
					EditUnitPrice = true;
				}
			}
		}

        private String _PriceListType ;
        private int _PriceListType_Limit = 50;
		[XmlIgnore]
		public bool EditPriceListType = false;
		public String PriceListType
		{
			get
			{
				return _PriceListType;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _PriceListType_Limit)
					throw new Exception("String length longer then prescribed on PriceListType property.");
				if(_PriceListType != value){
					_PriceListType = value;
					EditPriceListType = true;
				}
			}
		}

        private String _BarCode ;
        private int _BarCode_Limit = 50;
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
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _BarCode_Limit)
					throw new Exception("String length longer then prescribed on BarCode property.");
				if(_BarCode != value){
					_BarCode = value;
					EditBarCode = true;
				}
			}
		}

        private Nullable<Int32> _LeadTime ;
		[XmlIgnore]
		public bool EditLeadTime = false;
		public Nullable<Int32> LeadTime
		{
			get
			{
				return _LeadTime;
			}
			set
			{
				
				if(_LeadTime != value){
					_LeadTime = value;
					EditLeadTime = true;
				}
			}
		}

        private String _Status ;
        private int _Status_Limit = 50;
		[XmlIgnore]
		public bool EditStatus = false;
		public String Status
		{
			get
			{
				return _Status;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Status_Limit)
					throw new Exception("String length longer then prescribed on Status property.");
				if(_Status != value){
					_Status = value;
					EditStatus = true;
				}
			}
		}

        private Nullable<Int32> _CreateById ;
		[XmlIgnore]
		public bool EditCreateById = false;
		public Nullable<Int32> CreateById
		{
			get
			{
				return _CreateById;
			}
			set
			{
				
				if(_CreateById != value){
					_CreateById = value;
					EditCreateById = true;
				}
			}
		}

        private String _CreateBy ;
        private int _CreateBy_Limit = 100;
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

        private Nullable<Int32> _ModifyById ;
		[XmlIgnore]
		public bool EditModifyById = false;
		public Nullable<Int32> ModifyById
		{
			get
			{
				return _ModifyById;
			}
			set
			{
				
				if(_ModifyById != value){
					_ModifyById = value;
					EditModifyById = true;
				}
			}
		}

        private String _ModifyBy ;
        private int _ModifyBy_Limit = 100;
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

	}
	public partial class AP_Master_Item_Command
	{
		string TableName = "AP_Master_Item";

		AP_Master_Item _AP_Master_Item = null;
		DBHelper _DBHelper = null;

		internal AP_Master_Item_Command(AP_Master_Item obj_AP_Master_Item)
		{
			this._AP_Master_Item = obj_AP_Master_Item;
			this._DBHelper = new DBHelper();
		}

		internal AP_Master_Item_Command(string ConnectionStr,AP_Master_Item obj_AP_Master_Item)
		{
			this._AP_Master_Item = obj_AP_Master_Item;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _AP_Master_Item.GetType().GetProperties())
			{
				_AP_Master_Item.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Item, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_AP_Master_Item,dr[ProInfo.Name],null);
					    //_AP_Master_Item.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Item, true);
                    }
				}
			}
		}

        public void Load(String Code_Value,String CompanyId_Value)
        {
            Load(_DBHelper, Code_Value,CompanyId_Value);
        }

		public void Load(DBHelper _DBHelper ,String Code_Value,String CompanyId_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@Code", Code_Value, (DbType)Enum.Parse(typeof(DbType), Code_Value.GetType().Name, true)));
			Parameter.Add(new DBParameter("@CompanyId", CompanyId_Value, (DbType)Enum.Parse(typeof(DbType), CompanyId_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Item WHERE Code=@Code AND CompanyId=@CompanyId", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper ,IDbTransaction Trans ,String Code_Value,String CompanyId_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@Code", Code_Value, (DbType)Enum.Parse(typeof(DbType), Code_Value.GetType().Name, true)));
			Parameter.Add(new DBParameter("@CompanyId", CompanyId_Value, (DbType)Enum.Parse(typeof(DbType), CompanyId_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Item WHERE Code=@Code AND CompanyId=@CompanyId", "AP_Master_Item", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("AP_Master_Item");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<AP_Master_Item> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<AP_Master_Item> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("AP_Master_Item");
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
            List<AP_Master_Item> Res = new List<AP_Master_Item>();
            foreach(DataRow Dr in dt.Rows)
            {
                AP_Master_Item Item = new AP_Master_Item();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO AP_Master_Item (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _AP_Master_Item.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Item.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Item) )
				{
                    object Value = ProInfo.GetValue(_AP_Master_Item, null);
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
			string ssql = "UPDATE AP_Master_Item SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _AP_Master_Item.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Item.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Item)  && ProInfo.Name != "Code" && ProInfo.Name != "CompanyId")
				{
                    object Value = ProInfo.GetValue(_AP_Master_Item, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "Code" || ProInfo.Name == "CompanyId")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_AP_Master_Item, null)));
				}
			}
			ssql += extSql;
			ssql += " WHERE Code=@Code AND CompanyId=@CompanyId";
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
            string ssql = "DELETE AP_Master_Item WHERE Code=@Code AND CompanyId=@CompanyId";
            Parameter.Add(new DBParameter("@Code", _AP_Master_Item.Code));
			Parameter.Add(new DBParameter("@CompanyId", _AP_Master_Item.CompanyId));

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
