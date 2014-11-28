using System;
using XEP_EsaModelData.General.Interface;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdNameValueData
    {
        IXEP_EmdAttribute CreateAttribute();
        string Name { get; set; }
        string Value { get; set; }
    }
}