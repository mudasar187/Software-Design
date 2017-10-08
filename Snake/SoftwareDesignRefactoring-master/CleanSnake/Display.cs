using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSnake {
	class Display {

		public int Height { get; set; }
		public int Width { get; set; }

		public Display () {
			Console.CursorVisible = false;
			Height = Console.WindowHeight; Width = Console.WindowWidth;
		}

		public void PaintSnake (List<BodyPart> snake) {

			foreach (var bp in snake) {
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.SetCursorPosition(bp.X, bp.Y);
				Console.Write((char) bp.Icon);
			}

		}

		public void UpdateSnake (BodyPart nHead, BodyPart oldHead, BodyPart tail = null) { // old head changes ico repaint it

			if (tail != null) {
				Console.SetCursorPosition(tail.X, tail.Y);
				Console.Write(" ");
			}

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.SetCursorPosition(oldHead.X, oldHead.Y);
			Console.Write((char) oldHead.Icon);
			Console.SetCursorPosition(nHead.X, nHead.Y);
			Console.Write((char) nHead.Icon);

		}

		public void PaintApple (Apple apple) {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.SetCursorPosition(apple.X, apple.Y);
			Console.Write((char) apple.Icon);
		}

		public bool IsOutside (Vector2D v) => (v.X < 0 || v.X >= Width || v.Y < 0 || v.Y >= Height);

	}
}