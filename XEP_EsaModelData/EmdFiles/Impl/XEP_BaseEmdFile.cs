using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal abstract class XEP_BaseEmdFile
    {
        public XEP_BaseEmdFile(string rootName)
        {
            m_emdDocument = XEP_EmdFactrory.CreateEmdDocument();
            m_rootName = rootName;
        }

        #region MEMBERS
        readonly IXEP_EmdDocument m_emdDocument;
        readonly string m_rootName;
        #endregion

        #region INTERFACE IMPL
        public virtual void Load(Stream stream)
        {
            m_emdDocument.Load(stream);
            if (m_emdDocument.Root.Name != XEP_EmdFileConstants.s_FakeRootElementName)
            {
                CheckName(m_emdDocument.Root.Name, m_rootName);   
            }
        }
        public void Save(Stream stream)
        {
            m_emdDocument.Save(stream);
        }
        #endregion

        #region PROTECTED
        protected IXEP_EmdElement DocumentRoot 
        { 
            get 
            { 
                return m_emdDocument.Root; 
            } 
        }
        #endregion


        #region PROTECTED
        static protected void SaveElement(IXEP_EmdElement elem4Work, IXEP_EmdElement elem4Save, string name4Save)
        {
            CheckName(elem4Save.Name, name4Save);
            IXEP_EmdElement domEmdElem = GetElement(elem4Work, elem4Save.Name);
            if (domEmdElem == null)
            {
                elem4Work.Elements.Add(elem4Save);
            }
            else
            {
                domEmdElem.LoadEmdElement(elem4Save);
            }
        }
        static public void RemoveElements(IXEP_EmdElement elem4Work, string name)
        {
            List<IXEP_EmdElement> domEmdElems4Remove = elem4Work.Elements.Where(item => item.Name == name).ToList();
            if (domEmdElems4Remove == null || domEmdElems4Remove.Count == 0)
            {
                return;
            }
            foreach (var item in domEmdElems4Remove)
            {
                elem4Work.Elements.Remove(item);
            }
        }
        static public List<IXEP_EmdElement> GetElements(IXEP_EmdElement elem4Find, string name)
        {
            List<IXEP_EmdElement> domEmdElems = elem4Find.Elements.Where(item => item.Name == name).ToList();
            return domEmdElems;
        }
        static public IXEP_EmdElement GetElement(IXEP_EmdElement elem4Find, string name)
        {
            if (elem4Find.Elements == null || elem4Find.Elements.Count == 0)
            {
                return null;
            }
            IXEP_EmdElement domEmdElem = elem4Find.Elements.Where(item => item.Name == name).First();
            return domEmdElem;
        }
        static public void CheckName(string name4Check, string wantedName)
        {
            if (name4Check != wantedName)
            {
                throw new InvalidOperationException("Invalid name for element save, actual: " + name4Check + " ,wanted :" + wantedName + " !");
            }
        }
        #endregion

    }
}
