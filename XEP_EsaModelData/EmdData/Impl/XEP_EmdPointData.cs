using System;
using System.Windows;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Impl;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdPointData : IXEP_EmdPointData
    {
        public XEP_EmdPointData()
        {
            X = 0.0;
            Y = 0.0;
        }
        
        #region PROPERTIES
        public double X { get; set; }
        public double Y { get; set; }
        #endregion
        
        #region INTERFACE IMPL
        public void CreateFrom(Point actPoint)
        {
            X = actPoint.X;
            Y = actPoint.Y;
        }
        public Point Create()
        {
            Point retVal = new Point();
            retVal.X = X;
            retVal.Y = Y;
            return retVal;
        }
        public IXEP_EmdElement CreateEmdElement()
        {
            IXEP_EmdElement elemEmd = XEP_EmdFactrory.CreateEmdElement();
            elemEmd.Name = XEP_EmdNames.s_KeyPoint;
            //
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyPointX, X.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyPointY, Y.ToString()));
            return elemEmd;
        }
        public void CreateFromEmdElement(IXEP_EmdElement elem)
        {
            XEP_BaseEmdFile.CheckName(elem.Name, XEP_EmdNames.s_KeyPoint);
            X = elem.GetAttribute(XEP_EmdNames.s_KeyPointX).GetDoubleValue();
            Y = elem.GetAttribute(XEP_EmdNames.s_KeyPointY).GetDoubleValue();
        }
        #endregion
    }
}