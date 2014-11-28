using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XEP_EsaModelData.EmdData.Inputs;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal class XEP_ReinforcementEmdFile : XEP_BaseEmdFile, IXEP_ReinforcementEmdFile
    {
        public XEP_ReinforcementEmdFile()
            : base(XEP_EmdNames.s_KeyReinforcement)
        {
            m_reinfBarsProxy = null;
            m_reinfStirrupsProxy = null;
            m_zonePreparator = XEP_EmdFactrory.CreateStirrupZonePreparator();
            m_baseMaterial = String.Empty;
        }
        
        #region MEMBERS
        private IXEP_Reinf4BarsProxy m_reinfBarsProxy;
        private IXEP_Reinf4StirrupsProxy m_reinfStirrupsProxy;
        private readonly IXEP_StirrupZonePreparator m_zonePreparator;
        string m_baseMaterial;
        #endregion

        #region INTERFACE IMPL
        public override void Load(Stream stream)
        {
            base.Load(stream);
            m_reinfBarsProxy = XEP_EmdFactrory.CreateReinf4BarsProxy(DocumentRoot);
            m_reinfStirrupsProxy = XEP_EmdFactrory.CreateReinf4StirrupsProxy(DocumentRoot);
        }
        public void PrepareDocument(double sectionPos, string baseMaterial, double memberLenght, int sectionID)
        {
            m_baseMaterial = baseMaterial;
            m_zonePreparator.PrepareZones(m_reinfStirrupsProxy.Reinf4Stirrups, sectionPos, m_baseMaterial, 0.0, memberLenght);
            m_reinfBarsProxy.SectionID = sectionID;
        }
        public bool IsReinforcementInputed()
        {
            if (m_reinfBarsProxy.Reinf4Bars.Elements.Count == 0 && m_reinfStirrupsProxy.Reinf4Stirrups.Elements.Count == 0)
            {
		        return false;
            }
            return true;
        }
        public void SetBars(List<IXEP_BarIO> bars, int sectionID = -1)
        {
            if (sectionID != -1)
            {
                m_reinfBarsProxy.SectionID = sectionID;   
            }
            RemoveElements(m_reinfBarsProxy.Reinf4Bars, XEP_EmdNames.s_KeyBar);
            foreach (var bar in bars)
            {
                IXEP_EmdBarData barData = XEP_EmdFactrory.CreateEmdBarData();
                barData.CreateFrom(bar, 0, m_baseMaterial, 1, 0);
                m_reinfBarsProxy.Reinf4Bars.Elements.Add(barData.CreateEmdElement());
            }
        }
        public List<IXEP_BarIO> GetBars(int sectionID = -1)
        {
            if (sectionID != -1)
            {
                m_reinfBarsProxy.SectionID = sectionID;
            }
            List<IXEP_BarIO> retVal = new List<IXEP_BarIO>();
            List<IXEP_EmdElement> domBars = GetElements(m_reinfBarsProxy.Reinf4Bars, XEP_EmdNames.s_KeyBar);
            foreach (var domBar in domBars)
            {
                IXEP_EmdBarData barData = XEP_EmdFactrory.CreateEmdBarData();
                barData.CreateFromEmdElement(domBar);
                retVal.Add(barData.Create());
            }
            return retVal;
        }
        public IXEP_StirrupZoneIO GetStirrupZoneIO()
        {
            return m_zonePreparator.GetStirrupZoneIO();
        }
        public void SetStirrupZoneIO(IXEP_StirrupZoneIO zoneIO, bool insertShapeInAfterAndBefore)
        {
            m_zonePreparator.SetStirrupZoneIO(zoneIO, insertShapeInAfterAndBefore);
        }
        #endregion
        
        #region PRIVATE
        #endregion
    }
}