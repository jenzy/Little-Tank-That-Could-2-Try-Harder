using UnityEngine;
using System.Collections;

public class InWater : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		GameObject root = Tags.findParentWithTag(Tags.PLAYER, other.gameObject);
		if(other.CompareTag(Tags.PLAYER) || root!=null)
			GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLER).GetComponent<GeneralStuff>().Lose(3);
	}
}
