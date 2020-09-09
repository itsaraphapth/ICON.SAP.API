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
	[XmlRootAttribute("AP_Master_Supplier", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class AP_Master_Supplier
	{
		[XmlIgnore]
		[NonSerialized]
		public AP_Master_Supplier_Command ExecCommand = null;
		public AP_Master_Supplier()
		{
			ExecCommand = new AP_Master_Supplier_Command(this);
		}
		public AP_Master_Supplier(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Supplier_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Supplier_Command(ConnectionStr, this);
		}
		public AP_Master_Supplier(string ConnectionStr, Int32 Id_Value)
		{
			ExecCommand = new AP_Master_Supplier_Command(ConnectionStr, this);
			ExecCommand.Load(Id_Value);
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

        private String _ForeignName ;
        private int _ForeignName_Limit = 200;
		[XmlIgnore]
		public bool EditForeignName = false;
		public String ForeignName
		{
			get
			{
				return _ForeignName;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _ForeignName_Limit)
					throw new Exception("String length longer then prescribed on ForeignName property.");
				if(_ForeignName != value){
					_ForeignName = value;
					EditForeignName = true;
				}
			}
		}

        private String _TaxID ;
        private int _TaxID_Limit = 50;
		[XmlIgnore]
		public bool EditTaxID = false;
		public String TaxID
		{
			get
			{
				return _TaxID;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _TaxID_Limit)
					throw new Exception("String length longer then prescribed on TaxID property.");
				if(_TaxID != value){
					_TaxID = value;
					EditTaxID = true;
				}
			}
		}

        private String _Currency ;
        private int _Currency_Limit = 50;
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
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Currency_Limit)
					throw new Exception("String length longer then prescribed on Currency property.");
				if(_Currency != value){
					_Currency = value;
					EditCurrency = true;
				}
			}
		}

        private String _Tel1 ;
        private int _Tel1_Limit = 50;
		[XmlIgnore]
		public bool EditTel1 = false;
		public String Tel1
		{
			get
			{
				return _Tel1;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Tel1_Limit)
					throw new Exception("String length longer then prescribed on Tel1 property.");
				if(_Tel1 != value){
					_Tel1 = value;
					EditTel1 = true;
				}
			}
		}

        private String _Tel2 ;
        private int _Tel2_Limit = 50;
		[XmlIgnore]
		public bool EditTel2 = false;
		public String Tel2
		{
			get
			{
				return _Tel2;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Tel2_Limit)
					throw new Exception("String length longer then prescribed on Tel2 property.");
				if(_Tel2 != value){
					_Tel2 = value;
					EditTel2 = true;
				}
			}
		}

        private String _MobilePhone ;
        private int _MobilePhone_Limit = 50;
		[XmlIgnore]
		public bool EditMobilePhone = false;
		public String MobilePhone
		{
			get
			{
				return _MobilePhone;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _MobilePhone_Limit)
					throw new Exception("String length longer then prescribed on MobilePhone property.");
				if(_MobilePhone != value){
					_MobilePhone = value;
					EditMobilePhone = true;
				}
			}
		}

        private String _Fax ;
        private int _Fax_Limit = 50;
		[XmlIgnore]
		public bool EditFax = false;
		public String Fax
		{
			get
			{
				return _Fax;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Fax_Limit)
					throw new Exception("String length longer then prescribed on Fax property.");
				if(_Fax != value){
					_Fax = value;
					EditFax = true;
				}
			}
		}

        private String _Email ;
        private int _Email_Limit = 50;
		[XmlIgnore]
		public bool EditEmail = false;
		public String Email
		{
			get
			{
				return _Email;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Email_Limit)
					throw new Exception("String length longer then prescribed on Email property.");
				if(_Email != value){
					_Email = value;
					EditEmail = true;
				}
			}
		}

        private String _WebSite ;
        private int _WebSite_Limit = 200;
		[XmlIgnore]
		public bool EditWebSite = false;
		public String WebSite
		{
			get
			{
				return _WebSite;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _WebSite_Limit)
					throw new Exception("String length longer then prescribed on WebSite property.");
				if(_WebSite != value){
					_WebSite = value;
					EditWebSite = true;
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

        private String _PaymentTerms ;
        private int _PaymentTerms_Limit = 50;
		[XmlIgnore]
		public bool EditPaymentTerms = false;
		public String PaymentTerms
		{
			get
			{
				return _PaymentTerms;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _PaymentTerms_Limit)
					throw new Exception("String length longer then prescribed on PaymentTerms property.");
				if(_PaymentTerms != value){
					_PaymentTerms = value;
					EditPaymentTerms = true;
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
	public partial class AP_Master_Supplier_Command
	{
		string TableName = "AP_Master_Supplier";

		AP_Master_Supplier _AP_Master_Supplier = null;
		DBHelper _DBHelper = null;

		internal AP_Master_Supplier_Command(AP_Master_Supplier obj_AP_Master_Supplier)
		{
			this._AP_Master_Supplier = obj_AP_Master_Supplier;
			this._DBHelper = new DBHelper();
		}

		internal AP_Master_Supplier_Command(string ConnectionStr,AP_Master_Supplier obj_AP_Master_Supplier)
		{
			this._AP_Master_Supplier = obj_AP_Master_Supplier;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _AP_Master_Supplier.GetType().GetProperties())
			{
				_AP_Master_Supplier.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Supplier, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_AP_Master_Supplier,dr[ProInfo.Name],null);
					    //_AP_Master_Supplier.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Supplier, true);
                    }
				}
			}
		}

        public void Load(Int32 Id_Value)
        {
            Load(_DBHelper, Id_Value);
        }

		public void Load(DBHelper _DBHelper ,Int32 Id_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@Id", Id_Value, (DbType)Enum.Parse(typeof(DbType), Id_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Supplier WHERE Id=@Id", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper ,IDbTransaction Trans ,Int32 Id_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@Id", Id_Value, (DbType)Enum.Parse(typeof(DbType), Id_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Supplier WHERE Id=@Id", "AP_Master_Supplier", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("AP_Master_Supplier");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<AP_Master_Supplier> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<AP_Master_Supplier> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("AP_Master_Supplier");
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
            List<AP_Master_Supplier> Res = new List<AP_Master_Supplier>();
            foreach(DataRow Dr in dt.Rows)
            {
                AP_Master_Supplier Item = new AP_Master_Supplier();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO AP_Master_Supplier (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _AP_Master_Supplier.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Supplier.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Supplier)  && ProInfo.Name != "Id")
				{
                    object Value = ProInfo.GetValue(_AP_Master_Supplier, null);
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
            
            string GetIdentityComm = string.Empty;
            if (_DBHelper.Provider == "System.Data.SqlClient")
                GetIdentityComm = "SELECT @@IDENTITY";
            else if (_DBHelper.Provider == "MySql.Data.MySqlClient")
                GetIdentityComm = "SELECT LAST_INSERT_ID()";
            return ssql + ";" + GetIdentityComm;
                    
        }

        public void Insert(DBHelper DBHelp, IDbTransaction DbTransaction)
		{
            DBParameterCollection Parameter = null;
            string SqlInsert = GetInsertCommand(out Parameter);
            if (SqlInsert == "") return;
            _AP_Master_Supplier.Id = int.Parse(DBHelp.ExecuteScalar(SqlInsert, Parameter, DbTransaction).ToString());

		}

        public void Insert()
        {
            DBParameterCollection Parameter = null;
            string SqlInsert = GetInsertCommand(out Parameter);
            if (SqlInsert == "") return;
            _AP_Master_Supplier.Id = int.Parse(_DBHelper.ExecuteScalar(SqlInsert, Parameter).ToString());

        }
            
        private string GetUpdateCommand(out DBParameterCollection Parameter)
        {
            Parameter = new DBParameterCollection();
			string ssql = "UPDATE AP_Master_Supplier SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _AP_Master_Supplier.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Supplier.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Supplier)  && ProInfo.Name != "Id")
				{
                    object Value = ProInfo.GetValue(_AP_Master_Supplier, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "Id")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_AP_Master_Supplier, null)));
				}
			}
			ssql += extSql;
			ssql += " WHERE Id=@Id";
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
            string ssql = "DELETE AP_Master_Supplier WHERE Id=@Id";
            Parameter.Add(new DBParameter("@Id", _AP_Master_Supplier.Id));

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
