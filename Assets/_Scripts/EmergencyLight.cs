using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyLight : MonoBehaviour {

	public float rotSpeed = 10.0f;
	// Use this for initialization
	void Start () {
		transform.Rotate(0,Time.deltaTime * rotSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
