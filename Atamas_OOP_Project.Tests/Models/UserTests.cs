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
    public class UserTests
    {
        [TestMethod]
        public void User_Constructor_SetsDefaultValues()
        {
            var user = new Student();

            Assert.IsNotNull(user.UserId);
            Assert.AreEqual("", user.Name);
        }

        [TestMethod]
        public void User_SetAndGetName_WorksCorrectly()
        {
            var user = new Student();
            user.Name = "John";
            Assert.AreEqual("John", user.Name);
        }
    }
}

