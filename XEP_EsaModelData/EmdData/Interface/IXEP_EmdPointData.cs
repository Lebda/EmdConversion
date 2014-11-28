using System;
using System.Windows;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdPointData : IXEP_EmdElemConventor
    {
        double X { get; }
        double Y { get; }
        void CreateFrom(Point actPoint);
        Point Create();
    }
}