using System;
using System.Linq;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal class XEP_Reinf4StirrupsProxy : IXEP_Reinf4StirrupsProxy
    {
        public XEP_Reinf4StirrupsProxy(IXEP_EmdElement elem4Work)
        {
            m_elem4Work = elem4Work;
        }

        #region MEMBERS
        private IXEP_EmdElement m_reinf4Stirrups;
        private readonly IXEP_EmdElement m_elem4Work;
        #endregion

        #region PROPERTIES
        public IXEP_EmdElement Reinf4Stirrups
        {
            get
            {
                if (m_reinf4Stirrups == null)
                {
                    m_reinf4Stirrups = FindReinforcementElement4StirrupZones(m_elem4Work);
                }
                return m_reinf4Stirrups;
            }
        }
        #endregion

        #region INTERFACE IMPL

        #endregion

        #region METHODS PRIVATE
        static IXEP_EmdElement FindReinforcementElement4StirrupZones(IXEP_EmdElement elem4Work)
        {
            IXEP_EmdElement reinfElem = elem4Work;
            if (elem4Work.Name == XEP_EmdFileConstants.s_FakeRootElementName)
            { // more sections
                reinfElem = XEP_BaseEmdFile.GetElement(elem4Work, XEP_EmdNames.s_KeyReinforcement);
            }
            if (reinfElem == null)
            {
                throw new InvalidOperationException("There is not reinforcemnt element for stirrups !");
            }
            XEP_BaseEmdFile.CheckName(reinfElem.Name, XEP_EmdNames.s_KeyReinforcement);
            return reinfElem;
        }
        #endregion
    }
}
