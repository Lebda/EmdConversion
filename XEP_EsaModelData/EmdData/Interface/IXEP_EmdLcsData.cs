using System.Windows;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdLcsData : IXEP_EmdElemConventor
    {
        void CreateFrom(Point actPoint);
        Point Create();
        double y { get; }
        double z { get; }
    }
}