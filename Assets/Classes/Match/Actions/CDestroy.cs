using Libraries;
using Libraries.ActionSystem;
using Match.Gem;
using System.Collections;
using UnityEngine;

namespace Match.Actions {
	public class CDestroy : CBase, Gem.IDieObserver {
		private Gem.CIcon target;
		private IDieObserver observer;

		public CDestroy (Gem.CIcon target, IDieObserver observer) {
			this.target  = target;
			this.observer = observer;
		}

		public override bool Validation() {
			return target && target.IsIdle();
		}

		public override void StartAction() {
			new Gem.CDie(target, this);
		}

		public void OnDieBolt () {
			if (this.observer != null) {
				this.observer.OnDieBolt();
			}
		}

		public void OnDieEnd () {
			ComplateAction();
		}

		public override int GetIndex() {
			return (int) EEvents.Destroy;
		}
	}

}