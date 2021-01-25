using System;
using System.IO;
using AutoFixture;
using AutoFixture.AutoMoq;
using theRightDirection.Library.UnitTesting.AutoFixture;

namespace theRightDirection.Library.UnitTesting
{
    public abstract class UnitTestingBase
    {
        protected Fixture _fixture;

        public UnitTestingBase()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Clear();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customize(new AutoMoqCustomization());
            _fixture.Customize(new XDocumentCustomization());
        }

        /// <summary>
        /// Gets the unit test bin\debug-directory.
        /// </summary>
        public string UnitTestDirectory => Directory.GetCurrentDirectory().ToLowerInvariant();

        /// <summary>
        /// Deletes the file from bin\debug-directory.
        /// </summary>
        public void DeleteFileFromUnitTestDirectory(string filename)
        {
            var path = Path.Combine(UnitTestDirectory, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}