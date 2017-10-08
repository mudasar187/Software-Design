using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSnake {
	class Snake {

		public List<BodyPart> parts;
		public Vector2D LastDir; // movement direction of snake

		public Snake (Vector2D startDir) { // Initial movement dir of snake
			// Create snake
      parts = new List<BodyPart>();
			for (int i = 0; i < 4; i++) {
				parts.Add(new BodyPart(10, 6 + i));
			}
			parts.Add(new BodyPart(BodyPartIcon.HEAD, 10, 11));

			LastDir = new Vector2D(0, 1);
		}

		public BodyPart GetNewHead () {

			// returns a new BodyPart with icon head using the current dir to move it correctly
			// make sure to change the icon of the last head to the body icon
		  parts.Last().Icon = BodyPartIcon.BODY;
			BodyPart nHead = new BodyPart(parts.Last(), BodyPartIcon.HEAD);
			nHead.Add(LastDir);
			return nHead;
		}

		public void UpdateHead (BodyPart newHead) { // add the new head to the parts List
      parts.Add(newHead);
		}

		public List<Vector2D> BodyToVector2D () { // makes new list ov Vector2D and populates it by converting each BodyPart to a vector
		  var vectorList = new List<Vector2D>();
		  foreach (var variable in parts)
		  {
		    vectorList.Add(variable);
		  }
		  return vectorList;
		}

    public void RemoveTail()
	  {
	    parts.Remove(parts.First());
	  }


	}
}
