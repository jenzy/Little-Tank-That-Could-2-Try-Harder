using UnityEngine;
using System.Collections;

public class EnemyProjectileCollision : MonoBehaviour {
	public Transform m_ExplosionImpact;

	private TerrainCraterMaker m_Terrain;
	
	void Start () {
		m_Terrain = Terrain.activeTerrain.GetComponent<TerrainCraterMaker>();
	}
	
	void OnCollisionEnter(Collision collision){
		foreach(ContactPoint p in collision.contacts){
			this.collider.enabled = false;

			Collider otherCollider = p.otherCollider;
			GameObject other = otherCollider.gameObject;
			
			Vector3 impactLocation = p.point;
			Debug.Log("Enemy collision with " + other.name);
			
			if(otherCollider.CompareTag(Tags.TERRAIN)){
				m_Terrain.MakeCrater(impactLocation);
			}
			else {
				GameObject destructible = null;
				if( other.CompareTag(Tags.PLAYER) ) 
					destructible = other;
				else {
					GameObject root = Tags.findParentWithTag(Tags.PLAYER, other.gameObject);
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
			break;
		}
	}
}
