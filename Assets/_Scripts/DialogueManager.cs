﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	public Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		
	}

	public void StartDialogue(Dialogue dialogue){
		Debug.Log("Starting dialogue");
	}
	
}
