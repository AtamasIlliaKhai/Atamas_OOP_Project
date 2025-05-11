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
    public class AdminTests
    {
        [TestMethod]
        public void Admin_CanManageUsers()
        {
            var admin = new Admin();
            Assert.IsInstanceOfType(admin, typeof(User));
        }

        [TestMethod]
        public void Admin_SetAndGetPermissions()
        {
            var admin = new Admin();
            admin.PermissionLevel = "High";
            Assert.AreEqual("High", admin.PermissionLevel);
        }
    }
}