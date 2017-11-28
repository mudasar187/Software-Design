namespace Bot_B {
	class Item : Iitem {
		
		//Fields for the Class Item
		private string _name;
		private double _price;
		private string _desc;
		
		/**
		 * Simple constructer that takes name, price and a description in
		 * the parameters.
		 */
		public Item (string name, double price, string desc) {

			_name = name;
			_price = price;
			_desc = desc;

		}
		
		/**
         * Simple GetDesc method that will return the description of the item.
         */
		public string GetDesc () {
			return _desc;
		}
		
		/**
        * Simple GetName method that will return the name of the item name.
        */
		public string GetName () {
			return _name;
		}

		/**
      	 * Simple GetPrice method that will return the Item's price + the extra features price. 
     	 */
		public double GetPrice () {
			return _price;
		}
	}
}
