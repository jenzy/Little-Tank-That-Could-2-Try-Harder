using UnityEngine;
using System.Collections;

public class TurretRotation : MonoBehaviour {
	public GameObject mTarget;	// Turret follows the rotation of this object
	public float mMaxDegreesPerSecond = 30f;

	private Transform mTransform;

	void Start () {
		mTransform = transform;
	}

	void Update () {
		float dy = mTarget.transform.localEulerAngles.y - mTransform.localEulerAngles.y;
		Quaternion qTo = mTransform.localRotation * Quaternion.Euler(0, dy, 0);
		mTransform.localRotation = Quaternion.RotateTowards( mTransform.localRotation, qTo, mMaxDegreesPerSecond * Time.deltaTime);
	}
}
