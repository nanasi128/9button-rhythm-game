using UnityEngine;
using System.Collections;

public class JudgeAnimation : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		StartCoroutine (DeleteThis ());
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.localScale.x < 0.5) {
			this.transform.localScale += new Vector3 (Time.deltaTime, Time.deltaTime, 0);
		}
	}
	IEnumerator DeleteThis(){
		yield return new WaitForSeconds (0.2f);
		Destroy (this.gameObject);
	}
}
