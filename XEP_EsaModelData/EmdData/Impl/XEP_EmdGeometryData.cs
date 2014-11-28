using System;
using System.Collections.Generic;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdGeometryData : IXEP_EmdGeometryData
    {
        public XEP_EmdGeometryData()
        {
            Items = new List<IXEP_EmdNameValueData>();
        }

        #region PROPERTIES
        public List<IXEP_EmdNameValueData> Items { get; set; }
        #endregion

        #region INTERFACE IMPL
        public void CreateFrom(List<KeyValuePair<string, string>> items)
        {
            Items.Clear();
            foreach (var item in items)
            {
                IXEP_EmdNameValueData itemData = XEP_EmdFactrory.CreateEmdNameValueData();
                itemData.Name = item.Key;
                itemData.Value = item.Value;
                Items.Add(itemData);
            }
        }
        public IXEP_EmdElement CreateEmdElement()
        {
            IXEP_EmdElement elem = XEP_EmdFactrory.CreateEmdElement();
            elem.Name = XEP_EmdNames.s_KeyGeometry;
            foreach (var item in Items)
            {
                elem.AddAttribute(item.CreateAttribute());
            }
            return elem;
        }
        #endregion
    }
}
