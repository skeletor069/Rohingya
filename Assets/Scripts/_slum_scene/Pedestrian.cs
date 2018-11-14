using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour {
	private NavMeshAgent agent;
	
	void Awake () {
		agent = GetComponent<NavMeshAgent>();
	}

	void Start() {
		StartCoroutine(StartRandomWalk());
	}

	IEnumerator StartRandomWalk() {
		Vector3 position = PublicPlaces.GetRandomPosition();
		transform.position = position;
		WaitForSeconds wait2S = new WaitForSeconds(2);
		while (true) {
			Vector3 target = PublicPlaces.GetRandomPosition();
			agent.SetDestination(target);
			while (Vector3.Distance(agent.destination, transform.position)>1f) {
				yield return wait2S;
			}

			yield return wait2S;
		}
	}
}
