using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.General.Impl
{
    internal class XEP_EmdElement : IXEP_EmdElement
    {
        public XEP_EmdElement()
        {
            m_attributes = new Dictionary<string, XEP_EmdAttributeWithPos>();
            Elements = new List<IXEP_EmdElement>();
            Name = String.Empty;
            m_attCounter = 0;
        }

        public override string ToString()
        {
            return
                  "Name: " + Name + "|" +
                  "ElemCount: " + Elements.Count.ToString() + "|" +
                  "AttCount: " + m_attributes.Count.ToString();
        }
        #region MEMBERS
        private List<IXEP_EmdAttribute> m_attHelp;
        public List<IXEP_EmdAttribute> AttHelp
        {
            get
            {
                if (m_attHelp == null)
                {
                    m_attHelp = new List<IXEP_EmdAttribute>();
                    IOrderedEnumerable<KeyValuePair<string, XEP_EmdAttributeWithPos>> sortHelp = m_attributes.OrderBy(x => x.Value.Pos);
                    foreach (var item in sortHelp)
                    {
                        m_attHelp.Add(item.Value.Att);
                    }
                }
                return m_attHelp;
            }
            private set 
            {
                m_attHelp = value; 
            }
        }
        int m_attCounter;
        readonly Dictionary<string, XEP_EmdAttributeWithPos> m_attributes;
        #endregion

        #region PROPERTIES
        public string Name { get; set; }
        public List<IXEP_EmdElement> Elements { get; set; }
        #endregion
        
        #region INTERFACE IMPL
        public void AddAttribute(IXEP_EmdAttribute att)
        {
            AttHelp = null;
            m_attributes.Add(att.Name, new XEP_EmdAttributeWithPos(att, m_attCounter));
            m_attCounter++;
        }
        public IXEP_EmdAttribute GetAttribute(string attName)
        {
            if (m_attributes.ContainsKey(attName))
            {
                return m_attributes[attName].Att;
            }
            return null;
        }
        public XElement CreateXElement()
        {
            XElement xElem = new XElement(Name);
            foreach (var attribut in AttHelp)
            {
                xElem.Add(attribut.CreateXAttribute());
            }
            foreach (var childElem in Elements)
            {
                xElem.Add(childElem.CreateXElement());
            }
            return xElem;
        }
        public void Save(StreamWriter fileWriter, IXEP_EmdIntendationGetter intendationGetter)
        {
            fileWriter.Write(intendationGetter.GetIntendation() + XEP_EmdFileConstants.s_ElementStart + Name);
            foreach (var att in AttHelp)
            {
                att.Save(fileWriter);
            }
            fileWriter.Write(XEP_EmdFileConstants.s_ElementEnd);
            fileWriter.Write(Environment.NewLine);
            if (Elements.Count > 0)
            {
                IXEP_EmdIntendationGetter intendationGetter4MyElems = XEP_EmdFactrory.CreateEmdIntendationGetter();
                intendationGetter4MyElems.IntendationLevel = intendationGetter.IntendationLevel + 1;
                foreach (var elem in Elements)
                {
                    elem.Save(fileWriter, intendationGetter4MyElems);
                }
            }
        }
        public void LoadXElement(XElement elem)
        {
            Name = elem.Name.ToString();
            m_attributes.Clear();
            Elements.Clear();
            foreach (var actItem in elem.Attributes())
            {
                IXEP_EmdAttribute attribute = XEP_EmdFactrory.CreateEmdAttribute();
                attribute.LoadXAttribute(actItem);
                AddAttribute(attribute);
            }
            foreach (var actItem in elem.Elements())
            {
                IXEP_EmdElement elemEmd = XEP_EmdFactrory.CreateEmdElement();
                elemEmd.LoadXElement(actItem);
                Elements.Add(elemEmd);
            }
        }
        public void LoadEmdElement(IXEP_EmdElement elem)
        {
            Name = elem.Name;
            Elements.Clear();
            m_attributes.Clear();
            foreach (var attribut in elem.AttHelp)
            {
                AddAttribute(attribut);
            }
            foreach (var childElem in elem.Elements)
            {
                Elements.Add(childElem);
            }
        }
        #endregion    
  

        #region PRIVATE
        private class XEP_EmdAttributeWithPos
        {
            public XEP_EmdAttributeWithPos(IXEP_EmdAttribute att, int pos)
            {
                Att = att;
                Pos = pos;
            }
            public override string ToString()
            {
                return
                      "Pos: " + Pos + "|" +
                      "Att: " + Att.ToString();
            }
            public IXEP_EmdAttribute Att { get; set; }
            public int Pos { get; set; }
        }
        #endregion

    }
}