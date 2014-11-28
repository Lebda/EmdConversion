using System;
using System.Collections.Generic;
using System.Windows;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Impl;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdShapeData : IXEP_EmdShapeData
    {
        public XEP_EmdShapeData()
        {
            Points = new List<IXEP_EmdPointData>();
        }

        #region PROPERTIES
        public List<IXEP_EmdPointData> Points { get; set; }
        #endregion

        #region INTERFACE IMPL
        public void CreateFrom(List<Point> shape)
        {
            Points.Clear();
            foreach (var item in shape)
            {
                IXEP_EmdPointData pointData = XEP_EmdFactrory.CreateEmdPointData();
                pointData.CreateFrom(item);
                Points.Add(pointData);
            }
        }
        public List<Point> Create()
        {
            List<Point> retVal = new List<Point>();
            foreach (var item in Points)
            {
                retVal.Add(item.Create());
            }
            return retVal;
        }
        public IXEP_EmdElement CreateEmdElement()
        {
            IXEP_EmdElement elem = XEP_EmdFactrory.CreateEmdElement();
            elem.Name = XEP_EmdNames.s_KeyShape;
            foreach (var item in Points)
            {
                elem.Elements.Add(item.CreateEmdElement());
            }
            return elem;
        }
        public void CreateFromEmdElement(IXEP_EmdElement elem)
        {
            XEP_BaseEmdFile.CheckName(elem.Name, XEP_EmdNames.s_KeyShape);
            Points.Clear();
            foreach (var item in elem.Elements)
            {
                IXEP_EmdPointData pointData = XEP_EmdFactrory.CreateEmdPointData();
                pointData.CreateFromEmdElement(item);
                Points.Add(pointData);
            }
        }
        #endregion
    }
}
