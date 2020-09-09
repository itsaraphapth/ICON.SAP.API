/*
** Company Name     :  Trinity Solution Provider Co., Ltd.
** Contact          :  www.iconframework.co.th
** Product Name     :  Data Access framework(Class generator) (2012)
** Modify by        :  Anupong kwanpigul
** Modify Date      : 2019-05-17 09:45:15
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
	[XmlRootAttribute("HR_Master_User", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class HR_Master_User
	{
		[XmlIgnore]
		[NonSerialized]
		public HR_Master_User_Command ExecCommand = null;
		public HR_Master_User()
		{
			ExecCommand = new HR_Master_User_Command(this);
		}
		public HR_Master_User(string ConnectionStr)
		{
			ExecCommand = new HR_Master_User_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new HR_Master_User_Command(ConnectionStr, this);
		}
		public HR_Master_User(string ConnectionStr, String Username_Value)
		{
			ExecCommand = new HR_Master_User_Command(ConnectionStr, this);
			ExecCommand.Load(Username_Value);
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

        private String _Username ;
        private int _Username_Limit = 100;
		[XmlIgnore]
		public bool EditUsername = false;
		public String Username
		{
			get
			{
				return _Username;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Username_Limit)
					throw new Exception("String length longer then prescribed on Username property.");
				if(_Username != value){
					_Username = value;
					EditUsername = true;
				}
			}
		}

        private String _Password ;
        private int _Password_Limit = 200;
		[XmlIgnore]
		public bool EditPassword = false;
		public String Password
		{
			get
			{
				return _Password;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Password_Limit)
					throw new Exception("String length longer then prescribed on Password property.");
				if(_Password != value){
					_Password = value;
					EditPassword = true;
				}
			}
		}

        private String _FirstName ;
        private int _FirstName_Limit = 100;
		[XmlIgnore]
		public bool EditFirstName = false;
		public String FirstName
		{
			get
			{
				return _FirstName;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _FirstName_Limit)
					throw new Exception("String length longer then prescribed on FirstName property.");
				if(_FirstName != value){
					_FirstName = value;
					EditFirstName = true;
				}
			}
		}

        private String _LastName ;
        private int _LastName_Limit = 100;
		[XmlIgnore]
		public bool EditLastName = false;
		public String LastName
		{
			get
			{
				return _LastName;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _LastName_Limit)
					throw new Exception("String length longer then prescribed on LastName property.");
				if(_LastName != value){
					_LastName = value;
					EditLastName = true;
				}
			}
		}

        private String _Email ;
        private int _Email_Limit = 100;
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

        private Nullable<Int32> _PositionId ;
		[XmlIgnore]
		public bool EditPositionId = false;
		public Nullable<Int32> PositionId
		{
			get
			{
				return _PositionId;
			}
			set
			{
				
				if(_PositionId != value){
					_PositionId = value;
					EditPositionId = true;
				}
			}
		}

        private Nullable<Int32> _DepartmentId ;
		[XmlIgnore]
		public bool EditDepartmentId = false;
		public Nullable<Int32> DepartmentId
		{
			get
			{
				return _DepartmentId;
			}
			set
			{
				
				if(_DepartmentId != value){
					_DepartmentId = value;
					EditDepartmentId = true;
				}
			}
		}

        private Nullable<DateTime> _BirthDate ;
		[XmlIgnore]
		public bool EditBirthDate = false;
		public Nullable<DateTime> BirthDate
		{
			get
			{
				return _BirthDate;
			}
			set
			{
				
				if(_BirthDate != value){
					_BirthDate = value;
					EditBirthDate = true;
				}
			}
		}

        private String _Telephone ;
        private int _Telephone_Limit = 100;
		[XmlIgnore]
		public bool EditTelephone = false;
		public String Telephone
		{
			get
			{
				return _Telephone;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _Telephone_Limit)
					throw new Exception("String length longer then prescribed on Telephone property.");
				if(_Telephone != value){
					_Telephone = value;
					EditTelephone = true;
				}
			}
		}

        private String _Status ;
        private int _Status_Limit = 100;
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

        private String _CostCenter ;
        private int _CostCenter_Limit = 50;
		[XmlIgnore]
		public bool EditCostCenter = false;
		public String CostCenter
		{
			get
			{
				return _CostCenter;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _CostCenter_Limit)
					throw new Exception("String length longer then prescribed on CostCenter property.");
				if(_CostCenter != value){
					_CostCenter = value;
					EditCostCenter = true;
				}
			}
		}

	}
	public partial class HR_Master_User_Command
	{
		string TableName = "HR_Master_User";

		HR_Master_User _HR_Master_User = null;
		DBHelper _DBHelper = null;

		internal HR_Master_User_Command(HR_Master_User obj_HR_Master_User)
		{
			this._HR_Master_User = obj_HR_Master_User;
			this._DBHelper = new DBHelper();
		}

		internal HR_Master_User_Command(string ConnectionStr,HR_Master_User obj_HR_Master_User)
		{
			this._HR_Master_User = obj_HR_Master_User;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _HR_Master_User.GetType().GetProperties())
			{
				_HR_Master_User.GetType().GetField("Edit" + ProInfo.Name).SetValue(_HR_Master_User, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_HR_Master_User,dr[ProInfo.Name],null);
					    //_HR_Master_User.GetType().GetField("Edit" + ProInfo.Name).SetValue(_HR_Master_User, true);
                    }
				}
			}
		}

        public void Load(String Username_Value)
        {
            Load(_DBHelper, Username_Value);
        }

		public void Load(DBHelper _DBHelper ,String Username_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@Username", Username_Value, (DbType)Enum.Parse(typeof(DbType), Username_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM HR_Master_User WHERE Username=@Username", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper ,IDbTransaction Trans ,String Username_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@Username", Username_Value, (DbType)Enum.Parse(typeof(DbType), Username_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM HR_Master_User WHERE Username=@Username", "HR_Master_User", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("HR_Master_User");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<HR_Master_User> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<HR_Master_User> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("HR_Master_User");
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
            List<HR_Master_User> Res = new List<HR_Master_User>();
            foreach(DataRow Dr in dt.Rows)
            {
                HR_Master_User Item = new HR_Master_User();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO HR_Master_User (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _HR_Master_User.GetType().GetProperties())
			{
				if ((bool)_HR_Master_User.GetType().GetField("Edit" + ProInfo.Name).GetValue(_HR_Master_User) )
				{
                    object Value = ProInfo.GetValue(_HR_Master_User, null);
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
			string ssql = "UPDATE HR_Master_User SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _HR_Master_User.GetType().GetProperties())
			{
				if ((bool)_HR_Master_User.GetType().GetField("Edit" + ProInfo.Name).GetValue(_HR_Master_User)  && ProInfo.Name != "Username")
				{
                    object Value = ProInfo.GetValue(_HR_Master_User, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "Username")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_HR_Master_User, null)));
				}
			}
			ssql += extSql;
			ssql += " WHERE Username=@Username";
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
            string ssql = "DELETE HR_Master_User WHERE Username=@Username";
            Parameter.Add(new DBParameter("@Username", _HR_Master_User.Username));

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
