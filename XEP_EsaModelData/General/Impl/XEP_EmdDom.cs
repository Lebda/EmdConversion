using System;
using System.Collections.Generic;
using System.Linq;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.General.Impl
{
    internal class XEP_EmdDom : IXEP_EmdDom
    {
        public XEP_EmdDom ()
        {
        }
        
        #region PROPERTIES
        public IXEP_EmdLine Root { get; set; }
        #endregion
        
        #region INTERFACE IMPL
        public void CreateDom(List<IXEP_EmdLine> linesEmd)
        {
            List<IXEP_EmdLine> linesEmd4Dom = linesEmd;
            List<IXEP_EmdLine> roots = linesEmd4Dom.Where(item => item.IntendationLevel == 0).ToList();
            Root = PrepareRoot(roots);
            List<IXEP_EmdLine> linesEmd4DomReversed = Enumerable.Reverse(linesEmd4Dom).ToList();
            foreach (var actLineEmd in linesEmd4DomReversed)
            {
                if (actLineEmd.IntendationLevel == 0)
                {
                    continue;
                }
                IXEP_EmdLine ownerLine = FindOwnerInReversed(linesEmd4DomReversed, actLineEmd);
                ownerLine.Lines.Add(actLineEmd);
            }
            Root.ReverseLines();
        }
        #endregion        
    
        #region METHODS PRIVATE
        static private IXEP_EmdLine PrepareRoot(List<IXEP_EmdLine> roots)
        {
            if (roots == null || roots.Count == 0)
            {
                throw new InvalidOperationException("There are no root elements !");
            }
            IXEP_EmdLine preparedRoot = roots[0];
            if (roots.Count > 1)
            {
                preparedRoot = XEP_EmdFactrory.CreateEmdLine();
                preparedRoot.CreateFakeRoot();
                List<IXEP_EmdLine> rootsReversed = Enumerable.Reverse(roots).ToList();
                foreach (var root in rootsReversed)
                {
                    preparedRoot.Lines.Add(root);
                }
            }
            return preparedRoot;
        }
        static private IXEP_EmdLine FindOwnerInReversed(List<IXEP_EmdLine> linesEmd4DomReversed, IXEP_EmdLine actLineEmd)
        {
            foreach (var actItem in linesEmd4DomReversed)
            {
                if (actItem.LineIndex > actLineEmd.LineIndex)
                {
                    continue;
                }
                if (actItem.IntendationLevel < actLineEmd.IntendationLevel)
                {
                    return actItem;
                }
            }
            throw new InvalidOperationException("Element does not have a owner !");
        }
        #endregion
    }
}