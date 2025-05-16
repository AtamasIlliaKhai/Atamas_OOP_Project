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
using System.Windows.Shapes;
namespace Atamas_OOP_Project.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoginButton.Click += LoginButton_Click;
            RegisterButton.Click += RegisterButton_Click;
            BrowseButton.Click += BrowseButton_Click;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var guestBrowseWindow = new GuestBrowseWindow();
            guestBrowseWindow.Show();
            this.Close();
        }
    }
}