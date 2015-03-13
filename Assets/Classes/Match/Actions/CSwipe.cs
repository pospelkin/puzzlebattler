using Libraries;
using Libraries.ActionSystem;
using Match;
using Match.Gem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match.Actions {
	public class CSwipe : CCreating, IMoveObserver {

		protected CField field;
		protected CIcon selected;
		protected CIcon targeted;

		public CSwipe (CField field, CIcon selected, CIcon targeted) {
			this.field = field;
			this.selected = selected;
			this.targeted = targeted;
		}

		public override bool Validation() {
			return selected.IsIdle()
				&& targeted.IsIdle();
		}

		public override void StartAction() {
			var move = new CMove();

			CCell cell = selected.cell.Clone();

			field.SetIconAt(targeted.cell, selected);
			field.SetIconAt(cell, targeted);

			move.AddMove(selected, field.GetIconCenterByCell(selected.cell));
			move.AddMove(targeted, field.GetIconCenterByCell(targeted.cell));

			move.SetObserver(this);
		}

		public virtual void OnMoveEnd () {
			if (!IsCorrectSwipe()) {
				CreateSwipeBack();
			}
		}

		private void CreateSwipeBack () {
			Wait(new Actions.CSwipeBack(field, selected, targeted));
			CheckCompleteness();
		}

		private bool IsCorrectSwipe () {
			var matcher = new CMatcher(field).FindMatches();

			if (matcher.MatchesExists()) {
				foreach (List<CIcon> match in matcher.GetMatches()) {
					Wait(new Actions.CMatch(match));
				}
				CheckCompleteness();

				return true;
			} else {
				return false;
			}
		}

		public override int GetIndex() {
			return (int) EEvents.Swipe;
		}
	}
}