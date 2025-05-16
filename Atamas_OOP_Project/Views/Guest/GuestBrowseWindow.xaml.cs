using Atamas_OOP_Project.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Atamas_OOP_Project.Views
{
    public partial class GuestBrowseWindow : Window
    {
        private const string DataFileName = "tutors.json";

        public GuestBrowseWindow()
        {
            InitializeComponent();
            LoadTeachers();
        }

        private string GetTutorsFilePath()
        {
            // Шлях до папки Data проекту
            string projectPath = @"C:\Users\ілля\Desktop\2 к 2 с\Курсова\Atamas_OOP_Project\Atamas_OOP_Project";
            return Path.Combine(projectPath, "Data", DataFileName);
        }

        private void LoadTeachers()
        {
            try
            {
                string filePath = GetTutorsFilePath();

                if (!File.Exists(filePath))
                {
                    ShowMessage("Файл з викладачами не знайдено");
                    return;
                }

                string json = File.ReadAllText(filePath);
                var tutors = JsonConvert.DeserializeObject<List<Tutor>>(json) ?? new List<Tutor>();

                DisplayTeachers(tutors);
            }
            catch (Exception ex)
            {
                ShowError($"Помилка завантаження даних: {ex.Message}");
            }
        }

        private void DisplayTeachers(List<Tutor> tutors)
        {
            TeachersListBox.Items.Clear();

            if (!tutors.Any())
            {
                ShowMessage("Викладачі не знайдені");
                return;
            }

            foreach (var tutor in tutors)
            {
                string subjects = GetSubjectsString(tutor);
                TeachersListBox.Items.Add(CreateTeacherDisplayString(tutor, subjects));
            }
        }

        private string GetSubjectsString(Tutor tutor)
        {
            return tutor.Subjects?.Any() == true
                ? string.Join(", ", tutor.Subjects.Select(s => s.Name))
                : "—";
        }

        private string CreateTeacherDisplayString(Tutor tutor, string subjects)
        {
            return $"{tutor.FirstName} {tutor.LastName} | " +
                   $"Предмети: {subjects} | " +
                   $"Рейтинг: {tutor.Rating:F1} | " +
                   $"Досвід: {tutor.YearsOfExperience} р.";
        }

        private void ShowMessage(string message)
        {
            TeachersListBox.Items.Add(message);
        }

        private void ShowError(string error)
        {
            MessageBox.Show(error, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}