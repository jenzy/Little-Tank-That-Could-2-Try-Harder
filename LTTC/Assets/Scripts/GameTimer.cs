using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {
	public float m_TimeInMinutes;
	private GeneralStuff m_Manager;
	
	public int MinutesLeft {
		get{
			if(time <= 0) return 0;
			else return (int)time / 60;
		}
	}
	public int SecondsLeft {
		get{
			if(time <= 0) return 0;
			else return (int)time % 60;
		}
	}

	private float time;

	void Start () {
		time = m_TimeInMinutes * 60;
		m_Manager = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLER).GetComponent<GeneralStuff>();
		
	}
	
	void Update () {
		if(time > 0){
			time -= Time.deltaTime;
			if(time <= 0)
				m_Manager.Lose(3);
		}
	}

	private void OnLose(){
		EndMenu.EndState = EndMenu.EndMenuState.LOSE;
		Application.LoadLevel(Level.END_MENU);
	}
}
