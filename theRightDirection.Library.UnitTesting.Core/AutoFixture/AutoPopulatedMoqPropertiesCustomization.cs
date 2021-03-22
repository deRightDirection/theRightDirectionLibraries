using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library.UnitTesting.AutoFixture
{
    /*
    public class AutoPopulatedMoqPropertiesCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(
                new PropertiesPostprocessor(
                    new MockPostprocessor(
                        new MethodInvoker(
                            new MockConstructorQuery()))));
            fixture.ResidueCollectors.Add(
            new Postprocessor(
                new MockRelay(),
                new AutoPropertiesCommand(
                    new PropertiesOnlySpecification())));
        }
    }
    */
}