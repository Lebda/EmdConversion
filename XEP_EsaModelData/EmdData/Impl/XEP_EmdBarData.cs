﻿using System;
using XEP_EsaModelData.EmdData.Inputs;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Impl;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdBarData : IXEP_EmdBarData
    {
        public XEP_EmdBarData()
        {
            ComponentID = 0;
            X = 0.0;
            Y = 0.0;
            D = 0.0;
            RefMaterial = String.Empty;
            IsActive = 1;
            IsDetailing = 0;
        }

        public override string ToString()
        {
            return
                  "ComponentID: " + ComponentID.ToString() + "|" +
                  "X: " + X.ToString() + "|" +
                  "Y: " + Y.ToString() + "|" +
                  "D: " + D.ToString() + "|" +
                  "RefMaterial: " + RefMaterial + "|" +
                  "IsActive: " + IsActive.ToString() + "|" +
                  "IsDetailing: " + IsDetailing.ToString();
        }

        #region PROPERTIES
        public int ComponentID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double D { get; set; }
        public string RefMaterial { get; set; }
        public int IsActive { get; set; }
        public int IsDetailing { get; set; }
        #endregion

        #region INTERFACE IMPL
        public IXEP_BarIO Create()
        {
            return XEP_EmdFactrory.CreateBarIO(X, Y, D, (3.14159265358979 * D * D / 4));
        }
        public void CreateFrom(IXEP_BarIO barIO, int compID, string matName, int isActive, int isDetailing)
        {
            ComponentID = compID;
            X = barIO.X;
            Y = barIO.Y;
            D = barIO.D;
            RefMaterial = matName;
            IsActive = isActive;
            IsDetailing = isDetailing;
        }
        public IXEP_EmdElement CreateEmdElement()
        {
            IXEP_EmdElement elemEmd = XEP_EmdFactrory.CreateEmdElement();
            elemEmd.Name = XEP_EmdNames.s_KeyBar;
            //
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyBarComponentID, ComponentID.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyBarX, X.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyBarY, Y.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyBarD, D.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdFileConstants.s_RefAttributEmdTag + XEP_EmdNames.s_KeyBarMaterial, RefMaterial));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyBarIsActive, IsActive.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyBarIsDetailing, IsDetailing.ToString()));
            return elemEmd;
        }
        public void CreateFromEmdElement(IXEP_EmdElement elem)
        {
            XEP_BaseEmdFile.CheckName(elem.Name, XEP_EmdNames.s_KeyBar);
            ComponentID = elem.GetAttribute(XEP_EmdNames.s_KeyBarComponentID).GetIntValue();
            X = elem.GetAttribute(XEP_EmdNames.s_KeyBarX).GetDoubleValue();
            Y = elem.GetAttribute(XEP_EmdNames.s_KeyBarY).GetDoubleValue();
            D = elem.GetAttribute(XEP_EmdNames.s_KeyBarD).GetDoubleValue();
            RefMaterial = elem.GetAttribute(XEP_EmdFileConstants.s_RefAttributEmdTag + XEP_EmdNames.s_KeyBarMaterial).Value;
            IsActive = elem.GetAttribute(XEP_EmdNames.s_KeyBarIsActive).GetIntValue();
            IsDetailing = elem.GetAttribute(XEP_EmdNames.s_KeyBarIsDetailing).GetIntValue();
        }
        #endregion
    }
}
