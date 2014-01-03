using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {
	public bool m_LockRotationZX;

	private Transform mTransform;

	void Start () {
		mTransform = transform;
	}
	
	void LateUpdate(){
		float dHor = Input.GetAxis(In.AXIS_CAMERA_X);
		
		Vector3 currentRotation = mTransform.eulerAngles;
		float desiredY = currentRotation.y + dHor;

		if(m_LockRotationZX){
			Quaternion rotation = Quaternion.Euler(0, desiredY, 0);
			mTransform.rotation = rotation;
		} else {
			float dy = desiredY - currentRotation.y;
			Quaternion rotation = Quaternion.Euler(0, dy, 0);
			mTransform.localRotation *= rotation;
		}


		// OLD, (Working) CODE
		/* float dHor = Input.GetAxis(In.AXIS_CAMERA_X);
		 * Vector3 currentRotation = mTransform.eulerAngles;
		 * float desiredY = currentRotation.y + dHor;
		 * Quaternion rotation = Quaternion.Euler(0, desiredY, 0);
		 * mTransform.rotation = rotation;
		 */
		/* SAFE
		 * float dHor = Input.GetAxis(In.AXIS_CAMERA_X);
		 * Quaternion rotation = Quaternion.Euler(0, mTransform.rotation.eulerAngles.y + dHor, 0);
		 * mTransform.rotation = rotation;
		 */
	}
}
