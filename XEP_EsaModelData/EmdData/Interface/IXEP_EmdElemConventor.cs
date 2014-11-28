using XEP_EsaModelData.General.Interface;

namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdElemConventor
    {
        void CreateFromEmdElement(IXEP_EmdElement elem);
        IXEP_EmdElement CreateEmdElement();
    }
}