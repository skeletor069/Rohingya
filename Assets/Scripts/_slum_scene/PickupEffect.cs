
using UnityEngine;
using UnityEngine.UI;

public class PickupEffect : MonoBehaviour {
	private Animator animator;
	private int animShow = Animator.StringToHash("show");
	private Image image;
	
	void Awake () {
		animator = transform.GetChild(0).GetComponent<Animator>();
		image = transform.GetChild(0).GetComponent<Image>();
	}

	public void ShowEffect(Vector3 position, Sprite sprite) {
		image.sprite = sprite;
		transform.position = position;
		animator.SetTrigger(animShow);
	}
}
