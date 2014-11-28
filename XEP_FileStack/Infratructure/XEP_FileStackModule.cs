using System;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using XEP_FileStack.Views;
using XEP_Prism.Impl;

namespace XEP_FileStack.Infratructure
{
    public class XEP_FileStackModule : XEP_Module
    {
        public XEP_FileStackModule(IUnityContainer container, IRegionManager regionManager) :
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
            RegisterViewWithRegion<XEP_FileStackView>(XEP_RegionNames.s_FileStackRegionName);
        }
    }
}
