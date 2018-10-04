using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour {
	private Color color;
	private int number;
	public float rotSpeed = 1.5f;

	void Start(){
		number = (int)Random.Range(0, 100);
		color = Color.blue;
		gameObject.SetActive(true);
	}

	public Color getColor(){
		return this.color;
	}

	public int getNumber(){
		return this.number;
	}

	void Update(){
		transform.Rotate(0, 0, Time.deltaTime * rotSpeed * 10);
	}

	void OnTriggerEnter(Collider other){

		if(other.tag == "Player"){
			other.gameObject.GetComponent<Player>().addKeyCard(this);
			gameObject.SetActive(false);
		}
	}
}
