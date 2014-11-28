using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace XEP_EsaModelData.General.Interface
{
    public interface IXEP_EmdElement
    {
        string Name { get; set; }
        List<IXEP_EmdElement> Elements { get; }
        /// <summary>
        /// Just copy of attributes => e.g. clear, add will not take effect
        /// </summary>
        List<IXEP_EmdAttribute> AttHelp { get; }
        void AddAttribute(IXEP_EmdAttribute att);
        IXEP_EmdAttribute GetAttribute(string attName);
        void LoadEmdElement(IXEP_EmdElement elem);
        void Save(StreamWriter fileWriter, XEP_EsaModelData.General.Interface.IXEP_EmdIntendationGetter intendationGetter);
        void LoadXElement(XElement elem);
        XElement CreateXElement();
    }
}