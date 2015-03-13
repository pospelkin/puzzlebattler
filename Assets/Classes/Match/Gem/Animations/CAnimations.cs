using UnityEngine;

namespace Match.Gem.Animations {
	public class CAnimations : Object {

		public static void SetParent (Transform parent) { Instance.SetDefaultParent(parent); }

		public static CBolt      Bolt     (Vector3 position) { return (CBolt)      Req( EAnimations.Bolt     , position ); }
		public static CContour   Contour  (Vector3 position) { return (CContour)   Req( EAnimations.Contour  , position ); }
		public static CExplosion Explosion(Vector3 position) { return (CExplosion) Req( EAnimations.Explosion, position ); }
		public static CHighlight Highlight(Vector3 position) { return (CHighlight) Req( EAnimations.Highlight, position ); }
		public static CSpark     Spark    (Vector3 position) { return (CSpark)     Req( EAnimations.Spark    , position ); }
		public static CTransform Transform(Vector3 position) { return (CTransform) Req( EAnimations.Transform, position ); }

		static readonly CAnimations instance = new CAnimations();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static CAnimations() {}
		private CAnimations() { }

		public static CAnimations Instance{
			get { return instance; }
		}

		protected Transform transformParent;

		protected static CAnimation Req (EAnimations type, Vector3 position) {
			return Instance.Require(type, position);
		}

		public void OnAnimationFinish (CAnimation animation) {
			MonoBehaviour.Destroy(animation.gameObject);
		}

		// todo: use objects pool, not recreate
		public CAnimation Require (EAnimations type, Vector3 position) {
			var anim = Create(type, position);
			anim.SetListener(null);
			return anim;
		}

		public void SetDefaultParent (Transform parent) {
			this.transformParent = parent;
		}

		public CAnimation Create (EAnimations type, Vector3 position) {
			var prefab = CGame.Config.match.die.GetPrefab(type);

			GameObject gameObject = Instantiate(prefab) as GameObject;
			gameObject.transform.SetParent(this.transformParent);
			gameObject.transform.localScale = new Vector3(100, 100, 1);
			gameObject.transform.position = position;

			var anim = gameObject.GetComponent<CAnimation>();
			anim.SetAnimations(this);

			return anim;
		}

	}
}