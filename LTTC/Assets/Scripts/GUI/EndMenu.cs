using UnityEngine;
using System.Collections;

public class EndMenu : MonoBehaviour {
	public enum EndMenuState {WIN, LOSE}
	public static EndMenuState EndState {get; set;}

	// Use this for initialization
	void Start () {
		switch(EndState){
		case EndMenuState.WIN:
			this.guiText.text = "WIN";
			break;
		case EndMenuState.LOSE:
			this.guiText.text = "LOSE";
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
