using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour {
	public Transform m_Explosion;

	private Transform m_Transform;
	private TerrainCraterMaker m_Terrain;

	void Start () {
		m_Transform = transform;
		m_Terrain = GameObject.FindGameObjectWithTag(Tags.TERRAIN).GetComponent<TerrainCraterMaker>();
	}


	void OnTriggerEnter(Collider other) {
		Vector3 impactLocation = m_Transform.position;

		Instantiate(m_Explosion, impactLocation, Quaternion.identity);
		m_Terrain.MakeCrater(impactLocation);

		Destroy(this.gameObject);
	}
}
