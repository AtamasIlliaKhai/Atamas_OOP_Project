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

namespace Atamas_OOP_Project.Views
{
    public partial class TutorWindow : Window
    {
        private Tutor _tutor;

        public TutorWindow(Tutor tutor)
        {
            InitializeComponent();
            _tutor = tutor;

            Title = $"Панель викладача — {_tutor.FullDisplay}";

            // Заповнення полів
            NameTextBlock.Text = $"Ім'я: {_tutor.FirstName} {_tutor.LastName}";
            EmailTextBlock.Text = $"Email: {_tutor.Email}";
            PhoneTextBlock.Text = $"Телефон: {_tutor.Phone}";
            RateTextBlock.Text = $"Ставка за годину: {_tutor.HourlyRate} грн";
            RatingTextBlock.Text = $"Рейтинг: {_tutor.Rating}";
            ExperienceTextBlock.Text = $"Досвід: {_tutor.YearsOfExperience} років";

            var subjects = _tutor.Subjects?.Select(s => s.Name).ToList() ?? new();
            SubjectsTextBlock.Text = "Предмети: " + (subjects.Count > 0 ? string.Join(", ", subjects) : "немає");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}