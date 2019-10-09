using UnityEngine;
using System.Collections;

public class Notes : MonoBehaviour {
	private Vector2[] notesVec = {new Vector2(-4.0f,0.0f) ,new Vector2(-3.7f,-1.5f),new Vector2(-2.82f,-2.81f),new Vector2(-1.54f,-3.68f),
					new Vector2(0.0f,-4.08f),new Vector2(1.54f,-3.68f),new Vector2(2.82f,-2.81f),new Vector2(3.7f,-1.5f),new Vector2(4.0f,0.0f)};
	public int target;
	public float speed;
	public int id;
	private NotesLaunch script;
	// Use this for initialization
	void Awake () {
		script = GameObject.Find ("NotesLauncher").GetComponent<NotesLaunch>();
		speed = script.speed;
		StartCoroutine (Launch ());
		StartCoroutine (destroy());
	}
	IEnumerator Launch(){
		yield return null;
		gameObject.GetComponent <Rigidbody2D> ().velocity = notesVec [target].normalized * speed;
	}
	IEnumerator destroy(){
		float time = 4 / speed + 0.2f;
		yield return new WaitForSeconds (time);
		script.targets[target].SendMessage("Disappearance");
		Destroy (this.gameObject);
	}
	void OnTapped(int i){
		if (i == id) {
			Destroy (this.gameObject);
		}

	}
	void Auto(int i){
		if (i == id) {
			Destroy (this.gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x < 1) {
			transform.localScale = new Vector3 (transform.localScale.x + Time.deltaTime * speed / 4, transform.localScale.y + Time.deltaTime * speed / 4, 0);
		}	
	}
}

