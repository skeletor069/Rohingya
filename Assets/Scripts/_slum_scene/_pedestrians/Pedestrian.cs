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
	WaitForSeconds wait3S = new WaitForSeconds(3);
	private Vector3 homePosition;
	private GameController gameController;
	
	protected void Awake () {
		agent = GetComponent<NavMeshAgent>();
		animator = transform.GetChild(0).GetComponent<Animator>();
		gameController = GameController.GetInstance();
	}

	IEnumerator Start() {
		slumWorld = SlumWorld.GetInstance();
		Vector3 position = PublicPlaces.GetRandomPosition();
		transform.position = position;
		homePosition = PedestrianHomes.GetHomePosition();
		agent.enabled = false;
		transform.position = homePosition;
		yield return new WaitForEndOfFrame();
		agent.enabled = true;
		InitialDecision();
	}

	void InitialDecision() {
		if (IsPeakHour()) {
			if (Campfire.CAMPFIRE_STARTED)
				CampfireSitAndWalk();
			else {
				StartCoroutine(FindALocationAndGoThere());
			}
		}
		else {
			StartCoroutine(SleepRoutine());
		}
	}

	protected float GetVelocity() {
		return agent.velocity.magnitude;
	}

//	public IEnumerator StartRandomWalk() {
//		
//		
//		
//		while (true) {
//			Vector3 target = PublicPlaces.GetRandomPosition();
//			agent.SetDestination(target);
//			while (Vector3.Distance(agent.destination, transform.position)>1f) {
//				animator.SetFloat(animFloatWalk, 1.0f);
//				yield return wait1S;
//				if (Campfire.CAMPFIRE_STARTED)
//					CampfireSitAndWalk();
//			}
//			animator.SetFloat(animFloatWalk, 0f);
//			yield return wait1S;
//		}
//	}

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
			StartCoroutine(FindALocationAndGoThere());
		
	}

	protected void updateWalk(float value) {
		animator.SetFloat(animFloatWalk, value);
	}

	public void CampfireSitAndWalk() {
//		StopAllCoroutines();
		StartCoroutine(CampfireSitAndWalkRoutine());
	}

	IEnumerator CampfireSitAndWalkRoutine() {
		Transform freeSeat = slumWorld.GetAFreeSeat();
		if (freeSeat == null) {
			StartCoroutine(FindALocationAndGoThere());
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
		slumWorld.MakeSeatFree(freeSeat);
		StartCoroutine(StartWalkingForMinutes(10));
	}

	IEnumerator GoToHomeRoutine() {
		agent.SetDestination(homePosition);
		while (Vector3.Distance(transform.position, homePosition) > 1f) {
			animator.SetFloat(animFloatWalk, 1.0f);
			yield return wait1S;
		}
		animator.SetFloat(animFloatWalk, 0);
		animator.gameObject.SetActive(false);

		int randomMinute = Random.Range(30, 60);
		yield return new WaitForSeconds(randomMinute);
		
		// decide next routine

		if (IsPeakHour()) {
			animator.gameObject.SetActive(true);
			
			if (Campfire.CAMPFIRE_STARTED)
				CampfireSitAndWalk();
			else {
				StartCoroutine(FindALocationAndGoThere());
			}
		}
		else {
			StartCoroutine(SleepRoutine());
		}
	}

	public IEnumerator FindALocationAndGoThere() {
		Vector3 target = PublicPlaces.GetRandomPosition();
		agent.SetDestination(target);
		while (Vector3.Distance(agent.destination, transform.position)>1f) {
			animator.SetFloat(animFloatWalk, 1.0f);
			yield return wait1S;
		}
		animator.SetFloat(animFloatWalk, 0.0f);
		yield return wait3S;
		// decide next routine
		
		if (IsPeakHour()) {
			if (Campfire.CAMPFIRE_STARTED)
				CampfireSitAndWalk();
			else {
				if(Random.Range(0f,1f) < .5f)
					StartCoroutine(FindALocationAndGoThere());
				else
					StartCoroutine(GoToHomeRoutine());
			}
		}
		else {
			StartCoroutine(GoToHomeRoutine());
		}
	}

	IEnumerator SleepRoutine() {
		animator.gameObject.SetActive(false);
		int wakeUpMinute = Random.Range((6 * 24), (9 * 24));
		while (gameController.World.GetMinutesGone() < wakeUpMinute) {
			yield return wait3S;
		}
		animator.SetFloat(animFloatWalk, 0);
		animator.gameObject.SetActive(true);
		StartCoroutine(FindALocationAndGoThere());
	}
	

	bool IsPeakHour() {
		return gameController.World.GetHour() > 6;
	}
}
