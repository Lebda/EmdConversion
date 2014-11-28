using System;
using System.Collections.Generic;
using System.Linq;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal class XEP_Reinf4BarsProxy : IXEP_Reinf4BarsProxy
    {
        public XEP_Reinf4BarsProxy(IXEP_EmdElement elem4Work)
        {
            m_elem4Work = elem4Work;
        }

        #region MEMBERS
        private int m_sectionID;
        private IXEP_EmdElement m_reinf4Bars;
        private readonly IXEP_EmdElement m_elem4Work;
        #endregion

        #region PROPERTIES
        public IXEP_EmdElement Reinf4Bars
        {
            get
            {
                if (m_reinf4Bars == null)
                {
                    if (m_sectionID < 0)
                    {
                        throw new InvalidOperationException("SectionID is invalid actual value is: " + m_sectionID.ToString());
                    }
                    m_reinf4Bars = FindReinforcementElement(m_elem4Work, m_sectionID);
                }
                return m_reinf4Bars;
            }
        }
        public int SectionID
        {
            set 
            { 
                if (m_sectionID != value)
	            {
		            m_reinf4Bars = null;
	            }
                m_sectionID = value;
            }
        }
        #endregion

        #region INTERFACE IMPL

        #endregion

        #region METHODS PRIVATE
        static IXEP_EmdElement FindReinforcementElement(IXEP_EmdElement elem4Work, int sectionID)
        {
            IXEP_EmdElement reinfElem = elem4Work;
            if (elem4Work.Name == XEP_EmdFileConstants.s_FakeRootElementName)
            { // more sections
                List<IXEP_EmdElement> sections = XEP_BaseEmdFile.GetElements(elem4Work, XEP_EmdNames.s_KeySection);
                IXEP_EmdElement needSection = sections.Where(item => item.GetAttribute(XEP_EmdNames.s_KeyID).GetIntValue() == sectionID).First();
                IXEP_EmdAttribute attID = needSection.GetAttribute(XEP_EmdNames.s_KeyID);
                if (attID == null)
                {
                    throw new InvalidOperationException("Section in reinforcement file does not have attribute: " + XEP_EmdNames.s_KeyID);
                }
                if (attID.Value != sectionID.ToString())
                {
                    throw new InvalidOperationException("Section ID attribute is not equals section ID inputed, actual:" + attID.Value + " wanted: " + sectionID.ToString());
                }
                reinfElem = XEP_BaseEmdFile.GetElement(needSection, XEP_EmdNames.s_KeyReinforcement);
            }
            if (reinfElem == null)
            {
                throw new InvalidOperationException("There is not reinforcemnt element for sectionID: " + sectionID.ToString());
            }
            XEP_BaseEmdFile.CheckName(reinfElem.Name, XEP_EmdNames.s_KeyReinforcement);
            return reinfElem;
        }
        #endregion
    }
}
