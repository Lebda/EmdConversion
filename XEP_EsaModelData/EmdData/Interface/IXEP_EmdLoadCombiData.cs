using System;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdLoadCombiData : IXEP_EmdElemConventor
    {
        int ID { get; }
        int Type { get; }
        int LimitState { get; }
        string CombiKey { get; }
        int CombiType { get; }
    }
}