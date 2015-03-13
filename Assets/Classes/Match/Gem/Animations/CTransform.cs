using UnityEngine;

namespace Match.Gem.Animations {

	public class CTransform : CAnimation {
		override public EAnimations GetType () {
			return EAnimations.Transform;
		}

		public void OnTransformBoltStart () {
			if (listener != null) {
				listener.OnTransformBoltStart(this);
			}
		}

		public void OnIconDied () {
			if (listener != null) {
				listener.OnIconDied(this);
			}
		}

		public void OnTransformExplosionStart () {
			if (listener != null) {
				listener.OnTransformExplosionStart(this);
			}
		}

	}

}