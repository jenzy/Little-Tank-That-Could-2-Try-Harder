using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {
	public float m_TimeInMinutes;

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
	}
	
	void Update () {
		if(time > 0){
			time -= Time.deltaTime;
		}
	}
}
