﻿using System;
using System.Collections.Generic;
using XEP_EsaModelData.EmdData.Inputs;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Impl;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdStirrupZoneData : IXEP_EmdStirrupZoneData
    {
        public XEP_EmdStirrupZoneData()
        {
            Clear();
        }

        #region PROPERTIES
        public List<IXEP_EmdStirrupData> Stirrups { get; set; }
        public IXEP_EmdStirrupZoneShapeData ZoneShape { get; set; }
        public int ZoneID { get; set; }
        public string Material { get; set; }
        public double ZoneBeg { get; set; }
        public double ZoneEnd { get; set; }
        public int IsAutoCutsCalc { get; set; }
        public int NumCutUser { get; set; }
        public string Position { get; set; }
        public double Coeff { get; set; }
        #endregion

        #region INTERFACE IMPL
        public IXEP_StirrupZoneIO Create(double sectionPos)
        {
            IXEP_StirrupZoneIO zoneInput = XEP_EmdFactrory.CreateStirrupZoneIO();
            zoneInput.Shapes = ZoneShape.Create();
            zoneInput.NumCut = NumCutUser;
            IXEP_EmdStirrupData nearestStirrup = FindNearestStirrup(sectionPos);
            if (nearestStirrup == null)
            {
                zoneInput.Spacing = 0.0;
                zoneInput.StirrupDiameter = 0.0;
            }
            else
            {
                zoneInput.Spacing = nearestStirrup.Dss;
                zoneInput.StirrupDiameter = nearestStirrup.D;
            }
            return zoneInput;
        }
        public void CreateFrom(IXEP_StirrupZoneIO zoneInput, double sectionPos, string matName, double zoneBeg, double zoneEnd, int zoneID)
        {
            ZoneID = zoneID;
            Material = matName;
            ZoneBeg = zoneBeg;
            ZoneEnd = zoneEnd;
            IsAutoCutsCalc = 1;
            NumCutUser = zoneInput.NumCut;
            Position = XEP_EmdNames.s_Value_ZonePosCurrent;
            Coeff = 4.0;
            ZoneShape.CreateFrom(zoneInput.Shapes);
            CreateStirrups(zoneInput, sectionPos);
        }
        public IXEP_EmdElement CreateEmdElement()
        {
            IXEP_EmdElement elem = XEP_EmdFactrory.CreateEmdElement();
            elem.Name = XEP_EmdNames.s_KeyStirrupZone;
            foreach (var item in Stirrups)
            {
                elem.Elements.Add(item.CreateEmdElement());
            }
            elem.Elements.Add(ZoneShape.CreateEmdElement());
            elem.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyStirrupZoneID, ZoneID.ToString()));
            elem.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdFileConstants.s_RefAttributEmdTag + XEP_EmdNames.s_KeyStirrupZoneMaterial, Material));
            elem.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyStirrupZoneZoneBeg, ZoneBeg.ToString()));
            elem.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyStirrupZoneZoneEnd, ZoneEnd.ToString()));
            elem.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyStirrupZoneIsAutoCutsCalc, IsAutoCutsCalc.ToString()));
            elem.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyStirrupZoneNumCutUser, NumCutUser.ToString()));
            elem.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyStirrupZonePosition, Position));
            elem.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyStirrupZoneCoef, Coeff.ToString()));
            return elem;
        }
        public void CreateFromEmdElement(IXEP_EmdElement elem)
        {
            XEP_BaseEmdFile.CheckName(elem.Name, XEP_EmdNames.s_KeyStirrupZone);
            Clear();
            foreach (var item in elem.Elements)
            {
                if (item.Name == XEP_EmdNames.s_KeyStirrup)
                {
                    IXEP_EmdStirrupData stirrupData = XEP_EmdFactrory.CreateEmdStirrupData();
                    stirrupData.CreateFromEmdElement(item);
                    Stirrups.Add(stirrupData);   
                }
                else if (item.Name == XEP_EmdNames.s_KeyStirrupZoneShape)
                {
                    ZoneShape.CreateFromEmdElement(item);
                }
                else
                {
                    throw new InvalidOperationException("Invalid element in stirrup zone, element name: " + item.Name);
                }
            }
            ZoneID = elem.GetAttribute(XEP_EmdNames.s_KeyStirrupZoneID).GetIntValue();
            Material = elem.GetAttribute(XEP_EmdFileConstants.s_RefAttributEmdTag + XEP_EmdNames.s_KeyStirrupZoneMaterial).Value;
            ZoneBeg = elem.GetAttribute(XEP_EmdNames.s_KeyStirrupZoneZoneBeg).GetDoubleValue();
            ZoneEnd = elem.GetAttribute(XEP_EmdNames.s_KeyStirrupZoneZoneEnd).GetDoubleValue();
            IsAutoCutsCalc = elem.GetAttribute(XEP_EmdNames.s_KeyStirrupZoneIsAutoCutsCalc).GetIntValue();
            NumCutUser = elem.GetAttribute(XEP_EmdNames.s_KeyStirrupZoneNumCutUser).GetIntValue();
            Position = elem.GetAttribute(XEP_EmdNames.s_KeyStirrupZonePosition).Value;
            Coeff = elem.GetAttribute(XEP_EmdNames.s_KeyStirrupZoneCoef).GetDoubleValue();
        }
        #endregion

        #region METHODS PRIVATE
        private void Clear()
        {
            ZoneID = -1;
            Material = String.Empty;
            ZoneBeg = 0.0;
            ZoneEnd = 0.0;
            IsAutoCutsCalc = 1;
            NumCutUser = 2;
            Position = XEP_EmdNames.s_Value_ZonePosCurrent;
            Coeff = 4.0;
            ZoneShape = XEP_EmdFactrory.CreateEmdStirrupZoneShapeData();
            Stirrups = new List<IXEP_EmdStirrupData>();
        }
        private IXEP_EmdStirrupData FindNearestStirrup(double sectionPos)
        {
            if (Stirrups == null || Stirrups.Count == 0)
            {
                return null;
            }
            double minDistance = Double.MaxValue;
            IXEP_EmdStirrupData nearest = null;
            foreach (var item in Stirrups)
            {
                double actDistance = Math.Abs(sectionPos - item.DX);
                if (actDistance < minDistance)
                {
                    minDistance = actDistance;
                    nearest = item;
                }
            }
            return nearest;
        }
        private void CreateStirrups(IXEP_StirrupZoneIO zoneInput, double sectionPos)
        {
            Stirrups = new List<IXEP_EmdStirrupData>();
            if (zoneInput.Spacing <= 0.0)
            {
                return;
            }
            double memberLenght = Math.Abs(ZoneBeg - ZoneEnd);
            if ((sectionPos) < (ZoneBeg + memberLenght / 2))
            {
                CreateStirrupsFromBegin(zoneInput);   
            }
            else
            {
                CreateStirrupsFromEnd(zoneInput);  
            }
        }
        private void CreateStirrupsFromBegin(IXEP_StirrupZoneIO zoneInput)
        {
            double halfSpacing = zoneInput.Spacing / 2;
            double actStirrupDx = ZoneBeg + halfSpacing;
            while (actStirrupDx < ZoneEnd)
            {
                DoOneStep(actStirrupDx, halfSpacing, zoneInput.StirrupDiameter);
                actStirrupDx += zoneInput.Spacing;
            }
        }
        private void CreateStirrupsFromEnd(IXEP_StirrupZoneIO zoneInput)
        {
            double halfSpacing = zoneInput.Spacing / 2;
            double actStirrupDx = ZoneEnd - halfSpacing;
            while (actStirrupDx > ZoneBeg)
            {
                DoOneStep(actStirrupDx, halfSpacing, zoneInput.StirrupDiameter);
                actStirrupDx -= zoneInput.Spacing;
            }
        }
        private void DoOneStep(double actStirrupDx, double halfSpacing, double diam)
        {
            IXEP_EmdStirrupData stirrupFirst = XEP_EmdFactrory.CreateEmdStirrupData();
            stirrupFirst.DsL = actStirrupDx - halfSpacing;
            if (stirrupFirst.DsL < ZoneBeg)
            {
                stirrupFirst.DsL = ZoneBeg;
            }
            stirrupFirst.DsR = actStirrupDx + halfSpacing;
            if (stirrupFirst.DsR > ZoneEnd)
            {
                stirrupFirst.DsR = ZoneEnd;
            }
            stirrupFirst.Dss = (stirrupFirst.DsR - stirrupFirst.DsL);
            stirrupFirst.DX = actStirrupDx;
            stirrupFirst.D = diam;
            stirrupFirst.Multiply = 1;
            Stirrups.Add(stirrupFirst);
        }
        #endregion
    }
}
