using Match.Gem.Animations;
using System.Collections;
using UnityEngine;

namespace Match.Gem {
	public class CDie : Animations.IListener {

		protected CIcon icon;
		protected IDieObserver observer;

		public CDie (CIcon icon, IDieObserver observer) {
			this.icon = icon;
			this.observer = observer;

			CAnimations
				.Highlight( icon.transform.position )
				.SetColor( icon.color )
				.SetListener( this );
		}

		protected void StartDying () {
			CAnimations
				.Spark( icon.transform.position )
				.SetColor( icon.color )
				.SetListener( this );

			CAnimations
				.Transform( icon.transform.position )
				.SetColor( icon.color )
				.SetListener( this );

			icon.SetState(EState.Hidden);
		}

		public void OnTransformBoltStart(CAnimation animation) {
			CAnimations
				.Contour( icon.transform.position )
				.SetColor( icon.color )
				.SetListener( this );
			observer.OnDieBolt();
		}

		public void OnTransformExplosionStart(CAnimation animation) {
			CAnimations
				.Explosion( icon.transform.position )
				.SetColor( icon.color );
		}

		public void OnAnimationFinish(CAnimation animation) {
			if (animation.GetType() == EAnimations.Highlight) {
				StartDying();
			}
		}

		public void OnIconDied (CAnimation animation) {
			icon.SetState(EState.Death);
			observer.OnDieEnd();
		}


	}
}