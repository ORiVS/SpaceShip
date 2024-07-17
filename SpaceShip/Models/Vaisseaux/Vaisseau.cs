using ShipFactoryApp.Models.Pieces;
using System.Collections.Generic;

namespace ShipFactoryApp.Models.Vaisseaux
{
    public abstract class Vaisseau
    {
        public Coque Coque { get; protected set; }
        public Moteur Moteur { get; protected set; }
        public Ailes Ailes { get; protected set; }
        public List<Propulseur> Propulseurs { get; protected set; }
    }
}
