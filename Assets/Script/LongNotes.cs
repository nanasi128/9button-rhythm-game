using UnityEngine;
using System.Collections;

public class LongNotes : MonoBehaviour {
	private Mesh mesh;
	private Vector3[] newVertices = new Vector3[4];
	private Vector3 LaunchPos = new Vector3 (0.0f, 1.58f, 0.0f);
	public bool IsEnd = false;
	public bool IsTapped = false;
	private Vector3[] Vector = {new Vector3(0.0f,1.0f,0.0f) ,new Vector3(-1.0f,3.7f/1.5f,0.0f),new Vector3(-1.0f,1.0f,0.0f),new Vector3(-1.0f,1.54f/3.68f,0.0f),
		new Vector3(-1.0f,0.0f,0.0f),new Vector3(-1.0f,-1.54f/3.68f,0.0f),new Vector3(-1.0f,-1.0f,0.0f),new Vector3(-1.0f,-3.7f/1.5f,0.0f),new Vector3(0.0f,-1.0f,0.0f)};
	public int n; //行き先
	private Vector3[] notesVec = {new Vector3(-4.0f,0.0f,0.0f) ,new Vector3(-3.7f,-1.5f,0.0f),new Vector3(-2.82f,-2.81f,0.0f),new Vector3(-1.54f,-3.68f,0.0f),
		new Vector3(0.0f,-4.08f,0.0f),new Vector3(1.54f,-3.68f,0.0f),new Vector3(2.82f,-2.81f,0.0f),new Vector3(3.7f,-1.5f,0.0f),new Vector3(4.0f,0.0f,0.0f)};
	// Use this for initialization
	void Awake () {
		mesh = new Mesh ();
		Vector2[] newUV = new Vector2[4];
		int[] newTriangles = new int[2 * 3];

		newVertices[0] = new Vector3(0.0f, 1.58f, 0.0f);
		newVertices[1] = new Vector3(0.0f, 1.58f, 0.0f);
		newVertices[2] = new Vector3(0.0f, 1.58f, 0.0f);
		newVertices[3] = new Vector3(0.0f, 1.58f, 0.0f);

		newUV[0] = new Vector2(0.0f, 0.0f);
		newUV[1] = new Vector2(0.0f, 1.0f);
		newUV[2] = new Vector2(1.0f, 1.0f);
		newUV[3] = new Vector2(1.0f, 0.0f);

		newTriangles[0] = 2;
		newTriangles[1] = 1;
		newTriangles[2] = 0;
		newTriangles[3] = 0;
		newTriangles[4] = 3;
		newTriangles[5] = 2;

		mesh.vertices = newVertices;
		mesh.uv = newUV;
		mesh.triangles = newTriangles;

		mesh.RecalculateNormals();
		mesh.RecalculateBounds ();

		GetComponent<MeshFilter> ().sharedMesh = mesh;
		GetComponent<MeshFilter> ().sharedMesh.name = "mymesh";
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 diff = newVertices [1] - newVertices [2];
		Vector3 diff_up = newVertices [3] - newVertices [0];
		if (IsEnd) {
			if(diff_up.magnitude < 1.4){
				newVertices [0] += (Vector [n].normalized * Time.deltaTime * 0.6f);
				newVertices [3] -= (Vector [n].normalized * Time.deltaTime * 0.6f);
				newVertices [0] += (notesVec [n].normalized * Time.deltaTime * 4);
				newVertices [3] += (notesVec [n].normalized * Time.deltaTime * 4);
				mesh.vertices = newVertices;
				
				mesh.RecalculateNormals ();
				mesh.RecalculateBounds ();
			}else{
				Destroy(this.gameObject);
			}
		}
		if (diff.magnitude < 1.4 && !IsTapped) {
			newVertices [1] += (Vector [n].normalized * Time.deltaTime * 0.6f);
			newVertices [2] -= (Vector [n].normalized * Time.deltaTime * 0.6f);
			newVertices [1] += (notesVec [n].normalized * Time.deltaTime * 4.0f);
			newVertices [2] += (notesVec [n].normalized * Time.deltaTime * 4.0f);
			mesh.vertices = newVertices;
		
			mesh.RecalculateNormals ();
			mesh.RecalculateBounds ();
		}
		if (IsTapped) {
			newVertices [1] = notesVec [n] + (Vector [n].normalized * 0.6f) + new Vector3(0.0f,1.58f,0.0f);
			newVertices [2] = notesVec [n] - (Vector [n].normalized * 0.6f) + new Vector3(0.0f,1.58f,0.0f);

			mesh.vertices = newVertices;
			
			mesh.RecalculateNormals ();
			mesh.RecalculateBounds ();
		}
	}
}
