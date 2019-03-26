using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Campfire : MonoBehaviour {

	public static bool CAMPFIRE_STARTED = false;

	public Transform[] seats;
	public ParticleSystem[] fireParticles;
	public Light fireLight;
	
	
	List<Transform> freeSeats = new List<Transform>();
	List<Transform> occupiedSeats = new List<Transform>();
	private Animator animator;
	private AudioSource sound;
	private readonly int animStart = Animator.StringToHash("start");
	private readonly int animStop = Animator.StringToHash("stop");

	private void Awake() {
		animator = GetComponent<Animator>();
		sound = GetComponent<AudioSource>();
		for(int i = 0 ; i < seats.Length; i++)
			freeSeats.Add(seats[i]);
	}

	public void StartFire() {
		for (int i = 0; i < fireParticles.Length; i++)
			fireParticles[i].Play();
		animator.SetTrigger(animStart);
		sound.Play();
	}

	public void StopFire() {
		for (int i = 0; i < fireParticles.Length; i++)
			fireParticles[i].Stop();
		animator.SetTrigger(animStop);
		sound.Stop();
	}

	public Transform GetAFreeSeat() {
		if (freeSeats.Count > 0) {
			Transform seat = freeSeats[0];
			freeSeats.RemoveAt(0);
			occupiedSeats.Add(seat);
			return seat;
		}

		return null;
	}

	public void MakeFree(Transform seat) {
		if (occupiedSeats.Contains(seat)) {
			occupiedSeats.Remove(seat);
			freeSeats.Add(seat);
		}
	}
}
