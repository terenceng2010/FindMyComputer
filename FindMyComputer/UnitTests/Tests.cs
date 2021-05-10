using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindMyComputer.UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ReadMemorySizeText()
        {
            decimal gb = FindMyComputer.Manager.ComputerManagerUtility.ReadRam("500 GB");
            Assert.AreEqual(500000, gb);

            decimal mb = FindMyComputer.Manager.ComputerManagerUtility.ReadRam("500000 MB");
            Assert.AreEqual(500000, mb);
        }
    }
}
