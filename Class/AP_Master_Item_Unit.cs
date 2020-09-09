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
	[XmlRootAttribute("AP_Master_Item_Unit", Namespace = "http://www.iconframework.com",IsNullable = false)]
	public class AP_Master_Item_Unit
	{
		[XmlIgnore]
		[NonSerialized]
		public AP_Master_Item_Unit_Command ExecCommand = null;
		public AP_Master_Item_Unit()
		{
			ExecCommand = new AP_Master_Item_Unit_Command(this);
		}
		public AP_Master_Item_Unit(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Item_Unit_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new AP_Master_Item_Unit_Command(ConnectionStr, this);
		}
		public AP_Master_Item_Unit(string ConnectionStr, Int32 ItemId_Value,String UnitCode_Value)
		{
			ExecCommand = new AP_Master_Item_Unit_Command(ConnectionStr, this);
			ExecCommand.Load(ItemId_Value,UnitCode_Value);
		}
        private Int32 _ItemId ;
		[XmlIgnore]
		public bool EditItemId = false;
		public Int32 ItemId
		{
			get
			{
				return _ItemId;
			}
			set
			{
				
				if(_ItemId != value){
					_ItemId = value;
					EditItemId = true;
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

	}
	public partial class AP_Master_Item_Unit_Command
	{
		string TableName = "AP_Master_Item_Unit";

		AP_Master_Item_Unit _AP_Master_Item_Unit = null;
		DBHelper _DBHelper = null;

		internal AP_Master_Item_Unit_Command(AP_Master_Item_Unit obj_AP_Master_Item_Unit)
		{
			this._AP_Master_Item_Unit = obj_AP_Master_Item_Unit;
			this._DBHelper = new DBHelper();
		}

		internal AP_Master_Item_Unit_Command(string ConnectionStr,AP_Master_Item_Unit obj_AP_Master_Item_Unit)
		{
			this._AP_Master_Item_Unit = obj_AP_Master_Item_Unit;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach ( PropertyInfo ProInfo in _AP_Master_Item_Unit.GetType().GetProperties())
			{
				_AP_Master_Item_Unit.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Item_Unit, false);
				if(dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_AP_Master_Item_Unit,dr[ProInfo.Name],null);
					    //_AP_Master_Item_Unit.GetType().GetField("Edit" + ProInfo.Name).SetValue(_AP_Master_Item_Unit, true);
                    }
				}
			}
		}

        public void Load(Int32 ItemId_Value,String UnitCode_Value)
        {
            Load(_DBHelper, ItemId_Value,UnitCode_Value);
        }

		public void Load(DBHelper _DBHelper ,Int32 ItemId_Value,String UnitCode_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@ItemId", ItemId_Value, (DbType)Enum.Parse(typeof(DbType), ItemId_Value.GetType().Name, true)));
			Parameter.Add(new DBParameter("@UnitCode", UnitCode_Value, (DbType)Enum.Parse(typeof(DbType), UnitCode_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Item_Unit WHERE ItemId=@ItemId AND UnitCode=@UnitCode", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper ,IDbTransaction Trans ,Int32 ItemId_Value,String UnitCode_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@ItemId", ItemId_Value, (DbType)Enum.Parse(typeof(DbType), ItemId_Value.GetType().Name, true)));
			Parameter.Add(new DBParameter("@UnitCode", UnitCode_Value, (DbType)Enum.Parse(typeof(DbType), UnitCode_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM AP_Master_Item_Unit WHERE ItemId=@ItemId AND UnitCode=@UnitCode", "AP_Master_Item_Unit", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("AP_Master_Item_Unit");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<AP_Master_Item_Unit> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<AP_Master_Item_Unit> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("AP_Master_Item_Unit");
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
            List<AP_Master_Item_Unit> Res = new List<AP_Master_Item_Unit>();
            foreach(DataRow Dr in dt.Rows)
            {
                AP_Master_Item_Unit Item = new AP_Master_Item_Unit();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        private string GetInsertCommand(out DBParameterCollection Parameter)
        {
            StringBuilder SQL = new StringBuilder();
			string ssql = "INSERT INTO AP_Master_Item_Unit (";
			string extColumn = "";
			string extParameter = "";
			Parameter = new DBParameterCollection();
			foreach (PropertyInfo ProInfo in _AP_Master_Item_Unit.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Item_Unit.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Item_Unit) )
				{
                    object Value = ProInfo.GetValue(_AP_Master_Item_Unit, null);
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
			string ssql = "UPDATE AP_Master_Item_Unit SET ";
			string extSql = "";
			foreach (PropertyInfo ProInfo in _AP_Master_Item_Unit.GetType().GetProperties())
			{
				if ((bool)_AP_Master_Item_Unit.GetType().GetField("Edit" + ProInfo.Name).GetValue(_AP_Master_Item_Unit)  && ProInfo.Name != "ItemId" && ProInfo.Name != "UnitCode")
				{
                    object Value = ProInfo.GetValue(_AP_Master_Item_Unit, null);
                    if (Value == null) Value = DBNull.Value;
					if (extSql != "") extSql += ",";
					extSql += ProInfo.Name + "=@" + ProInfo.Name;
                    string PropType = ProInfo.PropertyType.Name;
                    if (PropType.IndexOf("Nullable") >= 0) PropType = ProInfo.PropertyType.GetGenericArguments()[0].Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
				    
                }
				if (ProInfo.Name == "ItemId" || ProInfo.Name == "UnitCode")
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_AP_Master_Item_Unit, null)));
				}
			}
			ssql += extSql;
			ssql += " WHERE ItemId=@ItemId AND UnitCode=@UnitCode";
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
            string ssql = "DELETE AP_Master_Item_Unit WHERE ItemId=@ItemId AND UnitCode=@UnitCode";
            Parameter.Add(new DBParameter("@ItemId", _AP_Master_Item_Unit.ItemId));
			Parameter.Add(new DBParameter("@UnitCode", _AP_Master_Item_Unit.UnitCode));

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
