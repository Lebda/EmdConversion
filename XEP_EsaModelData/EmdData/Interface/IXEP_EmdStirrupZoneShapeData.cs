using System.Collections.Generic;
using System.Windows;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdStirrupZoneShapeData : IXEP_EmdElemConventor
    {
        void CreateFrom(List<List<Point>> shape);
        List<List<Point>> Create();
    }
}