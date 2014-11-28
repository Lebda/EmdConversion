using System.Collections.Generic;
using System.Windows;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdStirrupBranchData : IXEP_EmdElemConventor
    {
        void CreateFrom(List<Point> shape);
        List<Point> Create();
    }
}