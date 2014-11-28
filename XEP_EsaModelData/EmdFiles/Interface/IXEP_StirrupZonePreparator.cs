using System;
using XEP_EsaModelData.EmdData.Inputs;
using XEP_EsaModelData.General.Interface;

namespace XEP_EsaModelData.EmdFiles.Interface
{
    public interface IXEP_StirrupZonePreparator
    {
        void SetStirrupZoneIO(IXEP_StirrupZoneIO zoneIO, bool insertShapeInAfterAndBefore);
        IXEP_StirrupZoneIO GetStirrupZoneIO();
        void PrepareZones(IXEP_EmdElement reinf4Stirrups, double sectionPos, string matName, double zoneBeg, double zoneEnd);

    }
}