using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Atamas_OOP_Project.Models;

namespace Atamas_OOP_Project.Tests.Models
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void Student_Constructor_InitializesEmptyLists()
        {
            var student = new Student();
            Assert.IsNotNull(student.Subjects);
            Assert.IsNotNull(student.Notifications);
        }

        [TestMethod]
        public void Student_CanSetGradeLevel()
        {
            var student = new Student();
            student.GradeLevel = 10;
            Assert.AreEqual(10, student.GradeLevel);
        }
    }
}
