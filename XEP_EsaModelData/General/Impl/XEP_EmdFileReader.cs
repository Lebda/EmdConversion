using System;
using System.Collections.Generic;
using System.IO;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.General.Impl
{
    internal class XEP_EmdFileReader : IXEP_EmdFileReader
    {
        public XEP_EmdFileReader()
        {
            m_domCreator = XEP_EmdFactrory.CreateEmdLinesParser();
        }

        #region MEMBERS
        IXEP_EmdDom m_domCreator;
        #endregion
        
        #region PROPERTIES
        public IXEP_EmdLine Root
        {
            get { return m_domCreator.Root; }
        }
        #endregion
        
        #region INTERFACE IMPL
        public void Read(StreamReader reader)
        {
            m_domCreator = XEP_EmdFactrory.CreateEmdLinesParser();
            List<IXEP_EmdLine> linesEmd = new List<IXEP_EmdLine>();
            string line;
            int counter = 0;
            while ((line = reader.ReadLine()) != null)
            {
                IXEP_EmdLine
                element = XEP_EmdFactrory.CreateEmdLine();
                element.Parse(line, counter);
                linesEmd.Add(element);
                counter++;
            }
            m_domCreator.CreateDom(linesEmd);
        }
        #endregion
    }
}