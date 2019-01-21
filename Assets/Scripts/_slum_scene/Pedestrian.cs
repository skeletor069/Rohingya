using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour {
	protected NavMeshAgent agent;
	
	protected void Awake () {
		agent = GetComponent<NavMeshAgent>();
	}

	void Start() {
		Vector3 position = PublicPlaces.GetRandomPosition();
		transform.position = position;
		StartCoroutine(StartRandomWalk());
	}

	public IEnumerator StartRandomWalk() {
		
		
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
