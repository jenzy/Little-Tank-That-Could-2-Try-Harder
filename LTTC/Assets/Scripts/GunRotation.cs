using UnityEngine;
using System.Collections;

public class GunRotation : MonoBehaviour {
	public float mMaxDegreesPerSecond = 15f;
	public float mClampMaxX = 7f;	// (-180, 180)
	public float mClampMinX = -25f;	// (-180, 180)

	private Transform mTransform;
	
	void Start () {
		mTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		float dVert = Input.GetAxis(In.AXIS_CAMERA_Y);
		Vector3 currentRotation = mTransform.eulerAngles;
		float desiredX = currentRotation.x - dVert;
		
		if(desiredX > 180) desiredX = desiredX - 360;	// convert (0, 360) to (-180, 180)
		desiredX = Mathf.Clamp(desiredX, mClampMinX, mClampMaxX);
		float dx = desiredX - currentRotation.x;

		Quaternion qTo = mTransform.localRotation * Quaternion.Euler(dx, 0, 0);
		mTransform.localRotation = Quaternion.RotateTowards( mTransform.localRotation, qTo, mMaxDegreesPerSecond * Time.deltaTime);

		/*
		 * SAFE
		 * float dx = mTarget.transform.localEulerAngles.x - mTransform.localEulerAngles.x;
		 * Quaternion qTo = mTransform.localRotation * Quaternion.Euler(dx, 0, 0);
		 * mTransform.localRotation = Quaternion.RotateTowards( mTransform.localRotation, qTo, mMaxDegreesPerSecond * Time.deltaTime);
		 * 
		 */
	}
}
