using System;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace EmdConventor.Views
{
    public partial class XEP_MainView : UserControl
    {
        [Dependency]
        public XEP_MainViewModel ViewModel
        {
            get { return this.DataContext as XEP_MainViewModel; }
            set { this.DataContext = value; }
        }

        public XEP_MainView()
        {
            InitializeComponent();
        }
    }
}
