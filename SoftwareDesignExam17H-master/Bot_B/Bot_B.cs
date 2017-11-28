using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Bot_B {
	class Bot_B {

		private List<Store>    _store_list;
		private List<Consumer> _consumer_list;
		private List<Producer> _producer_list;
		private List<Thread>   _treads;
		private List<Thread>   _consumer_treads;

		public Bot_B () {
			List<string> listOfConsumerNames;
			List<string> listofStoreNames;
			List<string> listofProducerNames; 
			_store_list      = new List<Store>();
			_consumer_list   = new List<Consumer>();
			_producer_list   = new List<Producer>();
			_treads          = new List<Thread>();
			_consumer_treads = new List<Thread>();
			
			listOfConsumerNames = readFile(@"TextFiles\ConsumerNames.txt");
			listofStoreNames = readFile(@"TextFiles\StoreNames.txt");
			listofProducerNames = readFile(@"TextFiles\ProducerNames.txt");

			
			var rng = TSRandom.Instance;
			int numStores    = rng.Next(4, 8);
			int numConsumers = rng.Next(4, 8);
			int numProducers = rng.Next(4, 8);

			for (int i = 0; i < numStores; i++) {
				
				_store_list.Add(new Store(listofStoreNames.ElementAt(i))); 
			}
 
			for (int i = 0; i < numConsumers; i++) {
				var newConsumer = new Consumer(listOfConsumerNames.ElementAt(i), _store_list);
				_consumer_list.Add(newConsumer); 
			}

			for (int i = 0; i < numProducers; i++) {

				// Each producer need a random list of stores to send items to
				List<Store> deliveryList = _store_list.Where( item => rng.Next(10) < 3 ).ToList();

				while (deliveryList.Count == 0) { // This list can never be empty
					deliveryList = _store_list.Where(item => rng.Next(10) < 4).ToList();
				}

				_producer_list.Add(new Producer(deliveryList, listofProducerNames.ElementAt(i))); 
			}
		}


		public List<string> readFile(string filename) {
			
			List<string> textlList = new List<string>();
			List<string> shuffledList;
			Random rnd = new Random();

			try
			{
				StreamReader reader = new StreamReader(filename);
				string line;

				while ((line = reader.ReadLine()) != null)
				{					
					textlList.Add(line);
				
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
			
			shuffledList = textlList.OrderBy (x => rnd.Next()).ToList();
			
			
			return shuffledList; 
		}
		
	

		public void Start () {

			// Setup threads for stores and consumers
			foreach (Store s in _store_list) {
				_treads.Add(new Thread(s.StartStore));
			}
			foreach (Producer p in _producer_list) {
				_treads.Add(new Thread(p.Start));
			}
			foreach (Consumer c in _consumer_list) {
				_consumer_treads.Add(new Thread(c.StartConsumer));
			}

			// Start all treads
			foreach (Thread t in _treads) {
				t.Start();
			}
			// Start all consumer treads
			foreach (Thread t in _consumer_treads) {
				t.Start();
			}
			// Make sure all threads are proporly running
			foreach (Thread t in _treads) {
				while (!t.IsAlive);
			}
			// Make sure all consumer threads are proporly running
			foreach (Thread t in _consumer_treads) {
				while (!t.IsAlive);
			}
			Thread.Sleep(1);

			// Lets give the stores some time to run.
			Thread.Sleep(4000);

			// Now lets gracefully stop all threads
			foreach (Producer p in _producer_list) {
				p.Shutdown();
			}
			foreach (Store s in _store_list) {
				s.Shutdown();
			}

			// Join all threads to make sure that they all are finished 
			foreach (Thread t in _treads) {
				t.Join();
			}

			// stop and join all consumer threads
			foreach (Consumer c in _consumer_list) {
				c.Shutdown();
			}

			// Join all threads to make sure that they all are finished 
			foreach (Thread t in _consumer_treads) {
				t.Join();
			}

		}

	}
}
