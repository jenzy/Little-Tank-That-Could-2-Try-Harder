using UnityEngine;
using System.Collections;

public class TerrainTextureChanger : MonoBehaviour {
	public Texture2D m_CraterTexture;

	private Color[] m_CraterData;

	private TerrainData m_TerrainData;
	private float[,,] m_saved;

	private int m_Width;	// Aplha map
	private int m_Height;	// Aplha map
	private int m_Layers;	// Aplha map

	// Use this for initialization
	void Start () {
		m_TerrainData = Terrain.activeTerrain.terrainData;
		m_Width = m_TerrainData.alphamapWidth;
		m_Height = m_TerrainData.alphamapHeight;
		m_Layers = m_TerrainData.alphamapLayers;
		m_saved = m_TerrainData.GetAlphamaps(0, 0, m_Width, m_Height);
		m_CraterData = m_CraterTexture.GetPixels();
	}

	void OnApplicationQuit () {
		m_TerrainData.SetAlphamaps(0, 0, m_saved);
	}

	public void MakeCrater( Vector3 impactWorldCoordinates ){
		Vector3 impact =  impactWorldCoordinates - Terrain.activeTerrain.transform.position;
		
		// convert impact to terrain coordinates
		int g = (int) Mathf.Lerp(0, m_Width, Mathf.InverseLerp(0, m_TerrainData.size.x, impact.x));
		int b = (int) Mathf.Lerp(0, m_Width, Mathf.InverseLerp(0, m_TerrainData.size.z, impact.z));
		g = Mathf.Clamp(g, m_CraterTexture.width/2, m_Width-m_CraterTexture.width/2);
		b = Mathf.Clamp(b, m_CraterTexture.height/2, m_Width-m_CraterTexture.height/2);

		MakeCrater(g, b);
	}

	public void MakeCrater( int terrainCoordinateX, int terrainCoordinateZ ){
		int g = terrainCoordinateX;
		int b = terrainCoordinateZ;

		// area to be changed
		float[,,] area = m_TerrainData.GetAlphamaps( g-m_CraterTexture.width/2, 
		                                            b-m_CraterTexture.height/2, 
		                                            m_CraterTexture.width, 
		                                            m_CraterTexture.height );
		// make the change
		for( int x = 0; x < m_CraterTexture.height; x++ ) {
			for( int y = 0; y < m_CraterTexture.width; y++ ) {
				for( int z = 0; z < m_Layers; z++){	
					if (z == 1){
						area[x,y,z] += m_CraterData[ x*m_CraterTexture.width + y ].a; // layer 1 becomes visible
					} else{	
						area[x,y,z] -= m_CraterData[ x*m_CraterTexture.width + y ].a;	
					}	
				}
			}
		}

		// apply the change
		m_TerrainData.SetAlphamaps (g-m_CraterTexture.width/2,b-m_CraterTexture.height/2,area);
	}
}