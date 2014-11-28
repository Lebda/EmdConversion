using System;
using System.IO;

namespace XEP_EsaModelData.EmdFiles.Interface
{
    public interface IXEP_BaseEmdFile
    {
        void Load(Stream stream);
        void Save(Stream stream);
    }
}
