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
			else if(otherCollider.CompareTag(Tags.TREE)){
				Destroy(other.gameObject);
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

}
