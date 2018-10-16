using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerMovement : MonoBehaviour {
	
	public float walkSpeed = 5;
	public float runSpeed = 8;
	public float Gravity = 9.8f;
	public float jumpSpeed = 5f;
	public float minFall = -1.5f;
	public float terminalVelocity = -10.0f;
	public float pushForce = 3.0f;

	public float rotSpeed = 15.0f;
	[SerializeField] private Text deathText;

	private CharacterController _controller;
	private Animator _animator;
	private float _vertSpeed;
	private ControllerColliderHit _contact;
	private bool isGrabbing;
	private bool dead;

	void Start(){
		_controller = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
		_vertSpeed = 0;
		isGrabbing = false;
		deathText.text = "";
	}

	void Update(){
		if(!dead){
			Vector3 movement = Vector3.zero;
			float horInput = Input.GetAxis("Horizontal");
			float vertInput = Input.GetAxis("Vertical");
			if(horInput != 0 || vertInput != 0){	
				if(Input.GetButton("Run")){
					movement.x = horInput * runSpeed;
					movement.z = vertInput * runSpeed;
					movement = Vector3.ClampMagnitude(movement, runSpeed);
				}else{
					movement.x = horInput * walkSpeed;
					movement.z = vertInput * walkSpeed;
					movement = Vector3.ClampMagnitude(movement, walkSpeed);
				}
				Quaternion dir = Quaternion.LookRotation(movement);
				transform.rotation = Quaternion.Lerp(transform.rotation, dir, rotSpeed * Time.deltaTime);
			}

			if(movement == Vector3.zero){
				_animator.SetBool("IsIdle", true);
			}else{
				_animator.SetBool("IsIdle", false);
			}

			
			bool hitGround = false;
			RaycastHit hit;
			if(_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)){
				float check = (_controller.height + _controller.radius) / 1.9f;
				hitGround = hit.distance <= check;
			}

			if(hitGround){
				if(Input.GetButtonDown("Jump")){
					_vertSpeed = jumpSpeed;
				} else {
					_vertSpeed = minFall;
					_animator.SetBool("IsJumping", false);
				}
			} else {
				_vertSpeed -= Gravity * 5 * Time.deltaTime;
				if(_vertSpeed < terminalVelocity){
					_vertSpeed = terminalVelocity;
				}

				if(_controller.isGrounded){
					if(Vector3.Dot(movement, _contact.normal) < 0){
						movement = _contact.normal * walkSpeed;
					}else{
						movement += _contact.normal * walkSpeed;
					}
				}
				_animator.SetBool("IsJumping", true);
			}
			_animator.SetFloat("Speed", movement.sqrMagnitude);

			movement.y = _vertSpeed;

			_controller.Move(movement * Time.deltaTime);
		}else{
			_animator.SetBool("Dead", true);
			StartCoroutine("playerDeath");
		}
	}

	IEnumerator playerDeath(){
		deathText.text = "You have been infected!! Restarting...";
		yield return new WaitForSeconds(3.5f);
		SceneManager.LoadScene("Main");
	}

	public void isDead(bool dead){
		this.dead = dead;
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		_contact = hit;

		Rigidbody body = hit.collider.attachedRigidbody;
		if(body != null && !body.isKinematic){
			body.velocity = hit.moveDirection * pushForce;
		}
	}

}
