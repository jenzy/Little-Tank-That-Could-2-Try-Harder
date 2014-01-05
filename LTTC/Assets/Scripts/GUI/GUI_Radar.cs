using UnityEngine;
using System.Collections;

public class GUI_Radar : MonoBehaviour {
	public Transform m_Camera;
	public Texture2D m_RadarArrowTex;
	public Texture2D m_RadarBackgroundTex;
	public int m_RadarSizeInPX = 150;

	private EnemyManager enemy = null;
	private Vector3 enemyLocation;

	private float rotationAngle = 0;

	
	// Update is called once per frame
	void Update () {
		if(MainMenu.ChosenDifficulty != MainMenu.Difficulty.EASY) return;
		
		if(enemy == null){
			enemy = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLER).GetComponent<EnemyManager>();
			enemyLocation = enemy.EnemyLocation;
		}

		Vector3 currentLocation = m_Camera.position;
		Vector3 enemyDirection = enemyLocation - currentLocation;
		float enemyAngle = Mathf.Rad2Deg * Mathf.Atan2(enemyDirection.x, enemyDirection.z);
		float cameraAngle = m_Camera.eulerAngles.y ;
		float deltaAngle = (cameraAngle - enemyAngle) % 360;

		rotationAngle = 360 - deltaAngle;
	}

	void OnGUI(){
		if(MainMenu.ChosenDifficulty != MainMenu.Difficulty.EASY) return;

		GUI.DrawTexture(new Rect(10, Screen.height-m_RadarSizeInPX-10, m_RadarSizeInPX, m_RadarSizeInPX), m_RadarBackgroundTex);
		GUI.BeginGroup(new Rect(10,Screen.height-m_RadarSizeInPX-10, m_RadarSizeInPX, m_RadarSizeInPX));
		GUIUtility.RotateAroundPivot(rotationAngle , new Vector2(m_RadarSizeInPX/2, m_RadarSizeInPX/2));
		GUI.DrawTexture(new Rect(0, 0, m_RadarSizeInPX, m_RadarSizeInPX), m_RadarArrowTex);
		GUI.EndGroup();


		/*
		 * UPPER LEFT CORNER
		 * GUI.BeginGroup(new Rect(0,0,200,200));
		 * GUIUtility.RotateAroundPivot(rotationAngle , new Vector2(100, 100));
		 * GUI.DrawTexture(new Rect(0, 0, 200, 200), radarTex);
		 * GUI.EndGroup();
		 */

	}
}
