using System;
using System.Collections.Generic;
using System.IO;

namespace ShipFactoryApp.Utilities
{
	public static class FileLoader
	{
		public static void LoadFromTextFile(string filename)
		{
			var lines = File.ReadAllLines(filename);
			var order = new Dictionary<string, int>();

			foreach (var line in lines)
			{
				var parts = line.Split(' ');
				if (parts.Length == 2)
				{
					string vaisseauType = parts[0];
					int quantity = int.Parse(parts[1]);
					order[vaisseauType] = quantity;
				}
			}

			Console.WriteLine("Commande chargée depuis le fichier texte.");
			DisplayOrder(order);
		}

		private static void DisplayOrder(Dictionary<string, int> order)
		{
			foreach (var item in order)
			{
				Console.WriteLine($"{item.Key} {item.Value}");
			}
		}
	}
}
