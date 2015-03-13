using Match.Gem.Animations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match.Gem {
	public class CBoltLauncher : IDieObserver {

		public List<Connection> connections;

		public struct Connection {
			public CIcon from;
			public CIcon to;

			public Vector3 GetPosition () {
				return Vector3.Lerp(from.transform.position, to.transform.position, 0.5f);
			}

			public bool IsVertical () {
				return Math.Abs(from.transform.position.x - to.transform.position.x) < 0.01f;
			}
		}

		public CBoltLauncher (List<Connection> connections) {
			this.connections = connections;
		}

		protected Vector3 GetPosition (Connection conn) {
			return Vector3.Lerp(conn.from.transform.position, conn.to.transform.position, 0.5f);
		}

		public void OnDieBolt() {
			foreach (Connection conn in connections) {
				CAnimations
					.Bolt( conn.GetPosition() )
					.Rotate( conn.IsVertical() )
					.SetColor( conn.from.color );
			}
		}

		public void OnDieEnd() {}
	}
}