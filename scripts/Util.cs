using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Util {

	public static bool HasObjectWithLocation (List<GameObject> objects, Vector3 hit) {
		return null != GetObjectWithLocation(objects, hit);
	}

	public static GameObject GetObjectWithLocation (List<GameObject> objects, Vector3 point) {
		foreach (GameObject obj in objects) {
			if (IsSamePoint(point, obj.transform.localPosition)) {
				return obj;
			}
		}
		return null;
	}

	public static bool IsSamePoint(Vector3 v1, Vector3 v2) {
		return Mathf.Approximately (v1.x, v2.x) && Mathf.Approximately (v1.z, v2.z);
	}

	public static bool isEmptyVector (Vector3 vec) {
		return Vector3.zero.Equals (vec);
	}
}
