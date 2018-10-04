using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mesh), typeof(MeshRenderer))]
public class Room : MonoBehaviour {

	public Vector2 roomSize;
	Mesh mesh;
	List<Vector3> vertices;
	List<int> triangles;
	
	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
	}

	public Room(){
		roomSize = new Vector2(30,10);
	}

	public Room(Vector2 roomSize){
		this.roomSize = roomSize;
	}

	void CreateRoom(){
		vertices = new List<Vector3>();
		triangles = new List<int>();

		for(int i = 0; i < 6; i++){
			CreateWall(i);
		}
	}

	void CreateWall(int dir){
		//vertices.AddRange()
	}

	void updateMesh(){
		mesh.Clear();

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();
	}

}
