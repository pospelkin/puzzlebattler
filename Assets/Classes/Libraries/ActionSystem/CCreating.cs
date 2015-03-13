using System.Collections.Generic;

namespace Libraries.ActionSystem {
	public abstract class CCreating : CBase, IAnticipant {

		protected List<IAction> mWaiting = new List<IAction>();

		public void OnActionEnd (IAction action) {
			mWaiting.Remove(action);
			CheckCompleteness();
		}

		protected void CheckCompleteness () {
			if (mWaiting.Count == 0) {
				ComplateAction();
			}
		}

		protected void Wait (IAction action) {
			action.SetAnticipant(this);

			if (mActionManager.Add(action)) {
				mWaiting.Add(action);
			}
		}

	}
}