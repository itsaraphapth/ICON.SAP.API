/*
** Company Name     :  Trinity Solution Provider Co., Ltd.
** Contact          :  www.iconframework.co.th
** Product Name     :  Data Access framework(Class generator) (2012)
** Modify by        :  Anupong kwanpigul
** Modify Date      : 2019-05-14 13:23:37
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
	[XmlRootAttribute("AP_Master_Unit", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class AP_Master_Unit
	{
		[XmlIgnore]
		[NonSerialized]
		public AP_Master_Unit_Command ExecCommand = null;
		public AP_Master_Unit()
		{
			ExecCommand = new AP_Master_Unit_Command(this);
		}
		public AP_Master_Unit(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Unit_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Unit_Command(ConnectionStr, this);
		}
		public AP_Master_Unit(string ConnectionStr, String Code_Value)
		{
			ExecCommand = new AP_Master_Unit_Command(ConnectionStr, this);
			ExecCommand.Load(Code_Value);
		}
        private String _Code ;
        private int _Code_Limit = 20;
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
        private int _Name_Limit = 50;
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
	public partial class AP_Master_Unit_Command
	{
		string TableName = "AP_Master_Unit";

		AP_Master_Unit _AP_Master_Unit = null;
		DBHelper _DBHelper = null;

		internal AP_Master_Unit_Command(AP_Master_Unit obj_AP_Master_Unit)
		{
			this._AP_Master_Unit = obj_AP_Master_Unit;
			this._DBHelper = new DBHelper();
		}

		internal AP_Master_Unit_Command(string ConnectionStr,AP_Master_Unit obj_AP_Master_Unit)
		{
			this._AP_Master_Unit = obj_AP_Master_Unit;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _AP_Master_Unit.GetType().GetProperties())
			{
				_AP_Master_Unit.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Unit, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_AP_Master_Unit,dr[ProInfo.Name],null);
					    //_AP_Master_Unit.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Unit, true);
                    }
				}
			}
		}

        public void Load(String Code_Value)
        {
            Load(_DBHelper, Code_Value);
        }

		public void Load(DBHelper _DBHelper ,String Code_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@Code", Code_Value, (DbType)Enum.Parse(typeof(DbType), Code_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Unit WHERE Code=@Code", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper ,IDbTransaction Trans ,String Code_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@Code", Code_Value, (DbType)Enum.Parse(typeof(DbType), Code_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Unit WHERE Code=@Code", "AP_Master_Unit", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("AP_Master_Unit");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<AP_Master_Unit> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<AP_Master_Unit> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("AP_Master_Unit");
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
            List<AP_Master_Unit> Res = new List<AP_Master_Unit>();
            foreach(DataRow Dr in dt.Rows)
            {
                AP_Master_Unit Item = new AP_Master_Unit();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO AP_Master_Unit (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _AP_Master_Unit.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Unit.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Unit) )
				{
                    object Value = ProInfo.GetValue(_AP_Master_Unit, null);
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
			string ssql = "UPDATE AP_Master_Unit SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _AP_Master_Unit.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Unit.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Unit)  && ProInfo.Name != "Code")
				{
                    object Value = ProInfo.GetValue(_AP_Master_Unit, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "Code")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_AP_Master_Unit, null)));
				}
			}
			ssql += extSql;
			ssql += " WHERE Code=@Code";
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
            string ssql = "DELETE AP_Master_Unit WHERE Code=@Code";
            Parameter.Add(new DBParameter("@Code", _AP_Master_Unit.Code));

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
