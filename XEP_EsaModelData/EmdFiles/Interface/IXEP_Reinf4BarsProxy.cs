using System;
using System.Linq;
using XEP_EsaModelData.General.Interface;

namespace XEP_EsaModelData.EmdFiles.Interface
{
    internal interface IXEP_Reinf4BarsProxy
    {
        IXEP_EmdElement Reinf4Bars { get; }
        int SectionID { set; }
    }
}