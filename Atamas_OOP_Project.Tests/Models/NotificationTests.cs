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
    public class NotificationTests
    {
        [TestMethod]
        public void Notification_MessageAssignment_Works()
        {
            var note = new Notification();
            note.Message = "Test Message";
            Assert.AreEqual("Test Message", note.Message);
        }

        [TestMethod]
        public void Notification_SetDate()
        {
            var note = new Notification();
            var date = DateTime.Now;
            note.Date = date;
            Assert.AreEqual(date, note.Date);
        }
    }
}