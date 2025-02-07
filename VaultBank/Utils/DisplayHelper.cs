namespace VaultBank.Utils;

using VaultBank.Models;

public static class DisplayHelper
{
    public static void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("=== VaultBank - Sistema Bancário ===");
        Console.WriteLine("1. Criar Nova Conta");
        Console.WriteLine("2. Acessar Conta");
        Console.WriteLine("3. Listar Todas as Contas");
        Console.WriteLine("4. Sair");
        Console.WriteLine("================================");
    }

    public static void ShowAccountMenu()
    {
        Console.WriteLine("\n=== Menu da Conta ===");
        Console.WriteLine("1. Consultar Saldo");
        Console.WriteLine("2. Depositar");
        Console.WriteLine("3. Sacar");
        Console.WriteLine("4. Transferir");
        Console.WriteLine("5. Extrato");
        Console.WriteLine("6. Voltar");
        Console.WriteLine("===================");
    }

    public static void ShowAccountDetails(Account account)
    {
        Console.WriteLine($"\nConta: {account.Id}");
        Console.WriteLine($"Titular: {account.Owner}");
        Console.WriteLine($"Saldo: R$ {account.Balance:F2}");
        Console.WriteLine($"Criada em: {account.CreatedAt:dd/MM/yyyy HH:mm:ss}");
    }

    public static void ShowTransactions(IEnumerable<Transaction> transactions)
    {
        Console.WriteLine("\n=== Extrato de Transações ===");
        foreach (var transaction in transactions)
        {
            string tipo = transaction.Type switch
            {
                TransactionType.Deposit => "Depósito",
                TransactionType.Withdrawal => "Saque",
                TransactionType.Transfer => "Transferência",
                _ => "Desconhecido"
            };

            Console.WriteLine($"Data: {transaction.Date:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"Tipo: {tipo}");
            Console.WriteLine($"Valor: R$ {transaction.Amount:F2}");
            if (!string.IsNullOrEmpty(transaction.Description))
                Console.WriteLine($"Descrição: {transaction.Description}");
            Console.WriteLine("----------------------------");
        }
    }

    public static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nErro: {message}");
        Console.ResetColor();
    }

    public static void ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{message}");
        Console.ResetColor();
    }
}