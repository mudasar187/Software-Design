using System;
using System.Collections.Generic;
using System.Threading;

namespace Bot_B {
	class Producer {

		private string _name;
		private List<List<ItemProperties>> _presets;
		private List<Store> _stores;
		private TSRandom _rng;
		private	bool _running = true;
		
		/**
		 * A proportie for the boolean _running.
		 */
		public bool IsRunning
		{
			get => _running;
			set => _running = value;
		}
		
		/**
		 * The constructer takes a List of Stores and a name in the
		 * parameters. 
		 */
		public Producer (List<Store> stores, string name) {

			_rng = TSRandom.Instance;
			_name = name;
			_stores = stores;
			_presets = new List<List<ItemProperties>>();

			var item_props = new List< ItemProperties>();
			Array values = Enum.GetValues(typeof(ItemProperties));

			int num_presetes = _rng.Next(3, 6);
			for (int i = 0; i < num_presetes; i++) {
				int j = 0;
				while (_rng.Next(10) < ( 7 - (j*1.5) )) {
					item_props.Add((ItemProperties) values.GetValue(_rng.Next(values.Length)));
				}
				_presets.Add(item_props);
				item_props.Clear();
			}

		}
		
		/**
		 * Every second the Start method will continue to call on the SendItem method every second
		 * which will start creating the items and send them to the stores. As long as the While loop is true. 
		 */
		public void Start ()
		{
			while (IsRunning)
			{
				SendItem();
				Thread.Sleep(1000);
			}

		}
		
		/**
		 * Creating a method that can stop creating the items. Notice
		 * this method will not stop the threads.
		 */
		public void Shutdown () => _running = false;

		private void SendItem () {
			
			int recievingStore = _rng.Next(_stores.Count);
			int preset = _rng.Next(_presets.Count);
			double price = _rng.Next(10, 5500); 
			Iitem itemToSend = ItemFactory.CreateSpecific(price, _presets[preset].ToArray());

			_stores[recievingStore].DeliverItem(itemToSend);

		}

	}
}
