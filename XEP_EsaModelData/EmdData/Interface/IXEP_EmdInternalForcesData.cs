namespace XEP_EsaModelData.EmdData.Interface
{
    public interface IXEP_EmdInternalForcesData : IXEP_EmdElemConventor
    {
        string Name();
        int ID { get; }
        double N { get; }
        double My { get; }
        double Mz { get; }
        double Mx { get; }
        double Vy { get; }
        double Vz { get; }
        int IsCritical { get; }
    }
}