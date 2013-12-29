using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	public Transform m_Explosion;

	public void Destroy(){
		Debug.Log(this.name + " is being destroyed");
		Instantiate(m_Explosion, this.transform.position, Quaternion.identity);
		Destroy(this.gameObject, 0.1f);
	}

}
