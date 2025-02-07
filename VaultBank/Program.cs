using VaultBank.Services;
using VaultBank.Utils;
using VaultBank.Models;

namespace VaultBank;

public class Program
{
    private static readonly BankService _bankService = new();

    public static void Main()
    {
        while (true)
        {
            DisplayHelper.ShowMainMenu();
            int option = InputHelper.ReadOption("Escolha uma opção", 1, 4);

            switch (option)
            {
                case 1:
                    CreateAccount();
                    break;
                case 2:
                    AccessAccount();
                    break;
                case 3:
                    ListAccounts();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private static void CreateAccount()
    {
        try
        {
            string owner = InputHelper.ReadString("Nome do titular");
            var account = _bankService.CreateAccount(owner);
            DisplayHelper.ShowSuccess($"Conta criada com sucesso!\nID da conta: {account.Id}");
        }
        catch (Exception ex)
        {
            DisplayHelper.ShowError(ex.Message);
        }
        InputHelper.WaitForKey();
    }

    private static void AccessAccount()
    {
        try
        {
            string accountId = InputHelper.ReadString("ID da conta");
            var account = _bankService.GetAccount(accountId);
            HandleAccountMenu(account);
        }
        catch (Exception ex)
        {
            DisplayHelper.ShowError(ex.Message);
            InputHelper.WaitForKey();
        }
    }

    private static void HandleAccountMenu(Account account)
    {
        while (true)
        {
            DisplayHelper.ShowAccountDetails(account);
            DisplayHelper.ShowAccountMenu();
            int option = InputHelper.ReadOption("Escolha uma opção", 1, 6);

            try
            {
                switch (option)
                {
                    case 1: // Consultar Saldo
                        DisplayHelper.ShowAccountDetails(account);
                        break;
                    case 2: // Depositar
                        HandleDeposit(account.Id);
                        break;
                    case 3: // Sacar
                        HandleWithdraw(account.Id);
                        break;
                    case 4: // Transferir
                        HandleTransfer(account.Id);
                        break;
                    case 5: // Extrato
                        var transactions = _bankService.GetAccountTransactions(account.Id);
                        DisplayHelper.ShowTransactions(transactions);
                        break;
                    case 6: // Voltar
                        return;
                }
            }
            catch (Exception ex)
            {
                DisplayHelper.ShowError(ex.Message);
            }
            InputHelper.WaitForKey();
        }
    }

    private static void HandleDeposit(string accountId)
    {
        decimal amount = InputHelper.ReadDecimal("Valor do depósito");
        _bankService.Deposit(accountId, amount);
        DisplayHelper.ShowSuccess("Depósito realizado com sucesso!");
    }

    private static void HandleWithdraw(string accountId)
    {
        decimal amount = InputHelper.ReadDecimal("Valor do saque");
        _bankService.Withdraw(accountId, amount);
        DisplayHelper.ShowSuccess("Saque realizado com sucesso!");
    }

    private static void HandleTransfer(string fromAccountId)
    {
        string toAccountId = InputHelper.ReadString("ID da conta de destino");
        decimal amount = InputHelper.ReadDecimal("Valor da transferência");
        _bankService.Transfer(fromAccountId, toAccountId, amount);
        DisplayHelper.ShowSuccess("Transferência realizada com sucesso!");
    }

    private static void ListAccounts()
    {
        var accounts = _bankService.GetAllAccounts();
        Console.WriteLine("\n=== Lista de Contas ===");
        foreach (var account in accounts)
        {
            DisplayHelper.ShowAccountDetails(account);
            Console.WriteLine("-------------------");
        }
        InputHelper.WaitForKey();
    }
}