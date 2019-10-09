using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
	private int i,j;
	private int id;
	private GameObject[] judge;
	public Combo_Manager Combo_Manager;
	public GameObject[] notes;
	public LayerMask mask;
	public string num;
	private NotesLaunch script;
	private float speed;
	public bool Auto;
	private bool IsTapped;
	private AudioSource Audio;
	// Use this for initialization
	void Start () {
		notes = new GameObject [256];
		judge = new GameObject [5];
		Combo_Manager = GameObject.Find ("combo").GetComponent<Combo_Manager>();
		script = GameObject.Find("NotesLauncher").GetComponent<NotesLaunch> ();
		Audio = this.gameObject.GetComponent<AudioSource> ();
		// 判定を設定
		judge [0] = Resources.Load ("Prefab/Perfect") as GameObject;
		judge [1] = Resources.Load ("Prefab/Great") as GameObject;
		judge [2] = Resources.Load ("Prefab/GOOD") as GameObject;
		judge [3] = Resources.Load ("Prefab/BAD") as GameObject;
		judge [4] = Resources.Load ("Prefab/MISS") as GameObject;
	}

	// Update is called once per frame
	void OnTapped() {
		bool IsMiss = false;
		if (notes [id] != null) {
			float diff = (this.transform.position - notes [id].transform.position).magnitude / script.speed;
			if (diff <= 0.04f) {
				Combo_Manager.combo_cal();
				Instantiate (judge [0], Vector3.zero, transform.rotation);
				Audio.Play ();
				notes [id].SendMessage ("OnTapped",id);
			} else if (diff > 0.04f && diff <= 0.10f) {
				Instantiate (judge [1], Vector3.zero, transform.rotation);
				Audio.Play ();
				notes [id].SendMessage ("OnTapped",id);
				Combo_Manager.combo_cal();
			} else if (diff > 0.10f && diff <= 0.15f) {
				Instantiate (judge [2], Vector3.zero, transform.rotation);
				Audio.Play ();
				notes [id].SendMessage ("OnTapped",id);
				Combo_Manager.Missed();
			} else if (diff > 0.15f && diff <= 0.20f) {
				Instantiate (judge [3], Vector3.zero, transform.rotation);
				Audio.Play ();
				notes [id].SendMessage ("OnTapped",id);
				Combo_Manager.Missed();
			}
			id++;
		}
	}
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit;
			hit = Physics2D.Raycast(ray, Vector2.left,1,mask);
			if(hit.collider != null){
				if (hit.collider.gameObject.tag == ("Tar" + num)) {
					OnTapped ();
				}
		}
	}
}
	void Disappearance(){
		id++;
		Instantiate (judge[4],Vector3.zero,transform.rotation);
		Combo_Manager.Missed ();
	}
	void OnTriggerStay2D(){
		if (notes[id] != null) {
			if (Auto && !IsTapped && (notes[id].transform.position - this.gameObject.transform.position).magnitude / script.speed < 0.005f) {
				IsTapped = true;
				Instantiate (judge [0], Vector3.zero, transform.rotation);
				Audio.Play ();
				Combo_Manager.combo_cal ();
				notes [id].SendMessage ("Auto", id);
				id++;
			}
			IsTapped = false;
		}
	}
	void OnLaunchNotes(GameObject Note){
		notes [i] = Note;
		i++;
	}
} 
