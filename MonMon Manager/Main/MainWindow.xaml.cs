using System.Windows;
using MaxExperiment.AppInfrastructure;

namespace MaxExperiment.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppData.MainWindowView = this;
        }
    }
}
