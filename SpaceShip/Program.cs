using System;
using System.Collections.Generic;
using ShipFactoryApp.Managers;

class Program
{
    static void Main(string[] args)
    {
        var stockManager = StockManager.Instance;

        stockManager.DisplayStock();

        while (true)
        {
            Console.WriteLine("Entrez une commande (ou tapez 'STOCKS' pour afficher les stocks, 'NEEDED_STOCKS ARGS' pour afficher les pièces nécessaires, 'INSTRUCTIONS ARGS' pour afficher les instructions d'assemblage, 'VERIFY ARGS' pour vérifier une commande ou 'QUIT' pour quitter) :");
            var input = Console.ReadLine();

            if (input.ToUpper() == "QUIT")
            {
                break;
            }
            else if (input.ToUpper() == "STOCKS")
            {
                stockManager.DisplayStock();
            }
            else if (input.ToUpper().StartsWith("NEEDED_STOCKS"))
            {
                var args = input.Substring("NEEDED_STOCKS".Length).Trim();
                var order = ParseOrder(args);
                stockManager.NeededStocks(order);
            }
            else if (input.ToUpper().StartsWith("INSTRUCTIONS"))
            {
                var args = input.Substring("INSTRUCTIONS".Length).Trim();
                var order = ParseOrder(args);
                stockManager.Instructions(order);
            }
            else if (input.ToUpper().StartsWith("VERIFY"))
            {
                var args = input.Substring("VERIFY".Length).Trim();
                var order = ParseOrder(args);
                stockManager.Verify(order);
            }
        }
    }

    static Dictionary<string, int> ParseOrder(string args)
    {
        var order = new Dictionary<string, int>();
        var items = args.Split(',');

        foreach (var item in items)
        {
            var parts = item.Trim().Split(' ');
            var quantity = int.Parse(parts[0]);
            var vaisseauType = parts[1];
            order[vaisseauType] = quantity;
        }

        return order;
    }
}
