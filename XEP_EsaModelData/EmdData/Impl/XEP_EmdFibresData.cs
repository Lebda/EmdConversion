using System;
using System.Collections.Generic;
using System.Windows;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdFibresData : IXEP_EmdFibresData
    {
        public XEP_EmdFibresData()
        {
            Fibres = new List<IXEP_EmdFibreData>();
        }

        #region PROPERTIES
        public List<IXEP_EmdFibreData> Fibres { get; set; }
        #endregion

        #region INTERFACE IMPL
        public void CreateFrom(List<Point> fibres)
        {
            Fibres.Clear();
            foreach (var item in fibres)
            {
                IXEP_EmdFibreData fibreData = XEP_EmdFactrory.CreateEmdFibreData();
                fibreData.CreateFrom(item);
                Fibres.Add(fibreData);
            }
        }
        public IXEP_EmdElement CreateEmdElement()
        {
            IXEP_EmdElement elem = XEP_EmdFactrory.CreateEmdElement();
            elem.Name = XEP_EmdNames.s_KeyFibres;
            foreach (var item in Fibres)
            {
                elem.Elements.Add(item.CreateEmdElement());
            }
            return elem;
        }
        #endregion
    }
}
