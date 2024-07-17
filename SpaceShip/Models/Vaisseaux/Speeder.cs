using ShipFactoryApp.Models.Pieces;
using System.Collections.Generic;

namespace ShipFactoryApp.Models.Vaisseaux
{
    public class Speeder : Vaisseau
    {
        public Speeder()
        {
            Coque = new Coque("Hull_HS1", 1);
            Moteur = new Moteur("Engine_ES1", 1);
            Ailes = new Ailes("Wings_WS1", 1);
            Propulseurs = new List<Propulseur> { new Propulseur("Thruster_TS1", 2) };
        }
    }
}
