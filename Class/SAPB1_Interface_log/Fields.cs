/*
** Company Name     :  Trinity Solution Provider Co., Ltd. && ICON Framework Co.,Ltd.
** Contact          :  www.iconrem.com
** Product Name     :  Data Access framework(Class generator) (2012)
** Product by       :  Anupong Kwanpigul
** Modify by        :  Yuttapong Benjapornraksa
** Modify Date      :  2020-02-10 00:44:08
** Version          :  1.0.0.10
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
	[XmlRootAttribute("Fields", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class Fields
	{
		[XmlIgnore]
		[NonSerialized]
		public Fields_Command ExecCommand = null;
		public Fields()
		{
			ExecCommand = new Fields_Command(this);
		}
		public Fields(string ConnectionStr)
		{
			ExecCommand = new Fields_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new Fields_Command(ConnectionStr, this);
		}
		public Fields(string ConnectionStr, String L_FieldsID_Value)
		{
			ExecCommand = new Fields_Command(ConnectionStr, this);
			ExecCommand.Load(L_FieldsID_Value);
		}
        private String _L_FieldsID;
        public readonly bool _L_FieldsID_PKFlag = true;
        public readonly bool _L_FieldsID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_FieldsID = false;
		public String L_FieldsID
		{
			get
			{
				return _L_FieldsID;
			}
			set
			{
				if (_L_FieldsID != value)
                {
					_L_FieldsID = value;
					EditL_FieldsID = true;
				}
			}
		}

        private String _L_InterfaceLogID;
        public readonly bool _L_InterfaceLogID_PKFlag = false;
        public readonly bool _L_InterfaceLogID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_InterfaceLogID = false;
		public String L_InterfaceLogID
		{
			get
			{
				return _L_InterfaceLogID;
			}
			set
			{
				if (_L_InterfaceLogID != value)
                {
					_L_InterfaceLogID = value;
					EditL_InterfaceLogID = true;
				}
			}
		}

        private String _L_InterfaceLogDetailID;
        public readonly bool _L_InterfaceLogDetailID_PKFlag = false;
        public readonly bool _L_InterfaceLogDetailID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_InterfaceLogDetailID = false;
		public String L_InterfaceLogDetailID
		{
			get
			{
				return _L_InterfaceLogDetailID;
			}
			set
			{
				if (_L_InterfaceLogDetailID != value)
                {
					_L_InterfaceLogDetailID = value;
					EditL_InterfaceLogDetailID = true;
				}
			}
		}

        private String _L_FromRefID;
        public readonly bool _L_FromRefID_PKFlag = false;
        public readonly bool _L_FromRefID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_FromRefID = false;
		public String L_FromRefID
		{
			get
			{
				return _L_FromRefID;
			}
			set
			{
				if (_L_FromRefID != value)
                {
					_L_FromRefID = value;
					EditL_FromRefID = true;
				}
			}
		}

        private String _L_FromTable;
        public readonly bool _L_FromTable_PKFlag = false;
        public readonly bool _L_FromTable_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_FromTable = false;
		public String L_FromTable
		{
			get
			{
				return _L_FromTable;
			}
			set
			{
				if (_L_FromTable != value)
                {
					_L_FromTable = value;
					EditL_FromTable = true;
				}
			}
		}

        private String _L_Type;
        public readonly bool _L_Type_PKFlag = false;
        public readonly bool _L_Type_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_Type = false;
		public String L_Type
		{
			get
			{
				return _L_Type;
			}
			set
			{
				if (_L_Type != value)
                {
					_L_Type = value;
					EditL_Type = true;
				}
			}
		}

        private String _L_Name;
        public readonly bool _L_Name_PKFlag = false;
        public readonly bool _L_Name_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_Name = false;
		public String L_Name
		{
			get
			{
				return _L_Name;
			}
			set
			{
				if (_L_Name != value)
                {
					_L_Name = value;
					EditL_Name = true;
				}
			}
		}

        private String _ValidValue;
        public readonly bool _ValidValue_PKFlag = false;
        public readonly bool _ValidValue_IDTFlag = false;
		[XmlIgnore]
		public bool EditValidValue = false;
		public String ValidValue
		{
			get
			{
				return _ValidValue;
			}
			set
			{
				if (_ValidValue != value)
                {
					_ValidValue = value;
					EditValidValue = true;
				}
			}
		}

        private String _Value;
        public readonly bool _Value_PKFlag = false;
        public readonly bool _Value_IDTFlag = false;
		[XmlIgnore]
		public bool EditValue = false;
		public String Value
		{
			get
			{
				return _Value;
			}
			set
			{
				if (_Value != value)
                {
					_Value = value;
					EditValue = true;
				}
			}
		}

	}
	public partial class Fields_Command
	{
		string TableName = "Fields";

		Fields _Fields = null;
		DBHelper _DBHelper = null;

		internal Fields_Command(Fields obj_Fields)
		{
			this._Fields = obj_Fields;
			this._DBHelper = new DBHelper();
		}

		internal Fields_Command(string ConnectionStr, Fields obj_Fields)
		{
			this._Fields = obj_Fields;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _Fields.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Fields.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_Fields, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_Fields, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_FieldsID_Value)
        {
            Load(_DBHelper, L_FieldsID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_FieldsID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_FieldsID", L_FieldsID_Value, (DbType)Enum.Parse(typeof(DbType), L_FieldsID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Fields WHERE L_FieldsID=@L_FieldsID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_FieldsID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_FieldsID", L_FieldsID_Value, (DbType)Enum.Parse(typeof(DbType), L_FieldsID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Fields WHERE L_FieldsID=@L_FieldsID", "Fields", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("Fields");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<Fields> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<Fields> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("Fields");
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
            List<Fields> Res = new List<Fields>();
            foreach (DataRow Dr in dt.Rows)
            {
                Fields Item = new Fields();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Fields.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Fields.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_Fields);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Fields.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Fields.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_Fields);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _Fields.GetType();
                Type BaseType = MyType.BaseType;
                Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

                System.Text.StringBuilder OverLimitMsg = new System.Text.StringBuilder();
                System.Text.StringBuilder SQL = new System.Text.StringBuilder();
                SQL.AppendLine("SELECT");
                SQL.AppendLine("	c.name				AS ColumnName");
                SQL.AppendLine("	, t.name			AS TypeName");
                SQL.AppendLine("	, c.max_length		AS MaxLength");
                SQL.AppendLine("FROM sys.columns c");
                SQL.AppendLine("INNER JOIN sys.objects tb ON c.object_id = tb.object_id");
                SQL.AppendLine("INNER JOIN sys.types t ON c.user_type_id = t.user_type_id");
                SQL.AppendLine("WHERE tb.name = '" + TableType.Name + "'");
                System.Data.DataTable DT = DBHelp.ExecuteDataTable(SQL.ToString(), DbTransaction);

                foreach (PropertyInfo ProInfo in _Fields.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _Fields.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_Fields))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_Fields, null);
                            string ValueStr = Value == null ? "" : Value.ToString();
                            if (ValueStr.Length > MaxLength)
                            {
                                OverLimitMsg.AppendFormat("String length longer then prescribed on {1} property.({2}>{3}){0}", Environment.NewLine, ProInfo.Name, ValueStr.Length, MaxLength);
                            }
                        }
                    }
                }

                return OverLimitMsg.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
            
        private string GetInsertCommand(DBHelper DBHelp, out DBParameterCollection Parameter)
        {
            List<string> IdentityInsertKey_List = this.GetIdentityInsertKey();
            Type MyType = _Fields.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Fields.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Fields.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_Fields) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Fields, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
            }
            string SQL = "INSERT INTO " + TableType.Name + " (" + string.Join(",", Field_List.Select(x => x)) + ")";
            SQL += " VALUES(" + string.Join(",", Field_List.Select(x => "@" + x)) + ")";
            if (Field_List.Count == 0) return "";
            return SQL;
        }

        public void Insert(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                DBParameterCollection Parameter = null;
                string SqlInsert = GetInsertCommand(DBHelp, out Parameter);
                if (SqlInsert == "") return;
                DBHelp.ExecuteNonQuery(SqlInsert, Parameter, DbTransaction);
            }
            catch (Exception ex)
            {
                string LimitErrorMsg = GetOverLimitErrorMsg(DBHelp, DbTransaction);
                if (LimitErrorMsg != "") throw new Exception(ex.Message + Environment.NewLine + LimitErrorMsg);
                else throw new Exception(ex.Message);
            }
        }

        public void Insert()
        {
            try
            {
                DBParameterCollection Parameter = null;
                string SqlInsert = GetInsertCommand(_DBHelper, out Parameter);
                if (SqlInsert == "") return;
                _DBHelper.ExecuteNonQuery(SqlInsert, Parameter);
            }
            catch (Exception ex)
            {
                string LimitErrorMsg = GetOverLimitErrorMsg(_DBHelper, null);
                if (LimitErrorMsg != "") throw new Exception(ex.Message + Environment.NewLine + LimitErrorMsg);
                else throw new Exception(ex.Message);
            }
        }
            
        private string GetUpdateCommand(out DBParameterCollection Parameter)
        {
            List<string> PrimaryKey_List = this.GetPrimaryKey();
            Type MyType = _Fields.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Fields.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Fields.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_Fields) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Fields, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_Fields, null)));
				}
            }
            string SQL = "UPDATE " + TableType.Name + " SET " + string.Join(",", Field_List.Select(x => x + "=@" + x));
            SQL += " WHERE " + string.Join(" AND ", PrimaryKey_List.Select(x => x + "=@" + x));
            if (Field_List.Count == 0) return "";
            return SQL;
        }

        public void Update(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                DBParameterCollection Parameter = null;
                string SqlUpdate = GetUpdateCommand(out Parameter);
                if (SqlUpdate == "") return;
                DBHelp.ExecuteNonQuery(SqlUpdate, Parameter, DbTransaction);
            }
            catch (Exception ex)
            {
                string LimitErrorMsg = GetOverLimitErrorMsg(_DBHelper, null);
                if (LimitErrorMsg != "") throw new Exception(ex.Message + Environment.NewLine + LimitErrorMsg);
                else throw new Exception(ex.Message);
            }
        }

        public void Update()
        {
            try
            {
                DBParameterCollection Parameter = null;
                string SqlUpdate = GetUpdateCommand(out Parameter);
                if (SqlUpdate == "") return;
                _DBHelper.ExecuteNonQuery(SqlUpdate, Parameter);
            }
            catch (Exception ex)
            {
                string LimitErrorMsg = GetOverLimitErrorMsg(_DBHelper, null);
                if (LimitErrorMsg != "") throw new Exception(ex.Message + Environment.NewLine + LimitErrorMsg);
                else throw new Exception(ex.Message);
            }
        }
            
        private string GetDeleteCommand(out DBParameterCollection Parameter)
        {
            Parameter = new DBParameterCollection();
            string ssql = "DELETE Fields WHERE L_FieldsID=@L_FieldsID";
            Parameter.Add(new DBParameter("@L_FieldsID", _Fields.L_FieldsID));

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
