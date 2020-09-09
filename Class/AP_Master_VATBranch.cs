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
	[XmlRootAttribute("AP_Master_VATBranch", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class AP_Master_VATBranch
	{
		[XmlIgnore]
		[NonSerialized]
		public AP_Master_VATBranch_Command ExecCommand = null;
		public AP_Master_VATBranch()
		{
			ExecCommand = new AP_Master_VATBranch_Command(this);
		}
		public AP_Master_VATBranch(string ConnectionStr)
		{
			ExecCommand = new AP_Master_VATBranch_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new AP_Master_VATBranch_Command(ConnectionStr, this);
		}
		public AP_Master_VATBranch(string ConnectionStr, String VatBranch_Value,String CompanyId_Value)
		{
			ExecCommand = new AP_Master_VATBranch_Command(ConnectionStr, this);
			ExecCommand.Load(VatBranch_Value,CompanyId_Value);
		}
        private String _VatBranch ;
        private int _VatBranch_Limit = 50;
		[XmlIgnore]
		public bool EditVatBranch = false;
		public String VatBranch
		{
			get
			{
				return _VatBranch;
			}
			set
			{
				if((string.IsNullOrEmpty(value)? 0:value.Length) > _VatBranch_Limit)
					throw new Exception("String length longer then prescribed on VatBranch property.");
				if(_VatBranch != value){
					_VatBranch = value;
					EditVatBranch = true;
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
	public partial class AP_Master_VATBranch_Command
	{
		string TableName = "AP_Master_VATBranch";

		AP_Master_VATBranch _AP_Master_VATBranch = null;
		DBHelper _DBHelper = null;

		internal AP_Master_VATBranch_Command(AP_Master_VATBranch obj_AP_Master_VATBranch)
		{
			this._AP_Master_VATBranch = obj_AP_Master_VATBranch;
			this._DBHelper = new DBHelper();
		}

		internal AP_Master_VATBranch_Command(string ConnectionStr,AP_Master_VATBranch obj_AP_Master_VATBranch)
		{
			this._AP_Master_VATBranch = obj_AP_Master_VATBranch;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _AP_Master_VATBranch.GetType().GetProperties())
			{
				_AP_Master_VATBranch.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_VATBranch, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_AP_Master_VATBranch,dr[ProInfo.Name],null);
					    //_AP_Master_VATBranch.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_VATBranch, true);
                    }
				}
			}
		}

        public void Load(String VatBranch_Value,String CompanyId_Value)
        {
            Load(_DBHelper, VatBranch_Value,CompanyId_Value);
        }

		public void Load(DBHelper _DBHelper ,String VatBranch_Value,String CompanyId_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@VatBranch", VatBranch_Value, (DbType)Enum.Parse(typeof(DbType), VatBranch_Value.GetType().Name, true)));
			Parameter.Add(new DBParameter("@CompanyId", CompanyId_Value, (DbType)Enum.Parse(typeof(DbType), CompanyId_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_VATBranch WHERE VatBranch=@VatBranch AND CompanyId=@CompanyId", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper ,IDbTransaction Trans ,String VatBranch_Value,String CompanyId_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@VatBranch", VatBranch_Value, (DbType)Enum.Parse(typeof(DbType), VatBranch_Value.GetType().Name, true)));
			Parameter.Add(new DBParameter("@CompanyId", CompanyId_Value, (DbType)Enum.Parse(typeof(DbType), CompanyId_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_VATBranch WHERE VatBranch=@VatBranch AND CompanyId=@CompanyId", "AP_Master_VATBranch", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("AP_Master_VATBranch");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<AP_Master_VATBranch> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<AP_Master_VATBranch> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("AP_Master_VATBranch");
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
            List<AP_Master_VATBranch> Res = new List<AP_Master_VATBranch>();
            foreach(DataRow Dr in dt.Rows)
            {
                AP_Master_VATBranch Item = new AP_Master_VATBranch();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO AP_Master_VATBranch (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _AP_Master_VATBranch.GetType().GetProperties())
			{
				if ((bool)_AP_Master_VATBranch.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_VATBranch) )
				{
                    object Value = ProInfo.GetValue(_AP_Master_VATBranch, null);
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
			string ssql = "UPDATE AP_Master_VATBranch SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _AP_Master_VATBranch.GetType().GetProperties())
			{
				if ((bool)_AP_Master_VATBranch.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_VATBranch)  && ProInfo.Name != "VatBranch" && ProInfo.Name != "CompanyId")
				{
                    object Value = ProInfo.GetValue(_AP_Master_VATBranch, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "VatBranch" || ProInfo.Name == "CompanyId")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_AP_Master_VATBranch, null)));
				}
			}
			ssql += extSql;
			ssql += " WHERE VatBranch=@VatBranch AND CompanyId=@CompanyId";
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
            string ssql = "DELETE AP_Master_VATBranch WHERE VatBranch=@VatBranch AND CompanyId=@CompanyId";
            Parameter.Add(new DBParameter("@VatBranch", _AP_Master_VATBranch.VatBranch));
			Parameter.Add(new DBParameter("@CompanyId", _AP_Master_VATBranch.CompanyId));

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
