using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wind_Waker_Event_Editor.src.Editor.ViewModel;
using Graph;
using Graph.Compatibility;
using Graph.Items;

namespace Wind_Waker_Event_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;

        public MainWindow()
        {
            viewModel = new ViewModel();
            InitializeComponent();
            DataContext = viewModel;
        }

        private void NodeHost_Initialized(object sender, EventArgs e)
        {
            viewModel.Graph = new GraphControl();
            viewModel.Graph.CompatibilityStrategy = new Graph.Compatibility.TagTypeCompatibility();
            viewModel.Graph.AllowDrop = true;
            viewModel.Graph.BackColor = System.Drawing.Color.FromArgb(255, 16, 16, 45);

            NodeHost.Child = viewModel.Graph;
            NodeHost.AllowDrop = true;
        }
    }
}
