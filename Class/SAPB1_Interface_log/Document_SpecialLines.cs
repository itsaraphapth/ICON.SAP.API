/*
** Company Name     :  Trinity Solution Provider Co., Ltd. && ICON Framework Co.,Ltd.
** Contact          :  www.iconrem.com
** Product Name     :  Data Access framework(Class generator) (2012)
** Product by       :  Anupong Kwanpigul
** Modify by        :  Yuttapong Benjapornraksa
** Modify Date      :  2020-02-03 15:54:27
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
	[XmlRootAttribute("Document_SpecialLines", Namespace = "http://www.iconrem.com", IsNullable = false)]
	public class Document_SpecialLines
	{
		[XmlIgnore]
		[NonSerialized]
		public Document_SpecialLines_Command ExecCommand = null;
		public Document_SpecialLines()
		{
			ExecCommand = new Document_SpecialLines_Command(this);
		}
		public Document_SpecialLines(string ConnectionStr)
		{
			ExecCommand = new Document_SpecialLines_Command(ConnectionStr, this);
		}
		public void InitCommand(string ConnectionStr)
		{
			ExecCommand = new Document_SpecialLines_Command(ConnectionStr, this);
		}
		public Document_SpecialLines(string ConnectionStr, String L_Document_SpecialLinesID_Value)
		{
			ExecCommand = new Document_SpecialLines_Command(ConnectionStr, this);
			ExecCommand.Load(L_Document_SpecialLinesID_Value);
		}
        private String _L_Document_SpecialLinesID;
        public readonly bool _L_Document_SpecialLinesID_PKFlag = true;
        public readonly bool _L_Document_SpecialLinesID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_Document_SpecialLinesID = false;
		public String L_Document_SpecialLinesID
		{
			get
			{
				return _L_Document_SpecialLinesID;
			}
			set
			{
				if (_L_Document_SpecialLinesID != value)
                {
					_L_Document_SpecialLinesID = value;
					EditL_Document_SpecialLinesID = true;
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

        private String _L_DocumentsID;
        public readonly bool _L_DocumentsID_PKFlag = false;
        public readonly bool _L_DocumentsID_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_DocumentsID = false;
		public String L_DocumentsID
		{
			get
			{
				return _L_DocumentsID;
			}
			set
			{
				if (_L_DocumentsID != value)
                {
					_L_DocumentsID = value;
					EditL_DocumentsID = true;
				}
			}
		}

        private Nullable<Int32> _L_LineNum;
        public readonly bool _L_LineNum_PKFlag = false;
        public readonly bool _L_LineNum_IDTFlag = false;
		[XmlIgnore]
		public bool EditL_LineNum = false;
		public Nullable<Int32> L_LineNum
		{
			get
			{
				return _L_LineNum;
			}
			set
			{
				if (_L_LineNum != value)
                {
					_L_LineNum = value;
					EditL_LineNum = true;
				}
			}
		}

        private Nullable<Int32> _AfterLineNumber;
        public readonly bool _AfterLineNumber_PKFlag = false;
        public readonly bool _AfterLineNumber_IDTFlag = false;
		[XmlIgnore]
		public bool EditAfterLineNumber = false;
		public Nullable<Int32> AfterLineNumber
		{
			get
			{
				return _AfterLineNumber;
			}
			set
			{
				if (_AfterLineNumber != value)
                {
					_AfterLineNumber = value;
					EditAfterLineNumber = true;
				}
			}
		}

        private String _LineText;
        public readonly bool _LineText_PKFlag = false;
        public readonly bool _LineText_IDTFlag = false;
		[XmlIgnore]
		public bool EditLineText = false;
		public String LineText
		{
			get
			{
				return _LineText;
			}
			set
			{
				if (_LineText != value)
                {
					_LineText = value;
					EditLineText = true;
				}
			}
		}

        private String _LineType;
        public readonly bool _LineType_PKFlag = false;
        public readonly bool _LineType_IDTFlag = false;
		[XmlIgnore]
		public bool EditLineType = false;
		public String LineType
		{
			get
			{
				return _LineType;
			}
			set
			{
				if (_LineType != value)
                {
					_LineType = value;
					EditLineType = true;
				}
			}
		}

	}
	public partial class Document_SpecialLines_Command
	{
		string TableName = "Document_SpecialLines";

		Document_SpecialLines _Document_SpecialLines = null;
		DBHelper _DBHelper = null;

		internal Document_SpecialLines_Command(Document_SpecialLines obj_Document_SpecialLines)
		{
			this._Document_SpecialLines = obj_Document_SpecialLines;
			this._DBHelper = new DBHelper();
		}

		internal Document_SpecialLines_Command(string ConnectionStr, Document_SpecialLines obj_Document_SpecialLines)
		{
			this._Document_SpecialLines = obj_Document_SpecialLines;
			this._DBHelper = new DBHelper(ConnectionStr, null);
		}

		public void Load(DataRow dr)
		{
			foreach (PropertyInfo ProInfo in _Document_SpecialLines.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Document_SpecialLines.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null) FieldInfo.SetValue(_Document_SpecialLines, false);
				if (dr.Table.Columns.Contains(ProInfo.Name))
				{
                    if (!dr.IsNull(ProInfo.Name))
                    {
					    ProInfo.SetValue(_Document_SpecialLines, dr[ProInfo.Name], null);
                    }
				}
			}
		}

        public void Load(String L_Document_SpecialLinesID_Value)
        {
            Load(_DBHelper, L_Document_SpecialLinesID_Value);
        }

		public void Load(DBHelper _DBHelper, String L_Document_SpecialLinesID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Document_SpecialLinesID", L_Document_SpecialLinesID_Value, (DbType)Enum.Parse(typeof(DbType), L_Document_SpecialLinesID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Document_SpecialLines WHERE L_Document_SpecialLinesID=@L_Document_SpecialLinesID", Parameter);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public void Load(DBHelper _DBHelper, IDbTransaction Trans, String L_Document_SpecialLinesID_Value)
		{
			DataTable dt = new DataTable();
			DBParameterCollection Parameter = new DBParameterCollection();
            Parameter.Add(new DBParameter("@L_Document_SpecialLinesID", L_Document_SpecialLinesID_Value, (DbType)Enum.Parse(typeof(DbType), L_Document_SpecialLinesID_Value.GetType().Name, true)));

            dt = _DBHelper.ExecuteDataTable("SELECT * FROM Document_SpecialLines WHERE L_Document_SpecialLinesID=@L_Document_SpecialLinesID", "Document_SpecialLines", Parameter, CommandType.Text, Trans);
			if (dt.Rows.Count > 0)
			{
				Load(dt.Rows[0]);
			}
		}

        public DataTable LoadByQueryBuilder(SelectQueryBuilder SelectQuery)
        {
            if (SelectQuery.SelectedTables.Length == 0) SelectQuery.SelectFromTable("Document_SpecialLines");
            return _DBHelper.ExecuteDataTable(SelectQuery.BuildQuery(), CommandType.Text);
        }
        
        public List<Document_SpecialLines> SelectAll()
        {
            return SelectAll(0, 0, null, null);
        }

        public List<Document_SpecialLines> SelectAll(int start, int length, List<WhereClause> Where, List<OrderByClause> OrderBy)
        {
            SelectQueryBuilder Sqb = new SelectQueryBuilder();
            Sqb.SelectAllColumns();
            if (start > 0 || length > 0)
                if (start == 0) Sqb.TopRecords = length; else Sqb.LimitRecords(start, length);
            Sqb.SelectFromTable("Document_SpecialLines");
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
            List<Document_SpecialLines> Res = new List<Document_SpecialLines>();
            foreach (DataRow Dr in dt.Rows)
            {
                Document_SpecialLines Item = new Document_SpecialLines();
                Item.ExecCommand.Load(Dr);
                Res.Add(Item);
            }

            return Res;
        }

        public List<string> GetPrimaryKey()
        {
            List<string> PrimaryKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Document_SpecialLines.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Document_SpecialLines.GetType().GetField("_" + ProInfo.Name + "_PKFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsPrimaryKey = (bool)FieldInfo.GetValue(_Document_SpecialLines);
                    if (IsPrimaryKey) PrimaryKey_List.Add(ProInfo.Name);
                }
            }
            return PrimaryKey_List;
        }
            
        public List<string> GetIdentityInsertKey()
        {
            List<string> IdentityInsertKey_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Document_SpecialLines.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Document_SpecialLines.GetType().GetField("_" + ProInfo.Name + "_IDTFlag", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (FieldInfo != null)
                {
                    bool IsIdentityInsertKey = (bool)FieldInfo.GetValue(_Document_SpecialLines);
                    if (IsIdentityInsertKey) IdentityInsertKey_List.Add(ProInfo.Name);
                }
            }
            return IdentityInsertKey_List;
        }
            
        private string GetOverLimitErrorMsg(DBHelper DBHelp, IDbTransaction DbTransaction)
        {
            try
            {
                Type MyType = _Document_SpecialLines.GetType();
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

                foreach (PropertyInfo ProInfo in _Document_SpecialLines.GetType().GetProperties())
                {
                    FieldInfo FieldInfo = _Document_SpecialLines.GetType().GetField("Edit" + ProInfo.Name);
                    if (FieldInfo != null && (bool)FieldInfo.GetValue(_Document_SpecialLines))
                    {
                        DataRow[] DR_List = DT.Select("ColumnName = '" + ProInfo.Name + "' AND TypeName LIKE '*char*' AND MaxLength <> -1");
                        DataRow DR = DR_List.FirstOrDefault();
                        if (DR != null)
                        {
                            int MaxLength = Convert.ToInt32(DR["MaxLength"]) / 2;
                            object Value = ProInfo.GetValue(_Document_SpecialLines, null);
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
            Type MyType = _Document_SpecialLines.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Document_SpecialLines.GetType().GetProperties())
            {
                FieldInfo FieldInfo = _Document_SpecialLines.GetType().GetField("Edit" + ProInfo.Name);
                if (FieldInfo != null && (bool)FieldInfo.GetValue(_Document_SpecialLines) && IdentityInsertKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Document_SpecialLines, null);
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
            Type MyType = _Document_SpecialLines.GetType();
            Type BaseType = MyType.BaseType;
            Type TableType = typeof(System.Object) == BaseType ? MyType : BaseType;

            Parameter = new DBParameterCollection();
            List<string> Field_List = new List<string>();
            foreach (PropertyInfo ProInfo in _Document_SpecialLines.GetType().GetProperties())
			{
                FieldInfo FieldInfo = _Document_SpecialLines.GetType().GetField("Edit" + ProInfo.Name);
				if (FieldInfo != null && (bool)FieldInfo.GetValue(_Document_SpecialLines) && PrimaryKey_List.IndexOf(ProInfo.Name) < 0)
                {
                    Field_List.Add(ProInfo.Name);
                    object Value = ProInfo.GetValue(_Document_SpecialLines, null);
                    if (Value == null) Value = DBNull.Value;

                    string PropType = ProInfo.PropertyType.IsGenericType && ProInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) 
                        ? Nullable.GetUnderlyingType(ProInfo.PropertyType).Name : ProInfo.PropertyType.Name;
                    Parameter.Add(new DBParameter("@" + ProInfo.Name, Value, (DbType)Enum.Parse(typeof(DbType), PropType, true)));
                }
				if (PrimaryKey_List.IndexOf(ProInfo.Name) >= 0)
				{
					Parameter.Add(new DBParameter("@" + ProInfo.Name, ProInfo.GetValue(_Document_SpecialLines, null)));
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
            string ssql = "DELETE Document_SpecialLines WHERE L_Document_SpecialLinesID=@L_Document_SpecialLinesID";
            Parameter.Add(new DBParameter("@L_Document_SpecialLinesID", _Document_SpecialLines.L_Document_SpecialLinesID));

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
