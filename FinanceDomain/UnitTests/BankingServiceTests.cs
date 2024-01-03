using FinanceModuleCoreAPI.Entities;
using FinanceModuleCoreAPI.Entities.Entities;
using FinanceModuleCoreAPI.Repository;
using FinanceModuleCoreAPI.Service;
using Moq;

public class BankingServiceTests
{
    [Fact]
    public void CreateUser_ReturnsNewUser()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var bankingService = new BankingService(userRepositoryMock.Object);
        var username = "test";
        var password = "testPassword";
        var user = new Users { Username = username, Password = password };

        userRepositoryMock.Setup(repo => repo.CreateUser(user))
            .Returns(user);

        // Act
        var result = bankingService.CreateUser(user);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(username, result.Username);
        Assert.Equal(password, result.Password);
        Assert.Equal(100, result.Balance); // Assuming the default balance is 100
        userRepositoryMock.Verify(repo => repo.CreateUser(It.IsAny<Users>()), Times.Once);
    }

    [Fact]
    public void Login_ReturnsUserIfExists()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var bankingService = new BankingService(userRepositoryMock.Object);
        var username = "testUser";
        var password = "testPassword";
        var user = new Users { Username = username, Password = password };

        userRepositoryMock.Setup(repo => repo.GetUserByUsernameAndPassword(username, password))
            .Returns(user);

        // Act
        var result = bankingService.Login(username, password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(username, result.Username);
        Assert.Equal(password, result.Password);
    }

    [Fact]
    public void CheckBalance_Test()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var bankingService = new BankingService(userRepositoryMock.Object);
        var username = "testUser";
        decimal amount = 100;
        userRepositoryMock.Setup(repo => repo.CheckBalance(username))
            .Returns(amount);

        // Act
        var result = bankingService.CheckBalance(username);

        // Assert
        Assert.Equal(amount, result);
    }

    [Fact]
    public void TransferFunds_InsufficientFunds_ReturnsFailureResult()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var bankingService = new BankingService(userRepositoryMock.Object);

        // Set up the mock to return a failure result for insufficient funds
        var userName = "Jack";
        var amount = 150;
        var insufficientFundsResult = new TransferResult(false, "There is insufficient funds in your account.");
        userRepositoryMock.Setup(repo => repo.TransferFunds(userName, amount))
            .Returns(insufficientFundsResult);

        // Act
        var result = bankingService.TransferFunds(userName, amount);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("There is insufficient funds in your account.", result.Message);
    }

}
