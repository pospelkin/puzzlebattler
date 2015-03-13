using UnityEngine;

namespace Libraries {
	public class CCell {

		public static CCell zero {
			get { return new CCell(0, 0); }
		}

		public int row, col;

		public CCell(int row, int col) {
			this.row = row;
			this.col = col;
		}

		public void Set (int newRow, int newCol) {
			this.row = newRow;
			this.col = newCol;
		}

		public void Set (CCell newPoint) {
			this.row = newPoint.row;
			this.col = newPoint.col;
		}

		public CCell Clone () {
			return new CCell(row, col);
		}

		public override string ToString () {
			return "Point(" + row + ", " + col + ")";
		}

		public bool Equals(CCell obj) {
			return row == obj.row && col == obj.col;
		}

		public static implicit operator CCell (Vector2 vector) {
			return new CCell((int) vector.x, (int) vector.y);
		}

		public static implicit operator Vector2 (CCell point) {
			return new Vector2((float) point.row, (float) point.col);
		}

		public static implicit operator CCell (Vector3 vector) {
			return new CCell((int) vector.x, (int) vector.y);
		}

		public static implicit operator Vector3 (CCell point) {
			return new Vector3((float) point.row, (float) point.col, 0);
		}

	}
}