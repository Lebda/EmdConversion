using System.Collections.Generic;
using XEP_EsaModelData.General.Interface;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdGeometryData
    {
        void CreateFrom(List<KeyValuePair<string, string>> items);
        IXEP_EmdElement CreateEmdElement();
    }
}