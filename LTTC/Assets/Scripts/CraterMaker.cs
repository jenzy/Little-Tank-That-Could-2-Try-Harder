using UnityEngine;
using System.Collections;

public class CraterMaker : MonoBehaviour {
	public Texture2D mCraterTexture;

	private TerrainData mTerrainData;
	private float[,] mSavedTerrainData;
	private Color[] mCraterData;

	private int mHeightmapWidth;
	private int mHeightmapHeight;

	private Queue queue;

	void Start () {
		mTerrainData = Terrain.activeTerrain.terrainData;
		mHeightmapWidth = mTerrainData.heightmapWidth;
		mHeightmapHeight = mTerrainData.heightmapHeight;
		mSavedTerrainData = mTerrainData.GetHeights(0, 0, mHeightmapWidth, mHeightmapHeight);
		mCraterData = mCraterTexture.GetPixels();
		queue = new Queue();
	}
	
	void OnApplicationQuit () {
		mTerrainData.SetHeights(0, 0, mSavedTerrainData);
	}

	public void AddCrater( Vector3 impact ){
		queue.Enqueue(impact);
	}

	void Update(){
		while(queue.Count > 0){
			Vector3 impact = (Vector3)queue.Dequeue();
			MakeCrater(impact);
		}
	}
	

	public void MakeCrater( Vector3 impact ){
		impact -= transform.position;

		int x = (int)Mathf.Lerp(0, mHeightmapWidth, Mathf.InverseLerp(0, mTerrainData.size.x, impact.x));
		int z = (int)Mathf.Lerp(0, mHeightmapHeight, Mathf.InverseLerp(0, mTerrainData.size.z, impact.z));


		//Debug.Log(impact + " " + x + " " + z);

		x = Mathf.Clamp(x, mCraterTexture.width/2, mHeightmapWidth-mCraterTexture.width/2);
		z = Mathf.Clamp(z, mCraterTexture.height/2, mHeightmapHeight-mCraterTexture.height/2);

		//Debug.Log("c# " + x + " " + z);
		

		float[,] areaT = mTerrainData.GetHeights((int)(x-mCraterTexture.width/2), (int)(z-mCraterTexture.height/2), mCraterTexture.width, mCraterTexture.height);

		for (int i = 0; i < mCraterTexture.height; i++) {
			for (int j = 0; j < mCraterTexture.width; j++) {
				areaT[i,j] = (float)(areaT[i,j] - mCraterData[i*mCraterTexture.width+j].a * 0.01);
				//areaT[i,j] = 200;
			}	
		}
		
		mTerrainData.SetHeights((int)(x-mCraterTexture.width/2), (int)(z-mCraterTexture.height/2), areaT);
	}
}
