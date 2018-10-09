using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	[SerializeField] private KeyCard keyCard;
	[SerializeField] private DoorLock doorLock;
	public Animator[] doorPanels;

	bool isOpen;
	bool playerNear;
	// Use this for initialization
	void Start () {
		isOpen = false;
		playerNear = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			doorLock.playerIsNear(true);
			if(other.gameObject.GetComponent<Player>().hasKeyCard(keyCard)){
				doorLock.locked(false);
				doorPanels[0].SetBool("Open", true);
				doorPanels[1].SetBool("Open", true);
			}
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			doorLock.playerIsNear(false);
			doorPanels[0].SetBool("Open", false);
			doorPanels[1].SetBool("Open", false);
		}
	}
}
