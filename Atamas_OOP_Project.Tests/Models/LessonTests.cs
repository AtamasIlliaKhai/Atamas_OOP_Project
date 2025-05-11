using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Atamas_OOP_Project.Models;
using System;

namespace Atamas_OOP_Project.Tests.Models
{
    [TestClass]
    public class LessonTests
    {
        [TestMethod]
        public void Lesson_CanSetDateTime()
        {
            var lesson = new Lesson();
            var date = DateTime.Now;
            lesson.Date = date;
            Assert.AreEqual(date, lesson.Date);
        }

        [TestMethod]
        public void Lesson_CanAssignSubject()
        {
            var lesson = new Lesson();
            var subject = new Subject { Name = "Math" };
            lesson.Subject = subject;
            Assert.AreEqual("Math", lesson.Subject.Name);
        }
    }
}