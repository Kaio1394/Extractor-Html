using HtmlAgilityPack;
using System.Net.Http;
using System.Security.Policy;
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
using System.Xml;

namespace ExtractorPageWeb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _html;
        private static string _parseHtml;
        public MainWindow()
        {
            InitializeComponent();
        }     
        private void BtnRun_ClickAsync(object sender, RoutedEventArgs e)
        {
            ExecuteRun(tbUrl, tbBlockHtml);
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            tbBlockHtml.Text = "";
        }

        private async void ExecuteRun(TextBox textBoxUrl, TextBox textBlock)
        {
            if (string.IsNullOrEmpty(textBoxUrl.Text))
            {
                MessageBox.Show("Param Url can't be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var url = textBoxUrl.Text;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    _html = await client.GetStringAsync(url);

                    textBlock.Dispatcher.Invoke(() =>
                    {
                        textBlock.Text = _html;
                    });
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            HtmlNode? node;
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(_html);

            ComboBoxItem? selectedItem = cbTypeSearch.SelectedItem as ComboBoxItem;
            var comboItem = selectedItem?.Content.ToString();

            switch (comboItem)
            {
                case "XPath":
                    node = htmlDoc.DocumentNode.SelectSingleNode(tbSearch.Text);
                    break;
                case "CssSelector":
                    node = null;
                    break;
                default:
                    return;
            }
            _parseHtml = node.InnerHtml;
        }
    }
}