using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSnake {

	public enum InputType {
		OPPERATION = 0,
		MOVE,
		NONE
	}

	public enum BodyPartIcon {
		HEAD = '@',
		BODY = '0'
	}

	class Program {
		static void Main (string[] args) {

			var gc = new GameController();
			gc.Run();

		}
	}
}
