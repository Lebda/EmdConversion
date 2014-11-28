using System;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using XEP_FileStack.Infratructure;

namespace EmdConventor.Infrastructure
{
    public class XEP_Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.RootVisual = (UIElement)this.Shell;
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            AddModuleInternal<XEP_EmdConventorModule>();
            AddModuleInternal<XEP_FileStackModule>();
        }

        private void AddModuleInternal<T>() where T : class
        {
            Type myPrismModule = typeof(T);
            ModuleCatalog.AddModule(new ModuleInfo(myPrismModule.Name, myPrismModule.AssemblyQualifiedName));
        }
    }
}
