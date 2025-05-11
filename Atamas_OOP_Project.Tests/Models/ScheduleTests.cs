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
    public class ScheduleTests
    {
        [TestMethod]
        public void Schedule_CanSetStartEnd()
        {
            var schedule = new Schedule();
            var start = DateTime.Now;
            var end = start.AddHours(1);
            schedule.StartTime = start;
            schedule.EndTime = end;
            Assert.AreEqual(end, schedule.EndTime);
        }

        [TestMethod]
        public void Schedule_IsAvailableByDefault()
        {
            var schedule = new Schedule();
            Assert.IsTrue(schedule.IsAvailable);
        }
    }
}