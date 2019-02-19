using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour {
	protected NavMeshAgent agent;
	private Animator animator;
	private int animFloatWalk = Animator.StringToHash("walk");
	private int animSit = Animator.StringToHash("sit");
	private int animStand = Animator.StringToHash("idle");
	private SlumWorld slumWorld;
	WaitForSeconds wait1S = new WaitForSeconds(1);
	
	protected void Awake () {
		agent = GetComponent<NavMeshAgent>();
		animator = transform.GetChild(0).GetComponent<Animator>();
	}

	void Start() {
		slumWorld = SlumWorld.GetInstance();
		Vector3 position = PublicPlaces.GetRandomPosition();
		transform.position = position;
		StartCoroutine(StartRandomWalk());
	}

	protected float GetVelocity() {
		return agent.velocity.magnitude;
	}

	public IEnumerator StartRandomWalk() {
		
		
		
		while (true) {
			Vector3 target = PublicPlaces.GetRandomPosition();
			agent.SetDestination(target);
			while (Vector3.Distance(agent.destination, transform.position)>1f) {
				animator.SetFloat(animFloatWalk, 1.0f);
				yield return wait1S;
				if (Campfire.CAMPFIRE_STARTED)
					CampfireSitAndWalk();
			}
			animator.SetFloat(animFloatWalk, 0f);
			yield return wait1S;
		}
	}

	IEnumerator StartWalkingForMinutes(float minutes) {
		float accum = 0;
		while (accum < minutes) {
			Vector3 target = PublicPlaces.GetRandomPosition();
			agent.SetDestination(target);
			while (accum < minutes && Vector3.Distance(agent.destination, transform.position)>1f) {
				animator.SetFloat(animFloatWalk, 1.0f);
				accum += 1;
				yield return wait1S;
//				if (Campfire.CAMPFIRE_STARTED)
//					CampfireSitAndWalk();
			}
			animator.SetFloat(animFloatWalk, 0f);
			accum += 1;
			yield return wait1S;
		}
		if (Campfire.CAMPFIRE_STARTED)
			CampfireSitAndWalk();
		else 
			StartCoroutine(StartRandomWalk());
		
	}

	protected void updateWalk(float value) {
		animator.SetFloat(animFloatWalk, value);
	}

	public void CampfireSitAndWalk() {
		StopAllCoroutines();
		StartCoroutine(CampfireSitAndWalkRoutine());
	}

	IEnumerator CampfireSitAndWalkRoutine() {
		Transform freeSeat = slumWorld.GetAFreeSeat();
		if (freeSeat == null) {
			StartCoroutine(StartRandomWalk());
			yield break;
		}

		Vector3 target = freeSeat.position;
		agent.SetDestination(target);
		while (Vector3.Distance(agent.destination, transform.position)>1f) {
			animator.SetFloat(animFloatWalk, 1.0f);
			if (!Campfire.CAMPFIRE_STARTED) {
				StartCoroutine(StartWalkingForMinutes(5));
				yield break;
			}

			yield return wait1S;
		}
		animator.SetFloat(animFloatWalk, 0f);
		transform.forward = freeSeat.forward;
		animator.SetTrigger(animSit);
		yield return new WaitForSeconds(Random.Range(10,12));
		animator.SetTrigger(animStand);
		yield return wait1S;
		StartCoroutine(StartWalkingForMinutes(10));
	}
}
