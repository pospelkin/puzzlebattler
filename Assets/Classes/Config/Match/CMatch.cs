using UnityEngine;

namespace Config.Match {
	public class CMatch : MonoBehaviour {
		public CField  field;
		public CPlayer player;
		public CPlayer opponent;
		public CGems   gems;
		public CDie    die;

		public bool isPlayerFirst = true;
	}
}