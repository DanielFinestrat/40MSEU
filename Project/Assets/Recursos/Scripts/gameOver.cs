using UnityEngine;
using System.Collections;

public class gameOver : MonoBehaviour {

	private bool partidaAcabada = false;

	private GameObject p1 = null;
	private GameObject p2 = null;

	void Update () {
		p1 = GameObject.Find ("P1");
		p2 = GameObject.Find ("P2");
		if (!partidaAcabada && p1 == null && p2 == null) itsGameOver();
		if (partidaAcabada && Input.GetKeyDown ("return")) Application.LoadLevel ("Menu");
	}

	void itsGameOver(){ partidaAcabada = true; visible();}

	void visible(){ GetComponent<Renderer>().enabled = true; Invoke("invisible", 0.3f); }
	void invisible(){ GetComponent<Renderer>().enabled = false; Invoke("visible", 0.3f); }
}
