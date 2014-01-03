using UnityEngine;
using System.Collections;

public class TankMovementControls : MonoBehaviour {
	// For caching transform and rigidbody.
	private Transform mTransform;
	private Rigidbody mRigidBody;

	// For checking if the tank is on the ground and not upside down.
	private Grounder mGrounder;

	// Tank Properties
	private float acceleration = 10; 			// The acceleration of the tank when vertical input is given.
	private float angularAcceleration = 10;		// The angular acceleration of the tank when horizontal input is given.
	private float maxTurnSpeed = 1;				// Maximum turning speed.
	public float maxSpeed = 20;				// Maximum speed.
	private float turnDrag = 10;				// Angular "drag", deceleration when tank is turning but input is not given.
	private float frictionMultiplier = 500;		// Variable for altering the friction force

	private AudioSource aIdle;
	private AudioSource aAcc;

	void Start () {
		mTransform = transform;
		mRigidBody = rigidbody;
		mGrounder = mTransform.GetComponentInChildren<Grounder>();


	}
	
	void Update () {
		//Get inputs into variables.
		var hInput = Input.GetAxis(In.AXIS_MOVEMENT_HORIZONTAL);
		var vInput = Input.GetAxis(In.AXIS_MOVEMENT_VERTICAL);		// negative => forward


		bool neutral = true;	// For checking if tracks have power.
		float traction = 1f / mRigidBody.velocity.magnitude;	// The faster the tank moves, the more we want it to slide

		// Local velocity is needed for calculating the friction force and limiting speed.
		// Transforms a direction from world space to local space. The opposite of Transform.TransformDirection.
		Vector3 velocityLocal = mTransform.InverseTransformDirection(mRigidBody.velocity);

		// The forward direction vector in world coordinates.
		Vector3 fwd = mTransform.TransformDirection (Vector3.forward);

		// The angle between our velocity vector and forward direction in world coordinates. Used for checking if we are 
		// moving forward or in reverse. When the tank is still the angle is 90, we substract it just for clarity.
		float velocityAngle = Vector3.Angle(mRigidBody.velocity, fwd) - 90;
		if(velocityAngle < 0)
			hInput *= -1;		// moving backward

		// Apply force if the tank is grounded and not moving at maximum speed. Set neutral to false.
		if( vInput != 0 && mGrounder.IsGrounded ){
			neutral = false;
			if(velocityLocal.z < maxSpeed && velocityLocal.z > -maxSpeed){
				mRigidBody.AddForce( fwd * -vInput * acceleration * mRigidBody.mass );	// F = m*a
			}
		}

		// Calculate friction force applied according to the difference between local velocity and forward direciton.
		// The more we are sliding sideways, the more friction force is applied. This is done to simulate the tracks
		// rolling freely when no power is applied to them.
		float frictionForce = 0;
		float frictionAngle = Vector3.Angle(velocityLocal, -Vector3.forward);
		if (frictionAngle <= 90)
			frictionForce = frictionAngle;
		else
			frictionForce = Mathf.Abs(frictionAngle - 180);
		
		// Apply the friction force if we are grounded and moving.
		if (mRigidBody.velocity.magnitude >= 0.5 && mGrounder.IsGrounded == true){
			Vector3 d = -mRigidBody.velocity.normalized;
			mRigidBody.AddForce(d * frictionForce * frictionMultiplier);
		}
		
		// Apply torque accoriding to horizontal input when the tank is grounded and not turning at maximum speed. Also set neutral to false
		// because the tracks have power.
		if( hInput != 0 && mGrounder.IsGrounded ){
			neutral = false;
			if (rigidbody.angularVelocity.magnitude < maxTurnSpeed){ 
				mRigidBody.AddTorque( Vector3.up * hInput * angularAcceleration * mRigidBody.mass);
			}
		}else{
			// If no input is given we should stop turning quite quickly.
			mRigidBody.AddTorque( -mRigidBody.angularVelocity * turnDrag * mRigidBody.mass );
		}
		// Changing direction when turning on the move is simulated with Vector3.Slerp. The faster we are moving the less we 
		// should slerp in order to simulate traction.
		if( velocityAngle > 0 && !neutral && mGrounder.IsGrounded){
			mRigidBody.velocity = Vector3.Slerp( mRigidBody.velocity, -mTransform.forward * mRigidBody.velocity.magnitude, traction);
		}else if ( velocityAngle < 0 && !neutral && mGrounder.IsGrounded  ){
			mRigidBody.velocity = Vector3.Slerp( mRigidBody.velocity, mTransform.forward * mRigidBody.velocity.magnitude, traction );
		}



	}
}