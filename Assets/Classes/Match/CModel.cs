using Libraries.ActionSystem;
using Match.Actions;
using Model;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Match {
	public class CModel {

		CField field;

		public CModel (CField field) {
			CGame.Model.ActivateFirst();

			this.field = field;

			var actionEvents = field.actions.events;

			actionEvents.OnFinish(OnActionsFinished);
			actionEvents.OnBreak((int) EEvents.FreeFall  , OnFreeFallBreak  );
			actionEvents.OnEnd  ((int) EEvents.FreeFall  , OnFreeFallEnd    );
			actionEvents.OnEnd  ((int) EEvents.Match     , OnMatchEnd       );
			actionEvents.OnBegin((int) EEvents.Swipe     , OnSwipeBegin     );
			actionEvents.OnBegin((int) EEvents.SwipeBack , OnSwipeBackBegin );
		}

		public void OnFreeFallBreak (IAction action) {
			CheckActive();
			field.ai.Play();
		}

		public void OnFreeFallEnd(IAction action) {
			var matcher = new CMatcher(field).FindMatches();

			field.actions.Add(new CAutoMatch(matcher.GetMatches()));
		}

		public void OnActionsFinished () {
			field.actions.Add(new CFreeFall(field));
		}

		public void OnMatchEnd (IAction action) {
			var count = (action as Actions.CMatch).GetCountMatchIcon();

			CGame.Model.GetActivePlayer().AddScore(CalculateScore(count));
		}

		public void OnSwipeBegin (IAction action) {
			CGame.Model.GetActivePlayer().UseMatch();
		}

		public void OnSwipeBackBegin (IAction action) {
			CGame.Model.GetActivePlayer().RestoreMatch();
		}

		protected void CheckActive () {
			if (CGame.Model.GetActivePlayer().IsStepFinished()) {
				CGame.Model.SwitchPlayer();
			}
		}

		private int CalculateScore (int count) {
			if (count < 4) {
				return 3;
			} else {
				return 3 + (count - 3) * count / 2;
			}
		}
	}
}