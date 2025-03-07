using CalcAndFilter.Model;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalcAndFilter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel viewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            this.DataContext = viewModel;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.ClacModel1.CalcContent = System.Text.Json.JsonSerializer.Serialize(viewModel.ClacModel1);
        }
    }
}