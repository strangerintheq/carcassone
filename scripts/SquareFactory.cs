using System;
using UnityEngine;

public static class SquareFactory {

	public static GameObject create() {
		GameObject square = GameObject.CreatePrimitive(PrimitiveType.Cube);
		square.transform.localScale = new Vector3(1, 0.1f, 1);
		square.GetComponent<MeshRenderer> ().material.shader = Shader.Find ("Unlit/Texture");
		foreach(Collider collider in square.GetComponents<Collider>()) {
			collider.enabled = false;
		}
		return square;
	}

}

