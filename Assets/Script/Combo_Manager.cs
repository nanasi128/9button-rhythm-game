using UnityEngine;
using System.Collections;

public class Combo_Manager : MonoBehaviour {
	public GameObject[] Numbers_digit;
	private SpriteRenderer[] Numbers_comp;
	private SpriteRenderer combo_comp;
	private int combo_num;
	public int k;
	//public GameObject combo;
	public Sprite[] Numbers; //数字の画像
	void Start(){
		combo_comp = this.gameObject.GetComponent<SpriteRenderer> ();
		Numbers_digit = new GameObject[3];
		Numbers_comp = new SpriteRenderer[3];
		Numbers_digit [0] = GameObject.Find ("Number_0");
		Numbers_digit [1] = GameObject.Find ("Number_1");
		Numbers_digit [2] = GameObject.Find ("Number_2");
		for (int i = 0; i < 3; i++) { 					//SpriteRenderer取得
			Numbers_comp [i] = Numbers_digit [i].GetComponent<SpriteRenderer> ();
		}
	}
	public void combo_cal(){
		if (!combo_comp.enabled) {
			combo_comp.enabled = true;
		}
		combo_num++;
		if (!Numbers_comp [0].enabled) {
			Numbers_comp [0].enabled = true;
		}
		if (combo_num < 10) { //コンボ数一桁の時
			Numbers_comp [0].sprite = Numbers [combo_num];
		}
		if (combo_num < 100 && combo_num >= 10) { //コンボ数二桁の時
			if (!Numbers_comp [1].enabled) {
				Numbers_comp [1].enabled = true;
			}
			Numbers_comp [0].sprite = Numbers [combo_num % 10];
			Numbers_comp [1].sprite = Numbers [combo_num / 10];
		}
		if (combo_num >= 100) { //コンボ数三桁の時
			if (!Numbers_comp [2].enabled) {
				Numbers_comp [2].enabled = true;
			}
			Numbers_comp [0].sprite = Numbers [combo_num % 10];
			Numbers_comp [1].sprite = Numbers [(combo_num % 100) / 10];
			Numbers_comp [2].sprite = Numbers [combo_num / 100];
		}
	}
	public void Missed(){
		if(combo_comp.enabled){
			combo_comp.enabled = false;
		}
		combo_num = 0;
		Numbers_comp [0].enabled = false;
		Numbers_comp [1].enabled = false;
		Numbers_comp [2].enabled = false;

	}
	void Update(){
	}
}
