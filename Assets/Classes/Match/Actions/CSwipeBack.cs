using Libraries.ActionSystem;
using Match.Gem;
using UnityEngine;

namespace Match.Actions {
	public class CSwipeBack : CSwipe {

		public CSwipeBack (CField field, CIcon selected, CIcon targeted) : base(field, selected, targeted) {}

		public override bool Validation() {
			return true;
		}

		public override void OnMoveEnd () {
			ComplateAction();
		}

		public override int GetIndex() {
			return (int) EEvents.SwipeBack;
		}
	}
}