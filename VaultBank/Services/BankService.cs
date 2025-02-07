namespace VaultBank.Services;

using VaultBank.Models;
using VaultBank.Data;

public class BankService
{
    private readonly BankDatabase _database;

    public BankService()
    {
        _database = new BankDatabase();
    }

    public Account CreateAccount(string owner)
    {
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException("Nome do proprietário é obrigatório");

        return _database.CreateAccount(owner);
    }

    public void Deposit(string accountId, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Valor do depósito deve ser maior que zero");

        var account = _database.GetAccount(accountId);
        account.Deposit(amount);
    }

    public void Withdraw(string accountId, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Valor do saque deve ser maior que zero");

        var account = _database.GetAccount(accountId);
        account.Withdraw(amount);
    }

    public void Transfer(string fromAccountId, string toAccountId, decimal amount)
    {
        if (fromAccountId == toAccountId)
            throw new ArgumentException("Não é possível transferir para a mesma conta");

        if (amount <= 0)
            throw new ArgumentException("Valor da transferência deve ser maior que zero");

        _database.TransferMoney(fromAccountId, toAccountId, amount);
    }

    public Account GetAccount(string accountId)
    {
        return _database.GetAccount(accountId);
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        return _database.GetAllAccounts();
    }

    public IEnumerable<Transaction> GetAccountTransactions(string accountId)
    {
        var account = _database.GetAccount(accountId);
        return account.Transactions;
    }
}