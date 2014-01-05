using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour {
	public Transform m_ExplosionImpact;

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
			Debug.Log("Collision with " + other.name);
			
			if(otherCollider.CompareTag(Tags.TERRAIN)){
				m_Terrain.MakeCrater(impactLocation);
			} 
			else {
				GameObject destructible = null;
				if( other.CompareTag(Tags.DESTRUCTIBLE) ) 
					destructible = other;
				else {
					GameObject root = Tags.findParentWithTag(Tags.DESTRUCTIBLE, other.gameObject);
					if(root != null)
						destructible = root;
				}

				if(destructible != null){
					Destroyer dest = destructible.GetComponent<Destroyer>();
					dest.Hit();
				}
			}

			Instantiate(m_ExplosionImpact, impactLocation, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
	/*
	private GameObject findParentWithTag(string tagToFind, GameObject startingObject) {
		Transform par = startingObject.transform.parent;
		while (par != null) { 
			if (par.CompareTag(tagToFind)) {
				return par.gameObject as GameObject;
			}
			par = par.parent;
		}
		return null;
	}*/

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
