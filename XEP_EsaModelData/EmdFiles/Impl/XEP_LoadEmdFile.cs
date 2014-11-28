using System;
using System.Collections.Generic;
using System.Linq;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal class XEP_LoadEmdFile : XEP_BaseEmdFile, IXEP_LoadEmdFile
    {
        public XEP_LoadEmdFile()
            : base(XEP_EmdNames.s_KeyLoad)
        {
        }

        #region INTERFACE IMPL
        public List<IXEP_EmdLoadCombiData> GetLoad()
        {
            List<IXEP_EmdElement> domElems = GetElements(DocumentRoot, XEP_EmdNames.s_KeyLoad);
            if (DocumentRoot.Name == XEP_EmdNames.s_KeyLoad && domElems.Count == 0)
            { // only one section
                domElems.Add(DocumentRoot);
            }
            List<IXEP_EmdLoadCombiData> retVal = new List<IXEP_EmdLoadCombiData>();
            foreach (var item in domElems)
            {
                IXEP_EmdLoadCombiData emdData = XEP_EmdFactrory.CreateEmdLoadData();
                emdData.CreateFromEmdElement(item);
                retVal.Add(emdData);
            }
            return retVal;
        }
        #endregion
    }
}
