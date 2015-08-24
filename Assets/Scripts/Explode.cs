using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	void Start () {
		foreach(Rigidbody2D child in GetComponentsInChildren<Rigidbody2D>()) {
			var force = new Vector2(Random.value * -3, Random.value * 10);
			child.AddForce(force, ForceMode2D.Impulse);
			child.AddTorque(Random.value * 10, ForceMode2D.Impulse);
		}
		GameObject.Find("boom").GetComponent<AudioSource>().Play();
	}

	void Update () {
	}
}
