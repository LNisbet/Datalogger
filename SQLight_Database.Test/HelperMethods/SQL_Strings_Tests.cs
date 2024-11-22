using SQLight_Database.HelperMethods;
using SQLight_Database.Models;
using Xunit;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace SQLight_Database.Test.HelperMethods
{
    public class SQL_Strings_Tests
    {
        [Fact]
        public void CreateConnection_ShouldReturnCorrectString()
        {
            // Arrange
            string dbName = "TestDB";
            int version = 3;
            bool newDb = true;
            bool compress = false;

            // Act
            string result = SQL_Strings.CreateConnection(dbName, version, newDb, compress);

            // Assert
            Assert.Equal("DATA SOURCE=TestDB.db; VERSION=3; NEW=True; COMPRESS=False; ", result);
        }

        [Fact]
        public void CreateDatabase_ShouldReturnCorrectString()
        {
            // Arrange
            string dbName = "TestDB";

            // Act
            string result = SQL_Strings.CreateDatabase(dbName);

            // Assert
            Assert.Equal("CREATE DATABASE TestDB.db", result);
        }

        [Fact]
        public void DeleteDatabase_ShouldReturnCorrectString()
        {
            // Arrange
            string dbName = "TestDB";

            // Act
            string result = SQL_Strings.DeleteDatabase(dbName);

            // Assert
            Assert.Equal("DROP DATABASE TestDB.db", result);
        }

        [Fact]
        public void BackupDatabase_ShouldReturnCorrectString()
        {
            // Arrange
            string dbName = "TestDB";
            string path = "C:\\Backups\\TestDB.bak";

            // Act
            string result = SQL_Strings.BackupDatabase(dbName, path);

            // Assert
            Assert.Equal("BACKUP DATABASE TestDB.db TO DISK='C:\\Backups\\TestDB.bak' WITH DIFFERETIAL;", result);
        }

        [Fact]
        public void CreateTable_ShouldReturnCorrectString()
        {
            // Arrange
            string tableName = "TestTable";
            var columns = new List<ColumnDescription>
            {
                new ColumnDescription("Id", "INTEGER", "PRIMARY KEY"),
                new ColumnDescription( "Name", "TEXT", "NOT NULL")
            };

            // Act
            string result = SQL_Strings.CreateTable(tableName, columns);

            // Assert
            Assert.Equal("CREATE TABLE IF NOT EXISTS TestTable (Id INTEGER PRIMARY KEY,Name TEXT NOT NULL); ", result);
        }

        [Fact]
        public void DeleteTable_ShouldReturnCorrectString()
        {
            // Arrange
            string tableName = "TestTable";

            // Act
            string result = SQL_Strings.DeleteTable(tableName);

            // Assert
            Assert.Equal("DROP TABLE TestTable; ", result);
        }

        [Fact]
        public void AddColumnToTable_ShouldReturnCorrectString()
        {
            // Arrange
            string tableName = "TestTable";
            var column = new ColumnDescription("Age", "INTEGER", "");

            // Act
            string result = SQL_Strings.AddColumnToTable(tableName, column);

            // Assert
            Assert.Equal("ALTER TABLE TestTable ADD Age INTEGER; ", result);
        }

        [Fact]
        public void ReadData_ShouldReturnCorrectString_WithCondition()
        {
            // Arrange
            string tableName = "TestTable";
            string columns = "*";
            bool distinct = true;
            string condition = "Age > 30";

            // Act
            string result = SQL_Strings.ReadData(tableName, columns, distinct, condition);

            // Assert
            Assert.Equal("SELECT DISTINCT * FROM TestTable WHERE Age > 30; ", result);
        }

        [Fact]
        public void ReadData_ShouldReturnCorrectString_WithoutCondition()
        {
            // Arrange
            string tableName = "TestTable";
            string columns = "*";
            bool distinct = false;

            // Act
            string result = SQL_Strings.ReadData(tableName, columns, distinct);

            // Assert
            Assert.Equal("SELECT * FROM TestTable ; ", result);
        }

        [Fact]
        public void SelectData_ShouldReturnCorrectString()
        {
            // Arrange
            string tableName = "TestTable";
            string columns = "Name";
            string inColumn = "Age";
            var selectOption = SQL_Strings.SelectDataOptions.MAX;
            string condition = "Age > 20";

            // Act
            string result = SQL_Strings.SelectData(tableName, columns, inColumn, selectOption, condition);

            // Assert
            Assert.Equal("SELECT Name FROM TestTable WHERE Age = (SELECT MAX(Age) FROM TestTable WHERE Age > 20) AND Age > 20 Limit 1; ", result);
        }
    }
}
