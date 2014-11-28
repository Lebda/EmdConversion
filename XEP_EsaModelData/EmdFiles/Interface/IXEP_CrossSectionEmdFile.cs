using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace XEP_EsaModelData.EmdFiles.Interface
{
    public interface IXEP_CrossSectionEmdFile : IXEP_BaseEmdFile
    {
        List<Point> GetShape();
        int GetFormCode();
        void SaveGeometry(List<KeyValuePair<string, string>> items);
        void SaveShape(List<Point> points);
        void SaveFibres(List<Point> fibres);
    }
}