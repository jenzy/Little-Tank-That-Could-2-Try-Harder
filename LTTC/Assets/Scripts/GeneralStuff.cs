using UnityEngine;
using System.Collections;

public class GeneralStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp(In.BUTTON_EXIT)){
			Application.LoadLevel(Level.MENU);
		}
	}

	public void Lose(int delay){
		Invoke("OnLose", delay);
	}

	public void Win(){
		Invoke("OnWin", 3);
	}

	private void OnLose(){
		EndMenu.EndState = EndMenu.EndMenuState.LOSE;
		Application.LoadLevel(Level.END_MENU);
	}

	private void OnWin(){
		EndMenu.EndState = EndMenu.EndMenuState.WIN;
		Application.LoadLevel(Level.END_MENU);
	}

}
