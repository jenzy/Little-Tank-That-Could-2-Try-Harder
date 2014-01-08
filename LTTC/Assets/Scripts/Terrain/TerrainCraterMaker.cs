using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainCraterMaker : MonoBehaviour {
	public Texture2D m_CraterTexture;
	public Transform m_ExplosionWithTerrain;

	private TerrainData m_TerrainData;
	private float[,] m_SavedTerrainData;
	private Color[] m_CraterData;

	private int m_HeightmapWidth;
	private int m_HeightmapHeight;

	private TerrainTextureChanger m_texChanger;
	private List<GameObject> m_Trees;

	void Start () {
		m_TerrainData = Terrain.activeTerrain.terrainData;
		m_HeightmapWidth = m_TerrainData.heightmapWidth;
		m_HeightmapHeight = m_TerrainData.heightmapHeight;
		m_SavedTerrainData = m_TerrainData.GetHeights(0, 0, m_HeightmapWidth, m_HeightmapHeight);
		m_CraterData = m_CraterTexture.GetPixels();

		m_texChanger = Terrain.activeTerrain.GetComponent<TerrainTextureChanger>();

		m_Trees = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.TREE));
	}
	
	void OnApplicationQuit () {
		m_TerrainData.SetHeights(0, 0, m_SavedTerrainData);
	}

	public void MakeCrater( Vector3 impactWorldCoordinates ){
		Instantiate(m_ExplosionWithTerrain, impactWorldCoordinates, Quaternion.identity);
		Vector3 impact = impactWorldCoordinates - Terrain.activeTerrain.transform.position;

		int x = (int)Mathf.Lerp(0, m_HeightmapWidth, Mathf.InverseLerp(0, m_TerrainData.size.x, impact.x));
		int z = (int)Mathf.Lerp(0, m_HeightmapHeight, Mathf.InverseLerp(0, m_TerrainData.size.z, impact.z));

		x = Mathf.Clamp(x, m_CraterTexture.width/2, m_HeightmapWidth-m_CraterTexture.width/2);
		z = Mathf.Clamp(z, m_CraterTexture.height/2, m_HeightmapHeight-m_CraterTexture.height/2);

		m_texChanger.MakeCrater( x, z );
		

		float[,] areaT = m_TerrainData.GetHeights((int)(x-m_CraterTexture.width/2), (int)(z-m_CraterTexture.height/2), m_CraterTexture.width, m_CraterTexture.height);

		for (int i = 0; i < m_CraterTexture.height; i++) {
			for (int j = 0; j < m_CraterTexture.width; j++) {
				areaT[i,j] = (float)(areaT[i,j] - m_CraterData[i*m_CraterTexture.width+j].a * 0.01);
			}	
		}
		
		m_TerrainData.SetHeights((int)(x-m_CraterTexture.width/2), (int)(z-m_CraterTexture.height/2), areaT);

		// Destroy trees
		for(int i=0; i<m_Trees.Count; i++){
			GameObject tree = m_Trees[i];
			//Debug.Log(Vector3.Distance(impactWorldCoordinates, tree.transform.position));
			if(Vector3.Distance(impactWorldCoordinates, tree.transform.position) < 13){
				Destroy(tree);
				m_Trees.RemoveAt(i);
			}
		}
	}
}
