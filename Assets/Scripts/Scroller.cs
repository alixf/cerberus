using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {
	public float threshold = 1920/100;
	public float speed = 1f;

	void Update () {
		transform.Translate(-speed * Time.deltaTime, 0, 0);
		while(transform.localPosition.x > threshold) {
			transform.Translate(-threshold, 0, 0);
		}
		while(transform.localPosition.x < -threshold) {
			transform.Translate(threshold, 0, 0);
		}
	}
}
