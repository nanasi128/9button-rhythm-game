using UnityEngine;
using System.Collections;

public class NotesLaunch : MonoBehaviour {
	[SerializeField]
	public TextAsset beatMap;
	public GameObject[] notes_type;
	public GameObject[] targets;
	public GameObject Long_Head, Long_Notes,Long_End,Long_Head_d;
	public float speed;
	private float wait, offset;
	private int[] id,notesInfo;
	private string[] eachBeat,Info;
	private bool flag,boo;
	private int n,k,l,count;
	private AudioSource audio;
	private float time = 0;
	private Vector3[] notesVec = {new Vector3(-4.0f,0.0f,0.0f) ,new Vector3(-3.7f,-1.5f,0.0f),new Vector3(-2.82f,-2.81f,0.0f),new Vector3(-1.54f,-3.68f,0.0f),
		new Vector3(0.0f,-4.08f,0.0f),new Vector3(1.54f,-3.68f,0.0f),new Vector3(2.82f,-2.81f,0.0f),new Vector3(3.7f,-1.5f,0.0f),new Vector3(4.0f,0.0f,0.0f)};
	// Use this for initialization
	void Start () {
		count = 1;
		audio = this.gameObject.GetComponent<AudioSource> ();
		speed = 4.0f;
		//GameObject[] notes_type = new GameObject[256];
		id = new int[9];
		flag = false;
		n = 2;
		eachBeat = new string[256];
		StartCoroutine(readmap ());
		MusicStart ();
	}
	IEnumerator  readmap(){
		char[] split = {'\n','/','\r'};
		eachBeat = beatMap.text.Split (split); //Split by "\n" and "/"
		wait = (int.Parse (eachBeat [0]) * 4 / 60);
		wait = 1 / wait;
		offset = float.Parse (eachBeat [1]);
		if (4 / speed < offset) {
			yield return new WaitForSeconds (offset - (4 / speed));
		}
		flag = true; 
		time = audio.time;
		boo = true;
		}
	void  LaunchNotes(int notesType,int pos){ //1:normal 2:long 3:double_normal 4: double_long
		GameObject Note = Instantiate (notes_type [notesType], transform.position, transform.rotation) as GameObject;
		Notes script =  Note.gameObject.GetComponent<Notes>(); 
		script.target = pos - 1;
		script.id = id[pos -1];
		targets [pos - 1].SendMessage ("OnLaunchNotes", Note);
		id [pos - 1]++;

	}
	void MusicStart(){
		var source = this.gameObject.GetComponent<AudioSource> ();
		if (4 / speed > offset) {
			source.PlayScheduled ((double)offset);
		}else
			source.Play ();
		//Start music
	}
	void Update(){
		if(boo && (((wait * count) + offset) - audio.time <= 0.02f && ((wait * count) + offset) - audio.time >= -0.02f)){ //再生時間からノーツ生成
			Notes ();
			count++;
			time = ((wait * count) + offset) - audio.time;
			//Debug.Log(time);
		}
		if (Input.GetMouseButtonDown (0)) {
			//Debug.Log (audio.time);
		}
	}

	IEnumerator LaunchLongNotes(int pos,int delay,bool Isdouble){
		float delaytime;
		GameObject LongHead;
		delaytime = (delay * wait);
		if (Isdouble) {
			LongHead = Instantiate (Long_Head_d, transform.position, transform.rotation) as GameObject;
		} else { 
			LongHead = Instantiate (Long_Head, transform.position, transform.rotation) as GameObject;
		}
		GameObject LongNotes = Instantiate (Long_Notes, Vector3.zero, transform.rotation) as GameObject;
		LongNotes Notes = LongNotes.GetComponent<LongNotes> ();
		Notes.n = pos - 1;
		LongHead Head = LongHead.GetComponent<LongHead> ();
		Head.LongNotes = LongNotes;
		Head.target = pos - 1;
		Head.id = id [pos - 1];
		Head.delaytime = delaytime;
		targets [pos - 1].SendMessage ("OnLaunchNotes", LongHead);
		id [pos - 1]++;
		yield return new WaitForSeconds (delaytime);
		if (targets [pos - 1].GetComponent<Target> ().notes [Head.id] != null) {
			GameObject LongEnd = Instantiate (Long_End, transform.position, transform.rotation) as GameObject;
			Head.LongEnd = LongEnd;
			var End = LongEnd.GetComponent<LongHead> ();
			End.target = pos - 1;
			Notes.IsEnd = true;
		}
	}

	// Update is called once per frame
	void  Notes() {
		if (n < eachBeat.Length) {
			flag = false;
			Info = eachBeat [n].Split ("," [0]);
			notesInfo = new int[10];
			for (int j = 0; j < Info.Length; j++) {
				notesInfo [j] = int.Parse (Info [j]);
			}
			switch (notesInfo [0]) {
			case 0: //休符
				break;
			case 1: //単押し
				LaunchNotes ((notesInfo [0] - 1), notesInfo [1]);
				break;
			case 2: //同時押し
				int index;
				if (notesInfo [1] == 1) { //通常なら
					LaunchNotes (1, notesInfo [2]);
					index = 3;
				} else {
					StartCoroutine (LaunchLongNotes (notesInfo [2], notesInfo [3], true));
					index = 4;
				}
				if (notesInfo [index] == 1) { //通常なら
					LaunchNotes (1, notesInfo [index + 1]);
				} else {
					StartCoroutine (LaunchLongNotes (notesInfo [index + 1], notesInfo [index + 2], true));
				}
				break;
			case 3: //ロングノーツ
				StartCoroutine(LaunchLongNotes(notesInfo[1],notesInfo[2],false));
				break;

			}
			n++;
			}
	}
}
