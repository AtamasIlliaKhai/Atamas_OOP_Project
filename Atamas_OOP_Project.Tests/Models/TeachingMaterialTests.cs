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
    public class TeachingMaterialTests
    {
        [TestMethod]
        public void Material_Title_Works()
        {
            var material = new TeachingMaterial();
            material.Title = "Geometry";
            Assert.AreEqual("Geometry", material.Title);
        }

        [TestMethod]
        public void Material_AssociateSubject()
        {
            var material = new TeachingMaterial();
            var subject = new Subject { Name = "Biology" };
            material.Subject = subject;
            Assert.AreEqual("Biology", material.Subject.Name);
        }
    }
}
