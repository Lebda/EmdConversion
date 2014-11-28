using System;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using XEP_Prism.Interface;

namespace XEP_Prism.Impl
{
    public abstract class XEP_Module : IXEP_Module
    {
        public XEP_Module(IUnityContainer container, IRegionManager regionManager)
        {
            m_container = container;
            m_regionManager = regionManager;
        }

        private readonly IUnityContainer m_container;
        private readonly IRegionManager m_regionManager;

        public void RegisterViewWithRegion<Tview>(string regionName)
        {
            m_regionManager.RegisterViewWithRegion(regionName, () => m_container.Resolve<Tview>());
        }
        public void RegisterType(Type from, Type to, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            m_container.RegisterType(from, to, lifetimeManager, injectionMembers);
        }
        public void RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            m_container.RegisterType<TFrom, TTo>(lifetimeManager, injectionMembers);
        }
        public void RegisterInstance<TInterface>(string name, TInterface instance = null)
            where TInterface : class
        {
            if (instance == null)
            {
                m_container.RegisterInstance<TInterface>(name, m_container.Resolve<TInterface>());
            }
            else
            {
                m_container.RegisterInstance<TInterface>(name, instance);
            }
        }
        public abstract void Initialize();
        public abstract void InitializeOnlyTypes();
    }
}
