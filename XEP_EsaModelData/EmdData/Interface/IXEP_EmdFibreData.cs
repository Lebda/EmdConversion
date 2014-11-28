using System;
using System.Windows;
using XEP_EsaModelData.General.Interface;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdFibreData
    {
        void CreateFrom(Point actPoint);
        IXEP_EmdElement CreateEmdElement();
        int Flag { get; set; }
        IXEP_EmdLcsData Lcs { get; set; }
        IXEP_EmdPrincipalData Principal { get; set; }
    }
}