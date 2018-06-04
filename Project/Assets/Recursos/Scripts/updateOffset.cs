using UnityEngine;
using System.Collections;

public class updateOffset : MonoBehaviour {

	public float speed = 0.5f;

	void Update(){ GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (0, (Time.time * speed) % 1); }

}
