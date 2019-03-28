using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Hero3d : MonoBehaviour {
	public Animator animator;
	private NavMeshAgent agent;
	private float moveSpeed = 4;
	private Vector3 forward;
	private Vector3 right;
	private bool movementActive = true;
	private Transform canvas;
	private Narrator narrator;
	WaitForSeconds waitOne = new WaitForSeconds(1);
	WaitForSeconds waitTwo = new WaitForSeconds(2);
	WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
	private bool waitForSkippingNarration = false;
	private int animIdle = Animator.StringToHash("idle");
	private int animWalk = Animator.StringToHash("walk");
	private int animMove = Animator.StringToHash("move");
	private int animDie = Animator.StringToHash("die");
	private int animSearch = Animator.StringToHash("search");
	

	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		canvas = transform.GetChild(1);
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);
		right = Quaternion.Euler(0, 90, 0) * forward;
		narrator = new Narrator(canvas.GetChild(0).gameObject, canvas.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>());
	}
	
	void Update () {
		
		if (movementActive && IsMovementKeyPressed()){
			Move();
			animator.SetFloat(animWalk,1);
		}
		else if(movementActive){
			animator.SetFloat(animWalk,0);
		}

		if (waitForSkippingNarration && Input.GetKeyDown(KeyCode.Return))
			waitForSkippingNarration = false;

		canvas.forward =  canvas.position - Camera.main.transform.position;
	}

	public void ActivateAgent() {
		agent.enabled = true;
	}

	public bool IsMovementActive() {
		return movementActive;
	}

	bool IsMovementKeyPressed() {
		return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
		       Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow);
	}

	void Move(){
		Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
		Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
		transform.forward = Vector3.Normalize(rightMovement + upMovement);
		if (upMovement.magnitude != 0 && rightMovement.magnitude != 0){
			transform.position += (upMovement + rightMovement) * .75f;
		}
		else{
			transform.position += upMovement;
			transform.position += rightMovement;
		}
	}

	public void SetMovementActive(bool movementActive){
		this.movementActive = movementActive;
		if(!movementActive)
			animator.SetFloat(animWalk, 0);
	}

	public void Speak(string text) {
		StartCoroutine(SpeakRoutine(text, true));
	}

	public IEnumerator SpeakRoutine(string text, bool auto) {
		narrator.ShowText(text);
		yield return waitTwo;
		if(auto)
			narrator.Hide();
		else {
			// enable btn hint
			waitForSkippingNarration = true;
			while (waitForSkippingNarration)
				yield return endOfFrame;
			narrator.Hide();
		}

		yield return waitOne;
	}
	
	public IEnumerator GoToTarget(Vector3 targetPosition) {
		agent.SetDestination(targetPosition);
		while (Vector3.Distance(transform.position, targetPosition) > .8f) {
			animator.SetFloat(animWalk,1);
			Debug.Log(Vector3.Distance(transform.position, targetPosition));
			yield return endOfFrame;
		}
		animator.SetFloat(animWalk,0);
		agent.SetDestination(transform.position);
		agent.velocity = Vector3.zero;
	}

	public void AnimDie() {
		animator.SetTrigger(animDie);
		SetMovementActive(false);
	}

	public void AnimSearch() {
		animator.SetTrigger(animSearch);
	}

	public void AnimMove() {
		animator.SetTrigger(animMove);
	}


}
