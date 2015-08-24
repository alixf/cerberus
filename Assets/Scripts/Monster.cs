using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
	Health health;

	void Start() {
		health = GetComponent<Health>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy(collider.gameObject);
		health.health--;
	}
}
