using UnityEngine;
using UnityEngine.UI;

namespace Libraries {
	public class CProgressBar : MonoBehaviour {

		private Image image;
		private RectTransform rect;

		public void Awake () {
			rect   = gameObject.GetComponent<RectTransform>();
			image  = gameObject.GetComponent<Image>();
		}

		public void SetValue (float bar) {
			if (bar > 1) {
				bar = 1;
			} else if (bar < 0) {
				bar = 0;
			}

			rect.sizeDelta = GetSize(bar);
			image.color = GetColor(bar);
		}

		private Vector2 GetSize (float bar) {
			return new Vector2(GetWidth(bar), rect.sizeDelta.y);
		}

		private float GetWidth (float bar) {
			var border = image.sprite.border[0] + image.sprite.border[2];

			return border + bar * (image.sprite.rect.width - border);
		}

		private Color GetColor (float bar) {
			return new Color(
				bar >= 0.5f ? (1 - bar) * 2 : 1,
				bar <= 0.5f ?      bar  * 2 : 1,
				0
			);
		}

	}
}