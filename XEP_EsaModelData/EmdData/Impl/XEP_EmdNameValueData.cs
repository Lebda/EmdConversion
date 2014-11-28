using System;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdNameValueData : IXEP_EmdNameValueData
    {
        public XEP_EmdNameValueData()
        {
            Name = String.Empty;
            Value = String.Empty;
        }

        #region PROPERTIES
        public string Name { get; set; }
        public string Value { get; set; }
        #endregion

        #region INTERFACE IMPL
        public IXEP_EmdAttribute CreateAttribute()
        {
            IXEP_EmdAttribute attEmd = XEP_EmdFactrory.CreateEmdAttribute();
            attEmd.Name = Name;
            attEmd.Value = Value;
            return attEmd;
        }
        #endregion
    }
}
