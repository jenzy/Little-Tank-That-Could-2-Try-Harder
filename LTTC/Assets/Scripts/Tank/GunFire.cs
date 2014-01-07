using UnityEngine;
using System.Collections;

public class GunFire : MonoBehaviour {
	public GameObject m_Projectile;
	public GameObject m_MuzzleFlash;

	public Gun TankGun{get; private set;}

	//private GameObject m_player;

	public float mInitialForce = 10000f;

	void Start () {
		//m_player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
		int ammo = MainMenu.ChosenDifficulty == MainMenu.Difficulty.EASY ? 50 : 25;
		TankGun = new Gun(ammo, 5);
	}
	
	void Update () {
		TankGun.update();

		if( Input.GetButtonUp(In.BUTTON_FIRE) ){
			if(TankGun.Fire()){

				Vector3 startPosition = GameObject.FindGameObjectWithTag(Tags.PROJECTILE_START).transform.position;
				GameObject projectile = Instantiate(m_Projectile, startPosition, Quaternion.identity) as GameObject;
				Vector3 force = transform.rotation *  Vector3.forward * mInitialForce;
				projectile.rigidbody.AddForce(force);

				//m_player.rigidbody.AddForce(-20 * force);
				this.animation.Play();

				Object o = Instantiate(m_MuzzleFlash, startPosition, transform.rotation);
				Destroy(o, 10);
			}
		}

	}

}

public class Gun {
	public bool OneInTheChamber{get; private set;}
	public int Ammo {get; private set;}
	private float reloadTime;

	private double lastFire;

	public Gun(int ammo, float reloadTime){
		OneInTheChamber = true;
		Ammo = ammo;
		this.reloadTime = reloadTime;
	}

	public void update(){
		if(OneInTheChamber) return;
		if(Ammo==0) return;
		if(Time.time > lastFire + reloadTime){
			OneInTheChamber = true;
			Ammo--;
		}
	}

	public bool Fire(){
		update();
		if(OneInTheChamber){
			lastFire = Time.time;
			OneInTheChamber = false;
			return true;
		}
		else {
			return false;
		}

	}
}