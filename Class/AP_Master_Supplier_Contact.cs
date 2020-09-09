/*
** Company Name     :  Trinity Solution Provider Co., Ltd.
** Contact          :  www.iconframework.co.th
** Product Name     :  Data Access framework(Class generator) (2012)
** Modify by        :  Anupong kwanpigul
** Modify Date      : 2019-08-13 17:29:39
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
	[XmlRootAttribute("AP_Master_Supplier_Contact", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class AP_Master_Supplier_Contact
	{
		[XmlIgnore]
		[NonSerialized]
		public AP_Master_Supplier_Contact_Command ExecCommand = null;
		public AP_Master_Supplier_Contact()
		{
			ExecCommand = new AP_Master_Supplier_Contact_Command(this);
		}
		public AP_Master_Supplier_Contact(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Supplier_Contact_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Supplier_Contact_Command(ConnectionStr, this);
		}
		public AP_Master_Supplier_Contact(string ConnectionStr, Int32 Id_Value)
		{
			ExecCommand = new AP_Master_Supplier_Contact_Command(ConnectionStr, this);
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

        private String _SupplierCode ;
        private int _SupplierCode_Limit = 50;
		[XmlIgnore]
		public bool EditSupplierCode = false;
		public String SupplierCode
		{
			get
			{
				return _SupplierCode;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _SupplierCode_Limit)
					throw new Exception("String length longer then prescribed on SupplierCode property.");
				if(_SupplierCode != value){
					_SupplierCode = value;
					EditSupplierCode = true;
				}
			}
		}

        private String _ContactName ;
		[XmlIgnore]
		public bool EditContactName = false;
		public String ContactName
		{
			get
			{
				return _ContactName;
			}
			set
			{
				
				if(_ContactName != value){
					_ContactName = value;
					EditContactName = true;
				}
			}
		}

        private Nullable<Int32> _IsDefault ;
		[XmlIgnore]
		public bool EditIsDefault = false;
		public Nullable<Int32> IsDefault
		{
			get
			{
				return _IsDefault;
			}
			set
			{
				
				if(_IsDefault != value){
					_IsDefault = value;
					EditIsDefault = true;
				}
			}
		}

        private Nullable<Int32> _SupplierId ;
		[XmlIgnore]
		public bool EditSupplierId = false;
		public Nullable<Int32> SupplierId
		{
			get
			{
				return _SupplierId;
			}
			set
			{
				
				if(_SupplierId != value){
					_SupplierId = value;
					EditSupplierId = true;
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

        private String _Mobile ;
        private int _Mobile_Limit = 50;
		[XmlIgnore]
		public bool EditMobile = false;
		public String Mobile
		{
			get
			{
				return _Mobile;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Mobile_Limit)
					throw new Exception("String length longer then prescribed on Mobile property.");
				if(_Mobile != value){
					_Mobile = value;
					EditMobile = true;
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

	}
	public partial class AP_Master_Supplier_Contact_Command
	{
		string TableName = "AP_Master_Supplier_Contact";

		AP_Master_Supplier_Contact _AP_Master_Supplier_Contact = null;
		DBHelper _DBHelper = null;

		internal AP_Master_Supplier_Contact_Command(AP_Master_Supplier_Contact obj_AP_Master_Supplier_Contact)
		{
			this._AP_Master_Supplier_Contact = obj_AP_Master_Supplier_Contact;
			this._DBHelper = new DBHelper();
		}

		internal AP_Master_Supplier_Contact_Command(string ConnectionStr,AP_Master_Supplier_Contact obj_AP_Master_Supplier_Contact)
		{
			this._AP_Master_Supplier_Contact = obj_AP_Master_Supplier_Contact;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _AP_Master_Supplier_Contact.GetType().GetProperties())
			{
				_AP_Master_Supplier_Contact.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Supplier_Contact, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_AP_Master_Supplier_Contact,dr[ProInfo.Name],null);
					    //_AP_Master_Supplier_Contact.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Supplier_Contact, true);
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

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Supplier_Contact WHERE Id=@Id", Parameter);
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

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Supplier_Contact WHERE Id=@Id", "AP_Master_Supplier_Contact", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("AP_Master_Supplier_Contact");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<AP_Master_Supplier_Contact> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<AP_Master_Supplier_Contact> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("AP_Master_Supplier_Contact");
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
            List<AP_Master_Supplier_Contact> Res = new List<AP_Master_Supplier_Contact>();
            foreach(DataRow Dr in dt.Rows)
            {
                AP_Master_Supplier_Contact Item = new AP_Master_Supplier_Contact();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO AP_Master_Supplier_Contact (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _AP_Master_Supplier_Contact.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Supplier_Contact.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Supplier_Contact)  && ProInfo.Name != "Id")
				{
                    object Value = ProInfo.GetValue(_AP_Master_Supplier_Contact, null);
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
            _AP_Master_Supplier_Contact.Id = int.Parse(DBHelp.ExecuteScalar(SqlInsert, Parameter, DbTransaction).ToString());

		}

        public void Insert()
        {
            DBParameterCollection Parameter = null;
            string SqlInsert = GetInsertCommand(out Parameter);
            if (SqlInsert == "") return;
            _AP_Master_Supplier_Contact.Id = int.Parse(_DBHelper.ExecuteScalar(SqlInsert, Parameter).ToString());

        }
            
        private string GetUpdateCommand(out DBParameterCollection Parameter)
        {
            Parameter = new DBParameterCollection();
			string ssql = "UPDATE AP_Master_Supplier_Contact SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _AP_Master_Supplier_Contact.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Supplier_Contact.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Supplier_Contact)  && ProInfo.Name != "Id")
				{
                    object Value = ProInfo.GetValue(_AP_Master_Supplier_Contact, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "Id")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_AP_Master_Supplier_Contact, null)));
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
            string ssql = "DELETE AP_Master_Supplier_Contact WHERE Id=@Id";
            Parameter.Add(new DBParameter("@Id", _AP_Master_Supplier_Contact.Id));

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
