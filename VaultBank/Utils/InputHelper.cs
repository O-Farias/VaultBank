namespace VaultBank.Utils;

public static class InputHelper
{
    public static string ReadString(string prompt)
    {
        string? input;
        do
        {
            Console.Write($"{prompt}: ");
            input = Console.ReadLine()?.Trim();
        } while (string.IsNullOrEmpty(input));

        return input;
    }

    public static decimal ReadDecimal(string prompt)
    {
        decimal value;
        while (true)
        {
            Console.Write($"{prompt}: R$ ");
            if (decimal.TryParse(Console.ReadLine(), out value) && value > 0)
            {
                return value;
            }
            DisplayHelper.ShowError("Valor inválido. Digite um número positivo.");
        }
    }

    public static int ReadOption(string prompt, int minValue, int maxValue)
    {
        int option;
        while (true)
        {
            Console.Write($"{prompt} ({minValue}-{maxValue}): ");
            if (int.TryParse(Console.ReadLine(), out option) && option >= minValue && option <= maxValue)
            {
                return option;
            }
            DisplayHelper.ShowError($"Opção inválida. Digite um número entre {minValue} e {maxValue}.");
        }
    }

    public static bool Confirm(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (S/N): ");
            string? input = Console.ReadLine()?.Trim().ToUpper();
            
            if (input == "S") return true;
            if (input == "N") return false;
            
            DisplayHelper.ShowError("Digite S para Sim ou N para Não.");
        }
    }

    public static void WaitForKey()
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey(true);
    }
}