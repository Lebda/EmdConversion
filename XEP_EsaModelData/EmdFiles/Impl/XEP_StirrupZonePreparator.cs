using System;
using System.Collections.Generic;
using System.Linq;
using XEP_EsaModelData.EmdData.Inputs;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal class XEP_StirrupZonePreparator : IXEP_StirrupZonePreparator
    {
        public XEP_StirrupZonePreparator()
        {
            m_zone4Work = XEP_EmdFactrory.CreateEmdStirrupZoneData();
            m_zonesBefore = new List<IXEP_EmdElement>();
            m_zonesAfter = new List<IXEP_EmdElement>();
            m_sectionPos = 0.0;
            m_zoneBeg = 0.0;
            m_zoneEnd = 0.0;
            m_baseMat = String.Empty;
            m_reinf4Stirrups = null;
        }

        #region MEMBERS
        static readonly int s_zoneIDstatic = 666;
        readonly List<IXEP_EmdElement> m_zonesBefore;
        readonly List<IXEP_EmdElement> m_zonesAfter;
        IXEP_EmdStirrupZoneData m_zone4Work;
        double m_sectionPos;
        double m_zoneBeg;
        double m_zoneEnd;
        string m_baseMat;
        IXEP_EmdElement m_reinf4Stirrups;
        #endregion

        #region INTERFACE IMPL
        public void SetStirrupZoneIO(IXEP_StirrupZoneIO zoneIO, bool insertShapeInAfterAndBefore)
        {
            if (m_zone4Work == null)
            {
                throw new InvalidOperationException("Zone4Work was not created, please call 'PrepareZones' ");
            }
            m_zone4Work.CreateFrom(zoneIO, m_sectionPos, m_baseMat, m_zoneBeg, m_zoneEnd, m_zone4Work.ZoneID);
            XEP_BaseEmdFile.RemoveElements(m_reinf4Stirrups, XEP_EmdNames.s_KeyStirrupZone);
            if (zoneIO.IsValid())
            {
                foreach (var zone in m_zonesBefore)
                {
                    m_reinf4Stirrups.Elements.Add(zone);
                }
                m_reinf4Stirrups.Elements.Add(m_zone4Work.CreateEmdElement());
                foreach (var zone in m_zonesAfter)
                {
                    m_reinf4Stirrups.Elements.Add(zone);
                }
                if (insertShapeInAfterAndBefore)
                {
                    foreach (var zone in m_zonesBefore)
                    {
                        XEP_BaseEmdFile.RemoveElements(zone, XEP_EmdNames.s_KeyStirrupZoneShape);
                        zone.Elements.Add(m_zone4Work.ZoneShape.CreateEmdElement());
                    }
                    foreach (var zone in m_zonesAfter)
                    {
                        XEP_BaseEmdFile.RemoveElements(zone, XEP_EmdNames.s_KeyStirrupZoneShape);
                        zone.Elements.Add(m_zone4Work.ZoneShape.CreateEmdElement());
                    }
                }
            }
        }
        public IXEP_StirrupZoneIO GetStirrupZoneIO()
        {
            if (m_zone4Work == null)
            {
               throw new InvalidOperationException("Zone4Work was not created, please call 'PrepareZones' ");             
            }
            return m_zone4Work.Create(m_sectionPos);
        }
        public void PrepareZones(IXEP_EmdElement reinf4Stirrups, double sectionPos, string baseMat, double zoneBeg, double zoneEnd)
        {
            m_zonesBefore.Clear();
            m_zonesAfter.Clear();
            m_zone4Work = null;
            m_sectionPos = sectionPos;
            m_baseMat = baseMat;
            m_zoneBeg = zoneBeg;
            m_zoneEnd = zoneEnd;
            m_reinf4Stirrups = reinf4Stirrups;
            XEP_BaseEmdFile.CheckName(m_reinf4Stirrups.Name, XEP_EmdNames.s_KeyReinforcement);
            List<IXEP_EmdElement> currentZoneCandidates = PrepareCurrentZoneCandidates();
            if (currentZoneCandidates == null || currentZoneCandidates.Count == 0)
            {
                PrepareZone4NoZones();
            }
            else if (currentZoneCandidates.Count == 1)
            {
                PrepareZoneFromElement(currentZoneCandidates[0]);
            }
            else
            {
                bool wasMatch = false;
                foreach (var zone in currentZoneCandidates)
                {
                    double actZoneBeg = zone.GetAttribute(XEP_EmdNames.s_KeyStirrupZoneZoneBeg).GetDoubleValue();
                    double actZoneEnd = zone.GetAttribute(XEP_EmdNames.s_KeyStirrupZoneZoneEnd).GetDoubleValue();
                    if (sectionPos > actZoneBeg && sectionPos < actZoneEnd)
                    {
                        PrepareZoneFromElement(zone);
                        wasMatch = true;
                        break;
                    }
                }
                if (!wasMatch)
                {
                    PrepareZoneFromElement(currentZoneCandidates[0]);
                }
            }
            if (m_zone4Work == null)
            {
                PrepareZone4NoZones();
            }
        }
        private List<IXEP_EmdElement> PrepareCurrentZoneCandidates()
        {
            List<IXEP_EmdElement> domStirrupZones = XEP_BaseEmdFile.GetElements(m_reinf4Stirrups, XEP_EmdNames.s_KeyStirrupZone);
            if (domStirrupZones == null || domStirrupZones.Count == 0)
            {
                return null;
            }
            List<IXEP_EmdElement> currentZoneCandidates = new List<IXEP_EmdElement>();
            XEP_BaseEmdFile.RemoveElements(m_reinf4Stirrups, XEP_EmdNames.s_KeyStirrupZone);
            SortZones(domStirrupZones, currentZoneCandidates);
            return currentZoneCandidates;
        }
        #endregion

        #region PRIVATE
        private void SortZones(List<IXEP_EmdElement> domStirrupZones, List<IXEP_EmdElement> currentZoneCandidates)
        {
            foreach (var zone in domStirrupZones)
            {
                string position = zone.GetAttribute(XEP_EmdNames.s_KeyStirrupZonePosition).Value;
                position = position.Replace(XEP_EmdFileConstants.s_AttributeValueStringStart, String.Empty);
                if (position == XEP_EmdNames.s_Value_ZonePosBefore)
                {
                    m_zonesBefore.Add(zone);
                }
                else if (position == XEP_EmdNames.s_Value_ZonePosAfter)
                {
                    m_zonesAfter.Add(zone);
                }
                else if (position == XEP_EmdNames.s_Value_ZonePosCurrent)
                {
                    currentZoneCandidates.Add(zone);
                }
            }
        }
        void PrepareZoneFromElement(IXEP_EmdElement zone)
        {
            m_zone4Work = XEP_EmdFactrory.CreateEmdStirrupZoneData();
            m_zone4Work.CreateFromEmdElement(zone);
            m_zone4Work.ZoneBeg = m_zoneBeg;
            m_zone4Work.ZoneEnd = m_zoneEnd;
        }
        void PrepareZone4NoZones()
        {
            m_zone4Work = XEP_EmdFactrory.CreateEmdStirrupZoneData();
            m_zone4Work.ZoneID = s_zoneIDstatic;
            m_zone4Work.ZoneBeg = m_zoneBeg;
            m_zone4Work.ZoneEnd = m_zoneEnd;
            m_zone4Work.Material = m_baseMat;
        }
        #endregion
    }
}
