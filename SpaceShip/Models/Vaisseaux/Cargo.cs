using ShipFactoryApp.Models.Pieces;
using System.Collections.Generic;

namespace ShipFactoryApp.Models.Vaisseaux
{
    public class Cargo : Vaisseau
    {
        public Cargo()
        {
            Coque = new Coque("Hull_HC1", 1);
            Moteur = new Moteur("Engine_EC1", 1);
            Ailes = new Ailes("Wings_WC1", 1);
            Propulseurs = new List<Propulseur> { new Propulseur("Thruster_TC1", 1) };
        }
    }
}
