using System.Collections.Generic;
using System.Windows;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdShapeData : IXEP_EmdElemConventor
    {
        List<Point> Create();
        void CreateFrom(List<Point> shape);
        List<IXEP_EmdPointData> Points { get; }
    }
}