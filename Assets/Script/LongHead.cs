using UnityEngine;
using System.Collections;

public class LongHead : MonoBehaviour {
	bool IsTapped;
	private Vector3[] notesVec = {new Vector3(-4.0f,0.0f,0.0f) ,new Vector3(-3.7f,-1.5f,0.0f),new Vector3(-2.82f,-2.81f,0.0f),new Vector3(-1.54f,-3.68f,0.0f),
		new Vector3(0.0f,-4.08f,0.0f),new Vector3(1.54f,-3.68f,0.0f),new Vector3(2.82f,-2.81f,0.0f),new Vector3(3.7f,-1.5f,0.0f),new Vector3(4.0f,0.0f,0.0f)};
	public int target;
	public float speed;
	NotesLaunch script;
	public int id;
	public GameObject[] judge;
	public GameObject LongNotes;
	public GameObject LongEnd;
	public float delaytime;
	private AudioSource Audio;
	// Use this for initialization
	void Awake () {
		script = GameObject.Find ("NotesLauncher").GetComponent<NotesLaunch>();
		speed = script.speed;
		Audio = GameObject.Find ("Audio").GetComponent <AudioSource>();
		StartCoroutine (Launch ());
		StartCoroutine (destroy());
	}
	void OnTapped(int i){
		if (i == id) {
			IsTapped = true;
			this.gameObject.transform.position = notesVec [target] + new Vector3(0.0f,1.58f,0.0f) ;
			this.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			LongNotes.GetComponent<LongNotes>().IsTapped = true;
		}
	}
	IEnumerator AutoOnTapped(int i){
		if (i == id) {
			IsTapped = true;
			this.gameObject.transform.position = notesVec [target] + new Vector3 (0.0f, 1.58f, 0.0f);
			this.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			LongNotes.GetComponent<LongNotes> ().IsTapped = true;
			yield return new WaitForSeconds (delaytime);
			Judge ();
		}
	}
	void Auto(int i){
		StartCoroutine (AutoOnTapped (i));
	}
	void Missed(int i){
		if (i == id) {
			Destroy (this.gameObject);
			Destroy (LongNotes);
			Destroy (LongEnd);
		}
	}
	IEnumerator Launch(){
		yield return null;
		gameObject.GetComponent <Rigidbody2D> ().velocity = notesVec [target].normalized * speed;
	}
	IEnumerator destroy(){
		float time = 4 / speed + 0.2f;
		yield return new WaitForSeconds (time);
		if (!IsTapped) {
			script.targets [target].SendMessage ("Disappearance");
			Destroy (this.gameObject);
			Destroy(LongNotes);
			Destroy(LongEnd);
		}
	}
	void Judge(){
		float diff = 100;
		if (LongEnd != null) {
			diff = ((LongEnd.transform.position) - (this.gameObject.transform.position)).magnitude / speed;
		}
		if (diff < 0.05f) {
			Instantiate (judge [0], Vector3.zero, transform.rotation);
			Audio.Play ();
		} else if (diff > 0.05f && diff < 0.15f) {
			Instantiate (judge [1], Vector3.zero, transform.rotation);
			Audio.Play ();
		} else {
			Debug.Log ("Miss");
		}
		Destroy (this.gameObject);
		Destroy (LongNotes);
		Destroy (LongEnd);
	}
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x < 1) {
			transform.localScale = new Vector3 (transform.localScale.x + Time.deltaTime * speed / 4, transform.localScale.y + Time.deltaTime * speed / 4, 0);
		}	
		if ((IsTapped && Input.GetMouseButtonUp (0))) {
			Judge ();
		}

	}
}
