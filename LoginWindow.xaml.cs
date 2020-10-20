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
using System.Dynamic;
using Newtonsoft.Json;
using System.Net;

namespace Client_Windows10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
      
        public LoginWindow()
        {
            InitializeComponent();
           
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {

           await Login();
        }


        public async Task Login()
        {
            var url = "http://localhost:5000/api/authenticate/login";

            dynamic user = new ExpandoObject();
            user.username = username_txt_box.Text;
            user.password = password_box.Password;

            string json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Status OK");
                
                var temp = await response.Content.ReadAsStringAsync();
                LoginToken t = JsonConvert.DeserializeObject<LoginToken>(temp);
                string message = $"Login Successful \n token expires in:  {t.expiration.ToString()}";
                MessageBox.Show(message, "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
                
            }
            else
            {
                Console.WriteLine("Error");
                MessageBox.Show("Username/Password is incorrect", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            App.Current.MainWindow = register;
            this.Close();
            register.Show();
        }
    }
}
