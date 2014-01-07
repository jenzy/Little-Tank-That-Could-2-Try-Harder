using UnityEngine;
using System.Collections;

public class Grounder : MonoBehaviour {
	public bool IsGrounded { get; private set;}

	private RaycastHit hit;
	private Transform m_Transform;

	public void OnCollisionEnter(Collision collision){
		Collider other = collision.collider;
		if(other.tag == Tags.TERRAIN){
			IsGrounded = true;
			//Debug.Log("Terrain collision enter");
		}
	}

	public void OnCollisionExit(Collision collision){
		Collider other = collision.collider;
		if(other.tag == Tags.TERRAIN){
			IsGrounded = false;
			//Debug.Log("Terrain collision exit");
		}
	}

	void Update(){
		if(!IsGrounded){
			if(Physics.Raycast(m_Transform.position, Vector3.down, out hit, 1.4f)){
				IsGrounded = hit.collider.CompareTag(Tags.TERRAIN);
			} else {
				IsGrounded = false;
			}
		}
	}

	void Start () {
		IsGrounded = false;
		m_Transform = transform;
	}
}
