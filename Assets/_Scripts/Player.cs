using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private List<KeyCard> _keyCards;
	
	
	List<KeyCard> getKeyCards(){
		return _keyCards;
	}

	void Start(){
		_keyCards = new List<KeyCard>();
	}

	public bool hasKeyCard(KeyCard keycard){
		if(_keyCards.Find(x => x.getNumber() == keycard.getNumber()) != null)
			return true;
		return false;
	}

	public void addKeyCard(KeyCard keyCard){
		_keyCards.Add(keyCard);
	}




}
