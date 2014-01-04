using UnityEngine;
using System.Collections;

public class EnemyProjectileCollision : MonoBehaviour {
	public Transform m_Explosion;
	
	//private Transform m_Transform;
	private TerrainCraterMaker m_Terrain;
	
	void Start () {
		m_Terrain = Terrain.activeTerrain.GetComponent<TerrainCraterMaker>();
	}
	
	void OnCollisionEnter(Collision collision){
		foreach(ContactPoint p in collision.contacts){
			Collider otherCollider = p.otherCollider;
			GameObject other = otherCollider.gameObject;
			
			Vector3 impactLocation = p.point;
			Debug.Log("Enemy collision with tag " + other.tag);
			
			if(otherCollider.CompareTag(Tags.TERRAIN)){
				Instantiate(m_Explosion, impactLocation, Quaternion.identity);
				m_Terrain.MakeCrater(impactLocation);
			} 
			else if(other.CompareTag(Tags.PLAYER)){
				//Destroyer dest = other.GetComponent<Destroyer>();
				//dest.Destroy();
				Debug.Log("player hit");
			}
			else {
				GameObject root = Tags.findParentWithTag(Tags.PLAYER, other.gameObject);
				if(root != null){
					//Destroyer dest = root.GetComponent<Destroyer>();
					//dest.Destroy();
					Debug.Log("player hit");
				}
			}
			
			Destroy(this.gameObject);
		}
	}
}
