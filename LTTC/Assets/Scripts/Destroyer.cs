using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	public Transform m_Explosion;
	public bool m_IsGameGoal = false;
	public int m_Health = 100;
	public int m_DefaultDamageTakenOnHit = 34;

	private EnemyManager manager;
	private TerrainCraterMaker craterMaker;

	void Start(){
		manager = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLER).GetComponent<EnemyManager>();
		craterMaker = Terrain.activeTerrain.GetComponent<TerrainCraterMaker>();
	}

	public void Hit(){
		Hit(m_DefaultDamageTakenOnHit);
	}

	public void Hit(int damage){
		m_Health -= damage;
		Debug.Log(string.Format("{0} {1} was hit ({2} dmg). Health left: {3}", (m_IsGameGoal ? "[GOAL] " : "" ), this.name, damage, m_Health));

		if(m_Health <= 0){

			if(m_IsGameGoal)
				manager.GoalDestroyed();
			craterMaker.MakeCrater(this.transform.position);
			Instantiate(m_Explosion, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject, 0.1f);
		}
	}

}
