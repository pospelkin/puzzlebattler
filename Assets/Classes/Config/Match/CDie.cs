using Match.Gem;
using Match.Gem.Animations;
using UnityEngine;

namespace Config.Match {
	public class CDie : MonoBehaviour {
		public GameObject Bolt;
		public GameObject Contour;
		public GameObject Explosion;
		public GameObject Highlight;
		public GameObject Spark;
		public GameObject Transform;

		public GameObject GetPrefab (EAnimations type) {
			switch (type) {
				case EAnimations.Bolt      : return Bolt;
				case EAnimations.Contour   : return Contour;
				case EAnimations.Explosion : return Explosion;
				case EAnimations.Highlight : return Highlight;
				case EAnimations.Spark     : return Spark;
				case EAnimations.Transform : return Transform;
			}

			return null;
		}
	}
}