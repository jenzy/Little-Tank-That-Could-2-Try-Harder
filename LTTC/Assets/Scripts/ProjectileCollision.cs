using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour {
	public Transform m_Explosion;

	//private Transform m_Transform;
	private TerrainCraterMaker m_Terrain;

	void Start () {
		//m_Transform = transform;
		m_Terrain = Terrain.activeTerrain.GetComponent<TerrainCraterMaker>();
	}

	void OnCollisionEnter(Collision collision){
		foreach(ContactPoint p in collision.contacts){
			Collider otherCollider = p.otherCollider;
			GameObject other = otherCollider.gameObject;

			Vector3 impactLocation = p.point;
			Debug.Log("Collision with tag " + other.tag);
			
			if(otherCollider.CompareTag(Tags.TERRAIN)){
				Instantiate(m_Explosion, impactLocation, Quaternion.identity);
				m_Terrain.MakeCrater(impactLocation);
			} 
			else {
				Destroy(other);
			}

			Destroy(this.gameObject);
		}
	}

	/*void OnTriggerEnter(Collider other) {
		Debug.Log(other.tag);
		Vector3 impactLocation = m_Transform.position;


		Instantiate(m_Explosion, impactLocation, Quaternion.identity);

		if(other.CompareTag(Tags.TERRAIN)){
			m_Terrain.MakeCrater(impactLocation);
		}
		Destroy(this.gameObject);
	}*/
}
