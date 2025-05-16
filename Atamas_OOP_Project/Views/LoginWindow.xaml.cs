using Atamas_OOP_Project.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Atamas_OOP_Project.Views
{
    public partial class LoginWindow : Window
    {
        private readonly string _studentsFilePath;
        private readonly string _tutorsFilePath;
        private readonly string _adminsFilePath;

        public LoginWindow()
        {
            InitializeComponent();

            // Ініціалізація шляхів до файлів
            string dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            Directory.CreateDirectory(dataDirectory);

            _studentsFilePath = Path.Combine(dataDirectory, "students.json");
            _tutorsFilePath = Path.Combine(dataDirectory, "tutors.json");
            _adminsFilePath = Path.Combine(dataDirectory, "admins.json");

            LoginUserButton.Click += LoginUserButton_Click;
        }

        private void LoginUserButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var loginResult = ValidateUserAndGetRole(email, password);

                if (loginResult.isValid)
                {
                    HandleSuccessfulLogin(loginResult.role, loginResult.user);
                }
                else
                {
                    MessageBox.Show("Невірна електронна пошта або пароль.", "Помилка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка: {ex.Message}", "Помилка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HandleSuccessfulLogin(string role, User user)
        {
            MessageBox.Show("Вхід успішний!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

            Window windowToOpen = role switch
            {
                "Admin" => new AdminWindow(user as Admin),
                "Tutor" => new TutorWindow(user as Tutor),
                "Student" => new StudentWindow(user as Student),
                _ => throw new InvalidOperationException($"Невідома роль: {role}")
            };

            windowToOpen.Show();
            this.Close();
        }


        private (bool isValid, string role, User user) ValidateUserAndGetRole(string email, string password)
        {
            // Студенти
            if (File.Exists(_studentsFilePath))
            {
                var students = LoadUsers<Student>(_studentsFilePath);
                var student = students.FirstOrDefault(u =>
                    u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (student != null && VerifyPassword(password, student.PasswordHash))
                {
                    return (true, "Student", student);
                }
            }

            // Викладачі
            if (File.Exists(_tutorsFilePath))
            {
                var tutors = LoadUsers<Tutor>(_tutorsFilePath);
                var tutor = tutors.FirstOrDefault(u =>
                    u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (tutor != null && VerifyPassword(password, tutor.PasswordHash))
                {
                    return (true, "Tutor", tutor);
                }
            }

            // Адміни
            if (File.Exists(_adminsFilePath))
            {
                var admins = LoadUsers<Admin>(_adminsFilePath);
                var admin = admins.FirstOrDefault(u =>
                    u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (admin != null && VerifyPassword(password, admin.PasswordHash))
                {
                    return (true, "Admin", admin);
                }
            }

            return (false, null, null);
        }

        private List<T> LoadUsers<T>(string filePath) where T : User
        {
            try
            {
                if (!File.Exists(filePath))
                    return new List<T>();

                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException($"Помилка читання файлу {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            if (string.IsNullOrEmpty(storedPasswordHash))
                return false;

            var enteredPasswordHash = CreateSHA256Hash(enteredPassword);
            return string.Equals(enteredPasswordHash, storedPasswordHash, StringComparison.OrdinalIgnoreCase);
        }

        private static string CreateSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                
                // байти у HEX-рядок
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // два нижніх регістри HEX
                }
                return builder.ToString();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToWindow(new MainWindow());
        }

        private void SwitchToRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigateToWindow(new RegisterWindow());
        }

        private void NavigateToWindow(Window window)
        {
            window.Show();
            this.Close();
        }

        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                PasswordBox.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordTextBox.Text = PasswordBox.Password;
            }
            else
            {
                PasswordBox.Visibility = Visibility.Visible;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Password = PasswordTextBox.Text;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                PasswordTextBox.Text = PasswordBox.Password;
            }
        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordTextBox.Visibility == Visibility.Visible)
            {
                PasswordBox.Password = PasswordTextBox.Text;
            }
        }
    }
}