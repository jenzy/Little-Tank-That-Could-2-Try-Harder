using UnityEngine;
using System.Collections;

public class EndMenu : MonoBehaviour {
	public GUISkin guiSkin;


	public GameObject m_MainText;
	public GameObject m_SubText;
	public GameObject m_LoseExplosion;
	public GameObject m_Ognjemet;
	public GameObject m_Camera;

	public enum EndMenuState {WIN, LOSE}
	public static EndMenuState EndState {get; set;}

	// Use this for initialization
	void Start () {
		Screen.showCursor = true;
		switch(EndState){
		case EndMenuState.WIN:
			m_Camera.transform.Rotate(-17, 0,0);
			m_MainText.guiText.text = "CONGRATULATIONS!";
			m_SubText.guiText.text = "You have successfully foiled the enemy's plan!";
			m_Ognjemet.particleSystem.Play();

			break;
		case EndMenuState.LOSE:
			m_MainText.guiText.text = "GAME OVER!";
			m_SubText.guiText.text = "You failed, the enemy has defeated us!";
			Invoke("LoseExplosion", 1);
			Invoke("CameraUp", 6);

			RenderSettings.skybox.color = new Color(0,0,0,0);

			break;
		}
	}

	void Update(){
		if(Input.GetButtonUp(In.BUTTON_EXIT)){
			Application.LoadLevel(Level.MENU);
		}
	}
	
	void OnGUI(){
		GUI.skin = guiSkin;
		
		if( GUI.Button(new Rect(Screen.width/2-75,Screen.height-50,150, 40), "Back") ){
			Application.LoadLevel(Level.MENU);
		}
	}

	private void CameraUp(){
		m_Camera.animation.Play();
	}

	private void LoseExplosion(){
		m_LoseExplosion.GetComponent<Detonator>().Explode();
	}
}
