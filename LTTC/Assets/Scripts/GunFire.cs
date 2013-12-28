using UnityEngine;
using System.Collections;

public class GunFire : MonoBehaviour {
	public GameObject m_Projectile;
	public GameObject m_MuzzleFlash;

	public float mInitialForce = 10000f;

	void Start () {
	
	}
	
	void Update () {
		if( Input.GetButtonUp(In.BUTTON_FIRE) ){
			Vector3 startPosition = GameObject.FindGameObjectWithTag(Tags.PROJECTILE_START).transform.position;
			GameObject projectile = Instantiate(m_Projectile, startPosition, Quaternion.identity) as GameObject;
			Vector3 force = transform.rotation *  Vector3.forward * mInitialForce;
			projectile.rigidbody.AddForce(force);

			Instantiate(m_MuzzleFlash, startPosition, transform.rotation);
		}
	}
}
