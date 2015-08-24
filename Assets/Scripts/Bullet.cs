using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed;
    private Collider2D bulletArea = null;

    // Use this for initialization
    void Start () {
        bulletArea = transform.parent.GetComponent<Collider2D>();
	}

	void FixedUpdate () {
		transform.localPosition += -transform.right * speed * Time.deltaTime;

        if(!bulletArea.OverlapPoint(new Vector2(transform.position.x, transform.position.y))) {
            Destroy(gameObject);
        }
	}
}
