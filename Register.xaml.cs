using Newtonsoft.Json;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client_Windows10
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private static readonly HttpClient client = new HttpClient();
        public Register()
        {
            InitializeComponent();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            var url = "http://localhost:5000/api/authenticate/login";

            dynamic user = new ExpandoObject();
            user.username = username_txt_box.Text;
            user.password = password_box.Password;
            user.email = email_txt_box.Text;
             
            string json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Status OK");   
                string message = $"Registration Successful!";
                MessageBox.Show(message, "Registration", MessageBoxButton.OK, MessageBoxImage.Information);
                LoginWindow login = new LoginWindow();
                App.Current.MainWindow = login;
                this.Close();
                login.Show();
                
            }
            else
            {
                var temp = await response.Content.ReadAsStringAsync();
                Response res = JsonConvert.DeserializeObject<Response>(temp);
                Console.WriteLine("Error");
                MessageBox.Show(res.Status , "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            App.Current.MainWindow = login;
            this.Close();
            login.Show();
        }
    }
}
