using System;
using System.Collections.Generic;
using System.Linq;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal class XEP_InternalForcesEmdFile : XEP_BaseEmdFile, IXEP_IternalForcesEmdFile
    {
        public XEP_InternalForcesEmdFile()
            : base(XEP_EmdNames.s_KeySection)
        {
            m_sectionID = -1;
        }

        #region MEMBERS
        int m_sectionID;
        #endregion


        #region INTERFACE IMPL
        public void PrepareDocument(int sectionID)
        {
            m_sectionID = sectionID;
        }
        public List<IXEP_EmdInternalForcesData> GetInternalForces(int sectionID = -1)
        {
            if (sectionID != -1)
            {
                 m_sectionID = sectionID;           
            }
            if (m_sectionID < 0)
            {
                throw new InvalidOperationException("Section ID has to be possitive, input possitive number or call PrepareDocument with possitive number before !");
            }
            List<IXEP_EmdElement> domElems = GetElements(DocumentRoot, XEP_EmdNames.s_KeySection);
            if (DocumentRoot.Name == XEP_EmdNames.s_KeySection && domElems.Count == 0)
            { // only one section
                domElems.Add(DocumentRoot);
            }
            if (domElems == null || domElems.Count == 0)
            {
		        return new List<IXEP_EmdInternalForcesData>();
            }
            IXEP_EmdElement neededSection = null;
            foreach (var item in domElems)
            {
                int actID = item.GetAttribute(XEP_EmdNames.s_KeyID).GetIntValue();
                if (actID == m_sectionID)
                {
                    neededSection = item;
                    break;
                }
            }
            if (neededSection == null)
            {
                return new List<IXEP_EmdInternalForcesData>();
            }
            List<IXEP_EmdElement> domElemsInternalForces = GetElements(neededSection, XEP_EmdNames.s_KeyInternalForces);
            List<IXEP_EmdInternalForcesData> retVal = new List<IXEP_EmdInternalForcesData>();
            foreach (var item in domElemsInternalForces)
            {
                IXEP_EmdInternalForcesData emdData = XEP_EmdFactrory.CreateEmdInternalForcesData();
                emdData.CreateFromEmdElement(item);
                retVal.Add(emdData);  
            }
            return retVal;
        }
        #endregion
    }
}
