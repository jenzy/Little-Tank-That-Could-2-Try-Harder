using UnityEngine;
using System.Collections;

public class GUI_Timer : MonoBehaviour {
	private GameTimer timer;


	void Start () {
		timer = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLER).GetComponent<GameTimer>();
	}
	
	void Update () {
		int secondsLeft = timer.SecondsLeft;
		this.guiText.text = timer.MinutesLeft + ":" + (secondsLeft < 10 ? "0" : "") + secondsLeft;
	}
}
