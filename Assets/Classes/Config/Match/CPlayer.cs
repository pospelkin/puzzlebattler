using Match.Gem;
using UnityEngine;

namespace Config.Match {
	public class CPlayer : MonoBehaviour {
		public int health = 1000;
		public int matchesPerTurn = 3;

		public int redPower;
		public int bluePower;
		public int greenPower;
		public int purplePower;
		public int yellowPower;

		public int GetPower (EColor type) {
			switch (type) {
				case EColor.Red   : return redPower;
				case EColor.Blue  : return bluePower;
				case EColor.Green : return greenPower;
				case EColor.Purple: return purplePower;
				case EColor.Yellow: return yellowPower;
			}

			return 0;
		}
	}
}