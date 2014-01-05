using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
	public GameObject[] m_Cameras;
	public int m_IndexMainCamera;
	public int m_IndexAimCamera;
	public bool m_ToggleAimCamera = true;

	private int m_IndexActiveCamera;
	private bool m_Ignore = false;

	public GameObject ActiveCamera {
		get { return m_Cameras[m_IndexActiveCamera]; }
	}
	public Transform ActiveCameraGroup {
		get { return ActiveCamera.transform.parent.parent; }
	}

	//Zoom
	public int m_ZoomFOV = 20;
	public int m_NormalFOV = 60;
	public float m_Smooth = 5f;
	private bool m_IsZoomed = false;

	void Start () {
		m_IndexActiveCamera = m_IndexMainCamera;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_Ignore) return;
		if(m_IndexActiveCamera == m_IndexAimCamera){
			float input = Input.GetAxis(In.AXIS_ZOOM);
			if(input != 0)
				m_IsZoomed = !m_IsZoomed;

			if(m_IsZoomed)
				ActiveCamera.camera.fieldOfView = Mathf.Lerp(ActiveCamera.camera.fieldOfView, m_ZoomFOV, Time.deltaTime * m_Smooth);
			else
				ActiveCamera.camera.fieldOfView = Mathf.Lerp(ActiveCamera.camera.fieldOfView, m_NormalFOV, Time.deltaTime * m_Smooth);
		}


		if( m_ToggleAimCamera ){
			if ( Input.GetButtonUp(In.BUTTON_AIM) ){
				if( m_IndexActiveCamera == m_IndexAimCamera ){
					m_IsZoomed = false;
					ActiveCamera.camera.fieldOfView = m_NormalFOV;
					switchToCamera(m_IndexMainCamera);
				}
				else
					switchToCamera(m_IndexAimCamera);
			}
		}
		else {
			if( Input.GetButtonDown(In.BUTTON_AIM))
				switchToCamera(m_IndexAimCamera);
			else if( Input.GetButtonUp(In.BUTTON_AIM)){
				m_IsZoomed = false;
				ActiveCamera.camera.fieldOfView = m_NormalFOV;
				switchToCamera(m_IndexMainCamera);
			}
		}
	}

	private void switchToCamera( int index ){
		if( index == m_IndexAimCamera ){
			Quaternion q = GameObject.FindGameObjectWithTag(Tags.TURRET_GROUP).transform.localRotation;
			m_Cameras[m_IndexAimCamera].transform.parent.parent.localRotation = q;
		}
		for (int i = 0; i < m_Cameras.Length; i++) {
			if(index==i){
				m_Cameras[i].camera.enabled = true;
			} else {
				m_Cameras[i].camera.enabled = false;
			}
		}
		m_IndexActiveCamera = index;
	}

	public void SetupDeadCamera(){
		if(m_IndexActiveCamera != m_IndexMainCamera)
			switchToCamera(m_IndexMainCamera);
		ActiveCamera.GetComponent<AudioListener>().enabled = true;
		ActiveCameraGroup.parent = null;
		m_Ignore = true;
	}
}
