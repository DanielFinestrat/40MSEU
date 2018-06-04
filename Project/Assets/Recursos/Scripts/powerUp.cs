using UnityEngine;
using System.Collections;

public class powerUp : MonoBehaviour {

	public float speed = 0.75f;

	void Start() { Invoke ("doDestroy", 5f); }

	void Update () { transform.Translate (Vector2.down * speed * Time.deltaTime); }

	void doDestroy(){ Destroy (this.gameObject); }

	void OnTriggerEnter2D(Collider2D other) {  if(other.tag == "Jugador" && other.transform.parent == null) Destroy(this.gameObject); }

}
