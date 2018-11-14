using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PublicPlaces : MonoBehaviour {
	private static List<Vector3> publicPlaces;

	void Awake () {
		publicPlaces = new List<Vector3>();
		for(int i = 0 ; i < transform.childCount; i++)
			publicPlaces.Add(transform.GetChild(i).position);
	}

	public static Vector3 GetRandomPosition() {
		int index = Random.Range(0, publicPlaces.Count);
		return publicPlaces[index];
	}
}
