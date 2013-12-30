using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public GameObject[] m_PossibleEnemyLocations;
	public GameObject m_EnemySitePrefab;

	private GameObject m_EnemySite;
	private int m_GoalsLeft = 0;

	void Start () {
		Transform chosenLocation = m_PossibleEnemyLocations[Random.Range(0,m_PossibleEnemyLocations.Length)].transform;
		m_EnemySite = Instantiate(m_EnemySitePrefab, chosenLocation.position, chosenLocation.rotation) as GameObject;

		foreach(Destroyer d in m_EnemySite.GetComponentsInChildren<Destroyer>()){
			if(d.m_IsGameGoal) m_GoalsLeft++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoalDestroyed(){
		m_GoalsLeft--;
		if(m_GoalsLeft==0){
			Debug.Log("WIN STATE");
		}
	}
}
