using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	[SerializeField] private KeyCard keyCard;
	[SerializeField] private DoorLock doorLock;

	bool Locked;
	// Use this for initialization
	void Start () {
		Locked = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

		if(other.tag == "Player" && other.gameObject.GetComponent<Player>().hasKeyCard(keyCard)){
			Debug.Log("UNLOCKED!");
			doorLock.locked(false);
		}
	}
}
