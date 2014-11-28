using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal class XEP_CrossSectionEmdFile : XEP_BaseEmdFile, IXEP_CrossSectionEmdFile
    {
        public XEP_CrossSectionEmdFile()
            : base(XEP_EmdNames.s_KeyCrossSection)
        {
        }
        
        #region INTERFACE IMPL
        public void SaveGeometry(List<KeyValuePair<string, string>> items)
        {
            IXEP_EmdGeometryData data = XEP_EmdFactrory.CreateEmdGeometryData();
            data.CreateFrom(items);
            SaveElement(DocumentRoot, data.CreateEmdElement(), XEP_EmdNames.s_KeyGeometry);
        }
        public void SaveShape(List<Point> points)
        {
            IXEP_EmdShapeData data = XEP_EmdFactrory.CreateEmdShapeData();
            data.CreateFrom(points);
            SaveElement(GetElement(DocumentRoot, XEP_EmdNames.s_KeyComponent), data.CreateEmdElement(), XEP_EmdNames.s_KeyShape);
        }
        public void SaveFibres(List<Point> fibres)
        {
            IXEP_EmdFibresData data = XEP_EmdFactrory.CreateEmdFibresData();
            data.CreateFrom(fibres);
            SaveElement(DocumentRoot, data.CreateEmdElement(), XEP_EmdNames.s_KeyFibres);
        }
        public List<Point> GetShape()
        {
            IXEP_EmdElement domCompElem = GetElement(DocumentRoot, XEP_EmdNames.s_KeyComponent);
            IXEP_EmdElement domShapeElem = GetElement(domCompElem, XEP_EmdNames.s_KeyShape);
            IXEP_EmdShapeData data = XEP_EmdFactrory.CreateEmdShapeData();
            data.CreateFromEmdElement(domShapeElem);
            return data.Create();
        }
        public int GetFormCode()
        {
            IXEP_EmdElement domElem = GetElement(DocumentRoot, XEP_EmdNames.s_KeyGeometry);
            return domElem.GetAttribute(XEP_EmdNames.s_KeyFormCode).GetIntValue();
        }
        #endregion
    }
}