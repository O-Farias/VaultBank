namespace VaultBank.Models;

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}

public class Transaction
{
    public string Id { get; private set; }
    public string AccountId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }

    public Transaction(string accountId, decimal amount, TransactionType type, string description = "")
    {
        Id = Guid.NewGuid().ToString();
        AccountId = accountId;
        Amount = amount;
        Type = type;
        Date = DateTime.UtcNow;
        Description = description;
    }
}