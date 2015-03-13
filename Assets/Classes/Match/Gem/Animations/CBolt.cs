using UnityEngine;

namespace Match.Gem.Animations {

	public class CBolt : CAnimation {
		public CBolt Rotate (bool isVertical) {
			gameObject.transform.rotation = Quaternion.Euler(0,0,GetAngle(isVertical));

			return this;
		}

		protected int GetAngle (bool isVertical) {
			var random = (Random.Range(0f, 1f) > 0.5f) ? 180 : 0;

			return random + (isVertical ? 90 : 0);
		}

		override public EAnimations GetType () {
			return EAnimations.Bolt;
		}
	}

}