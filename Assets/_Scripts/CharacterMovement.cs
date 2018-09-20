//Zachary Cobb
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour {

	public float gravity = -9.8f;
	public float moveSpeed = 5.0f;
	public float jumpSpeed = 10.0f;
	public float terminalVelocity = -10.0f;
	public float minFall = -1.5f;
	public float pushForce = 3.0f;

	private float _vertSpeed;
	private CharacterController _charController;
	private ControllerColliderHit _contact;

	void Start () {
		_charController = GetComponent<CharacterController>();
		_vertSpeed = minFall;
	}
	
	void Update () {
		Vector3 movement = Vector3.zero;

		Vector2 dirVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
		
		movement.x = dirVector.x;
		movement.z = dirVector.y;
		movement = Vector3.ClampMagnitude(movement, moveSpeed);

		bool hitGround = false;
		RaycastHit hit;
		if(_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)){
			float check = (_charController.height + _charController.radius) / 1.9f;
			hitGround = hit.distance <= check;
		}

		if(hitGround){
			_vertSpeed = (Input.GetButtonDown("Jump")) ? jumpSpeed : minFall;
		} else {
			movement.z *= 0.2f;
			movement.x *= 0.2f;
			_vertSpeed += gravity * Time.deltaTime;
			_vertSpeed = (_vertSpeed < terminalVelocity) ? terminalVelocity : _vertSpeed;
		}

		movement.y = _vertSpeed;

		movement *= Time.deltaTime;
		_charController.Move(movement);
	}
}
