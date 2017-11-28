using System.Threading;
using NUnit.Framework;

namespace Bot_B.Test
{
  [TestFixture]
  public class StoreTests
  {
    private Store _store;
    private Iitem _testItem;
    
    [SetUp]
    public void init()
    {
      _store = new Store("test");
      _testItem = new Item("Audi", 100, "Dette er en bil");
    }
    
    [Test]// test deliver and buy method
    public void TestIfBuyDeliverTheItemIfExistInStore()
    {
      _store.DeliverItem(_testItem);
      var testItem2 = _store.Buy(_testItem);
      
      Assert.True(testItem2.Equals(_testItem));
      Assert.True(_store.Items.Count == 0);
    }

    [Test]
    public void TestIfStartStoreAddItems()
    {
      Thread a = new Thread(_store.StartStore);
      a.Start();
      Thread.Sleep(3000);
      _store.Shutdown();
      Assert.True(_store.Items.Count == 3);

    }
  }
}
