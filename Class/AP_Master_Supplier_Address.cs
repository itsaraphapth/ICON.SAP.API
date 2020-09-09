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
	[XmlRootAttribute("AP_Master_Supplier_Address", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class AP_Master_Supplier_Address
	{
		[XmlIgnore]
		[NonSerialized]
		public AP_Master_Supplier_Address_Command ExecCommand = null;
		public AP_Master_Supplier_Address()
		{
			ExecCommand = new AP_Master_Supplier_Address_Command(this);
		}
		public AP_Master_Supplier_Address(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Supplier_Address_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Supplier_Address_Command(ConnectionStr, this);
		}
		public AP_Master_Supplier_Address(string ConnectionStr, Int32 Id_Value,Int32 SupplierId_Value)
		{
			ExecCommand = new AP_Master_Supplier_Address_Command(ConnectionStr, this);
			ExecCommand.Load(Id_Value,SupplierId_Value);
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

        private Int32 _SupplierId ;
		[XmlIgnore]
		public bool EditSupplierId = false;
		public Int32 SupplierId
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

        private String _AddressType ;
        private int _AddressType_Limit = 50;
		[XmlIgnore]
		public bool EditAddressType = false;
		public String AddressType
		{
			get
			{
				return _AddressType;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _AddressType_Limit)
					throw new Exception("String length longer then prescribed on AddressType property.");
				if(_AddressType != value){
					_AddressType = value;
					EditAddressType = true;
				}
			}
		}

        private String _Address ;
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
				
				if(_Address != value){
					_Address = value;
					EditAddress = true;
				}
			}
		}

        private String _SubDistrict ;
		[XmlIgnore]
		public bool EditSubDistrict = false;
		public String SubDistrict
		{
			get
			{
				return _SubDistrict;
			}
			set
			{
				
				if(_SubDistrict != value){
					_SubDistrict = value;
					EditSubDistrict = true;
				}
			}
		}

        private String _District ;
		[XmlIgnore]
		public bool EditDistrict = false;
		public String District
		{
			get
			{
				return _District;
			}
			set
			{
				
				if(_District != value){
					_District = value;
					EditDistrict = true;
				}
			}
		}

        private String _Province ;
		[XmlIgnore]
		public bool EditProvince = false;
		public String Province
		{
			get
			{
				return _Province;
			}
			set
			{
				
				if(_Province != value){
					_Province = value;
					EditProvince = true;
				}
			}
		}

        private String _PostCode ;
        private int _PostCode_Limit = 10;
		[XmlIgnore]
		public bool EditPostCode = false;
		public String PostCode
		{
			get
			{
				return _PostCode;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _PostCode_Limit)
					throw new Exception("String length longer then prescribed on PostCode property.");
				if(_PostCode != value){
					_PostCode = value;
					EditPostCode = true;
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

        private String _BranchCode ;
        private int _BranchCode_Limit = 60;
		[XmlIgnore]
		public bool EditBranchCode = false;
		public String BranchCode
		{
			get
			{
				return _BranchCode;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _BranchCode_Limit)
					throw new Exception("String length longer then prescribed on BranchCode property.");
				if(_BranchCode != value){
					_BranchCode = value;
					EditBranchCode = true;
				}
			}
		}

        private String _BranchName ;
        private int _BranchName_Limit = 60;
		[XmlIgnore]
		public bool EditBranchName = false;
		public String BranchName
		{
			get
			{
				return _BranchName;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _BranchName_Limit)
					throw new Exception("String length longer then prescribed on BranchName property.");
				if(_BranchName != value){
					_BranchName = value;
					EditBranchName = true;
				}
			}
		}

        private Nullable<Boolean> _IsDefault ;
		[XmlIgnore]
		public bool EditIsDefault = false;
		public Nullable<Boolean> IsDefault
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

	}
	public partial class AP_Master_Supplier_Address_Command
	{
		string TableName = "AP_Master_Supplier_Address";

		AP_Master_Supplier_Address _AP_Master_Supplier_Address = null;
		DBHelper _DBHelper = null;

		internal AP_Master_Supplier_Address_Command(AP_Master_Supplier_Address obj_AP_Master_Supplier_Address)
		{
			this._AP_Master_Supplier_Address = obj_AP_Master_Supplier_Address;
			this._DBHelper = new DBHelper();
		}

		internal AP_Master_Supplier_Address_Command(string ConnectionStr,AP_Master_Supplier_Address obj_AP_Master_Supplier_Address)
		{
			this._AP_Master_Supplier_Address = obj_AP_Master_Supplier_Address;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _AP_Master_Supplier_Address.GetType().GetProperties())
			{
				_AP_Master_Supplier_Address.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Supplier_Address, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_AP_Master_Supplier_Address,dr[ProInfo.Name],null);
					    //_AP_Master_Supplier_Address.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Supplier_Address, true);
                    }
				}
			}
		}

        public void Load(Int32 Id_Value,Int32 SupplierId_Value)
        {
            Load(_DBHelper, Id_Value,SupplierId_Value);
        }

		public void Load(DBHelper _DBHelper ,Int32 Id_Value,Int32 SupplierId_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@Id", Id_Value, (DbType)Enum.Parse(typeof(DbType), Id_Value.GetType().Name, true)));
			Parameter.Add(new DBParameter("@SupplierId", SupplierId_Value, (DbType)Enum.Parse(typeof(DbType), SupplierId_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Supplier_Address WHERE Id=@Id AND SupplierId=@SupplierId", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper ,IDbTransaction Trans ,Int32 Id_Value,Int32 SupplierId_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@Id", Id_Value, (DbType)Enum.Parse(typeof(DbType), Id_Value.GetType().Name, true)));
			Parameter.Add(new DBParameter("@SupplierId", SupplierId_Value, (DbType)Enum.Parse(typeof(DbType), SupplierId_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Supplier_Address WHERE Id=@Id AND SupplierId=@SupplierId", "AP_Master_Supplier_Address", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("AP_Master_Supplier_Address");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<AP_Master_Supplier_Address> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<AP_Master_Supplier_Address> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("AP_Master_Supplier_Address");
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
            List<AP_Master_Supplier_Address> Res = new List<AP_Master_Supplier_Address>();
            foreach(DataRow Dr in dt.Rows)
            {
                AP_Master_Supplier_Address Item = new AP_Master_Supplier_Address();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO AP_Master_Supplier_Address (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _AP_Master_Supplier_Address.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Supplier_Address.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Supplier_Address)  && ProInfo.Name != "Id")
				{
                    object Value = ProInfo.GetValue(_AP_Master_Supplier_Address, null);
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
            _AP_Master_Supplier_Address.Id = int.Parse(DBHelp.ExecuteScalar(SqlInsert, Parameter, DbTransaction).ToString());

		}

        public void Insert()
        {
            DBParameterCollection Parameter = null;
            string SqlInsert = GetInsertCommand(out Parameter);
            if (SqlInsert == "") return;
            _AP_Master_Supplier_Address.Id = int.Parse(_DBHelper.ExecuteScalar(SqlInsert, Parameter).ToString());

        }
            
        private string GetUpdateCommand(out DBParameterCollection Parameter)
        {
            Parameter = new DBParameterCollection();
			string ssql = "UPDATE AP_Master_Supplier_Address SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _AP_Master_Supplier_Address.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Supplier_Address.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Supplier_Address)  && ProInfo.Name != "Id" && ProInfo.Name != "SupplierId")
				{
                    object Value = ProInfo.GetValue(_AP_Master_Supplier_Address, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "Id" || ProInfo.Name == "SupplierId")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_AP_Master_Supplier_Address, null)));
				}
			}
			ssql += extSql;
			ssql += " WHERE Id=@Id AND SupplierId=@SupplierId";
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
            string ssql = "DELETE AP_Master_Supplier_Address WHERE Id=@Id AND SupplierId=@SupplierId";
            Parameter.Add(new DBParameter("@Id", _AP_Master_Supplier_Address.Id));
			Parameter.Add(new DBParameter("@SupplierId", _AP_Master_Supplier_Address.SupplierId));

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
