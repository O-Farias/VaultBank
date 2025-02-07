namespace VaultBank.Data;

using VaultBank.Models;

public class BankDatabase
{
    private readonly Dictionary<string, Account> _accounts;

    public BankDatabase()
    {
        _accounts = new Dictionary<string, Account>();
    }

    public Account CreateAccount(string owner)
    {
        var account = new Account(owner);
        _accounts.Add(account.Id, account);
        return account;
    }

    public Account GetAccount(string id)
    {
        if (!_accounts.ContainsKey(id))
            throw new KeyNotFoundException("Conta não encontrada");
            
        return _accounts[id];
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        return _accounts.Values;
    }

    public void DeleteAccount(string id)
    {
        if (!_accounts.ContainsKey(id))
            throw new KeyNotFoundException("Conta não encontrada");

        var account = _accounts[id];
        if (account.Balance > 0)
            throw new InvalidOperationException("Não é possível excluir conta com saldo");

        _accounts.Remove(id);
    }

    public void TransferMoney(string fromAccountId, string toAccountId, decimal amount)
    {
        var sourceAccount = GetAccount(fromAccountId);
        var targetAccount = GetAccount(toAccountId);

        sourceAccount.Withdraw(amount);
        targetAccount.Deposit(amount);
    }
}