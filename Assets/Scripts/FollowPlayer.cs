using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform targetTransform;
	public Vector3 offset;
	public float followSpeed = 3.5f;

	// Use this for initialization
	void Start () {
		offset = new Vector3(-14.4f, 21.8f, -14.9f);
	}

	public void Initiate() {
		transform.position = targetTransform.position + offset;
	}

	public void SetTarget(Transform targetTransform) {
		this.targetTransform = targetTransform;
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, targetTransform.position + offset,
			Time.deltaTime * followSpeed);
	}
}
