using Match.Gem;
using UnityEngine;

namespace Match {
	public class CInput : Libraries.IInputObserver {
		public CField field = null;

		protected CIcon selectedIcon = null;

		public CInput (CField field) {
			this.field = field;

			CGame.Input.registerObserver(this, 0);
		}

		public bool OnInputBegin (Vector2 position) {
			var selectedIcon = field.GetIconByPoint(position);

			if (IsActiveIcon(selectedIcon)) {
				this.selectedIcon = selectedIcon;
				return true;
			} else {
				return false;
			}
		}

		public void OnInputMove (Vector2 position) {
			if (!CGame.Instance.model.player.CanSwipe()) return;
			if (!IsActiveIcon(selectedIcon)) return;

			CIcon targetIcon = field.GetIconByPoint(position);

			if (selectedIcon != targetIcon && IsActiveIcon(targetIcon)) {
				if (selectedIcon.IsNeighbour(targetIcon)) {
					if (StartSwipe(selectedIcon, targetIcon)) {
						CGame.Input.Block();
					}
				}

				selectedIcon = null;
			}
		}

		public void OnInputEnd (Vector2 position) {
			selectedIcon = null;
		}

		public bool StartSwipe(CIcon selected, CIcon targeted) {
			return field.actions.Add(new Actions.CSwipe(field, selected, targeted));
		}

		private bool IsActiveIcon (CIcon icon) {
			return icon != null && icon.IsIdle();
		}
	}
}