using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using theRightDirection.Library.UnitTesting.AutoFixture;

namespace theRightDirection.Library.UnitTesting
{
    public sealed class MockBuilder
    {
        public static Mock<T> BuildMock<T>() where T:class
        {
            var fixture = new Fixture();
            fixture.Behaviors.Clear();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customize(new AutoPopulatedMoqPropertiesCustomization());
            fixture.Customize(new AutoMoqCustomization());
            fixture.Customize(new XDocumentCustomization());
            var mock = fixture.Create<T>();
            return Mock.Get<T>(mock);
        }
    }
}