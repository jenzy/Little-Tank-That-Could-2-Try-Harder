using UnityEngine;
using System.Collections;

public class EngineSound : MonoBehaviour {

	private Transform m_Transform;
	private Rigidbody m_RigidBody;

	private AudioSource aIdle;
	private AudioSource aAcc;
	private float maxSpeed;

	void Start () {
		m_Transform = transform;
		m_RigidBody = rigidbody;
		maxSpeed = GetComponent<TankMovementControls>().maxSpeed;

		Component[] audios = GetComponents<AudioSource>();
		aIdle = audios[0] as AudioSource;
		aAcc = audios[1] as AudioSource;
	}
	
	void Update () {
		float fwdVelocity = -1f * (m_Transform.InverseTransformDirection(m_RigidBody.velocity)).z;
		
		if(Mathf.Abs(fwdVelocity) > 0.5f){
			if(!aAcc.isPlaying){
				aAcc.Play();
				aIdle.Stop();
			}
			float dp = (Mathf.Abs(fwdVelocity)/maxSpeed * 4 - 2)/10;
			aAcc.pitch = 1 + dp;
		} else {
			if(aAcc.isPlaying){
				aIdle.Play();
				aAcc.Stop();
			}
		}
	}
}
