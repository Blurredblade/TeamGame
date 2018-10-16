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
	public Transform[] targets;
	public float deathDistance = 0.5f;
	private Animator _animator;
	private GameObject player;

	private bool playerSeen;
	private int destPoint = 0;
	private float spottedVA;
	private float spottedVR;
	float tempVA;
	float tempVR;

	void Start () {
		player = GameObject.FindWithTag("Player");
		_animator = GetComponent<Animator>();
		StartCoroutine("FindPlayerWithDelay", 0.3f);
		playerSeen = false;
		spottedVA = spottedViewAngleIncrease + viewAngle;
		spottedVR = spottedRadiusIncrease + viewRadius;
		tempVA = viewAngle;
		tempVR = viewRadius;
		agent.autoBraking = false;

		GotoNextTarget();
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
			agent.speed = 4.5f;
		}else if(viewRadius == spottedVR && !playerSeen){ 
			spot.color = Color.white;
			viewAngle = tempVA;
			viewRadius = tempVR;
			agent.speed = 3;
		}
		if(!agent.pathPending && agent.remainingDistance < 0.5f)
				GotoNextTarget();
	}

	void GotoNextTarget(){
		if(targets.Length == 0)
			return;

		agent.destination = targets[destPoint].position;

		destPoint = (destPoint + 1) % targets.Length;

	}

	IEnumerator pausePatrol(){

		yield return new WaitForSeconds(0.5f);

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
			if(disToPlayer <= deathDistance){
				GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isDead(true);
				agent.isStopped = true;
				_animator.SetBool("Attack", true);
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
