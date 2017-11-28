using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Bot_B
{
    class Consumer
    {
        //Fields for the class Consumer
        private List<Iitem> _wish_list;
        private Log _log;
        private List<Store> _stores;
        private TSRandom _rng;
        private bool _running;
        
        /**
         * Propertie for string name.
         */
        public string Name { get; }

        public Consumer(string name, List<Store> stores)
        {
            _rng = TSRandom.Instance;

            Name = name;
            _log = Log.Instance;
            _stores = stores;

            _wish_list = new List<Iitem>();
            int list_count = _rng.Next(3, 5);
            for (int i = 0; i < list_count; i++)
            {
                _wish_list.Add(ItemFactory.CreateRandom(0)); // Price in this case is not imporant
            }
        }
        
        
        /**
         * This method will check if the costumers wishlist has the item that it is looking for.
         * If it does have the item that is on their wishlist it will then start the process of
         * buying it and then print the statment of buying it. This method will also start to log this so
         * that we can easier print out the whole proccess of the program later in to a file. 
         */
        
        private void OnNewItem(Store store, Iitem item)
        {
            _log.Write(Name, "Saw a new item in " + store.Name);

            Iitem bought;
            if (_wish_list.Contains(item))
            {
                bought = store.Buy(item);
            }
            else
            {
                Thread.Sleep(1);
                bought = store.Buy(item);
            }

            if (bought != null)
            {
                // right align text
                string[] strings =
                {
                    Name,
                    "- Bought the new item: ",
                    "- " + item.GetName(),
                    item.GetDesc(),
                    "| For: " + item.GetPrice() + " ¥"
                };
                int longestString = 0;

                for (int i = 0; i < strings.Length; i++)
                {
                    if (strings[i].Length > longestString)
                    {
                        longestString = strings[i].Length;
                    }
                }


                string output = String.Format("{0," + (Console.BufferWidth - (1 + longestString - strings[0].Length)) +
                                              "}\n" +
                                              "{1," + (Console.BufferWidth - (1 + longestString - strings[1].Length)) +
                                              "}\n" +
                                              "{2," + (Console.BufferWidth - (1 + longestString - strings[2].Length)) +
                                              "}\n" +
                                              "{3," + (Console.BufferWidth - (1 + longestString - strings[3].Length)) +
                                              "}\n" +
                                              "{4," + (Console.BufferWidth - (1 + longestString - strings[4].Length)) +
                                              "}\n",
                    strings[0],
                    strings[1],
                    strings[2],
                    strings[3],
                    strings[4]);
                lock (Console.Out)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(output);
                    Console.ForegroundColor = ConsoleColor.White;

                }
                //Console.WriteLine("{0," + (Console.BufferWidth - 1) + "}", output);
            }
        }
        
        /**
         * Will start to close the threads???!?! 
         */
        public void Shutdown()
        {
            _running = false;
        }
        
        /**
         * The Startconsumer method will start going through the items-list as long as items-list is not
         * empty and try to buy them buy calling on OnNewItem method. 
         */
        public void StartConsumer()
        {
            _running = true;

            while (_running)
            {
                foreach (Store s in _stores)
                {
                    while (s.Items.Count > 0)
                    {
                        OnNewItem(s, s.Items.First()); // Buy item
                    }
                } // END foreach
            } // END while running
        }
    }
}

