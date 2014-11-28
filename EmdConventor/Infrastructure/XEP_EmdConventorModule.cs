using System;
using EmdConventor.Views;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using XEP_Prism.Impl;

namespace EmdConventor.Infrastructure
{
    public class XEP_EmdConventorModule : XEP_Module
    {
        public XEP_EmdConventorModule(IUnityContainer container, IRegionManager regionManager) :
            base(container, regionManager)
        {
        }

        public override void InitializeOnlyTypes()
        {

        }
        public override void Initialize()
        {
            InitializeOnlyTypes();
            // Regions
            RegisterViewWithRegion<XEP_MainView>(XEP_RegionNames.s_MainViewRegionName);
        }
    }
}
