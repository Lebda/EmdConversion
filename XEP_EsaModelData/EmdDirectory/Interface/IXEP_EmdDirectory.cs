using System;
using XEP_EsaModelData.EmdFiles.Interface;

namespace XEP_EsaModelData.EmdDirectory.Interface
{
    public interface IXEP_EmdDirectory
    {
        void CheckReinforcementFile(string emdDirPath);
        IXEP_CrossSectionEmdFile CrossSectionFile { get; }
        IXEP_ReinforcementEmdFile ReinforcementFile { get; }
        IXEP_LoadEmdFile LoadFile { get; }
        IXEP_IternalForcesEmdFile IternalForcesFile { get; }
        void Load(string emdDirPath);
        void Save(string emdDirPath);
        void PrepareDirectory(int sectionID);
    }
}