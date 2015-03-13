using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Libraries {
	public class CInput {

		private bool isBlocked = false;
		private bool isClickStart = false;
		private IInputObserver currentObserver = null;
		private Dictionary<int, List<IInputObserver>> observers = new Dictionary<int, List<IInputObserver>>();

		public CInput () {
			Input.simulateMouseWithTouches = true;
		}

		public void registerObserver(IInputObserver aObserver, int aPriority) {
			if (!observers.ContainsKey(aPriority)) {
				observers.Add(aPriority, new List<IInputObserver>());
			}
			observers[aPriority].Add(aObserver);
		}

		public void unregisterObserver(int aPriority, IInputObserver aObserver) {
			observers[aPriority].Remove(aObserver);
		}

		public void unregisterObserver(IInputObserver aObserver) {
			foreach (KeyValuePair<int, List<IInputObserver>> pair in observers) {
				unregisterObserver(pair.Key, aObserver);
			}
		}


		public void Check() {
			if (Input.touchCount == 1) {
				HandleTouch(Input.GetTouch(0));
			} else if ( Input.GetMouseButton(0) ) {
				HandleMouse((Vector2) Input.mousePosition);
			} else if (isClickStart) {
				Reset();
			}
		}

		public void Block() {
			isBlocked = true;
		}

		private void Reset() {
			isClickStart = false;
			isBlocked = false;
			currentObserver = null;
		}

		private void HandleMouse(Vector2 mousePosition) {
			if (isClickStart) {
				OnMove(mousePosition);
			} else {
				isClickStart = true;
				OnBegin(mousePosition);
			}
		}

		private void HandleTouch(Touch aTouch) {
			switch (aTouch.phase) {
				case TouchPhase.Began: OnBegin(aTouch.position); break;
				case TouchPhase.Moved: OnMove (aTouch.position); break;
				case TouchPhase.Ended: OnEnd  (aTouch.position); break;
			}
		}

		private void OnBegin(Vector2 aPosition) {
			foreach (KeyValuePair<int, List<IInputObserver>> pair in observers) {
				for (int i = 0; i < pair.Value.Count; i++) {
					if (pair.Value[i].OnInputBegin(aPosition)) {
						currentObserver = pair.Value[i];
						return;
					}
				}
			}
		}

		private void OnMove(Vector2 aPosition) {
			if (!isBlocked && currentObserver != null) {
				currentObserver.OnInputMove(aPosition);
			}
		}

		private void OnEnd(Vector2 aPosition) {
			if (currentObserver != null) {
				currentObserver.OnInputEnd(aPosition);
			}

			Reset();
		}

	}
}