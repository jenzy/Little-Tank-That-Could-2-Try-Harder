using UnityEngine;
using System.Collections;

public class CameraAimUpDownRotation : MonoBehaviour {
	public GameObject m_Gun; 	// Folow this object

	private Transform m_Transform;
	void Start () {
		m_Transform = transform;
	}
	
	void LateUpdate () {
		float x = m_Gun.transform.localEulerAngles.x;
		float dx = x - m_Transform.localEulerAngles.x;
		m_Transform.localRotation *= Quaternion.Euler(dx, 0, 0);

	}
}
