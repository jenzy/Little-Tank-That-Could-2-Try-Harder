using UnityEngine;
using System.Collections;

public class GunRotation : MonoBehaviour {
	public float m_MaxDegreesPerSecond = 15f;
	public float m_ClampMaxX = 7f;	// (-180, 180)
	public float m_ClampMinX = -25f;	// (-180, 180)
	public float sensitivity = 0.5f;

	private Transform mTransform;
	
	void Start () {
		mTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		float dVert = Input.GetAxis(In.AXIS_CAMERA_Y) * sensitivity;
		Vector3 currentRotation = mTransform.localEulerAngles;
		float desiredX = currentRotation.x - dVert;
		
		if(desiredX > 180) desiredX = desiredX - 360;	// convert (0, 360) to (-180, 180)
		desiredX = Mathf.Clamp(desiredX, m_ClampMinX, m_ClampMaxX);
		float dx = desiredX - currentRotation.x;
		
		mTransform.localRotation *= Quaternion.Euler(dx, 0, 0);

		/*float dVert = Input.GetAxis(In.AXIS_CAMERA_Y);
		Vector3 currentRotation = mTransform.eulerAngles;
		float desiredX = currentRotation.x - dVert;
		
		if(desiredX > 180) desiredX = desiredX - 360;	// convert (0, 360) to (-180, 180)
		desiredX = Mathf.Clamp(desiredX, m_ClampMinX, m_ClampMaxX);
		float dx = desiredX - currentRotation.x;

		Quaternion qTo = mTransform.localRotation * Quaternion.Euler(dx, 0, 0);
		mTransform.localRotation = Quaternion.RotateTowards( mTransform.localRotation, qTo, m_MaxDegreesPerSecond * Time.deltaTime);
		*/
		/*
		 * SAFE
		 * float dx = mTarget.transform.localEulerAngles.x - mTransform.localEulerAngles.x;
		 * Quaternion qTo = mTransform.localRotation * Quaternion.Euler(dx, 0, 0);
		 * mTransform.localRotation = Quaternion.RotateTowards( mTransform.localRotation, qTo, mMaxDegreesPerSecond * Time.deltaTime);
		 * 
		 */
	}
}
