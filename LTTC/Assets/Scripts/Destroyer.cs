using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	public Transform m_Explosion;
	public bool m_IsGameGoal = false;
	public int m_Health = 100;
	public int m_DefaultDamageTakenOnHit = 34;

	public int HealthLeft {
		get { 
			if(m_Health < 0) return 0; 
			else return m_Health; 
		}
	}

	private EnemyManager m_EnemyManager;
	private TerrainCraterMaker craterMaker;
	private CameraSwitcher m_CameraSwitcher;
	private GeneralStuff m_Manager;

	void Start(){
		m_EnemyManager = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLER).GetComponent<EnemyManager>();
		m_Manager = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLER).GetComponent<GeneralStuff>();
		craterMaker = Terrain.activeTerrain.GetComponent<TerrainCraterMaker>();
		m_CameraSwitcher = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLER).GetComponent<CameraSwitcher>();
	}

	public void Hit(){
		Hit(m_DefaultDamageTakenOnHit);
	}

	public void Hit(int damage){
		m_Health -= damage;
		Debug.Log(string.Format("{0} {1} was hit ({2} dmg). Health left: {3}", (m_IsGameGoal ? "[GOAL] " : "" ), this.name, damage, m_Health));

		if(m_Health <= 0){
			if(this.CompareTag(Tags.PLAYER)){
				Debug.Log("player dead");
				m_CameraSwitcher.SetupDeadCamera();
				m_Manager.Lose(7);
			}
			if(m_IsGameGoal)
				m_EnemyManager.GoalDestroyed();
			craterMaker.MakeCrater(this.transform.position);
			Instantiate(m_Explosion, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject, 0.1f);
		}
	}

}
