using System;
using System.IO;
using System.Xml.Linq;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.General.Impl
{
    internal class XEP_EmdDocument : IXEP_EmdDocument
    {
        public XEP_EmdDocument()
        {
            m_emdReader = XEP_EmdFactrory.CreateEmdFileReader();
        }

        #region MEMBERS
        readonly IXEP_EmdFileReader m_emdReader;
        #endregion
        
        #region PROPERTIES
        public IXEP_EmdElement Root { get; set; }
        #endregion
        
        #region INTERFACE IMPL
        public void Load(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                m_emdReader.Read(reader);
            }
            Root = m_emdReader.Root.CreateEmdElement();
        }
        public void Load(XDocument xDocument)
        {
            Root = XEP_EmdFactrory.CreateEmdElement();
            Root.LoadXElement(xDocument.Root);
        }
        public void Save(Stream stream)
        {
            using (StreamWriter fileWriter = new StreamWriter(stream))
            {
                if (Root.Name == XEP_EmdFileConstants.s_FakeRootElementName)
                {
                    foreach (var elem in Root.Elements)
                    {
                        IXEP_EmdIntendationGetter intendationGetter = XEP_EmdFactrory.CreateEmdIntendationGetter();
                        elem.Save(fileWriter, intendationGetter);
                    }
                }
                else
                {
                    IXEP_EmdIntendationGetter intendationGetter = XEP_EmdFactrory.CreateEmdIntendationGetter();
                    Root.Save(fileWriter, intendationGetter);
                }
            }
        }
        #endregion
    }
}