namespace VaultBank.Utils;

using VaultBank.Models;

public static class DisplayHelper
{
    private static readonly ConsoleColor TransactionColor = ConsoleColor.Cyan;
    private static readonly ConsoleColor MenuColor = ConsoleColor.Yellow;
    private static readonly ConsoleColor BalanceColor = ConsoleColor.Blue;
    private static readonly ConsoleColor ErrorColor = ConsoleColor.Red;
    private static readonly ConsoleColor SuccessColor = ConsoleColor.Green;

    public static void ShowMainMenu()
    {
        Console.Clear();
        WriteColored("=== VaultBank - Sistema Bancário ===\n", MenuColor);
        Console.WriteLine("1. Criar Nova Conta");
        Console.WriteLine("2. Acessar Conta");
        Console.WriteLine("3. Listar Todas as Contas");
        Console.WriteLine("4. Ajuda");
        Console.WriteLine("5. Sair");
        WriteColored("================================\n", MenuColor);
    }

    public static void ShowAccountMenu()
    {
        WriteColored("\n=== Menu da Conta ===\n", MenuColor);
        Console.WriteLine("1. Consultar Saldo");
        Console.WriteLine("2. Depositar");
        Console.WriteLine("3. Sacar");
        Console.WriteLine("4. Transferir");
        Console.WriteLine("5. Extrato");
        Console.WriteLine("6. Voltar");
        WriteColored("===================\n", MenuColor);
    }

    public static void ShowHelp()
    {
        Console.Clear();
        WriteColored("=== Ajuda do Sistema ===\n", MenuColor);
        Console.WriteLine("• Criar Conta: Permite abrir uma nova conta bancária");
        Console.WriteLine("• Depositar: Adiciona dinheiro à sua conta");
        Console.WriteLine("• Sacar: Retira dinheiro da sua conta");
        Console.WriteLine("• Transferir: Envia dinheiro para outra conta");
        Console.WriteLine("• Extrato: Mostra histórico de transações");
        WriteColored("\nLimites de Operações:\n", MenuColor);
        Console.WriteLine("- Saque diário: R$ 1.000,00");
        Console.WriteLine("- Transferência: R$ 5.000,00 por operação");
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
        Console.ReadKey(true);
    }

    public static void ShowAccountDetails(Account account)
    {
        Console.WriteLine($"\nConta: {account.Id}");
        Console.WriteLine($"Titular: {account.Owner}");
        WriteColored($"Saldo: R$ {account.Balance:F2}\n", BalanceColor);
        Console.WriteLine($"Criada em: {account.CreatedAt:dd/MM/yyyy HH:mm:ss}");
    }

    public static void ShowTransactions(IEnumerable<Transaction> transactions)
    {
        WriteColored("\n=== Extrato de Transações ===\n", TransactionColor);
        foreach (var transaction in transactions)
        {
            string tipo = transaction.Type switch
            {
                TransactionType.Deposit => "Depósito",
                TransactionType.Withdrawal => "Saque",
                TransactionType.Transfer => "Transferência",
                _ => "Desconhecido"
            };

            WriteColored($"Tipo: {tipo}\n", TransactionColor);
            Console.WriteLine($"Data: {transaction.Date:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"Valor: R$ {transaction.Amount:F2}");
            if (!string.IsNullOrEmpty(transaction.Description))
                Console.WriteLine($"Descrição: {transaction.Description}");
            Console.WriteLine("----------------------------");
        }
    }

    public static void ShowError(string message)
    {
        WriteColored($"\nErro: {message}\n", ErrorColor);
    }

    public static void ShowSuccess(string message)
    {
        WriteColored($"\n✓ {message}\n", SuccessColor);
    }

    private static void WriteColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    public static bool ConfirmOperation(string operation)
    {
        Console.WriteLine($"\nConfirmar {operation}?");
        return InputHelper.Confirm("Tem certeza que deseja continuar");
    }
}