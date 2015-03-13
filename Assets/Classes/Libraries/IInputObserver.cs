using UnityEngine;

namespace Libraries {

	public interface IInputObserver {
		bool OnInputBegin(Vector2 position);
		void OnInputMove (Vector2 position);
		void OnInputEnd  (Vector2 position);
	}

}