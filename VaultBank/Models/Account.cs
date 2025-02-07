namespace VaultBank.Models;

public class Account
{
    public string Id { get; private set; }
    public string Owner { get; private set; }
    public decimal Balance { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public List<Transaction> Transactions { get; private set; }

    public Account(string owner)
    {
        Id = Guid.NewGuid().ToString();
        Owner = owner;
        Balance = 0;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
        Transactions = new List<Transaction>();
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("O valor do depósito deve ser positivo");

        Balance += amount;
        Transactions.Add(new Transaction(Id, amount, TransactionType.Deposit));
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("O valor do saque deve ser positivo");
        
        if (amount > Balance)
            throw new InvalidOperationException("Saldo insuficiente");

        Balance -= amount;
        Transactions.Add(new Transaction(Id, amount, TransactionType.Withdrawal));
    }
}