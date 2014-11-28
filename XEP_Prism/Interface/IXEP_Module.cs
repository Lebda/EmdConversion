using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace XEP_Prism.Interface
{
    public interface IXEP_Module : IModule
    {
        void RegisterInstance<TInterface>(string name, TInterface instance = null) where TInterface : class;
        void InitializeOnlyTypes();
        void RegisterViewWithRegion<Tview>(string regionName);
        void RegisterType(Type from, Type to, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers);
    }
}
