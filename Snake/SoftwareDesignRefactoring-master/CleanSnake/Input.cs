using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSnake {
    class Input {

        public InputType Type;
        public ConsoleKey Key;
        public Vector2D Dir;

        public Input (InputType t, ConsoleKey key, Vector2D v = null) {
            Type = t;
            Key = key;
            Dir = v;
        }

    }
}
