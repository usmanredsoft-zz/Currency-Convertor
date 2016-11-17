using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

namespace currencyconvertor2go
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
       
        string originCurrency = "USD";
        decimal [] convertrate = {1,1,1,1,1,1};
        string[] currency = { "USD", "EUR", "JPY", "GBP", "AUD", "CAD" };

        public MainWindow()
        {
            InitializeComponent();
            this.Width = 450;
            this.Height = 600;
            initializeCurrency();
            webBrowser1.Source = new BitmapImage(new Uri(@"http://ads.pipaffiliates.com/afs/show.php?id=8901&cid=72783&ctgid=16\\"));
            webimage2.Source = new BitmapImage(new Uri(@"http://ads.pipaffiliates.com/afs/show.php?id=8901&cid=72783&ctgid=16\\"));
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 5, 0);
            dispatcherTimer.Start();
            
        }

 
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // code goes here
            calcConverRate();
        }
        private void calcConverRate()
        {

            convertrate[1] = decimal.Parse((string)CurrencyConvert(1, "USD", "EUR"), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
            convertrate[2] = decimal.Parse((string)CurrencyConvert(1, "USD", "JPY"), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
            convertrate[3] = decimal.Parse((string)CurrencyConvert(1, "USD", "GBP"), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
            convertrate[4] = decimal.Parse((string)CurrencyConvert(1, "USD", "AUD"), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
            convertrate[5] = decimal.Parse((string)CurrencyConvert(1, "USD", "CAD"), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);

        }


        public string CurrencyConvert(decimal amount, string fromCurrency, string toCurrency)
        {

            //Grab your values and build your Web Request to the API
            string apiURL = String.Format("https://www.google.com/finance/converter?a={0}&from={1}&to={2}&meta={3}", amount, fromCurrency, toCurrency, Guid.NewGuid().ToString());

            //Make your Web Request and grab the results
            var request = WebRequest.Create(apiURL);

            //Get the Response
            var streamReader = new StreamReader(request.GetResponse().GetResponseStream(), System.Text.Encoding.ASCII);

            //Grab your converted value (ie 2.45 USD)
            var result = Regex.Matches(streamReader.ReadToEnd(), "<span class=\"?bld\"?>([^<]+)</span>")[0].Groups[1].Value;

            result = result.Remove(result.Length - 4);
            //Get the Result
            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txtCurrency.Text = "";
            txtCurrency.Focus();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string str = txtCurrency.Text;

            if (str.Length > 0)
            {
                str = str.Remove(str.Length - 1);
            }

            txtCurrency.Text = str;


        }
        private void inputKeybord(string s)
        {
            string str = txtCurrency.Text;

            if (s == "." && str.Contains(".")) return;
            if (s == "0" && str == "0") return;
            if (s == "00" && str == "0") return;
            if (s == "00" && str == "") return;

            txtCurrency.Text = txtCurrency.Text + s;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            inputKeybord("7");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            inputKeybord("8");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            inputKeybord("9");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            inputKeybord("4");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            inputKeybord("5");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            inputKeybord("6");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            inputKeybord("1");
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            inputKeybord("2");
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            inputKeybord("3");
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            inputKeybord("0");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            inputKeybord("00");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            inputKeybord(".");
        }
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        private void txtCurrency_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = txtCurrency.Text;

            if (str.Length > 0)
            {
                bool ret = IsTextAllowed(str);
                if (!ret)
                    str = str.Remove(str.Length - 1);
            }

            txtCurrency.Text = str;
            if (str != "")
            {
                decimal a = decimal.Parse(txtCurrency.Text.ToString());
                convertCurrency(a, originCurrency);

            }

        }

        private decimal getValue(string from, string to)
        {
            int i1 = 0, i2 = 0;
            for ( int i = 0; i < 6; i++)
            {
                if (currency[i] == from) i1 = i;
                if (currency[i] == to) i2 = i;
            }

            decimal v1 = convertrate[i1];
            decimal v2 = convertrate[i2];


            return v2 / v1;


        }
        private void initButtons( decimal value, string currency)
        {
            btnUsd.Style = (Style)Application.Current.Resources["RoundCorner"];
            btnEur.Style = (Style)Application.Current.Resources["RoundCorner"];
            btnGbp.Style = (Style)Application.Current.Resources["RoundCorner"];
            btnJpy.Style = (Style)Application.Current.Resources["RoundCorner"];
            btnCad.Style = (Style)Application.Current.Resources["RoundCorner"];
            btnAud.Style = (Style)Application.Current.Resources["RoundCorner"];

            if (!(currency == "USD"))
            {
                txtUsdValue.Text = "$" + string.Format("{0:0.####}", value * getValue(currency, "USD")); // "256.58"value * getValue(currency, "USD");
                txtUsdDescription.Text = "1" + currency + " = " + string.Format("{0:0.####}", getValue(currency, "USD")) + " USD"; 
            }

            if (!(currency == "EUR"))
            {
                txtEurValue.Text = "€" + string.Format("{0:0.####}", value * getValue(currency, "EUR"));
                txtEurDescription.Text = "1" + currency + " = " + string.Format("{0:0.####}", getValue(currency, "EUR") )+" EUR";
            }
            if (!(currency == "GBP"))
            {
                txtGbpValue.Text = "£" + string.Format("{0:0.####}", value * getValue(currency, "GBP"));
                txtGbpDescription.Text = "1" + currency + " = " + string.Format("{0:0.####}", getValue(currency, "GBP")) +" GBP";
            }
            if (!(currency == "JPY"))
            {
                txtJpyValue.Text = "¥" + string.Format("{0:0.####}", value * getValue(currency, "JPY"));
                txtJpyDescription.Text = "1" + currency + " = " + string.Format("{0:0.####}", getValue(currency, "JPY")) +" JPY";
            }
            if (!(currency == "AUD"))
            {
                txtAudValue.Text = "$" + string.Format("{0:0.####}", value * getValue(currency, "AUD"));
                txtAudDescription.Text = "1" + currency + " = " + string.Format("{0:0.####}", getValue(currency, "AUD")) +" AUD";
            }
            if (!(currency == "CAD"))
            {
                txtCadValue.Text = "$" + string.Format("{0:0.####}", value * getValue(currency, "CAD"));
                txtCadDescription.Text = "1" + currency + " = " + string.Format("{0:0.####}",  getValue(currency, "CAD"))  +" CAD"; 
            }
        }
        
        private void convertCurrency(decimal value, string currency)
        {
            initButtons(value, currency);
            txtMainCurrency.Text = currency;
            originCurrency = currency;
            switch (currency)
            {
                case "USD":
                    btnUsd.Style = (Style)Application.Current.Resources["ButtonActive"];
                    imgMainflag.Source = new BitmapImage(new Uri(@"/image/usflag.png", UriKind.Relative));

                    txtUsdDescription.Text = "\"FROM\" CURRENCY";
                    txtUsdValue.Text = "$" + value;
                    break;
                case "EUR":
                    btnEur.Style = (Style)Application.Current.Resources["ButtonActive"];
                    imgMainflag.Source = new BitmapImage(new Uri(@"/image/eurflag.png", UriKind.Relative));

                    txtEurDescription.Text = "\"FROM\" CURRENCY";
                    txtEurValue.Text = "€" + value;
                    break;
                case "GBP":
                    btnGbp.Style = (Style)Application.Current.Resources["ButtonActive"];
                    imgMainflag.Source = new BitmapImage(new Uri(@"/image/gbpflag.png", UriKind.Relative));

                    txtGbpDescription.Text = "\"FROM\" CURRENCY";
                    txtGbpValue.Text = "£" + value;
                    break;
                case "JPY":
                    btnJpy.Style = (Style)Application.Current.Resources["ButtonActive"];
                    imgMainflag.Source = new BitmapImage(new Uri(@"/image/jpyflag.png", UriKind.Relative));

                    txtJpyDescription.Text = "\"FROM\" CURRENCY";
                    txtJpyValue.Text = "¥" + value;
                    break;
                case "CAD":
                    btnCad.Style = (Style)Application.Current.Resources["ButtonActive"];
                    imgMainflag.Source = new BitmapImage(new Uri(@"/image/cadflag.png", UriKind.Relative));

                    txtCadDescription.Text = "\"FROM\" CURRENCY";
                    txtCadValue.Text = "$" + value;
                    break;
                case "AUD":
                    btnAud.Style = (Style)Application.Current.Resources["ButtonActive"];
                    imgMainflag.Source = new BitmapImage(new Uri(@"/image/audflag.png", UriKind.Relative));

                    txtAudDescription.Text = "\"FROM\" CURRENCY";
                    txtAudValue.Text = "$" + value;
                    break;
            }
        }


        private void initializeCurrency()
        {
            imgMainflag.Source = new BitmapImage(new Uri(@"/image/usflag.png", UriKind.Relative)); //"image/usflag.png";
            txtMainCurrency.Text = "USD";

            

            txtCurrency.Text = "1";
            calcConverRate();
            convertCurrency(1, "USD");
            btnUsd.Style = (Style)Application.Current.Resources["ButtonActive"];


        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            //USD
            decimal a = decimal.Parse(txtCurrency.Text.ToString());
            convertCurrency(a, "USD");
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            //EUR
            decimal a = decimal.Parse(txtCurrency.Text.ToString());
            convertCurrency(a, "EUR");
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            //JPY
            decimal a = decimal.Parse(txtCurrency.Text.ToString());
            convertCurrency(a, "JPY");
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            //GBP
            decimal a = decimal.Parse(txtCurrency.Text.ToString());
            convertCurrency(a, "GBP");
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            //AUD
            decimal a = decimal.Parse(txtCurrency.Text.ToString());
            convertCurrency(a, "AUD");
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            //CAD
            decimal a = decimal.Parse(txtCurrency.Text.ToString());
            convertCurrency(a, "CAD");
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //advertize();
        }

 
        private void webimage2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://clicks.pipaffiliates.com/afs/come.php?id=8901&cid=72783&atype=1&ctgid=16");
        }

        private void webBrowser1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://clicks.pipaffiliates.com/afs/come.php?id=8901&cid=72783&atype=1&ctgid=16");
        }

    

    }

   
}
