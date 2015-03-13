using Match.Gem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Libraries.ActionSystem;

namespace Match.Actions {

	public class CAutoMatch : CCreating {
		private List<List<CIcon>> mAutoMatches;

		public CAutoMatch (List<List<CIcon>> aMatches) {
			mAutoMatches = aMatches;
		}

		public override bool Validation() {
			return mAutoMatches != null && mAutoMatches.Count > 0;
		}

		public override void StartAction() {
			foreach (List<CIcon> match in mAutoMatches) {
				Wait(new CMatch(match));
			}
			CheckCompleteness();
		}

		public override int GetIndex() {
			return (int) EEvents.AutoMatch;
		}

	}

}

