using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
            // This nested class must be ComVisible for the JavaScript to be able to call it.
            [ComVisible(true)]
            public class ScriptManager
            {
                // Variable to store the form of type Form1.
                private MainWindow window;

                // Constructor.
                public ScriptManager(MainWindow form)
                {
                    // Save the form so it can be referenced later.
                    window = form;
                }

                // This method can be called from JavaScript.
                public void MethodToCallFromScript()
                {
                    // Call a method on the form.
                    //mForm.DoSomething();
                }

                // This method can also be called from JavaScript.
                public void onLoad()
                {
                window.on_load();
                }
            }

            public MainWindow()
        {
            InitializeComponent();
            this.browser.ObjectForScripting = new ScriptManager(this);
            string uri = AppDomain.CurrentDomain.BaseDirectory + "Map/index.html";
            this.browser.Navigate(new Uri(uri, UriKind.Absolute));
            
            

        }

        public void on_load()
        {
            Double[] loc = { 51.05011, -114.08529 };
            this.browser.InvokeScript("addMarker", new Object[] { loc[0], loc[1] });
            //MessageBox.Show("Marker added", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Double[] loc = { 51.05011, -114.200 };
            this.browser.InvokeScript("addMarker", new Object[] { loc[0], loc[1] });
        }
    }
}
