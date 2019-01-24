using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour {
	protected NavMeshAgent agent;
	private Animator animator;
	private int animWalk = Animator.StringToHash("walk");
	
	protected void Awake () {
		agent = GetComponent<NavMeshAgent>();
		animator = transform.GetChild(0).GetComponent<Animator>();
	}

	void Start() {
		Vector3 position = PublicPlaces.GetRandomPosition();
		transform.position = position;
		StartCoroutine(StartRandomWalk());
	}

	protected float GetVelocity() {
		return agent.velocity.magnitude;
	}

	public IEnumerator StartRandomWalk() {
		
		
		WaitForSeconds wait2S = new WaitForSeconds(2);
		while (true) {
			Vector3 target = PublicPlaces.GetRandomPosition();
			agent.SetDestination(target);
			while (Vector3.Distance(agent.destination, transform.position)>1f) {
				animator.SetFloat(animWalk, 1.0f);
				yield return wait2S;
			}
			animator.SetFloat(animWalk, 0f);
			yield return wait2S;
		}
	}

	protected void updateWalk(float value) {
		animator.SetFloat(animWalk, value);
	}
}
