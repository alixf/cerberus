using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public GameObject monster;
	private Health monsterHealth;
	public GameObject gameOver;
	private CustomCamera theCamera;
	private int healthSegment = 1;

	public int storyStep = 0;
	public Text text;

	public GameObject unicornprefab;
	private List<GameObject> enemies = new List<GameObject>();
	private int latestStoryStep = -1;
	private int retryStoryStep = 0;
	private float fixedDeltaTime;
	public GameObject cerberusPrefab;
	public GameObject unicorn2prefab;
	public GameObject unicorn3prefab;
	public GameObject unicorn4prefab;
	public GameObject unicorn5prefab;

	void Start () {
		monsterHealth = monster.GetComponent<Health>();
		theCamera = Camera.main.GetComponent<CustomCamera>();
		StartCoroutine(stepStory());
		fixedDeltaTime = Time.fixedDeltaTime;
	}

	public void Quit() {
		Application.Quit();
	}

	public void Retry() {
		gameOver.SetActive(false);
		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.timeScale * fixedDeltaTime;
		latestStoryStep = -1;
		monsterHealth.health = monsterHealth.maxHealth;

		storyStep = retryStoryStep;
		print(storyStep);
		stepStory();

		for (int i = 0; i < enemies.Count; i++) {
			if(enemies[i] != null) {
				Destroy(enemies[i]);
			}
			enemies.RemoveAt(i);
		}

		var bulletArea = GameObject.Find("bulletArea");
		foreach(Transform bullet in bulletArea.transform) {
			Destroy(bullet.gameObject);
		}

		StartCoroutine(displayText(""));
	}

	void Update () {
		if(healthSegment < 4 && (monsterHealth.health/monsterHealth.maxHealth) < 1 - (healthSegment * 0.25)) {
			healthSegment++;
			theCamera.shake(0.33f, 0.1f);
		}
		if(monsterHealth.health <= 0) {
			gameOver.SetActive(true);
			Time.timeScale = 0.1f;
			Time.fixedDeltaTime = Time.timeScale * fixedDeltaTime;
		}

		for (int i = 0; i < enemies.Count; i++) {
			if(enemies[i] == null)
				enemies.RemoveAt(i);
		}

		if(enemies.Count == 0 && latestStoryStep != storyStep) {
			StartCoroutine(stepStory());
		}

		if(storyStep == 2) {
			if(Input.GetButton("Fire1")) {
				triggerMove = true;
				storyStep++;
			}
			if(triggerMove && !enemies[0].GetComponent<Animator>().enabled) {
				StartCoroutine(MoveCoroutine(enemies[0].transform, new Vector3(0f, 2f, 0f), 0.65f, 0f));
				StartCoroutine(MoveCoroutine(enemies[0].transform, new Vector3(0f, -2f, 0f), 1.0f, 0.65f));
				StartCoroutine(enableMoveAfterDelay(enemies[0], 1.65f));
				StartCoroutine(displayText("oh oh oh, this one seems more awake than its friend !"));
			}
		}
	}
	private bool triggerMove = false;

	public IEnumerator enableMoveAfterDelay(GameObject unicorn, float delay) {
		yield return new WaitForSeconds(delay);
		if(unicorn != null)
			unicorn.GetComponent<UnicornMove>().enabled = true;
	}

	public IEnumerator stepStory() {
		latestStoryStep = storyStep;
		switch(storyStep) {
			case 0 :
				retryStoryStep = storyStep;
				yield return new WaitForSeconds(1.5f);
				enemies.Add(Instantiate(unicornprefab));
				StartCoroutine(displayText("Press [Q] to bring death to this unicorn !"));
				storyStep++;
			break;
			case 1 :
				retryStoryStep = storyStep;
				monster.transform.Find("weapon1").gameObject.SetActive(false);
				yield return new WaitForSeconds(1.5f);
				GameObject e0 = Instantiate(unicornprefab);
				e0.GetComponent<Collider2D>().enabled = false;
				enemies.Add(e0);
				StartCoroutine(displayText("Here comes another one !"));
				storyStep++;
				monster.transform.Find("weapon1").gameObject.SetActive(true);
			break;
			case 3 :
				retryStoryStep = storyStep;
				StartCoroutine(displayText("More are coming, we'll need to get serious now !\nUse [Q], [W] and [E] to fire"));
				Destroy(monster);
				monster = Instantiate(cerberusPrefab);
				monsterHealth = monster.GetComponent<Health>();
				GameObject.Find("HealthUI").GetComponent<HealthUI>().target = monsterHealth;

				yield return new WaitForSeconds(1.5f);
				GameObject e1 = (GameObject) Instantiate(unicorn2prefab, new Vector3(-11f,2.5f,0f), Quaternion.identity);
				enemies.Add(e1);
				StartCoroutine(MoveCoroutine(e1.transform, new Vector3(3.5f, 0f, 0f), 0.5f, 0f));

				yield return new WaitForSeconds(1f);
				GameObject e2 = (GameObject) Instantiate(unicorn2prefab, new Vector3(-11f,0f,0f), Quaternion.identity);
				enemies.Add(e2);
				StartCoroutine(MoveCoroutine(e2.transform, new Vector3(3.5f, 0f, 0f), 0.5f, 0f));

				yield return new WaitForSeconds(1f);
				GameObject e3 = (GameObject) Instantiate(unicorn2prefab, new Vector3(-11f,-2.5f,0f), Quaternion.identity);
				enemies.Add(e3);
				StartCoroutine(MoveCoroutine(e3.transform, new Vector3(3.5f, 0f, 0f), 0.5f, 0f));
				storyStep++;
			break;

			case 4 :
				retryStoryStep = storyStep;

				yield return new WaitForSeconds(1.5f);
				GameObject e4 = (GameObject) Instantiate(unicorn3prefab, new Vector3(-11.0f,1.75f,0f), Quaternion.identity);
				enemies.Add(e4);
				StartCoroutine(MoveCoroutine(e4.transform, new Vector3(3.5f, 0f, 0f), 0.5f, 0f));

				yield return new WaitForSeconds(0.5f);
				GameObject e5 = (GameObject) Instantiate(unicorn3prefab, new Vector3(-11.5f,-1.75f,0f), Quaternion.identity);
				enemies.Add(e5);
				StartCoroutine(MoveCoroutine(e5.transform, new Vector3(4.5f, 0f, 0f), 1.0f, 0f));

				storyStep++;
				break;


			case 5 :
				retryStoryStep = storyStep;

				yield return new WaitForSeconds(1.5f);
				GameObject e6 = (GameObject) Instantiate(unicorn4prefab, new Vector3(-11f,2.5f,0f), Quaternion.identity);
				enemies.Add(e6);
				StartCoroutine(MoveCoroutine(e6.transform, new Vector3(3.5f, 0f, 0f), 0.5f, 0f));

				yield return new WaitForSeconds(0.5f);
				GameObject e7 = (GameObject) Instantiate(unicorn4prefab, new Vector3(-11f,-2.5f,0f), Quaternion.identity);
				enemies.Add(e7);
				StartCoroutine(MoveCoroutine(e7.transform, new Vector3(4.0f, 0f, 0f), 0.5f, 0f));

				yield return new WaitForSeconds(0.5f);
				GameObject e8 = (GameObject) Instantiate(unicorn4prefab, new Vector3(-11f,2.5f,0f), Quaternion.identity);
				enemies.Add(e8);
				StartCoroutine(MoveCoroutine(e8.transform, new Vector3(4.5f, 0f, 0f), 0.5f, 0f));

				yield return new WaitForSeconds(0.5f);
				GameObject e9 = (GameObject) Instantiate(unicorn4prefab, new Vector3(-11f,-2.5f,0f), Quaternion.identity);
				enemies.Add(e9);
				StartCoroutine(MoveCoroutine(e9.transform, new Vector3(5.0f, 0f, 0f), 0.5f, 0f));

				storyStep++;
				break;

			case 6 :
				StartCoroutine(displayText("Congratulations, you're a true monster !\nThanks for playing !"));
				break;

			default : break;
		}
	}

	public IEnumerator displayText(string str) {
		float clock = 0f;
		text.text = "";
		float delay = 0.01f;
		while(text.text.Length < str.Length) {

			clock += Time.deltaTime;
			while(clock >= delay && text.text.Length < str.Length) {
				text.text = str.Substring(0, text.text.Length+1);
				clock -= delay;
			}
			yield return null;
		}
	}

	public IEnumerator MoveCoroutine(Transform target, Vector3 offset, float duration, float delay) {
		if(delay > 0f) {
			yield return new WaitForSeconds(delay);
		}
		if(target != null) {
			var origin = target.transform.position;

			float clock = 0f;
			while(clock < duration && target != null) {
				clock += Time.deltaTime;
				target.position = new Vector3(Mathf.SmoothStep(origin.x, origin.x + offset.x, clock / duration),
											  Mathf.SmoothStep(origin.y, origin.y + offset.y, clock / duration),
											  Mathf.SmoothStep(origin.z, origin.z + offset.z, clock / duration));
				yield return null;
			}
			if(target != null)
				transform.position = origin+offset;
		}
	}
}
