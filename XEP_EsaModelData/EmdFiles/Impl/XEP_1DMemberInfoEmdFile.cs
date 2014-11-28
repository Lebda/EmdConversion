using System;
using System.Linq;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal class XEP_1DMemberInfoEmdFile : XEP_BaseEmdFile, IXEP_1DMemberInfoEmdFile
    {
        public XEP_1DMemberInfoEmdFile()
            : base(XEP_EmdNames.s_KeyMember)
        {
        }

        #region INTERFACE IMPL
        public double GetMemberLength()
        {
            IXEP_EmdElement domElem = GetElement(DocumentRoot, XEP_EmdNames.s_KeyLength);
            return domElem.GetAttribute(XEP_EmdFileConstants.s_NoNameAttribut).GetDoubleValue();
        }
        #endregion
    }
}
