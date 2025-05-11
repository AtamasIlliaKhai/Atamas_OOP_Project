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
    public class SubjectTests
    {
        [TestMethod]
        public void Subject_NameAssignment_Works()
        {
            var subject = new Subject();
            subject.Name = "Physics";
            Assert.AreEqual("Physics", subject.Name);
        }

        [TestMethod]
        public void Subject_Id_IsUnique()
        {
            var subject1 = new Subject();
            var subject2 = new Subject();
            Assert.AreNotEqual(subject1.SubjectId, subject2.SubjectId);
        }
    }
}
