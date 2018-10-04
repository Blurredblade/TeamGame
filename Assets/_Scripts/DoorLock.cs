using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour {

	Renderer r;
	[SerializeField] private Material lockedMat;
	[SerializeField] private Material unlockedMat;
	private bool isLocked;

	public void locked(bool l){
		isLocked = l;
	}

	void Start(){
		r = GetComponent<Renderer>();
		isLocked = true;
	}

	void Update(){
		if(isLocked){
			r.material = lockedMat;
		}else if(!isLocked){
			r.material = unlockedMat;
		}
	}
	
}
