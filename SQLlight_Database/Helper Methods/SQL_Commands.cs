using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SQLight_Database
{
    static public class SQL_Commands
    {

        public static SQLiteConnection CreateConnection(string dbName, SQLiteConnection conn)
        {
            string newConnectionString = SQL_Commands.CreateConnectionString(dbName, 3, true, true);
            string ExistingConnectionString = SQL_Commands.CreateConnectionString(dbName, 3, false, true);

            // Create a new database connection
            if (conn == null)
                conn = new SQLiteConnection(newConnectionString);
            else
                conn = new SQLiteConnection(ExistingConnectionString);
            // Open the connection:
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                conn = new SQLiteConnection(ExistingConnectionString);
                conn.Open();
            }
            return conn;
        }

        public static SQLiteConnection CloseConnection(string dbName, SQLiteConnection conn)
        {
            // close the connection:
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return conn;
        }

        public static void ExecuteNonQueryCommand(string command, SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = command;
            sqlite_cmd.ExecuteNonQuery();
        }

        static public string CreateDatabaseString(string dbName)
        {
            return "CREATE DATABASE " + dbName;
        }

        static public string DeleteDatabaseString(string dbName)
        {
            return "DROP DATABASE " + dbName;
        }

        static public string BackupDatabaseString(string dbName, string path)
        {
            return "BACKUP DATABASE " + dbName + " TO DISK = '" + path + "' WITH DIFFERETIAL;";
        }

        static public string CreateConnectionString(string dbName, int version, bool newDb, bool compress)
        {
            return "Data Source=" + dbName + "; Version = " + version.ToString() + "; New = " + newDb.ToString() + "; Compress = " + compress.ToString() + "; ";
        }

        static public string CreateTableString(string tableName, List<ColumnDescription> columns )
        {
            List<string> strings = new();
            foreach ( ColumnDescription column in columns )
            {
                strings.Add(column.Name + " " + column.DataType + " " + column.Constraints + ",");
            }
            return "CREATE TABLE " + tableName + CreateStringFromList(strings) + "; ";
        }

        static public string DeleteTableString(string tableName)
        {
            return "DROP TABLE " + tableName + ";";
        }

        static public string AlterTableString(string tableName, ColumnDescription column) //Add
        {
            return "ALTER TABLE " + tableName + " ADD " + column.Name + " " + column.DataType + ";";
        }
        static public string AlterTableString(string tableName, string columnName) //Delete
        {
            return "ALTER TABLE " + tableName + " DROP COLUMN " + columnName + ";";
        }

        static public string AlterTableString(string tableName, string oldColumnName, string newColumnName) //Rename
        {
            return "ALTER TABLE " + tableName + " RENAME COLUMN " + oldColumnName + " TO " + newColumnName + ";";
        }

        /* Not Implemented alter table commands
         * change data type of column
         */

        static public string CreateIndexString(string tableName, string indexName, List<string> columnNames, bool unique)
        {
            if (unique)
                return "CREATE UNIQUE INDEX " + indexName + " ON " + tableName + CreateStringFromList((List<string>)columnNames) + ";";
            else
                return "CREATE INDEX " + indexName + " ON " + tableName + CreateStringFromList((List<string>)columnNames) + ";";
        }

        static public string DeleteIndexString(string tableName, string indexName)
        {
            return "DROP INDEX " + indexName + " ON " + tableName + ";";
        }

        static public string InsertDataString(string tableName, DataDescription data)
        {
            return "INSERT INTO " + tableName + CreateStringFromList(data.ColumnNames) + " VALUES" + CreateStringFromList(data.Values) + "; ";
        }

        static public string InsertDataString(string tableName, List<string> data)
        {
            return "INSERT INTO " + tableName + " VALUES" + CreateStringFromList(data) + "; ";
        }

        static public string AlterDataString(string tableName, string set, string condition)
        {
            return "UPDATE " + tableName + " SET" + set + "WHERE " + condition + "; ";
        }

        static public string DeleteFromTableString(string tableName, string condition)
        {
            return "DELETE FROM " + tableName + " WHERE " + condition + "; ";
        }

        static public string DeleteFromTableString(string tableName)
        {
            return "DELETE FROM " + tableName + "; ";
        }

        /* Not Implemented read commands
         * ORDER BY column1, column2, ... ASC|DESC;
         * 
         */
        static public string ReadDataString(string tableName, string columns, bool distinct)
        {
            var select = "SELECT ";
            if (distinct)
                select += "DISTINCT ";

            return select + columns + " FROM " + tableName + "; ";
        }

        static public string ReadDataString(string tableName, string columns, bool distinct, string condition)
        {
            var select = "SELECT ";
            if (distinct)
                select += "DISTINCT ";

            return select + columns + " FROM " + tableName + " WHERE " + condition + "; ";
        }

        static public string SelectDataString(string tableName, string inColumn, string condition, SelectDataOptions selectOption)
        {
            switch (selectOption)
            {
                case SelectDataOptions.MIN:
                    return "SELECT MIN(" + inColumn + ") FROM " + tableName + " WHERE " + condition + "; ";
                case SelectDataOptions.MAX:
                    return "SELECT MAX(" + inColumn + ") FROM " + tableName + " WHERE " + condition + "; ";
                case SelectDataOptions.COUNT:
                    return "SELECT COUNT(" + inColumn + ") FROM " + tableName + " WHERE " + condition + "; ";
                case SelectDataOptions.SUM:
                    return "SELECT SUM(" + inColumn + ") FROM " + tableName + " WHERE " + condition + "; ";
                case SelectDataOptions.AVG:
                    return "SELECT AVG(" + inColumn + ") FROM " + tableName + " WHERE " + condition + "; ";
                default:
                    return "";
            }
        }

        public enum SelectDataOptions
        {
            MIN,
            MAX,
            COUNT,
            SUM,
            AVG
        }

        static private string CreateStringFromList (List<string> list)
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
