using Libraries;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Match.Gem {

	public class CIcon : MonoBehaviour {

		public CCell cell = CCell.zero;
		public EColor color { get; private set; }
		private EState state = EState.Idle;

		public void SetRandomColor() {
			int count = Enum.GetNames( typeof( EColor ) ).Length;

			color = (EColor) UnityEngine.Random.Range(0, count);

			GetComponent<Image>().sprite = CGame.Config.match.gems.GetCorrectSprite(color);
			UpdateName();
		}

		public void SetState (EState state) {
			this.state = state;

			gameObject.SetActive(IsVisibleState());

			UpdateName();
		}

		protected bool IsVisibleState () {
			return state == EState.Idle || state == EState.Movement;
		}

		protected void UpdateName () {
			gameObject.name = String.Format("Gem {0} [{1}:{2}] {3}",
				color.ToString()[0],
				cell.col,
				cell.row,
				IsIdle() ? "" : "*"
			);
		}

		public bool IsIdle() {
			return (state == EState.Idle);
		}

		public bool IsDead() {
			return (state == EState.Death);
		}

		public bool IsMoving() {
			return (state == EState.Movement);
		}

		public bool HitTest(Vector2 coordinates) {
			var worldPoint = Camera.main.ScreenToWorldPoint((Vector3) coordinates);

			return collider2D.OverlapPoint((Vector2) worldPoint);
		}

		public bool IsMatchable (CIcon icon) {
			return icon.color == this.color;
		}

		public bool IsNeighbour (CIcon target) {
			if (this == target) return false;

			int rowDistance = Mathf.Abs(cell.row - target.cell.row);
			int colDistance = Mathf.Abs(cell.col - target.cell.col);

			return (rowDistance == 1 && colDistance == 0) || (rowDistance == 0 && colDistance == 1);
		}

	}
}