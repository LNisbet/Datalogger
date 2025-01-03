﻿
using SQLight_Database.Models;

namespace SQLight_Database.HelperMethods
{
    static internal class SQL_Strings
    {
        static internal string CreateConnection(string dbName, int version, bool newDb, bool compress)
        {
            return $"DATA SOURCE={dbName}.db; VERSION={version}; NEW={newDb}; COMPRESS={compress}; ";
        }

        static internal string CreateDatabase(string dbName)
        {
            return $"CREATE DATABASE {dbName}.db";
        }

        static internal string DeleteDatabase(string dbName)
        {
            return $"DROP DATABASE {dbName}.db";
        }

        static internal string BackupDatabase(string dbName, string path)
        {
            return $"BACKUP DATABASE {dbName}.db TO DISK='{path}' WITH DIFFERETIAL;";
        }

        static internal string CreateTable(string tableName, List<ColumnDescription> columns)
        {
            List<string> strings = [];

            foreach (ColumnDescription column in columns)
            {
                strings.Add($"{column.Name} {column.DataType} {column.Constraints}");
            }
            return $"CREATE TABLE IF NOT EXISTS {tableName} {CreateStringFromList(strings)}; ";
        }

        static internal string DeleteTable(string tableName)
        {
            return $"DROP TABLE {tableName}; ";
        }

        static internal string AddColumnToTable(string tableName, ColumnDescription column)
        {
            return $"ALTER TABLE {tableName} ADD {column.Name} {column.DataType}; ";
        }
        static internal string DeleteColumnFromTable(string tableName, string columnName)
        {
            return $"ALTER TABLE {tableName} DROP COLUMN {columnName}; ";
        }

        static internal string RenameColumnInTable(string tableName, string oldColumnName, string newColumnName)
        {
            return $"ALTER TABLE {tableName} RENAME COLUMN {oldColumnName} TO {newColumnName}; ";
        }

        /* Not Implemented alter table commands
         * change data type of column
         */

        static internal string CreateIndex(string tableName, string indexName, List<string> columnNames, bool unique)
        {
            if (unique)
                return $"CREATE UNIQUE INDEX {indexName} ON {tableName}{CreateStringFromList(columnNames)}; ";
            else
                return $"CREATE INDEX {indexName} ON {tableName}{CreateStringFromList(columnNames)}; ";
        }

        static internal string DeleteIndex(string tableName, string indexName)
        {
            return $"DROP INDEX {indexName} VALUES {tableName}; ";
        }

        static internal string InsertData(string tableName, DataDescription data)
        {
            return $"INSERT INTO {tableName}{CreateStringFromList(data.ColumnNames)} VALUES{CreateStringFromList(data.Values)}; ";
        }

        static internal string InsertData(string tableName, List<string> data)
        {
            return $"INSERT INTO {tableName} VALUES{CreateStringFromList(data)}; ";
        }

        static internal string AlterData(string tableName, string set, string condition)
        {
            return $"UPDATE {tableName} SET {set} WHERE {condition}; ";
        }

        static internal string DeleteFromTable(string tableName, string? condition = null)
        {
            var con = "";
            if (condition != null)
                con = $"WHERE {condition}";

            return $"DELETE FROM {tableName} {con}; ";
        }

        /* Not Implemented read commands
         * ORDER BY column1, column2, ... ASC|DESC;
         * 
         */

        static internal string ReadData(string tableName, string columns, bool distinct, string? condition = null)
        {
            var select = "SELECT";
            if (distinct)
                select += " DISTINCT";

            var con = "";
            if (condition != null)
                con = $"WHERE {condition}";

            return $"{select} {columns} FROM {tableName} {con}; ";
        }

        static internal string SelectData(string tableName, string columns, string inColumn, SelectDataOptions selectOption, string? condition = null)
        {
            var whereCon = "";
            var andCon = "";
            if (condition != null)
            {
                whereCon = $"WHERE {condition}";
                andCon = $"AND {condition}";
            }

            return $"SELECT {columns} FROM {tableName} WHERE {inColumn} = (SELECT {selectOption}({inColumn}) FROM {tableName} {whereCon}) {andCon} Limit 1; ";
        }
        public enum SelectDataOptions
        {
            MIN,
            MAX,
            COUNT,
            SUM,
            AVG
        }

        static private string CreateStringFromList(List<string> list)
        {
            var x = "";
            var i = 0;

            foreach (var item in list)
            {
                if (i == 0)
                    x += "(";

                x += item;

                if (i >= list.Count - 1)
                    x += ")";
                else
                    x += ",";

                i++;
            }
            return x;
        }
    }
}
