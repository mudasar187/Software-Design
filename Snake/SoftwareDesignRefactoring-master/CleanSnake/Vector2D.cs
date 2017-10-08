using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSnake {
	class Vector2D {
		public int X { get; set; }
		public int Y { get; set; }

		public Vector2D (int x = 0, int y = 0) {
			X = x;
			Y = y;
		}

		public Vector2D (Vector2D v) {
			X = v.X;
			Y = v.Y;
		}

		/*
		 * ## Methuds
		 */
		public Vector2D Add (Vector2D v) {
			X += v.X;
			Y += v.Y;
			return this;
		}

		public static Vector2D Add (Vector2D v1, Vector2D v2) => new Vector2D(v1).Add(v2);

		public Vector2D Multiply (Vector2D v) {
			X *= v.X;
			Y *= v.Y;
			return this;
		}

		public Vector2D Multiply (int scale) {
			X *= scale;
			Y *= scale;
			return this;
		}

		public static Vector2D Multiply (Vector2D v1, Vector2D v2) => new Vector2D(v1).Multiply(v2);

		public static Vector2D Multiply (Vector2D v, int scale) => new Vector2D(v).Multiply(scale);

		public bool Equals (Vector2D v1) => X == v1.X && Y == v1.Y;

		public bool Equals (Vector2D v1, Vector2D v2) => v1.Equals(v2);

		/*
		 * ## Operators
		 */

		public static bool operator == (Vector2D v1, Vector2D v2) {

			// Chech if comparing with null
			bool v1Null;
			try {
				v1.Equals(v1);
				v1Null = false;
			} catch (NullReferenceException e) {
				v1Null = true;
			}

			bool v2Null;
			try {
				v2.Equals(v2);
				v2Null = false;
			} catch (NullReferenceException e) {
				v2Null = true;
			}
			if (v1Null && v2Null) return true;
			if (v1Null || v2Null) return false;
			return v1.Equals(v2);
		}

		public static bool operator != (Vector2D v1, Vector2D v2) => !(v1 == v2);

		public static Vector2D operator + (Vector2D v1, Vector2D v2) => Vector2D.Add(v1, v2);

		public static Vector2D operator * (Vector2D v1, Vector2D v2) => Vector2D.Multiply(v1, v2);

		public static Vector2D operator * (Vector2D v, int scale) => Vector2D.Multiply(v, scale);

	}
}
