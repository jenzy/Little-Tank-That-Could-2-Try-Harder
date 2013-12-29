﻿using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
	public GameObject[] m_Cameras;
	public int m_IndexMainCamera;
	public int m_IndexAimCamera;
	public bool m_ToggleAimCamera = true;

	private int m_IndexActiveCamera;

	void Start () {
		m_IndexActiveCamera = m_IndexMainCamera;
	}
	
	// Update is called once per frame
	void Update () {
		if( m_ToggleAimCamera ){
			if ( Input.GetButtonUp(In.BUTTON_AIM) ){
				if( m_IndexActiveCamera == m_IndexAimCamera )
					switchToCamera(m_IndexMainCamera);
				else
					switchToCamera(m_IndexAimCamera);
			}
		}
		else {
			if( Input.GetButtonDown(In.BUTTON_AIM))
				switchToCamera(m_IndexAimCamera);
			else if( Input.GetButtonUp(In.BUTTON_AIM))
				switchToCamera(m_IndexMainCamera);
		}
	}

	private void switchToCamera( int index ){
		for (int i = 0; i < m_Cameras.Length; i++) {
			if(index==i){
				m_Cameras[i].camera.enabled = true;
			} else {
				m_Cameras[i].camera.enabled = false;
			}
		}
		m_IndexActiveCamera = index;
	}
}
