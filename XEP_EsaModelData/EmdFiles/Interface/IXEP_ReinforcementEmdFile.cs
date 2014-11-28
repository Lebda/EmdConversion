using System.Collections.Generic;
using XEP_EsaModelData.EmdData.Inputs;

namespace XEP_EsaModelData.EmdFiles.Interface
{
    public interface IXEP_ReinforcementEmdFile : IXEP_BaseEmdFile
    {
        void PrepareDocument(double sectionPos, string baseMaterial, double memberLenght, int sectionID);
        void SetBars(List<IXEP_BarIO> bars, int sectionID = -1);
        void SetStirrupZoneIO(IXEP_StirrupZoneIO zoneIO, bool insertShapeInAfterAndBefore);
        IXEP_StirrupZoneIO GetStirrupZoneIO();
        List<IXEP_BarIO> GetBars(int sectionID = -1);
        bool IsReinforcementInputed();
    }
}