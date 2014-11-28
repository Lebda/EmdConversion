using System;
using System.Windows;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdFibreData : IXEP_EmdFibreData
    {
        public XEP_EmdFibreData()
        {
            Flag = 0;
            Lcs = XEP_EmdFactrory.CreateEmdLcsData();
            Principal = XEP_EmdFactrory.CreateIEmdPrincipalData();
        }

        #region PROPERTIES
        public int Flag { get; set; }
        public IXEP_EmdLcsData Lcs { get; set; }
        public IXEP_EmdPrincipalData Principal { get; set; }
        #endregion

        #region INTERFACE IMPL
        public IXEP_EmdElement CreateEmdElement()
        {
            IXEP_EmdElement elemEmd = XEP_EmdFactrory.CreateEmdElement();
            elemEmd.Name = XEP_EmdNames.s_KeyFibre;
            IXEP_EmdAttribute attEmd = XEP_EmdFactrory.CreateEmdAttribute();
            attEmd.Name = XEP_EmdNames.s_KeyFlag;
            attEmd.Value = Flag.ToString();
            elemEmd.AddAttribute(attEmd);
            //
            elemEmd.Elements.Add(Lcs.CreateEmdElement());
            elemEmd.Elements.Add(Principal.CreateEmdElement());
            return elemEmd;
        }
        public void CreateFrom(Point actPoint)
        {
            Flag = 0;
            Lcs.CreateFrom(actPoint);
            Principal.CreateFrom(actPoint);
        }
        #endregion
    }
}
