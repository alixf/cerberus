using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject bulletPrefab;

	public bool auto = false;
	public string keyToFire = "";
	public bool inverted = false;

	public float cooldown;
	private float shotRotation;
	public float shotRotationOrigin;
	public float shotRotationDelta;

	public int bulletsPerShot;
	public float bulletRotOrigin;
	public float bulletRotDelta;
	public Vector3 bulletPosOrigin;
	public Vector3 bulletPosDelta;

	public float bulletSpeed;
    private float clock;

	private Transform bulletArea;

    // Use this for initialization
    void Start () {
		bulletArea = GameObject.Find("bulletArea").transform;
		shotRotation = shotRotationOrigin;
	}

	// Update is called once per frame
	void Update () {
		clock += Time.deltaTime;
		if((auto || (keyToFire != "" && Input.GetButton(keyToFire))) && clock > cooldown) {
			shoot();
			clock = 0f;
		}
	}

	void shoot () {
		float rotation = shotRotation + transform.eulerAngles.z + bulletRotOrigin;
		Vector3 position = transform.position + bulletPosOrigin;

		for (int i = 0; i < bulletsPerShot; i++) {
			var bullet = Instantiate(bulletPrefab) as GameObject;
			bullet.transform.SetParent(bulletArea);
			bullet.GetComponent<Bullet>().speed = bulletSpeed;
			bullet.transform.Rotate(new Vector3(0, 0, rotation));
			bullet.transform.localPosition = position;

			rotation += bulletRotDelta;
			position += bulletPosDelta;

			if(inverted) {
				bullet.transform.Rotate(new Vector3(0, 0, 180));
				bullet.transform.localScale = new Vector3(-bullet.transform.localScale.x,
														  -bullet.transform.localScale.y,
														  bullet.transform.localScale.z);
			}
		}

		shotRotation += shotRotationDelta;

		if(inverted)
			GameObject.Find("fire1").GetComponent<AudioSource>().Play();
		else
			GameObject.Find("fire2").GetComponent<AudioSource>().Play();
	}
}
