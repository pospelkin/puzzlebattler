using Libraries.ActionSystem;
using Match.Gem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match.Actions {
	public class CMatch : CCreating {
		private List<CIcon> icons;

		public CMatch (List<CIcon> icons) {
			this.icons = icons;
		}

		public EColor GetMatchIconType() {
			return icons[0].color;
		}

		public override bool Validation() {
			if (icons == null) {
				return false;
			}

			foreach (CIcon icon in icons) {
				if (icon == null || !icon.IsIdle()) {
					return false;
				}
			}

			return true;
		}

		public int GetCountMatchIcon() {
			return icons.Count;
		}

		protected CBoltLauncher GetBoltLauncher() {
			List<CBoltLauncher.Connection> connections = new List<CBoltLauncher.Connection>();

			foreach (CIcon from in icons) {
				bool ignore = true;

				foreach (CIcon to in icons) {
					if (!ignore && to.IsNeighbour(from)) {
						connections.Add(
							new CBoltLauncher.Connection(){
								from = from,
								to   = to
							}
						);
					}

					if (from == to) {
						ignore = false;
					}
				}
			}

			return connections.Count > 0 ? new CBoltLauncher(connections) : null;
		}

		public override void StartAction() {
			bool isFirst = true;

			foreach (CIcon icon in icons) {
				if (isFirst) {
					Wait(new CDestroy(icon, GetBoltLauncher()));
					isFirst = false;
				} else {
					Wait(new CDestroy(icon, null));
				}
			}
			CheckCompleteness();
		}


		public override int GetIndex() {
			return (int) EEvents.Match;
		}
	}
}