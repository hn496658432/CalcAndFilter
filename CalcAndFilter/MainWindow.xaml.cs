using CalcAndFilter.Helper;
using CalcAndFilter.Model;

using Microsoft.Win32;

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

#if DEBUG
            viewModel.ClacModel1.CalcContent = "1231231231231123*3.14159261231234461263541625341625436512436514263514263514263541263541625334615234";
            viewModel.ClacModel1.IsBitwise = true;
            viewModel.ClacModel2.CalcContent = "123*3.141592";
            viewModel.ClacModel2.IsBitwise = true;
            viewModel.ClacModel3.CalcContent = "123*3.1415926";
            viewModel.ClacModel3.IsBitwise = true;
#endif
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
            if (sender is TextBox textBox)
            {
                viewModel.ClacModel3.IsFormula = textBox.Text.Contains("*");
            }
        }

        private async void Btn_Calc_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(async () =>
            {
                await ResetContent(calc_result_1, viewModel.ClacModel1);
                await ResetContent(calc_result_2, viewModel.ClacModel2);
                await ResetContent(calc_result_3, viewModel.ClacModel3);
                await viewModel.SumCalcAsync();
            });

            this.Dispatcher.Invoke(() =>
            {
                calc_result_4.RichTextWriteList(viewModel.CalcARecords);
                calc_result_5.RichTextWriteList(viewModel.CalcBRecords);
            });
        }

        private async static Task ResetContent(RichTextBox richTextBox, ClacModel clacModel)
        {
            await clacModel.CalcAsync();

            richTextBox.Dispatcher.Invoke(() =>
            {
                richTextBox.RichTextWriteList(clacModel.calcRecords);
            });
            
        }


        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            var keyWords = viewModel.Filter.Split(',');
            if (keyWords.Length == 0)
            {
                return;
            }
            this.Dispatcher.Invoke(() =>
            {
                if (viewModel.CalcARecords.Count > 0) { calc_result_4.FindFilter(keyWords); }
                if (viewModel.CalcBRecords.Count > 0) { calc_result_5.FindFilter(keyWords); }
            });
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            // calc_result_5 Export to Word
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Word RTF 文档|*.rtf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                calc_result_5.ExportToWord(saveFileDialog.FileName);
            }
        }

        private void SeachButton_Click(object sender, RoutedEventArgs e)
        {
            var keyword = searchbox.Text;
            if (string.IsNullOrEmpty(keyword))
            {
                return;
            }

            this.Dispatcher.Invoke(() =>
            {
                calc_result_5.SearchNext(keyword);
            });
        }
    }
}