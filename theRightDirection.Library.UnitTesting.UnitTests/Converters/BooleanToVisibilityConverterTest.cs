using System;
using System.Windows;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using theRightDirection.WPF.Xaml.Converters;

namespace theRightDirection.Library.UnitTesting.UnitTests.Converters
{
    [TestClass]
    public class BooleanToVisibilityConverterTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var converter = new BooleanToVisibilityConverter();
            var result = converter.Convert(false, null, null, null);
            result.Should().Be(Visibility.Collapsed);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var converter = new BooleanToVisibilityConverter();
            var result = converter.Convert(true, null, null, null);
            result.Should().Be(Visibility.Visible);
        }
        [TestMethod]
        public void TestMethod3()
        {
            var converter = new BooleanToVisibilityConverter();
            var result = converter.Convert("False", null, null, null);
            result.Should().Be(Visibility.Collapsed);
        }
        [TestMethod]
        public void TestMethod4()
        {
            var converter = new BooleanToVisibilityConverter();
            var result = converter.Convert("True", null, null, null);
            result.Should().Be(Visibility.Visible);
        }
    }
}
