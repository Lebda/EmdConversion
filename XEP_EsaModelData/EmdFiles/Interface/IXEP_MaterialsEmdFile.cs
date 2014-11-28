using System;

namespace XEP_EsaModelData.EmdFiles.Interface
{
    public interface IXEP_MaterialsEmdFile : IXEP_BaseEmdFile
    {
        string GetBaseMaterial(string matNameEnum);
    }
}