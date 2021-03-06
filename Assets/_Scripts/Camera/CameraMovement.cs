﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	[SerializeField] private Transform target;

	[Range(0,1)]
	public float cameraSmoothing = 1f;

	public float horizontalOffset = 25f;
	public float verticalOffset = 7f;
	public float zLimit = -30f;
	bool bound;
	float boundPos;
	private Vector3 _currentPos;

	void Start () {
		bound = false;
		_currentPos = transform.position;
	}

	public void setBound(bool bound){
		this.bound = bound;
	}
	
	void Update () {
		Vector3 targetPos = target.transform.position;
		targetPos.z -= horizontalOffset;
		targetPos.y += verticalOffset;
		if(bound){
			targetPos.x = _currentPos.x;	
		}
		targetPos.z = Mathf.Clamp(targetPos.z, -50, zLimit);
		transform.position = Vector3.Lerp(_currentPos, targetPos, cameraSmoothing);
		_currentPos = transform.position;
	}
}
