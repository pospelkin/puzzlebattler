using System;
using System.Collections;
using UnityEngine;

namespace Etc {
	public class AnimatorColorState : MonoBehaviour {

		protected const string KEY = "Color";

		protected Animator animator;
		protected int min = 0;
		protected int max = 4;

		public int Value { get { return Get(); } private set {} }

		public void Awake () {
			animator = GetComponent<Animator>();
		}

		public void Set (int colorIndex) {
			if (colorIndex > max) {
				throw new ArgumentOutOfRangeException("ColorIndex is bigger than max (" + colorIndex + " > " + max + ")");
			}
			if (colorIndex < min) {
				throw new ArgumentOutOfRangeException("ColorIndex is smaller than min (" + colorIndex + " < " + min + ")");
			}

			animator.SetInteger(KEY, colorIndex);
		}

		public int Get () {
			return animator.GetInteger(KEY);
		}

	}
}