using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hero3d : MonoBehaviour
{
	private NavMeshAgent agent;
	private float moveSpeed = 3;
	private Vector3 forward;
	private Vector3 right;
	private bool movementActive = true;

	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);
		right = Quaternion.Euler(0, 90, 0) * forward;
	}
	
	void Update () {
		if (movementActive && Input.anyKey){
			Move();
		}
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
	}
}
