using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Transform mTransform;

	void Start () {
		mTransform = transform;
	}
	
	void LateUpdate(){
		float dx = Input.GetAxis(In.AXIS_CAMERA_X);


		Quaternion rotation = Quaternion.Euler(0, mTransform.rotation.eulerAngles.y + dx, 0);
		mTransform.rotation = rotation;
		
		/*Quaternion rotation = Quaternion.Euler(0, dx, 0);
		mTransform.rotation *= rotation;*/
	}
}
