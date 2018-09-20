using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerMovement : MonoBehaviour {
	
	public float walkSpeed = 5;
	public float runSpeed = 8;
	public float Gravity = 9.8f;
	public float jumpSpeed = 5f;
	public float minFall = -1.5f;
	public float terminalVelocity = -10.0f;

	private CharacterController _controller;
	private Animator _animator;
	private float _vertSpeed;


	void Start(){
		_controller = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
		_vertSpeed = 0;
	}

	void Update(){
		Vector3 movement = Vector3.zero;
		Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		dir.Normalize();
		if(Input.GetButton("Run")){
			movement.x = dir.x * runSpeed;
			movement.z = dir.y * runSpeed;
		}else{
			movement.x = dir.x * walkSpeed;
			movement.z = dir.y * walkSpeed;
		}
		if(movement == Vector3.zero){
			_animator.SetBool("IsIdle", true);
		}else{
			_animator.SetBool("IsIdle", false);
		}
		transform.forward = (movement != Vector3.zero) ? movement : transform.forward;
		
		bool hitGround = false;
		RaycastHit hit;
		if(_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)){
			float check = (_controller.height + _controller.radius) / 4.6f;
			hitGround = hit.distance <= check;
			Debug.Log(hitGround);
		}

		if(hitGround){
			if(Input.GetButtonDown("Jump")){
				_vertSpeed = jumpSpeed;
			} else {
				_vertSpeed = minFall;
				_animator.SetBool("IsJumping", false);
			}
		} else {
			//movement.z *= 0.2f;
			//movement.x *= 0.2f;
			_vertSpeed -= Gravity * 3 * Time.deltaTime;
			if(_vertSpeed < terminalVelocity){
				_vertSpeed = terminalVelocity;
			}
			_animator.SetBool("IsJumping", true);
		}
		_animator.SetFloat("Speed", movement.sqrMagnitude);
		Debug.Log(movement.sqrMagnitude);
		movement.y = _vertSpeed;
		//_animator.SetFloat("Speed",)
		_controller.Move(movement * Time.deltaTime);
	}
}
