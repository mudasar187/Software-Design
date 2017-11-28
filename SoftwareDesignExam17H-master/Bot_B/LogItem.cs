using System;

namespace Bot_B {
	struct LogItem {
		
		//Fields of the class
		public string Sender { get; }
		public string Message { get; }
		public DateTime Timestamp { get; }
		
		/*'
		 *	A constructer that takes to Strings in the parameters.
		 * 	sender's information and their message. 
		 */
		public LogItem (string sender, string msg) {

			Timestamp = DateTime.Now;
			Sender = sender;
			Message = msg;

		}

	}
}
