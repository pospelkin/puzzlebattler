using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Libraries.ActionSystem {
	public abstract class CBase : IAction {

		protected IAnticipant mAnticipant;
		protected CManager mActionManager;

		public void SetAnticipant(IAnticipant anticipant) {
			this.mAnticipant = anticipant;
		}

		public void SetActionManager (CManager aManager) {
			this.mActionManager = aManager;
		}

		public abstract int GetIndex();
		public abstract bool Validation ();
		public abstract void StartAction ();

		public void ComplateAction() {
			mActionManager.OnActionEnd(this);

			if (mAnticipant != null) {
				mAnticipant.OnActionEnd(this);
			}
		}
	}

}