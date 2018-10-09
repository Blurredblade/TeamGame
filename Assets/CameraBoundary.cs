using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundary : MonoBehaviour {
	[SerializeField] private CameraMovement camera;
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			camera.setBound(true);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			camera.setBound(false);
		}
	}
}
