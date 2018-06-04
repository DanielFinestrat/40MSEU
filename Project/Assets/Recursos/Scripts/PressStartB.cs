using UnityEngine;
using System.Collections;

public class PressStartB : MonoBehaviour {

	public float intervalo = 0.5f;
	public string textoBoton = "Press Start";
	public GameObject siguienteMenu;

	void Start() { visible(); }

	void Update(){ 
		if (Input.GetKeyDown ("return")){
			Instantiate (siguienteMenu);
			Destroy (this.gameObject);
		}
	}

	void visible(){ GetComponent<Renderer>().enabled = true; Invoke("invisible", intervalo);}

	void invisible(){ GetComponent<Renderer>().enabled = false; Invoke("visible", intervalo); }

}
