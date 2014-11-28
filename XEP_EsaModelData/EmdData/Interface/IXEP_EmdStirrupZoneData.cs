using System;
using System.Collections.Generic;
using XEP_EsaModelData.EmdData.Inputs;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdStirrupZoneData : IXEP_EmdElemConventor
    {
        void CreateFrom(IXEP_StirrupZoneIO zoneInput, double sectionPos, string matName, double zoneBeg, double zoneEnd, int zoneID);
        IXEP_StirrupZoneIO Create(double sectionPos);
        List<IXEP_EmdStirrupData> Stirrups { get; set; }
        IXEP_EmdStirrupZoneShapeData ZoneShape { get; set; }
        int ZoneID { get; set; }
        string Material { get; set; }
        double ZoneBeg { get; set; }
        double ZoneEnd { get; set; }
        int IsAutoCutsCalc { get; set; }
        int NumCutUser { get; set; }
        string Position { get; set; }
    }
}