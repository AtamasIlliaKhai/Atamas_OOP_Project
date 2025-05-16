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
using Atamas_OOP_Project.Models;
using Atamas_OOP_Project.Views;

namespace Atamas_OOP_Project
{
    public partial class AdminWindow : Window
    {
        private Admin _admin;

        public AdminWindow(Admin admin)
        {
            InitializeComponent();
            _admin = admin;

            Title = $"Панель адміністратора — {_admin.FullDisplay}";

            // Виводимо дані
            NameTextBlock.Text = $"Ім'я: {_admin.FirstName} {_admin.LastName}";
            EmailTextBlock.Text = $"Email: {_admin.Email}";
            PhoneTextBlock.Text = $"Телефон: {_admin.Phone}";
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}