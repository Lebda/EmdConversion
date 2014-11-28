using System.Collections.Generic;
using XEP_EsaModelData.EmdData.Interface;

namespace XEP_EsaModelData.EmdFiles.Interface
{
    public interface IXEP_IternalForcesEmdFile : IXEP_BaseEmdFile
    {
        void PrepareDocument(int sectionID);
        List<IXEP_EmdInternalForcesData> GetInternalForces(int sectionID = -1);
    }
}