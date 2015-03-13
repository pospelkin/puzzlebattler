using Model;
using System.Collections.Generic;
using UnityEngine;

namespace Etc {
	public class SwipesCounter : MonoBehaviour {

		public List<GameObject> elements;

		public void Watch (CPlayer player) {
			player.onMatchesChange += OnMatchesChange;
			OnMatchesChange(player);
		}

		public void OnMatchesChange (CPlayer player) {
			int count = player.GetRealtimeMatches();

			foreach (GameObject gameObject in elements) {
				gameObject.SetActive( --count >= 0 );
			}
		}

	}
}