using ShipFactoryApp.Models.Pieces;
using System.Collections.Generic;

namespace ShipFactoryApp.Models.Vaisseaux
{
    public class Exploreur : Vaisseau
    {
        public Exploreur()
        {
            Coque = new Coque("Hull_HE1", 1);
            Moteur = new Moteur("Engine_EE1", 1);
            Ailes = new Ailes("Wings_WE1", 1);
            Propulseurs = new List<Propulseur> { new Propulseur("Thruster_TE1", 2) };
        }
    }
}
