using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {
	public float mClampMaxX = 30f;	// (-180, 180)
	public float mClampMinX = -10f;	// (-180, 180)

	private Transform mTransform;

	void Start () {
		mTransform = transform;
	}
	
	void LateUpdate(){
		float dHor = Input.GetAxis(In.AXIS_CAMERA_X);
		
		Vector3 currentRotation = mTransform.eulerAngles;
		float desiredY = currentRotation.y + dHor;
		float dy = desiredY - currentRotation.y;
		
		Quaternion rotation = Quaternion.Euler(0, dy, 0);
		mTransform.rotation *= rotation;

		/*float dHor = Input.GetAxis(In.AXIS_CAMERA_X);
		//float dVert = Input.GetAxis(In.AXIS_CAMERA_Y);

		Vector3 currentRotation = mTransform.eulerAngles;
		float desiredY = currentRotation.y + dHor;
		//float desiredX = currentRotation.x - dVert;

		//if(desiredX > 180) desiredX = desiredX - 360;	// convert (0, 360) to (-180, 180)
		//desiredX = Mathf.Clamp(desiredX, mClampMinX, mClampMaxX);

		Quaternion rotation = Quaternion.Euler(0, desiredY, 0);
		mTransform.rotation = rotation;
		*/
		/* SAFE
		 * float dHor = Input.GetAxis(In.AXIS_CAMERA_X);
		 * Quaternion rotation = Quaternion.Euler(0, mTransform.rotation.eulerAngles.y + dHor, 0);
		 * mTransform.rotation = rotation;
		 */
	}
}
