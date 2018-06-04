using UnityEngine;
using System.Collections;

public class comportamientoSpawner : MonoBehaviour {

	public GameObject[] enemigos;
	private bool direccion = false;

	public float speed = 1f;
	public float spawnMin = 2f;
	public float spawnMax = 4f;


	void Start(){
		Invoke ("spawn", Random.Range(spawnMin, spawnMax));
		Invoke ("spawn", Random.Range(spawnMin*10, spawnMax*10));
	}

	void Update(){ movimiento(); }

	void movimiento(){
		if(transform.position.x > 0.7) direccion = false;
		else if(transform.position.x < -0.7) direccion = true;
		if (direccion) transform.Translate (Vector2.right * speed * Time.deltaTime);
		else if (!direccion) transform.Translate (Vector2.left * speed * Time.deltaTime);
	}

	void spawn(){
		Instantiate (enemigos[Random.Range(0,enemigos.Length-1)], this.transform.position, this.transform.rotation);
		Invoke ("spawn", Random.Range (spawnMin, spawnMax));
	}

	void spawnBoss(){
		Instantiate (enemigos[enemigos.Length-1], this.transform.position, this.transform.rotation);
		Invoke ("spawn", Random.Range(spawnMin*10, spawnMax*10));
	}

}
