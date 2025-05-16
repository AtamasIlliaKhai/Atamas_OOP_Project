using Atamas_OOP_Project.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Atamas_OOP_Project.Views
{
    public partial class RegisterWindow : Window
    {
        private const string StudentsFileName = "students.json";
        private const string TutorsFileName = "tutors.json";
        private readonly string _dataDirectory;
        private readonly string _studentsFilePath;
        private readonly string _tutorsFilePath;

        public RegisterWindow()
        {
            InitializeComponent();

            // Ініціалізація шляхів до файлів
            _dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(_dataDirectory);

            _studentsFilePath = Path.Combine(_dataDirectory, StudentsFileName);
            _tutorsFilePath = Path.Combine(_dataDirectory, TutorsFileName);

            // Ініціалізація файлів даних
            InitializeDataFiles();
        }

        private void InitializeDataFiles()
        {
            try
            {
                if (!File.Exists(_studentsFilePath))
                {
                    File.WriteAllText(_studentsFilePath, JsonConvert.SerializeObject(new List<Student>(), Formatting.Indented));
                }

                if (!File.Exists(_tutorsFilePath))
                {
                    File.WriteAllText(_tutorsFilePath, JsonConvert.SerializeObject(new List<Tutor>(), Formatting.Indented));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка ініціалізації файлів даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void RegisterUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Збір даних з форми
                var (firstName, lastName, email, password, confirmPassword, role) = CollectUserData();

                // Валідація введених даних
                if (!ValidateInput(firstName, lastName, email, password, confirmPassword, role))
                    return;

                // Перевірка наявності користувача
                if (await IsUserExists(email))
                {
                    MessageBox.Show("Користувач з такою поштою вже існує", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Створення об'єкта користувача
                var user = CreateUser(firstName, lastName, email, password, role);

                // Збереження користувача
                string filePath = role == "Student" ? _studentsFilePath : _tutorsFilePath;
                if (await SaveUser(user, filePath))
                {
                    MessageBox.Show("Реєстрація успішна! Тепер ви можете увійти.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Відкриття вікна входу
                    new LoginWindow().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не вдалося зберегти дані користувача", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критична помилка реєстрації: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private (string firstName, string lastName, string email, string password, string confirmPassword, string role) CollectUserData()
        {
            return (
                FirstNameTextBox.Text.Trim(),
                LastNameTextBox.Text.Trim(),
                EmailTextBox.Text.Trim().ToLower(),
                PasswordBox.Password,
                ConfirmPasswordBox.Password,
                (RoleComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString()
            );
        }

        private bool ValidateInput(string firstName, string lastName, string email, string password, string confirmPassword, string role)
        {
            // Перевірка імені та прізвища
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Будь ласка, введіть ім'я та прізвище", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Перевірка email
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Будь ласка, введіть коректну електронну пошту", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Перевірка пароля
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                MessageBox.Show("Пароль повинен містити щонайменше 6 символів", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Перевірка збігу паролів
            if (password != confirmPassword)
            {
                MessageBox.Show("Паролі не співпадають", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Перевірка ролі
            if (string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Будь ласка, оберіть роль", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private async Task<bool> IsUserExists(string email)
        {
            try
            {
                var students = await LoadUsers<Student>(_studentsFilePath);
                var tutors = await LoadUsers<Tutor>(_tutorsFilePath);

                return students?.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) == true ||
                       tutors?.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) == true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка перевірки наявності користувача: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }

        private User CreateUser(string firstName, string lastName, string email, string password, string role)
        {
            var passwordHash = HashPassword(password);
            var defaultBirthDate = new DateTime(2000, 1, 1);
            var defaultPhone = "+380000000000";

            return role switch
            {
                "Student" => new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Name = $"{firstName} {lastName}",
                    Email = email,
                    PasswordHash = passwordHash,
                    BirthDate = defaultBirthDate,
                    Phone = defaultPhone,
                    GradeLevel = 1,
                    Level = "Beginner",
                    Balance = 0,
                    Subjects = new List<Subject>()
                },
                "Tutor" => new Tutor
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Name = $"{firstName} {lastName}",
                    Email = email,
                    PasswordHash = passwordHash,
                    BirthDate = defaultBirthDate,
                    Phone = defaultPhone,
                    HourlyRate = 300,
                    Rating = 0,
                    YearsOfExperience = 1,
                    Subjects = new List<Subject>()
                },
                _ => throw new InvalidOperationException("Невідома роль користувача")
            };
        }

        private async Task<bool> SaveUser(User user, string filePath)
        {
            try
            {
                List<User> users = await LoadUsers<User>(filePath);
                user.UserId = users.Count > 0 ? users.Max(u => u.UserId) + 1 : 1;
                users.Add(user);

                string json = JsonConvert.SerializeObject(users, Formatting.Indented);

                string tempFile = Path.GetTempFileName();
                await File.WriteAllTextAsync(tempFile, json);
                File.Replace(tempFile, filePath, null);

                var savedUsers = await LoadUsers<User>(filePath);
                return savedUsers.Any(u => u.Email == user.Email);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка збереження користувача: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private async Task<List<T>> LoadUsers<T>(string filePath) where T : User
        {
            if (!File.Exists(filePath)) return new List<T>();

            try
            {
                string json = await File.ReadAllTextAsync(filePath);
                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Помилка читання файлу {Path.GetFileName(filePath)}: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<T>();
            }
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void SwitchToLogin_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
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

        private void ToggleConfirmPasswordVisibility(object sender, RoutedEventArgs e)
        {
            if (ConfirmPasswordBox.Visibility == Visibility.Visible)
            {
                ConfirmPasswordBox.Visibility = Visibility.Collapsed;
                ConfirmPasswordTextBox.Visibility = Visibility.Visible;
                ConfirmPasswordTextBox.Text = ConfirmPasswordBox.Password;
            }
            else
            {
                ConfirmPasswordBox.Visibility = Visibility.Visible;
                ConfirmPasswordTextBox.Visibility = Visibility.Collapsed;
                ConfirmPasswordBox.Password = ConfirmPasswordTextBox.Text;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
                PasswordTextBox.Text = PasswordBox.Password;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ConfirmPasswordBox.Visibility == Visibility.Visible)
                ConfirmPasswordTextBox.Text = ConfirmPasswordBox.Password;
        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordTextBox.Visibility == Visibility.Visible)
                PasswordBox.Password = PasswordTextBox.Text;
        }

        private void ConfirmPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ConfirmPasswordTextBox.Visibility == Visibility.Visible)
                ConfirmPasswordBox.Password = ConfirmPasswordTextBox.Text;
        }
    }
}