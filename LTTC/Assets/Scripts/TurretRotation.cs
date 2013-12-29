using UnityEngine;
using System.Collections;

public class TurretRotation : MonoBehaviour {
	public float mMaxDegreesPerSecond = 30f;

	private Transform mTransform;
	private CameraSwitcher m_Camera;

	void Start () {
		mTransform = transform;
		m_Camera = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLER).GetComponent<CameraSwitcher>();
	}

	void Update () {
		Transform target = m_Camera.ActiveCameraGroup;
		float dy = target.localEulerAngles.y - mTransform.localEulerAngles.y;
		Quaternion qTo = mTransform.localRotation * Quaternion.Euler(0, dy, 0);
		mTransform.localRotation = Quaternion.RotateTowards( mTransform.localRotation, qTo, mMaxDegreesPerSecond * Time.deltaTime);
	}
}
