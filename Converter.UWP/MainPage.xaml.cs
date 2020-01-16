using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

namespace Converter.UWP
{
    public sealed partial class MainPage
    {
        string rate;
        string Code1, Code2;
        public string responseString;
        public List<string> Codes = new List<string>();
        public string calcType = "";
        public double var = 0.0;

        public MainPage()
        {
            this.InitializeComponent();
            GetCode();
        }
        
        public async void GetCode()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://free.currencyconverterapi.com/api/v6/currencies?apiKey=4eba8e10bbc2421ce2e0");
            responseString = await response.Content.ReadAsStringAsync();
            ConvertString();
        }

        public void ConvertString()
        {
            string val = responseString.Substring(12);
            val = val.Remove(val.Length - 2);
            List<string> CodesRaw = val.Split("},").ToList<string>();

            for (int i = 0; i < CodesRaw.Count; i++)
            {
                //Debug.WriteLine("Adding Stuff");
                string item = CodesRaw[i].Substring(1, 3);
                Debug.WriteLine("Adding: " + item);
                Codes.Add(item);
            }

            //Debug.WriteLine("wut");
            codeSelector1.ItemsSource = Codes;
            codeSelector2.ItemsSource = Codes;
        }

        public async void GetAPI()
        {
            Code1 = codeSelector1.SelectedItem.ToString();
            Code2 = codeSelector2.SelectedItem.ToString();
            var client = new HttpClient();
            string query = "http://free.currencyconverterapi.com/api/v6/convert?q=" + Code1 + "_" + Code2 + "," + Code2 + "_" + Code1 + "&compact=ultra&apiKey=4eba8e10bbc2421ce2e0";
            //Debug.WriteLine(query);
            var response = await client.GetAsync(query);
            //Debug.WriteLine(response.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            string val = responseString.Substring(31);
            int len = val.Length;
            //Debug.WriteLine(val.ToString());
            rate = val.Remove(len - 1);
            Debug.WriteLine(rate.ToString());
            ExchangeRate.Text = rate;
        }

        private void GetRateButton_Click(object sender, RoutedEventArgs e)
        {
            GetAPI();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetAPI();
            ConvertCal();
        }

        public void ConvertCal()
        {
            double result = Convert.ToDouble(input.Text) * Convert.ToDouble(rate);
            output.Text = result.ToString();
        }

        public void Calculate(string pressed)
        {
            switch (pressed)
            {
                case "0":
                    input.Text += 0;
                    break;

                case "1":
                    input.Text += 1;
                    break;

                case "2":
                    input.Text += 2;
                    break;

                case "3":
                    input.Text += 3;
                    break;

                case "4":
                    input.Text += 4;
                    break;

                case "5":
                    input.Text += 5;
                    break;

                case "6":
                    input.Text += 6;
                    break;

                case "7":
                    input.Text += 7;
                    break;

                case "8":
                    input.Text += 8;
                    break;

                case "9":
                    input.Text += 9;
                    break;

                case "+":
                    var = Convert.ToDouble(input.Text);
                    input.Text = "";
                    calcType = "+";
                    break;

                case "-":
                    var = Convert.ToDouble(input.Text);
                    input.Text = "";
                    calcType = "-";
                    break;

                case "*":
                    var = Convert.ToDouble(input.Text);
                    input.Text = "";
                    calcType = "*";
                    break;

                case "/":
                    var = Convert.ToDouble(input.Text);
                    input.Text = "";
                    calcType = "/";
                    break;

                case "=":
                    if (calcType == "+")
                    {
                        input.Text = (var + Convert.ToDouble(input.Text)).ToString();
                    }
                    else if (calcType == "-")
                    {
                        input.Text = (var - Convert.ToDouble(input.Text)).ToString();
                    }
                    else if (calcType == "*")
                    {
                        input.Text = (var * Convert.ToDouble(input.Text)).ToString();
                    }
                    else if (calcType == "/")
                    {
                        input.Text = (var / Convert.ToDouble(input.Text)).ToString();
                    }
                    break;

                case "C":
                    input.Text = "";
                    break;

                case ".":
                    input.Text += ".";
                    break;

                default:
                    Console.WriteLine("Shouldn't be possible");
                    break;
            }
        }

        private void _9_Click(object sender, RoutedEventArgs e)
        {
            Calculate("9");
        }

        private void _8_Click(object sender, RoutedEventArgs e)
        {
            Calculate("8");
        }

        private void _7_Click(object sender, RoutedEventArgs e)
        {
            Calculate("7");
        }

        private void _6_Click(object sender, RoutedEventArgs e)
        {
            Calculate("6");
        }

        private void _5_Click(object sender, RoutedEventArgs e)
        {
            Calculate("5");
        }

        private void _4_Click(object sender, RoutedEventArgs e)
        {
            Calculate("4");
        }

        private void _3_Click(object sender, RoutedEventArgs e)
        {
            Calculate("3");
        }

        private void _2_Click(object sender, RoutedEventArgs e)
        {
            Calculate("2");
        }

        private void _1_Click(object sender, RoutedEventArgs e)
        {
            Calculate("1");
        }

        private void _0_Click(object sender, RoutedEventArgs e)
        {
            Calculate("0");
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            Calculate("C");
        }

        private void _multi_Click(object sender, RoutedEventArgs e)
        {
            Calculate("*");
        }

        private void _plus_Click(object sender, RoutedEventArgs e)
        {
            Calculate("+");
        }

        private void _minus_Click(object sender, RoutedEventArgs e)
        {
            Calculate("-");
        }

        private void _divide_Click(object sender, RoutedEventArgs e)
        {
            Calculate("/");
        }

        private void _equals_Click(object sender, RoutedEventArgs e)
        {
            Calculate("=");
        }

        private void _dot_Click(object sender, RoutedEventArgs e)
        {
            Calculate(".");
        }
    }
}
