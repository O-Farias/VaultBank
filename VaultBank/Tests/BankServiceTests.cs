using Xunit;
using VaultBank.Services;
using VaultBank.Models;

namespace VaultBank.Tests;

public class BankServiceTests
{
    private readonly BankService _bankService;

    public BankServiceTests()
    {
        _bankService = new BankService();
    }

    [Fact]
    public void CreateAccount_WithValidOwner_ShouldCreateAccount()
    {
        // Arrange
        string owner = "João Silva";

        // Act
        var account = _bankService.CreateAccount(owner);

        // Assert
        Assert.NotNull(account);
        Assert.Equal(owner, account.Owner);
        Assert.Equal(0m, account.Balance);
        Assert.True(account.IsActive);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateAccount_WithInvalidOwner_ShouldThrowException(string? owner)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _bankService.CreateAccount(owner!));
    }

    [Fact]
    public void Deposit_ValidAmount_ShouldIncreaseBalance()
    {
        // Arrange
        var account = _bankService.CreateAccount("Maria Santos");
        decimal depositAmount = 100m;

        // Act
        _bankService.Deposit(account.Id, depositAmount);

        // Assert
        var updatedAccount = _bankService.GetAccount(account.Id);
        Assert.Equal(depositAmount, updatedAccount.Balance);
    }

    [Fact]
    public void Withdraw_ValidAmount_ShouldDecreaseBalance()
    {
        // Arrange
        var account = _bankService.CreateAccount("Pedro Costa");
        _bankService.Deposit(account.Id, 100m);
        decimal withdrawAmount = 50m;

        // Act
        _bankService.Withdraw(account.Id, withdrawAmount);

        // Assert
        var updatedAccount = _bankService.GetAccount(account.Id);
        Assert.Equal(50m, updatedAccount.Balance);
    }

    [Fact]
    public void Transfer_ValidAmount_ShouldTransferMoney()
    {
        // Arrange
        var sourceAccount = _bankService.CreateAccount("José Lima");
        var targetAccount = _bankService.CreateAccount("Ana Paula");
        _bankService.Deposit(sourceAccount.Id, 100m);
        decimal transferAmount = 50m;

        // Act
        _bankService.Transfer(sourceAccount.Id, targetAccount.Id, transferAmount);

        // Assert
        var updatedSourceAccount = _bankService.GetAccount(sourceAccount.Id);
        var updatedTargetAccount = _bankService.GetAccount(targetAccount.Id);
        Assert.Equal(50m, updatedSourceAccount.Balance);
        Assert.Equal(50m, updatedTargetAccount.Balance);
    }

    [Fact]
    public void Withdraw_InsufficientFunds_ShouldThrowException()
    {
        // Arrange
        var account = _bankService.CreateAccount("Carlos Santos");
        _bankService.Deposit(account.Id, 50m);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => 
            _bankService.Withdraw(account.Id, 100m));
    }

    [Fact]
    public void Transfer_InsufficientFunds_ShouldThrowException()
    {
        // Arrange
        var sourceAccount = _bankService.CreateAccount("Roberto Silva");
        var targetAccount = _bankService.CreateAccount("Lucia Costa");
        _bankService.Deposit(sourceAccount.Id, 50m);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => 
            _bankService.Transfer(sourceAccount.Id, targetAccount.Id, 100m));
    }
}