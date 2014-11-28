using System.Collections.Generic;

namespace XEP_EsaModelData.General.Interface
{
    internal interface IXEP_EmdDom
    {
        IXEP_EmdLine Root { get; }
        void CreateDom(List<IXEP_EmdLine> linesEmd);
    }
}