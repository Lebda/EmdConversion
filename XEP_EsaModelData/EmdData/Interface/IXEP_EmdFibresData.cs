using System.Collections.Generic;
using System.Windows;
using XEP_EsaModelData.General.Interface;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdFibresData
    {
        void CreateFrom(List<Point> fibres);
        IXEP_EmdElement CreateEmdElement();
    }
}