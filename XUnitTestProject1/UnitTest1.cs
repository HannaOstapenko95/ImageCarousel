using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var x = 5;
            var y = 5;
            Assert.Equal(x, y);
        }

        [Fact]
       public void WriteImagesNamesInFile()
        {

        }
    }
}
