using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour {

	Renderer r;
	[SerializeField] private Material lockedMat;
	[SerializeField] private Material unlockedMat;
	[SerializeField] private Material doorFrameMat;
	private bool isLocked;
	private bool playerNear;

	public void locked(bool l){
		isLocked = l;
	}

	public bool locked(){
		return isLocked;
	}
	public void playerIsNear(bool p){
		playerNear = p;
	}

	void Start(){
		r = GetComponent<Renderer>();
		isLocked = true;
		playerNear = false;
	}

	void Update(){
		if(playerNear && isLocked){
			r.material = lockedMat;
		}else if(playerNear && !isLocked){
			r.material = unlockedMat;
		}else{
			r.material = doorFrameMat; 
		}
	}
	
}
