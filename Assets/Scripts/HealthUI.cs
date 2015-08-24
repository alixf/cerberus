using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	public Health target;
	private Image fill;

	void Start() {
		fill = transform.Find("Fill").GetComponent<Image>();
	}

	void Update () {
		fill.fillAmount = target.health / target.maxHealth;
	}
}
