using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {
	public Transform m_Explosion;

	void OnDestroy(){
		Debug.Log(this.name + " is being destroyed");
		Instantiate(m_Explosion, this.transform.position, Quaternion.identity);
	}
}
