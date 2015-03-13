using Libraries;
using Match.Gem;
using Match.Gem.Animations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match {

	public class CField : MonoBehaviour {

		public Libraries.ActionSystem.CManager actions;
		public CAi ai;

		public void Start() {
			actions = new Libraries.ActionSystem.CManager();

			ai = new CAi(this);

			new CInput(this);
			new CModel(this);

			Gem.Animations.CAnimations.SetParent(this.transform);

			CreateIcons();
		}

		public int height {
			get { return CGame.Config.match.field.rows; }
		}

		public int width {
			get { return CGame.Config.match.field.columns; }
		}

		public CIcon [,] icons;

		protected void CreateIcons () {
			icons = new CIcon[height, width];

			for (int row = 0; row < height; row++) {
				for (int col = 0; col < width; col++) {
					RegisterIcon(new CCell(row, col));
				}
			}
		}

		public void SetIconAt(CCell position, CIcon icon) {
			icons[position.row, position.col] = icon;
			icon.cell.Set(position);
		}

		public CIcon GetIconAt(CCell position) {
			return icons[position.row, position.col];
		}

		public Vector3 GetIconCenterByCell(CCell cell) {
			var config = CGame.Config.match.field;

			return new Vector3(
				config.from.x + cell.col * config.offset.x,
				config.from.y + cell.row * config.offset.y,
				this.transform.position.z
			);
		}

		public CIcon GetIconByPoint(Vector2 position) {
			foreach (CIcon icon in icons) {
				if (icon.HitTest(position)) {
					return icon;
				}
			}

			return null;
		}

		private CIcon RegisterIcon(CCell position) {
			GameObject gameObject = Instantiate(CGame.Config.match.gems.prefab) as GameObject;
			gameObject.transform.SetParent(this.transform);
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.position = GetIconCenterByCell(position);

			CIcon icon = gameObject.GetComponent<CIcon>();

			SetIconAt(position, icon);

			do {
				icon.SetRandomColor();
			} while (CanCreateMatch(icon));

			return icon;
		}

		private bool CanCreateMatch (CIcon icon) {
			CCell cell = icon.cell;

			if (cell.col >= 2
				&& GetIconAt(new CCell(cell.row, cell.col-1)).IsMatchable(icon)
				&& GetIconAt(new CCell(cell.row, cell.col-2)).IsMatchable(icon)
			) {
				return true;
			}

			if (cell.row >= 2
				&& GetIconAt(new CCell(cell.row-1, cell.col)).IsMatchable(icon)
				&& GetIconAt(new CCell(cell.row-2, cell.col)).IsMatchable(icon)
			) {
				return true;
			}

			return false;
		}

	}
}