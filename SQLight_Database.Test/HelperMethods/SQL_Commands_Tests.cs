using Moq;
using SQLight_Database.HelperMethods;
using SQLight_Database.Exceptions;
using System.Data;
using Xunit;
using SQLight_Database.Database.Wrappers;
using System.Data.SQLite;

namespace SQLight_Database.Test.HelperMethods
{
    public class SQL_Commands_Tests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateConnection_ShouldReturnOpenConnection(bool dbExists)
        {
            // Arrange
            var mockConnectionWrapper = new Mock<IDbConnectionWrapper>();
            string dbName = "TestDB";

            // Act
            var conn = SQL_Commands.CreateConnection(dbName, dbExists);

            // Assert
            Assert.NotNull(conn);
            //mockConnectionWrapper.Verify(conn => conn.Open(), Times.Once);
        }

        [Fact]
        public void CloseConnection_ShouldCloseConnection()
        {
            // Arrange
            var mockConnectionWrapper = new Mock<IDbConnectionWrapper>();
            mockConnectionWrapper.Setup(conn => conn.Close()).Verifiable();

            // Act
            var result = SQL_Commands.CloseConnection(mockConnectionWrapper.Object);

            // Assert
            mockConnectionWrapper.Verify(conn => conn.Close(), Times.Once); // Verify Close was called.
            Assert.Same(mockConnectionWrapper.Object, result); // Ensure the same wrapper is returned.
        }

        [Fact]
        public void ExecuteSQLString_ShouldExecuteNonQuery_WhenCommandTypeIsNonQuery()
        {
            // Arrange
            var mockConnectionWrapper = new Mock<IDbConnectionWrapper>();
            string sqlString = "UPDATE table SET column = value";
            var commandType = SQL_Commands.CommandType.NonQuery;

            // Mock ExecuteNonQueryCommand
            var mockCommand = new Mock<IDbCommand>();
            mockConnectionWrapper.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Verifiable();

            // Act
            var result = SQL_Commands.ExecuteSQLString(mockConnectionWrapper.Object, sqlString, commandType);

            // Assert
            Assert.Null(result); // NonQuery commands return null.
            mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Once); // Verify that ExecuteNonQuery was called.
        }

        [Fact]
        public void ExecuteSQLString_ShouldExecuteReader_WhenCommandTypeIsReader()
        {
            // Arrange
            var mockConnectionWrapper = new Mock<IDbConnectionWrapper>();
            string sqlString = "SELECT * FROM table";
            var commandType = SQL_Commands.CommandType.Reader;

            // Mock ExecuteReader
            var mockCommand = new Mock<IDbCommand>();
            var mockDataReader = new Mock<IDataReader>();
            mockConnectionWrapper.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockDataReader.Object);

            // Act
            var result = SQL_Commands.ExecuteSQLString(mockConnectionWrapper.Object, sqlString, commandType);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IDataReader>(result); // The result should be of type IDataReader.
            mockCommand.Verify(cmd => cmd.ExecuteReader(), Times.Once); // Verify that ExecuteReader was called.
        }

        [Fact]
        public void ExecuteSQLString_ShouldThrowException_WhenConnectionIsNull()
        {
            // Arrange
            IDbConnectionWrapper? mockConnection = null;
            string sqlString = "SELECT * FROM table";
            var commandType = SQL_Commands.CommandType.Reader;

            // Act & Assert
            Assert.Throws<NoOpenSQLConnection>(() =>
                SQL_Commands.ExecuteSQLString(mockConnection, sqlString, commandType));
        }

        [Fact]
        public void ExecuteSQLString_ShouldThrowNotImplementedException_WhenCommandTypeIsInvalid()
        {
            // Arrange
            var mockConnectionWrapper = new Mock<IDbConnectionWrapper>();
            string sqlString = "SELECT * FROM table";

            // Act & Assert
            Assert.Throws<NotImplementedException>(() =>
                SQL_Commands.ExecuteSQLString(mockConnectionWrapper.Object, sqlString, (SQL_Commands.CommandType)999));
        }
    }
}
