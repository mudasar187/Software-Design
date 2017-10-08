using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSnake {
	class BodyPart : Vector2D {

		public BodyPartIcon Icon { get; set; }

		public BodyPart (BodyPartIcon ico = BodyPartIcon.BODY, int x = 0, int y = 0) : base(x, y) {
			Icon = ico;
		}

		public BodyPart (int x = 0, int y = 0, BodyPartIcon ico = BodyPartIcon.BODY) : base(x, y) {
			Icon = ico;
		}

		public BodyPart (Vector2D v, BodyPartIcon ico = BodyPartIcon.BODY) : base(v) {
			Icon = ico;
		}

	}
}
