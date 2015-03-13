using Libraries;
using Match.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Libraries.ActionSystem {
	public class CManager : IAnticipant {
		private List<IAction> activeActions = new List<IAction>();

		public CEvents events = new CEvents();

		public bool Add(IAction action) {
			action.SetActionManager(this);

			if (action.Validation()) {
				activeActions.Add(action);
				events.Invoke(EActionState.Begin, action);
				action.StartAction();
				return true;
			} else {
				events.Invoke(EActionState.Break, action);
				return false;
			}
		}

		public void OnActionEnd (IAction action) {
			activeActions.Remove(action);
			events.Invoke(EActionState.End, action);

			if (!HasActions()) {
				events.InvokeFinish();
			}
		}

		public bool HasActions() {
			return (activeActions.Count > 0);
		}
	}
}