using UnityEngine;
using System.Collections;

public class GUI_Health : MonoBehaviour {

	private Destroyer destroyer;

	// Use this for initialization
	void Start () {
		destroyer = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponentInChildren<Destroyer>();
	}
	
	// Update is called once per frame
	void Update () {
		this.guiText.text = destroyer.HealthLeft.ToString();
	}
}
