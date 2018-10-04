using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMeshData : MonoBehaviour {

	public static Vector3[] vertices = {
		new Vector3(1,1,1),
		new Vector3(-1,1,1),
		new Vector3(-1,-1,1),
		new Vector3(1,-1,1),
		new Vector3(-1,1,-1),
		new Vector3(1,1,-1),
		new Vector3(1,-1,-1),
		new Vector3(-1,-1,-1)
	};

	public static int[][] faceTriangles = {
		new int[]{0,1,2,3},
		new int[]{5,0,3,6},
		new int[]{0,1,2,3},
		new int[]{0,1,2,3},
		new int[]{0,1,2,3},
		new int[]{0,1,2,3}
	};
}
