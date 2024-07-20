using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ShipFactoryApp.Utilities
{
	public static class JsonLoader
	{
		public static void LoadFromJsonFile(string filename)
		{
			string jsonData = File.ReadAllText(filename);
			var order = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonData);

			Console.WriteLine("Commande chargée depuis le fichier JSON.");
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
