using System;
using System.Collections.Generic;
using System.Linq;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal class XEP_PlacesEmdFile : XEP_BaseEmdFile, IXEP_PlacesEmdFile
    {
        public XEP_PlacesEmdFile()
            : base(XEP_EmdNames.s_KeySection)
        {
        }

        #region INTERFACE IMPL
        public double GetSectionPos(int sectionID)
        {
            List<IXEP_EmdElement> domElems = GetElements(DocumentRoot, XEP_EmdNames.s_KeySection);
            if (DocumentRoot.Name != XEP_EmdFileConstants.s_FakeRootElementName && domElems.Count == 0)
            { // only one section
                domElems.Add(DocumentRoot);
            }
            foreach (var item in domElems)
            {
                int actID = item.GetAttribute(XEP_EmdNames.s_KeyID).GetIntValue();
                if (actID == sectionID)
                {
                    return item.GetAttribute(XEP_EmdNames.s_KeyDx).GetDoubleValue();
                }
            }
            return -1.0;
        }
        #endregion
    }
}
