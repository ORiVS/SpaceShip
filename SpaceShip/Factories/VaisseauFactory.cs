using ShipFactoryApp.Models.Vaisseaux;
using System;

namespace ShipFactoryApp.Factories
{
    public class VaisseauFactory
    {
        public static IVaisseau CreateVaisseau(string vaisseauType)
        {
            switch (vaisseauType)
            {
                case "Explorer":
                    return new Exploreur();
                case "Speeder":
                    return new Speeder();
                case "Cargo":
                    return new Cargo();
                default:
                    throw new ArgumentException($"Vaisseau type `{vaisseauType}` non reconnu.");
            }
        }
    }
}
