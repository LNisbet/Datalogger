using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SQLight_Database
{
    internal class SQL_Helper
    {
        public string CreatTableString(string tableName, List<ColumnDescription> columns )
        {
            List<string> strings = new();
            foreach ( ColumnDescription column in columns )
            {
                strings.Add( column.Name + " " + column.DataType);
            }
            return "CREATE TABLE " + tableName + CreateStringFromList(strings);
        }

        public string CreatInsertDataString(string tableName, DataDescription data)
        {
            return "INSERT INTO " + tableName + CreateStringFromList(data.ColumnNames) + " VALUES" + CreateStringFromList(data.Values) + ";";
        }

        public string CreatReadDataString(string tableName, string data)
        {
            return "SELECT " + data + " FROM " + tableName;
        }

        private string CreateStringFromList (List<string> list)
        {
            var x = "";
            var i = 0;
            foreach (var item in list)
            {
                if (i == 0)
                    x += "(";

                x += item;

                if (i == list.Count - 1)
                    x += ")";
                else
                    x += ", ";

                i++;
            }
            return x;
        }
    }
}
