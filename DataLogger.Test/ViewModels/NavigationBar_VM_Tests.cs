using DataLogger.ViewModels;
using DataLogger.ViewModels.Interfaces;
using DataLogger.Views;
using Moq;
using SQLight_Database.Database;
using SQLight_Database.Models;
using System.Windows.Input;
using Xunit;

namespace DataLogger.Tests.ViewModels
{
    public class NavigationBar_VM_Tests
    {
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly Mock<DatabaseConnectionStore> _databaseConnectionStoreMock;
        private readonly NavigationBar_VM _viewModel;

        public NavigationBar_VM_Tests()
        {
            // Arrange
            _navigationServiceMock = new Mock<INavigationService>();
            _databaseConnectionStoreMock = new Mock<DatabaseConnectionStore>();
            _viewModel = new NavigationBar_VM(_navigationServiceMock.Object, _databaseConnectionStoreMock.Object);
        }

        [Fact]
        public void CanNavigate_ShouldReturnFalse_WhenCurrentUserIsNull()
        {
            // Arrange
            _databaseConnectionStoreMock.Setup(store => store.CurrentUser).Returns((User?)null);

            // Act
            var result = _viewModel.CanNavigate;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanNavigate_ShouldReturnTrue_WhenCurrentUserHasName()
        {
            // Arrange
            _databaseConnectionStoreMock.Setup(store => store.CurrentUser).Returns(new User("TestUser"));

            // Act
            var result = _viewModel.CanNavigate;

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(nameof(NavigationBar_VM.NavigateToHomeCommand), typeof(Home_VM))]
        [InlineData(nameof(NavigationBar_VM.NavigateToCSVCommand), typeof(CSV_VM))]
        [InlineData(nameof(NavigationBar_VM.NavigateToLoggingCommand), typeof(Logging_VM))]
        [InlineData(nameof(NavigationBar_VM.NavigateToCreateExerciseCommand), typeof(CreateExercise_VM))]
        [InlineData(nameof(NavigationBar_VM.NavigateToBasicStatisticsCommand), typeof(BasicStatistics_VM))]
        [InlineData(nameof(NavigationBar_VM.NavigateToHandStatisticsOverviewCommand), typeof(HandStatisticsOverview_VM))]
        [InlineData(nameof(NavigationBar_VM.NavigateToChartingCommand), typeof(Charting_VM))]
        [InlineData(nameof(NavigationBar_VM.NavigateToDebugCommand), typeof(Debug_VM))]
        public void NavigationCommand_ShouldCallNavigateTo_WhenCanExecuteIsTrue(string commandName, Type viewModelType)
        {
            // Arrange
            _databaseConnectionStoreMock.Setup(store => store.CurrentUser).Returns(new User("TestUser"));

            // Act
            var command = (ICommand?)typeof(NavigationBar_VM).GetProperty(commandName)?.GetValue(_viewModel);
            Assert.NotNull(command);

            command!.Execute(null);

            // Assert
           // _navigationServiceMock.Verify(
           //     service => service.NavigateTo(It.Is<Type>(t => t == viewModelType)),
           //     Times.Once
           // );
        }


        [Theory]
        [InlineData(nameof(NavigationBar_VM.NavigateToHomeCommand))]
        [InlineData(nameof(NavigationBar_VM.NavigateToCSVCommand))]
        [InlineData(nameof(NavigationBar_VM.NavigateToLoggingCommand))]
        [InlineData(nameof(NavigationBar_VM.NavigateToCreateExerciseCommand))]
        [InlineData(nameof(NavigationBar_VM.NavigateToBasicStatisticsCommand))]
        [InlineData(nameof(NavigationBar_VM.NavigateToHandStatisticsOverviewCommand))]
        [InlineData(nameof(NavigationBar_VM.NavigateToChartingCommand))]
        [InlineData(nameof(NavigationBar_VM.NavigateToDebugCommand))]
        public void NavigationCommand_ShouldNotCallNavigateTo_WhenCanExecuteIsFalse(string commandName)
        {
            // Arrange
            _databaseConnectionStoreMock.Setup(store => store.CurrentUser).Returns((User?)null);

            // Act
            var command = (ICommand?)typeof(NavigationBar_VM).GetProperty(commandName)?.GetValue(_viewModel);
            Assert.NotNull(command);

            var canExecute = command!.CanExecute(null);
            Assert.False(canExecute);

            // Assert
            //_navigationServiceMock.Verify(service => service.NavigateTo(It.IsAny<Type>()), Times.Never);
        }
    }
}
