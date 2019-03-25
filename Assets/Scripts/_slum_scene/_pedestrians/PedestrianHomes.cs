using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianHomes : MonoBehaviour {

	static List<Vector3> homePositions = new List<Vector3>();
	static int count = 0;
	private static int childCount = 0;

	void Awake () {
		for (int i = 0; i < transform.childCount; i++) {
			homePositions.Add(transform.GetChild(i).position);
		}

		childCount = transform.childCount;
	}

	public static Vector3 GetHomePosition() {
		Vector3 position = homePositions[count % childCount];
		count++;
		return position;
	}
}
