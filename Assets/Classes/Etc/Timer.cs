using System;
using UnityEngine;
using UnityEngine.UI;

namespace Etc {

	public class Timer : MonoBehaviour {

		public delegate void OnEnd();

		public event OnEnd onEnd;

		private float turnTime = 60f;
		private float value = 0f;

		public void SetTurnTime (float time) {
			turnTime = time;
			Reset();
		}

		public void Update () {
			if (IsEnded()) {
				return;
			}

			value -= Time.deltaTime;

			if (IsEnded()) {
				value = 0;
				onEnd();
			}

			gameObject.GetComponent<Text>().text = "" + GetCurrentTime();
		}

		public int GetCurrentTime () {
			return (int) Math.Ceiling(value);
		}

		public void Reset () {
			value = turnTime;
		}

		public bool IsEnded() {
			return value <= 0;
		}

	}

}