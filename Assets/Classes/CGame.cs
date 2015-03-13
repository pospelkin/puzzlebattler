using Config;
using Etc;
using Libraries;
using Model;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CGame : MonoBehaviour {

	public Config.CConfig config;
	public Model.CModel model;
	public Match.CField match;
	public CInput input;
	public Etc.Timer timer;

	public CProgressBar playerProgress;
	public CProgressBar opponentProgress;

	public Text playerText;
	public Text opponentText;

	public SwipesCounter playerSwipesCounter;
	public SwipesCounter opponentSwipesCounter;

	public static CConfig Config {
		get { return Instance.config; }
	}

	public static CModel Model {
		get { return Instance.model; }
	}

	public static CInput Input {
		get { return Instance.input; }
	}

	public static CGame Instance { get; private set; }

	public void Awake() {
		Instance = this;
		model = new Model.CModel(this);
		input = new Libraries.CInput();

		  playerSwipesCounter.Watch(model.player);
		opponentSwipesCounter.Watch(model.opponent);

		timer.SetTurnTime(Config.turnTime);
		timer.onEnd += OnTimerEnd;
	}

	public void Start() {
		model.player  .onScoreChange += OnScoreChange;
		model.opponent.onScoreChange += OnScoreChange;

		Recount();
	}

	public void OnScoreChange(CPlayer player, int scoreChange) {
		Recount();
	}

	public void OnTimerEnd () {
		if (!match.actions.HasActions()) {
			Model.SwitchPlayer();
			match.ai.Play();
		}
	}

	public void Recount() {
		Count(model.player, Config.match.opponent.health, opponentProgress);
		Count(model.opponent, Config.match.player.health, playerProgress);

		  playerText.text = "" + CGame.Instance.model.player.score;
		opponentText.text = "" + CGame.Instance.model.opponent.score;
	}

	public void Update () {
		input.Check();
	}

	protected void Count(CPlayer player, int maxHealth, CProgressBar progress) {
		progress.SetValue((maxHealth - player.score) / (float) maxHealth);
	}
}