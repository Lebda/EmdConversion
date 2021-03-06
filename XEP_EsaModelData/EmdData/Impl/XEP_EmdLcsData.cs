﻿using System;
using System.Windows;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Impl;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdLcsData : IXEP_EmdLcsData
    {
        public XEP_EmdLcsData()
        {
            y = 0.0;
            z = 0.0;
        }

        #region PROPERTIES
        public double y { get; set; }
        public double z { get; set; }
        #endregion

        #region INTERFACE IMPL
        public void CreateFrom(Point actPoint)
        {
            y = actPoint.X;
            z = actPoint.Y;
        }
        public Point Create()
        {
            Point retVal = new Point();
            retVal.X = y;
            retVal.Y = z;
            return retVal;
        }
        public IXEP_EmdElement CreateEmdElement()
        {
            IXEP_EmdElement elemEmd = XEP_EmdFactrory.CreateEmdElement();
            elemEmd.Name = XEP_EmdNames.s_KeyLcs;
            //
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeysmallY, y.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeysmallZ, z.ToString()));
            return elemEmd;
        }
        public void CreateFromEmdElement(IXEP_EmdElement elem)
        {
            XEP_BaseEmdFile.CheckName(elem.Name, XEP_EmdNames.s_KeyPrincipal);
            y = elem.GetAttribute(XEP_EmdNames.s_KeysmallY).GetDoubleValue();
            z = elem.GetAttribute(XEP_EmdNames.s_KeysmallZ).GetDoubleValue();
        }
        #endregion
    }
}
