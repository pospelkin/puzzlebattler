using Etc;
using UnityEngine;

namespace Match.Gem.Animations {

	abstract public class CAnimation : MonoBehaviour {

		protected CAnimations animations;
		protected IListener listener;

		public void SetAnimations (CAnimations animations) {
			this.animations = animations;
		}

		public void OnAnimationFinish () {
			animations.OnAnimationFinish(this);

			if (listener != null) {
				listener.OnAnimationFinish(this);
			}
		}

		public CAnimation SetColor (EColor color) {
			GetComponent<AnimatorColorState>().Set((int) color);
			return this;
		}

		public CAnimation SetListener (IListener listener) {
			this.listener = listener;
			return this;
		}

		abstract public EAnimations GetType ();

	}

}