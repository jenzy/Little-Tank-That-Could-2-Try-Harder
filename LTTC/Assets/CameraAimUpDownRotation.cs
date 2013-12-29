using UnityEngine;
using System.Collections;

public class CameraAimUpDownRotation : MonoBehaviour {
	public GameObject m_Gun;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float x = m_Gun.transform.localEulerAngles.x;
		float dx = x - transform.localEulerAngles.x;
		Quaternion q = Quaternion.Euler(dx, 0, 0);
		transform.localRotation *= q;

	}
}
