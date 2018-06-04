using UnityEngine;
using System.Collections;

public class comportamientoEnemigo : MonoBehaviour {

	public int enemigo = 0;
	public int vida = 1;
	public int puntos = 100;
	public float speed = 0f;

	public GameObject bala;
	public GameObject powerUp;

	private bool direccion = false;
	private float initX = 0;

	public AudioClip disparo;
	public GameObject explosion;

	void Start(){
		initX = transform.position.x;
		disparar ();
	}

	void Update(){
		if (enemigo == 1) comp1 ();
		else if (enemigo == 2) comp2 ();
		else if (enemigo == 3) comp3 ();
		else  comp4 ();
		if (transform.position.y < -2) Destroy (this.gameObject);
	}

	void comp1(){ 
		transform.Translate (Vector2.down * speed * Time.deltaTime);
	}

	void comp2(){
		if (transform.position.x < 0) direccion = true;
		transform.Translate (Vector2.down * speed * Time.deltaTime);
		if (transform.position.y < 0 && direccion) transform.Translate (Vector2.right * speed * Time.deltaTime);
		else if (transform.position.y < 0 && !direccion) transform.Translate (Vector2.left * speed * Time.deltaTime);
	}

	void comp3(){
		transform.Translate (Vector2.down * speed * Time.deltaTime);
		if(transform.position.x > initX + 0.3f) direccion = false;
		else if(transform.position.x < initX - 0.3f) direccion = true;
		if (direccion) transform.Translate (Vector2.right * speed * Time.deltaTime);
		else if (!direccion) transform.Translate (Vector2.left * speed * Time.deltaTime);
	}

	void comp4(){
		if(transform.position.y > 0.7) transform.Translate (Vector2.down * speed * Time.deltaTime);
		if(transform.position.x > 0.7) direccion = false;
		else if(transform.position.x < -0.7) direccion = true;
		if (direccion) transform.Translate (Vector2.right * speed * Time.deltaTime);
		else if (!direccion) transform.Translate (Vector2.left * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {
		string myTag = "";
		if (other.transform.parent != null) myTag = other.transform.parent.tag;
		if (myTag == "p1projectile" || myTag == "p2projectile") { aumentarPuntos (myTag); Destroy (other.gameObject); }
		if (other.tag == "Jugador") disminuirVida();
	}

	void disminuirVida(){
		vida--;
		if (vida <= 0) muerte();
	}

	void aumentarPuntos(string myTag){
		if (myTag == "p1projectile") {
			TextMesh myText = GameObject.Find ("ContadorP1/P1Puntuacion").GetComponent<TextMesh> ();
			myText.text = (int.Parse(myText.text) + puntos).ToString();
		}
		if (myTag == "p2projectile") {
			TextMesh myText = GameObject.Find ("ContadorP2/P2Puntuacion").GetComponent<TextMesh> ();
			myText.text = (int.Parse(myText.text) + puntos).ToString();
		}
	}
	
	void muerte(){
		Instantiate (explosion, this.transform.position, this.transform.rotation);
		if(Random.Range(0, 8) == 0) Instantiate (powerUp, this.transform.position, this.transform.rotation);
		Destroy (this.gameObject);
	}

	void disparar(){ playSound (disparo); Instantiate (bala, this.transform.position, this.transform.rotation); Invoke ("disparar", Random.Range(1f, 3f)); }

	void playSound (AudioClip miAudio){ AudioSource miSpeaker = GetComponent<AudioSource>(); miSpeaker.clip = miAudio; miSpeaker.Play(); }

}
