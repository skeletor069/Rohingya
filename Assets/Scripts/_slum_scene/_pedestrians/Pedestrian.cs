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
	WaitForSeconds wait2S = new WaitForSeconds(2);
	
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
				yield return wait2S;
				if (Campfire.CAMPFIRE_STARTED)
					CampfireSitAndWalk();
			}
			animator.SetFloat(animFloatWalk, 0f);
			yield return wait2S;
		}
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
		Vector3 target = freeSeat.position;
		agent.SetDestination(target);
		while (Vector3.Distance(agent.destination, transform.position)>1f) {
			animator.SetFloat(animFloatWalk, 1.0f);
			yield return wait2S;
		}
		animator.SetFloat(animFloatWalk, 0f);
		yield return wait2S;
		transform.forward = freeSeat.forward;
		animator.SetTrigger(animSit);
		yield return new WaitForSeconds(Random.Range(10,12));
		animator.SetTrigger(animStand);
		StartCoroutine(StartRandomWalk());
		
		// wait till going there
		// sit on the seat
		// wait 10s
		// start random walk for 15s
		yield return 0;
	}
}
