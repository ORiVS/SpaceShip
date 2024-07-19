using ShipFactoryApp.Models.Pieces;
using System.Collections.Generic;

namespace ShipFactoryApp.Models.Vaisseaux
{
    public interface IVaisseau
    {
        string GetVaisseauType();
        Coque Coque { get; set; }
        Moteur Moteur { get; set; }
        Ailes Ailes { get; set; }
        List<Propulseur> Propulseurs { get; set; }
    }

    public abstract class Vaisseau : IVaisseau
    {
        public abstract string GetVaisseauType();
        public Coque Coque { get; set; }
        public Moteur Moteur { get; set; }
        public Ailes Ailes { get; set; }
        public List<Propulseur> Propulseurs { get; set; }
    }
}
