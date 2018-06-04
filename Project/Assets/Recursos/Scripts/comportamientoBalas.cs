using UnityEngine;
using System.Collections;

public class comportamientoBalas : MonoBehaviour {

	public bool esJugador = true;
	public bool esMisil = false;
	public bool derecha = false;

	public float vel = 2;

	void Update(){
		if (esJugador && !esMisil) { compA(); }
		else if (esJugador && esMisil && derecha) { compB(); }
		else if (esJugador && esMisil && !derecha) { compC(); }

		if (!esJugador && esMisil) { compD(); }
		else if (!esJugador && !esMisil) { compE(); }

		destruir();
	}

	void destruir(){
		if(this.transform.position.y > 1.5f) Destroy(transform.parent.gameObject);
		if(this.transform.position.y < -1.5f) Destroy(this.gameObject);
	}

	void compA(){ transform.Translate (Vector2.up * vel * Time.deltaTime); }
	void compB(){ transform.Translate (Vector2.down * vel * Time.deltaTime); }
	void compC(){ transform.Translate (Vector2.up * vel * Time.deltaTime); }
	void compD(){ transform.Translate (Vector2.down * vel/2.3f * Time.deltaTime); }
	void compE(){ transform.Translate (Vector2.down * vel/2f * Time.deltaTime); }

}
