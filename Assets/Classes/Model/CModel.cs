namespace Model {
	public class CModel {

		private CGame game;
		public CPlayer player;
		public CPlayer opponent;

		public CModel(CGame game) {
			this.game      = game;
			this.player    = new CPlayer(game);
			this.opponent  = new CPlayer(game);
		}

		public CPlayer GetActivePlayer () {
			return player.isActive ? player : opponent;
		}

		public void SwitchPlayer () {
			game.timer.Reset();

			if (player.isActive) {
				ActivatePlayer(opponent);
			} else {
				ActivatePlayer(player);
			}
		}

		public void ActivateFirst () {
			if (CGame.Config.match.isPlayerFirst) {
				ActivatePlayer(player);
			} else  {
				ActivatePlayer(opponent);
			}
		}

		public void ActivatePlayer(CPlayer active) {
			if (active == player) {
				player.isActive = true;
				player.SetMatches(CGame.Config.match.player.matchesPerTurn);
				opponent.isActive = false;
			} else {
				opponent.isActive = true;
				opponent.SetMatches(CGame.Config.match.opponent.matchesPerTurn);
				player.isActive = false;
			}
		}

	}
}