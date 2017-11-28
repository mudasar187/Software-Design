using System;
using System.Collections.Generic;

namespace Bot_B {
	class ItemFactory {

		// _names and _descriptions should be the same size
		private static string[] _names = {
			"Flying carpet", 
            "Vase", 
            "Cape", 
            "Whip", 
            "Riffle",
            "Goat", 
            "Turban",
            "shawl", 
            "stool",
            "plate",
		};
		private static string[] _descriptions = {
			"Simple but usefull ",
			"Huge but light ",
            "Colorful but still cool ", 
            "Cool and hot ", 
            "Weighs next to nothing ", 
            "Cool stickers on ", 
            "Almost new ", 
            "Shaped like a horse ", 
            "Easy and portable ", 
		};

		private ItemFactory () { } // Prevent initialization

		/**
		 * Creates a plain item without any features.
		 */
		public static Iitem Create (double price) {

			var random = TSRandom.Instance;
			/*                                     if defferent size pick smallest */
			int nameDescIndex = random.Next( Math.Min(_names.Length, _descriptions.Length) );
			var newItem = new Item(_names[nameDescIndex], price, _descriptions[nameDescIndex]);

			return newItem;

		}

		/**
		 * Creates an item with a random range of features
		 */
		public static Iitem CreateRandom (double price) {

			Array values = Enum.GetValues(typeof(ItemProperties));
			TSRandom random = TSRandom.Instance;
			List<Object> propertiesChosen = new List<Object>();


			var itemProps = new List<ItemProperties>();
			int i = 0;
			while (random.Next(10) < (4 - i)) {
				//make sure there is no duplicate property
				while (true)
				{
					var nextProperty = values.GetValue(random.Next(values.Length));
					if (propertiesChosen.Contains(nextProperty)) continue;
					propertiesChosen.Add(nextProperty);
					break;
				} 
				
				itemProps.Add((ItemProperties) propertiesChosen[propertiesChosen.Count-1]);
			    i++; 
			}

			return CreateSpecific(price, itemProps.ToArray());
			
		}

		/**
		 * Creates an item with fixed features
		 */
		public static Iitem CreateSpecific (double price, params ItemProperties[] parameters) {

			Iitem newItem = Create(price);
			foreach (ItemProperties prop in parameters) {


				switch (prop) {
                    case ItemProperties.Gold: newItem = new FeatureDecoratorGold(newItem);
                       break;
                    case ItemProperties.Diamond: newItem = new FeatureDecoratorDiamond(newItem);
                        break;

                    case ItemProperties.Furry: newItem = new FeatureDecoratorFurry(newItem);
                        break;

                    case ItemProperties.Leather: newItem = new FeatureDecoratorLeather(newItem);
                        break;

                    case ItemProperties.TigerPrint: newItem = new FeatureDecoratorTigerPrint(newItem);
                        break;

					default:
						throw new ArgumentException("Did not know how to handle ItemProporty : " + prop);

				}

			}

			return newItem;
		}

	}
}
