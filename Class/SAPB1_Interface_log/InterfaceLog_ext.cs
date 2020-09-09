/*
** Company Name     :  Trinity Solution Provider Co., Ltd. && ICON Framework Co.,Ltd.
** Contact          :  www.iconrem.com
** Product Name     :  Data Access framework(Class generator) (2012)
** Product by       :  Anupong Kwanpigul
** Modify by        :  Yuttapong Benjapornraksa
** Modify Date      :  2020-01-30 13:51:34
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

    public partial class InterfaceLog_Command
    {

    }
    public class InterfaceLog_ext : InterfaceLog
    {
        private String _CompanyDB;
        public readonly bool _CompanyDB_PKFlag = false;
        public readonly bool _CompanyDB_IDTFlag = false;
        [XmlIgnore]
        public bool EditCompanyDB = false;
        public String CompanyDB
        {
            get
            {
                return _CompanyDB;
            }
            set
            {
                if (_CompanyDB != value)
                {
                    _CompanyDB = value;
                    EditCompanyDB = false;
                }
            }
        }
    }
}