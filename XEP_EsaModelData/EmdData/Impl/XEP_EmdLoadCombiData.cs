using System;
using System.Linq;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Impl;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdLoadCombiData : IXEP_EmdLoadCombiData
    {
        public XEP_EmdLoadCombiData()
        {
            ID = 0;
            Type = 1;
            LimitState = 1;
            CombiKey = String.Empty;
            CombiType = 4;
        }

        public override string ToString()
        {
            return
                  "ID: " + ID.ToString() + "|" +
                  "Type: " + Type.ToString() + "|" +
                  "LimitState: " + LimitState.ToString() + "|" +
                  "CombiKey: " + CombiKey + "|" +
                  "CombiType: " + CombiType;
        }

        #region PROPERTIES
        public int ID { get; private set; }
        public int Type { get; private set; }
        public int LimitState { get; private set; }
        public string CombiKey { get; private set; }
        public int CombiType { get; private set; }
        #endregion

        #region INTERFACE IMPL
        public IXEP_EmdElement CreateEmdElement()
        {
            IXEP_EmdElement elemEmd = XEP_EmdFactrory.CreateEmdElement();
            elemEmd.Name = XEP_EmdNames.s_KeyLoad;
            //
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyLoadID, ID.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyLoadType, Type.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyLoadLimitState, LimitState.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyLoadCombiKey, CombiKey));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyLoadCombiType, CombiType.ToString()));
            return elemEmd;
        }
        public void CreateFromEmdElement(IXEP_EmdElement elem)
        {
            XEP_BaseEmdFile.CheckName(elem.Name, XEP_EmdNames.s_KeyLoad);
            ID = elem.GetAttribute(XEP_EmdNames.s_KeyLoadID).GetIntValue();
            Type = elem.GetAttribute(XEP_EmdNames.s_KeyLoadType).GetIntValue();
            if (elem.AttHelp.Count > 2)
            {
                LimitState = elem.GetAttribute(XEP_EmdNames.s_KeyLoadLimitState).GetIntValue();
                CombiKey = elem.GetAttribute(XEP_EmdNames.s_KeyLoadCombiKey).Value;
                CombiType = elem.GetAttribute(XEP_EmdNames.s_KeyLoadCombiType).GetIntValue();   
            }
        }
        #endregion
    }
}
