using UnityEngine;
using System.Collections;

public class GunFire : MonoBehaviour {
	public GameObject mProjectile;
	public float mInitialForce = 10000f;

	void Start () {
	
	}
	
	void Update () {
		if( Input.GetButtonUp(In.BUTTON_FIRE_1) ){
			Vector3 startPosition = GameObject.FindGameObjectWithTag(Tags.ProjectileStart).transform.position;
			GameObject projectile = Instantiate(mProjectile, startPosition, Quaternion.identity) as GameObject;
			Vector3 force = transform.rotation *  Vector3.forward * mInitialForce;
			projectile.rigidbody.AddForce(force);
		}
	}
}
