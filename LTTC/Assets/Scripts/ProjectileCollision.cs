using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour {
	public Transform mExplosion;

	private Transform mTransform;
	private CraterMaker mTerrain;

	void Start () {
		mTransform = transform;
		mTerrain = GameObject.FindGameObjectWithTag(Tags.Terrain).GetComponent<CraterMaker>();
	}


	void OnTriggerEnter(Collider other) {
		Vector3 impactLocation = mTransform.position;

		Instantiate(mExplosion, impactLocation, Quaternion.identity);
		mTerrain.MakeCrater(impactLocation);
		//mTerrain.AddCrater(impactLocation);

		Destroy(this.gameObject);
	}
}
