using UnityEngine;
using System.Collections;

public class GUI_Shell : MonoBehaviour {

	private Gun m_Gun;
	private GunFire m_GunFireObject;
	
	void Start () {
		m_GunFireObject = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponentInChildren<GunFire>();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_Gun == null) m_Gun = m_GunFireObject.TankGun;
		this.guiTexture.enabled = m_Gun.OneInTheChamber;
	}
}
