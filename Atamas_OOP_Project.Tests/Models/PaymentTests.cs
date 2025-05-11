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
    public class PaymentTests
    {
        [TestMethod]
        public void Payment_SetAndGetAmount()
        {
            var payment = new Payment();
            payment.Amount = 100.5m;
            Assert.AreEqual(100.5m, payment.Amount);
        }

        [TestMethod]
        public void Payment_SetPaymentDate()
        {
            var payment = new Payment();
            var date = DateTime.Now;
            payment.Date = date;
            Assert.AreEqual(date, payment.Date);
        }
    }
}