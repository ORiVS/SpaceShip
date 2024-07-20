using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace ShipFactoryApp.Utilities
{
  public static class XmlLoader
  {
    public static void LoadFromXmlFile(string filename)
    {
      var doc = XDocument.Load(filename);
      var order = new Dictionary<string, int>();

      foreach (var element in doc.Root.Elements("Order"))
      {
        string vaisseauType = element.Element("Type").Value;
        int quantity = int.Parse(element.Element("Quantity").Value);
        order[vaisseauType] = quantity;
      }

      Console.WriteLine("Commande chargée depuis le fichier XML.");
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
