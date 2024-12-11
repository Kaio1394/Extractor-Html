using HtmlAgilityPack;
using System.Collections.ObjectModel;
using System;
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
using ExtractorPageWeb.Helpers;
using System.Windows.Controls.Primitives;

namespace ExtractorPageWeb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _html;
        private static string _parseHtml;
        private Helpers.SQLiteHelper _sQLiteHelper;
        const string CONNECTION_STRING = "Data Source=ExtratorPageWeb.db;Version=3;";

        private static ObservableCollection<string> _listDataBase { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _sQLiteHelper = new Helpers.SQLiteHelper(CONNECTION_STRING);
            _sQLiteHelper.CreateTable();
        }     
        private void BtnRun_ClickAsync(object sender, RoutedEventArgs e)
        {
            ExecuteRun(tbUrl, tbBlockHtml);
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            tbBlockHtml.Text = "";
        }
        private void ButtonClearResult_Click(object sender, RoutedEventArgs e)
        {
            tbBlockResult.Text = "";
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

            var result = node.InnerHtml;
            tbBlockResult.Text = result;
            tabMain.SelectedItem = tabGetResult;

            result = result.Replace("\n", "").Replace("\r", "").Trim();

            _sQLiteHelper.InsertData(comboItem, tbSearch.Text, result, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<ExtractorPageWeb.Helpers.Selector> listSelectors = _sQLiteHelper.GetAllSelectors();
            gridHistory.ItemsSource = listSelectors;
        }
    }
}