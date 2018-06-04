using UnityEngine;
using System.Collections;

public class SeleccionJugadores : MonoBehaviour {

	private bool dosJugadores = false;
	private GameObject miPuntero;
	private GameObject miFondo;
	private GameObject miTitulo;

	public GameObject P1;
	public GameObject P2;

	public AudioClip cancionJuego;
	private AudioSource miSpeaker;

	private GameObject contP1;
	private GameObject contP2;

	private GameObject spawner;
	private GameObject gameOverSign;

	public float velJugadores = 1f;
	public float velDesaparecer = 5f;
	public float velFondo = 0.5f;

	public bool empezar = false;


	void Start(){ 
		miPuntero = GameObject.Find("Puntero");
		miFondo = GameObject.Find ("Fondo");
		miTitulo = GameObject.Find ("Título");
		contP1 = GameObject.Find ("ContadorP1");
		contP2 = GameObject.Find ("ContadorP2");
		spawner = GameObject.Find ("Spawner");
		gameOverSign = GameObject.Find ("gameOverSign");
		velFondo = miFondo.GetComponent<updateOffset> ().speed;
		miSpeaker = GameObject.Find ("Main Camera").GetComponent<AudioSource>();
		instanciarJugadores();
	}

	void Update(){ 
		if ((Input.GetKeyDown ("left") || Input.GetKeyDown ("right")) && !empezar) {
			dosJugadores = !dosJugadores;
			cambiarPuntero();
		}
		if (Input.GetKeyDown ("return")){ if (!empezar)Invoke("finEmpezar", 0.5f); empezar = true; }
		if (empezar) empezarPartida ();
		posicionarP1();
		if (dosJugadores) posicionarP2(); else devolverP2();
	}

	void instanciarJugadores(){
		P1 = Instantiate(P1, new Vector2(-0.25f, -1.5f), Quaternion.identity) as GameObject;
		P2 = Instantiate(P2, new Vector2(0.25f, -1.5f), Quaternion.identity) as GameObject;
		P1.name = "P1"; P2.name = "P2";
	}

	void empezarPartida(){ 
		if (!dosJugadores) desaparecer (P2.transform, velDesaparecer);
		desaparecer(this.transform, velDesaparecer);
		desaparecer(miTitulo.transform, velDesaparecer);
		miFondo.GetComponent<updateOffset>().speed = 3;
		posicionarContadores ();
		cambiarAudio ();
	}

	void cambiarAudio(){ miSpeaker.clip = cancionJuego; miSpeaker.Play(); }

	void finEmpezar(){
		P1.GetComponent<comportamientoJugador> ().enabled = true;
		if(dosJugadores) P2.GetComponent<comportamientoJugador> ().enabled = true;
		spawner.GetComponent<comportamientoSpawner>().enabled = true;
		gameOverSign.GetComponent<gameOver>().enabled = true;
		miFondo.GetComponent<updateOffset> ().speed = velFondo;
		if (!dosJugadores) { Destroy (P2.gameObject); Destroy (contP2.gameObject); }
		Destroy (miTitulo.gameObject);
		Destroy(this.gameObject);
	}

	void posicionarContadores(){ 
		if(contP1.transform.position.y > 0.9f) contP1.transform.Translate (Vector2.down * velDesaparecer * Time.deltaTime);
		if(dosJugadores) if(contP2.transform.position.y > 0.9f) contP2.transform.Translate (Vector2.down * velDesaparecer * Time.deltaTime);
	}

	void desaparecer(Transform trans, float vel){ trans.Translate (Vector2.down * vel * Time.deltaTime); }

	void cambiarPuntero(){ miPuntero.transform.position = new Vector2(miPuntero.transform.position.x * -1 , miPuntero.transform.position.y); }

	void posicionarP1(){ if(P1.transform.position.y < -0.75f) P1.transform.Translate (Vector2.up * velJugadores * Time.deltaTime); }

	void posicionarP2(){ if(P2.transform.position.y < -0.75f) P2.transform.Translate (Vector2.up * velJugadores * Time.deltaTime); }

	void devolverP2(){ if(P2.transform.position.y > -1.5f) P2.transform.Translate (Vector2.down * velJugadores * Time.deltaTime); }

}
