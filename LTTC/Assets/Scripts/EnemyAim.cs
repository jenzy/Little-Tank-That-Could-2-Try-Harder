using UnityEngine;
using System.Collections;

public class EnemyAim : MonoBehaviour {
	public Transform m_Turret;
	public Transform m_Gun;
	public Transform m_ProjectileStart;
	public GameObject m_Projectile;
	public float m_InitialForce = 100000f;
	public GameObject m_MuzzleFlash;
	public float m_Range = 100f;
	public float m_FireChance = 0.005f;

	private GameObject m_Target;
	private Gun tankGun;
	private RaycastHit LOS;

	void Start () {
		m_Target = GameObject.FindGameObjectWithTag(Tags.PLAYER);
		tankGun = new Gun(1000, 10);
	}
	
	void Update () {
		if(m_Target==null) return;
		Vector3 targetLocation = m_Target.transform.position;// + new Vector3(0,1,0);
		Vector3 vFromMeToTarget = targetLocation - transform.position;

		// Rotate Turret and Gun ~ cheaty
		float angleEnemyY = Mathf.Atan2(vFromMeToTarget.x, vFromMeToTarget.z) * Mathf.Rad2Deg;
		float currentTurretAngle = m_Turret.eulerAngles.y;
		m_Turret.rotation = m_Turret.rotation * Quaternion.Euler(0, angleEnemyY-currentTurretAngle, 0);
		m_Gun.rotation = Quaternion.LookRotation(vFromMeToTarget, Vector3.up);

		float distance = Vector3.Distance(transform.position, m_Target.transform.position);
		//if(Input.GetButtonUp("Jump")){
			float random = Random.Range(0f, 1f);
			if(distance < m_Range && (random < m_FireChance) && tankGun.Fire()){
				Vector3 startPosition = m_ProjectileStart.position;
				Vector3 direction = m_Gun.transform.rotation * Vector3.forward;
				
				if(Physics.Raycast(startPosition, m_Gun.transform.rotation * Vector3.forward, out LOS)){
					GameObject other = LOS.collider.gameObject;
					GameObject par = Tags.findParentWithTag(Tags.PLAYER, other);
					if(other.CompareTag(Tags.PLAYER) || (par != null && par.CompareTag(Tags.PLAYER))){
						Debug.Log("LOS player");

						GameObject projectile = Instantiate(m_Projectile, startPosition, Quaternion.identity) as GameObject;
						Vector3 force = direction * m_InitialForce;
						projectile.rigidbody.AddForce(force);
						
						Object o = Instantiate(m_MuzzleFlash, startPosition, m_Gun.transform.rotation);
						Destroy(o, 5);
					}
				}
			}
		//}

	}


	/*// Rotate Turret
	float angleEnemyY = Mathf.Atan2(vFromMeToTarget.x, vFromMeToTarget.z) * Mathf.Rad2Deg;
	float currentTurretAngle = m_Turret.eulerAngles.y;
	Quaternion qTo = m_Turret.rotation * Quaternion.Euler(0, angleEnemyY-currentTurretAngle, 0);
	m_Turret.rotation = Quaternion.RotateTowards(m_Turret.rotation, qTo, maxDegreesPerSecond * Time.deltaTime);
	
	//Rotate Gun
	float angleEnemyX = Mathf.Atan2(vFromMeToTarget.y, vFromMeToTarget.z) * Mathf.Rad2Deg;
	float currentGuntAngle = m_Gun.eulerAngles.x;
	qTo = m_Gun.rotation * Quaternion.Euler(angleEnemyX-currentGuntAngle-180, 0, 0);
	m_Gun.rotation = Quaternion.RotateTowards(m_Gun.rotation, qTo, maxDegreesPerSecond * Time.deltaTime);*/

}
