using SocPK.Desktop.ViewModels;
using System.Windows;

namespace SocPK.Desktop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            
            this.DataContext = viewModel;
        }
    }
}
