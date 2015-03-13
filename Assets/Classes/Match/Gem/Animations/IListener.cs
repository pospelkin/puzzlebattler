namespace Match.Gem.Animations {
	public interface IListener {
		void OnIconDied(CAnimation animation);
		void OnAnimationFinish(CAnimation animation);
		void OnTransformBoltStart(CAnimation animation);
		void OnTransformExplosionStart(CAnimation animation);
	}
}