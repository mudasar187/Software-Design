using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CleanSnake {
	class GameController {

		private Snake snake;
		private Apple apple;
		private InputHandler IH;
		private Thread IHThread;
		private Display display;
		private bool playing, paused;
		private short ticksPerSecond;

		public GameController () {

			// setup display so we can use its dimentions
			display = new Display();

			// Setup primitives
			playing = true;
			paused = false;
			ticksPerSecond = 10;

			// Setup the snake and apple
			snake = new Snake(new Vector2D(1, 0));
			apple = new Apple(display.Width, display.Height, snake.BodyToVector2D());

			// Initilize the input handler
			IH = new InputHandler();
			IHThread = new Thread(IH.Run);
			IHThread.Start();
			// make sure thread is alive and running
			while (!IHThread.IsAlive) ;
			Thread.Sleep(1);

			// now last but not least lets print out the snake and apple
			display.PaintSnake(snake.parts);
			display.PaintApple(apple);

		}

		public void Run () {

			while (playing) {

				// Get and handle input
				Input _in = IH.Input;
				HandleInput(_in);

				// if we are paused stop here and redo while
				if (paused) {
					Thread.Sleep(1); /* Since there are two threads working on the same
															bool make sure we are not overwhelming IHThread */
					continue;
				}

				// Now that we got our input wich at this point should have been handled
				// lets calculate the next frame and print it

				// get the new head
				BodyPart nHead = snake.GetNewHead();

				/* 
				 * ### Check for end of game events
				 */

				// Check if new head is outside window
				if (display.IsOutside(nHead)) {
					playing = false;
					break;
				}
				foreach (BodyPart bp in snake.parts) {
					if (nHead == bp) {
						// Death by accidental self-cannibalism
						playing = false;
					}
				}
				// now make sure that the player is not a bot, by checing if the snake is covering the entire window
				if (snake.parts.Count + 1 == display.Width * display.Height) {
					// player is a bot make sure to break here as apple wont find another spot to place itself so we'll get a while (true) scenario
					playing = false;
					break;
				}

				/* 
				 * ### Now check for if snake has eaten the apple and update accordingly 
				 */

				if (nHead == apple) {
					// the snake ate the apple ChangePos on apple and update display
					apple.ChangePos(display.Width, display.Height, snake.BodyToVector2D());
					display.PaintApple(apple);
					display.UpdateSnake(nHead, snake.parts.Last());
				} else {
					// snake didn't eat the apple update the snake
					display.UpdateSnake(nHead, snake.parts.Last(), snake.parts.First());
					snake.RemoveTail();
				}

				// now finally add the new head to the sake's List of BodyPart(s)
				snake.UpdateHead(nHead);

				Thread.Sleep(1000 / ticksPerSecond);

			} // END While

			// Do ceanup before exit
			Cleanup();

		}

		private void HandleInput (Input _in) {

			if (_in != null) {
				if (_in.Type == InputType.OPPERATION) {

					if (_in.Key == ConsoleKey.Spacebar) { // pause the game
						paused = !paused;
					} else if (_in.Key == ConsoleKey.Escape) {
						playing = false;
					}


				} else if (_in.Type == InputType.MOVE) { // currently only two InputTypes so this has to be InputType.MOVE

					if (_in.Dir != snake.LastDir && // if same dir no need to update
							_in.Dir != Vector2D.Multiply(snake.LastDir, -1) /* if exact oposite can't upate*/) {

						snake.LastDir = _in.Dir;

					}

				} // END Direction handling
			} // END If _in != null

		} // END HandleInput()

		private void Cleanup () {

			Console.ResetColor();

			// shutdown IH 
			Console.WriteLine("Shuting down...");
			IH.Shutdown();
			IHThread.Join();
			Console.Write("All done! Goodbye o/");

		}

	}
}
