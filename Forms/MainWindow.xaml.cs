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
using System.Net.Http;

namespace AduloRolloGrowe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, In.variante> variants = new Dictionary<string, In.variante>();
        Dictionary<string, In.leistung> leistung = new Dictionary<string, In.leistung>();
        RestClient rClient = new RestClient();
        Login log = new Login();
        bool connect = false;
#region Application entry point
        public MainWindow()
        {
            InitializeComponent();
            txtRequestURI.Text = log.GroweAPIUrl;
            txtUserName.Text = log.User;
            txtPassword.Text = log.Password;
            ConnectToRest();
            cboVerb.SelectedIndex = 0;
            GetLeistung();
            GetVarianten();
        }
      
        /// <summary>
        /// ConnectToRest
        /// </summary>
        private void ConnectToRest()
        {
            rClient.EndPoint = txtRequestURI.Text;
            rClient.Methode = (FunctionType)Enum.Parse(typeof(FunctionType), "authentificate");
            rClient.HttpMethod = HttpVerb.POST;
            rClient.PostJSON = "";
            rClient.UserName = txtUserName.Text;
            rClient.UserPassword = txtPassword.Text;
            string strResponse = string.Empty;
            connect = rClient.Authentificate();
            txtResponse.Text = rClient.GetJson;
            if (connect == true)
            {
                Status.Fill = new SolidColorBrush(Colors.Green);
            }
            else
            { 
                Status.Fill = new SolidColorBrush(Colors.Red); 
            }
        }
        private void GetLeistung()
        {
            rClient.Methode = FunctionType.getLeistungen;
            rClient.MakeRequest();
            leistung = rClient.Leistung;
            //List_leistung.ItemsSource = leistung.Values;
            for (int index = 0; index < leistung.Count -1; index++)
            {
                var item = leistung.ElementAt(index);
                var itemKey = item.Key;
                var itemValue = item.Value;
                List_leistung.Items.Add(new In.leistung { leistung_id = itemValue.leistung_id , name = itemValue.name, zusatztext = itemValue.zusatztext , anbaute = itemValue.anbaute , aufsatz = itemValue.aufsatz, bild = itemValue.bild });
            }

        }

        private void GetVarianten()
        {
            rClient.Methode = FunctionType.listConfigurations;
            rClient.MakeRequest();
            variants = rClient.Variants;
            for (int index = 0; index < variants.Count - 1; index++)
            {
                var item = variants.ElementAt(index);
                var itemKey = item.Key;
                var itemValue = item.Value;
                List_varianten.Items.Add(new In.variante { variante_id = itemValue.variante_id, name = itemValue.name, leistung_id = itemValue.leistung_id});
            }
        }

        #endregion Application entry point
        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            rClient.HttpMethod = (HttpVerb)Enum.Parse(typeof(HttpVerb), cboVerb.Text);
            rClient.Methode = (FunctionType)Enum.Parse(typeof(FunctionType), cboFunction.Text);
            rClient.Variante_id = txt_variantID.Text;
            bool connect = rClient.MakeRequest();
            txtResponse.Text = rClient.GetJson;
            txtRJson.Text = rClient.PostJSON;
            leistung = rClient.Leistung;
            
    }

        private void btnWebbrowser_Click(object sender, RoutedEventArgs e)
        {
            CefSharp.MinimalExample.Wpf.WebBrowser webbrowser = new CefSharp.MinimalExample.Wpf.WebBrowser();
            webbrowser.Show();
        }
    }
}
