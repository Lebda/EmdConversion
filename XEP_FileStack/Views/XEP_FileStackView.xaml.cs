using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Windows.Controls;

namespace XEP_FileStack.Views
{
    public partial class XEP_FileStackView : UserControl
    {
        [Dependency]
        public XEP_FileStackViewModel ViewModel
        {
            get { return this.DataContext as XEP_FileStackViewModel; }
            set { this.DataContext = value; }
        }

        public XEP_FileStackView()
        {
            InitializeComponent();
        }
    }
}
