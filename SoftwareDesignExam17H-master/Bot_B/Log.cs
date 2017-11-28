using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bot_B {
	class Log {
		
		//Fields for the Class Log
		private Object _lock = new Object();
		private static Log _instace = null;
		private List<LogItem> _entries;
		private StreamWriter _sw; 
		
		
		private Log () {
			_entries = new List<LogItem>();
			
		}
		/*
		 * Log Instance allows us to create a instance of the class.
		 */
		public static Log Instance {
			get {
				if (_instace == null) {
					_instace = new Log();
				}
				return _instace;
			}
		}
		
		/**
		 *  Puts every Item object in a list so that its easier for us to later on print the
		 * 	proccess of the program. The method is equipped with a lock method so that Items can
		 * 	be added to the list one by one. 
		 */
		public void Write (LogItem item) {

			lock(_lock) {

				_entries.Add(item);

			}
		}
		
		/**
		 * Write method will create a LogItem Object based on the parameters and then
		 * send the object to Write(LogItem item) method.
		 */
		public void Write (string sender, string msg) {

			LogItem newItem = new LogItem(sender, msg);

			Write(newItem);

		}
		
		/**
		 * The Save method will start the proccess for creating a textfile and
		 * write the whole proccess of the program. Every Items that has been created
		 * and sold. It will also append to the same file for everytime you run the program. 
		 */
		public void Save () {

			
			lock (_lock)
			{
				
				try
				{
					_sw = new StreamWriter(@"TextFiles\Logging.txt", append: true);
					
					_sw.WriteLine("Start of the program");
					_sw.WriteLine(_entries.ElementAt(0).Timestamp);
					
					for (int i = 0; i < _entries.Count; i++)
					{
						_sw.WriteLine( "\n Sender: " + _entries.ElementAt(i).Sender +
					         	 "\n Message: " + _entries.ElementAt(i).Message);
								
						
					}
					
					_sw.WriteLine("______________________________________________________________\n ");
					
					
					_sw.Close();
					
				}
				catch (Exception e)
				{
					Console.WriteLine(e);

				}

			}

		}

	}
}
