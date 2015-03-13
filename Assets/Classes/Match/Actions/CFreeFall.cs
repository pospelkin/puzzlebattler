using Libraries;
using Libraries.ActionSystem;
using Match.Gem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match.Actions {
	public class CFreeFall : CBase, IMoveObserver {
		protected CField field;
		protected CMove  move;

		public CFreeFall (CField field) {
			this.field = field;
		}

		public override bool Validation() {
			foreach (CIcon icon in field.icons) {
				if (icon.IsDead()) {
					return true;
				}
			}

			return false;
		}

		private void DeadFreeFall (int col, List<CIcon> dead) {
			int number = 0;
			int height = field.height;

			foreach (CIcon icon in dead) {
				field.SetIconAt(new CCell(height - dead.Count + number, col), icon);

				icon.SetState(EState.Idle);
				icon.gameObject.transform.position = field.GetIconCenterByCell(
					new CCell(height + number, col)
				);
				icon.SetRandomColor();
				move.AddMove( icon, field.GetIconCenterByCell(icon.cell) );

				number++;
			}
		}

		private void ColumnFreeFall (int col) {
			List<CIcon> dead = new List<CIcon>();

			for ( int row = 0; row < field.height; row++ ) {
				CIcon current = field.GetIconAt(new CCell(row, col));

				if (current.IsDead()) {
					dead.Add(current);
					continue;
				} else if (dead.Count == 0) {
					continue; // do nothing - current icon is now at correct place
				}

				field.SetIconAt(new CCell(row - dead.Count, col), current);

				move.AddMove( current, field.GetIconCenterByCell(current.cell) );
			}

			DeadFreeFall(col, dead);
		}

		public override void StartAction() {
			move = new CMove();

			for ( int col = 0; col < field.width; col++ ) {
				ColumnFreeFall(col);
			}

			if (move.IsFinished()) {
				ComplateAction();
			} else {
				move.SetObserver(this);
			}
		}

		public void OnMoveEnd () {
			ComplateAction();
		}

		public override int GetIndex() {
			return (int) EEvents.FreeFall;
		}
	}

}