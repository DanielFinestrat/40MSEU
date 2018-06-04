using UnityEngine;
using System.Collections;

public class comportamientoJugador : MonoBehaviour {

	public int vidas = 3;
	public int powerUps = 0;
	public int puntuacion = 0;
	public bool firstPlayer = true;

	public GameObject[] arrayPoderes;
	private TextMesh contador;

	public float maxX = 0.7f;
	public float maxY = 0.9f;

	public float speed = 1f;

	private bool puedeDisparar = true;
	public float recoil = 0.35f;

	public bool inmortal = false;

	public AudioClip disparo;
	public GameObject explosion;

	void Start(){
		if (firstPlayer) contador = GameObject.Find ("ContadorP1/P1Contador").GetComponent<TextMesh>();
		else contador = GameObject.Find ("ContadorP2/P2Contador").GetComponent<TextMesh> ();
		invencible();
	}

	void Update () {
		if (firstPlayer) moveFirstPlayer();
		else moveSecondPlayer();
	}
		
	void moveFirstPlayer(){
		if (Input.GetKey ("a") && transform.position.x > -1 * maxX) transform.Translate (Vector2.left * speed * Time.deltaTime);
		else if (Input.GetKey ("d") && transform.position.x < maxX) transform.Translate (Vector2.right * speed * Time.deltaTime);
		if (Input.GetKey ("w") && transform.position.y < maxY) transform.Translate (Vector2.up * speed * Time.deltaTime);
		else if (Input.GetKey ("s") && transform.position.y > -1 * maxY) transform.Translate (Vector2.down * speed * Time.deltaTime);
		if (Input.GetKeyDown ("space")) disparar();
	}

	void moveSecondPlayer(){
		if (Input.GetKey ("left") && transform.position.x > -1 * maxX) transform.Translate (Vector2.left * speed * Time.deltaTime);
		else if (Input.GetKey ("right") && transform.position.x < maxX) transform.Translate (Vector2.right * speed * Time.deltaTime);
		if (Input.GetKey ("up") && transform.position.y < maxY) transform.Translate (Vector2.up * speed * Time.deltaTime);
		else if (Input.GetKey ("down") && transform.position.y > -1 * maxY) transform.Translate (Vector2.down * speed * Time.deltaTime);
		if (Input.GetKeyDown (KeyCode.KeypadEnter)) disparar();
	}

	void OnTriggerEnter2D(Collider2D other) { 
		if(other.tag == "PowerUp") aumentarPowerUps();
		if (other.tag == "Enemigo" && !inmortal) muerte();
	}

	void aumentarPowerUps(){
		if(powerUps < arrayPoderes.Length - 1) powerUps++;
	}

	void disparar(){
		if (puedeDisparar) {
			playSound (disparo);
			puedeDisparar = false;
			GameObject miBala = Instantiate (arrayPoderes [powerUps], this.transform.position, transform.rotation) as GameObject;
			if(firstPlayer) miBala.tag = "p1projectile"; else miBala.tag = "p2projectile";
			Invoke ("permitirDisparar", recoil);
		}
	}

	void muerte(){
		vidas--;
		powerUps = 0;

		Vector3 vecRespawn;
		if (firstPlayer) vecRespawn = new Vector3 (-0.25f, -0.72f, this.transform.position.z);
		else vecRespawn = new Vector3 (0.25f, -0.72f, this.transform.position.z);

		if (vidas >= 0) { 
			updateCont (); 
			GameObject Jugador = Instantiate (this.gameObject, vecRespawn, this.transform.rotation) as GameObject;
			if(firstPlayer) Jugador.name = "P1"; else Jugador.name = "P2";
		}
		Instantiate (explosion, this.transform.position, Quaternion.Inverse(this.transform.rotation));
		Destroy (this.gameObject);
	}

	void permitirDisparar(){ puedeDisparar = true; }

	void updateCont(){ contador.text = "x" + vidas; }

	void invencible(){
		inmortal = true;
		GetComponent<Renderer>().enabled = false; Invoke("normalAgain", 1.75f);
		GetComponent<Renderer>().enabled = false; Invoke("visible", 0.15f);
	}

	void normalAgain(){
		inmortal = false;
		GetComponent<Renderer>().enabled = true;
	}

	void visible(){ if(inmortal) GetComponent<Renderer>().enabled = true; Invoke("invisible", 0.15f); }
	void invisible(){ if(inmortal) GetComponent<Renderer>().enabled = false; Invoke("visible", 0.15f); }

	void playSound (AudioClip miAudio){ AudioSource miSpeaker = GetComponent<AudioSource>(); miSpeaker.clip = miAudio; miSpeaker.Play(); }

}
