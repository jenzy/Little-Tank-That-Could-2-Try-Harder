using UnityEngine;
using System.Collections;

public class Grounder : MonoBehaviour {
	public bool IsGrounded { get; private set;}

	public void OnCollisionEnter(Collision collision){
		Collider other = collision.collider;
		if(other.tag == Tags.Terrain){
			IsGrounded = true;
			//Debug.Log("Terrain collision enter");
		}
	}

	public void OnCollisionExit(Collision collision){
		Collider other = collision.collider;
		if(other.tag == Tags.Terrain){
			IsGrounded = false;
			//Debug.Log("Terrain collision exit");
		}
	}

	void Start () {
		IsGrounded = false;
	}
}
