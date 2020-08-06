using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace theRightDirection.Library.UnitTesting.UnitTests
{
    [TestClass]
    public class ExceptionExtensionTest
    {
        private AggregateException _ae;

        [TestInitialize]
        public void Setup()
        {
            _ae = new AggregateException(new List<Exception>() { new FileNotFoundException(), new ArgumentNullException(), new NullReferenceException() });
        }
        [TestMethod]
        public void InnerExceptions()
        {
            var result = _ae.InnerExceptions;
            result.Count.Should().Be(3);
        }
        [TestMethod]
        public void InnerExceptions2()
        {
            var result = _ae.GetInnerExceptions();
            result.Count().Should().Be(5);
        }
        [TestMethod]
        public void AggregateMessages()
        {
            var result = _ae.AggregateMessages();
            result.Length.Should().Be(180);
        }
    }
}