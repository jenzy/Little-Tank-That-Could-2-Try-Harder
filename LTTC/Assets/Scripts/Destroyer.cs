using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	public Transform m_Explosion;
	public bool m_IsGameGoal = false;

	private EnemyManager manager;
	private TerrainCraterMaker craterMaker;

	void Start(){
		manager = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLER).GetComponent<EnemyManager>();
		craterMaker = Terrain.activeTerrain.GetComponent<TerrainCraterMaker>();
	}

	public void Destroy(){
		Debug.Log( (m_IsGameGoal ? "[GOAL] " : "" ) + this.name + " is being destroyed");

		if(m_IsGameGoal){
			manager.GoalDestroyed();
			craterMaker.MakeCrater(this.transform.position);
		}

		Instantiate(m_Explosion, this.transform.position, Quaternion.identity);
		Destroy(this.gameObject, 0.1f);
	}

}
