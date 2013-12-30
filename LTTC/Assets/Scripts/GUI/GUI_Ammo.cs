using UnityEngine;
using System.Collections;

public class GUI_Ammo : MonoBehaviour {

	private Gun m_Gun;
	private GunFire m_GunFireObject;

	void Start () {
		m_GunFireObject = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponentInChildren<GunFire>();
	}

	void Update () {
		if(m_Gun == null) m_Gun = m_GunFireObject.TankGun;
		this.guiText.text = m_Gun.Ammo.ToString();
	}
}
