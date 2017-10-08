using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSnake {
	class Apple : Vector2D {

		public char Icon { get; set; }

		public Apple (int xMax, int yMax, List<Vector2D> mask, int x = 0, int y = 0) : base(x, y) { // initilize the apples pos somewhere not on the snake
			ChangePos(xMax, yMax, mask);
			Icon = '$';
		}

		public void ChangePos (int xMax, int yMax, List<Vector2D> mask = null) {
			Random rnd = new Random();
			bool okPos = false;
			int x;
			int y;
			while (!okPos) {
				x = rnd.Next(0, xMax);
				y = rnd.Next(0, yMax);
				if (checkIsOutside(mask)) {
					X = x;
					Y = y;
					okPos = true;
				}
			}
		}

		public bool checkIsOutside (List<Vector2D> mask) {
			bool isOutside = true;
			foreach (Vector2D v in mask) {
				if (this == v) {
					isOutside = false;
				  break;
				}
			}
			return isOutside;
		}

	}
}
