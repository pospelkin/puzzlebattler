using Libraries;
using Match.Gem;
using System.Collections.Generic;

namespace Match {
	public abstract class CFieldHandler {
		public CField field;

		public CFieldHandler (CField field) {
			this.field = field;
		}

		protected int GetSquare () {
			return field.width * field.height;
		}

		protected bool IsOutOfHorisontalRange (int index) {
			return index % field.width >= field.width - 2;
		}

		protected bool IsOutOfVeticalRange (int index) {
			return index >= (GetSquare() - field.width * 2);
		}

		public List<CIcon> GetNeighbours (CIcon icon) {
			var list = new List<CIcon>();

			if (icon.cell.col > 0) {
				list.Add(field.GetIconAt(new CCell( icon.cell.row, icon.cell.col - 1 )));
			}
			if (icon.cell.col < field.width - 1) {
				list.Add(field.GetIconAt(new CCell( icon.cell.row, icon.cell.col + 1 )));
			}
			if (icon.cell.row > 0) {
				list.Add(field.GetIconAt(new CCell( icon.cell.row - 1, icon.cell.col )));
			}
			if (icon.cell.row < field.height - 1) {
				list.Add(field.GetIconAt(new CCell( icon.cell.row + 1, icon.cell.col )));
			}

			return list;
		}

		protected CIcon GetIconByIndex(int index) {
			if (index < 0 || index >= GetSquare()) {
				return null;
			}

			int row = index / field.width;
			int column = index - row * field.width;

			return field.GetIconAt(new CCell(row, column));
		}

		protected bool IsMatch(int first, int second, int third) {
			var icons = new List<CIcon>();

			icons.Add( GetIconByIndex(first) );
			icons.Add( GetIconByIndex(second) );
			icons.Add( GetIconByIndex(third) );

			return IsAllIconsActive(icons)
				&& icons[1].IsMatchable(icons[0])
				&& icons[2].IsMatchable(icons[0]);
		}

		protected bool IsAllIconsActive (List<CIcon> icons) {
			foreach (CIcon icon in icons) {
				if(icon && !icon.IsIdle()) {
					return false;
				}
			}

			return true;
		}
	}
}