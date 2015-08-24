using UnityEngine;
using System.Collections;

public class CustomCamera : MonoBehaviour {
	public void shake(float duration, float intensity) {
		StartCoroutine(shakeCoroutine(duration, intensity));
	}

	IEnumerator shakeCoroutine(float duration, float intensity) {
		var origin = transform.localPosition;
		var clock = 0f;
		while(clock < duration) {
			clock += Time.deltaTime;
			var offset = Mathf.Sin(clock*100) * intensity;
			transform.localPosition = origin + new Vector3(0,offset,0);
			yield return null;
		}
		transform.localPosition = origin;
	}
}
