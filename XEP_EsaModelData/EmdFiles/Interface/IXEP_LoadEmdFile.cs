using System.Collections.Generic;
using XEP_EsaModelData.EmdData.Interface;

namespace XEP_EsaModelData.EmdFiles.Interface
{
    public interface IXEP_LoadEmdFile : IXEP_BaseEmdFile
    {
        List<IXEP_EmdLoadCombiData> GetLoad();
    }
}