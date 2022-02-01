using NUnit.Framework;
using Xamarin.UITest;

namespace BigCart.UITests
{
    public abstract class TestClass
    {
        protected readonly Platform _platform;
        protected IApp _app;

        public TestClass(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);
        }
    }
}
