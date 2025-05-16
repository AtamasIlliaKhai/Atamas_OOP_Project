using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Atamas_OOP_Project.Models
{
    [JsonObject(IsReference = false)]
    public class Admin : User
    {
        [JsonProperty("permissionLevel")]
        public string PermissionLevel { get; set; } = "Basic";

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Admin() : base() { }

        public Admin(
            int userId,
            string name,
            string email,
            string passwordHash,
            string firstName,
            string lastName,
            DateTime birthDate,
            string phone,
            string permissionLevel = "Basic",
            DateTime? createdAt = null)
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
            PermissionLevel = permissionLevel;
            CreatedAt = createdAt ?? DateTime.Now;
        }

        public void BlockStudent(List<Student> students, int studentId)
        {
            var student = students.FirstOrDefault(s => s.UserId == studentId);
            if (student != null)
            {
                students.Remove(student);
            }
        }

        public void BlockTutor(List<Tutor> tutors, int tutorId)
        {
            var tutor = tutors.FirstOrDefault(t => t.UserId == tutorId);
            if (tutor != null)
            {
                tutors.Remove(tutor);
            }
        }

        public Subject CreateSubject(string name)
        {
            return new Subject { Name = name };
        }

        public void AssignSubjectToStudent(Student student, Subject subject)
        {
            student.AddSubject(subject);
        }

        public void AssignSubjectToTutor(Tutor tutor, Subject subject)
        {
            tutor.AddTeachingSubject(subject);
        }

        public string FullDisplay => $"{FirstName} {LastName} ({Email})";

        public void ChangePermissionLevel(string newLevel)
        {
            if (!string.IsNullOrWhiteSpace(newLevel))
            {
                PermissionLevel = newLevel;
            }
        }

        public void UpdateStudentInfo(Student student, string firstName, string lastName, string phone)
        {
            if (student != null)
            {
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Phone = phone;
            }
        }

        public void UpdateTutorInfo(Tutor tutor, string firstName, string lastName, string phone)
        {
            if (tutor != null)
            {
                tutor.FirstName = firstName;
                tutor.LastName = lastName;
                tutor.Phone = phone;
            }
        }

        public List<Student> GetStudentsBySubject(List<Student> students, string subjectName)
        {
            return students.Where(s => s.Subjects.Any(sub => sub.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        public List<Tutor> GetTutorsBySubject(List<Tutor> tutors, string subjectName)
        {
            return tutors.Where(t => t.Subjects.Any(sub => sub.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase))).ToList();
        }

    }
}