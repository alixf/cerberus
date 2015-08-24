using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class Unicorn : MonoBehaviour {
	Health health;

    public GameObject destroyPrefab;

    void Start() {
		health = GetComponent<Health>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy(collider.gameObject);
		health.health--;

		if(health.health <= 0) {
			Destroy(gameObject);
			if(destroyPrefab != null) {
				var tmp = Instantiate(destroyPrefab, transform.position, transform.rotation);
				Destroy(tmp, 2f);
			}
			Camera.main.GetComponent<CustomCamera>().shake(0.5f, 0.1f);
		}
	}

	void animationEnd() {
		GetComponent<Animator>().enabled = false;
		GetComponent<Collider2D>().enabled = true;
	}

	void animationBegin() {
		GetComponent<Collider2D>().enabled = false;
	}
}
