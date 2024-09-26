namespace SQLight_Database
{
    static internal class SQL_Strings
    {
        static internal string CreateDatabase(string dbName)
        {
            return $"CREATE DATABASE {dbName}";
        }

        static internal string DeleteDatabase(string dbName)
        {
            return $"DROP DATABASE {dbName}";
        }

        static internal string BackupDatabase(string dbName, string path)
        {
            return $"BACKUP DATABASE {dbName} TO DISK='{path}' WITH DIFFERETIAL;";
        }

        static internal string CreateConnection(string dbName, int version, bool newDb, bool compress)
        {
            return $"DATA SOURCE={dbName}; VERSION={version}; NEW={newDb}; COMPRESS={compress}; ";
        }

        static internal string CreateTable(string tableName, List<ColumnDescription> columns )
        {
            List<string> strings = new();

            foreach ( ColumnDescription column in columns )
            {
                strings.Add($"{column.Name} {column.DataType} {column.Constraints}");
            }
            return $"CREATE TABLE IF NOT EXISTS {tableName} {CreateStringFromList(strings)}; ";
        }

        static internal string DeleteTable(string tableName)
        {
            return $"DROP TABLE {tableName}; ";
        }

        static internal string AlterTable(string tableName, ColumnDescription column) //Add
        {
            return $"ALTER TABLE {tableName} ADD {column.Name} {column.DataType}; ";
        }
        static internal string AlterTable(string tableName, string columnName) //Delete
        {
            return $"ALTER TABLE {tableName} DROP COLUMN {columnName}; ";
        }

        static internal string AlterTable(string tableName, string oldColumnName, string newColumnName) //Rename
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

        static internal string DeleteFromTable(string tableName, string condition)
        {
            return $"DELETE FROM {tableName} WHERE {condition}; ";
        }

        static internal string DeleteFromTable(string tableName)
        {
            return $"DELETE FROM {tableName}; ";
        }

        /* Not Implemented read commands
         * ORDER BY column1, column2, ... ASC|DESC;
         * 
         */
        static internal string ReadData(string tableName, string columns, bool distinct)
        {
            var select = "SELECT";
            if (distinct)
                select += " DISTINCT";

            return $"{select} {columns} FROM {tableName}; ";
        }

        static internal string ReadData(string tableName, string columns, bool distinct, string condition)
        {
            var select = "SELECT";
            if (distinct)
                select += " DISTINCT";

            return $"{select} {columns} FROM {tableName} WHERE {condition}; ";
        }

        static internal string SelectData(string tableName, string inColumn, string condition, Enums.SelectDataOptions selectOption)
        {
            switch (selectOption)
            {
                case Enums.SelectDataOptions.MIN:
                    return $"SELECT MIN({inColumn}) FROM {tableName} WHERE {condition}; ";
                case Enums.SelectDataOptions.MAX:
                    return $"SELECT MAX({inColumn}) FROM {tableName} WHERE {condition}; ";
                case Enums.SelectDataOptions.COUNT:
                    return $"SELECT COUNT({inColumn}) FROM {tableName} WHERE {condition}; ";
                case Enums.SelectDataOptions.SUM:
                    return $"SELECT SUM({inColumn}) FROM {tableName} WHERE {condition}; ";
                case Enums.SelectDataOptions.AVG:
                    return $"SELECT AVG({inColumn}) FROM {tableName} WHERE {condition}; ";
                default:
                    throw new NotImplementedException(selectOption.ToString());
            }
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
