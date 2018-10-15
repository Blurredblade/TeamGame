using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class fieldOfViewAI : MonoBehaviour {

	public NavMeshAgent agent;
	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;
	public LayerMask obstacleMask;
	public Light spot;
	public float spottedViewAngleIncrease;
	public float spottedRadiusIncrease;
	public AITarget[] targets;
	private Animator _animator;
	private GameObject player;

	private bool playerSeen;
	private float spottedVA;
	private float spottedVR;
	float tempVA;
	float tempVR;

	void Start () {
		player = GameObject.FindWithTag("Player");
		_animator = GetComponent<Animator>();
		StartCoroutine("FindPlayerWithDelay", 0.4f);
		playerSeen = false;
		spottedVA = spottedViewAngleIncrease + viewAngle;
		spottedVR = spottedRadiusIncrease + viewRadius;
		tempVA = viewAngle;
		tempVR = viewRadius;
	}
	

	void Update () {
		_animator.SetFloat("Speed", agent.velocity.magnitude);
		spot.spotAngle = viewAngle;
		spot.range = viewRadius;
		if(playerSeen){
			agent.SetDestination(player.transform.position);
			spot.color = Color.red;
			viewAngle = spottedVA;
			viewRadius = spottedVR;
		}else if(viewRadius == spottedVR && !playerSeen){ 
			spot.color = Color.white;
			
			viewAngle = tempVA;
			viewRadius = tempVR;
		}
		
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal){
		if(!angleIsGlobal){
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	void FindPlayer(){
		Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
		if(Vector3.Angle(transform.forward, dirToPlayer)<viewAngle/2){
			float disToPlayer = Vector3.Distance(player.transform.position, transform.position);
			if(!Physics.Raycast(transform.position, dirToPlayer, disToPlayer, obstacleMask) && disToPlayer <= viewRadius){
				playerSeen = true;
			}else{
				playerSeen = false;
			}
		}
	}

	IEnumerator FindPlayerWithDelay(float delay){
		while(true){
			yield return new WaitForSeconds(delay);
			FindPlayer();
		}
	}
}
