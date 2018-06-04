using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

	private SpriteRenderer miRenderer;
	private float fadeSpeed = 2.5f;

	void Start(){ miRenderer = GetComponent<SpriteRenderer>(); }

	void Update () {
		float miA = Mathf.Lerp (miRenderer.color.a, 0, Time.deltaTime * fadeSpeed);
		if (miRenderer.color.a < 0.01f) Destroy (this.gameObject);
		else miRenderer.color = new Color(1f,1f,1f,miA);
	}
}
