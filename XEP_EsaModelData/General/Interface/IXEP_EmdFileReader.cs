using System;
using System.IO;

namespace XEP_EsaModelData.General.Interface
{
    internal interface IXEP_EmdFileReader
    {
        IXEP_EmdLine Root { get; }
        void Read(StreamReader reader);
    }
}
