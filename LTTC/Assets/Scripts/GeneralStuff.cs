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
}
