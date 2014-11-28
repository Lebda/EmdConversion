using System;
using XEP_EsaModelData.EmdData.Inputs;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdBarData : IXEP_EmdElemConventor
    {
        IXEP_BarIO Create();
        void CreateFrom(IXEP_BarIO barIO, int compID, string matName, int isActive, int isDetailing);
        int ComponentID { get; set; }
        double X { get; set; }
        double Y { get; set; }
        double D { get; set; }
        string RefMaterial { get; set; }
        int IsActive { get; set; }
        int IsDetailing { get; set; }
    }
}