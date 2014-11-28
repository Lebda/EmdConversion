using System;
using System.Collections.Generic;

namespace XEP_EsaModelData.General.Interface
{
    internal interface IXEP_EmdLine
    {
        void CreateFakeRoot();
        void Parse(string line, int lineIndex);
        IXEP_EmdElement CreateEmdElement();
        void ReverseLines();
        List<IXEP_EmdLine> Lines { get; }
        int LineIndex { get; }
        int IntendationLevel { get; }
    }
}