using Atamas_OOP_Project.Interfaces;
using Atamas_OOP_Project.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Atamas_OOP_Project.Services
{
    public class LocalDataManager : IDataManager
    {
        public string JsonFilePath { get; set; }

        public LocalDataManager(string jsonFilePath)
        {
            JsonFilePath = jsonFilePath;

            // Створюємо директорію, якщо її немає
            Directory.CreateDirectory(jsonFilePath);
        }

        public void Save(object data, string fileName)
        {
            try
            {
                string filePath = Path.Combine(JsonFilePath, fileName);
                string json = JsonConvert.SerializeObject(data, GetJsonSettings());

                // Запис з тимчасовим файлом для безпеки
                string tempFile = Path.GetTempFileName();
                File.WriteAllText(tempFile, json);
                File.Replace(tempFile, filePath, null);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Помилка збереження файлу {fileName}: {ex.Message}", ex);
            }
        }

        public T Load<T>(string fileName)
        {
            string filePath = Path.Combine(JsonFilePath, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {fileName} не знайдено в {JsonFilePath}");
            }

            try
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(json, GetJsonSettings());
            }
            catch (JsonException ex)
            {
                throw new ApplicationException($"Помилка формату JSON у файлі {fileName}: {ex.Message}", ex);
            }
        }

        private JsonSerializerSettings GetJsonSettings()
        {
            return new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
            };
        }

        public void SaveSubjects(List<Subject> subjects, string fileName = "subjects.json")
        {
            string filePath = Path.Combine(JsonFilePath, fileName);
            var json = JsonConvert.SerializeObject(subjects, GetJsonSettings());
            File.WriteAllText(filePath, json);
        }

        public List<Subject> LoadSubjects(string fileName = "subjects.json")
        {
            string filePath = Path.Combine(JsonFilePath, fileName);
            if (!File.Exists(filePath)) return new List<Subject>();

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Subject>>(json, GetJsonSettings())
                   ?? new List<Subject>();
        }

        // Додаткові методи для конкретних типів
        public List<Student> LoadStudents()
        {
            return Load<List<Student>>("students.json");
        }

        public List<Tutor> LoadTutors()
        {
            return Load<List<Tutor>>("tutors.json");
        }

        public List<Admin> LoadAdmins()
        {
            return Load<List<Admin>>("admins.json");
        }

        public void SaveStudents(List<Student> students)
        {
            Save(students, "students.json");
        }

        public void SaveTutors(List<Tutor> tutors)
        {
            Save(tutors, "tutors.json");
        }

        public void SaveAdmins(List<Admin> admins)
        {
            Save(admins, "admins.json");
        }
    }
}