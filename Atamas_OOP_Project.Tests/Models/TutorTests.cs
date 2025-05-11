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
    public class TutorTests
    {
        [TestMethod]
        public void Tutor_Constructor_InitializesEmptyLists()
        {
            var tutor = new Tutor();
            Assert.IsNotNull(tutor.Subjects);
            Assert.IsNotNull(tutor.Schedules);
        }

        [TestMethod]
        public void Tutor_CanSetExperience()
        {
            var tutor = new Tutor();
            tutor.YearsOfExperience = 5;
            Assert.AreEqual(5, tutor.YearsOfExperience);
        }
    }
}