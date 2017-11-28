using System;
using System.Threading;

namespace Bot_B {
	class Program {
		static void Main (string[] args) {

			Thread.Sleep(1000); // Wait for console size to change

			var b1 = new Bot_B();
			b1.Start();

			Log ls = Log.Instance; 
			ls.Save();
			Console.WriteLine("\nPress any key to continue ...");
			Console.ReadKey();

		}
	}
}
