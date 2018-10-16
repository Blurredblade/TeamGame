using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	[Range(0,1)]
	public float MaxVolume = 0.6f;
	public int fadeSeconds = 3;
	private AudioSource _audio;
	void Start(){
		_audio = GetComponent<AudioSource>();
	}
	void FixedUpdate(){
		if(_audio.volume < MaxVolume){
			_audio.volume = _audio.volume + (Time.deltaTime / (fadeSeconds + 1));
		}else{
			Destroy(this);
		}
	}
}
