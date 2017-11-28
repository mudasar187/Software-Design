using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Bot_B.Test
{
    [TestFixture]
    public class ItemFactoryTest
    {
        private string[] _descriptions;
        private ItemProperties[] _itemProperties; 
        
        [SetUp]
        public void Init()
        {
             _descriptions = new []{
                "with beautiful south-african diamonds",
                 "with Super-duper soft furry",
                 "covered in 5-karat gold",
                 "covered in high quality leather",
                 "covered in beautiful tiger print"
            };
            _itemProperties = new[]
            {
                ItemProperties.Furry,
                ItemProperties.Leather,
                ItemProperties.TigerPrint,
                ItemProperties.Gold,
                ItemProperties.Diamond
            };
        }

        [Test]
        public void CreateRandomGotNoDuplicatePropertysTest()
        {
            for (int i = 0; i < 1000; i++)
            {
                Iitem checkItem = ItemFactory.CreateRandom(100);

                foreach (var property in _descriptions)
                {
                    Assert.True(Regex.Matches(checkItem.GetDesc(),property).Count <2);
                }
            }
        }

        [Test]
        public void CreateTest()
        { 
            Iitem item =ItemFactory.Create(99.99);
           
            Assert.True(item != null && item.GetPrice() == 99.99);
        }
        
        [Test]
        public void CreateSpesificTest()
        { 
            Iitem checkItem =ItemFactory.CreateSpecific(100,_itemProperties);
           
            foreach (var property in _descriptions)
            {
                Assert.True(Regex.Matches(checkItem.GetDesc(),property).Count == 1);
            }
        }
    }
}