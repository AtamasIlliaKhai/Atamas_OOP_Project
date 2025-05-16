using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Atamas_OOP_Project.Models
{
    [JsonObject(IsReference = false)]
    public class Student : User
    {
        [JsonProperty("gradeLevel")]
        public int GradeLevel { get; set; } = 1;

        [JsonProperty("subjects")]
        public List<Subject> Subjects { get; set; } = new List<Subject>();

        [JsonProperty("level")]
        public string Level { get; set; } = "Beginner";

        [JsonProperty("balance")]
        public decimal Balance { get; set; } = 0;

        [JsonIgnore]
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        [JsonIgnore]
        public List<Payment> Payments { get; set; } = new List<Payment>();

        [JsonIgnore]
        public string FullDisplay => $"{FirstName} {LastName} — {Email}";

        public Student() : base() { }

        public Student(
            int userId,
            string name,
            string email,
            string passwordHash,
            string firstName,
            string lastName,
            DateTime birthDate,
            string phone,
            int gradeLevel = 1,
            List<Subject> subjects = null,
            string level = "Beginner",
            decimal balance = 0)
            : base()
        {
            UserId = userId;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Phone = phone;
            GradeLevel = gradeLevel;
            Subjects = subjects ?? new List<Subject>();
            Level = level;
            Balance = balance;
        }

        public void AddSubject(Subject subject)
        {
            if (!Subjects.Any(s => s.SubjectId == subject.SubjectId))
            {
                Subjects.Add(subject);
            }
        }

        public void RemoveSubject(int subjectId)
        {
            var subject = Subjects.FirstOrDefault(s => s.SubjectId == subjectId);
            if (subject != null)
            {
                Subjects.Remove(subject);
            }
        }

        public List<Subject> GetSubjects()
        {
            return Subjects.ToList();
        }
    }
}