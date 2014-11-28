using System;

namespace XEP_EsaModelData.General.Interface
{
    public interface IXEP_EmdIntendationGetter
    {
        int IntendationLevel { get; set; }
        string GetIntendation();
    }
}