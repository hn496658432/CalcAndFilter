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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is TextBox textBox)
            {
                viewModel.ClacModel3.IsFormula = textBox.Text.Contains("*");
            }
        }

        private void Btn_Calc_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show(viewModel.ClacModel1.CalcContent);
            this.Dispatcher.InvokeAsync(async () => {
                await viewModel.ClacModel1.CalcAsync();
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(viewModel.ClacModel1.CalcResult));
                calc_result_1.Document.Blocks.Add(paragraph);
            });

        }
    }
}