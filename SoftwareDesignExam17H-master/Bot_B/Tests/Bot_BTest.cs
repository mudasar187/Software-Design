using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Bot_B.Test
{
    [TestFixture]
    public class Bot_BTest
    {
        private Bot_B _botB;

        [SetUp]
        public void Init()
        {
            _botB = new Bot_B();
        }

        [Test]//Files need to be in the test build folder to work
        public void TestReadfile()
        {
            
            List<String> testList = _botB.readFile(@"test.txt");
            Console.WriteLine(testList);
            
            Assert.True(testList.Count == 4);
            Assert.True(testList.Contains("test1"));
            Assert.True(testList.Contains("test2"));
            Assert.True(testList.Contains("test3"));
            Assert.True(testList.Contains("test4"));
        }
    }
}