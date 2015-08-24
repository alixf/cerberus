using UnityEngine;
using System.Collections;

public class UnicornMove : MonoBehaviour {

	Vector3 origin;
	float clock = 0f;
	public float amplitude = 2.5f;
	public float frequency = 1f;
	private float latestClock = 0f;

	void Start () {
	}

	void FixedUpdate () {
		clock += Time.deltaTime;
		transform.localPosition += (Mathf.Sin(clock * frequency) - Mathf.Sin(latestClock * frequency))
									* new Vector3(0, amplitude, 0);
		latestClock = clock;
	}
}

/*﻿using UnityEngine;
using System.Collections;

public class UnicornMove : MonoBehaviour {

	Vector3 origin;
	float clock = 0f;
	public float amplitude = 2.5f;
	public float frequency = 1f;
	private float latestClock = 0f;

	void Start () {
		origin = transform.localPosition;
	}

	void FixedUpdate () {
		clock += Time.deltaTime;
		transform.localPosition = origin + Mathf.Sin(clock * frequency) * new Vector3(0, amplitude, 0);
	}
}
*/
