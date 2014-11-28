using System;
using System.Linq;

namespace XEP_EsaModelData.EmdDirectory.Interface
{
    internal interface IXEP_CheckReinfEmdInDirectory
    {
        void CheckDirectorySave(string directoryPath, bool isReinforcementInputed);
        void CheckDirectoryLoad(string directoryPath);
    }
}