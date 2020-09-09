/*
** Company Name     :  Trinity Solution Provider Co., Ltd.
** Contact          :  www.iconframework.co.th
** Product Name     :  Data Access framework(Class generator) (2012)
** Modify by        :  Anupong kwanpigul
** Modify Date      : 2019-05-17 09:45:14
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
	[XmlRootAttribute("HR_Master_Department", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class HR_Master_Department
	{
		[XmlIgnore]
		[NonSerialized]
		public HR_Master_Department_Command ExecCommand = null;
		public HR_Master_Department()
		{
			ExecCommand = new HR_Master_Department_Command(this);
		}
		public HR_Master_Department(string ConnectionStr)
		{
			ExecCommand = new HR_Master_Department_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new HR_Master_Department_Command(ConnectionStr, this);
		}
		public HR_Master_Department(string ConnectionStr, Int32 Id_Value)
		{
			ExecCommand = new HR_Master_Department_Command(ConnectionStr, this);
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

        private String _Name ;
        private int _Name_Limit = 100;
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
	public partial class HR_Master_Department_Command
	{
		string TableName = "HR_Master_Department";

		HR_Master_Department _HR_Master_Department = null;
		DBHelper _DBHelper = null;

		internal HR_Master_Department_Command(HR_Master_Department obj_HR_Master_Department)
		{
			this._HR_Master_Department = obj_HR_Master_Department;
			this._DBHelper = new DBHelper();
		}

		internal HR_Master_Department_Command(string ConnectionStr,HR_Master_Department obj_HR_Master_Department)
		{
			this._HR_Master_Department = obj_HR_Master_Department;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _HR_Master_Department.GetType().GetProperties())
			{
				_HR_Master_Department.GetType().GetField("Edit" + ProInfo.Name).SetValue(_HR_Master_Department, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_HR_Master_Department,dr[ProInfo.Name],null);
					    //_HR_Master_Department.GetType().GetField("Edit" + ProInfo.Name).SetValue(_HR_Master_Department, true);
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

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM HR_Master_Department WHERE Id=@Id", Parameter);
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

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM HR_Master_Department WHERE Id=@Id", "HR_Master_Department", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("HR_Master_Department");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<HR_Master_Department> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<HR_Master_Department> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("HR_Master_Department");
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
            List<HR_Master_Department> Res = new List<HR_Master_Department>();
            foreach(DataRow Dr in dt.Rows)
            {
                HR_Master_Department Item = new HR_Master_Department();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO HR_Master_Department (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _HR_Master_Department.GetType().GetProperties())
			{
				if ((bool)_HR_Master_Department.GetType().GetField("Edit" + ProInfo.Name).GetValue(_HR_Master_Department)  && ProInfo.Name != "Id")
				{
                    object Value = ProInfo.GetValue(_HR_Master_Department, null);
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
            _HR_Master_Department.Id = int.Parse(DBHelp.ExecuteScalar(SqlInsert, Parameter, DbTransaction).ToString());

		}

        public void Insert()
        {
            DBParameterCollection Parameter = null;
            string SqlInsert = GetInsertCommand(out Parameter);
            if (SqlInsert == "") return;
            _HR_Master_Department.Id = int.Parse(_DBHelper.ExecuteScalar(SqlInsert, Parameter).ToString());

        }
            
        private string GetUpdateCommand(out DBParameterCollection Parameter)
        {
            Parameter = new DBParameterCollection();
			string ssql = "UPDATE HR_Master_Department SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _HR_Master_Department.GetType().GetProperties())
			{
				if ((bool)_HR_Master_Department.GetType().GetField("Edit" + ProInfo.Name).GetValue(_HR_Master_Department)  && ProInfo.Name != "Id")
				{
                    object Value = ProInfo.GetValue(_HR_Master_Department, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "Id")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_HR_Master_Department, null)));
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
            string ssql = "DELETE HR_Master_Department WHERE Id=@Id";
            Parameter.Add(new DBParameter("@Id", _HR_Master_Department.Id));

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
