using DG.Tweening;
using Libraries;
using System.Collections.Generic;
using UnityEngine;

namespace Match.Gem {

	public class CMove {

		private IMoveObserver listener;
		private List<CIcon> moving = new List<CIcon>();

		public void SetObserver(IMoveObserver listener) {
			this.listener = listener;
		}

		public bool IsFinished () {
			return moving.Count == 0;
		}

		public void OnEndMoveComplete (CIcon icon) {
			if (IsFinished()) {
				return;
			}

			icon.SetState(EState.Idle);
			moving.Remove(icon);

			if (IsFinished() && listener != null) {
				listener.OnMoveEnd();
			}
		}

		public bool AddMove(CIcon icon, Vector3 pos) {
			if (icon.transform.position == pos) {
				return false;
			} else {
				moving.Add(icon);

				icon.SetState(EState.Movement);
				icon.gameObject.transform
					.DOMove(pos, CGame.Config.match.gems.movingTime)
					.OnComplete(() => OnEndMoveComplete(icon));

				return true;
			}
		}

	}

}
